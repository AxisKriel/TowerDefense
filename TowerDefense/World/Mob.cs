using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.World
{
    class Mob
    {
        public Vector2 Position { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }

        public static Texture2D SpriteSheet;

        public Mob()
        {
            Health = 1;
            Speed = 1;
            Defense = 0;
            Type = 0;
        }

        public static void LoadSpriteSheet(ContentManager manager)
        {
            SpriteSheet = manager.Load<Texture2D>(@"Sprites\SpriteSheet.png");
        }
    }
}
