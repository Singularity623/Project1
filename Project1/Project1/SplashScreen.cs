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
    public class SplashScreen : Screen
    {
        private Texture2D _splash;
        private double _time;

        public SplashScreen(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
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
            if (Game.lastState.IsKeyDown(Keys.G))
            {
                Game.SetScreen(Game.Screens.Game);
            }
            if (Game.lastState.IsKeyDown(Keys.T))
            {
                Game.SetScreen(Game.Screens.Title);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Game.Graphics.GraphicsDevice.Clear(Color.CadetBlue);
            base.Draw(gameTime);
        }
        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.DrawSprites(gameTime, spriteBatch);
        }
    }
}
