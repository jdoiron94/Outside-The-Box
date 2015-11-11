using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using OutsideTheBox.com.otb.api.wrapper;
using OutsideTheBox.com.otb.api.wrapper.locatable;
using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles the whole game
    /// </summary>

    public class Game1 : Game {

        private readonly GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Player player;
        private Level level;
        private List<Level> levels;
        private PlayerManager playerManager;
        private InputManager inputManager;
        private Menu pauseMenu;
        private Texture2D cursor;
        private Target target;
        private MouseState mouse;
        private Texture2D startMenu;
        private Texture2D instructions;
        private SpriteFont font;

        private Texture2D pixel;
        //private PowerBar powerBar;

        private Song factorySong;
        private SoundEffect dashSound;
        private SoundEffect boltSound;

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
        /// <param name="index">The index to retrieve</param>
        /// <returns>Returns the level at the specified index</returns>
        public Level getLevel(int index) {
            return levels[index];
        }

        /// <summary>
        /// Returns the level list
        /// </summary>
        /// <returns>Returns the list of levels</returns>
        public List<Level> getLevels() {
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
        public void setLevel(int index) {
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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 521;
            graphics.ApplyChanges();
            Window.Title = "Outside The Box";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            width = 800;
            height = 521;

            factorySong = Content.Load<Song>("audio/songs/Factory");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(factorySong);

            font = Content.Load<SpriteFont>("fonts/font1");

            boltSound = Content.Load<SoundEffect>("audio/Sound Effects/boltSound");
            dashSound = Content.Load<SoundEffect>("audio/Sound Effects/dashSound");
            startMenu = Content.Load<Texture2D>("menus/StartMenu");
            instructions = Content.Load<Texture2D>("menus/instructions");

            Texture2D playur = Content.Load<Texture2D>("sprites/entities/player/Standing1");
            Texture2D bullet = Content.Load<Texture2D>("sprites/projectiles/BulletOrb");
            Texture2D fireOrb = Content.Load<Texture2D>("sprites/projectiles/FireOrb");
            Texture2D iceOrb = Content.Load<Texture2D>("sprites/projectiles/IceOrb");
            Texture2D confusionOrb = Content.Load<Texture2D>("sprites/projectiles/ConfusionOrb");
            Texture2D lightningOrb = Content.Load<Texture2D>("sprites/projectiles/LightningOrb");
            Texture2D paralysisOrb = Content.Load<Texture2D>("sprites/projectiles/ParalysisOrb");
            Texture2D health = Content.Load<Texture2D>("ui/HealthBarTexture");
            Texture2D back = Content.Load<Texture2D>("ui/BackBarTexture");
            Texture2D mana = Content.Load<Texture2D>("ui/ManaBarTexture");
            Texture2D normBox = Content.Load<Texture2D>("sprites/objects/KeyOutline");
            Texture2D nullBox = Content.Load<Texture2D>("sprites/objects/KeyOutlineNull");
            Texture2D key = Content.Load<Texture2D>("sprites/objects/KeyFrame1");
            Texture2D powerbarText = Content.Load<Texture2D>("ui/powerbar");
            
            Texture2D button1 = Content.Load<Texture2D>("menus/assets/button_instructions");
            Texture2D button2 = Content.Load<Texture2D>("menus/assets/button_quit");
            //Texture2D button1 = Content.Load<Texture2D>("menus/assets/button_mind_read");
            //Texture2D button2 = Content.Load<Texture2D>("menus/assets/button_clairvoyance");
            Texture2D button3 = Content.Load<Texture2D>("menus/assets/button_confusion");
            Texture2D button4 = Content.Load<Texture2D>("menus/assets/button_dash");
            Texture2D button5 = Content.Load<Texture2D>("menus/assets/button_slow_time");
            //Texture2D button6 = Content.Load<Texture2D>("menus/assets/button_invisibility");
            //Texture2D button7 = Content.Load<Texture2D>("menus/assets/button_fire_bolt");
            //Texture2D button8 = Content.Load<Texture2D>("menus/assets/button_ice_bolt");
            //Texture2D button9 = Content.Load<Texture2D>("menus/assets/button_lightning_bolt");

            Texture2D pOff = Content.Load<Texture2D>("sprites/objects/PressButtonOff");
            Texture2D pOn = Content.Load<Texture2D>("sprites/objects/PressButtonOn");
            Texture2D pDeactivated = Content.Load<Texture2D>("sprites/objects/PressButtonDeactivated");

            Texture2D bOpenV = Content.Load<Texture2D>("sprites/objects/BarrierOpenVertical");
            Texture2D bClosedV = Content.Load<Texture2D>("sprites/objects/BarrierClosedVertical");

            Texture2D[] buttonTextures = { button1, button2, button3, button4, button5};
            Button[] menuButtons = { new Button(button1, new Vector2(270F, 140F), 0), new Button(button2, new Vector2(270F, 220F), 1),
                                   new Button(button3, new Vector2(270F, 310F), 0), new Button(button4, new Vector2(270F, 310F), 0),
                                   new Button(button5, new Vector2(270F, 310F), 0),};

            midX = (graphics.PreferredBackBufferWidth - playur.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playur.Height) / 2;
            player = new Player(playur, new Vector2(125F, 295F), Direction.South, 100, 50, 0, 3);
            player.setProjectile(new Projectile(player, lightningOrb, 5, 250, 0.25F, boltSound));
            PowerBar powerBar = new PowerBar(powerbarText, new Vector2(0F, height - 41F));
            KeyBox keyBox = new KeyBox(new Texture2D[] { normBox, nullBox, key }, new Vector2(750F, 20F));
            playerManager = new PlayerManager(player, Content, new DisplayBar(health, new Vector2(252F, height - 41F), Color.Red, back, 549, 20), new DisplayBar(mana, new Vector2(252F, height - 21F), Color.Blue, back, 549, 21), keyBox, buttonTextures, powerBar);
            player.loadTextures(Content);
            

            Texture2D male1 = Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand1");
            Texture2D male2 = Content.Load<Texture2D>("sprites/entities/npcs/NormieMaleStand2");
            Npc npc = new Npc(this, male1, new Vector2(430F, height - 135F), Direction.East, new NpcDefinition("Normie", new string[0], new int[0]), 150, 0x5);
            Npc npc2 = new Npc(this, male2, new Vector2(80F, 205F), Direction.East, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            Npc npc3 = new Npc(this, male2, new Vector2(666F, 205F), Direction.East, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);
            //Npc npc2 = new Npc(this, male1, new Vector2(midX + 350F, midY + 100F), Direction.East, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            //Npc npc3 = new Npc(this, male2, new Vector2(midX + 240F, midY + 123F), Direction.North, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);
            //npc2.setProjectile(new Projectile(npc2, bullet, 10, 500, boltSound));
            //npc3.setProjectile(new Projectile(npc3, bullet, 10, 500, boltSound));
            //Npc npc4 = new Npc(this, male2, new Vector2(50F, 50F), Direction.West, new NpcDefinition("Normie4", new string[0], new int[0]), 150, 0x5);
            //Npc npc5 = new Npc(this, male2, new Vector2(150F, 130F), Direction.South, new NpcDefinition("Normie5", new string[0], new int[0]), 150, 0x5);
            //Npc npc6 = new Npc(this, male2, new Vector2(400F, 200F), Direction.South, new NpcDefinition("Normie6", new string[0], new int[0]), 150, 0x5);

            //Npc npc7 = new Npc(this, male2, new Vector2(50F, 150F), Direction.South, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);
            //Npc npc8 = new Npc(this, male2, new Vector2(200F, 150F), Direction.South, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);
            //Npc npc9 = new Npc(this, male2, new Vector2(350F, 150F), Direction.South, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);
            //Npc npc10 = new Npc(this, male2, new Vector2(500F, 150F), Direction.South, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);

            //npc6.setProjectile(new Projectile(npc6, bullet, 10, 500, boltSound));
            //npc7.setProjectile(new Projectile(npc7, bullet, 10, 500, boltSound));
            //npc8.setProjectile(new Projectile(npc8, bullet, 10, 500, boltSound));
            //npc9.setProjectile(new Projectile(npc9, bullet, 10, 500, boltSound));
            //npc10.setProjectile(new Projectile(npc10, bullet, 10, 500, boltSound));

            Texture2D door = Content.Load<Texture2D>("sprites/objects/DoorOpen");
            Texture2D doorClosed = Content.Load<Texture2D>("sprites/objects/Door");
            Door door1 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, height - 51F), Direction.East, false, true, 64, 10, true);
            //Door door2 = new Door(door, null, new Vector2(0F, height - 89F), Direction.West, false, false, 10, 64, true);
            //Door door3 = new Door(door, null, new Vector2(width - 10F, height - 89F), Direction.East, false, true, 10, 64, false);
            //Door door4 = new Door(door, null, new Vector2(0F, height - 89F), Direction.West, false, false, 10, 64, true);

            barrier b1 = new barrier(new Texture2D[] { bOpenV, bClosedV }, new Vector2(200F, 400F));

            Texture2D bronze = Content.Load<Texture2D>("sprites/objects/BronzeBar");
            Texture2D silver = Content.Load<Texture2D>("sprites/objects/SilverBar");
            Texture2D gold = Content.Load<Texture2D>("sprites/objects/GoldBar");
            Token token1 = new Token(bronze, new Vector2(midX + 260F, midY + 140F), TokenType.Bronze);
            Token token2 = new Token(silver, new Vector2(midX, midY), TokenType.Silver);
            //Token token3 = new Token(gold, new Vector2(200F, 200F), TokenType.Gold);
            //Token token4 = new Token(gold, new Vector2(200F, 200F), TokenType.Gold);

            Texture2D wall = Content.Load<Texture2D>("sprites/objects/WallTexture");

            Texture2D l1 = Content.Load<Texture2D>("sprites/levels/Level1");
            //Texture2D l2 = Content.Load<Texture2D>("sprites/levels/Level1Map");
            //Texture2D l3 = Content.Load<Texture2D>("sprites/levels/StorageRoom");

            Texture2D desk = Content.Load<Texture2D>("sprites/objects/ComputerDesk");
            GameObject desk1 = new GameObject(desk, new Vector2(125F, 145F), true);

            Cubicle cube1 = new Cubicle(80F, 30F, 150, 150, this, Direction.East, wall);
            cube1.addObject(desk1);
            Cubicle cube2 = new Cubicle(80F, 280F, 150, 150, this, Direction.East, wall);
            Cubicle cube3 = new Cubicle(width - 230F, 30F, 150, 150, this, Direction.West, wall);
            cube3.addObject(token1);
            Cubicle cube4 = new Cubicle(width - 230F, 280F, 150, 150, this, Direction.West, wall);

            Key k = new Key(key, new Vector2(200F, 200F));
            Texture2D[] pBTextures = new Texture2D[] { pOn, pOff, pDeactivated };
            
            BarrierButton p1 = new BarrierButton(pBTextures, new Vector2(200F, 200F), false, false, b1);
            ActivateButton p2 = new ActivateButton(pBTextures, new Vector2(300F, 400F), false, false, p1);

            Level level1 = new Level(this, player, l1, new Npc[] { npc, npc2, npc3 }, new GameObject[] { }, new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() },
            new Token[] { token1, token2 }, new Door[] { door1 }, new Wall[] { }, new ThoughtBubble[] { }, new PressButton[] {p1, p2}, new Key[] { }, new barrier[] {b1 }, 0);
            level1.addCubicle(cube1);
            level1.addCubicle(cube2);
            level1.addCubicle(cube3);
            level1.addCubicle(cube4);
            //Level level2 = new Level(this, player, l2, new Npc[] { npc3, npc4, npc6 }, new GameObject[0], new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() }, new Token[] { token4 }, new Door[] { door2, door3 }, new Wall[] { wall1, wall2, wall3, wall4, wall5, wall6 }, new ThoughtBubble[] { new ThoughtBubble(bubble, font, Vector2.Zero, npc3, false, false) }, new PressButton[] { }, 2);
            //Level level3 = new Level(this, player, l3, new Npc[] { npc7, npc8, npc9, npc10 }, new GameObject[0], new DisplayBar[] { playerManager.getHealthBar(), playerManager.getManaBar() }, new Token[] { token5 }, new Door[] { door4 }, new Wall[] { }, new ThoughtBubble[] { }, new PressButton[] { }, 3);
            level1.setPlayerOrigin(new Vector2(125F, 295F));
            //level2.setPlayerOrigin(new Vector2(40F, 391F));
            levels = new List<Level>();
            levels.Add(level1);
            //levels.Add(level2);
            //levels.Add(level3);
            level = levels[0];
            levelIndex = 0;

            Texture2D pauseScreen = Content.Load<Texture2D>("menus/PausePlaceholderScreen");
            pauseMenu = new Menu(pauseScreen, menuButtons);

            Texture2D targ = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");
            target = new Target(targ);

            Screen[] screens = { new Screen("Menu"), new Screen("Normal", true), new Screen("Telekinesis-Select"), new Screen("Telekinesis-Move"), new Screen("Start"), new Screen("Instructions") };
            MindRead read = new MindRead(2, 1, 20, 1000, 200, 100, true, false, button1);
            inputManager = new InputManager(this, player, level, pauseMenu, target, playerManager, screens, read);
            keyBox.update(inputManager);
            level.setInputManager(inputManager);
            pauseMenu.setInputManager(inputManager);
            inputManager.setDeathManager(new DeathManager(inputManager));

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            npc.setPath(new AIPath(npc, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            npc2.setPath(new AIPath(npc2, this, new int[] { 80, 175 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc3.setPath(new AIPath(npc3, this, new int[] { 570, 665 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            //npc3.setPath(new AIPath(npc3, this, new int[] { midX - 100, midY - 100, midX + 100, midY + 150 }, new int[0], new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            //npc4.setPath(new AIPath(npc4, this, new int[] { 200, 60 }, new int[0], new Direction[] { Direction.East, Direction.West }));
            //npc5.setPath(new AIPath(npc5, this, new int[] { 200, 150 }, new int[0], new Direction[] { Direction.East, Direction.West }));
            //npc7.setPath(new AIPath(npc7, this, new int[] { 50, 50, 50 }, new int[0], new Direction[] { Direction.North, Direction.East, Direction.West }));
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
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            level.draw(spriteBatch);
            if (pauseMenu.isActive()) {
                pauseMenu.draw(spriteBatch);
            }
            if (mouse != null) {
                if (level.getMode() < 1) {
                    spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
                } else {
                    spriteBatch.Draw(target.getTexture(), new Vector2(mouse.X - (target.getTexture().Width / 2F), mouse.Y - (target.getTexture().Height / 2F)), Color.White);
                }
            }
            if (inputManager.getScreenManager().getActiveScreen().getName() == "Start") {
                spriteBatch.Draw(startMenu, new Vector2(-290F, -100F), Color.White);
            }
            if (inputManager.getScreenManager().getActiveScreen().getName() == "Instructions") {
                spriteBatch.Draw(instructions, Vector2.Zero, Color.White);
            }

            spriteBatch.End();
        }
    }
}
