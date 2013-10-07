using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class AttackingEntity : Entity
    {
        public AttackingEntity( Game game ) : base(game)
        {
            speed = 100;
        }

        public override void LoadContent( ContentManager content )
        {
            int x = 10;
        }

        public override void Update( GameTime gameTime )
        {
        }

        protected override void DrawModel( GraphicsDeviceManager graphics, Model model, Matrix world )
        {
        }
    }
}
