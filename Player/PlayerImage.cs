using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class PlayerImage
    {
        public static readonly  Image heroRight = Image.FromFile(@"..\..\..\image\frogy2.png");
        public static readonly Image heroLeft = Image.FromFile(@"..\..\..\image\frogy2left.png");
        public static readonly Image heroUpRight = Image.FromFile(@"..\..\..\image\frogy2jumpright.png");
        public static readonly Image heroUpLeft = Image.FromFile(@"..\..\..\image\frog2jumpleft.png");

        public static void MakePlayerImage(PictureBox hero,int dx,int dy)
        {
            if (dx > 0 && dy == 0)
                hero.Image = heroRight;
            if (dx < 0 && dy == 0)
                hero.Image = heroLeft;
            if (dx > 0 && dy != 0)
                hero.Image = heroUpRight;
            if (dx < 0 && dy != 0)
                hero.Image = heroUpLeft;
        }

    }
}
