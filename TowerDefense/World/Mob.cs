using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.Pathfinding;

namespace TowerDefense.World
{
    class Mob : Interfaces.IUpdateable
    {
        public Vector2 Position { get; set; }
        public Vector2 Tile { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }
        public bool Dead { get; set; }
        private List<Node> path; 

        public static Texture2D SpriteSheet;

        private float rotation = 0f;

        public Mob(List<Node> p, Point spawn)
        {
            Health = 1;
            Speed = 1;
            Defense = 0;
            Type = 0;
            path = new List<Node>(p);
            Dead = false;

            Position = new Vector2(spawn.X * TowerDefense.World.Tile.Width, spawn.Y * TowerDefense.World.Tile.Height);
            Tile = new Vector2(spawn.X, spawn.Y);
        }

        

        public void Update(GameTime time)
        {
            if(Dead)
                return;

            Vector2 offset = (Tile*16) - Position;

            if(offset.X == 0 && offset.Y == 0)
            {
                path.RemoveAt(0);

                if(path.Count == 0)
                {
                    Dead = true;
                    return;
                }

                Tile = new Vector2(path[0].location.X, path[0].location.Y);
            }

            Vector2 movement = (Tile*16) - Position;

            movement.Normalize();

            Vector2 distance = (Tile * 16) - Position;

            Position += movement * Speed;

            if (distance.Length() < (movement * Speed).Length())
                Position = (Tile*16);

            rotation = (float)((Math.Atan2(movement.Y, movement.X) * 180) / Math.PI);
        }

        public void SetPath(List<Node> p)
        {
            path = new List<Node>(p);
        }

        public void Draw(SpriteBatch batch, Camera cam)
        {
            if(cam.OnScreen(Position))
            {
                batch.Draw(SpriteSheet,
                           cam.ScreenCoords(Position),
                           Mob.GetTileFromSheet(Type),
                           Color.White,
                           rotation,
                           new Vector2(0,0),
                           1.0f,
                           SpriteEffects.None,
                           1.0f
                    );
            }
        }



        public static void LoadSpriteSheet(ContentManager manager)
        {
            SpriteSheet = manager.Load<Texture2D>(@"Sprites\MobSheet.png");
        }

        public static Rectangle GetTileFromSheet(int id)
        {
            int x = (TowerDefense.World.Tile.Width * id) % SpriteSheet.Width;
            int y = (int)Math.Floor((TowerDefense.World.Tile.Width * id) / (SpriteSheet.Width * 1.0));

            return new Rectangle(x, y, TowerDefense.World.Tile.Width, TowerDefense.World.Tile.Height);
        }
    }
}
