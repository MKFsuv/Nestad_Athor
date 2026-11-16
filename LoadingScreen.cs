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
    public class LoadingScreen
    {
        public static void LoadingScreenUpdate(Game1 game, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                game.Level = 1;
            }

        }
        public static void LoadingScreenDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D loadingScreenTexture)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingScreenTexture, new Rectangle(0, 0, 1800, 1080), Color.Orange);
            spriteBatch.DrawString(game.StartScreenText, "Press Enter to Start", new Vector2(200, 200), Color.White);


            spriteBatch.End();
        }

    }
}
