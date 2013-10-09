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
    public class Entity
    {
        #region Private Members
        protected Game game;
        protected Vector3 position = new Vector3 (0,100,0);
        protected float speed;
        protected Quaternion orientation = Quaternion.CreateFromAxisAngle( new Vector3(0,0,1), 0);
        protected Model model;
        #endregion

        #region Properties
        public Game Game
        {
            get
            {
                return game;
            }
        }
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateFromQuaternion( orientation ) *
                Matrix.CreateTranslation( position );
            }
        }
        public Model Model
        {
            get
            {
                return model;
            }
        }
        #endregion Properties

        public Entity( Game game )
        {
            this.game = game;
        }

        public void Draw( GraphicsDeviceManager graphics, GameTime gameTime )
        {
            DrawModel( graphics, model, Transform );
        }

        #region Protected Methods
        public virtual void LoadContent( ContentManager content )
        {
        }

        public virtual void Update( GameTime gameTime )
        {
        }

        protected virtual void DrawModel( GraphicsDeviceManager graphics, Model model, Matrix world )
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo( transforms );

            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.View = game.Camera.View;
                    effect.Projection = game.Camera.Projection;
                }
                mesh.Draw();
            }
        }
        #endregion
    }
}
