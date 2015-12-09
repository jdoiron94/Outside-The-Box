using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles playing through 1..* frames with the same audio
    /// </summary>

    public class FrameSet {

        private readonly Texture2D[] frames;
        private readonly SoundEffect sound;
        private readonly SoundEffectInstance effect;

        private readonly int timer;

        private int step;
        private int index;
        private double end;
        private bool active;
        private bool played;
        private bool completed;
        private bool initialized;

        public FrameSet(Texture2D[] frames, SoundEffect sound) {
            this.frames = frames;
            this.sound = sound;
            this.effect = sound == null ? null : sound.CreateInstance();
            this.timer = sound == null ? 60 : (int) (sound.Duration.TotalSeconds * 60) / frames.Length;
        }

        public void reset() {
            step = 0;
            index = 0;
            active = false;
            played = false;
            completed = false;
            initialized = false;
        }

        /// <summary>
        /// Returns whether or not the frame set is active
        /// </summary>
        /// <returns>Returns true if the frame set is active; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Sets the frame set's active status
        /// </summary>
        /// <param name="active">The active status to set</param>
        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Returns whether or not the frame set has been initialized
        /// </summary>
        /// <returns>Returns true if the frame set has been initialized; otherwise, false</returns>
        public bool isInitialized() {
            return initialized;
        }

        /// <summary>
        /// Initializes the frame set
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public void initialize(GameTime time) {
            if (sound == null) {
                end = time.TotalGameTime.TotalMilliseconds + 1000;
            } else {
                end = time.TotalGameTime.TotalMilliseconds + sound.Duration.TotalMilliseconds;
            }
            initialized = true;
        }

        /// <summary>
        /// Determines whether or not the frame set has finished (for ManualVideo#update)
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        /// <returns>Returns true if the frame set has finished; otherwise, false</returns>
        public bool finished(GameTime time) {
            completed = time.TotalGameTime.TotalMilliseconds >= end;
            return completed;
        }

        /// <summary>
        /// Returns whether or not the frame set has completed (for ManualVideo#draw)
        /// </summary>
        /// <returns>Returns true if the frame set has completed; otherwise, false</returns>
        public bool isCompleted() {
            return completed && effect.State != SoundState.Playing;
        }

        /// <summary>
        /// Updates the frame set
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public void update(GameTime time) {
            if (!played && sound != null) {
                sound.Play();
                played = true;
            }
            if (step >= timer) {
                if (index < frames.Length - 1) {
                    index++;
                }
                step = 0;
            } else {
                step++;
            }
        }

        /// <summary>
        /// Draws the active frame
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public void draw(SpriteBatch batch) {
            Vector2 loc = new Vector2(0.0F, (520.0F - frames[index].Height) / 2.0F);
            batch.Draw(frames[index], loc, Color.White);
        }
    }
}
