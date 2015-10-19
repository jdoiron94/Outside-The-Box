using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;

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
        private Menu pauseMenu;
        private Texture2D cursor;
        private MouseState mouse;
        private Texture2D target;

        private Texture2D pixel;

        private Token token1;
        private Token token2;
        private Token token3;

        private Texture2D side1;
        private Texture2D side2;
        private Texture2D side3;

        private Song factorysong;
        private SoundEffect effect;

        private int midX;
        private int midY;
        private int width;
        private int height;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Returns the center x coordinate of the game
        /// </summary>
        /// <returns>Returns the center x coordinate of the game, with respect to the player</returns>
        public int getMidX() {
            return midX;
        }

        /// <summary>
        /// Returns the center y coordinate of the game
        /// </summary>
        /// <returns>Returns the center y coordinate of the game, with respect to the player</returns>
        public int getMidY() {
            return midY;
        }

        /// <summary>
        /// Returns the width of the game
        /// </summary>
        /// <returns>Returns the width of the game</returns>
        public int getWidth() {
            return width;
        }

        /// <summary>
        /// Returns the height of the game
        /// </summary>
        /// <returns>Returns the height of the game</returns>
        public int getHeight() {
            return height;
        }

        /// <summary>
        /// Returns the mouse state of the game
        /// </summary>
        /// <returns>Returns the mouse state of the game</returns>
        public MouseState getMouse() {
            return mouse;
        }

        /// <summary>
        /// Returns an instance of the current level
        /// </summary>
        /// <returns>Returns an instance of the current level</returns>
        public Level getLevel() {
            return level;
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        public InputManager getInputManager() {
            return inputManager;
        }

        /// <summary>
        /// Adds a projectile to the game from an NPC
        /// </summary>
        /// <param name="projectile">The projectile to be added</param>
        public void addProjectile(Projectile projectile) {
            level.addProjectile(projectile);
        }

        /// <summary>
        /// Draws an outline of the bounds of a specified area
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        /// <param name="area">The area to be drawn</param>
        public void outline(SpriteBatch batch, Rectangle area) {
            batch.Draw(pixel, new Rectangle(area.X, area.Y, area.Width, 1), Color.Green);
            batch.Draw(pixel, new Rectangle(area.X, area.Y, 1, area.Height), Color.Green);
            batch.Draw(pixel, new Rectangle(area.X + area.Width - 1, area.Y, 1, area.Height), Color.Green);
            batch.Draw(pixel, new Rectangle(area.X, area.Y + area.Height - 1, area.Width, 1), Color.Green);
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
           
            playerTexture = Content.Load<Texture2D>("Standing1");

            midX = (graphics.PreferredBackBufferWidth - playerTexture.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playerTexture.Height) / 2;
           
            player = new Player(playerTexture, Vector2.Zero, Direction.SOUTH, 100, 50, 0, 3);
            player.setProjectile(new Projectile(player, Content.Load<Texture2D>("bullet"), 5, 250));
            playerManager = new PlayerManager(player, new DisplayBar(Content.Load<Texture2D>("HealthBarTexture"), new Vector2(20, 20), Color.Red, Content.Load<Texture2D>("BackBarTexture")), new DisplayBar(Content.Load<Texture2D>("ManaBarTexture"), new Vector2(20, 50), Color.Blue, Content.Load<Texture2D>("BackBarTexture")));
            player.loadTextures(Content);

            npc = new Npc(this, Content.Load<Texture2D>("enemy"), new Vector2(midX + 148, midY + 148), Direction.EAST, new NpcDefinition("Goku", new string[0], new int[0]), 150, 0x5);
            npc2 = new Npc(this, Content.Load<Texture2D>("npc"), new Vector2(midX, midY), Direction.EAST, new NpcDefinition("Goku2", new string[0], new int[0]), 150, 0x5);
            npc2.setProjectile(new Projectile(npc2, Content.Load<Texture2D>("bullet"), 5, 500));

            obj = new GameObject(Content.Load<Texture2D>("sprite"), new Vector2(midX + 50, midY + 220), true);
            obj2 = new GameObject(Content.Load<Texture2D>("GreenMushroom"), new Vector2(midX + 42, midY + 100), true);

            side1 = Content.Load<Texture2D>("BronzeCoinSide");
            side2 = Content.Load<Texture2D>("SilverCoinSide");
            side3 = Content.Load<Texture2D>("GoldCoinSide");

            token1 = new Token(Content.Load<Texture2D>("BronzeCoinFront"), new Vector2(midX + 120, midY + 120), TokenType.BRONZE, side1);
            token2 = new Token(Content.Load<Texture2D>("SilverCoinFront"), new Vector2(midX + 180, midY + 180), TokenType.SILVER, side2);
            token3 = new Token(Content.Load<Texture2D>("GoldCoinFront"), new Vector2(midX + 200, midY + 200), TokenType.GOLD, side3);

            level = new Level(this, player, Content.Load<Texture2D>("map"), new Npc[] { npc, npc2 }, new GameObject[] { obj, obj2 }, new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() }, new Token[] { token1, token2, token3 });

            pauseMenu = new Menu(Content.Load<Texture2D>("menu_background"), new Button[] { new Button(Content.Load<Texture2D>("resume_button"), new Vector2(100, 200)) });

            factorysong = Content.Load<Song>("Factory");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(factorysong);

            inputManager = new InputManager(this, player, level, pauseMenu, playerManager, new Screen[] { new Screen("Menu"), new Screen("Normal", true), new Screen("Telekinesis-Select"), new Screen("Telekinesis-Move") });
            level.setInputManager(inputManager);
            pauseMenu.setInputManager(inputManager);
            
            cursor = Content.Load<Texture2D>("cursor");
            target = Content.Load<Texture2D>("TargetingCursor");

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            npc.setPath(new AIPath(npc, this, new int[] { midX - 100, midY - 100, midX + 100, midY + 150 }, new int[0], new Direction[] { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH })); 

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
            
            Console.WriteLine("hello");
            
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
            if (pauseMenu.isActive()) {
                pauseMenu.draw(spriteBatch);
            }
            if (mouse != null) {
                if (level.getMode() < 1)
                {
                    spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
                }
                else
                {
                    spriteBatch.Draw(target, new Vector2(mouse.X, mouse.Y), Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
