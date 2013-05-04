using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefense.Pathfinding
{
    class NodeList
    {
        private List<Node> list;
        public int Count { get { return list.Count; } }

        public NodeList()
        {
            list = new List<Node>();
        }

        public Node Get(Point p)
        {
            foreach(Node n in list)
            {
                if (n.location == p)
                    return n;
            }

            return null;
        }

        public void Add(Node n)
        {
            list.Add(n);
        }

        public void Remove(Node n)
        {
            list.Remove(n);
        }

        public Node Lowest()
        {
            Node l = list[0];
            for(int i = 1; i < list.Count; i++)
            {
                if (list[i].Cost < l.Cost)
                    l = list[i];
            }

            return l;
        }
    }
}
