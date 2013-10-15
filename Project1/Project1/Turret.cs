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
    public class Turret
    {
        #region Fields

        /// <summary>
        /// the variable for the game
        /// </summary>
        private Game game;

        /// <summary>
        /// Ship orientation as a quaternion
        /// </summary>
        Quaternion orientation = Quaternion.Identity;

        Quaternion gunOrientation = Quaternion.Identity;

        /// <summary>
        /// model for the turret
        /// </summary>
        private Model model;

        private ModelBone gun;

        /// <summary>
        /// Current position
        /// </summary>
        private Vector3 position = Vector3.Zero;

        /// <summary>
        /// How fast we are going (cm/sec)
        /// </summary>
        private float speed = 0;

        //private LaserFire laserFire;

        private Vector3 laserLoc;


        /// <summary>
        /// The current turning rate in radians per second
        /// Effectively the azimuth change rate
        /// </summary>
        private float turnRate = 0;

        private float pitchRate = 0;

        /// <summary>
        /// The maximum turning rate
        /// </summary>
        private const float MaxTurnRate = (float)Math.PI / 4;

        private const float MaxBankRate = (float)Math.PI / 4;

        private const float MaxPitchRate = (float)Math.PI/2;

        private float MaxTurnAngle = (float)Math.PI / 2;
        private float MinTurnAngle = -(float)Math.PI / 2;

        private bool banking = false;

        #endregion

        #region properties

        public float PitchChange { get { return MaxPitchRate; } }

        /// <summary>
        /// Turning rate in radians per second
        /// </summary>
        public float TurnRate { get { return turnRate; } set { turnRate = value; } }

        public bool Banking { get { return banking; } set { banking = value; } }
        public Vector3 Position { get { return position; } set { position = value; } }
        public float PitchRate { get { return pitchRate; } set { pitchRate = value; } }
        /// <summary>
        /// Access to the underlying turret model
        /// </summary>
        public Model Model { get { return model; } }

        #endregion

        #region Construction and LoadContent

        public Turret(Game game)
        {
            this.game = game;
            //laserFire = new LaserFire(game);
        }
        /// <summary>
        /// This function is called to load content into this component
        /// of our game.
        /// </summary>
        /// <param name="content">The content manager to load from.</param>
        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("DeckTurrentGun");

            gun = model.Bones["GunMain"];
            gunOrientation = Quaternion.CreateFromRotationMatrix(gun.Transform);

            //laserFire.LoadContent(content);
            laserLoc = gun.Transform.Translation;
        }

        #endregion

        #region Update, Fire, and Draw

        /// <summary>
        /// This function is called to update this component of our game
        /// to the current game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //
            // Orientation updates
            //

            float turnAngle = turnRate * MaxTurnRate * delta;

            gunOrientation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), -turnAngle);
            gunOrientation.Normalize();

            gun.Transform = Matrix.CreateFromQuaternion(gunOrientation);
        }

        /// <summary>
        /// The current ship transformation
        /// </summary>
        /// <summary>
        /// The current ship transformation
        /// </summary>
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateFromQuaternion(orientation) *
                        Matrix.CreateTranslation(position);
            }
        }

        public Matrix GunTransform
        {
            get
            {
                return Matrix.CreateFromQuaternion(gunOrientation);
            }
        }

        /// <summary>
        /// This function is called to draw this game component.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            DrawModel(Matrix.Identity);
        }

        public void FireLaser()
        {/*
            Vector3 position = Vector3.Transform(laserLoc, Transform);
            Matrix orientation = Transform;
            orientation.Translation = Vector3.Zero;

            Vector3 direction = Vector3.TransformNormal(new Vector3(0, 0, 1), orientation);

            laserFire.FireLaser(speed, position, orientation);
            //game.SoundBank.PlayCue("tx0_fire1");*/
        }

        private void DrawModel(Matrix world)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

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

        #endregion
    }
}
