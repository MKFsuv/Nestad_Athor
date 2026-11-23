using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nestad_Athor.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class Level5
    {

        Damage_up_items chunky = new Damage_up_items();

        public static void Level5Update(Game1 game, GameTime gameTime, Player player, CancerousCell cancerousCell, Damage_up_items damage)
        {

            Rectangle Level2Wall1 = new Rectangle(0, 0, 1800, 20);
            Rectangle Level2Wall2 = new Rectangle(0, 0, 20, 1080);
            Rectangle Level2Wall3 = new Rectangle(1780, 0, 20, 1080);
            Rectangle Level2Wall4 = new Rectangle(0, 1060, 1800, 20);

            Rectangle Door = new Rectangle(0, 270, 21, 540);
            Rectangle PlayerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);


            damage.ChunkofFleshLogic(player, damage);

            player.PlayerMovement(gameTime, player, cancerousCell);

            if (PlayerHitbox.Intersects(Door))
            {
                game.Level = 4;
                player.x = 1700;
            }

        }



        public static void Level5Drawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Texture2D LevelWalls, Player player, CancerousCell cancerousCell,
        Texture2D chunkofflesh, Texture2D PlayerAttackTexture, Damage_up_items damage)
        {


            spriteBatch.Begin();

            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 1080), Color.White);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 1800, 20), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(1780, 0, 20, 1080), Color.DarkRed);
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 1060, 1800, 20), Color.DarkRed);

            //Door
            spriteBatch.Draw(LevelWalls, new Rectangle(0, 270, 21, 540), Color.MediumVioletRed);

            spriteBatch.End();


            damage.ChunkofFleshDraw(game, spriteBatch, damage, chunkofflesh);

            player.PlayerDrawn(game, spriteBatch, playerTexture, game.HealthBarTexture, PlayerAttackTexture, player);
            
        }


    }
}
