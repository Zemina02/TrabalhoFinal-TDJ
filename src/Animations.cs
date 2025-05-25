using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nd_trabalho.src
{
    public class Animations
    {
        Texture2D spritesheet;
        int frames;
        int rows = 0;
        int c = 0;
        float timeSinceLastFrame = 0;
 
        public Animations(Texture2D spritesheet, float width = 32, float heigth = 32)
        {
            this.spritesheet = spritesheet;
            frames = (int)(spritesheet.Height / heigth);
        }
  
        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gametime, SpriteEffects effect, float milisecondsframes = 500)
        {
            if (rows < frames)
            {
                var rect = new Rectangle(c, 32 * rows, 32, 32);

                spriteBatch.Draw(spritesheet, position, rect, Color.White, 0f, new Vector2(), 1f, effect, 1);

                timeSinceLastFrame += (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeSinceLastFrame > milisecondsframes)
                {
                    timeSinceLastFrame -= milisecondsframes;
                    rows++;
                }

                if (rows == frames)
                {
                    rows = 0;
                }
            }
        }
    }

}