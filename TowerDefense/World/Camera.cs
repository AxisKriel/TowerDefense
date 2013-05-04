using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefense.World
{
    class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 WorldSize { get; set; }

        public Camera()
        {
            Position = new Vector2(0,0);
            Size = new Vector2(800, 600);
            WorldSize = new Vector2(800, 600);
        }

        public Camera(int width, int height)
        {
            Position = new Vector2(0, 0);
            Size = new Vector2(800, 600);
            WorldSize = new Vector2(width, height);
        }

        public void Move(Vector2 move)
        {
            Position += move;
            Position = new Vector2(MathHelper.Clamp(Position.X, 0, WorldSize.X), MathHelper.Clamp(Position.Y, 0, WorldSize.Y));
        }
    }
}
