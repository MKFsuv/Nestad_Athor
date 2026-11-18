using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class DeathScreen
    {
        public static void DeathScreenScreenUpdate(Game1 game, GameTime gameTime, Player player)
        {


        }
        public static void DeathScreenDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D loadingScreenTexture, Player player)
        {
            spriteBatch.Begin();
            if (player.Health <= 0)
            {
                spriteBatch.DrawString(game.StartScreenText, "You died.", new Vector2(200, 200), Color.Black);
            }

            spriteBatch.End();
        }

    }
}
