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

        Quaternion barrelOrientation = Quaternion.Identity;

        Quaternion bulletOrientation = Quaternion.Identity;

        private float turnAngle;

        /// <summary>
        /// model for the turret
        /// </summary>
        private Model model;

        private ModelBone barrel;
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

        /// <summary>
        /// The current turning rate in radians per second
        /// Effectively the azimuth change rate
        /// </summary>
        private float turnRate = 0;

        private float angle = 0f;

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

        private LaserFire laserFire;

        private Vector3 laserLoc;

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
        public float Angle { get { return angle; } }
        /// <summary>
        /// Access to the underlying turret model
        /// </summary>
        public Model Model { get { return model; } }

        #endregion

        #region Construction and LoadContent

        public Turret(Game game)
        {
            this.game = game;
            laserFire = new LaserFire(game);
            
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

            barrel = model.Bones["Barrel"];

            laserFire.LoadContent(content);
            
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
            turnAngle = turnRate * MaxTurnRate * delta;

            gunOrientation *= Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), -turnAngle);
            gunOrientation.Normalize();
            angle -= turnAngle;
            gun.Transform = Matrix.CreateFromQuaternion(gunOrientation);

            laserFire.Update(gameTime);

            if (angle > (float)(Math.PI * 2))
            {
                angle = angle - (float)(Math.PI * 2);
            }
            System.Diagnostics.Debug.WriteLine(angle);
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

        public Matrix BarrelTransform
        {
            get
            {
                return Matrix.CreateFromQuaternion(barrelOrientation);
            }
        }

        public Matrix BulletTransform
        {
            get
            {
                return Matrix.CreateFromQuaternion(bulletOrientation);
            }
        }

        public void FireLaser()
        {
            laserLoc = new Vector3(-750f, 131f, 0f);
            Matrix orientation = GunTransform;
            
            Matrix so = Matrix.CreateFromAxisAngle(Vector3.Up, angle);

            Matrix newOr = Matrix.CreateFromAxisAngle(Vector3.Up, (angle - (float)Math.PI/2));
            Vector3 position = Vector3.Transform(laserLoc, so);

            laserFire.FireLaser(position,newOr);
            //game.SoundBank.PlayCue("tx0_fire1");
        }

        /// <summary>
        /// This function is called to draw this game component.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            DrawModel(Transform);
            laserFire.Draw(graphics, gameTime);
            //laserFire.LoadContent(content);
            laserLoc = gun.Transform.Translation;

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