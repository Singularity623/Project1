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
    class Sky
    {
        private Game game;

        private Model model;


        public Sky(Game game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("Sky");
        }

        public void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            DrawModel(Matrix.Identity);
        }

        private void DrawModel(Matrix world)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            //transforms[wing1] = Matrix.CreateRotationY(wingAngle) * transforms[wing1];
            //transforms[wing2] = Matrix.CreateRotationY(-wingAngle) * transforms[wing2];

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.View = game.Camera.View;
                    effect.Projection = game.Camera.Projection;
                }
                mesh.Draw();
            }

        }
    }
}
