using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class Player
    {


        //Variabçles that are used for the player
        public float speed = 600;
        public float x = 900;
        public float y = 540;
        public float attacktime = 0.0f;
        public float rotation;
        public Vector2 distance;
        public MouseState prevMouseState;
        public bool IsAttacking = false;
        public float attackTimer = 0f;


        //Code that draws the player

        public static void PlayerDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerAttackTexture, Texture2D playerTexture, Player player)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, new Rectangle((int)player.x, (int)player.y, 50, 50), Color.Gold);

            if (player.IsAttacking)
            {
                spriteBatch.Draw(
                    playerAttackTexture,
                    new Vector2(
                        player.x + 50 + 100 * (float)Math.Cos(player.rotation),
                        player.y + 50 + 100 * (float)Math.Sin(player.rotation)
                    ),
                    null,
                    Color.Yellow,
                    player.rotation,
                    new Vector2(0.5f, 0.5f),
                    new Vector2(100f, 15f),
                    SpriteEffects.None,
                    0f
                );
            }

            spriteBatch.End();
        }


        //Code that makes the player  move
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

            Rectangle playerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);
            
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

            MouseState mouse = Mouse.GetState();

            // Aim at mouse
            float dx = mouse.X - (player.x + 50);
            float dy = mouse.Y - (player.y + 50);

            player.rotation = (float)Math.Atan2(dy, dx);

            // Attack
            if (mouse.LeftButton == ButtonState.Pressed && !player.IsAttacking)
            {
                player.IsAttacking = true;
                player.attackTimer = 0.3f;
            }

            // Attack duration
            if (player.IsAttacking)
            {
                player.attackTimer -= deltaTime;

                if (player.attackTimer <= 0)
                {
                    player.IsAttacking = false;
                }
            }


            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            player.distance.X = mousePosition.X - (player.x + 50);
            player.distance.Y = mousePosition.Y - (player.y + 50);

            player.rotation = (float)Math.Atan2(
                player.distance.Y,
                player.distance.X
            );

        }

    }
}

