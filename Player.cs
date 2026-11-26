using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        //Variables that are used for the player
        public float speed = 600;
        public float x = 900;
        public float y = 540;
        public float rotation;
        public Vector2 distance;
        public MouseState prevMouseState;
        public bool IsAttacking = false;
        public float attackTimer = 1f;
        public float attackCooldown = 5f;
        public float playerAttackCooldown = 1f;
        public float Health = 100f;
        public Color livingStatus = Color.Gold;
        public float hitCooldown = 3f;
        public float HitCooldownDuration = 1f;
        public bool hasHitThisSwing = false;
        public bool IsRolling = false;
        public float rollTimer = 1f;
        public float rollCooldown = 0f;
        public float rollDuration = 2;
        public float rollDistance = 200f;
        public Vector2 StartRollPos;
        public Vector2 EndRollPos;
        public float LevelTransitionCooldown = 0f;
        public int Damage = 10;
        public bool hasPlayed = true;

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
        public void PlayerDrawn(Game1 game, 
            SpriteBatch spriteBatch, 
            Texture2D playerAttackTexture, 
            SoundEffect chemoDead, 
            Texture2D HealthBarTexture, 
            Texture2D playerTexture, 
            Player player)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, new Rectangle((int)player.x, (int)player.y, 50, 50), player.livingStatus);

            if (player.Health <= 0 && hasPlayed == true) //play audio when dead
            {
                chemoDead.Play();
                hasPlayed = false;
            }


            if (player.IsAttacking)
            {
                spriteBatch.Draw(
                    playerAttackTexture,
                    new Vector2(
                        player.x + 25 + 100 * (float)Math.Cos(player.rotation),
                        player.y + 25 + 100 * (float)Math.Sin(player.rotation)),
                    null,
                    player.livingStatus, //This is color btw
                    player.rotation,
                    new Vector2(0.5f, 0.5f),
                    new Vector2(100f, 15f),
                    SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(HealthBarTexture, new Rectangle(0, 0, 440, 60), Color.Black);
            spriteBatch.Draw(HealthBarTexture, new Rectangle(20, 20, (int)(player.Health * 4), 20), Color.Red);

            spriteBatch.End();
        }

        //Code that makes the player move
        public void PlayerMovement(GameTime gameTime, 
            Player player, 
            CancerousCell cancerousCell)

        {
            Rectangle cancerousCellHitbox = new Rectangle((int)cancerousCell.x, (int)cancerousCell.y, 50, 50);

            Rectangle playerHitbox = new Rectangle(
                (int)player.x,
                (int)player.y,
                50,
                50);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboard = Keyboard.GetState();

            //level cooldown (i did this to make it less buggier)
            if (LevelTransitionCooldown > 0f)
            {
                LevelTransitionCooldown -= deltaTime;
            }

            if (player.Health <= 0) //change color when dead
            {
                player.livingStatus = Color.Black;

            }



            bool AttackHits(Player p, Rectangle target)
            {
                if (!p.IsAttacking) return false;

                Vector2 center = new Vector2(p.x + 25, p.y + 25);
                Vector2 dir = new Vector2((float)Math.Cos(p.rotation), (float)Math.Sin(p.rotation));

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

            

            Rectangle topWall = new Rectangle(0, 0, 1800, 19);
            Rectangle leftWall = new Rectangle(0, 0, 19, 1080);
            Rectangle rightWall = new Rectangle(1780, 0, 20, 1080);
            Rectangle bottomWall = new Rectangle(0, 1060, 1800, 20);

            float oldX = player.x;

            //Death
            if (player.Health > 0)
            {
                if (!player.IsRolling)
                {
                    //Horizontal movement
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
                }
            }

            MouseState mouse = Mouse.GetState();

            float dx = mouse.X - (player.x + 25);
            float dy = mouse.Y - (player.y + 25);

            player.rotation = (float)Math.Atan2(dy, dx);

            if (mouse.LeftButton == ButtonState.Pressed && 
               !player.IsAttacking && player.Health > 0 && 
               attackCooldown >= playerAttackCooldown)
            {
                player.IsAttacking = true;
                player.attackTimer = 0.5f;
            }

            if (player.IsAttacking)
            {
                player.attackTimer -= deltaTime;

                if (player.attackTimer <= 0)
                {
                    player.IsAttacking = false;
                    attackCooldown = 0f;
                }
            }
            else
            {
                player.attackCooldown += deltaTime;
            }
            // Start roll (self explanatory)
            if (mouse.RightButton == ButtonState.Pressed &&
                player.prevMouseState.RightButton == ButtonState.Released &&
                !player.IsRolling &&
                player.rollCooldown <= 0f)
            {
                player.IsRolling = true;

                player.StartRollPos = new Vector2(player.x, player.y);

                player.EndRollPos = new Vector2(
                    player.x + player.rollDistance * (float)Math.Cos(player.rotation),
                    player.y + player.rollDistance * (float)Math.Sin(player.rotation));

                player.EndRollPos.X =
                    MathHelper.Clamp(player.EndRollPos.X, 19, 1730);

                player.EndRollPos.Y =
                    MathHelper.Clamp(player.EndRollPos.Y, 19, 1010);

                player.rollTimer = 0f;
            }

            // Rolling (yeah)
            if (player.IsRolling && player.Health > 0)
            {
                player.rollTimer += deltaTime * 8f;

                float nextX = MathHelper.Lerp(
                    player.StartRollPos.X,
                    player.EndRollPos.X,
                    player.rollTimer);

                float nextY = MathHelper.Lerp(
                    player.StartRollPos.Y,
                    player.EndRollPos.Y,
                    player.rollTimer);

                Rectangle nextHitbox = new Rectangle(
                    (int)nextX,
                    (int)nextY,
                    50,
                    50);

                if (!nextHitbox.Intersects(leftWall) &&
                    !nextHitbox.Intersects(rightWall) &&
                    !nextHitbox.Intersects(topWall) &&
                    !nextHitbox.Intersects(bottomWall))
                {
                    player.x = nextX;
                    player.y = nextY;
                }
                else
                {
                    player.StopRoll();
                    player.rollCooldown = 2f;
                }

                // Wall Clamp (so no noclip)
                player.x = MathHelper.Clamp(player.x, 19, 1730);
                player.y = MathHelper.Clamp(player.y, 19, 1010);

                if (player.rollTimer >= 1f)
                {
                    player.x = player.EndRollPos.X;
                    player.y = player.EndRollPos.Y;

                    player.IsRolling = false;
                    player.rollCooldown = 2f;
                }
            }

            if (player.rollCooldown > 0)
            {
                player.rollCooldown -= deltaTime;
            }

            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            player.distance.X = mousePosition.X - (player.x + 25);
            player.distance.Y = mousePosition.Y - (player.y + 25);

            player.rotation = (float)Math.Atan2(
                player.distance.Y,
                player.distance.X
            );

            player.prevMouseState = mouse;
        }

        //function to stop rolling (ayden use this whenever u want the player not being able to roll)
        public void StopRoll()
        {
            IsRolling = false;
            rollTimer = 0f;

            StartRollPos = new Vector2(x, y);
            EndRollPos = new Vector2(x, y);
        }
    }
}
