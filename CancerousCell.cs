using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Nestad_Athor
{
    public class CancerousCell
    {

        public float x = 1200;
        public float y = 400;
        public float x2 = 1000;
        public float y2 = 400;
        public int health = 100;
        public int maxHealth = 100;
        public float chaseSpeed = 200;
        public float chaseSpeed2 = 300;

        public static void CancerousCellMovement1(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            float dx = player.x - cancerousCell.x;
            float dy = player.y - cancerousCell.y;

            float distance = MathF.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                cancerousCell.x += (dx / distance) * cancerousCell.chaseSpeed * deltaTime;
                cancerousCell.y += (dy / distance) * cancerousCell.chaseSpeed * deltaTime;
            }


        }

        public static void CancerousCellDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D cancerousCellTexture, CancerousCell cancerousCell)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cancerousCellTexture, new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 100, 100), Color.Red);
            spriteBatch.End();
        }

        public static void CancerousCellMovement2(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            float dx = player.x - cancerousCell.x2;
            float dy = player.y - cancerousCell.y2;

            float distance = MathF.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                cancerousCell.x2 += (dx / distance) * cancerousCell.chaseSpeed2 * deltaTime;
                cancerousCell.y2 += (dy / distance) * cancerousCell.chaseSpeed2 * deltaTime;
            }


        }

        public static void CancerousCellDrawn2(Game1 game, SpriteBatch spriteBatch, Texture2D cancerousCellTexture, CancerousCell cancerousCell)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cancerousCellTexture, new Rectangle((int)cancerousCell.x2, (int)cancerousCell.y2, 100, 100), Color.Red);
            spriteBatch.End();
        }

    }
}
