using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nestad_Athor
{
    public class Level2
    {
        public static void Level2Update(Game1 game, GameTime gameTime, Player player, CancerousCell cancerousCell, CancerousCell cancerousCell2)
        {
            Rectangle Level2Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level2Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level2Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level2Wall4 = new Rectangle(0, 1060, 1800, 20);

            Rectangle Door = new Rectangle(0, 270, 21, 540);

            Rectangle PlayerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            if (PlayerHitbox.Intersects(Door))
            {
                game.Level = 1;

                player.x = 1600;
            }


            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell);
            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell2);
            Player.PlayerMovement(gameTime, player);
        }

        public static void Level2Drawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Texture2D playerAttackTexture, Texture2D LevelWalls, 
           Player player, CancerousCell cancerousCell, CancerousCell cancerousCell2,
           Texture2D cancerousCellTexture, Texture2D playerHitboxTexture)
        {

            
            
            spriteBatch.Begin();

            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 1080), Color.DarkBlue);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.Green);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.Green);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.Green);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.Green);


            //Door
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 270, 21, 540), Color.Red);

            spriteBatch.End();

            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell);
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell2);

            Player.PlayerDrawn(game, spriteBatch, playerTexture, playerAttackTexture, player);
        }
    }
}
