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
    public class Player
    {
      
        public float speed = 600;
        public float x = 900;
        public float y = 540;



        public static void PlayerDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerTexture, Player player)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, new Rectangle((int)player.x, (int)player.y, 100, 100), Color.Gold);

            spriteBatch.End();
        }
        public static void PlayerMovement(GameTime gameTime, Player player)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                player.y -= player.speed * deltaTime;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                player.y += player.speed * deltaTime;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                player.x -= player.speed * deltaTime;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                player.x += player.speed * deltaTime;
            }

            Rectangle playerHitbox = new Rectangle((int)player.x, (int)player.y, 100, 100);
            
            if (playerHitbox.Intersects(new Rectangle(0, 0, 1800, 19)) || // Top wall
                playerHitbox.Intersects(new Rectangle(0, 0, 19, 1080)) || // Left wall
                playerHitbox.Intersects(new Rectangle(1780, 0, 20, 1080)) || // Right wall
                playerHitbox.Intersects(new Rectangle(0, 1060, 1800, 19))) // Bottom wall
            {
             
                if (keyboard.IsKeyDown(Keys.W))
                {
                    player.y += player.speed * deltaTime; 
                }
                if (keyboard.IsKeyDown(Keys.S))
                {
                    player.y -= player.speed * deltaTime; 
                }
                if (keyboard.IsKeyDown(Keys.A))
                {
                    player.x += player.speed * deltaTime; 
                }
                if (keyboard.IsKeyDown(Keys.D))
                {
                    player.x -= player.speed * deltaTime; 
                }
            }
        }

    }
}

