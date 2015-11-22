using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;

namespace OutsideTheBox
{

    /// <summary>
    /// Class which handles the whole game
    /// </summary>

    public class Game1 : Game
    {

        private readonly GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Player player;
        private Level level;
        private List<Level> levels;
        private PlayerManager playerManager;
        private InputManager inputManager;
        private Texture2D cursor;
        private Target target;
        private MouseState mouse;
        private Texture2D startMenu;
        private SpriteFont font1;
        private SpriteFont font2;
        private SpriteFont font3;
        private SpriteFont font4;

        private Texture2D pixel;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Returns the center x coordinate of the game
        /// </summary>
        /// <returns>Returns the center x coordinate of the game, with respect to the player</returns>
        public int getMidX()
        {
            return midX;
        }

        /// <summary>
        /// Returns the center y coordinate of the game
        /// </summary>
        /// <returns>Returns the center y coordinate of the game, with respect to the player</returns>
        public int getMidY()
        {
            return midY;
        }

        /// <summary>
        /// Returns the width of the game
        /// </summary>
        /// <returns>Returns the width of the game</returns>
        public int getWidth()
        {
            return width;
        }

        /// <summary>
        /// Returns the height of the game
        /// </summary>
        /// <returns>Returns the height of the game</returns>
        public int getHeight()
        {
            return height;
        }

        /// <summary>
        /// Returns the mouse state of the game
        /// </summary>
        /// <returns>Returns the mouse state of the game</returns>
        public MouseState getMouse()
        {
            return mouse;
        }

        /// <summary>
        /// Returns an instance of the current level
        /// </summary>
        /// <returns>Returns an instance of the current level</returns>
        public Level getLevel()
        {
            return level;
        }

        /// <summary>
        /// Sets the game's level
        /// </summary>
        /// <param name="level">The level to set</param>
        public void setLevel(Level level)
        {
            this.level = level;
        }

        /// <summary>
        /// Returns the level at the specified index
        /// </summary>
        /// <param name="index">The index to retrieve</param>
        /// <returns>Returns the level at the specified index</returns>
        public Level getLevel(int index)
        {
            return levels[index];
        }

