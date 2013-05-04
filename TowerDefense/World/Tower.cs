using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.World
{
    class Tower : Interfaces.IUpdateable
    {
        public int Range { get; set; }
        public int Rate { get; set; }

        private DateTime lastFired = DateTime.Now;

        public void Update(GameTime time)
        {
            if (Rate < 1)
                return;

            DateTime now = DateTime.Now;
            if((now-lastFired).TotalMilliseconds > Rate)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            lastFired = DateTime.Now;
            Console.WriteLine("Fired a projectile");
        }
    }
}
