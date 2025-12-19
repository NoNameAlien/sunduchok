using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace svin
{
    public partial class FormMainMenu : Form
    {
        // поля для анимации
        private Timer _pulseTimer;
        private Button _hoveredButton;
        private Size _originalSize;
        private Point _originalLocation;
        private bool _growing;
        private const int MaxDelta = 10; // на сколько пикселей увеличиваем максимум
        private const int Step = 2;      // шаг изменения за тик
        private SoundPlayer _menuMusic;

        public FormMainMenu()
        {
            InitializeComponent();

            // фиксируем окно
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            // запускаем музыку в меню (цикл)
            _menuMusic = new SoundPlayer(Properties.Resources.snd_music_ogg);
            _menuMusic.PlayLooping();

            // настраиваем таймер пульса
            _pulseTimer = new Timer();
            _pulseTimer.Interval = 30; // 30 мс ~ 33 кадра/сек
            _pulseTimer.Tick += PulseTimer_Tick;

            // подписываем все нужные кнопки на общие события
            SetupPulse(button1);
            SetupPulse(buttonPlay);
            SetupPulse(buttonShop);
            SetupPulse(buttonStat);
            SetupPulse(buttonClose);
            SetupPulse(buttonAboutUs);

            // подписываем клики
            buttonShop.Click += buttonShop_Click;
            buttonAboutUs.Click += buttonAboutUs_Click;
            buttonStat.Click += buttonStat_Click;
        }

        private void buttonShop_Click(object sender, EventArgs e)
        {
            using (var shop = new FormShop())
            {
                shop.ShowDialog(this); // модальное окно поверх меню
            }
        }

        private void buttonAboutUs_Click(object sender, EventArgs e)
        {
            string text =
                "Автор: Шестаков А.А.\n" +
                "Берите карту, Мистер Свин — это однопользовательская карточная игра по мотивам «Сундучка», " +
                "где вы торгуетесь картами с Мистером Свином, собираете четвёрки одинаковых рангов в сундучки " +
                "и стараетесь обыграть хитрого противника.";

            MessageBox.Show(text, "О игре", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonStat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В доработке", "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetupPulse(Button btn)
        {
            btn.MouseEnter += Button_MouseEnter_Pulse;
            btn.MouseLeave += Button_MouseLeave_Pulse;
        }

        private void Button_MouseEnter_Pulse(object sender, EventArgs e)
        {
            _hoveredButton = (Button)sender;
            _originalSize = _hoveredButton.Size;
            _originalLocation = _hoveredButton.Location;
            _growing = true;
            _pulseTimer.Start();
        }

        private void Button_MouseLeave_Pulse(object sender, EventArgs e)
        {
            _pulseTimer.Stop();
            if (_hoveredButton != null)
            {
                _hoveredButton.Size = _originalSize;
                _hoveredButton.Location = _originalLocation;
                _hoveredButton = null;
            }
        }

        private void PulseTimer_Tick(object sender, EventArgs e)
        {
            if (_hoveredButton == null) return;

            int currentDelta = _hoveredButton.Width - _originalSize.Width;

            if (_growing)
            {
                if (currentDelta >= MaxDelta)
                {
                    _growing = false;
                    return;
                }

                _hoveredButton.Width += Step;
                _hoveredButton.Height += Step;
                _hoveredButton.Location = new Point(
                    _hoveredButton.Location.X - Step / 2,
                    _hoveredButton.Location.Y - Step / 2
                );
            }
            else
            {
                if (currentDelta <= 0)
                {
                    _growing = true;
                    return;
                }

                _hoveredButton.Width -= Step;
                _hoveredButton.Height -= Step;
                _hoveredButton.Location = new Point(
                    _hoveredButton.Location.X + Step / 2,
                    _hoveredButton.Location.Y + Step / 2
                );
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // стопаем музыку меню, чтобы не играла поверх игры
            _menuMusic?.Stop();

            var gameForm = new FormGame();

            // когда игра закроется (стрелка или крестик), показать меню обратно
            gameForm.FormClosed += (s, args) =>
            {
                this.Show();
                // возвращаем музыку
                _menuMusic?.PlayLooping();
            };

            gameForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _menuMusic?.Stop();
            Application.Exit();
        }
    }
}
