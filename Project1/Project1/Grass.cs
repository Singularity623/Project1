using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project1
{
    class Grass
    {
        private Game game;

        private Model model;


        public Grass(Game game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("GrassField");
        }

        public void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            DrawModel(Matrix.Identity * Matrix.CreateRotationX(-(float)Math.PI/2) * Matrix.CreateScale(100f));
        }

        private void DrawModel(Matrix world)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = game.Camera.View;
                    effect.Projection = game.Camera.Projection;
                }
                mesh.Draw();
            }

        }
    }
}

