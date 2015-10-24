using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the whole game
    /// </summary>

    public class Game1 : Game {

        private readonly GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Texture2D playerTexture;
        private Player player;
        private Npc npc;
        private Npc npc2;
        private Npc npc3;
        private GameObject obj;
        private GameObject obj2;
        private Level level;
        private Level level1;
        private Level level2;
        private List<Level> levels;
        private Door door;
        private Door door2;
        private PlayerManager playerManager;
        private InputManager inputManager;
        private Menu pauseMenu;
        private Texture2D cursor;
        private MouseState mouse;
        private Texture2D target;
        private Target targetReticle;
        private Texture2D startMenu;

        private Texture2D pixel;

        private Token token1;
        private Token token2;
        private Token token3;
        private Token token4; 

        private Texture2D side1;
        private Texture2D side2;
        private Texture2D side3;

        private Song factorySong;
        private SoundEffect effect;

        private int midX;
        private int midY;
        private int width;
        private int height;

        private int levelIndex;

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
        /// Sets the game's level
        /// </summary>
        /// <param name="level">The level to set</param>
        public void setLevel(Level level) {
            this.level = level;
        }

        /// <summary>
        /// Returns the level at the specified index
        /// </summary>
        /// <param name="i">The index to retrieve</param>
        /// <returns>Returns the level at the specified index</returns>
        public Level getLevelByIndex(int i) {
            return levels[i];
        }

        /// <summary>
        /// Returns the level list
        /// </summary>
        /// <returns>Returns the list of levels</returns>
        public List<Level> getLevelList() {
            return levels;
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns the input manager for the game
        /// </summary>
        /// <returns>Returns the input manager</returns>
        public InputManager getInputManager() {
            return inputManager;
        }

        /// <summary>
        /// Sets the level index for the game
        /// </summary>
        /// <param name="index">The index to be set</param>
        public void setLevelIndex(int index) {
            levelIndex = index;
        }

        /// <summary>
        /// Returns the level index for the game
        /// </summary>
        /// <returns>Returns the level index</returns>
        public int getLevelIndex() {
            return levelIndex;
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

            startMenu = Content.Load<Texture2D>("menus/StartMenu");

            playerTexture = Content.Load<Texture2D>("sprites/entities/player/Standing1");

            midX = (graphics.PreferredBackBufferWidth - playerTexture.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playerTexture.Height) / 2;

            player = new Player(playerTexture, Vector2.Zero, Direction.SOUTH, 100, 50, 0, 3);
            player.setProjectile(new Projectile(player, Content.Load<Texture2D>("sprites/projectiles/Bullet"), 5, 250));
            playerManager = new PlayerManager(player, new DisplayBar(Content.Load<Texture2D>("ui/HealthBarTexture"), new Vector2(20, 20), Color.Red, Content.Load<Texture2D>("ui/BackBarTexture")), new DisplayBar(Content.Load<Texture2D>("ui/ManaBarTexture"), new Vector2(20, 50), Color.Blue, Content.Load<Texture2D>("ui/BackBarTexture")));
            player.loadTextures(Content);

            npc = new Npc(this, Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand1"), new Vector2(midX + 148, midY + 135), Direction.EAST, new NpcDefinition("Normie", new string[0], new int[0]), 150, 0x5);
            npc2 = new Npc(this, Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand1"), new Vector2(midX + 350, midY + 100), Direction.EAST, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            npc3 = new Npc(this, Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand2"), new Vector2(midX + 240, midY + 123), Direction.NORTH, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);
            npc2.setProjectile(new Projectile(npc2, Content.Load<Texture2D>("sprites/projectiles/Bullet"), 5, 500));
            npc3.setProjectile(new Projectile(npc3, Content.Load<Texture2D>("sprites/projectiles/Bullet"), 10, 500));
            Npc npc4 = new Npc(this, Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand2"), new Vector2(50, 50), Direction.WEST, new NpcDefinition("Normie4", new string[0], new int[0]), 150, 0x5);
            Npc npc5 = new Npc(this, Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand2"), new Vector2(150, 130), Direction.SOUTH, new NpcDefinition("Normie5", new string[0], new int[0]), 150, 0x5);

            obj2 = new GameObject(Content.Load<Texture2D>("sprites/objects/CardboardBox"), new Vector2(midX + 20, midY + 65), true);
            obj = new GameObject(Content.Load<Texture2D>("sprites/objects/CardboardBox"), new Vector2(midX + 20, midY + 205), true);

            door = new Door(Content.Load<Texture2D>("sprites/objects/DoorTexture"), null, new Vector2(midX + 420, midY + 200), Direction.EAST, false, true, 20, 60);
            door2 = new Door(Content.Load<Texture2D>("sprites/objects/DoorTexture"), null, new Vector2(0, midY + 200), Direction.WEST, false, false, 20, 60);
            side1 = Content.Load<Texture2D>("sprites/objects/BronzeCoinSide");
            side2 = Content.Load<Texture2D>("sprites/objects/SilverCoinSide");
            side3 = Content.Load<Texture2D>("sprites/objects/GoldCoinSide");

            token1 = new Token(Content.Load<Texture2D>("sprites/objects/BronzeCoinFront"), new Vector2(midX + 230, midY + 95), TokenType.BRONZE, side1);
            token2 = new Token(Content.Load<Texture2D>("sprites/objects/SilverCoinFront"), new Vector2(midX + 230, midY + 225), TokenType.SILVER, side2);
            token3 = new Token(Content.Load<Texture2D>("sprites/objects/GoldCoinFront"), new Vector2(200, 200), TokenType.GOLD, side3);
            token4 = new Token(Content.Load<Texture2D>("sprites/objects/GoldCoinFront"), new Vector2(200, 200), TokenType.GOLD, side3);

            Texture2D wallText = Content.Load<Texture2D>("sprites/objects/WallTexture");

            Wall wall1 = new Wall(wallText, null, new Vector2(120, 250), Direction.EAST, false, false, 120, 20);
            Wall wall2 = new Wall(wallText, null, new Vector2(120, 350), Direction.EAST, false, false, 120, 20);
            Wall wall3 = new Wall(wallText, null, new Vector2(100, 250), Direction.EAST, false, false, 20, 120);
            Wall wall4 = new Wall(wallText, null, new Vector2(650, 100), Direction.EAST, false, false, 120, 20);
            Wall wall5 = new Wall(wallText, null, new Vector2(650, 200), Direction.EAST, false, false, 120, 20);
            Wall wall6 = new Wall(wallText, null, new Vector2(770, 100), Direction.EAST, false, false, 20, 120);

            Wall[] walls1 = { wall1, wall2, wall3, wall4, wall5, wall6 };
            level1 = new Level(this, player, Content.Load<Texture2D>("sprites/levels/Level1"), new Npc[] { npc, npc2, npc5 }, new GameObject[] { obj, obj2 }, new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() }, new Token[] { token1, token2, token3 }, new Door[] { door }, new Wall[] { }, new ThoughtBubble[] { }, 1);
            level2 = new Level(this, player, Content.Load<Texture2D>("sprites/levels/Leve1Map"), new Npc[] { npc3, npc4 }, new GameObject[] { }, new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() }, new Token[] { token4 }, new Door[] { door2 }, walls1, new ThoughtBubble[] { new ThoughtBubble(Content.Load<Texture2D>("sprites/thoughts/PassBubble1"), new Vector2(0, 0), npc3, false, false) }, 2);
            levels = new List<Level>();
            levels.Add(level1);
            levels.Add(level2);
            level = levels[0];
            levelIndex = 0;

            Button[] menuButtons = { new Button(Content.Load<Texture2D>("menus/assets/button_mind_read"), new Vector2(270, 140)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_clairvoyance"), new Vector2(270, 220)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_confusion"), new Vector2(270, 310)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_dash"), new Vector2(355, 140)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_slow_time"), new Vector2(355, 220)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_invisibility"), new Vector2(355, 310)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_fire_bolt"), new Vector2(445, 140)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_ice_bolt"), new Vector2(445, 220)),
                                       new Button(Content.Load<Texture2D>("menus/assets/button_lightning_bolt"), new Vector2(445, 310)) };
            pauseMenu = new Menu(Content.Load<Texture2D>("menus/PausePlaceholderScreen"), menuButtons);
            targetReticle = new Target(Content.Load<Texture2D>("sprites/cursors/TargetingCursor"));

            factorySong = Content.Load<Song>("audio/songs/Factory");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(factorySong);

            Screen[] screens = { new Screen("Menu"), new Screen("Normal", true), new Screen("Telekinesis-Select"), new Screen("Telekinesis-Move"), new Screen("Start") };
            inputManager = new InputManager(this, player, level, pauseMenu, targetReticle, playerManager, screens, new MindRead(Content.Load<Texture2D>("sprites/thoughts/PassBubble1")));
            level.setInputManager(inputManager);
            pauseMenu.setInputManager(inputManager);
            inputManager.setDeathManager(new DeathManager(inputManager));

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");
            target = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            npc.setPath(new AIPath(npc, this, new int[] { midX - 100, midY - 100, midX + 100, midY + 135 }, new int[0], new Direction[] { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH }));
            npc3.setPath(new AIPath(npc3, this, new int[] { midX - 100, midY - 100, midX + 100, midY + 150 }, new int[0], new Direction[] { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH }));
            npc4.setPath(new AIPath(npc4, this, new int[] { 200, 60 }, new int[0], new Direction[] { Direction.EAST, Direction.WEST }));
            npc5.setPath(new AIPath(npc5, this, new int[] { 200, 150 }, new int[0], new Direction[] { Direction.EAST, Direction.WEST }));

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
            if (level.isActive()) {
                level.updateProjectiles();
                level.updateNpcs(gameTime);
            }
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
                if (level.getMode() < 1) {
                    spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
                } else {
                    spriteBatch.Draw(target, new Vector2(mouse.X, mouse.Y), Color.White);
                }
            }
            if (inputManager.getScreenManager().getActiveScreen().getName() == "Start") {
                spriteBatch.Draw(startMenu, new Vector2(-290, -100), Color.White);
            }
            spriteBatch.End();
        }
    }
}
