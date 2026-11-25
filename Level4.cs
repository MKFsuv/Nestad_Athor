using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nestad_Athor
{
    public class Level4
    {



        public static void Level4Update(Game1 game, 
            GameTime gameTime, 
            Player player, 
            CancerousCell cancerousCell, 
            Spitters spitter, 
            Tears tears)

        {
            Rectangle Level2Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level2Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level2Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level2Wall4 = new Rectangle(0, 1060, 1800, 20);

            Rectangle Door = new Rectangle(0, 270, 21, 540);
            Rectangle Door2 = new Rectangle(1779, 270, 21, 540);
            Rectangle PlayerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);
            

            Spitters.SpittersMovement(game, gameTime, player, spitter, tears);

            player.PlayerMovement(gameTime, player, cancerousCell);



            if (PlayerHitbox.Intersects(Door))
            {
                game.Level = 3;
                player.x = 1700;
            }
            else if (PlayerHitbox.Intersects(Door2))
            {
                game.Level = 5;
                player.x = 100;
            }
        }

        public static void Level4Drawn(Game1 game, 
            SpriteBatch spriteBatch, 
            Texture2D playerTexture, 
            Texture2D LevelWalls, 
            Player player, 
            CancerousCell cancerousCell, 
            Texture2D PlayerAttackTexture, Tears tears,
            Spitters spitter, Texture2D Door)

        {
            spriteBatch.Begin();

            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 1080), Color.White);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.DarkRed);
            //Door

            spriteBatch.Draw(Door, new Rectangle(0, 270, 21, 540), Color.MediumVioletRed);
            spriteBatch.Draw(Door, new Rectangle(1779, 270, 21, 540), Color.MediumVioletRed);


            spriteBatch.End();

            Spitters.SpittersDrawn(game, spriteBatch, game.SpittersTexture, spitter, tears);

            player.PlayerDrawn(game, 
                spriteBatch, 
                playerTexture, 
                game.chemoDead, 
                game.HealthBarTexture, 
                PlayerAttackTexture, 
                player);
        }

    }
}
