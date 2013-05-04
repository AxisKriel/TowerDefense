using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefense.Pathfinding
{
    class Node
    {
        public Point location;

        public Node Parent { get; set; }
        public int G_Cost { get; set; }
        public int H_Cost { get; set; }

        public int Cost
        {
            get { return G_Cost + H_Cost; }
        }

        public Node(Point l)
        {
            location = l;
        }
    }
}
