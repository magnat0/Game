using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameMenuAssets
    {
        public static readonly Image PauseOff =  Image.FromFile(@"..\..\..\image\pause1.png");
        public static readonly Image PauseOn =  Image.FromFile(@"..\..\..\image\pause2.png");
        public static readonly Image CloseForm = Image.FromFile(@"..\..\..\image\closegame.png");
        public static readonly Image Reference =  Image.FromFile(@"..\..\..\image\reference.png");
        public static readonly string BriefTraining =
@" A - движение влево
  D - движение вправо
  Space - прыжок
  Shift - ускорение
  Escape - закрыть игру
  P/Enter - запустить игру/поставить на паузу
  Tab - посмотреть управление";

        public static readonly string StartGame =
@"Приветсвую тебя, игрок! В этой игре ты будешь управлять лягушонком,
и чтобы перейти на следующий уровень, нужно добраться до киви.
Лягушонгок погибает при касании с пилами, огненными шарами и каменными головами.
Посли смерти уровент перезапускается.
Удачной тебе игры!
Управление:
  A - движение влево
  D - движение вправо
  Space - прыжок
  Shift - ускорение
  Escape - закрыть игру
  P/Enter - запустить игру/поставить на паузу
  Tab - посмотреть управление";
    }

}
