using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Hello git
namespace Nestad_Athor
{
    public class CancerousCell
    {
        //Variables for each type of enemy
        public float x;
        public float y;
        public float chaseSpeed;

        public CancerousCell(float initialX, float initialY, float initialchaseSpeed)
        {
            x = initialX;
            y = initialY;
            chaseSpeed = initialchaseSpeed;
        }

        public int health = 100;
        public bool CanDash = true;
        public float dashCooldown = 4;
        public float dashTimer = 2;
        public float dashSpeed = 2000;
        public bool Alive = true;




        //Code that makes the enemy move and other shi
        public static void CancerousCellMovement(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            if (cancerousCell.Alive)
            {
                Rectangle cancerousCellHitbox = new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 100, 100);
                Rectangle playerHitbox = new Rectangle(
                (int)player.x,
                (int)player.y,
                50,
                50);

                if (cancerousCell.health <= 0)
                {
                    cancerousCell.Alive = false;
                }

                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (player.IsAttacking)
                {
                    if (player.AttackHitbox.Intersects(cancerousCellHitbox))
                    {
                        cancerousCell.health -= player.Damage;
                    }
                }


                float dx = player.x - cancerousCell.x;
                float dy = player.y - cancerousCell.y;

                float distance = MathF.Sqrt(dx * dx + dy * dy);

                //Movement of ememy
                if (distance > 0)
                {
                    cancerousCell.x += (dx / distance) * cancerousCell.chaseSpeed * deltaTime;
                    cancerousCell.y += (dy / distance) * cancerousCell.chaseSpeed * deltaTime;
                }

                // Dash cooldown
                if (!cancerousCell.CanDash)
                {
                    cancerousCell.dashTimer += deltaTime;

                    if (cancerousCell.dashTimer >= cancerousCell.dashCooldown)
                    {
                        cancerousCell.CanDash = true;
                        cancerousCell.dashTimer = 0f;
                    }
                }

                // Dash attack
                if (cancerousCell.CanDash && distance <= 200 && distance > 0)
                {
                    cancerousCell.x += (dx / distance) * cancerousCell.dashSpeed * deltaTime;
                    cancerousCell.y += (dy / distance) * cancerousCell.dashSpeed * deltaTime;

                    cancerousCell.CanDash = false;
                }


                if (cancerousCellHitbox.Intersects(playerHitbox) && player.hitCooldown <= 0f)
                {
                    player.Health -= 10;
                    player.hitCooldown = player.HitCooldownDuration;
                }

            }

        }


        //Used to make the visuals of the emeny
        public static void CancerousCellDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D cancerousCellTexture, CancerousCell cancerousCell)
        {
            spriteBatch.Begin();

             spriteBatch.Draw(game.EnemyHealthBarTexture, new Rectangle((int)cancerousCell.x, (int)cancerousCell.y - 21, 102, 12), Color.Black);
            spriteBatch.Draw(game.EnemyHealthBarTexture, new Rectangle((int)cancerousCell.x + 1, (int)cancerousCell.y - 20, (int)cancerousCell.health, 10), Color.Red);

            spriteBatch.Draw(cancerousCellTexture, new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 100, 100), Color.Red);

            spriteBatch.End();
        }


    }
}
