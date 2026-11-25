using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Nestad_Athor
{
    public class BouncingBall
    {
        public Vector2 Position;   // top-left corner
        public Vector2 Velocity;
        public int Size = 40;
        public float LifeLeft;
        public bool on = true;

        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Size, Size);
    }

    public class TumorBoss
    {
        public float x = 825;
        public float y = 465;
        public int Size = 150;
        public int maxHealth = 100;
        public int health = 100;
        public bool hitThisSwing = false;
        public bool Alive = true;

        // Tear attack
        public float shootTimer = 0;
        public float ShootInterval = 0f;      
        public float TearSpeed = 500;
        public float TearAttackDuration = 5f;  
        float tearAttackTimeLeft = 0f;

        // Ball attack
        public float BallSpeed = 450;
        public float BallLifetime = 8f;          // seconds before the balls disappear
        public int BallCount = 8;

        // Attack scheduling
        public float AttackDelay = 2f;           // pause between attacks
        float attackCooldown = 2f;               // time until the next attack starts
        Random random = new Random();

        public List<Tears> tearList = new List<Tears>();
        public List<BouncingBall> balls = new List<BouncingBall>();

        public Rectangle Hitbox => new Rectangle((int)x, (int)y, Size, Size);

        public void TumorBossLogic(GameTime gameTime, Player player, SoundEffect minigun, SoundEffect minigunEnd)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle playerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            if (Alive)
            {
                // Player's attack: one hit per swing
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
                    balls.Clear();
                    tearAttackTimeLeft = 0f;
                }
            }

            if (Alive)
            {
                // Tear shoots
                if (tearAttackTimeLeft > 0f)
                {
                    tearAttackTimeLeft -= deltaTime;

                    shootTimer -= deltaTime;
                    if (shootTimer <= 0f)
                    {
                        shootTimer = ShootInterval;
                        FireTear(player);
                        //minigun.Play();
                    }

                    // SHwooooooooooooooooooooooooooooooooooot just ended: start the pause before the next attack
                    if (tearAttackTimeLeft <= 0f)
                    {
                        attackCooldown = AttackDelay;
                    }
                }
                else
                {
                    // Waiting for the next attack
                    attackCooldown -= deltaTime;

                    if (attackCooldown <= 0f)
                    {
                        if (random.Next(2) == 0)
                        {
                            // Attack 1: tear shotss
                            tearAttackTimeLeft = TearAttackDuration;
                            shootTimer = 0f;
                        }
                        else
                        {
                            // Attack 2: bouncing balls
                            SpawnBalls();
                            attackCooldown = AttackDelay;
                        }
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
                    player.Health -= 0.5f;
                    tear.on = false;
                }
            }
            tearList.RemoveAll(t => !t.on);

            // Balls: move, bounce off the room walls, hurt the player
            foreach (BouncingBall ball in balls)
            {
                ball.Position += ball.Velocity * deltaTime;

                // Same wall limits as the player: x 19 to 1780, y 19 to 1060
                if (ball.Position.X < 19)
                {
                    ball.Position.X = 19;
                    ball.Velocity.X = Math.Abs(ball.Velocity.X);
                }
                else if (ball.Position.X + ball.Size > 1780)
                {
                    ball.Position.X = 1780 - ball.Size;
                    ball.Velocity.X = -Math.Abs(ball.Velocity.X);
                }

                if (ball.Position.Y < 19)
                {
                    ball.Position.Y = 19;
                    ball.Velocity.Y = Math.Abs(ball.Velocity.Y);
                }
                else if (ball.Position.Y + ball.Size > 1060)
                {
                    ball.Position.Y = 1060 - ball.Size;
                    ball.Velocity.Y = -Math.Abs(ball.Velocity.Y);
                }

                
                if (ball.Hitbox.Intersects(playerHitbox) && player.hitCooldown <= 0f)
                {
                    player.Health -= 10;
                    player.hitCooldown = player.HitCooldownDuration;
                }

                ball.LifeLeft -= deltaTime;
                if (ball.LifeLeft <= 0f)
                {
                    ball.on = false;
                }
            }   
            balls.RemoveAll(b => !b.on);
        }

        void FireTear(Player player)
        {
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

        void SpawnBalls()
        {
            for (int i = 0; i < BallCount; i++)
            {
                
                float angle = MathHelper.ToRadians(random.Next(20, 70) + 90 * random.Next(4));
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                balls.Add(new BouncingBall
                {
                    Position = new Vector2(x + Size / 2f - 20, y + Size / 2f - 20),
                    Velocity = direction * BallSpeed,
                    LifeLeft = BallLifetime
                });
            }
        }

        public void TumorBossDraw(Game1 game, 
            SpriteBatch spriteBatch, 
            Texture2D tumorBossTexture)

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

            foreach (BouncingBall ball in balls)
            {
                spriteBatch.Draw(game.EnemyHealthBarTexture, ball.Hitbox, Color.Orange);
            }

            if (Alive)
            {
                spriteBatch.Draw(tumorBossTexture, Hitbox, Color.Purple);

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