using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Vector2 = OpenTK.Vector2;

namespace TowerDefense.World
{
    class Map : Interfaces.IUpdateable
    {
        public Tile[,] map;
        public Vector2 Size { get; set; }

        public Map(int w, int h)
        {
            map = new Tile[w,h];
            for (int i = 0; i < w; i++)
            {
                for(int j = 0; j < h; j++)
                {
                    map[i,j] = new Tile();
                }
            }

            Size = new Vector2(w, h);
        }

        public void Update(GameTime time)
        {
            foreach(Tile t in map)
            {
                if (t is TowerTile)
                    ((TowerTile) t).Tower.Update(time);
            }
        }
    }
}
