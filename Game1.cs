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

        Texture2D player;
        Texture2D npc;
        Texture2D map;
        
        Rectangle playerBounds;

        int x;
        int y;
        int centX;
        int centY;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
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
            player = Content.Load<Texture2D>("player");
            npc = Content.Load<Texture2D>("npc");
            map = Content.Load<Texture2D>("map");
            centX = (Window.ClientBounds.Width - player.Width) / 2;
            centY = (Window.ClientBounds.Height - player.Height) / 2;
            playerBounds = new Rectangle(centX, centY, player.Width, player.Height);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbs.IsKeyDown(Keys.Escape)) {
                Exit();
            }
            if (kbs.IsKeyDown(Keys.W)) {
                if (y + 2 < centY) {
                    y += 2;
                }
            } else if (kbs.IsKeyDown(Keys.S)) {
                if (y - 2 > -1 * (map.Height - centY - player.Height)) {
                    y -= 2;
                }
            } else if (kbs.IsKeyDown(Keys.A)) {
                if (x + 2 < centX) {
                    x += 2;
                }
            } else if (kbs.IsKeyDown(Keys.D)) {
                if (x - 2 > -1 * ((map.Width - centX - player.Width))) {
                    x -= 2;
                }
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
            spriteBatch.Draw(map, new Rectangle(x, y, map.Width, map.Height), Color.White);
            spriteBatch.Draw(player, playerBounds, Color.White);
            //spriteBatch.Draw(npc, new Rectangle(700, 700, npc.Width, npc.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
