using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KineticCamp
{
    public class Target
    {
        private Texture2D texture;
        private InputManager inputManager;
        private bool active;
        private Player player;
        private Vector2 location;
        private Vector2 destination;
        private Direction direction;
        private Rectangle bounds;

        public Target(Texture2D texture, Player player, Vector2 location, Direction direction)
        {
            this.texture = texture;
            this.active = false;
            this.player = player;
            this.direction = direction;
            this.location = location;
            bounds = new Rectangle((int)location.X, (int)location.Y, x, y);
            //this.inputManager = inputManager;
        }

        public void setInputManager(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public bool isActive()
        {
            //return true;
            return active;
        }


        public void setActive(bool active)
        {
            this.active = active;
        }

        public Vector2 getLocation()
        {
            return location;
        }

        /// <summary>
        /// Returns the target's destination
        /// </summary>
        /// <returns>Returns the game object's destination</returns>
        public Vector2 getDestination()
        {
            return destination;
        }

        /// <summary>
        /// Sets the target's destination
        /// </summary>
        /// <param name="destination">The destination to be set</param>
        public void setDestination(Vector2 destination)
        {
            this.destination = destination;
        }

        public void setLocation(Vector2 location)
        {
            this.location = location;
        }
        /// <summary>
        /// Returns the target's direction
        /// </summary>
        /// <returns>Returns the target's direction</returns>
        public Direction getDirection()
        {
            return direction;
        }

        /// <summary>
        /// Returns the target's bounds
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds()
        {
            return bounds;
        }

        public void setBounds(int y, int x)
        {
            bounds.Height = y;
            bounds.Width = x;
        }

        /// <summary>
        /// Derives the target's x coordinate in location and bounds by the specified x amount
        /// </summary>
        /// <param name="x">The x amount to be derived by</param>
        public void deriveX(int x)
        {
            location.X += x;
            bounds.X += x;
        }

        /// <summary>
        /// Derives the target's y coordinate in location and bounds by the specified y amount
        /// </summary>
        /// <param name="y">The y amount to be derived by</param>
        public void deriveY(int y)
        {
            location.Y += y;
            bounds.Y += y;
        }

        /// <summary>
        /// Sets the target's direction
        /// </summary>
        /// <param name="direction">The direction to be set</param>
        public void setDirection(Direction direction)
        {
            this.direction = direction;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(player.getLocation().X, player.getLocation().Y), Color.White);

        }

    }
}

