using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nd_trabalho.src
{
    public class Camara
    {

        public Matrix Transform;
        public Matrix Follow (Rectangle target) 
        {
            target.X = MathHelper.Clamp(target.X,(int) Game1.screenWidth/2 - 185,    (int)Game1.screenWidth/2 + 165);
            target.Y = MathHelper.Clamp(target.Y, (int)Game1.screenHeigth/2, (int)Game1.screenHeigth);

            Vector3 translation = new Vector3(-target.X - target.Width/2,
                                              -target.Y - target.Height/2,
                                                                       0);



            Vector3 offset = new Vector3(Game1.screenWidth/3,
                                         Game1.screenHeigth/2,
                                                           0);

            Transform = Matrix.CreateTranslation(translation)* Matrix.CreateTranslation(offset);
            return Transform;
        }
    }
}
