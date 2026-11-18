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


        public static void Level1Update(Game1 game, GameTime gameTime, Player player, CancerousCell cancerousCell)
        {

            Rectangle Level2Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level2Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level2Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level2Wall4 = new Rectangle(0, 1060, 1800, 20);

            Rectangle Door = new Rectangle(1779, 270, 21, 540);
            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell);

            player.PlayerMovement(gameTime, player, cancerousCell);

            Rectangle PlayerHitbox = new Rectangle(
                (int)player.x,
                (int)player.y,
                50,
                50);

            if (PlayerHitbox.Intersects(Door))
            {
                player.StopRoll();

                game.Level = 2;

                player.x = 30;
                player.y = 540;
            }
        }


        public static void Level1Drawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Texture2D LevelWalls, Player player, CancerousCell cancerousCell, 
        Texture2D cancerousCellTexture, Texture2D PlayerAttackTexture)
        {

            
            spriteBatch.Begin();

            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 1080), Color.White);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.DarkRed);

            //Door
            spriteBatch.Draw(LevelWalls, new Rectangle(1779, 270, 21, 540), Color.MediumVioletRed);

            spriteBatch.End();

            player.PlayerDrawn(game, spriteBatch, playerTexture, game.HealthBarTexture, PlayerAttackTexture, player);
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell);
        }


    }
}
