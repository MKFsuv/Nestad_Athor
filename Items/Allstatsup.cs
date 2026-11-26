using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor.Items
{
    public class Allstatsup
    {

        public bool ItemPickedUp = false;


        public static void AllStatsUpLogic(Player player, Allstatsup statup)
        {
            Rectangle playerHitbox = new Rectangle(
                (int)player.x,
                (int)player.y,
                50,
                50);

            Rectangle ItemHitbox = new Rectangle(1000, 500, 30, 30);

            if (playerHitbox.Intersects(ItemHitbox))
            {

                player.Damage += 10;
                player.speed = 700;
                player.Health = 100;
                player.attackCooldown = 0f;
                statup.ItemPickedUp = true;

            }




        }

        public static void AllStatsUpDraw(Game1 game, SpriteBatch spriteBatch, Texture2D StatupItem, Allstatsup statup)
        {
            if (!statup.ItemPickedUp)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(StatupItem, new Rectangle(1000, 500, 30, 30), Color.Blue);

                spriteBatch.End();

            }
        }



    }




}

