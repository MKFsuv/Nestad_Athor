using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nestad_Athor.Items
{
    public class Damage_up_items
    {

        Player player = new Player();

        public bool ItemPickedUp = false;


        public void ChunkofFleshLogic(Player player, Damage_up_items damage)
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
                damage.ItemPickedUp = true;

            }




        }

        public void ChunkofFleshDraw(Game1 game, SpriteBatch spriteBatch, Damage_up_items damage, Texture2D chunkofflesh)
        {
            if (!damage.ItemPickedUp)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(chunkofflesh, new Rectangle(1000, 500, 30, 30), Color.Blue);

                spriteBatch.End();

            }
        }



    }
}
