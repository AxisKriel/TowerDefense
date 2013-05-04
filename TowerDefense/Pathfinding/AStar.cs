using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDefense.World;

namespace TowerDefense.Pathfinding
{
    class AStar
    {
        private static Node FindPath(Map m, Node origin, Point goal, NodeList open, NodeList closed)
        {
            if (origin.location == goal)
                return origin; //our current node is the goal

            open.Remove(origin);
            closed.Add(origin);

            Node left, up, right, down;

            //Left node case
            if (origin.location.X > 0)
            {
                Point loc = new Point(origin.location.X - 1, origin.location.Y);
                if (m.map[loc.X, loc.Y].Walkable && closed.Get(loc) == null)
                {
                    Node ex = open.Get(loc);

                    if(ex != null)
                    {
                        if (ex.G_Cost > origin.G_Cost + 10)
                        {
                            ex.Parent = origin;
                            ex.G_Cost = origin.G_Cost + 10;
                        }
                    }
                    else
                    {
                        left = new Node(loc);
                        left.Parent = origin;
                        left.G_Cost = origin.G_Cost + 10;
                        left.H_Cost = (Math.Abs(loc.X - goal.X) * 10) + (Math.Abs(loc.Y - goal.Y) * 10);
                        open.Add(left);
                    }
                    
                }
            }

            //Right node case
            if (origin.location.X < m.Size.X - 1)
            {
                Point loc = new Point(origin.location.X + 1, origin.location.Y);
                if (m.map[loc.X, loc.Y].Walkable && closed.Get(loc) == null)
                {
                    Node ex = open.Get(loc);

                    if (ex != null)
                    {
                        if (ex.G_Cost > origin.G_Cost + 10)
                        {
                            ex.Parent = origin;
                            ex.G_Cost = origin.G_Cost + 10;
                        }
                    }
                    else
                    {
                        right = new Node(loc);
                        right.Parent = origin;
                        right.G_Cost = origin.G_Cost + 10;
                        right.H_Cost = (Math.Abs(loc.X - goal.X) * 10) + (Math.Abs(loc.Y - goal.Y) * 10);
                        open.Add(right);
                    }

                }
            }

            //Up node case
            if (origin.location.Y > 0)
            {
                Point loc = new Point(origin.location.X, origin.location.Y - 1);
                if (m.map[loc.X, loc.Y].Walkable && closed.Get(loc) == null)
                {
                    Node ex = open.Get(loc);

                    if (ex != null)
                    {
                        if (ex.G_Cost > origin.G_Cost + 10)
                        {
                            ex.Parent = origin;
                            ex.G_Cost = origin.G_Cost + 10;
                        }
                    }
                    else
                    {
                        up = new Node(loc);
                        up.Parent = origin;
                        up.G_Cost = origin.G_Cost + 10;
                        up.H_Cost = (Math.Abs(loc.X - goal.X) * 10) + (Math.Abs(loc.Y - goal.Y) * 10);
                        open.Add(up);
                    }

                }
            }

            //Down node case
            if (origin.location.Y < m.Size.Y - 1)
            {
                Point loc = new Point(origin.location.X, origin.location.Y + 1);
                if (m.map[loc.X, loc.Y].Walkable && closed.Get(loc) == null)
                {
                    Node ex = open.Get(loc);

                    if (ex != null)
                    {
                        if (ex.G_Cost > origin.G_Cost + 10)
                        {
                            ex.Parent = origin;
                            ex.G_Cost = origin.G_Cost + 10;
                        }
                    }
                    else
                    {
                        down = new Node(loc);
                        down.Parent = origin;
                        down.G_Cost = origin.G_Cost + 10;
                        down.H_Cost = (Math.Abs(loc.X - goal.X) * 10) + (Math.Abs(loc.Y - goal.Y) * 10);
                        open.Add(down);
                    }
                }
            }

            if (open.Count == 0)
                return null;

            Node lowest = open.Lowest();
            return FindPath(m, lowest, goal, open, closed);
        }

        public static Node FindPath(Map m, Point start, Point end)
        {
            if (!m.map[end.X, end.Y].Walkable)
                return null;

            Node parent = new Node(start);
            parent.Parent = null;
            parent.G_Cost = 0;
            parent.H_Cost = (Math.Abs(start.X - end.X) * 10) + (Math.Abs(start.Y - end.Y) * 10);

            NodeList open = new NodeList();
            open.Add(parent);

            return FindPath(m, parent, end, open, new NodeList());
        }
    }
}
