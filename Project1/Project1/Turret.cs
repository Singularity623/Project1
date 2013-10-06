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
    class Turret : Entity
    {
        public Turret(Game game)
        : base(game)
        {

        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
        }

        protected override void DrawModel(GraphicsDeviceManager graphics, Model model, Matrix world)
        {
            base.DrawModel(graphics, model, world);


        }

    }
}
