using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Nestad_Athor
{
    public class TumorBoss
    {
        public float x = 825;   
        public float y = 465;
        public int Size = 150;
        public int maxHealth = 500;
        public int health = 500;
        public bool hitThisSwing = false;

        public bool Alive = true;
        public float shootTimer = 0;
        public float ShootInterval = 0f;
        public float TearSpeed = 500;

        public List<Tears> tearList = new List<Tears>();

        public Rectangle Hitbox => new Rectangle((int)x, (int)y, Size, Size);

        public void TumorBossLogic(GameTime gameTime, Player player)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle playerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            if (Alive)
            {
                
                if (!player.IsAttacking)
                {
                    hitThisSwing = false;
                }
                else if (!hitThisSwing && player.AttackHitbox.Intersects(Hitbox))
                {
                    health -= 10;
                    hitThisSwing = true;
                }

                if (health <= 0)
                {
                    Alive = false;
                }
            }

            if (Alive)
            {
                shootTimer -= deltaTime;
                if (shootTimer <= 0f)
                {
                    shootTimer = ShootInterval;

                    Vector2 bossCenter = new Vector2(x + Size / 2f, y + Size / 2f);
                    Vector2 playerCenter = new Vector2(player.x + 25, player.y + 25);
                    Vector2 direction = playerCenter - bossCenter;

                    if (direction != Vector2.Zero)
                    {
                        direction.Normalize();
                        tearList.Add(new Tears(bossCenter.X, bossCenter.Y)
                        {
                            vx = direction.X * TearSpeed,
                            vy = direction.Y * TearSpeed
                        });
                    }
                }
            }

            // Tears keep flying even if the boss dies
            foreach (Tears tear in tearList)
            {
                tear.Update(deltaTime);

                Rectangle tearHitbox = new Rectangle((int)tear.x - 10, (int)tear.y - 4, 20, 8);
                if (tear.on && tearHitbox.Intersects(playerHitbox))
                {
                    player.Health -= 10;
                    tear.on = false;
                }
            }

            tearList.RemoveAll(t => !t.on);
        }



        public void TumorBossDraw(Game1 game, SpriteBatch spriteBatch, Texture2D bossTexture)
        {
            spriteBatch.Begin();

            
            Texture2D tearTex = game.TearsTexture;
            Vector2 tearOrigin = new Vector2(tearTex.Width / 2f, tearTex.Height / 2f);
            Vector2 tearScale = new Vector2(20f / tearTex.Width, 8f / tearTex.Height);

            foreach (Tears tear in tearList)
            {
                float angle = (float)Math.Atan2(tear.vy, tear.vx);
                spriteBatch.Draw(tearTex, new Vector2(tear.x, tear.y), null, Color.White,
                    angle, tearOrigin, tearScale, SpriteEffects.None, 0f);
            }

            if (Alive)
            {
                
                spriteBatch.Draw(bossTexture, Hitbox, Color.Purple);

                
                float percent = MathHelper.Clamp((float)health / maxHealth, 0f, 1f);
                int barWidth = Size; 

                spriteBatch.Draw(game.EnemyHealthBarTexture,
                    new Rectangle((int)x - 2, (int)y - 24, barWidth + 4, 16), Color.Black);
                spriteBatch.Draw(game.EnemyHealthBarTexture,
                    new Rectangle((int)x, (int)y - 22, (int)(barWidth * percent), 12), Color.Red);
            }

            spriteBatch.End();
        }











    }
}