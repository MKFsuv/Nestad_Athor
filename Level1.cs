using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



namespace Nestad_Athor
{
    public class Level1
    {
        public static void Level1Update(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            Rectangle Level1Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level1Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level1Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level1Wall4 = new Rectangle(0, 1060, 1800, 20);

            CancerousCell.CancerousCellMovement1(gameTime, player, cancerousCell);

            Player.PlayerMovement(gameTime, player);
        }
        public static void Level1Drawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Texture2D LevelWalls, Player player, CancerousCell cancerousCell, 
        Texture2D cancerousCellTexture)
        {

            Player.PlayerDrawn(game, spriteBatch, playerTexture, player);
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell);
            spriteBatch.Begin();
            
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.Tan);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.Tan);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.Tan);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.Tan);



            spriteBatch.End();
        }


    }
}
