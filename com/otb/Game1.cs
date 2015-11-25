using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
        private SpriteFont font4;

        private Texture2D pixel;
        
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

            Song factorySong = Content.Load<Song>("audio/songs/Factory");
            Song streetSong = Content.Load<Song>("audio/songs/Streets");
            Song officeSong = Content.Load<Song>("audio/songs/Office (Trimmed)");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(factorySong);

            SpriteFont font1 = Content.Load<SpriteFont>("fonts/font1");
            SpriteFont font2 = Content.Load<SpriteFont>("fonts/font2");
            SpriteFont font3 = Content.Load<SpriteFont>("fonts/font3");
            font4 = Content.Load<SpriteFont>("fonts/font4");
            SpriteFont font5 = Content.Load<SpriteFont>("fonts/font5");
            SpriteFont font6 = Content.Load<SpriteFont>("fonts/font6");

            cursor = Content.Load<Texture2D>("sprites/cursors/Cursor");
            Texture2D targ = Content.Load<Texture2D>("sprites/cursors/TargetingCursor");
            target = new Target(targ);

            Texture2D gradient = Content.Load<Texture2D>("ui/gradient");
            startMenu = Content.Load<Texture2D>("menus/Title Screen");
            Texture2D controls = Content.Load<Texture2D>("menus/Controls");
            Texture2D about = Content.Load<Texture2D>("menus/About");
            SoundEffect hoverEffect = Content.Load<SoundEffect>("audio/sound effects/menuButtonSound");
            TitleScreen title = new TitleScreen(startMenu, controls, about, cursor, font1, hoverEffect.CreateInstance(), "Normal", true);
            PauseMenu pause = new PauseMenu(gradient, controls, cursor, font4, font5, hoverEffect.CreateInstance(), "Pause", false);
            Hint hint1 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "If only someone could give me the password, or if I could brute force it...", "Hint 1", false);
            Hint hint2 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "A laser and a key. I'll need to figure out a clever way to push the button.", "Hint 2", false);
            Hint hint3 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "So many buttons. I can't push all of them myself. Hmmm...", "Hint 3", false);
            Hint hint4 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "Looks like that button in the lava is deactivated. How do I fix that? Oh and look at all that gold!", "Hint 4", false);
            Hint hint5 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "This floor seems to prevent me from regenerating... Those enemies look dangerous.", "Hint 5", false);
            Hint hint6 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "Hmm... Those enemies are really pressin' my buttons. But maybe I should leave them alone.", "Hint 6", false);
            Hint hint7 = new Hint(gradient, cursor, font4, hoverEffect.CreateInstance(), "No more levels. Thanks for playing our beta! May the box be with you.", "Hint 7", false);
            pause.addHint(hint1);
            pause.addHint(hint2);
            pause.addHint(hint3);
            pause.addHint(hint4);
            pause.addHint(hint5);
            pause.addHint(hint6);
            pause.addHint(hint7);
            screens = new Screen[] { title, pause };

            boltSound = Content.Load<SoundEffect>("audio/sound effects/boltSound");
            dashSound = Content.Load<SoundEffect>("audio/sound effects/dashSound");
            buttonSound = Content.Load<SoundEffect>("audio/sound effects/buttonSound");
            lavaSound = Content.Load<SoundEffect>("audio/sound effects/lavaSound");
            paralyzeSound = Content.Load<SoundEffect>("audio/sound effects/paralyzeSound");
            slowSound = Content.Load<SoundEffect>("audio/sound effects/slowSound");

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
            GameObject box4_1 = new GameObject(box, new Vector2(280F, 20F), true);
            GameObject box4_2 = new GameObject(box, new Vector2(40F, 360F), true);
            GameObject box5_1 = new GameObject(box, new Vector2(120F, 320F), true);
            GameObject box6_1 = new GameObject(box, new Vector2(40F, 340F), true);

            //COLLECTIBLES 
            Texture2D bronze = Content.Load<Texture2D>("sprites/objects/BronzeBar");
            Texture2D silver = Content.Load<Texture2D>("sprites/objects/SilverBar");
            Texture2D gold = Content.Load<Texture2D>("sprites/objects/GoldBar");
            SoundEffect barEffect = Content.Load<SoundEffect>("audio/sound effects/barSound");
            Token token1_1 = new Token(bronze, new Vector2(midX + 260F, midY + 140F), barEffect, TokenType.Bronze);
            Token token1_2 = new Token(silver, new Vector2(midX, midY), barEffect, TokenType.Silver);
            Token token2_1 = new Token(silver, new Vector2(20F, 60F), barEffect, TokenType.Silver);
            Token token2_2 = new Token(gold, new Vector2(730F, 400F), barEffect, TokenType.Gold);
            Token token3_1 = new Token(silver, new Vector2(midX, midY), barEffect, TokenType.Silver);
            Token token3_2 = new Token(silver, new Vector2(midX + 100F, midY), barEffect, TokenType.Silver);
            Token token4_1 = new Token(gold, new Vector2(30F, 420F), barEffect, TokenType.Gold);
            Token token4_2 = new Token(gold, new Vector2(50F, 435F), barEffect, TokenType.Gold);
            Token token4_3 = new Token(bronze, new Vector2(400F, 380F), barEffect, TokenType.Bronze);
            Token token5_1 = new Token(gold, new Vector2(460F, 380F), barEffect, TokenType.Gold);
            Token token5_2 = new Token(gold, new Vector2(470F, 400F), barEffect, TokenType.Gold);
            Token token5_3 = new Token(silver, new Vector2(640F, 50F), barEffect, TokenType.Silver);

            //KEYS 
            SoundEffect keyEffect = Content.Load<SoundEffect>("audio/sound effects/keySound");
            Key key2_1 = new Key(key, new Vector2(45F, 60F), keyEffect);
            Key key3_1 = new Key(key, new Vector2(120F, 20F), keyEffect);
            Key key5_1 = new Key(key, new Vector2(300F, 380F), keyEffect);
            Key key6_1 = new Key(key, new Vector2(690F, 50F), keyEffect);


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
            Cubicle cube3_3 = new Cubicle(60F, 0F, 150, 90, this, Direction.South, wall);
            //level 4 cubicles
            Cubicle cube4_1 = new Cubicle(690F, 0F, 110, 110, this, Direction.West, wall);
            Cubicle cube4_2 = new Cubicle(0F, 340F, 150, 130, this, Direction.East, wall);
            //level 5 cubicles
            Cubicle cube5_1 = new Cubicle(-20F, 190F, 150, 120, this, Direction.East, wall);
            Cubicle cube5_2 = new Cubicle(width - 110F, 190F, 120, 120, this, Direction.West, wall);
            Cubicle cube5_3 = new Cubicle(240F, 20F, 150, 150, this, Direction.South, wall);
            Cubicle cube5_4 = new Cubicle(240F, 320F, 150, 150, this, Direction.North, wall);
            Cubicle cube5_5 = new Cubicle(400F, 20F, 150, 150, this, Direction.South, wall);
            Cubicle cube5_6 = new Cubicle(400F, 320F, 150, 150, this, Direction.North, wall);
            //level 6 cubicles
            Cubicle cube6_1 = new Cubicle(0F, 320F, 150, 150, this, Direction.North, wall);
            Cubicle cube6_2 = new Cubicle(640F, 0F, 150, 150, this, Direction.West, wall);


            //DOORS
            Texture2D door = Content.Load<Texture2D>("sprites/objects/DoorOpen");
            Texture2D doorClosed = Content.Load<Texture2D>("sprites/objects/Door");
            Door door1to2 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, height - 51F), Direction.South, false, true, 64, 10, true);

            Door door2to1 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2((width - 64F) / 2F, 0F), Direction.North, false, false, 64, 10, true);
            Door door2to3 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(width - 10F, height - 200), Direction.East, false, true, 10, 64, false);

            Door door3to2 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(0F, height - 200), Direction.West, false, false, 10, 64, true);
            Door door3to4 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(120F, 470), Direction.South, false, true, 64, 10, false);

            Door door4to3 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(140F, 0F), Direction.North, false, false, 64, 10, true);
            Door door4to5 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(width - 10F, 360F), Direction.East, false, true, 10, 60, true);

            Door door5to4 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(0F, 230F), Direction.West, false, false, 10, 60, true);
            Door door5to6 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(width - 10F, 230F), Direction.East, false, true, 10, 64, false);

            Door door6to5 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(0F, 10F), Direction.West, false, false, 10, 64, true);
            Door door6to7 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(790F, 400F), Direction.West, false, true, 10, 64, false);

            Door door7to6 = new Door(new Texture2D[] { door, doorClosed }, null, new Vector2(0F, 230F), Direction.West, false, false, 10, 64, true);

            //PITS, LASERS, and BARRIERS
            Texture2D lavaPit = Content.Load<Texture2D>("sprites/objects/Lava");
            Texture2D HealthLaserV = Content.Load<Texture2D>("sprites/objects/HPLaser");
            Texture2D HealthLaserH = Content.Load<Texture2D>("sprites/objects/HPLaserHorizontal");
            Texture2D ManaLaserV = Content.Load<Texture2D>("sprites/objects/ManaLaser");
            Texture2D ManaLaserH = Content.Load<Texture2D>("sprites/objects/ManaLaserHorizontal");
            Texture2D limitationField = Content.Load<Texture2D>("sprites/objects/PlayerLimitationField");
            Texture2D[] barrier1_vertical = { Content.Load<Texture2D>("sprites/objects/BarrierOpenVertical"), Content.Load<Texture2D>("sprites/objects/BarrierClosedVertical") };
            Texture2D[] barrier1_horizontal = { Content.Load<Texture2D>("sprites/objects/BarrierOpenHorizontal"), Content.Load<Texture2D>("sprites/objects/BarrierClosedHorizontal") };
            Texture2D[] barrier2_vertical = { Content.Load<Texture2D>("sprites/objects/Barrier3OV"), Content.Load<Texture2D>("sprites/objects/Barrier3CV") };
            Texture2D[] barrier2_horizontal = { Content.Load<Texture2D>("sprites/objects/Barrier3OH"), Content.Load<Texture2D>("sprites/objects/Barrier3CH") };
            SoundEffect laserEffect = Content.Load<SoundEffect>("audio/sound effects/laserSound");
            SoundEffect barrierEffect = Content.Load<SoundEffect>("audio/sound effects/barrierSound");
            HPLaser las2_1 = new HPLaser(HealthLaserV, new Vector2(630F, 12F), laserEffect.CreateInstance(), 140, 10);
            Barrier bar2_1 = new Barrier(barrier2_vertical, new Vector2(145F, 18F), barrierEffect.CreateInstance());

            HPLaser las3_1 = new HPLaser(HealthLaserH, new Vector2(70F, 90F), laserEffect.CreateInstance(), 10, 140);
            Barrier bar3_1 = new Barrier(barrier2_vertical, new Vector2(300F, 190F), barrierEffect.CreateInstance());
            Barrier bar3_2 = new Barrier(barrier2_horizontal, new Vector2(660F, 170F), barrierEffect.CreateInstance());

            LavaPit pit4_1 = new LavaPit(lavaPit, new Vector2(0F, 120F), lavaSound.CreateInstance(), 100, 200);
            LavaPit pit4_2 = new LavaPit(lavaPit, new Vector2(260F, 120F), lavaSound.CreateInstance(), 540, 200);
            HPLaser las4_1 = new HPLaser(HealthLaserH, new Vector2(100, 320F), laserEffect.CreateInstance(), 10, 160, false);
            HPLaser las4_2 = new HPLaser(HealthLaserV, new Vector2(640F, 320F), laserEffect.CreateInstance(), 160, 10);
            Barrier bar4_1 = new Barrier(barrier2_vertical, new Vector2(690F, 0F), barrierEffect.CreateInstance());
            Barrier bar4_2 = new Barrier(barrier2_vertical, new Vector2(110F, 345F), barrierEffect.CreateInstance());

            PlayerLimitationField lim5_1 = new PlayerLimitationField(limitationField, new Vector2(120F, 200F), lavaSound.CreateInstance(), 560, 120);
            ManaLaser mlas5_1 = new ManaLaser(ManaLaserV, new Vector2(100F, 200F), laserEffect.CreateInstance(), 120, 10);
            HPLaser las5_1 = new HPLaser(HealthLaserV, new Vector2(690F, 200F), laserEffect.CreateInstance(), 120, 10);
            Barrier bar5_1 = new Barrier(barrier2_horizontal, new Vector2(255F, 170F), barrierEffect.CreateInstance(), true); //upper left
            Barrier bar5_2 = new Barrier(barrier2_horizontal, new Vector2(255F, 310F), barrierEffect.CreateInstance()); //lower left
            Barrier bar5_3 = new Barrier(barrier2_horizontal, new Vector2(415F, 170F), barrierEffect.CreateInstance(), true); //upper right 
            Barrier bar5_4 = new Barrier(barrier2_horizontal, new Vector2(415F, 310F), barrierEffect.CreateInstance()); //lower right

            HPLaser las6_1 = new HPLaser(HealthLaserH, new Vector2(0F, 160F), laserEffect.CreateInstance(), 10, 800); //north h
            HPLaser las6_2 = new HPLaser(HealthLaserH, new Vector2(160F, 320F), laserEffect.CreateInstance(), 10, 640, false); //south  h
            HPLaser las6_3 = new HPLaser(HealthLaserV, new Vector2(160F, 0F), laserEffect.CreateInstance(), 480, 10); //east v
            HPLaser las6_4 = new HPLaser(HealthLaserV, new Vector2(640F, 160F), laserEffect.CreateInstance(), 320, 10, false); //west v
            Barrier bar6_1 = new Barrier(barrier2_horizontal, new Vector2(15F, 320F), barrierEffect.CreateInstance());
            Barrier bar6_2 = new Barrier(barrier2_vertical, new Vector2(640F, 15F), barrierEffect.CreateInstance());
            
            //BUTTONS
            Texture2D buttonOn = Content.Load<Texture2D>("sprites/objects/PressButtonOn");
            Texture2D buttonOff = Content.Load<Texture2D>("sprites/objects/PressButtonOff");
            Texture2D buttonNull = Content.Load<Texture2D>("sprites/objects/PressButtonDeactivated");
            SoundEffect pressEffect = Content.Load<SoundEffect>("audio/sound effects/buttonSound");
            Texture2D[] button = { buttonOn, buttonOff, buttonNull };
            BarrierButton barbutt2_1 = new BarrierButton(button, new Vector2(700F, 50F), pressEffect.CreateInstance(), false, false, bar2_1);

            BarrierButton barbutt3_1 = new BarrierButton(button, new Vector2(540F, 220F), pressEffect.CreateInstance(), false, false, bar3_1);
            BarrierButton barbutt3_2 = new BarrierButton(button, new Vector2(360F, 220F), pressEffect.CreateInstance(), false, false, bar3_2);
            LaserButton lasbutt3_1 = new LaserButton(button, new Vector2(700F, 40F), pressEffect.CreateInstance(), false, false, las3_1);

            BarrierButton barbutt4_1 = new BarrierButton(button, new Vector2(140F, 140F), pressEffect.CreateInstance(), false, false, bar4_1);
            BarrierButton barbutt4_2 = new BarrierButton(button, new Vector2(710F, 20F), pressEffect.CreateInstance(), false, false, bar4_2);
            LaserButton lasbutt4_1 = new LaserButton(button, new Vector2(710F, 20F), pressEffect.CreateInstance(), false, false, las4_1);
            LaserButton lasbutt4_2 = new LaserButton(button, new Vector2(460F, 160F), pressEffect.CreateInstance(), true, false, las4_2); //deactivated
            ActivateButton actbutt4_1 = new ActivateButton(button, new Vector2(710F, 20F), pressEffect.CreateInstance(), false, false, lasbutt4_2);

            LaserButton lasbutt5_1 = new LaserButton(button, new Vector2(20F, 380F), pressEffect.CreateInstance(), false, false, mlas5_1);
            LaserButton lasbutt5_2 = new LaserButton(button, new Vector2(280F, 360F), pressEffect.CreateInstance(), false, false, las5_1);
            BarrierButton barbutt5_1 = new BarrierButton(button, new Vector2(20F, 40F), pressEffect.CreateInstance(), false, false, bar5_1);
            BarrierButton barbutt5_2 = new BarrierButton(button, new Vector2(20F, 40F), pressEffect.CreateInstance(), false, false, bar5_2);
            BarrierButton barbutt5_3 = new BarrierButton(button, new Vector2(20F, 40F), pressEffect.CreateInstance(), false, false, bar5_3);
            BarrierButton barbutt5_4 = new BarrierButton(button, new Vector2(700F, 40F), pressEffect.CreateInstance(), false, false, bar5_4);

            LaserButton lasbutt6_1 = new LaserButton(button, new Vector2(460F, 40F), pressEffect.CreateInstance(), false, false, las6_1);
            LaserButton lasbutt6_2 = new LaserButton(button, new Vector2(460F, 40F), pressEffect.CreateInstance(), false, false, las6_2);
            LaserButton lasbutt6_3 = new LaserButton(button, new Vector2(280F, 200F), pressEffect.CreateInstance(), false, false, las6_3);
            LaserButton lasbutt6_4 = new LaserButton(button, new Vector2(280F, 200F), pressEffect.CreateInstance(), false, false, las6_4);
            BarrierButton barbutt6_1 = new BarrierButton(button, new Vector2(680F, 200F), pressEffect.CreateInstance(), false, false, bar6_1);
            BarrierButton barbutt6_2 = new BarrierButton(button, new Vector2(200F, 360F), pressEffect.CreateInstance(), true, false, bar6_2); //deactivated
            ActivateButton actbutt6_1 = new ActivateButton(button, new Vector2(40F, 200F), pressEffect.CreateInstance(), false, false, barbutt6_2);

            //NPCS
            Texture2D male1 = Content.Load<Texture2D>("sprites/entities/npcs/Standing1");
            Texture2D male2 = Content.Load<Texture2D>("sprites/entities/npcs/Standing2");
            Texture2D hitsplat = Content.Load<Texture2D>("ui/Hitsplat");
            Texture2D lineofsight = Content.Load<Texture2D>("ui/LOS");
            SoundEffect deathEffect = Content.Load<SoundEffect>("audio/sound effects/deathSound");
            //level 1 npcs
            Npc npc1_1 = new Npc(this, male1, lineofsight, new Vector2(430F, height - 135F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie", new string[0], new int[0]), 150, 0x5);
            Npc npc1_2 = new Npc(this, male2, lineofsight, new Vector2(80F, 205F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie2", new string[0], new int[0]), 150, 0x5);
            Npc npc1_3 = new Npc(this, male2, lineofsight, new Vector2(666F, 205F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie3", new string[0], new int[0]), 150, 0x5);
            //level 2 npcs
            Npc npc2_1 = new Npc(this, male1, lineofsight, new Vector2(550F, 50F), deathEffect.CreateInstance(), Direction.West, new NpcDefinition("Normie4", new string[0], new int[0]), 150, 0x5);
            Npc npc2_2 = new Npc(this, male1, lineofsight, new Vector2(660F, 200F), deathEffect.CreateInstance(), Direction.West, new NpcDefinition("Normie5", new string[0], new int[0]), 150, 0x5);
            Npc npc2_3 = new Npc(this, male1, lineofsight, new Vector2(100F, 400F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie6", new string[0], new int[0]), 150, 0x5);
            //level 3 npcs
            Npc npc3_1 = new Npc(this, male1, lineofsight, new Vector2(420F, 100F), deathEffect.CreateInstance(), Direction.West, new NpcDefinition("Normie7", new string[0], new int[0]), 150, 0x5);
            Npc npc3_2 = new Npc(this, male1, lineofsight, new Vector2(540F, 380F), deathEffect.CreateInstance(), Direction.North, new NpcDefinition("Normie8", new string[0], new int[0]), 150, 0x5);
            //level 5 npcs
            Npc npc5_1 = new Npc(this, male1, null, new Vector2(256F, 95F), deathEffect.CreateInstance(), Direction.South, new NpcDefinition("Normie9", new string[0], new int[0]), 150, 0x5);
            Npc npc5_2 = new Npc(this, male1, null, new Vector2(324F, 95F), deathEffect.CreateInstance(), Direction.South, new NpcDefinition("Normie10", new string[0], new int[0]), 150, 0x5);
            Npc npc5_3 = new Npc(this, male1, null, new Vector2(416F, 95F), deathEffect.CreateInstance(), Direction.South, new NpcDefinition("Normie11", new string[0], new int[0]), 150, 0x5);
            Npc npc5_4 = new Npc(this, male1, null, new Vector2(484F, 95F), deathEffect.CreateInstance(), Direction.South, new NpcDefinition("Normie12", new string[0], new int[0]), 150, 0x5);
            //level 6 npcs
            Npc npc6_1 = new Npc(this, male1, lineofsight, new Vector2(370F, 40F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie13", new string[0], new int[0]), new int[0], 100, 6, 150, 0x5, false);
            Npc npc6_2 = new Npc(this, male1, lineofsight, new Vector2(370F, 200F), deathEffect.CreateInstance(), Direction.East, new NpcDefinition("Normie14", new string[0], new int[0]), new int[0], 100, 6, 150, 0x5, false);

            SoundEffect hitEffect = Content.Load<SoundEffect>("audio/sound effects/hitSound");
            midX = (graphics.PreferredBackBufferWidth - playur.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playur.Height) / 2;
            player = new Player(playur, new Vector2(125F, 295F), deathEffect.CreateInstance(), Direction.South, 100, 50, 0, 3);
            player.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(player.getLocation().X + (hitsplat.Width / 2), player.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
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
            npc1_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_1.getLocation().X + (hitsplat.Width / 2), npc1_1.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc1_2.loadNPCTextures(Content);
            npc1_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc1_2.getLocation().X, npc1_2.getLocation().Y - 5.0F), null, 64, 15));
            npc1_2.getDisplayBar().setColor(Color.Red);
            npc1_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_2.getLocation().X + (hitsplat.Width / 2), npc1_2.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc1_3.loadNPCTextures(Content);
            npc1_3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc1_3.getLocation().X, npc1_3.getLocation().Y - 5.0F), null, 64, 15));
            npc1_3.getDisplayBar().setColor(Color.Red);
            npc1_3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc1_3.getLocation().X + (hitsplat.Width / 2), npc1_3.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc2_1.loadNPCTextures(Content);
            npc2_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_1.getLocation().X, npc2_1.getLocation().Y - 5.0F), null, 64, 15));
            npc2_1.getDisplayBar().setColor(Color.Red);
            npc2_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_1.getLocation().X + (hitsplat.Width / 2), npc2_1.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc2_2.loadNPCTextures(Content);
            npc2_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_2.getLocation().X, npc2_2.getLocation().Y - 5.0F), null, 64, 15));
            npc2_2.getDisplayBar().setColor(Color.Red);
            npc2_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_2.getLocation().X + (hitsplat.Width / 2), npc2_2.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc2_3.loadNPCTextures(Content);
            npc2_3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc2_3.getLocation().X, npc2_3.getLocation().Y - 5.0F), null, 64, 15));
            npc2_3.getDisplayBar().setColor(Color.Red);
            npc2_3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc2_3.getLocation().X + (hitsplat.Width / 2), npc2_3.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc3_1.loadNPCTextures(Content);
            npc3_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc3_1.getLocation().X, npc3_1.getLocation().Y - 5.0F), null, 64, 15));
            npc3_1.getDisplayBar().setColor(Color.Red);
            npc3_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc3_1.getLocation().X + (hitsplat.Width / 2), npc3_1.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc3_2.loadNPCTextures(Content);
            npc3_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc3_2.getLocation().X, npc3_2.getLocation().Y - 5.0F), null, 64, 15));
            npc3_2.getDisplayBar().setColor(Color.Red);
            npc3_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc3_2.getLocation().X + (hitsplat.Width / 2), npc3_2.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

            npc5_1.loadNPCTextures(Content);
            npc5_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc5_1.getLocation().X, npc5_1.getLocation().Y - 5.0F), null, 64, 15));
            npc5_1.getDisplayBar().setColor(Color.Red);
            npc5_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc5_1.getLocation().X + (hitsplat.Width / 2), npc5_1.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

            npc5_2.loadNPCTextures(Content);
            npc5_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc5_2.getLocation().X, npc5_2.getLocation().Y - 5.0F), null, 64, 15));
            npc5_2.getDisplayBar().setColor(Color.Red);
            npc5_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc5_2.getLocation().X + (hitsplat.Width / 2), npc5_2.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

            npc5_3.loadNPCTextures(Content);
            npc5_3.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc5_3.getLocation().X, npc5_3.getLocation().Y - 5.0F), null, 64, 15));
            npc5_3.getDisplayBar().setColor(Color.Red);
            npc5_3.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc5_3.getLocation().X + (hitsplat.Width / 2), npc5_3.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

            npc5_4.loadNPCTextures(Content);
            npc5_4.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc5_4.getLocation().X, npc5_4.getLocation().Y - 5.0F), null, 64, 15));
            npc5_4.getDisplayBar().setColor(Color.Red);
            npc5_4.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc5_4.getLocation().X + (hitsplat.Width / 2), npc5_4.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

            npc6_1.loadNPCTextures(Content);
            npc6_1.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc6_1.getLocation().X, npc6_1.getLocation().Y - 5.0F), null, 64, 15));
            npc6_1.getDisplayBar().setColor(Color.Red);
            npc6_1.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc6_1.getLocation().X + (hitsplat.Width / 2), npc6_1.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));
            npc6_2.loadNPCTextures(Content);
            npc6_2.setDisplayBar(new DisplayBar(green, font2, new Vector2(npc6_2.getLocation().X, npc6_2.getLocation().Y - 5.0F), null, 64, 15));
            npc6_2.getDisplayBar().setColor(Color.Red);
            npc6_2.setHitsplat(new Hitsplat(font2, hitsplat, new Vector2(npc6_2.getLocation().X + (hitsplat.Width / 2), npc6_2.getLocation().Y + (hitsplat.Height / 2)), hitEffect.CreateInstance()));

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
            level1.setPlayerReentryPoint(new Vector2(368F, 455F - 64F));
            level1.setScreens(screens);
            level1.setSong(factorySong);

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
            level2.setPlayerOrigin(new Vector2(368F, 15F));
            level2.setPlayerReentryPoint(new Vector2(785F - 64F, height - 200F));
            level2.setScreens(screens);
            level2.setSong(factorySong);

            //LEVEL 3
            List<GameObject> Level3Objects = new List<GameObject>();
            Level3Objects.Add(door3to2);
            Level3Objects.Add(door3to4);
            Level3Objects.Add(box3_1);
            Level3Objects.Add(las3_1);
            Level3Objects.Add(bar3_1);
            Level3Objects.Add(bar3_2);
            Level3Objects.Add(barbutt3_1);
            Level3Objects.Add(barbutt3_2);
            Level3Objects.Add(lasbutt3_1);
            Level3Objects.Add(key3_1);
            Texture2D l3 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level3 = new Level(this, player, l3, new Npc[] { npc3_1, npc3_2 }, Level3Objects.ToArray(), 0);
            level3.addCubicle(cube3_1);
            level3.addCubicle(cube3_2);
            level3.addCubicle(cube3_3);
            level3.setPlayerOrigin(new Vector2(15F, height - 200));
            level3.setPlayerReentryPoint(new Vector2(120F, 455F - 64F));
            level3.setScreens(screens);
            level3.setSong(streetSong);

            //LEVEL 4
            List<GameObject> Level4Objects = new List<GameObject>();
            Level4Objects.Add(door4to3);
            Level4Objects.Add(door4to5);
            Level4Objects.Add(token4_1);
            Level4Objects.Add(token4_2);
            Level4Objects.Add(token4_3);
            Level4Objects.Add(box4_1);
            Level4Objects.Add(box4_2);
            Level4Objects.Add(pit4_1);
            Level4Objects.Add(pit4_2);
            Level4Objects.Add(bar4_1);
            Level4Objects.Add(bar4_2);
            Level4Objects.Add(las4_1);
            Level4Objects.Add(las4_2);
            Level4Objects.Add(barbutt4_1);
            Level4Objects.Add(barbutt4_2);
            Level4Objects.Add(lasbutt4_1);
            Level4Objects.Add(lasbutt4_2);
            Level4Objects.Add(actbutt4_1);
            Texture2D l4 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level4 = new Level(this, player, l4, new Npc[] { }, Level4Objects.ToArray(), 0);
            level4.addCubicle(cube4_1);
            level4.addCubicle(cube4_2);
            level4.setPlayerOrigin(new Vector2(140F, 15F));
            level4.setPlayerReentryPoint(new Vector2(785F - 64F, 360F));
            level4.setScreens(screens);
            level4.setSong(streetSong);

            //LEVEL 5
            List<GameObject> Level5Objects = new List<GameObject>();
            Level5Objects.Add(door5to4);
            Level5Objects.Add(door5to6);
            Level5Objects.Add(box5_1);
            Level5Objects.Add(key5_1);
            Level5Objects.Add(token5_1);
            Level4Objects.Add(token5_2);
            Level4Objects.Add(token5_3);
            Level5Objects.Add(mlas5_1);
            Level5Objects.Add(las5_1);
            Level5Objects.Add(lim5_1);
            Level5Objects.Add(bar5_1);
            Level5Objects.Add(bar5_2);
            Level5Objects.Add(bar5_3);
            Level5Objects.Add(bar5_4);
            Level5Objects.Add(lasbutt5_1);
            Level5Objects.Add(lasbutt5_2);
            Level5Objects.Add(barbutt5_1); // noise
            Level5Objects.Add(barbutt5_2);
            Level5Objects.Add(barbutt5_3); // noise
            Level5Objects.Add(barbutt5_4);
            Texture2D l5 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level5 = new Level(this, player, l5, new Npc[] { npc5_1, npc5_2, npc5_3, npc5_4 }, Level5Objects.ToArray(), 0);
            level5.addCubicle(cube5_1);
            level5.addCubicle(cube5_2);
            level5.addCubicle(cube5_3);
            level5.addCubicle(cube5_4);
            level5.addCubicle(cube5_5);
            level5.addCubicle(cube5_6);
            level5.setPlayerOrigin(new Vector2(15F, 230F));
            level5.setPlayerReentryPoint(new Vector2(785F - 64F, 230F));
            level5.setScreens(screens);
            level5.setSong(officeSong);

            //LEVEL 6
            List<GameObject> Level6Objects = new List<GameObject>();
            Level6Objects.Add(door6to5);
            Level6Objects.Add(door6to7);
            Level6Objects.Add(las6_1);
            Level6Objects.Add(las6_2);
            Level6Objects.Add(las6_3);
            Level6Objects.Add(las6_4);
            Level6Objects.Add(bar6_1);
            Level6Objects.Add(bar6_2);
            Level6Objects.Add(lasbutt6_1);
            Level6Objects.Add(lasbutt6_2);
            Level6Objects.Add(lasbutt6_3);
            Level6Objects.Add(lasbutt6_4);
            Level6Objects.Add(barbutt6_1);
            Level6Objects.Add(barbutt6_2);
            Level6Objects.Add(actbutt6_1);
            Level6Objects.Add(box6_1);
            Level6Objects.Add(key6_1);

            Texture2D l6 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level6 = new Level(this, player, l6, new Npc[] { npc6_1, npc6_2 }, Level6Objects.ToArray(), 0);
            level6.addCubicle(cube6_1);
            level6.addCubicle(cube6_2);
            level6.setPlayerOrigin(new Vector2(20F, 10F));
            level6.setPlayerReentryPoint(new Vector2(785F - 64F, 400F));
            level6.setScreens(screens);
            level6.setSong(officeSong);

            //LEVEL 7
            List<GameObject> Level7Objects = new List<GameObject>();
            Level7Objects.Add(door7to6);

            Texture2D l7 = Content.Load<Texture2D>("sprites/levels/Level1");
            Level level7 = new Level(this, player, l7, new Npc[] { }, Level7Objects.ToArray(), 0);
            level7.setPlayerOrigin(new Vector2(15F, 220F));
            level7.setPlayerReentryPoint(new Vector2(785F - 64F, 220F));
            level7.setScreens(screens);

            levels = new List<Level>();
            //levels.Add(level1);
            //levels.Add(level2);
            //levels.Add(level3);
            //levels.Add(level4);
            //levels.Add(level5);
            levels.Add(level6);
            levels.Add(level7);
            level = levels[0];
            levelIndex = 0;

            SoundEffect readEffect = Content.Load<SoundEffect>("audio/sound effects/mindreadSound");
            MindRead read = new MindRead(2, 1, 20, 1000, 200, 100, true, false);
            read.setPlayerManager(playerManager);
            read.setEffect(readEffect);

            inputManager = new InputManager(this, player, level, target, playerManager, screens, read);
            keyBox.update(inputManager);
            level.setInputManager(inputManager);
            inputManager.setDeathManager(new DeathManager(inputManager));

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            Projectile n1_1 = new Projectile(npc1_1, fireOrb, 5, 250, boltSound);
            n1_1.setDamage(33);
            Projectile n1_2 = new Projectile(npc1_2, lightningOrb, 10, 500, boltSound);
            n1_2.setDamage(75);
            Projectile n1_3 = new Projectile(npc1_3, iceOrb, 7, 333, boltSound);
            n1_3.setDamage(20);
            Projectile n2_1 = new Projectile(npc2_1, iceOrb, 5, 250, boltSound);
            n2_1.setDamage(33);
            Projectile n2_2 = new Projectile(npc2_2, iceOrb, 10, 500, boltSound);
            n2_2.setDamage(33);
            Projectile n2_3 = new Projectile(npc2_3, iceOrb, 7, 333, boltSound);
            n2_3.setDamage(20);
            Projectile n3_1 = new Projectile(npc3_1, lightningOrb, 5, 250, boltSound);
            n3_1.setDamage(33);
            Projectile n3_2 = new Projectile(npc3_2, lightningOrb, 10, 500, boltSound);
            n3_2.setDamage(33);
            Projectile n5_1 = new Projectile(npc5_1, fireOrb, 7, 333, boltSound);
            n5_1.setDamage(30);
            Projectile n5_2 = new Projectile(npc5_2, fireOrb, 7, 333, boltSound);
            n5_2.setDamage(30);
            Projectile n5_3 = new Projectile(npc5_3, fireOrb, 7, 333, boltSound);
            n5_3.setDamage(30);
            Projectile n5_4 = new Projectile(npc5_4, fireOrb, 7, 333, boltSound);
            n5_4.setDamage(30);
            Projectile n6_1 = new Projectile(npc6_1, lightningOrb, 7, 333, boltSound);
            n6_1.setDamage(25);
            Projectile n6_2 = new Projectile(npc6_2, lightningOrb, 7, 333, boltSound);
            n6_2.setDamage(25);

            npc1_1.setPath(new AIPath(npc1_1, this, new int[] { midX - 105, midY - 180, midX + 120, midY + 165 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South }));
            npc1_1.setProjectile(n1_1);
            npc1_2.setPath(new AIPath(npc1_2, this, new int[] { 80, 175 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc1_2.setProjectile(n1_2);
            npc1_3.setPath(new AIPath(npc1_3, this, new int[] { 570, 665 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc1_3.setProjectile(n1_3);

            npc2_1.setPath(new AIPath(npc2_1, this, new int[] { 200, 550 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc2_1.setProjectile(n2_1);
            npc2_2.setPath(new AIPath(npc2_2, this, new int[] { 180, 660 }, new int[] { 45, 45 }, new Direction[] { Direction.West, Direction.East }));
            npc2_2.setProjectile(n2_2);
            npc2_3.setPath(new AIPath(npc2_3, this, new int[] { 600, 300, 100, 400 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.East, Direction.North, Direction.West, Direction.South }));
            npc2_3.setProjectile(n2_3);

            npc3_1.setPath(new AIPath(npc3_1, this, new int[] { 100, 360, 100, 420 }, new int[] { 60, 60, 60, 60 }, new Direction[] { Direction.West, Direction.South, Direction.North, Direction.East }));
            npc3_1.setProjectile(n3_1);
            npc3_2.setPath(new AIPath(npc3_2, this, new int[] { 80, 380 }, new int[] { 60, 60 }, new Direction[] { Direction.North, Direction.South }));
            npc3_2.setProjectile(n3_2);

            npc5_1.setProjectile(n5_1);
            npc5_2.setProjectile(n5_2);
            npc5_3.setProjectile(n5_3);
            npc5_4.setProjectile(n5_4);

            npc6_1.setPath(new AIPath(npc6_1, this, new int[] { 550, 370 }, new int[] { 20, 20 }, new Direction[] { Direction.East, Direction.West }));
            npc6_1.setProjectile(n6_1);
            npc6_2.setPath(new AIPath(npc6_2, this, new int[] { 190, 370 }, new int[] { 20, 20 }, new Direction[] { Direction.West, Direction.East }));
            npc6_2.setProjectile(n6_2);

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
                    level.setActive(false);
                    s.update(gameTime);
                    busy = true;
                    break;
                }
            }
            mouse = Mouse.GetState();
            if (busy)
            {
                return;
            }
            else
            {
                level.setActive(true);
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
                    //level.setActive(false);
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
            //level.setActive(true);
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
