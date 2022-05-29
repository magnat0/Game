using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MapController
    {
        public static void LevitatingMove(List<Entity> map, Size size)
        {
            foreach (var e in map.Where(x => x.Tag == Tags.LevitatingHorisontal1 || x.Tag == Tags.LevitatingHorisontal2 || x.Tag == Tags.LevitatingVertical))
            {
                if ((e.Count < 50 && e.Tag != Tags.LevitatingVertical) || (e.Count < 42 && e.Tag == Tags.LevitatingVertical))
                    e.ChangeLocation(new Point(e.Location.X + e.Hspeed, e.Location.Y - e.Vspeed));
                else
                {
                    e.Count = 0;
                    e.Hspeed *= -1;
                    e.Vspeed *= -1;
                }
                e.Count++;
            }
            foreach (var e in map.Where(x => x.Tag == Tags.LevHorObstacle || x.Tag == Tags.LevHorObstacle2 || x.Tag == Tags.LevVertObstacle || x.Tag == Tags.LevVertObstacle2))
            {
                if (map.Any(x => x.Image.Bounds.IntersectsWith(e.Image.Bounds) && x != e)
                    || e.Location.Y < 0 || e.Location.Y > size.Height || e.Location.X < 0 || e.Location.X > size.Width)
                    e.ChangeLocation(e.ShowTheInitialLocation());
                else e.ChangeLocation(new Point(e.Location.X - e.Hspeed, e.Location.Y - e.Vspeed));
            }
        }
        public static void MoveOpponent(List<Entity> map)
        {
            foreach (var e in map.Where(x => x.Tag == Tags.Opponent || x.Tag == Tags.Opponent2))
            {
                var newmap = map.Where(x => x.Tag != Tags.LevHorObstacle && x.Tag != Tags.LevHorObstacle2 && x.Tag != Tags.LevVertObstacle && x.Tag != Tags.LevVertObstacle2 && x.Tag != Tags.Opponent && x.Tag != Tags.Opponent2).ToList();
                var offset = MakeVariants(e);
                if (!newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[0]) && x != e)
                    && newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[2]) && x != e))
                {
                    var t = newmap.Find(x => (!x.Image.Bounds.IntersectsWith(offset[0]) && x != e)
                    && (x.Image.Bounds.IntersectsWith(offset[2]) && x != e));
                    e.ChangeLocation(new Point(e.Location.X + e.Hspeed + t.Hspeed, e.Location.Y + t.Vspeed));
                }
                else if ((newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[6]) && x != e) || newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[1]) && x != e))
                    && !newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[2]) && x != e))
                {
                    var t = newmap.Find(x => (x.Image.Bounds.IntersectsWith(offset[6]) && x != e) || (x.Image.Bounds.IntersectsWith(offset[1]) && x != e)
                    && (!x.Image.Bounds.IntersectsWith(offset[2]) && x != e));
                    e.ChangeLocation(new Point(e.Location.X + t.Hspeed, e.Location.Y + e.Vspeed + t.Vspeed));
                }
                else if (newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[5]) && x != e) || newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[3]) && x != e))
                {
                    var t = newmap.Find(x => (x.Image.Bounds.IntersectsWith(offset[5]) && x != e) || (x.Image.Bounds.IntersectsWith(offset[3]) && x != e));
                    e.ChangeLocation(new Point(e.Location.X - e.Hspeed + t.Hspeed, e.Location.Y + t.Vspeed));
                }
                else if (newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[0]) && x != e) || newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[4]) && x != e)
                    || newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[7]) && x != e))
                {
                    var t = newmap.Find(x => (x.Image.Bounds.IntersectsWith(offset[0]) && x != e) || (x.Image.Bounds.IntersectsWith(offset[4]) && x != e)
                    || (x.Image.Bounds.IntersectsWith(offset[7]) && x != e));
                    e.ChangeLocation(new Point(e.Location.X + t.Hspeed, e.Location.Y - e.Vspeed + t.Vspeed));
                }
                else if (newmap.Any(x => x.Image.Bounds.IntersectsWith(offset[8]) && x != e))
                {
                    var t = newmap.Find(x => x.Image.Bounds.IntersectsWith(offset[8]) && x != e);
                    e.ChangeLocation(new Point(e.Location.X + e.Hspeed + t.Hspeed, e.Location.Y + e.Vspeed + t.Vspeed));
                }
                else
                    e.ChangeLocation(new Point(e.Location.X, e.Location.Y + e.Vspeed));
            }
        }
        public static List<Rectangle> MakeVariants(Entity opp)
        {
            var opp1 = opp.Image.Bounds;
            opp1.Location = new Point(opp.Location.X + opp.Hspeed, opp.Location.Y);
            var opp2 = opp.Image.Bounds;
            opp2.Location = new Point(opp.Location.X - opp.Hspeed, opp.Location.Y);
            var opp3 = opp.Image.Bounds;
            opp3.Location = new Point(opp.Location.X, opp.Location.Y + opp.Vspeed*2);
            var opp4 = opp.Image.Bounds;
            opp4.Location = new Point(opp.Location.X, opp.Location.Y - opp.Vspeed*2);
            var opp5 = opp.Image.Bounds;
            opp5.Location = new Point(opp.Location.X + opp.Hspeed, opp.Location.Y - opp.Vspeed*2);
            var opp6 = opp.Image.Bounds;
            opp6.Location = new Point(opp.Location.X - opp.Hspeed, opp.Location.Y - opp.Vspeed*2);
            var opp7 = opp.Image.Bounds;
            opp7.Location = new Point(opp.Location.X - opp.Hspeed, opp.Location.Y + opp.Vspeed*2);
            var opp8 = opp.Image.Bounds;
            opp8.Location = new Point(opp.Location.X + opp.Hspeed, opp.Location.Y + opp.Vspeed*2);
            var opp9 = opp.Image.Bounds;
            opp9.Location = new Point(opp.Location.X +  opp.Hspeed*3, opp.Location.Y + opp.Vspeed*3);
            return new List<Rectangle> { opp1, opp2, opp3, opp4, opp5, opp6, opp7, opp8, opp9 };
        }
    }
}