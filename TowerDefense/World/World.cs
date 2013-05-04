using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;

namespace TowerDefense.World
{
    class World : Interfaces.IDrawable, Interfaces.IUpdateable
    {
        private Map map;
        private Camera camera;

        public World()
        {
            map = new Map(400, 200);

            map.map[3, 0].Type = 1;
            map.map[4, 0].Type = 1;
            map.map[3, 1].Type = 1;
            map.map[4, 1].Type = 1;

            map.map[5,0] = new TowerTile(new Tower{Range = 10, Rate = 2000});
            map.map[5, 0].Type = 2;

            camera = new Camera(400*Tile.Width, 200*Tile.Height);
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

                    batch.Draw(Tile.TextureSheet,
                               new Rectangle(Tile.Width*(x-first_x_tile), Tile.Height*(y-first_y_tile), Tile.Width, Tile.Height),
                               Tile.GetTileFromSheet(map.map[x, y].Type),
                               Color.White,
                               0.0f,
                               new Vector2(0,0), 
                               SpriteEffects.None,
                               1.0f
                        );
                }
            }

            batch.End();
        }

        public void Update(GameTime time)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Key.A))
                camera.Move(new Vector2(-2, 0));
            if (state.IsKeyDown(Key.W))
                camera.Move(new Vector2(0, -2));
            if (state.IsKeyDown(Key.D))
                camera.Move(new Vector2(2, 0));
            if (state.IsKeyDown(Key.S))
                camera.Move(new Vector2(0, 2));


            map.Update(time);
        }
    }
}
