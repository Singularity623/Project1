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
    public class Arachnid : Entity
    {
        public Arachnid(Game game)
            : base(game)
        {
        }

        public override void LoadContent( ContentManager content )
        {
            model = content.Load<Model>( "Arachnid" );
            base.LoadContent( content );
        }

        public override void Update( GameTime gameTime )
        {
            Vector3 directedThrust = Vector3.TransformNormal( new Vector3( 0, 0, 1 ), Transform );
            position += directedThrust * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        public override Entity Clone( Vector3 position )
        {
            Arachnid clone = new Arachnid( this.Game );
            clone.model = this.model;
            clone.Position = position;
            clone.Rotation = this.Rotation;
            clone.SpawnPosition = this.SpawnPosition;
            return clone;
        }

        protected override void DrawModel( GraphicsDeviceManager graphics, Model model, Matrix world )
        {
            base.DrawModel( graphics, model, world );
        }
    }
}
