using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public class Player
    {
        public PictureBox Hero;
        public bool Left;
        public bool Right;
        public bool Jump;
        public bool Force;
        public bool Boost;
        public bool Dead;
        public bool Win;

        public int PlayerSpeed = 11;
        public int JumpSpeed = 62;
        private int Gravity = 1;
        private int dx = 0;
        private int dy = 0;
        private int dg = 0;

        public Player(Point loc)
        {
            Hero = new PictureBox();
            Hero.Image = PlayerImage.heroRight;
            Hero.Location = loc;
        }

        public void PlayerController(List<Entity> map, Size size)
        {
            dx = dy = dg = 0;
            if (Boost && PlayerSpeed == 11)
                PlayerSpeed += 5;
            if (Left)
                CanMoveLeft(map);
            if (Right)
                CanMoveRight(map);
            if (Jump && Force)
            {
                CanMoveUp(map);
                Jump = Force = false;
            }
            Physics(map);
            DeathOrWinСheck(map, size);
            PlayerImage.MakePlayerImage(Hero, dx, dy);
            Hero.Location = new Point(Hero.Location.X + dx, Hero.Location.Y + dy + dg);
        }

        private void CanMoveLeft(List<Entity> map)
        {
            var move = PlayerSpeed;
            var hero = Hero.Bounds;
            hero.Location = new Point(Hero.Location.X - move, Hero.Location.Y);
            foreach (var e in map.Where(x => InitialTrafficCheck(x.Tag)))
                if (hero.IntersectsWith(e.Image.Bounds))
                {
                    move = Hero.Location.X - e.Location.X - e.Image.Width;
                    break;
                }
            dx -= move;
        }

        private void CanMoveRight(List<Entity> map)
        {
            var move = PlayerSpeed;
            var hero = Hero.Bounds;
            hero.Location = new Point(Hero.Location.X + move, Hero.Location.Y);
            foreach (var e in map.Where(x => InitialTrafficCheck(x.Tag)))
                if (hero.IntersectsWith(e.Image.Bounds))
                {
                    move = e.Location.X - Hero.Location.X - Hero.Width;
                    break;
                }
            dx += move;
        }

        private void CanMoveUp(List<Entity> map)
        {
            var move = JumpSpeed;
            var hero2 = Hero.Bounds;
            hero2.Location = new Point(Hero.Location.X + dx, Hero.Location.Y - move);
            dx = dx * 4;
            var hero1 = Hero.Bounds;
            hero1.Location = new Point(Hero.Location.X + dx, Hero.Location.Y - move);
            foreach (var e in map.Where(x => InitialTrafficCheck(x.Tag)))
                if (hero1.IntersectsWith(e.Image.Bounds) || hero2.IntersectsWith(e.Image.Bounds))
                {
                    move = Hero.Location.Y - (e.Location.Y + e.Image.Height);
                    if (dx != 0)
                    {
                        if (Right && Math.Abs(e.Location.X - Hero.Location.X - Hero.Width) < dx)
                            dx = Math.Abs(e.Location.X - Hero.Location.X - Hero.Width);
                        if (Left && -Math.Abs(Hero.Location.X - e.Location.X - e.Image.Width) > dx)
                            dx = -Math.Abs(Hero.Location.X - e.Location.X - e.Image.Width);
                    }
                    break; //добавлено
                }
            dy -= move;
        }

        private void Physics(List<Entity> map)
        {
            Gravity += 1;
            var move = Gravity;
            var hero = Hero.Bounds;
            hero.Location = new Point(Hero.Location.X + dx, Hero.Location.Y + dy + move);
            foreach (var e in map.Where(x => InitialTrafficCheck(x.Tag)))
            {
                if (hero.IntersectsWith(e.Image.Bounds))
                {
                    move = e.Location.Y - Hero.Location.Y - Hero.Height;
                    Gravity = 1;
                    dx += e.Count == 50 || e.Count == 0 ? 0 : e.Hspeed;
                    dy -= e.Count == 42 || e.Count == 0 ? 0 : e.Vspeed;
                    break; //добавлено
                }
            }
            dg += move;
            Force = dg == 0;
        }

        private void DeathOrWinСheck(List<Entity> map, Size size)
        {
            Dead = map.Any(
                x => (x.Tag == Tags.Obstacle || x.Tag == Tags.LevVertObstacle ||
                x.Tag == Tags.LevHorObstacle || x.Tag == Tags.LevHorObstacle2 ||
                x.Tag == Tags.LevVertObstacle2 || x.Tag == Tags.Opponent || x.Tag == Tags.Opponent2)
                && Hero.Bounds.IntersectsWith(x.Image.Bounds)) || Hero.Location.X < 0 || Hero.Location.X>size.Width;
            Win = map.Any(x => x.Tag == Tags.KeySubject && Hero.Bounds.IntersectsWith(x.Image.Bounds));
        }

        private bool InitialTrafficCheck(Tags tag) => 
            tag == Tags.Ground || tag == Tags.LevitatingHorisontal1 || tag == Tags.LevitatingVertical || tag == Tags.LevitatingHorisontal2;

    }
}
