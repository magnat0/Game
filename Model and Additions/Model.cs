using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Model
    {
        public Player Player;
        public List<Entity> Map;
        public int Level;
        public int TotalNumberOfLevels;

        public Model(int level, int totalNumberOfLevels )
        {
            Level = level;
            TotalNumberOfLevels = totalNumberOfLevels;
            var GameParts = CreateMap.CreateGameMap(Level,TotalNumberOfLevels);
            Player = GameParts.Item2;
            Map = GameParts.Item1;
        }

        public void Movegame(Size size)
        {
            if (Player.Win || Player.Dead)
            {
                Level += Player.Win ? 1 : 0;
                Restart();
            }
            Player.PlayerController(Map,size);
            MapController.LevitatingMove(Map, size);
            MapController.MoveOpponent(Map);
        }
        public void Restart()
        {
            var GameParts = CreateMap.CreateGameMap(Level,TotalNumberOfLevels);
            Player = GameParts.Item2;
            Map = GameParts.Item1;
        }

        public void DrawGameParts(Graphics g) => Drawer.DrawGameParts(Player, Map, g);
    }
}
