﻿using System;
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

        private Matrix LW1Flap;
        private Matrix LW2Flap;

        private Matrix RW1Flap;
        private Matrix RW2Flap;

        private float angle1 = 0;
        private float rate1 = 0.25f;
        private float angle2 = 0;

        public BatRigid(Game game)
            : base(game)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("Bat-rigid");

            LeftWing1 = model.Bones["LeftWing1"];
            LW1Flap = LeftWing1.Transform;

            LeftWing2 = model.Bones["LeftWing2"];
            LW2Flap = LeftWing2.Transform;

            RightWing1 = model.Bones["RightWing1"];
            RW1Flap = RightWing1.Transform;

            RightWing2 = model.Bones["RightWing2"];
            RW2Flap = RightWing2.Transform;

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (angle1 >= Math.PI / 8)
            {
                angle1 =(float) Math.PI / 8;
                angle2 = (float)Math.PI / 4;
                rate1 = -rate1;
            }
            else if (angle1 <= -Math.PI / 8)
            {
                angle1 = -(float)Math.PI / 8;
                angle2 = 0f;
                rate1 = -rate1;
            }
            angle1 += (float)(2 * Math.PI * rate1 * delta);
            angle2 += (float)(2 * Math.PI * rate1 * delta);

            LeftWing1.Transform = Matrix.CreateRotationY(angle1) * LW1Flap;
            LeftWing2.Transform = Matrix.CreateRotationY(angle2) * LW2Flap;

            RightWing1.Transform = Matrix.CreateRotationY(-angle1) * RW1Flap;
            RightWing2.Transform = Matrix.CreateRotationY(-angle2) * RW2Flap;

            // Control movement
            Vector3 directedThrust = Vector3.TransformNormal( new Vector3( 0, 0, 1 ), Transform );
            position += directedThrust * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

        }

        public override Entity Clone( Vector3 position )
        {
            BatRigid clone = new BatRigid( this.Game );

            clone.model = this.model;

            clone.LeftWing1 = this.LeftWing1;
            clone.LW1Flap = this.LW1Flap;
            clone.LeftWing2 = this.LeftWing2;
            clone.LW2Flap = this.LW2Flap;
            clone.RightWing1 = this.RightWing1;
            clone.RW1Flap = this.RW1Flap;
            clone.RightWing2 = this.RightWing2;
            clone.RW2Flap = this.RW2Flap;

            clone.Position = position;
            clone.Rotation = this.Rotation;
            clone.SpawnPosition = this.SpawnPosition;
            return clone;
        }

        protected override void DrawModel(GraphicsDeviceManager graphics, Model model, Matrix world)
        {
            base.DrawModel(graphics, model, Matrix.CreateScale(15) * world);
        }

    }
}
