using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace KineticCamp {

    /* CHANGELOG
     * Version 0.0.0.6
     *
     * 0.0.0.1:
     *      Added a few sprites to test functionality, centered player on screen, locked player to the map's bounds
     * 0.0.0.2:
     *      Created data structures for relevant information, handling entities on/off screen
     * 0.0.0.3:
     *      Direction handling, basic projectile support, changed rendering order
     * 0.0.0.4:
     *      Projectile rotation, npc hotfix
     * 0.0.0.5:
     *      Fixed #isOnScreen logic, added GameObject, ScreenManager, InputManager, CollisionManager
     * 0.0.0.6:
     *      Drawing of cursor, object bounds fixed, clicking on objects
     * 0.0.0.7:
     *      Mostly fixed collisions/projectiles to new map/player system
     * 0.0.0.8:
     *      Fixed #setActiveScreen
     */

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        
        // TODO: test ScreenManager, have InputManager determine actions based on ScreenManager's active screen,
        // TOOD: check on coordinate system/fix CollisionManager

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D playerTexture;
        private Entity player;
        private PlayerManager playerManager;
        private Entity npc;
        private GameObject obj;
        private GameObject obj2; 
        private Level level;
        private List<Screen> screens;

        private InputManager inputManager;

        private Texture2D cursor;
        private MouseState mouse;

        private int midX;
        private int midY;
        private int mode; 

        private const int stepSize = 4;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            mode = 0; 
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
         * Returns the stepping size for the player and entities
         */
        public int getStepSize() {
            return stepSize;
        }

        /*
         * Returns the mouse state
         */
        public MouseState getMouse() {
            return mouse;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //
        // TODO: set viewport bounds to be a factor of sprite dimensions
        protected override void Initialize() {
            base.Initialize();
            //graphics.PreferredBackBufferWidth = x;
            //graphics.PreferredBackBufferHeight = y;
            //graphics.ApplyChanges();
            Window.Title = "Outside The Box";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            midX = (graphics.PreferredBackBufferWidth - playerTexture.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playerTexture.Height) / 2;
            player = new Entity(playerTexture, new Projectile(Content.Load<Texture2D>("bullet"), new Vector2(midX, midY), 7, 250, 0.5f), new Vector2(midX, midY), Direction.EAST, GraphicsDevice.Viewport.Bounds, 50, 5);
            npc = new Entity(Content.Load<Texture2D>("npc"), null, new Vector2(midX + 148, midY + 148), Direction.EAST, GraphicsDevice.Viewport.Bounds, 50, 5);
            npc.setPath(new AIPath(npc, new int[] { midX - 100, midY - 100, midX + 100, midY + 100 }, new int[] { 0, 0, 0, 0 }, new Direction[] { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH }));
            obj = new GameObject(Content.Load<Texture2D>("sprite"), null, new Vector2(midX + 100, midY + 100), GraphicsDevice.Viewport.Bounds, true);
            playerManager = new PlayerManager(player, new DisplayBar(Content.Load<Texture2D>("HealthBarTexture"), new Vector2(20, 20), Color.Red, Content.Load<Texture2D>("BackBarTexture")));
            obj2 = new GameObject(Content.Load<Texture2D>("GreenMushroom"), null, new Vector2(midX + 50, midY + 120), GraphicsDevice.Viewport.Bounds, true);
            level = new Level(player, Content.Load<Texture2D>("map"), new Entity[] { npc }, new GameObject[] { obj, obj2}, new DisplayBar[] {playerManager.getHealthBar()});
            // Initialize list of game screens, and add screens. Menu should be the first screen active.
            screens = new List<Screen>(); 
            screens.Add(new Screen("Menu", false));
            screens.Add(new Screen("Normal", true));
            screens.Add(new Screen("Telekinesis-Select", false));
            screens.Add(new Screen("Telekinesis-Move", false));
            inputManager = new InputManager(this, player, level, playerManager, screens);
            cursor = Content.Load<Texture2D>("cursor");
            // TODO: use this.Content to load your game content here
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
            inputManager.getPlayerManager().updateHealthCooldown();
            inputManager.update(gameTime);
            level.updateProjectiles();
            level.updateNpcs();
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
