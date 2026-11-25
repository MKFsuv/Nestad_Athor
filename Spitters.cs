using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nestad_Athor
{
    public class Spitters
    {
        public float x;
        public float y;
        public float chaseSpeed = 50;

        public Spitters(float initialX, float initialY)
        {
            x = initialX;
            y = initialY;
            
        }

        public int maxHealth = 20;
        public int health = 20;
        public bool Alive = true;
        public bool hitThisSwing = false;  

        public float shootTimer = 0;
        public float ShootInterval = 0.5f;
        public float TearSpeed = 500;

        public List<Tears> tearList = new List<Tears>();

        public static void SpittersMovement(Game1 game, GameTime gameTime, Player player, Spitters spitters, Tears tears)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle playerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            if (spitters.Alive)
            {
         
                float dx = player.x - spitters.x;
                float dy = player.y - spitters.y;
                float distance = MathF.Sqrt(dx * dx + dy * dy);

                if (distance > 0)
                {
                    spitters.x += (dx / distance) * spitters.chaseSpeed * deltaTime;
                    spitters.y += (dy / distance) * spitters.chaseSpeed * deltaTime;
                }

                Rectangle spittersHitbox = new Rectangle((int)spitters.x, (int)spitters.y, 100, 100);

           
                if (!player.IsAttacking)
                {
                    spitters.hitThisSwing = false;
                }
                else if (!spitters.hitThisSwing && player.AttackHitbox.Intersects(spittersHitbox))
                {
                    spitters.health -= player.Damage;
                    spitters.hitThisSwing = true;
                }

                if (spitters.health <= 0)
                {
                    spitters.Alive = false;
                }

              
                if (spitters.Alive)
                {
                    spitters.shootTimer -= deltaTime;
                    if (spitters.shootTimer <= 0f)
                    {
                        spitters.shootTimer = spitters.ShootInterval;

                        Vector2 spitterCenter = new Vector2(spitters.x + 50, spitters.y + 50);
                        Vector2 playerCenter = new Vector2(player.x + 25, player.y + 25);
                        Vector2 direction = playerCenter - spitterCenter;

                        if (direction != Vector2.Zero)
                        {
                            direction.Normalize();
                            spitters.tearList.Add(new Tears(spitterCenter.X, spitterCenter.Y)
                            {
                                vx = direction.X * spitters.TearSpeed,
                                vy = direction.Y * spitters.TearSpeed
                            });
                        }
                    }
                }
            }

            foreach (Tears tear in spitters.tearList)
            {
                tear.Update(deltaTime);

                Rectangle tearHitbox = new Rectangle((int)tear.x - 10, (int)tear.y - 4, 20, 8);
                if (tear.on && tearHitbox.Intersects(playerHitbox))
                {
                    player.Health -= 10;
                    tear.on = false;
                }
            }

            spitters.tearList.RemoveAll(t => !t.on);
        }

        public static void SpittersDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D spittersTexture, Spitters spitters, Tears tears)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(game.EnemyHealthBarTexture, new Rectangle((int)spitters.x, (int)spitters.y - 21, 102, 12), Color.Black);
            spriteBatch.Draw(game.EnemyHealthBarTexture, new Rectangle((int)spitters.x + 1, (int)spitters.y - 20, (int)spitters.health * 5, 10), Color.Red);

            spriteBatch.Draw(spittersTexture, new Rectangle((int)spitters.x, (int)spitters.y, 100, 100), Color.Green);


            foreach (Tears tear in spitters.tearList)
            {
                if (tear.on)
                {
                    spriteBatch.Draw(
                        game.TearsTexture,
                        new Rectangle(
                            (int)tear.x,
                            (int)tear.y,
                            40,
                            40
                        ),
                        Color.Blue
                    );
                }
            }

            spriteBatch.End();
        }
    }
}
