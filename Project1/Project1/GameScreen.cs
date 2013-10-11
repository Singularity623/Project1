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
    public class GameScreen : Screen
    {
        private Camera camera;
        private Swarm swarm;

        private Turret player;
       // private Grass field;
        private Sky sky;

        private BatRigid Bat;
        private float lastFireTime;


        public GameScreen(Game game)
            : base(game)
        {
            player = new Turret(game);
            sky = new Sky(game);
            swarm = new Swarm(game, Matrix.Identity);
           // field = new Grass(game);
            Bat = new BatRigid(game);
            camera = Game.Camera;

            camera.UseChaseCamera = true;
        }

        public override void Initialize()
        {

            base.Initialize();
        }
        public override void LoadContent(ContentManager content)
        {
            player.LoadContent(content);
            swarm.LoadContent(content);
            sky.LoadContent(Game.GetContent);
            //field.LoadContent(Game.GetContent);
            Bat.LoadContent(content);
            base.LoadContent(content);
        }
        public override void Activate()
        {
            base.Activate();
        }
        public override void Deactivate()
        {
            base.Deactivate();
        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);


            camera.Center = player.Position;

            camera.DesiredEye = Vector3.Transform(new Vector3(1200,315,0), player.Transform);
            camera.DesiredUp = player.Transform.Up;

            camera.Update(gameTime);

            if (Game.lastState.IsKeyDown(Keys.S))
            {
                Game.SetScreen(Game.Screens.Splash);
            }
            else if (Game.lastState.IsKeyDown(Keys.T))
            {
                Game.SetScreen(Game.Screens.Title);
            }

            // Control turret rotation
            if (Game.lastState.IsKeyDown(Keys.Left))
            {
                player.TurnRate = -1;
            }
            else if (Game.lastState.IsKeyDown(Keys.Right))
            {
                player.TurnRate = 1;
            }
            else
            {
                player.TurnRate = 0;
            }

            //fire
            if (Game.lastState.IsKeyDown(Keys.Space))
            {
                player.FireLaser();
                lastFireTime = 0;
            }

            Bat.Update(gameTime);
            swarm.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            player.Draw(Game.Graphics, gameTime);
            //swarm.Draw(Game.Graphics, gameTime);
            sky.Draw(Game.Graphics, gameTime);
            //field.Draw(Game.Graphics, gameTime);
            //Bat.Draw(Game.Graphics, gameTime);
            base.Draw(gameTime);
        }
        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.DrawSprites(gameTime, spriteBatch);
        }
    }
}
