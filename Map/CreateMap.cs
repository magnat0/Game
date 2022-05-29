using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class CreateMap
    {
        private static readonly Image Ground = Image.FromFile(@"..\..\..\image\\ground.jpg");
        private static readonly Image Saw = Image.FromFile(@"..\..\..\image\\Off.png");
        private static readonly Image Platform = Image.FromFile(@"..\..\..\image\\platform.jpg");
        private static readonly Image Fruit = Image.FromFile(@"..\..\..\image\\fruit.png");
        private static readonly Image Fireball = Image.FromFile(@"..\..\..\image\\fireball.png");
        private static readonly Image Opp = Image.FromFile(@"..\..\..\image\\xz.jpg");


        public static (List<Entity>, Player) CreateGameMap(int Level, int TotalNumberOfLevels)
        {
            var resultMap = new List<Entity>();
            var resultPlayer = new Player(new Point(-1, -1));
            var map = MapVariants.GetMap(Level, TotalNumberOfLevels);
            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    switch (map[i][j])
                    {
                        case 'G':
                            var g = new Entity(Tags.Ground, Ground, j * 48, i * 48);
                            g.Image.Size = new Size(g.Image.Image.Width - 5, g.Image.Image.Height);
                            resultMap.Add(g);
                            break;
                        case 'P':
                            var p = new Entity(Tags.Obstacle, Saw, j * 48, i * 48);
                            p.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(p);
                            break;
                        case 'X':
                            var x = new Entity(Tags.LevHorObstacle, Fireball, j * 48, i * 48);
                            x.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(x);
                            break;
                        case 'Z':
                            var z = new Entity(Tags.LevHorObstacle2, Fireball, j * 48, i * 48);
                            z.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(z);
                            break;
                        case 'K':
                            var k = new Entity(Tags.LevVertObstacle2, Fireball, j * 48, i * 48);
                            k.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(k);
                            break;
                        case 'L':
                            var l = new Entity(Tags.LevVertObstacle, Fireball, j * 48, i * 48);
                            l.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(l);
                            break;
                        case 'H':
                            var H = new Entity(Tags.LevitatingHorisontal1, Platform, j * 48, i * 48);
                            H.Image.Size = new Size(H.Image.Image.Width + 25, H.Image.Image.Height);
                            resultMap.Add(H);
                            break;
                        case 'V':
                            var v = new Entity(Tags.LevitatingVertical, Platform, j * 48 + 15, i * 48);
                            v.Image.Size = new Size(v.Image.Image.Width + 25, v.Image.Image.Height);
                            resultMap.Add(v);
                            break;
                        case 'h':
                            var h = new Entity(Tags.LevitatingHorisontal2, Platform, j * 48, i * 48);
                            h.Image.Size = new Size(h.Image.Image.Width + 25, h.Image.Image.Height);
                            resultMap.Add(h);
                            break;
                        case 'F':
                            var f = new Entity(Tags.KeySubject, Fruit, j * 48, i * 48);
                            f.Image.SizeMode = PictureBoxSizeMode.AutoSize;
                            resultMap.Add(f);
                            break;
                        case 'I':
                            resultPlayer = new Player(new Point(j * 48, i * 48));
                            resultPlayer.Hero.Size = new Size(resultPlayer.Hero.Image.Width - 7, resultPlayer.Hero.Image.Height); //было 5
                            break;
                        case 'S':
                            var s = new Entity(Tags.Opponent, Opp, j * 48, i * 48 + 15);
                            s.Image.Size = new Size(s.Image.Image.Width - 5, s.Image.Image.Height - 5);
                            resultMap.Add(s);
                            break;
                        case 'B':
                            var b = new Entity(Tags.Opponent2, Opp, j * 48, i * 48 + 15);
                            b.Image.Size = new Size(b.Image.Image.Width - 5, b.Image.Image.Height - 5);
                            resultMap.Add(b);
                            break;
                    }
            return (resultPlayer.Hero.Location.X == -1 && resultPlayer.Hero.Location.Y == -1)
                || resultMap.Count == 0
                || !resultMap.Any(x => x.Tag == Tags.KeySubject)? 
                throw new ArgumentException("Отсутсвует один из ключевый элементов игры!"):(resultMap,resultPlayer);

        }
    }
}
