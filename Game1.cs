using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KineticCamp {

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        private readonly GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Texture2D playerTexture;
        private Player player;
        private Npc npc;
        private Npc npc2;
        private GameObject obj;
        private GameObject obj2;
        private Level level;
        private PlayerManager playerManager;
        private InputManager inputManager;
        private Texture2D cursor;
        private MouseState mouse;

        private Song song;
        private SoundEffect effect;

        private int midX;
        private int midY;
        private int width;
        private int height;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /*
         * Returns the center x coordinate of the game
         */
        public int getMidX() {
            return midX;
        }

        /*
         * Returns the center y coordinate of the game
         */
        public int getMidY() {
            return midY;
        }

        /*
         * Returns the game's width
         */
        public int getWidth() {
            return width;
        }

        /*
         * Returns the game's height
         */
        public int getHeight() {
            return height;
        }

        /*
         * Returns the mouse state
         */
        public MouseState getMouse() {
            return mouse;
        }

        /*
         * Allows npcs to add projectiles (they can't get a non-nulled level instance due to structure)
         */
        public void addProjectile(Projectile projectile) {
            level.addProjectile(projectile);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //
        protected override void Initialize() {
            base.Initialize();
            //graphics.PreferredBackBufferWidth = x;
            //graphics.PreferredBackBufferHeight = y;
            //graphics.ApplyChanges();
            width = 800;
            height = 480;
            Window.Title = "Outside The Box";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            midX = (graphics.PreferredBackBufferWidth - playerTexture.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playerTexture.Height) / 2;
            player = new Player(playerTexture, Vector2.Zero, Direction.SOUTH, 100, 50, 0, 5);
            player.setProjectile(new Projectile(player, Content.Load<Texture2D>("bullet"), 5, 250));
            npc = new Npc(this, Content.Load<Texture2D>("npc"), new Vector2(midX + 148, midY + 148), Direction.EAST, new NpcDefinition("Goku", new string[] { "a", "b", "c" }, new int[] { 0, 1, 2 }), 100, 5, 100, 0x5);
            npc.setPath(new AIPath(npc, new int[] { midX - 100, midY - 100, midX + 100, midY + 100 }, new int[] { 0, 0, 0, 0 }, new Direction[] { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH }));
            npc2 = new Npc(this, Content.Load<Texture2D>("npc"), new Vector2(midX, midY), Direction.EAST, new NpcDefinition("Goku2", new string[] { }, new int[] { }), 100, 5, 250, 0x5);
            npc2.setProjectile(new Projectile(npc2, Content.Load<Texture2D>("bullet"), 5, 250));
            obj = new GameObject(Content.Load<Texture2D>("sprite"), new Vector2(midX + 100, midY + 100), true);
            playerManager = new PlayerManager(player, new DisplayBar(Content.Load<Texture2D>("HealthBarTexture"), new Vector2(20, 20), Color.Red, Content.Load<Texture2D>("BackBarTexture")), new DisplayBar(Content.Load<Texture2D>("ManaBarTexture"), new Vector2(20, 50), Color.Blue, Content.Load<Texture2D>("BackBarTexture")));
            obj2 = new GameObject(Content.Load<Texture2D>("GreenMushroom"), new Vector2(midX + 50, midY + 120), true);
            level = new Level(this, player, Content.Load<Texture2D>("map"), new Npc[] { npc, npc2 }, new GameObject[] { obj, obj2 }, new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() });
            //song = Content.Load<Song>("Rhinoceros");
            inputManager = new InputManager(this, player, level, playerManager, new Screen[] { new Screen("Menu"), new Screen("Normal", true), new Screen("Telekinesis-Select"), new Screen("Telekinesis-Move") });
            level.setInputManager(inputManager);
            cursor = Content.Load<Texture2D>("cursor");
            //effect = Content.Load<SoundEffect>("gun");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            base.UnloadContent();
            spriteBatch.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            playerManager.updateHealthCooldown();
            inputManager.update(gameTime);
            level.updateProjectiles();
            level.updateNpcs(gameTime); 
            mouse = Mouse.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            level.draw(spriteBatch);
            if (mouse != null) {
                spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
            }
            spriteBatch.End();
        }
    }
}
