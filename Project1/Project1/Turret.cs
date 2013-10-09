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

        private Cue engineSound = null;

        /// <summary>
        /// Current position
        /// </summary>
        private Vector3 position = Vector3.Zero;

        /// <summary>
        /// How fast we are going (cm/sec)
        /// </summary>
        private float speed = 0;

        /// <summary>
        /// Thrust in cm/sec^2
        /// </summary>
        private float thrust = 0;

        /// <summary>
        ///  Decelleration due to drag
        /// </summary>
        private float Drag = .5f;

        private float bank = 0f;

        /// <summary>
        /// Maximum thrust (cm/sec^2)
        /// </summary>
        private const float MaxThrust = 2940;

        private Vector3[] laserLocs = { new Vector3(-492.673f, 164.817f, 376.025f),
            new Vector3(492.217f, -142.531f, 379.140f),
            new Vector3(-492.673f, -142.626f, 379.140f),
            new Vector3(492.217f, 164.842f, 375.952f)};

        private float bankRate = 0; //bankRate

        private float fire = 0;

        private bool deployed = false;


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

        private bool banking = false;

        #endregion

        #region properties

        public bool Deployed { get { return deployed; } set { deployed = value; } }

        public float PitchChange { get { return MaxPitchRate; } }

        public float TheDrag { get { return Drag; } set { Drag = value; } }

        /// <summary>
        /// The current ship thrust
        /// </summary>
        public float Thrust { get { return thrust; } set { thrust = value; } }

        /// <summary>
        /// Turning rate in radians per second
        /// </summary>
        public float TurnRate { get { return turnRate; } set { turnRate = value; } }

        public bool Banking { get { return banking; } set { banking = value; } }
        public Vector3 Position { get { return position; } set { position = value; } }
        public float PitchRate { get { return pitchRate; } set { pitchRate = value; } }
        public float BankRate { get { return bankRate; } set { bankRate = value; } }
        public float Fire { get { return fire; } set { fire = value; } }
        /// <summary>
        /// Access to the underlying turret model
        /// </summary>
        public Model Model { get { return model; } }

        #endregion

        #region Construction and LoadContent

        public Turret(Game game)
        {
            this.game = game;
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

           /* Matrix transform = Matrix.CreateRotationZ(bank) * 
                Matrix.CreateRotationX(elevation) *
                Matrix.CreateRotationY(azimuth);

            Vector3 directedThrust = Vector3.TransformNormal(new Vector3(0, 0, 1), transform);

            position += directedThrust * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            azimuth += turnRate * MaxTurnRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float bankTemp = bankRate * MaxBankRate - bank;
            bank += bankTemp * delta;

            float elevationTemp = elevationRate * MaxElevationChangeRate - elevation;
            elevation += elevationTemp * (float)gameTime.ElapsedGameTime.TotalSeconds;
            */
            //
            // Orientation updates
            //

            float turnAngle = turnRate * MaxTurnRate * delta;

            gunOrientation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), -turnAngle);
            gunOrientation.Normalize();

            gun.Transform = Matrix.CreateFromQuaternion(gunOrientation);

            /*
            //
            // Position updates
            //

            float acceleration = thrust * MaxThrust - Drag * speed;
            speed += acceleration * delta;

            Matrix transform = Matrix.CreateFromQuaternion(orientation);

            Vector3 directedThrust = Vector3.TransformNormal(new Vector3(0, 0, 1), transform);
            position += directedThrust * speed * delta;*/


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
