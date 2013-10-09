using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Project1
{
    public class Swarm
    {
        private Game game;
        private LinkedList<Entity> monsters = new LinkedList<Entity>();

        public Swarm(Game game, Matrix location)
        {
            //Debug: Create an entity at the specified point to make sure
            // stuff is working
            this.game = game;
            Arachnid alienSpidersAreNoJoke = new Arachnid( game );
            monsters.AddLast( alienSpidersAreNoJoke );
        }

        public void Draw(GraphicsDeviceManager graphics, GameTime gametime)
        {
            foreach(Entity monster in monsters)
            {
                monster.Draw( graphics, gametime );
            }
        }

        public void LoadContent(ContentManager content)
        {
            foreach(Entity monster in monsters)
            {
                monster.LoadContent( content );
            }
        }

        public void Update( GameTime gameTime )
        {
            foreach(Entity monster in monsters)
            {
                monster.Update( gameTime );
            }
        }

        /// <summary>
        /// Choose how the swarm will spawn. If set to static, all monsters will spawn at the
        /// specified point. If set to radial, all monsters will spawn in a circle around the given point.
        /// </summary>
        enum SpawnType
        {
            Static,
            Radial
        }
    }
}
