using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles cubicles
    /// </summary>

    public class Cubicle {

        private readonly int width;
        private readonly int height;

        private readonly Game1 game;
        private readonly Direction orientation;
        private readonly Texture2D texture;

        private Wall[] walls;
        private List<GameObject> objects;

        public Cubicle(float x, float y, int width, int height, Game1 game, Direction orientation, Texture2D texture) {
            this.width = width;
            this.height = height;
            this.game = game;
            this.orientation = orientation;
            this.texture = texture;
            this.walls = new Wall[3];
            if (orientation == Direction.North) {
                walls[0] = new Wall(texture, null, new Vector2(x, y), Direction.North, false, false, 10, height);
                walls[1] = new Wall(texture, null, new Vector2(x, y + height), Direction.East, false, false, width + 10, 10);
                walls[2] = new Wall(texture, null, new Vector2(x + width, y), Direction.North, false, false, 10, height);
            } else if (orientation == Direction.South) {
                walls[0] = new Wall(texture, null, new Vector2(x, y + 10), Direction.South, false, false, 10, height);
                walls[1] = new Wall(texture, null, new Vector2(x, y), Direction.East, false, false, width + 10, 10);
                walls[2] = new Wall(texture, null, new Vector2(x + width, y + 10F), Direction.South, false, false, 10, height);
            } else if (orientation == Direction.West) {
                walls[0] = new Wall(texture, null, new Vector2(x, y), Direction.West, false, false, height, 10);
                walls[1] = new Wall(texture, null, new Vector2(x + height, y), Direction.South, false, false, 10, width + 10);
                walls[2] = new Wall(texture, null, new Vector2(x, y + width), Direction.West, false, false, height, 10);
            } else {
                walls[0] = new Wall(texture, null, new Vector2(x + 10, y), Direction.East, false, false, height, 10);
                walls[1] = new Wall(texture, null, new Vector2(x, y), Direction.South, false, false, 10, height + 10);
                walls[2] = new Wall(texture, null, new Vector2(x + 10, y + height), Direction.East, false, false, height, 10);
            }
            this.objects = new List<GameObject>();
        }

        /// <summary>
        /// Returns an array of all of the walls
        /// </summary>
        /// <returns>Returns the walls that construct the cubicle</returns>
        public Wall[] getWalls() {
            return walls;
        }

        /// <summary>
        /// Returns a list of all objects within the cubicle
        /// </summary>
        /// <returns>Returns objects in the cubicle</returns>
        public List<GameObject> getObjects() {
            return objects;
        }

        /// <summary>
        /// Adds an object to the cubicle
        /// </summary>
        /// <param name="go">The object to add</param>
        public void addObject(GameObject go) {
            objects.Add(go);
        }

        /// <summary>
        /// Draws the cubicle
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        /// <param name="debug">True to show debug boundaries; otherwise, false</param>
        public void draw(SpriteBatch batch, bool debug) {
            foreach (Wall w in walls) {
                batch.Draw(w.getTexture(), w.getBounds(), Color.White);
                if (debug) {
                    game.outline(batch, w.getBounds());
                }
            }
        }
    }
}
