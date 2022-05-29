using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Entity
    {
        public readonly Tags Tag;
        public readonly PictureBox Image;
        public Point Location;
        public int Count;
        public int Hspeed;
        public int Vspeed;
        private Point InitialLocation;

        public Entity(Tags tag, Image sprite, int x, int y)
        {
            this.Tag = tag;
            Hspeed = MakeHspeedAndVspeed(tag).Item1;
            Vspeed = MakeHspeedAndVspeed(tag).Item2;
            Image = new PictureBox();
            Image.Image = sprite;
            Image.Location = new Point(x, y);
            Location = Image.Location;
            InitialLocation = Image.Location;
        }

        public void ChangeLocation(Point point)
        {
            Location = point;
            Image.Location = point;
        }

        public Point ShowTheInitialLocation() => InitialLocation;

        private (int,int) MakeHspeedAndVspeed(Tags tag)
        {
            var Hspeed = 0;
            var Vspeed = 0;
            switch (tag)
            {
                case Tags.LevitatingHorisontal1:
                    Hspeed = 7;
                    break;
                case Tags.LevitatingHorisontal2:
                    Hspeed = -7;
                    break;
                case Tags.LevitatingVertical:
                    Vspeed = 7;
                    break;
                case Tags.LevVertObstacle:
                    Vspeed = 10;
                    break;
                case Tags.LevVertObstacle2:
                    Vspeed = -10;
                    break;
                case Tags.LevHorObstacle:
                    Hspeed = 20;
                    break;
                case Tags.LevHorObstacle2:
                    Hspeed = -20;
                    break;
                case Tags.Opponent:
                    Hspeed = 4;
                    Vspeed = 4;
                    break;
                case Tags.Opponent2:
                    Hspeed = -4;
                    Vspeed = 4;
                    break;
            }
            return (Hspeed, Vspeed);
        }
    }
}
