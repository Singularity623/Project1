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
    public class Arachnid : AttackingEntity
    {
        public Arachnid(Game game)
            : base(game)
        {
            int x = 10;
        }

        public override void LoadContent( ContentManager content )
        {
            model = content.Load<Model>( "Arachnid" );
            base.LoadContent( content );
        }

        public override void Update( GameTime gameTime )
        {

            base.Update(gameTime);
        }

        protected override void DrawModel( GraphicsDeviceManager graphics, Model model, Matrix world )
        {
            base.DrawModel( graphics, model, world );
        }
    }
}
