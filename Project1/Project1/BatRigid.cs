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
    public class BatRigid : Entity
    {
        private ModelBone LeftWing1;
        private ModelBone LeftWing2;
        private ModelBone RightWing1;
        private ModelBone RightWing2;

        private ModelBone head;

        private Matrix LW1Flap;
        private Matrix LW2Flap;

        private Matrix RW1Flap;
        private Matrix RW2Flap;

        private float angle = 0;
        private float rate = 0.25f;

        private bool deployed = false;

        private LinkedList<Bat> bats = new LinkedList<Bat>();

        public LinkedList<Bat> Bats { get { return bats; } }

        public bool Deployed { get { return deployed; } set { deployed = value; } }

        public BatRigid(Game game)
            : base(game)
        {     
        }

        public override void LoadContent( ContentManager content )
        {
            model = content.Load<Model>("Bat-rigid");

            LeftWing1 = model.Bones["LeftWing1"];
            LW1Flap = LeftWing1.Transform;

            LeftWing2 = model.Bones["LeftWing2"];
            LW2Flap = LeftWing2.Transform;

            RightWing1 = model.Bones["RightWing1"];
            RW1Flap = LeftWing1.Transform;

            RightWing2 = model.Bones["RightWing2"];
            RW2Flap = LeftWing2.Transform;

            head = model.Bones["Head"];

            base.LoadContent( content );
        }

        public override void Update( GameTime gameTime )
        {
            Bat bat = new Bat();
            bat.position = Vector3.Zero;
            bat.velocity = 10;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

           
                if ( angle > 0)
                {
                    while ((angle - 25) > Math.PI)
                        angle -= 2 * (float)Math.PI;
                    LeftWing1.Transform = Matrix.CreateRotationY(angle) * LW1Flap;
                }

                else
                {
                    while ((angle - 25) > Math.PI)
                        angle += 2 * (float)Math.PI;
                    LeftWing1.Transform = Matrix.CreateRotationY(angle) * LW1Flap;
                }
                //  angle += (float)(2 * Math.PI * rate * delta);

                // LeftWing1.Transform = Matrix.CreateRotationY(angle) * LW1Flap;
                  LeftWing2.Transform = Matrix.CreateRotationY(25) * LW2Flap;

                //  RightWing1.Transform = Matrix.CreateRotationY(angle) * LW1Flap;
                  RightWing2.Transform = Matrix.CreateRotationY(-25) * LW1Flap;


                base.Update(gameTime);
            
        }

        protected override void DrawModel(GraphicsDeviceManager graphics, Model model, Matrix world)
        {
            base.DrawModel(graphics, model, world);
        }

        public class Bat
        {
            public int model;
            public Vector3 position;
            public int velocity;
        }
    }
}
