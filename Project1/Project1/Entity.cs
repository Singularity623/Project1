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
    class Entity
    {
        private Game game;


        public Game Game { get { return game; } }

        public Entity(Game game)
        {
            this.game = game;
        }


        protected virtual void LoadContent(ContentManager content)
        {
        }

        protected virtual void Update(GameTime gameTime)
        {
        }

        protected virtual void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        { 
        }


        protected virtual void DrawModel(GraphicsDeviceManager graphics, Model model, Matrix world)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
        }

    }
}
