using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense.World
{
    class TowerTile : Tile
    {
        public Tower Tower { get; set; }
        public TowerTile(Tower t)
        {
            Tower = t;
            Walkable = false;
        }
    }
}
