using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.World
{
    class Tile
    {
        public bool Walkable { get; set; }
        public int Type { get; set; }
        
        public static int Width = 16, Height = 16;
        public static Texture2D TextureSheet;

        public Tile()
        {
            Walkable = true;
            Type = 0;
        }

        public static void LoadSheet(ContentManager content)
        {
            TextureSheet = content.Load<Texture2D>(@"Sprites\TileSheet.png");
        }

        public static Rectangle GetTileFromSheet(int id)
        {
            int x = (Width * id)%TextureSheet.Width;
            int y = (int)Math.Floor((Width*id)/(TextureSheet.Width*1.0));

            return new Rectangle(x, y, Width, Height);
        }
    }
}
