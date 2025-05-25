using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nd_trabalho.src
{
     public abstract class Entity
     {
         public Texture2D spritesheet;
         public Vector2 position;
         public Vector2 velocity;
        public Rectangle hitbox;

        public enum currentAnimation
            {
                Idle,
                Run,
                Jump,
                Falling,
                Attacking,

            }

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
     }
}   
