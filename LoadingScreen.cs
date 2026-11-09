using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class LoadingScreen
    {

        public static void LoadingScreenDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D loadingScreenTexture)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingScreenTexture, new Rectangle(0, 0, 1800, 1080), Color.White);

            spriteBatch.End();
        }

    }
}
