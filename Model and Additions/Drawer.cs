using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
   public  class Drawer
    {
        public static void DrawGameParts(Player player,List<Entity> map, Graphics g)
        {
            g.DrawImage(player.Hero.Image, player.Hero.Location.X, player.Hero.Location.Y);
            foreach (var e in map)
                g.DrawImage(e.Image.Image, e.Location);
        }
    }
}
