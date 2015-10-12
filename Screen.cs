using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace KineticCamp {

    public class Screen {

        /*
         * Class which is meant to represent a given screen.
         */

        // TODO: discover why song doesn't load

        private readonly string name;
        private readonly Song song;
        
        private bool active;
        private bool songPlaying;

        public Screen(string name, Song song, bool active) {
            this.name = name;
            this.song = song;
            this.active = active;
            songPlaying = false;
        }

        public Screen(string name, bool active) :
            this(name, null, active) {
        }

        public Screen(string name) :
            this(name, false) {
        }

        /*
         * Returns the screen's name
         */
        public string getName() {
            return name;
        }

        /*
         * Returns the screen's song
         */
        public Song getSong() {
            return song;
        }

        /*
         * Returns true if the screen is currently active; otherwise, false
         */
        public bool isActive() {
            return active;
        }

        /*
         * Returns true if a song is playing; otherwise, false
         */
        public bool isSongPlaying() {
            return songPlaying;
        }

        /*
         * Sets the screen's active status to the given boolean
         */
        public void setActive(bool active) {
            this.active = active;
        }

        /*
         * Sets the songPlaying boolean according to the parameter
         */
        public void setSongPlaying(bool songPlaying) {
            this.songPlaying = songPlaying;
        }

        /*
         * Overridable method which handles updating the screen
         */
        public virtual void update(GameTime time) {
        }

        /*
         * Overridable method which handles drawing the screen
         */
        public virtual void draw(SpriteBatch batch) {
        }
    }
}
