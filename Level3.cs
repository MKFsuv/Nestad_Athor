using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class Level3
    {


        public static void Level3Update(Game1 game, GameTime gameTime, Player player, CancerousCell cancerousCell4, CancerousCell cancerousCell5, CancerousCell cancerousCell6)
        {

            Rectangle Level2Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level2Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level2Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level2Wall4 = new Rectangle(0, 1060, 1800, 20);

            Rectangle Door = new Rectangle(1779, 270, 21, 540);
            Rectangle Door2 = new Rectangle(550, 1059, 900, 21);

            Rectangle PlayerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell4);
            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell5);
            CancerousCell.CancerousCellMovement(gameTime, player, cancerousCell6);

            player.PlayerMovement(gameTime, player, cancerousCell4);

            if (PlayerHitbox.Intersects(Door))
            {
                game.Level = 4;
                player.x = 80;
            }
            else if (PlayerHitbox.Intersects(Door2))
            {
                game.Level = 2;
                player.y = 100;
            }

        }


        public static void Level3Drawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Texture2D LevelWalls, Player player, CancerousCell cancerousCell4, CancerousCell cancerousCell5, CancerousCell cancerousCell6,
        Texture2D cancerousCellTexture, Texture2D PlayerAttackTexture, Texture2D Door)
        {


            spriteBatch.Begin();

            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 1080), Color.White);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.DarkRed);

            //Door
            spriteBatch.Draw(Door, new Rectangle(550, 1059, 900, 21), Color.MediumVioletRed);
            spriteBatch.Draw(Door, new Rectangle(1779, 270, 21, 540), Color.MediumVioletRed);

            spriteBatch.End();

            
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell4);
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell5);
            CancerousCell.CancerousCellDrawn(game, spriteBatch, cancerousCellTexture, cancerousCell6);
            player.PlayerDrawn(game, spriteBatch, playerTexture, game.chemoDead, game.HealthBarTexture, PlayerAttackTexture, player);
        }




    }
}
