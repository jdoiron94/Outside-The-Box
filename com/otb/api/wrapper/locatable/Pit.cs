using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsideTheBox.com.otb.api.wrapper.locatable
{
    public class Pit : GameObject
    {
        private Rectangle size;
        private SoundEffect effect;

        public Pit(Texture2D texture, Vector2 location, int width, int height, SoundEffect sound) :
            base(texture, location)
        {
            size = new Rectangle((int)getLocation().X, (int)getLocation().Y, width, height);
            setBounds(size);
            setDestinationBounds(size);
        }

        /// <summary>
        /// Returns the pit's sound effect
        /// </summary>
        /// <returns>Returns the pit's sound effect</returns>
        public SoundEffect getEffect()
        {
            return effect;
        }

        /// <summary>
        /// Sets the pit's sound effect
        /// </summary>
        /// <param name="effect">The sound effect to set</param>
        public void setEffect(SoundEffect effect)
        {
            this.effect = effect;
        }

        /// <summary>
        /// Plays the pit's sound effect
        /// </summary>
        public void playEffect()
        {
            if (effect != null)
            {
                effect.Play();
            }
        }

        public virtual void update(InputManager inputManager)
        {

        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(getTexture(), size, Color.White);
        }
    }
}

