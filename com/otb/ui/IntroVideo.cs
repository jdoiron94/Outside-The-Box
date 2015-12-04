using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace OutsideTheBox 
{
    class IntroVideo : Screen
    {

        private readonly Video video;
        private VideoPlayer vidplayer;

        public IntroVideo(Video video, VideoPlayer vidplayer, string name, bool active) :
            base(name, active) {
            this.video = video;
            this.vidplayer = vidplayer;
        }

        /// <summary>
        /// Handles drawing of the video
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch)
        {
            Texture2D videoTexture = null;

            if (vidplayer.State != MediaState.Stopped)
            {
                videoTexture = vidplayer.GetTexture();
             }
            
             if (videoTexture != null)
            {
                batch.Draw(videoTexture, new Rectangle(0, 0, 400, 240), Color.White);         
            }
        }

        /// <summary>
        /// Handles updating of the intro video
        /// </summary>
        /// <param name="time">The GameTime to respect</param>
        public override void update(GameTime time)
        {
    }
}
}
