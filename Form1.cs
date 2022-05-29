using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        private readonly Model GameModel;
        private readonly Timer Timer;

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show(GameMenuAssets.StartGame);
            BackColor = Color.FromArgb(0, 250, 154);
            GameModel = new Model(1, 5);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1820,880);
            this.KeyDown += new KeyEventHandler(ProcessingKeystrokes);
            this.KeyUp += new KeyEventHandler(ProcessingWhenReleasingKeystrokes);
            this.DoubleBuffered = true;
            Timer = new Timer();
            Timer.Interval = 20;
            Timer.Tick += new EventHandler(MoveGame);
            Controls.Add(MakeMenu(Timer));
        }

        private void MoveGame(object sender, EventArgs e)
        {
            GameModel.Movegame(this.Size);
            Invalidate();
        }

        private void ProcessingWhenReleasingKeystrokes(object sender, KeyEventArgs e)
        {
            if (GameModel.Player.Boost && e.KeyCode == Keys.ShiftKey && GameModel.Player.PlayerSpeed == 16)
            {
                GameModel.Player.Boost = false;
                GameModel.Player.PlayerSpeed -= 5;
            }
            if (e.KeyCode == Keys.A)
                GameModel.Player.Left = false;
            if (e.KeyCode == Keys.D)
                GameModel.Player.Right = false;
            if (e.KeyCode == Keys.Space && GameModel.Player.Jump)
                GameModel.Player.Jump = false;
            if (e.KeyCode == Keys.R)
                GameModel.Player.Dead = true;
            if (e.KeyCode == Keys.T)
                GameModel.Player.Win = true;
        }

        private void ProcessingKeystrokes(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey && !GameModel.Player.Boost)
                GameModel.Player.Boost = true;
            if (e.KeyCode == Keys.A)
                GameModel.Player.Left = true;
            if (e.KeyCode == Keys.D)
                GameModel.Player.Right = true;
            if (e.KeyCode == Keys.Space && !GameModel.Player.Jump)
                GameModel.Player.Jump = true;
            if (e.KeyCode == Keys.Escape)
                Close();
            if(e.KeyCode == Keys.P || e.KeyCode == Keys.Enter)
            {
                if (Timer.Enabled)
                    Timer.Stop();
                else
                    Timer.Start();
                Controls.Clear();
                Controls.Add(MakeMenu(Timer));
            }
            if(e.KeyCode == Keys.Tab)
                MessageBox.Show(GameMenuAssets.BriefTraining);
        }

        private void OnPaint(object sender, PaintEventArgs e) => GameModel.DrawGameParts(e.Graphics);

        public MenuStrip MakeMenu(Timer time)
        {
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem management = new ToolStripMenuItem("Управление");
            management.Image = GameMenuAssets.Reference;
            management.Click += (s, e) => MessageBox.Show(GameMenuAssets.BriefTraining);
            ToolStripMenuItem closeGame = new ToolStripMenuItem("Выйти из игры");
            closeGame.Image = GameMenuAssets.CloseForm;
            closeGame.Click += (s, e) => Close();
            ToolStripMenuItem levels = new ToolStripMenuItem("Уровни");
            for (int i = 1; i <= GameModel.TotalNumberOfLevels; i++)
            {
                ToolStripMenuItem level = new ToolStripMenuItem(i.ToString());
                level.Click += (s, e) =>
                {
                    GameModel.Level = int.Parse(level.Text);
                    GameModel.Restart();
                };
                levels.DropDownItems.Add(level);
            }
            ToolStripMenuItem pause = new ToolStripMenuItem(Timer.Enabled ? "Приостановить игру": "Продолжить игру");
            pause.Image = Timer.Enabled ? GameMenuAssets.PauseOn : GameMenuAssets.PauseOff;
            pause.Click += (s, e) =>
            {
                if (time.Enabled)
                    time.Stop();
                else
                    time.Start();
                pause.Image = Timer.Enabled ? GameMenuAssets.PauseOn: GameMenuAssets.PauseOff;
                pause.Text = Timer.Enabled ? "Приостановить игру" : "Продолжить игру";
            };
            menu.Items.Add(pause);
            menu.Items.Add(levels);
            menu.Items.Add(management);
            menu.Items.Add(closeGame);
            return menu;
        }
    }
}
