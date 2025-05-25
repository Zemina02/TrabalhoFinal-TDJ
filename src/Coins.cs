using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nd_trabalho.src
{
    public class Coins: Entity
    {
        Animations moedas;        

        public  Coins(Texture2D textura, Vector2 posicao)
        {
            this.moedas = new Animations(textura);
            this.position = posicao;
        }

        public override void Update() 
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            moedas.Draw(spriteBatch, position, gameTime, SpriteEffects.None, 100);

        }

    }
}
