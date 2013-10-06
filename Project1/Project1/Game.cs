using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Screen screen = null;
        SplashScreen splashScreen = null;
        TitleScreen titleScreen = null;
        GameScreen gameScreen = null;

        public Camera Camera;

        public KeyboardState lastState;

        public enum Screens { Splash, Title, Game };



        public GraphicsDeviceManager Graphics { get { return graphics; } }


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            lastState = Keyboard.GetState();

            splashScreen = new SplashScreen(this);
            titleScreen = new TitleScreen(this);
            gameScreen = new GameScreen(this);

            screen = splashScreen;
            
        }

        public void SetScreen(Screens newScreen)
        {
            screen.Deactivate();

            switch (newScreen)
            {
                case Screens.Splash:
                    screen = splashScreen;
                    break;

                case Screens.Title:
                    screen = titleScreen;
                    break;

                case Screens.Game:
                    screen = gameScreen;
                    break;

            }

            screen.Activate();
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //gameScreen.Initialize();
            //titleScreen.Initialize();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            splashScreen.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

           KeyboardState keyboard = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            screen.Update(gameTime);

            lastState = keyboard;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            screen.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            screen.DrawSprites(gameTime, spriteBatch);
            spriteBatch.End();
                              


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
