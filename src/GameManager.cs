using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace nd_trabalho.src
{
    public class GameManager
    {
        private Rectangle endRectangle;
        public GameManager(Rectangle endRectangle) 
        {
            this.endRectangle = endRectangle;

        }

        public bool hasGameEnded(Rectangle playerHitbox) 
        {
            return endRectangle.Intersects(playerHitbox);
        }
    }
}
