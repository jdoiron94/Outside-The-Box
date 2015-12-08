using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OutsideTheBox {

    /// <summary>
    /// Class for manually looping through a video's frames with audio
    /// </summary>

    public class ManualVideo : Screen {

        private readonly FrameSet[] sets;

        public ManualVideo(FrameSet[] sets, string name, bool active) :
            base(name, active) {
            this.sets = sets;
        }

        /// <summary>
        /// Handles the updating of the video
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public override void update(GameTime time) {
            for (int i = 0; i < sets.Length; i++) {
                FrameSet f = sets[i];
                if (f.isActive()) {
                    if (!f.isInitialized()) {
                        f.initialize(time);
                    }
                    if (!f.finished(time)) {
                        f.update(time);
                    } else {
                        f.setActive(false);
                        if (i != sets.Length - 1) {
                            sets[i + 1].setActive(true);
                        } else {
                            setActive(false);
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the drawing of the video's frames
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch) {
            foreach (FrameSet f in sets) {
                if (f.isActive() && !f.isCompleted()) {
                    f.draw(batch);
                    break;
                }
            }
        }
    }
}
