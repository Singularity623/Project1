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
    public class LaserFire
    {
        private Model model;
        private Game game;

        public class LaserBlast
        {
            public Vector3 position;
            public Matrix orientation;
            public float speed;
            public float life;
        }


        private LinkedList<LaserBlast> laserBlasts = new LinkedList<LaserBlast>();
        public LinkedList<LaserBlast> LaserBlasts { get { return laserBlasts; } }

        public LaserFire(Game game)
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
            model = content.Load<Model>("LaserBlast");
        }

        public void Reset()
        {
            laserBlasts.Clear();
        }

        public void FireLaser(Vector3 position, Matrix orientation)
        {
            LaserBlast blast = new LaserBlast();
            blast.position = position;
            blast.orientation = orientation;
            blast.speed = 3000.0f;     // cm/sec
            blast.life = 2.0f;                       // 2 seconds

            laserBlasts.AddLast(blast);
        }


        /// <summary>
        /// This function is called to update this component of our game
        /// to the current game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Loop over all laser blasts in the list
            for (LinkedListNode<LaserBlast> blastNode = laserBlasts.First; blastNode != null; )
            {
                // blastNode is the current node and we set
                // nextBlast to the next node in the list
                LinkedListNode<LaserBlast> nextBlast = blastNode.Next;

                // This is the actual blast object we are working on 
                LaserBlast blast = blastNode.Value;

                // Update the position
                Vector3 direction = Vector3.TransformNormal(new Vector3(0,0,1), blast.orientation);
                blast.position += direction * blast.speed * delta;

                // Decrease the life of the blast
                blast.life -= delta;
                if (blast.life <= 0)
                {
                    // When done, remove from the list
                    laserBlasts.Remove(blastNode);
                }

                blastNode = nextBlast;
            }
        }

        /// <summary>
        /// This function is called to draw this game component.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public void Draw(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            foreach (LaserBlast blast in laserBlasts)
            {
                DrawModel(graphics, model, Matrix.CreateScale(2) * blast.orientation * Matrix.CreateTranslation(blast.position));
            }
        }


        private void DrawModel(GraphicsDeviceManager graphics, Model model, Matrix world)
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
    }
}
