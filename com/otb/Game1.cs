﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        private SpriteFont font1;
        private SpriteFont font2;
        private SpriteFont font3;
        private SpriteFont font4;

        private Texture2D pixel;
        //private PowerBar powerBar;

        private Song factorySong;
        private SoundEffect dashSound;
        private SoundEffect buttonSound;
        private SoundEffect lavaSound;
        private SoundEffect paralyzeSound;
        private SoundEffect slowSound;
        private SoundEffect boltSound;

        private Screen[] screens;

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
        /// Returns the font used when the player gains health/mana
        /// </summary>
        /// <returns>Returns the font used when the player gains health/mana</returns>
        public SpriteFont getDropFont() {
            return font4;
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
            height = 520;
            midX = (width / 2);
            midY = (height - 40) / 2;

            factorySong = Content.Load<Song>("audio/songs/Factory");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(factorySong);

            font1 = Content.Load<SpriteFont>("fonts/font1");
            font2 = Content.Load<SpriteFont>("fonts/font2");
            font3 = Content.Load<SpriteFont>("fonts/font3");
            font4 = Content.Load<SpriteFont>("fonts/font4");

            boltSound = Content.Load<SoundEffect>("audio/sound effects/boltSound");
            dashSound = Content.Load<SoundEffect>("audio/sound effects/dashSound");
            buttonSound = Content.Load<SoundEffect>("audio/sound effects/buttonSound");
            lavaSound = Content.Load<SoundEffect>("audio/sound effects/lavaSound");
            paralyzeSound = Content.Load<SoundEffect>("audio/sound effects/paralyzeSound");
            slowSound = Content.Load<SoundEffect>("audio/sound effects/slowSound");
            startMenu = Content.Load<Texture2D>("menus/Title Screen");
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
            Texture2D green = Content.Load<Texture2D>("ui/EnemyBarText");
            Texture2D mana = Content.Load<Texture2D>("ui/ManaBarTexture");
            Texture2D normBox = Content.Load<Texture2D>("sprites/objects/KeyOutline");
            Texture2D nullBox = Content.Load<Texture2D>("sprites/objects/KeyOutlineNull");
            Texture2D key = Content.Load<Texture2D>("sprites/objects/KeyFrame1");
            //Texture2D powerbarText = Content.Load<Texture2D>("ui/powerbar");
            Texture2D HealthLaserV = Content.Load<Texture2D>("sprites/objects/HPLaser");
            Texture2D HealthLaserH = Content.Load<Texture2D>("sprites/objects/HPLaserHorizontal");
            Texture2D ManaLaserV = Content.Load<Texture2D>("sprites/objects/ManaLaser");
            Texture2D ManaLaserH = Content.Load<Texture2D>("sprites/objects/ManaLaserHorizontal");
            Texture2D limitationField = Content.Load<Texture2D>("sprites/objects/PlayerLimitationField");

            //MENUS and MENU BUTTONS
            Texture2D button1 = Content.Load<Texture2D>("menus/assets/button_instructions");
            Texture2D button2 = Content.Load<Texture2D>("menus/assets/button_quit");
            Texture2D button3 = Content.Load<Texture2D>("menus/assets/button_confusion");
            Texture2D button4 = Content.Load<Texture2D>("menus/assets/button_dash");
            Texture2D button5 = Content.Load<Texture2D>("menus/assets/button_slow_time");
            Texture2D[] buttonTextures = { button1, button2, button3, button4, button5 };
            Button[] menuButtons = { new Button(button1, new Vector2(270F, 140F), 0), new Button(button2, new Vector2(270F, 220F), 1),
                                   new Button(button3, new Vector2(270F, 310F), 0), new Button(button4, new Vector2(270F, 310F), 0),
                                   new Button(button5, new Vector2(270F, 310F), 0),};
            Texture2D pauseScreen = Content.Load<Texture2D>("menus/PausePlaceholderScreen");
            pauseMenu = new Menu(pauseScreen, menuButtons);

            //GAME OBJECTS
            Texture2D desk = Content.Load<Texture2D>("sprites/objects/Desk");
            GameObject desk1 = new GameObject(desk, new Vector2(125.0F, 70.0F), true);
            GameObject desk2 = new GameObject(desk, new Vector2(600.0F, 300.0F), true);

            //COLLECTIBLES 
            Texture2D bronze = Content.Load<Texture2D>("sprites/objects/BronzeBar");
            Texture2D silver = Content.Load<Texture2D>("sprites/objects/SilverBar");
            Texture2D gold = Content.Load<Texture2D>("sprites/objects/GoldBar");
            Token token1 = new Token(bronze, new Vector2(midX + 260F, midY + 140F), TokenType.Bronze);
            Token token2 = new Token(silver, new Vector2(midX, midY), TokenType.Silver);

            //WALLS
            Texture2D wall = Content.Load<Texture2D>("sprites/objects/WallTexture");

            //CUBICLES
            Cubicle cube1 = new Cubicle(80F, 30F, 150, 150, this, Direction.East, wall);
            cube1.addObject(desk1);
            Cubicle cube2 = new Cubicle(80F, 280F, 150, 150, this, Direction.East, wall);
            Cubicle cube3 = new Cubicle(width - 230F, 30F, 150, 150, this, Direction.West, wall);
            cube3.addObject(token1);
            Cubicle cube4 = new Cubicle(width - 230F, 280F, 150, 150, this, Direction.West, wall);
            cube4.addObject(desk2);

            //DOORS
            Texture2D door = Content.Load<Texture2D>("sprites/objects/DoorOpen");
            Texture2D doorClosed = Content.Load<Texture2D>("sprites/objects/Door");
            Door door1 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, height - 51F), Direction.South, false, true, 64, 10, true);
            Door door2 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, 0F), Direction.North, false, false, 64, 10, true);

            //NPCS
            Texture2D hitsplat = Content.Load<Texture2D>("ui/Hitsplat");
            Texture2D male1 = Content.Load<Texture2D>("sprites/entities/npcs/Standing1");
            Texture2D male2 = Content.Load<Texture2D>("sprites/entities/npcs/Standing2");
            Texture2D lineofsight = Content.Load<Texture2D>("ui/LOS");
            Npc npc = new Npc(this, male1, lineofsight, new Vector2(430F, height - 135F), Direction.East, new NpcDefinition("Normie", new string[0], new int[0]), 150, 0x5);
            Npc npc2 = new Npc(this, male2, lineofsight, new Vector2(80F, 205F), Direction.East, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            Npc npc3 = new Npc(this, male2, lineofsight, new Vector2(666F, 205F), Direction.East, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);

            midX = (graphics.PreferredBackBufferWidth - playur.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playur.Height) / 2;
            player = new Player(playur, new Vector2(125F, 295F), Direction.South, 100, 50, 0, 3);
            player.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(player.getLocation().X + (hitsplat.Width / 2), player.getLocation().Y + (hitsplat.Height / 2))));
            Projectile p = new Projectile(player, lightningOrb, 5, 250, 0.25F, boltSound);
            p.setDamage(25);
            player.setProjectile(p);

            Texture2D teleText = Content.Load<Texture2D>("ui/Telekinesis");
            Texture2D dashText = Content.Load<Texture2D>("ui/Dash");
            Texture2D mindText = Content.Load<Texture2D>("ui/Mind Read");
            Texture2D confText = Content.Load<Texture2D>("ui/Confuse");
            Texture2D slowText = Content.Load<Texture2D>("ui/Slowmo");
            Texture2D boltText = Content.Load<Texture2D>("ui/Bolt");

            AbilityIcon tk = new AbilityIcon(teleText, new Vector2(0.0F, height - 41.0F), font3, "Q");
            AbilityIcon ds = new AbilityIcon(dashText, new Vector2(40.0F, height - 41.0F), font3, "W");
            AbilityIcon mr = new AbilityIcon(mindText, new Vector2(80.0F, height - 41.0F), font3, "E");
            AbilityIcon cf = new AbilityIcon(confText, new Vector2(120.0F, height - 41.0F), font3, "S");
            AbilityIcon sm = new AbilityIcon(slowText, new Vector2(160.0F, height - 41.0F), font3, "A");
            AbilityIcon bt = new AbilityIcon(boltText, new Vector2(200.0F, height - 41.0F), font3, "Space");

            PowerBar powerBar = new PowerBar(new AbilityIcon[] { tk, ds, mr, cf, sm, bt });
            KeyBox keyBox = new KeyBox(new Texture2D[] { normBox, nullBox, key }, new Vector2(750F, 20F));
            playerManager = new PlayerManager(player, Content, new DisplayBar(health, font2, new Vector2(240.0F, height - 41.0F), back, 560, 20), new DisplayBar(mana, font2, new Vector2(240.0F, height - 21.0F), back, 560, 20), keyBox, buttonTextures, powerBar);
            player.loadTextures(Content);
            npc.loadNPCTextures(Content);
            npc2.loadNPCTextures(Content);
            npc3.loadNPCTextures(Content);

            //Pits

            Texture2D lavaPit = Content.Load<Texture2D>("sprites/objects/Lava");
            Texture2D buttonOn = Content.Load<Texture2D>("sprites/objects/PressButtonOn");
            Texture2D buttonOff = Content.Load<Texture2D>("sprites/objects/PressButtonOff");
            Texture2D buttonNull = Content.Load<Texture2D>("sprites/objects/PressButtonDeactivated");

            LavaPit p2 = new LavaPit(lavaPit, new Vector2(0F, 200F), lavaSound.CreateInstance(), 480, 128);

            HPLaser laz1 = new HPLaser(HealthLaserV, new Vector2(200F, 300F), null, 200, 20, true);
            PlayerLimitationField plf1 = new PlayerLimitationField(limitationField, new Vector2(400F, 400F), null, 200, 200);
            LaserButton lb1 = new LaserButton(new Texture2D[] { buttonOn, buttonOff, buttonNull }, new Vector2(150F, 300F), false, false, laz1);


            //LEVELS
            //LEVEL 1
            List<GameObject> Level1Objects = new List<GameObject>();
            Level1Objects.Add(door1);
            Level1Objects.Add(token1);
            Level1Objects.Add(token2);
            Texture2D l1 = Content.Load<Texture2D>("sprites/levels/Level1Map");
            Level level1 = new Level(this, player, l1, new Npc[] { npc, npc2, npc3 }, Level1Objects.ToArray(), 0);
            level1.addCubicle(cube1);
            level1.addCubicle(cube2);
            level1.addCubicle(cube3);
            level1.addCubicle(cube4);
            level1.setPlayerOrigin(new Vector2(125F, 295F));
            //level2.setPlayerOrigin(new Vector2(40F, 391F));

            //LEVEL 2
            List<GameObject> Level2Objects = new List<GameObject>();
            Level2Objects.Add(door2);
            Level2Objects.Add(p2);
            Level2Objects.Add(laz1);
            Level2Objects.Add(plf1);
            Level2Objects.Add(lb1);
            Texture2D l2 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level2 = new Level(this, player, l2, new Npc[] { }, Level2Objects.ToArray(), 0);
            //level2.setPlayerOrigin(new Vector2(100F, 100F));

            levels = new List<Level>();
            levels.Add(level1);
            levels.Add(level2);
            //levels.Add(level3);
            level = levels[0];
            levelIndex = 0;

            Texture2D controls = Content.Load<Texture2D>("menus/Controls");
            Texture2D about = Content.Load<Texture2D>("menus/About");
            screens = new Screen[] { new TitleScreen(startMenu, controls, about, font1, "Normal", true) };

            MindRead read = new MindRead(2, 1, 20, 1000, 200, 100, true, false, button1);
            read.setPlayerManager(playerManager);

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");
            Texture2D targ = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");
            target = new Target(targ);

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");
            Texture2D targetting = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");
            target = new Target(targetting);

            inputManager = new InputManager(this, player, level, pauseMenu, target, playerManager, screens, read);
            keyBox.update(inputManager);
            level.setInputManager(inputManager);
            pauseMenu.setInputManager(inputManager);
            inputManager.setDeathManager(new DeathManager(inputManager));

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            Projectile n1 = new Projectile(npc, fireOrb, 5, 250, boltSound);
            n1.setDamage(33);
            Projectile n2 = new Projectile(npc2, lightningOrb, 10, 500, boltSound);
            n2.setDamage(75);
            Projectile n3 = new Projectile(npc3, iceOrb, 7, 333, boltSound);
            n3.setDamage(20);
            npc.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc.getLocation().X, npc.getLocation().Y - 5.0F), null, 64, 15));
            npc.getDisplayBar().setColor(Color.Red);
            npc.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc.getLocation().X + (hitsplat.Width / 2), npc.getLocation().Y + (hitsplat.Height / 2))));
            npc.setPath(new AIPath(npc, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            npc.setProjectile(n1);
            npc2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2.getLocation().X, npc2.getLocation().Y - 5.0F), null, 64, 15));
            npc2.getDisplayBar().setColor(Color.Red);
            npc2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2.getLocation().X + (hitsplat.Width / 2), npc2.getLocation().Y + (hitsplat.Height / 2))));
            npc2.setPath(new AIPath(npc2, this, new int[] { 80, 175 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc2.setProjectile(n2);
            npc3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc3.getLocation().X, npc3.getLocation().Y - 5.0F), null, 64, 15));
            npc3.getDisplayBar().setColor(Color.Red);
            npc3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc3.getLocation().X + (hitsplat.Width / 2), npc3.getLocation().Y + (hitsplat.Height / 2))));
            npc3.setPath(new AIPath(npc3, this, new int[] { 570, 665 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc3.setProjectile(n3);
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
            bool busy = false;
            foreach (Screen s in screens) {
                if (s.isActive()) {
                    s.update(gameTime);
                    busy = true;
                }
            }
            mouse = Mouse.GetState();
            if (busy) {
                return;
            }
            playerManager.updateHealthCooldown();
            inputManager.update(gameTime);
            if (level.isActive()) {
                level.updateProjectiles();
                level.updateNpcs(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
            bool busy = false;
            spriteBatch.Begin();
            foreach (Screen s in screens) {
                if (s.isActive()) {
                    s.draw(spriteBatch);
                    busy = true;
                    break;
                }
            }
            if (mouse != null) {
                if (level.getMode() < 1) {
                    spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
                } else {
                    spriteBatch.Draw(target.getTexture(), new Vector2(mouse.X - (target.getTexture().Width / 2F), mouse.Y - (target.getTexture().Height / 2F)), Color.White);
                }
            }
            if (busy) {
                spriteBatch.End();
                return;
            }
            level.draw(spriteBatch);
            /*if (pauseMenu.isActive()) {
                pauseMenu.draw(spriteBatch);
            }
            if (inputManager.getScreenManager().getActiveScreen().getName() == "Start") {
                spriteBatch.Draw(startMenu, Vector2.Zero, Color.White);
            } else if (inputManager.getScreenManager().getActiveScreen().getName() == "Instructions") {
                spriteBatch.Draw(instructions, Vector2.Zero, Color.White);
            }*/
            spriteBatch.End();
        }
    }
}
