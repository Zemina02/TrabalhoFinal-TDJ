using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nd_trabalho.src
{
    public class Enemy : Entity
    {

        Animations enemyAnim;
        float speed = - 1f;
        Rectangle path;
        SpriteEffects effects;
        bool olharParaEsquerda = true;

        public Enemy(Texture2D texture, Rectangle pathWay) 
        {
            enemyAnim = new Animations(texture);
            this.path = pathWay;
            position = new Vector2(pathWay.X,pathWay.Y);
            hitbox = new Rectangle(pathWay.X, pathWay.Y, 16, 16);
        }

        public override void Update()
        {

            if (!path.Contains(hitbox)) 
            {
                speed = -speed;
                olharParaEsquerda = false;

            }
            position.X += speed;

            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;

        }
        public bool Hit(Rectangle player) 
        {
            return hitbox.Intersects(player);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(olharParaEsquerda)
            {
                enemyAnim.Draw(spriteBatch, position, gameTime, SpriteEffects.None, 200);
            }
            else
            {
                enemyAnim.Draw(spriteBatch, position, gameTime, SpriteEffects.FlipHorizontally, 200);
            }
        }
    }
}
