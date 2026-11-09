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

        public CancerousCell(float initialX, float initialY)
        {
            x = initialX;
            y = initialY;
        }

        public int health = 100;
        public int maxHealth = 100;
        public float chaseSpeed = 200;
        public bool CanDash = true;
        public float dashCooldown = 4;
        public float dashTimer = 2;
        public float dashSpeed = 2000;





        public static void BadGuyTakings()
        {
            CancerousCell cancerousCell = new CancerousCell(1200, 400);
            CancerousCell cancerousCell2 = new CancerousCell(1000, 400);
            CancerousCell cancerousCell3 = new CancerousCell(1400, 400);
        }

        public static void CancerousCellMovement(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


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

        }

        //Used to make the visuals of the emeny
        public static void CancerousCellDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D cancerousCellTexture, CancerousCell cancerousCell)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cancerousCellTexture, new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 100, 100), Color.Red);
            spriteBatch.End();
        }


    }
}
