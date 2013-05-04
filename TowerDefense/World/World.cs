using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using TowerDefense.Pathfinding;

namespace TowerDefense.World
{
    class World : Interfaces.IDrawable, Interfaces.IUpdateable
    {
        private Map map;
        private Camera camera;

        private Point spawn, goal;
        private List<Node> path;

        private List<Tower> towers;
        private List<Mob> mobs;
 
        public World()
        {
            map = new Map(40, 5);
            towers = new List<Tower>();
            mobs = new List<Mob>();

            map.map[3, 0].Type = 1;
            map.map[4, 0].Type = 1;
            map.map[3, 1].Type = 1;
            map.map[4, 1].Type = 1;

            map.map[5,0] = new TowerTile(new Tower{Range = 10, Rate = 2000});
            map.map[5, 0].Type = 2;


            map.map[1, 0].Walkable = false;
            map.map[1, 1].Walkable = false;
            map.map[1, 3].Walkable = false;
            map.map[1, 4].Walkable = false;


            camera = new Camera(400*Tile.Width, 200*Tile.Height);
            
            Random rng = new Random(DateTime.Now.Millisecond);

            spawn = new Point(0, rng.Next(0, (int)map.Size.Y));
            goal = new Point((int)map.Size.X - 1, rng.Next(0, (int)map.Size.Y));

            map.map[spawn.X, spawn.Y] = new SpawnTile();
            map.map[goal.X, goal.Y] = new GoalTile();

            Node n = AStar.FindPath(map, spawn, goal);
            path = new List<Node>();

            while(n.Parent != null)
            {
                path.Insert(0, n);
                n = n.Parent;
            }

            towers.Add(((TowerTile)map.map[5,0]).Tower);

            Mob m = new Mob(path, spawn);
            mobs.Add(m);
        }

        public void Draw(SpriteBatch batch)
        {
            int first_x_tile = (int)(Math.Floor(camera.Position.X/Tile.Width));
            int first_y_tile = (int)(Math.Floor(camera.Position.Y / Tile.Height));

            int tiles_to_draw_x = (int)(Math.Ceiling(camera.Size.X/Tile.Width));
            int tiles_to_draw_y = (int)(Math.Ceiling(camera.Size.Y / Tile.Height));

            int last_x_tile = (int)Math.Min(first_x_tile + tiles_to_draw_x, map.Size.X);
            int last_y_tile = (int)Math.Min(first_y_tile + tiles_to_draw_y, map.Size.Y);

            batch.Begin();

            for(int x = first_x_tile; x < last_x_tile; x++)
            {
                for (int y = first_y_tile; y < last_y_tile; y++)
                {
                    Color overlay = Color.White;
                    if (map.map[x, y] is SpawnTile)
                        overlay = Color.Yellow;
                    else if (map.map[x, y] is GoalTile)
                        overlay = Color.Black;

                    batch.Draw(Tile.TextureSheet,
                               new Rectangle(Tile.Width*(x-first_x_tile), Tile.Height*(y-first_y_tile), Tile.Width, Tile.Height),
                               Tile.GetTileFromSheet(map.map[x, y].Type),
                               overlay,
                               0.0f,
                               new Vector2(0,0), 
                               SpriteEffects.None,
                               1.0f
                        );
                }
            }

            batch.End();

            batch.Begin();
            foreach(Mob m in mobs)
            {
                m.Draw(batch, camera);
            }

            batch.End();
        }

        private KeyboardState old_keyboard = Keyboard.GetState();

        public void Update(GameTime time)
        {
            KeyboardState keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Key.A))
                camera.Move(new Vector2(-2, 0));
            if (keyboard_state.IsKeyDown(Key.W))
                camera.Move(new Vector2(0, -2));
            if (keyboard_state.IsKeyDown(Key.D))
                camera.Move(new Vector2(2, 0));
            if (keyboard_state.IsKeyDown(Key.S))
                camera.Move(new Vector2(0, 2));

            //Pathfinding debug test
            if (keyboard_state.IsKeyUp(Key.Enter) && old_keyboard.IsKeyDown(Key.Enter))
            {
                DateTime start = DateTime.Now;
                Node n = AStar.FindPath(map, new Point(0, 0), new Point(2, 0));
                DateTime end = DateTime.Now;

                Console.WriteLine("Time to path: {0}", (end-start).TotalMilliseconds);
                if (n == null)
                {
                    Console.WriteLine("Goal is unreachable.");
                }
                else
                {
                    while (n.Parent != null)
                    {
                        Console.Write("{0}->", n.location.ToString());
                        n = n.Parent;
                    }

                    Console.WriteLine("{0}", n.location.ToString());
                }
            }

            if (keyboard_state.IsKeyUp(Key.ShiftRight) && old_keyboard.IsKeyDown(Key.ShiftRight))
            {
                Mob m = new Mob(path, spawn){Speed = 1};
                mobs.Add(m);
            }

            old_keyboard = keyboard_state;

            map.Update(time);
            for (int i = mobs.Count - 1; i >= 0;i--)
            {
                Mob m = mobs[i];
                if (m.Dead)
                    mobs.Remove(m);
                else
                    m.Update(time);
            }
        }
    }
}
