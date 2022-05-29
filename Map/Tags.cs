using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public enum Tags
    {
        Ground,
        Obstacle,
        Opponent,  // не ставить на левитирующие платформы, со временем слетают
        Opponent2, // не ставить на левитирующие платформы, со временем слетают
        KeySubject,
        LevitatingHorisontal1,
        LevitatingHorisontal2,
        LevitatingVertical,
        LevHorObstacle,
        LevHorObstacle2,
        LevVertObstacle,
        LevVertObstacle2
    }
}
