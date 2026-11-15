using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestad_Athor
{
    public class Tears
    {
        public float x;
        public float y;
        public float vx = 0;
        public float vy = 0;
        public bool on = true;

        public Tears(float startx, float starty)
        {
            x = startx;
            y = starty;
        }

        public void Update(float deltaTime)
        {
            x += vx * deltaTime;
            y += vy * deltaTime;

            if (y > 1920 || y < -100 || x < -100 || x > 2000)
            {
                on = false;
            }
        }
    }
}