        /// <summary>
        /// Returns the level list
        /// </summary>
        /// <returns>Returns the list of levels</returns>
        public List<Level> getLevels()
        {
            return levels;
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer()
        {
            return player;
        }

        /// <summary>
        /// Returns the input manager for the game
        /// </summary>
        /// <returns>Returns the input manager</returns>
        public InputManager getInputManager()
        {
            return inputManager;
        }

        /// <summary>
        /// Sets the level index for the game
        /// </summary>
        /// <param name="index">The index to be set</param>
        public void setLevel(int index)
        {
            levelIndex = index;
        }

        /// <summary>
        /// Returns the level index for the game
        /// </summary>
        /// <returns>Returns the level index</returns>
        public int getLevelIndex()
        {
            return levelIndex;
        }

        /// <summary>
        /// Returns the font used when the player gains health/mana
        /// </summary>
        /// <returns>Returns the font used when the player gains health/mana</returns>
        public SpriteFont getDropFont()
        {
            return font4;
        }

        /// <summary>
        /// Adds a projectile to the game from an NPC
        /// </summary>
        /// <param name="projectile">The projectile to be added</param>
        public void addProjectile(Projectile projectile)
        {
            level.addProjectile(projectile);
        }

        /// <summary>
        /// Draws an outline of the bounds of a specified area
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        /// <param name="area">The area to be drawn</param>
        public void outline(SpriteBatch batch, Rectangle area)
        {
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
        protected override void Initialize()
        {
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
        protected override void LoadContent()
        {
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

            //GAME OBJECTS
            Texture2D box = Content.Load<Texture2D>("sprites/objects/CardboardBox");
            Texture2D desk = Content.Load<Texture2D>("sprites/objects/Desk");
            GameObject desk1_1 = new GameObject(desk, new Vector2(125.0F, 70.0F), true);
            GameObject desk1_2 = new GameObject(desk, new Vector2(600.0F, 300.0F), true);
            GameObject box2_1 = new GameObject(box, new Vector2(730F, 200F), true);
            GameObject box3_1 = new GameObject(box, new Vector2(700F, 400F), true);

            //COLLECTIBLES 
            Texture2D bronze = Content.Load<Texture2D>("sprites/objects/BronzeBar");
            Texture2D silver = Content.Load<Texture2D>("sprites/objects/SilverBar");
            Texture2D gold = Content.Load<Texture2D>("sprites/objects/GoldBar");
            Token token1_1 = new Token(bronze, new Vector2(midX + 260F, midY + 140F), TokenType.Bronze);
            Token token1_2 = new Token(silver, new Vector2(midX, midY), TokenType.Silver);
            Token token2_1 = new Token(silver, new Vector2(30F, 60F), TokenType.Silver);
            Token token2_2 = new Token(gold, new Vector2(730F, 400F), TokenType.Gold);
            Token token3_1 = new Token(silver, new Vector2(midX, midY), TokenType.Silver);
            Token token3_2 = new Token(silver, new Vector2(midX + 100F, midY), TokenType.Silver);

            //KEYS 
            Key key2_1 = new Key(key, new Vector2(40F, 60F));
            Key key3_1 = new Key(key, new Vector2(120F, 20F));

            //WALLS
            Texture2D wall = Content.Load<Texture2D>("sprites/objects/WallTexture");

            //CUBICLES
            //level 1 cubicles
            Cubicle cube1_1 = new Cubicle(80F, 30F, 150, 150, this, Direction.East, wall);
            Cubicle cube1_2 = new Cubicle(80F, 280F, 150, 150, this, Direction.East, wall);
            Cubicle cube1_3 = new Cubicle(width - 230F, 30F, 150, 150, this, Direction.West, wall);
            Cubicle cube1_4 = new Cubicle(width - 230F, 280F, 150, 150, this, Direction.West, wall);
            //level 2 cubicles
            Cubicle cube2_1 = new Cubicle(0F, 0F, 150, 150, this, Direction.East, wall);
            Cubicle cube2_2 = new Cubicle(width - 160, 0F, 150, 150, this, Direction.West, wall);
            Cubicle cube2_3 = new Cubicle(120F, height - 41F - 160F, 150, 150, this, Direction.North, wall);
            Cubicle cube2_4 = new Cubicle(270F, height - 41F - 160F, 250, 150, this, Direction.North, wall);
            //level 3 cubicles
            Cubicle cube3_1 = new Cubicle(300, 180F, 150, 150, this, Direction.West, wall);
            Cubicle cube3_2 = new Cubicle(640F, 0F, 150, 150, this, Direction.South, wall);


            //DOORS
            Texture2D door = Content.Load<Texture2D>("sprites/objects/DoorOpen");
            Texture2D doorClosed = Content.Load<Texture2D>("sprites/objects/Door");
            Door door1to2 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, height - 51F), Direction.South, false, true, 64, 10, true);
            Door door2to1 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, 0F), Direction.North, false, false, 64, 10, true);
            Door door2to3 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(width - 10F, height - 200), Direction.East, false, true, 10, 64, false);
            Door door3to2 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(0F, height - 200), Direction.West, false, false, 10, 64, true);
            Door door3to4 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(120F, height - 10), Direction.South, false, true, 64, 10, false);
            Door door4to3 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(120F, 0F), Direction.North, false, false, 10, 64, true);

            //PITS, LASERS, and BARRIERS
            Texture2D lavaPit = Content.Load<Texture2D>("sprites/objects/Lava");
            Texture2D HealthLaserV = Content.Load<Texture2D>("sprites/objects/HPLaser");
            Texture2D HealthLaserH = Content.Load<Texture2D>("sprites/objects/HPLaserHorizontal");
            Texture2D ManaLaserV = Content.Load<Texture2D>("sprites/objects/ManaLaser");
            Texture2D ManaLaserH = Content.Load<Texture2D>("sprites/objects/ManaLaserHorizontal");
            Texture2D limitationField = Content.Load<Texture2D>("sprites/objects/PlayerLimitationField");
            Texture2D[] barrier1_vertical = { Content.Load<Texture2D>("sprites/objects/BarrierOpenVertical"), Content.Load<Texture2D>("sprites/objects/BarrierClosedVertical") };
            Texture2D[] barrier1_horizontal = { Content.Load<Texture2D>("sprites/objects/BarrierOpenHorizontal"), Content.Load<Texture2D>("sprites/objects/BarrierClosedHorizontal") };
            Texture2D[] barrier2_vertical = { Content.Load<Texture2D>("sprites/objects/Barrier2OpenV"), Content.Load<Texture2D>("sprites/objects/Barrier2ClosedV") };
            Texture2D[] barrier2_horizontal = { Content.Load<Texture2D>("sprites/objects/Barrier2OpenH"), Content.Load<Texture2D>("sprites/objects/Barrier2ClosedH") };
            //LavaPit p2 = new LavaPit(lavaPit, new Vector2(0F, 200F), lavaSound.CreateInstance(), 480, 128);
            //PlayerLimitationField plf1 = new PlayerLimitationField(limitationField, new Vector2(400F, 400F), lavaSound.CreateInstance(), 200, 200);
            HPLaser las2_1 = new HPLaser(HealthLaserV, new Vector2(630F, 12F), boltSound.CreateInstance(), 140, 10, true);
            Barrier bar2_1 = new Barrier(barrier2_vertical, new Vector2(145F, 18F));

            HPLaser las3_1 = new HPLaser(HealthLaserV, new Vector2(160F, 640F), boltSound.CreateInstance(), 140, 10, true);
            HPLaser las3_2 = new HPLaser(HealthLaserV, new Vector2(80F, 80F), boltSound.CreateInstance(), 140, 10, true);
            Barrier bar3_1 = new Barrier(barrier2_vertical, new Vector2(300F, 180F));

            //BUTTONS
            Texture2D buttonOn = Content.Load<Texture2D>("sprites/objects/PressButtonOn");
            Texture2D buttonOff = Content.Load<Texture2D>("sprites/objects/PressButtonOff");
            Texture2D buttonNull = Content.Load<Texture2D>("sprites/objects/PressButtonDeactivated");
            Texture2D[] button = { buttonOn, buttonOff, buttonNull };
            //LaserButton lb1 = new LaserButton(button, new Vector2(150F, 300F), false, false, laz1);
            BarrierButton barbutt2_1 = new BarrierButton(button, new Vector2(700F, 50F), false, false, bar2_1);

            BarrierButton barbutt3_1 = new BarrierButton(button, new Vector2(540F, 220F), false, false, bar3_1);
            LaserButton lasbutt3_1 = new LaserButton(button, new Vector2(700F, 40F), false, false, las3_2);


            //NPCS
            Texture2D male1 = Content.Load<Texture2D>("sprites/entities/npcs/Standing1");
            Texture2D male2 = Content.Load<Texture2D>("sprites/entities/npcs/Standing2");
            Texture2D hitsplat = Content.Load<Texture2D>("ui/Hitsplat");
            Texture2D lineofsight = Content.Load<Texture2D>("ui/LOS");
            //level 1 npcs
            Npc npc1_1 = new Npc(this, male1, lineofsight, new Vector2(430F, height - 135F), Direction.East, new NpcDefinition("Normie", new string[0], new int[0]), 150, 0x5);
            Npc npc1_2 = new Npc(this, male2, lineofsight, new Vector2(80F, 205F), Direction.East, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            Npc npc1_3 = new Npc(this, male2, lineofsight, new Vector2(666F, 205F), Direction.East, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);
            //level 2 npcs
            Npc npc2_1 = new Npc(this, male1, lineofsight, new Vector2(550F, 50F), Direction.West, new NpcDefinition("Normie4", new string[0], new int[0]), 150, 0x5);
            Npc npc2_2 = new Npc(this, male1, lineofsight, new Vector2(660F, 200F), Direction.West, new NpcDefinition("Normie5", new string[0], new int[0]), 150, 0x5);
            Npc npc2_3 = new Npc(this, male1, lineofsight, new Vector2(100F, 400F), Direction.East, new NpcDefinition("Normie6", new string[0], new int[0]), 150, 0x5);
            //level 3 npcs
            Npc npc3_1 = new Npc(this, male1, lineofsight, new Vector2(430F, height - 135F), Direction.East, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);
            Npc npc3_2 = new Npc(this, male1, lineofsight, new Vector2(80F, 205F), Direction.East, new NpcDefinition("Normie8", new string[0], new int[0]), 150, 0x5);

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
            playerManager = new PlayerManager(player, Content, new DisplayBar(health, font2, new Vector2(240.0F, height - 41.0F), back, 560, 20), new DisplayBar(mana, font2, new Vector2(240.0F, height - 21.0F), back, 560, 20), keyBox, powerBar);
            player.loadTextures(Content);

            npc1_1.loadNPCTextures(Content);
            npc1_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc1_1.getLocation().X, npc1_1.getLocation().Y - 5.0F), null, 64, 15));
            npc1_1.getDisplayBar().setColor(Color.Red);
            npc1_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_1.getLocation().X + (hitsplat.Width / 2), npc1_1.getLocation().Y + (hitsplat.Height / 2))));
            npc1_2.loadNPCTextures(Content);
            npc1_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc1_2.getLocation().X, npc1_2.getLocation().Y - 5.0F), null, 64, 15));
            npc1_2.getDisplayBar().setColor(Color.Red);
            npc1_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_2.getLocation().X + (hitsplat.Width / 2), npc1_2.getLocation().Y + (hitsplat.Height / 2))));
            npc1_3.loadNPCTextures(Content);
            npc1_3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc1_3.getLocation().X, npc1_3.getLocation().Y - 5.0F), null, 64, 15));
            npc1_3.getDisplayBar().setColor(Color.Red);
            npc1_3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_3.getLocation().X + (hitsplat.Width / 2), npc1_3.getLocation().Y + (hitsplat.Height / 2))));
            npc2_1.loadNPCTextures(Content);
            npc2_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_1.getLocation().X, npc2_1.getLocation().Y - 5.0F), null, 64, 15));
            npc2_1.getDisplayBar().setColor(Color.Red);
            npc2_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_1.getLocation().X + (hitsplat.Width / 2), npc2_1.getLocation().Y + (hitsplat.Height / 2))));
            npc2_2.loadNPCTextures(Content);
            npc2_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_2.getLocation().X, npc2_2.getLocation().Y - 5.0F), null, 64, 15));
            npc2_2.getDisplayBar().setColor(Color.Red);
            npc2_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_2.getLocation().X + (hitsplat.Width / 2), npc2_2.getLocation().Y + (hitsplat.Height / 2))));
            npc2_3.loadNPCTextures(Content);
            npc2_3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_3.getLocation().X, npc2_3.getLocation().Y - 5.0F), null, 64, 15));
            npc2_3.getDisplayBar().setColor(Color.Red);
            npc2_3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_3.getLocation().X + (hitsplat.Width / 2), npc2_3.getLocation().Y + (hitsplat.Height / 2))));
            npc3_1.loadNPCTextures(Content);
            npc3_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc3_1.getLocation().X, npc3_1.getLocation().Y - 5.0F), null, 64, 15));
            npc3_1.getDisplayBar().setColor(Color.Red);
            npc3_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc3_1.getLocation().X + (hitsplat.Width / 2), npc3_1.getLocation().Y + (hitsplat.Height / 2))));
            npc3_2.loadNPCTextures(Content);
            npc3_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc3_2.getLocation().X, npc3_2.getLocation().Y - 5.0F), null, 64, 15));
            npc3_2.getDisplayBar().setColor(Color.Red);
            npc3_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc3_2.getLocation().X + (hitsplat.Width / 2), npc3_2.getLocation().Y + (hitsplat.Height / 2))));


            //LEVELS
            //LEVEL 1
            List<GameObject> Level1Objects = new List<GameObject>();
            Level1Objects.Add(door1to2);
            Level1Objects.Add(token1_1);
            Level1Objects.Add(token1_2);
            Texture2D l1 = Content.Load<Texture2D>("sprites/levels/Level1Map");
            Level level1 = new Level(this, player, l1, new Npc[] { npc1_1, npc1_2, npc1_3 }, Level1Objects.ToArray(), 0);
            level1.addCubicle(cube1_1);
            level1.addCubicle(cube1_2);
            level1.addCubicle(cube1_3);
            level1.addCubicle(cube1_4);
            level1.setPlayerOrigin(new Vector2(165F, 100F));

            //LEVEL 2
            List<GameObject> Level2Objects = new List<GameObject>();
            Level2Objects.Add(door2to1);
            Level2Objects.Add(door2to3);
            Level2Objects.Add(box2_1);
            Level2Objects.Add(token2_1);
            Level2Objects.Add(token2_2);
            Level2Objects.Add(key2_1);
            Level2Objects.Add(las2_1);
            Level2Objects.Add(bar2_1);
            Level2Objects.Add(barbutt2_1);
            Texture2D l2 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level2 = new Level(this, player, l2, new Npc[] { npc2_1, npc2_2, npc2_3 }, Level2Objects.ToArray(), 0);
            level2.addCubicle(cube2_1);
            level2.addCubicle(cube2_2);
            level2.setPlayerOrigin(new Vector2((width - 64F) / 2F, 20F));

            //LEVEL 3
            List<GameObject> Level3Objects = new List<GameObject>();
            Level3Objects.Add(door3to2);
            Level3Objects.Add(door3to4);
            Level3Objects.Add(las3_1);
            Level3Objects.Add(las3_2);
            Level3Objects.Add(bar3_1);
            Level3Objects.Add(barbutt3_1);
            Level3Objects.Add(lasbutt3_1);
            Level3Objects.Add(key3_1);
            Texture2D l3 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level3 = new Level(this, player, l3, new Npc[] { npc3_1, npc3_2 }, Level3Objects.ToArray(), 0);
            level3.addCubicle(cube3_1);
            level3.addCubicle(cube3_2);
            level3.setPlayerOrigin(new Vector2(100F, height - 200));

            levels = new List<Level>();
            levels.Add(level1);
            levels.Add(level2);
            levels.Add(level3);
            level = levels[0];
            levelIndex = 0;

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");
            Texture2D targ = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");
            target = new Target(targ);

            Texture2D controls = Content.Load<Texture2D>("menus/Controls");
            Texture2D about = Content.Load<Texture2D>("menus/About");
            screens = new Screen[] { new TitleScreen(startMenu, controls, about, cursor, font1, "Normal", true) };

            MindRead read = new MindRead(2, 1, 20, 1000, 200, 100, true, false);
            read.setPlayerManager(playerManager);

            inputManager = new InputManager(this, player, level, target, playerManager, screens, read);
            keyBox.update(inputManager);
            level.setInputManager(inputManager);
            inputManager.setDeathManager(new DeathManager(inputManager));

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            Projectile n1 = new Projectile(npc1_1, fireOrb, 5, 250, boltSound);
            n1.setDamage(33);
            Projectile n2 = new Projectile(npc1_2, lightningOrb, 10, 500, boltSound);
            n2.setDamage(75);
            Projectile n3 = new Projectile(npc1_3, iceOrb, 7, 333, boltSound);
            n3.setDamage(20);

            npc1_1.setPath(new AIPath(npc1_1, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            npc1_1.setProjectile(n1);
            npc1_2.setPath(new AIPath(npc1_2, this, new int[] { 80, 175 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc1_2.setProjectile(n2);
            npc1_3.setPath(new AIPath(npc1_3, this, new int[] { 570, 665 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc1_3.setProjectile(n3);

            npc2_1.setPath(new AIPath(npc2_1, this, new int[] { 200, 550 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc2_2.setPath(new AIPath(npc2_2, this, new int[] { 180, 660 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc2_3.setPath(new AIPath(npc2_3, this, new int[] { 600, 300, 100, 400 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.East, Direction.North, Direction.West, Direction.South }));

            npc3_1.setPath(new AIPath(npc3_1, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            npc3_2.setPath(new AIPath(npc3_2, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));

        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bool busy = false;
            foreach (Screen s in screens)
            {
                if (s.isActive())
                {
                    s.update(gameTime);
                    busy = true;
                }
            }
            mouse = Mouse.GetState();
            if (busy)
            {
                return;
            }
            playerManager.updateHealthCooldown();
            inputManager.update(gameTime);
            if (level.isActive())
            {
                level.updateProjectiles();
                level.updateNpcs(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
            bool busy = false;
            spriteBatch.Begin();
            foreach (Screen s in screens)
            {
                if (s.isActive())
                {
                    s.draw(spriteBatch);
                    busy = true;
                    break;
                }
            }
            if (busy)
            {
                spriteBatch.End();
                return;
            }
            level.draw(spriteBatch);
            if (level.getMode() < 1)
            {
                spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
            }
            else
            {
                spriteBatch.Draw(target.getTexture(), new Vector2(mouse.X - (target.getTexture().Width / 2F), mouse.Y - (target.getTexture().Height / 2F)), Color.White);
            }
            spriteBatch.End();
        }
    }
}
