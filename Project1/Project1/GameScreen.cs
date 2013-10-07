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
        private Swarm swarm;
        private Game game;

        public GameScreen(Game game)
            : base(game)
        {
            this.game = game;
            swarm = new Swarm( game, Matrix.Identity );
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent( ContentManager content )
        {
            swarm.LoadContent(content);
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
            if (Game.lastState.IsKeyDown(Keys.S))
            {
                Game.SetScreen(Game.Screens.Splash);
            }
            if (Game.lastState.IsKeyDown(Keys.T))
            {
                Game.SetScreen(Game.Screens.Title);
            }
            swarm.Update( gameTime );
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Game.Graphics.GraphicsDevice.Clear(Color.Black);
            swarm.Draw( Game.Graphics, gameTime );
            base.Draw(gameTime);
        }
        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            base.DrawSprites(gameTime, spriteBatch);
        }
    }
}
