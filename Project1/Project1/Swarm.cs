using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

/***************************************
 *  Spawn Positions (x = turret)
 * 
 * 
 *              2
 *          1       3
 * 
 * 
 * 
 * 
 *              x
 * ***********************************/
namespace Project1
{
    public class Swarm
    {
        private Game game;
        private LinkedList<Entity> monsters = new LinkedList<Entity>();
        private int spawnedMonsters = 3;
        private bool isDefeated = false;

        /// <summary>
        /// How many monsters will the swarm spawn before it is defeated.
        /// </summary>
        private int monsterLimit = 20;

        /// <summary>
        /// How far away from the target we draw the monsters.
        /// </summary>
        private float distance = 2500;

        /// <summary>
        /// The angle at which we spawn the flanking monsters.
        /// </summary>
        private float spawnAngle = 0.44f;

        public float Distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
            }
        }
        public int MonsterLimit
        {
            get
            {
                return monsterLimit;
            }
            set
            {
                monsterLimit = value;
            }
        }
        public bool IsDefeated
        {
            get
            {
                return isDefeated;
            }
        }

        public Swarm(Game game, Matrix location)
        {
            this.game = game;

            // Create initial three monsters

            // First up, arachnid front and center
            Arachnid spider = new Arachnid( game );
            spider.Position = new Vector3( -distance, 0, 0 );
            spider.SpawnPosition = 2;
            monsters.AddLast( spider );

            // Flanked by two bats
            BatRigid batLeft = new BatRigid( game );
            batLeft.Position =  new Vector3( -distance, 300, 0 ) ;
            batLeft.Rotation = Matrix.CreateRotationY( spawnAngle );
            batLeft.SpawnPosition = 1;
            monsters.AddLast( batLeft );

            BatRigid batRight = new BatRigid( game );
            batRight.Position = new Vector3( -distance, 300, 0 );
            batRight.Rotation = Matrix.CreateRotationY( -spawnAngle ); 
            batRight.SpawnPosition = 3;
            monsters.AddLast( batRight );
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
            for( LinkedListNode<Entity> monsterNode = monsters.First; monsterNode != null; )
            {
                LinkedListNode<Entity> nextNode = monsterNode.Next;
                monsterNode.Value.Update( gameTime );

                if(monsterNode.Value.Position.X > 100)
                {
                    // Remove monster, and create new one, if applicable
                    monsters.Remove( monsterNode );
                    if(spawnedMonsters < monsterLimit)
                    {
                        BatRigid testBat = new BatRigid( this.game );
                        Entity clone;
                        if(monsterNode.Value.GetType() == testBat.GetType())
                        {
                            clone = monsterNode.Value.Clone( new Vector3( -distance, 300, 0 ) );       
                        }
                        else
                        {
                            clone = monsterNode.Value.Clone( new Vector3( -distance, 0, 0 ) );
                        }
                        clone.Speed = monsterNode.Value.Speed + 100;
                        monsters.AddLast( clone );
                        spawnedMonsters++;
                    }
                }

                monsterNode = nextNode;
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
