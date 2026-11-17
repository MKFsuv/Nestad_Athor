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
        public float attackTimer = 1f;
        public float Health = 100f;
        public float hitCooldown = 0f;
        public float HitCooldownDuration = 1f;
        public bool hasHitThisSwing = false;
        public Rectangle AttackHitbox
        {
            get
            {
                int attackX = (int)(x + 25 + 100 * Math.Cos(rotation));
                int attackY = (int)(y + 25 + 100 * Math.Sin(rotation));

                return new Rectangle(
                    attackX - 50,
                    attackY - 7,
                    100,
                    15
                );
            }
        }


        //Code that draws the player

        public void PlayerDrawn(Game1 game, SpriteBatch spriteBatch, Texture2D playerAttackTexture, Texture2D HealthBarTexture, Texture2D playerTexture, Player player)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, new Rectangle((int)player.x, (int)player.y, 50, 50), Color.Gold);

            if (player.IsAttacking)
            {
                spriteBatch.Draw(
                playerAttackTexture,
                new Vector2(
                player.x + 25 + 100 * (float)Math.Cos(player.rotation),
                player.y + 25 + 100 * (float)Math.Sin(player.rotation)),
                null,
                Color.Yellow,
                player.rotation,
                new Vector2(0.5f, 0.5f),
                new Vector2(100f, 15f),
                SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(HealthBarTexture, new Rectangle(0, 0, 440, 60), Color.Black);
            spriteBatch.Draw(HealthBarTexture, new Rectangle(20, 20, (int)(player.Health * 4), 20), Color.Red);

            spriteBatch.End();
        }


        //Code that makes the player  move
        public void PlayerMovement(GameTime gameTime, Player player, CancerousCell cancerousCell)
        {
            Rectangle cancerousCellHitbox = new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 50, 50);

            Rectangle playerHitbox = new Rectangle(
    (int)player.x,
    (int)player.y,
    50,
    50);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboard = Keyboard.GetState();

            bool AttackHits(Player player, Rectangle target)
            {
                if (!player.IsAttacking) return false;

              
                Vector2 center = new Vector2(player.x + 25, player.y + 25);
                Vector2 dir = new Vector2((float)Math.Cos(player.rotation), (float)Math.Sin(player.rotation));

                Rectangle t = target;
                t.Inflate(8, 8);

              
                for (int i = 0; i <= 10; i++)
                {
                    Vector2 point = center + dir * (50 + i * 10);
                    if (t.Contains(point))
                        return true;
                }
                return false;
            }

            if (!player.IsAttacking)
                hasHitThisSwing = false;

            if (!hasHitThisSwing && AttackHits(player, cancerousCellHitbox))
            {
                cancerousCell.health -= 10;
                hasHitThisSwing = true;
            }

            if (player.hitCooldown > 0f)
                player.hitCooldown -= deltaTime;

            if (cancerousCellHitbox.Intersects(playerHitbox) && player.hitCooldown <= 0f)
            {
                player.Health -= 10;
                player.hitCooldown = player.HitCooldownDuration;
            }


            Rectangle topWall = new Rectangle(0, 0, 1800, 19);
            Rectangle leftWall = new Rectangle(0, 0, 19, 1080);
            Rectangle rightWall = new Rectangle(1780, 0, 20, 1080);
            Rectangle bottomWall = new Rectangle(0, 1060, 1800, 20);


            float oldX = player.x;


            if (keyboard.IsKeyDown(Keys.A))
            {
                player.x -= player.speed * deltaTime;

                if (player.x < 19)
                    player.x = 19;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                player.x += player.speed * deltaTime;

                if (player.x + 50 > 1780)
                    player.x = 1730;
            }

            if (playerHitbox.Intersects(leftWall) ||
                playerHitbox.Intersects(rightWall))
            {
                player.x = oldX;
            }

            // Vertical movement
            float oldY = player.y;

            if (keyboard.IsKeyDown(Keys.W))
                player.y -= player.speed * deltaTime;

            if (keyboard.IsKeyDown(Keys.S))
                player.y += player.speed * deltaTime;

            playerHitbox = new Rectangle((int)player.x, (int)player.y, 50, 50);

            if (playerHitbox.Intersects(topWall) ||
                playerHitbox.Intersects(bottomWall))
            {
                player.y = oldY;
            }

            MouseState mouse = Mouse.GetState();

        
            float dx = mouse.X - (player.x + 50);
            float dy = mouse.Y - (player.y + 50);

            player.rotation = (float)Math.Atan2(dy, dx);

         
            if (mouse.LeftButton == ButtonState.Pressed && !player.IsAttacking)
            {
                player.IsAttacking = true;
                player.attackTimer = 0.3f;
            }

            
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

