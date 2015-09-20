using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace KineticCamp {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        Entity player;
        Entity npc;
        Level level;

        int midX;
        int midY;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            midX = (Window.ClientBounds.Width - playerTexture.Width) / 2;
            midY = (Window.ClientBounds.Height - playerTexture.Height) / 2;
            player = new Entity(playerTexture, new Projectile(Content.Load<Texture2D>("bullet"), new Vector2(midX + 32, midY), 5, 150), new Vector2(midX, midY), Window.ClientBounds, 50, 5);
            npc = new Entity(Content.Load<Texture2D>("npc"), null, new Vector2(midX + 150, midY + 150), Window.ClientBounds, 50, 5);
            level = new Level(player, Content.Load<Texture2D>("map"), new Entity[] { npc });
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.Escape)) {
                Exit();
            } else if (kbs.IsKeyDown(Keys.W)) {
                if (level.getY() + 2 < midY) {
                    level.deriveY(2);
                }
            } else if (kbs.IsKeyDown(Keys.S)) {
                if (level.getY() - 2 > -1 * (level.getMap().Height - midY - player.getTexture().Height)) {
                    level.deriveY(-2);
                }
            } else if (kbs.IsKeyDown(Keys.A)) {
                if (level.getX() + 2 < midX) {
                    level.deriveX(2);
                }
            } else if (kbs.IsKeyDown(Keys.D)) {
                if (level.getX() - 2 > -1 * ((level.getMap().Width - midX - player.getTexture().Width))) {
                    level.deriveX(-2);
                }
            } else if (kbs.IsKeyDown(Keys.Space)) {
                foreach (Entity e in level.getNpcs()) {
                    if (e != null) {
                        // handle projectile interaction with npcs
                    }
                }
                npc.deriveHealth(-50);
                Console.WriteLine("Health: " + npc.getHealth());
            }
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
           
            spriteBatch.Begin();
            level.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
