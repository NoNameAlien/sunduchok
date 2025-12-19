using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace svin
{
    public partial class FormGame : Form
    {
        private Timer _pulseTimer;
        private Button _hoveredButton;
        private Size _originalSize;
        private Point _originalLocation;
        private bool _growing;
        private const int MaxDelta = 10;
        private const int Step = 2;

        private Image[] _souvenirFrames;
        private Image[] _cardSprites;
        private Random _rnd = new Random();

        private Image[] _deckFrames;
        private Image[] _pitFrames;

        // визуальные слои колоды (базовая + клоны)
        private List<PictureBox> _deckVisualCards = new List<PictureBox>();

        private Timer _handTimer;
        private Point _handTarget;
        private int _handSpeed = 10; // скорость падения руки (пикселей за тик)

        private Timer _headTimer;
        private Image[] _headFrames;
        private int _headFrameIndex = 0;

        private Timer _eyesTimer;
        private Image[] _eyesFrames;
        private int _eyesFrameIndex = 0;
        private bool _eyesBlinkPhase = false;

        private Timer _pigHandTimer;
        private Image[] _pigHandFrames;
        private int _pigHandFrameIndex = 0;
        private Point _pigHandTarget;
        private HandPhase _pigHandPhase = HandPhase.None; // можно переиспользовать тот же enum
        private int _pigHandWaitTicks = 0;
        private TaskCompletionSource<bool> _pigHandAnimationTcs;

        private TaskCompletionSource<bool> _playerHandAnimationTcs;

        // ----- ЛОГИКА КАРТ -----
        private Deck _deck;
        private List<Card> _playerHand;
        private List<PictureBox> _playerCardBoxes = new List<PictureBox>();

        // рука свина
        private List<Card> _pigHand;

        // сундучки игрока (визуал и счётчик)
        private List<PictureBox> _playerChestBoxes = new List<PictureBox>();
        private int _playerChestCount = 0;

        // сундучки свина (визуал и счётчик)
        private List<PictureBox> _pigChestBoxes = new List<PictureBox>();
        private int _pigChestCount = 0;

        // флаг конца игры
        private bool _gameOver = false;

        // запущена ли уже игровая часть (чтобы StartGame не вызывался дважды)
        private bool _gameStarted = false;

        // был ли нажат Skip (чтобы основное интро остановилось)
        private bool _introSkipped = false;
        private SoundPlayer _introPlayer;


        // чей сейчас ход
        private enum TurnOwner
        {
            Player,
            Pig
        }

        private TurnOwner _currentTurn;

        // ждём ли сейчас, что игрок возьмёт карту из колоды (стрелка горит)
        private bool _waitingPlayerDraw = false;
        // идёт ли сейчас обработка хода игрока (чтобы не спамили клики)
        private bool _playerActionInProgress = false;

        public FormGame()
        {
            InitializeComponent();

            _introPlayer = new SoundPlayer();

            // показываем кнопку Skip во время интро
            buttonSkip.Visible = true;

            _souvenirFrames = new Image[]
            {
                Properties.Resources.s_suvenirs_0,
                Properties.Resources.s_suvenirs_1,
                Properties.Resources.s_suvenirs_2,
                Properties.Resources.s_suvenirs_3,
                Properties.Resources.s_suvenirs_4,
            };

            // Все 52 карты (s_all_card_0 .. s_all_card_51)
            _cardSprites = new Image[]
            {
                Properties.Resources.s_all_card_0,
                Properties.Resources.s_all_card_1,
                Properties.Resources.s_all_card_2,
                Properties.Resources.s_all_card_3,
                Properties.Resources.s_all_card_4,
                Properties.Resources.s_all_card_5,
                Properties.Resources.s_all_card_6,
                Properties.Resources.s_all_card_7,
                Properties.Resources.s_all_card_8,
                Properties.Resources.s_all_card_9,
                Properties.Resources.s_all_card_10,
                Properties.Resources.s_all_card_11,
                Properties.Resources.s_all_card_12,

                Properties.Resources.s_all_card_13,
                Properties.Resources.s_all_card_14,
                Properties.Resources.s_all_card_15,
                Properties.Resources.s_all_card_16,
                Properties.Resources.s_all_card_17,
                Properties.Resources.s_all_card_18,
                Properties.Resources.s_all_card_19,
                Properties.Resources.s_all_card_20,
                Properties.Resources.s_all_card_21,
                Properties.Resources.s_all_card_22,
                Properties.Resources.s_all_card_23,
                Properties.Resources.s_all_card_24,
                Properties.Resources.s_all_card_25,

                Properties.Resources.s_all_card_26,
                Properties.Resources.s_all_card_27,
                Properties.Resources.s_all_card_28,
                Properties.Resources.s_all_card_29,
                Properties.Resources.s_all_card_30,
                Properties.Resources.s_all_card_31,
                Properties.Resources.s_all_card_32,
                Properties.Resources.s_all_card_33,
                Properties.Resources.s_all_card_34,
                Properties.Resources.s_all_card_35,
                Properties.Resources.s_all_card_36,
                Properties.Resources.s_all_card_37,
                Properties.Resources.s_all_card_38,

                Properties.Resources.s_all_card_39,
                Properties.Resources.s_all_card_40,
                Properties.Resources.s_all_card_41,
                Properties.Resources.s_all_card_42,
                Properties.Resources.s_all_card_43,
                Properties.Resources.s_all_card_44,
                Properties.Resources.s_all_card_45,
                Properties.Resources.s_all_card_46,
                Properties.Resources.s_all_card_47,
                Properties.Resources.s_all_card_48,
                Properties.Resources.s_all_card_49,
                Properties.Resources.s_all_card_50,
                Properties.Resources.s_all_card_51,
            };

            // уровни колоды (0..4) и питов (0..4)
            _deckFrames = new Image[]
            {
                Properties.Resources.s_deck_0,
                Properties.Resources.s_deck_1,
                Properties.Resources.s_deck_2,
                Properties.Resources.s_deck_3,
                Properties.Resources.s_deck_4
            };

            _pitFrames = new Image[]
            {
                Properties.Resources.s_card_done_0,
                Properties.Resources.s_card_done_1,
                Properties.Resources.s_card_done_2,
                Properties.Resources.s_card_done_3,
                Properties.Resources.s_card_done_4
            };

            // по умолчанию питов нет
            pictureBoxMyPit.Visible = false;
            pictureBoxSvinPit.Visible = false;

            // глаза «лежат» поверх головы, прозрачность берётся от головы
            // сначала запоминаем координаты глаз на форме
            var glazaOnForm = pictureBoxGlaza.Location;

            // меняем родителя на голову
            pictureBoxGlaza.Parent = pictureBoxHead;
            pictureBoxGlaza.BackColor = Color.Transparent;

            // пересчитываем координату в систему координат головы
            pictureBoxGlaza.Location = new Point(
                glazaOnForm.X - pictureBoxHead.Location.X,
                glazaOnForm.Y - pictureBoxHead.Location.Y
            );

            // фиксируем окно
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            // ПУЛЬС кнопки выхода
            _pulseTimer = new Timer();
            _pulseTimer.Interval = 30;
            _pulseTimer.Tick += PulseTimer_Tick;

            buttonFinish.MouseEnter += Button_MouseEnter_Pulse;
            buttonFinish.MouseLeave += Button_MouseLeave_Pulse;

            // НОВОЕ: настраиваем таймер анимации руки
            _handTimer = new Timer();
            _handTimer.Interval = 30; // 30 мс
            _handTimer.Tick += HandTimer_Tick;

            // запомним конечную позицию руки (куда она должна "упасть")
            _handTarget = pictureBoxHand.Location;
            // стартовая позиция - выше стола (рука "летит" сверху)
            pictureBoxHand.Location = new Point(_handTarget.X, _handTarget.Y + 200);
            pictureBoxHand.Visible = false;
            pictureBoxSouvenir.Visible = false;

            // начальное состояние анимации
            _handPhase = HandPhase.None;
            _handWaitTicks = 0;

            // порядок слоёв: сувенир под рукой, кнопка сверху
            pictureBoxSouvenir.SendToBack();
            pictureBoxHand.BringToFront();
            buttonFinish.BringToFront();

            // КАДРЫ ГОЛОВЫ И ТАЙМЕР АНИМАЦИИ
            _headFrames = new Image[]
            {
                Properties.Resources.s_ehead_idle_0,  // замени на реальные имена ресурсов
                Properties.Resources.s_ehead_idle_1,
                Properties.Resources.s_ehead_idle_2,
                Properties.Resources.s_ehead_idle_3
            };

            _headTimer = new Timer();
            _headTimer.Interval = 175; // мс между кадрами
            _headTimer.Tick += HeadTimer_Tick;

            pictureBoxHead.Image = _headFrames[0];

            // КАДРЫ ГЛАЗ и ТАЙМЕР МОРГАНИЯ
            _eyesFrames = new Image[]
            {
                Properties.Resources.s_glaza_idle_0,
                Properties.Resources.s_glaza_idle_1,
            };

            _eyesTimer = new Timer();
            _eyesTimer.Interval = 1500; // мс между кадрами моргания
            _eyesTimer.Tick += EyesTimer_Tick;

            pictureBoxGlaza.Image = _eyesFrames[0];

            // РУКА СВИНА (будем анимировать тремя PictureBox'ами)
            _pigHandTimer = new Timer();
            _pigHandTimer.Interval = 150;          // скорость анимации руки свина
            _pigHandTimer.Tick += PigHandTimer_Tick;

            // запоминаем конечную позицию (дефолтную позицию из дизайнера)
            _pigHandTarget = pictureBoxLeftSvinHand.Location;

            // начальный кадр: только базовая рука видна
            _pigHandFrameIndex = 0;
            pictureBoxLeftSvinHand.Visible = true;
            pictureBoxLeftSvinHand1.Visible = false;
            pictureBoxLeftSvinHand2.Visible = false;

            // сувенир свина пока не виден
            pictureBoxPigSouvenir.Visible = false;

            // запустить постоянное моргание
            _eyesTimer.Start();

            // начальное состояние колоды — полная
            pictureBox1StackCard.Image = _deckFrames[4];

            // повернуть стрелку вниз
            pictureBoxDrawArrow.Visible = false;
            if (pictureBoxDrawArrow.Image != null)
            {
                pictureBoxDrawArrow.Image = (Image)pictureBoxDrawArrow.Image.Clone();
                pictureBoxDrawArrow.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            pictureBoxDrawArrow.BringToFront();

            // подпишемся на событие Shown (когда форма уже показана) —
            // стартуем вступление
            this.Shown += FormGame_Shown;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
        }

        // Стартовая сцена после открытия формы игры
        private async void FormGame_Shown(object sender, EventArgs e)
        {
            await PlayIntroSequenceAsync();
        }

        private async Task PlayIntroSequenceAsync()
        {
            // голова шевелится на первой речи
            StartHeadAnimation();
            await PlayIntroSoundAsync(Properties.Resources.snd_ru_start_game_0_ogg);
            StopHeadAnimation();

            await PlayIntroSoundAsync(Properties.Resources.snd_ru_start_game_1_ogg);

            // третья фраза
            StartHeadAnimation();
            await PlayIntroSoundAsync(Properties.Resources.snd_ru_start_game_2_ogg);
            StopHeadAnimation();

            // если во время диалогов нажали Skip — выходим, дальше займётся кнопка Skip
            if (_introSkipped)
                return;

            int myIndex = _rnd.Next(_souvenirFrames.Length);
            int pigIndex;
            do
            {
                pigIndex = _rnd.Next(_souvenirFrames.Length);
            } while (pigIndex == myIndex);

            // назначаем картинки
            pictureBoxSouvenir.Image = _souvenirFrames[myIndex];
            pictureBoxPigSouvenir.Image = _souvenirFrames[pigIndex];

            // НАЧИНАЕМ АНИМАЦИЮ НАШЕЙ РУКИ
            pictureBoxHand.Visible = true;
            _playerHandAnimationTcs = new TaskCompletionSource<bool>();
            _handPhase = HandPhase.MovingUp;
            _handWaitTicks = 0;
            _handTimer.Start();

            // ЖДЁМ, ПОКА РУКА ПОЛНОСТЬЮ ОТЫГРАЕТ (поднялась, положила сувенир, ушла вниз)
            await _playerHandAnimationTcs.Task;

            // ПОСЛЕ этого — 4‑я фраза
            StartHeadAnimation();
            await PlaySoundResourceAsync(Properties.Resources.snd_ru_start_game_4_ogg);
            StopHeadAnimation();

            // ЗАПУСК АНИМАЦИИ РУКИ СВИНА
            _pigHandAnimationTcs = new TaskCompletionSource<bool>();
            _pigHandPhase = HandPhase.MovingUp;
            _pigHandWaitTicks = 0;
            _pigHandTimer.Start();

            // ждём пока свин положит свой сувенир
            await _pigHandAnimationTcs.Task;

            // «GO»
            StartHeadAnimation();
            await PlaySoundResourceAsync(Properties.Resources.snd_ru_start_game_go_ogg);
            StopHeadAnimation();

            // ПОСЛЕ вступления запускаем собственно игру
            StartGame();
        }

        // Проиграть одну фразу интро с возможностью остановки по Skip
        private async Task PlayIntroSoundAsync(System.IO.UnmanagedMemoryStream sound)
        {
            if (_introSkipped) return;

            // подстрахуемся: если что-то уже играло — остановить
            _introPlayer.Stop();

            // Important: сбросить позицию потока ресурса
            if (sound.CanSeek)
                sound.Position = 0;

            _introPlayer.Stream = sound;

            // стартуем проигрывание
            _introPlayer.Play();

            // Ждём примерно 2 секунды, но выходим раньше, если нажали Skip
            const int totalMs = 2000;
            const int stepMs = 50;
            int elapsed = 0;

            while (elapsed < totalMs && !_introSkipped)
            {
                await Task.Delay(stepMs);
                elapsed += stepMs;
            }

            // останавливаем фразу (если до сих пор играет)
            _introPlayer.Stop();
        }

        // Короткое интро для Skip: только сувениры + GO, без начальных диалогов
        private async Task PlaySkipIntroSequenceAsync()
        {
            if (_gameStarted) return;

            // выбираем случайные сувениры (как в полном интро)
            int myIndex = _rnd.Next(_souvenirFrames.Length);
            int pigIndex;
            do
            {
                pigIndex = _rnd.Next(_souvenirFrames.Length);
            } while (pigIndex == myIndex);

            pictureBoxSouvenir.Image = _souvenirFrames[myIndex];
            pictureBoxPigSouvenir.Image = _souvenirFrames[pigIndex];

            // подготовка состояний для нашей руки
            pictureBoxSouvenir.Visible = false;
            pictureBoxPigSouvenir.Visible = false;

            pictureBoxHand.Location = new Point(_handTarget.X, _handTarget.Y + 200);
            pictureBoxHand.Image = Properties.Resources.s_hand4_1; // открытая рука
            pictureBoxHand.Visible = true;

            _handPhase = HandPhase.MovingUp;
            _handWaitTicks = 0;
            _playerHandAnimationTcs = new TaskCompletionSource<bool>();
            _handTimer.Start();

            // ждём, пока наша рука отыграет (поднялась, положила сувенир, ушла вниз)
            await _playerHandAnimationTcs.Task;

            // подготовка руки свина
            _pigHandFrameIndex = 0;
            pictureBoxLeftSvinHand.Visible = true;
            pictureBoxLeftSvinHand1.Visible = false;
            pictureBoxLeftSvinHand2.Visible = false;
            pictureBoxPigSouvenir.Visible = false;

            _pigHandPhase = HandPhase.MovingUp;
            _pigHandWaitTicks = 0;
            _pigHandAnimationTcs = new TaskCompletionSource<bool>();
            _pigHandTimer.Start();

            // ждём, пока рука свина отыграет
            await _pigHandAnimationTcs.Task;

            // "GO" и старт игры
            StartHeadAnimation();
            await PlaySoundResourceAsync(Properties.Resources.snd_ru_start_game_go_ogg);
            StopHeadAnimation();

            StartGame();
        }

        private Task PlaySoundResourceAsync(System.IO.UnmanagedMemoryStream sound)
        {
            return Task.Run(() =>
            {
                using (var player = new SoundPlayer(sound))
                {
                    player.PlaySync();
                }
            });
        }

        // игрок спрашивает
        private System.IO.UnmanagedMemoryStream GetHaveSoundPlayer()
        {
            // 60% вариант 0, 40% вариант 1
            if (_rnd.NextDouble() < 0.6)
                return Properties.Resources.snd_ru_have_0_ogg;
            else
                return Properties.Resources.snd_ru_have_1_ogg;
        }

        // свин спрашивает
        private System.IO.UnmanagedMemoryStream GetHaveSoundPig()
        {
            if (_rnd.NextDouble() < 0.6)
                return Properties.Resources.snd_ru_ehave_0_ogg;
            else
                return Properties.Resources.snd_ru_ehave_1_ogg;
        }

        // "берите карту" (свин нам)
        private System.IO.UnmanagedMemoryStream GetDontSoundPig()
        {
            if (_rnd.NextDouble() < 0.6)
                return Properties.Resources.snd_ru_dont_0_ogg;
            else
                return Properties.Resources.snd_ru_dont_1_ogg;
        }

        // "у меня нет" (мы свину) – 4 варианта, равномерно
        private System.IO.UnmanagedMemoryStream GetMyDontSound()
        {
            int i = _rnd.Next(4); // 0..3
            switch (i)
            {
                case 0: return Properties.Resources.snd_ru_my_dont_0_ogg;
                case 1: return Properties.Resources.snd_ru_my_dont_1_ogg;
                case 2: return Properties.Resources.snd_ru_my_dont_2_ogg;
                default: return Properties.Resources.snd_ru_my_dont_3_ogg;
            }
        }

        // озвучка числа (2..14) – когда ГОВОРИМ МЫ
        private System.IO.UnmanagedMemoryStream GetRankNumberSoundPlayer(Rank rank)
        {
            switch (rank)
            {
                case Rank.Two: return Properties.Resources.snd_ru_2_ogg;
                case Rank.Three: return Properties.Resources.snd_ru_3_ogg;
                case Rank.Four: return Properties.Resources.snd_ru_4_ogg;
                case Rank.Five: return Properties.Resources.snd_ru_5_ogg;
                case Rank.Six: return Properties.Resources.snd_ru_6_ogg;
                case Rank.Seven: return Properties.Resources.snd_ru_7_ogg;
                case Rank.Eight: return Properties.Resources.snd_ru_8_ogg;
                case Rank.Nine: return Properties.Resources.snd_ru_9_ogg;
                case Rank.Ten: return Properties.Resources.snd_ru_10_ogg;
                case Rank.Jack: return Properties.Resources.snd_ru_11_ogg;
                case Rank.Queen: return Properties.Resources.snd_ru_12_ogg;
                case Rank.King: return Properties.Resources.snd_ru_13_ogg;
                case Rank.Ace: return Properties.Resources.snd_ru_14_ogg;
                default: return Properties.Resources.snd_ru_2_ogg;
            }
        }

        // озвучка числа (2..14) – когда ГОВОРИТ СВИН
        private System.IO.UnmanagedMemoryStream GetRankNumberSoundPig(Rank rank)
        {
            switch (rank)
            {
                case Rank.Two: return Properties.Resources.snd_ru_e_2_ogg;
                case Rank.Three: return Properties.Resources.snd_ru_e_3_ogg;
                case Rank.Four: return Properties.Resources.snd_ru_e_4_ogg;
                case Rank.Five: return Properties.Resources.snd_ru_e_5_ogg;
                case Rank.Six: return Properties.Resources.snd_ru_e_6_ogg;
                case Rank.Seven: return Properties.Resources.snd_ru_e_7_ogg;
                case Rank.Eight: return Properties.Resources.snd_ru_e_8_ogg;
                case Rank.Nine: return Properties.Resources.snd_ru_e_9_ogg;
                case Rank.Ten: return Properties.Resources.snd_ru_e_10_ogg;
                case Rank.Jack: return Properties.Resources.snd_ru_e_11_ogg;
                case Rank.Queen: return Properties.Resources.snd_ru_e_12_ogg;
                case Rank.King: return Properties.Resources.snd_ru_e_13_ogg;
                case Rank.Ace: return Properties.Resources.snd_ru_e_14_ogg;
                default: return Properties.Resources.snd_ru_e_2_ogg;
            }
        }

        private void StartHeadAnimation()
        {
            _headFrameIndex = 0;
            _headTimer.Start();
        }

        private void StopHeadAnimation()
        {
            _headTimer.Stop();
            // вернуть голову в первый кадр
            pictureBoxHead.Image = _headFrames[0];
        }

        private void HeadTimer_Tick(object sender, EventArgs e)
        {
            _headFrameIndex++;
            if (_headFrameIndex >= _headFrames.Length)
                _headFrameIndex = 0;

            pictureBoxHead.Image = _headFrames[_headFrameIndex];
        }

        private void EyesTimer_Tick(object sender, EventArgs e)
        {
            if (!_eyesBlinkPhase)
            {
                pictureBoxGlaza.Image = _eyesFrames[1]; // закрытые
                _eyesBlinkPhase = true;
                _eyesTimer.Interval = 200; // быстрое закрытие/открытие
            }
            else
            {
                pictureBoxGlaza.Image = _eyesFrames[0]; // открытые
                _eyesBlinkPhase = false;
                _eyesTimer.Interval = 1500; // пауза до следующего мигания
            }
        }

        private enum HandPhase
        {
            None,
            MovingUp,
            WaitOnTable,
            MovingDown
        }

        private class PlayerCardVisual
        {
            public Card Card { get; set; }
            public Point OriginalLocation { get; set; }
        }

        private HandPhase _handPhase = HandPhase.None;
        private int _handWaitTicks = 0;

        private void HandTimer_Tick(object sender, EventArgs e)
        {
            switch (_handPhase)
            {
                case HandPhase.MovingUp:
                    // пока рука НИЖЕ цели - поднимаем вверх
                    if (pictureBoxHand.Location.Y > _handTarget.Y)
                    {
                        pictureBoxHand.Location = new Point(
                            pictureBoxHand.Location.X,
                            pictureBoxHand.Location.Y - _handSpeed
                        );
                    }
                    else
                    {
                        // дошли до стола
                        pictureBoxHand.Location = _handTarget;

                        // меняем руку на закрытую
                        pictureBoxHand.Image = Properties.Resources.s_hand4_1;

                        // показываем сувенир
                        pictureBoxSouvenir.Visible = true;

                        // переходим в фазу ожидания
                        _handPhase = HandPhase.WaitOnTable;
                        _handWaitTicks = 0;
                    }
                    break;

                case HandPhase.WaitOnTable:
                    // ждём ~1 секунду (1000 мс)
                    _handWaitTicks++;
                    if (_handWaitTicks * _handTimer.Interval >= 1000)
                    {
                        _handPhase = HandPhase.MovingDown;
                    }
                    break;

                case HandPhase.MovingDown:
                    if (pictureBoxHand.Location.Y < this.ClientSize.Height)
                    {
                        pictureBoxHand.Location = new Point(
                            pictureBoxHand.Location.X,
                            pictureBoxHand.Location.Y + _handSpeed
                        );
                    }
                    else
                    {
                        pictureBoxHand.Visible = false;
                        _handPhase = HandPhase.None;
                        _handTimer.Stop();

                        // СИГНАЛ В intro‑последовательность, что мы закончили
                        _playerHandAnimationTcs?.TrySetResult(true);
                    }
                    break;

                case HandPhase.None:
                default:
                    // ничего не делаем
                    break;
            }
        }

        private void PigHandTimer_Tick(object sender, EventArgs e)
        {
            switch (_pigHandPhase)
            {
                case HandPhase.MovingUp:
                    if (_pigHandFrameIndex == 0)
                    {
                        // кадр 1
                        _pigHandFrameIndex = 1;
                        pictureBoxLeftSvinHand.Visible = false;
                        pictureBoxLeftSvinHand1.Visible = true;
                        pictureBoxLeftSvinHand2.Visible = false;
                    }
                    else if (_pigHandFrameIndex == 1)
                    {
                        // кадр 2 (рука кладёт сувенир)
                        _pigHandFrameIndex = 2;
                        pictureBoxLeftSvinHand.Visible = false;
                        pictureBoxLeftSvinHand1.Visible = false;
                        pictureBoxLeftSvinHand2.Visible = true;

                        // показываем сувенир
                        pictureBoxPigSouvenir.Visible = true;

                        // переходим к фазе ожидания
                        _pigHandPhase = HandPhase.WaitOnTable;
                        _pigHandWaitTicks = 0;
                    }
                    break;

                case HandPhase.WaitOnTable:
                    // небольшая пауза с рукой на столе
                    _pigHandWaitTicks++;
                    if (_pigHandWaitTicks * _pigHandTimer.Interval >= 800) // ~0.8 сек
                    {
                        _pigHandPhase = HandPhase.MovingDown;
                    }
                    break;

                case HandPhase.MovingDown:
                    if (_pigHandFrameIndex == 2)
                    {
                        _pigHandFrameIndex = 1;
                        pictureBoxLeftSvinHand.Visible = false;
                        pictureBoxLeftSvinHand1.Visible = true;
                        pictureBoxLeftSvinHand2.Visible = false;
                    }
                    else if (_pigHandFrameIndex == 1)
                    {
                        _pigHandFrameIndex = 0;
                        pictureBoxLeftSvinHand.Visible = true;
                        pictureBoxLeftSvinHand1.Visible = false;
                        pictureBoxLeftSvinHand2.Visible = false;
                    }
                    else
                    {
                        // вернулись в исходную позу
                        _pigHandPhase = HandPhase.None;
                        _pigHandTimer.Stop();
                        _pigHandAnimationTcs?.TrySetResult(true);
                    }
                    break;

                case HandPhase.None:
                default:
                    break;
            }
        }

        private void UpdateDeckVisual()
        {
            if (_deck == null)
                return;

            int count = _deck.Count;
            int frameIndex;

            if (count <= 0) frameIndex = 0;
            else if (count > 39) frameIndex = 4;
            else if (count > 26) frameIndex = 3;
            else if (count > 13) frameIndex = 2;
            else frameIndex = 1;

            pictureBox1StackCard.Image = _deckFrames[frameIndex];
            pictureBox1StackCard.Visible = count > 0;
        }

        // ================== ИГРОВАЯ ЛОГИКА: РАЗДАЧА ==================

        // Запуск новой игры (пока только выдача 7 карт игроку)
        private void StartGame()
        {
            if (_gameStarted) return;
            _gameStarted = true;

            buttonSkip.Visible = false;

            _deck = new Deck();
            _deck.Shuffle();

            _playerHand = new List<Card>();
            _pigHand = new List<Card>();

            // 7 карт игроку
            for (int i = 0; i < 7; i++)
            {
                var card = _deck.Draw();
                if (card != null)
                    _playerHand.Add(card);
            }

            // 7 карт свину
            for (int i = 0; i < 7; i++)
            {
                var card = _deck.Draw();
                if (card != null)
                    _pigHand.Add(card);
            }
            UpdateDeckVisual();
            RenderPlayerHand();

            _currentTurn = TurnOwner.Player;
        }

        // Проверка руки на наличие сундучков (4 карты одного ранга)
        private void CheckAndCollectChests(List<Card> hand, bool isPlayer)
        {
            // группируем по рангу
            var groups = hand
                .GroupBy(c => c.Rank)
                .Where(g => g.Count() >= 4)
                .ToList();

            if (!groups.Any())
                return;

            foreach (var g in groups)
            {
                // убрать ровно 4 карты этого ранга из руки
                int removeCount = 4;
                for (int i = hand.Count - 1; i >= 0 && removeCount > 0; i--)
                {
                    if (hand[i].Rank == g.Key)
                    {
                        hand.RemoveAt(i);
                        removeCount--;
                    }
                }

                if (isPlayer)
                {
                    _playerChestCount++;
                }
                else
                {
                    _pigChestCount++;
                }
            }

            // после удаления карт обновим руку игрока (если это он)
            if (isPlayer)
                RenderPlayerHand();

            // обновить питов и проверить конец игры
            UpdatePitsVisual();
            CheckGameEnd();
        }

        private void CheckGameEnd()
        {
            if (_gameOver)
                return;

            if (_playerChestCount >= 5 || _pigChestCount >= 5)
            {
                _gameOver = true;

                string message;
                if (_playerChestCount > _pigChestCount)
                    message = "Вы собрали 5 сундучков и победили!";
                else if (_playerChestCount < _pigChestCount)
                    message = "Мистер Свин собрал 5 сундучков. Вы проиграли!";
                else
                    message = "Оба собрали по 5 сундучков. Ничья!";

                MessageBox.Show(message, "Игра окончена");
                this.Close();
            }
        }

        // обновить изображение питов (стопок сундучков) у нас и у свина
        private void UpdatePitsVisual()
        {
            // ограничиваем индекс 0..4 (4 - это 5 и больше)
            int playerIndex = Math.Min(_playerChestCount, 4);
            int pigIndex = Math.Min(_pigChestCount, 4);

            // наш пит
            if (playerIndex > 0)
            {
                pictureBoxMyPit.Visible = true;
                pictureBoxMyPit.Image = _pitFrames[playerIndex];
            }
            else
            {
                pictureBoxMyPit.Visible = false;
            }

            // пит свина
            if (pigIndex > 0)
            {
                pictureBoxSvinPit.Visible = true;
                pictureBoxSvinPit.Image = _pitFrames[pigIndex];
            }
            else
            {
                pictureBoxSvinPit.Visible = false;
            }

            // вывести поверх нужных слоёв
            pictureBoxMyPit.BringToFront();
            pictureBoxSvinPit.BringToFront();
            buttonFinish.BringToFront();
        }

        // ХОД ИГРОКА: пока автоматически спрашивает ранг первой карты в руке
        private async Task PlayerTurnAsync(Rank askedRank)
        {
            if (_gameOver) return;
            if (_playerHand == null || _playerHand.Count == 0)
                return;

            _currentTurn = TurnOwner.Player;

            // 1) озвучка: "У вас есть ..." (вариант 0/1) + номер
            await PlaySoundResourceAsync(GetHaveSoundPlayer());
            await PlaySoundResourceAsync(GetRankNumberSoundPlayer(askedRank));

            // 2) проверяем, есть ли у свина карты этого ранга
            var taken = _pigHand.Where(c => c.Rank == askedRank).ToList();

            if (taken.Count > 0)
            {
                // есть совпадения — свин "отдаёт" все карты этого ранга
                foreach (var c in taken)
                {
                    _pigHand.Remove(c);
                    _playerHand.Add(c);
                }

                RenderPlayerHand();
                CheckAndCollectChests(_playerHand, isPlayer: true);

                // по правилам: игрок ходит дальше, но теперь он снова кликнет по карте сам.
                _currentTurn = TurnOwner.Player;
            }
            else
            {
                // у свина НЕТ таких карт — он говорит "берите карту"
                StartHeadAnimation();
                await PlaySoundResourceAsync(GetDontSoundPig());
                StopHeadAnimation();

                // включаем стрелку над колодой, ждём клика по ней
                _waitingPlayerDraw = true;
                pictureBoxDrawArrow.Visible = true;
            }

            if (_gameOver) return;
        }

        private async Task PigTurnAsync()
        {
            if (_gameOver) return;
            if (_pigHand == null || _pigHand.Count == 0)
                return;

            _currentTurn = TurnOwner.Pig;

            // ИИ: выбираем ранг случайной карты из руки свина
            var distinctRanks = _pigHand.Select(c => c.Rank).Distinct().ToList();
            if (distinctRanks.Count == 0)
            {
                _currentTurn = TurnOwner.Player;
                return;
            }

            Rank askedRank = distinctRanks[_rnd.Next(distinctRanks.Count)];

            // 1) озвучка: "У вас есть ..." от свина + номер, с анимацией головы
            StartHeadAnimation();
            await PlaySoundResourceAsync(GetHaveSoundPig());
            await PlaySoundResourceAsync(GetRankNumberSoundPig(askedRank));
            StopHeadAnimation();

            // 2) проверяем, есть ли у НАС такие карты
            var given = _playerHand.Where(c => c.Rank == askedRank).ToList();

            if (given.Count > 0)
            {
                // отдаём все карты этого ранга свину
                foreach (var c in given)
                {
                    _playerHand.Remove(c);
                    _pigHand.Add(c);
                }

                RenderPlayerHand();
                CheckAndCollectChests(_pigHand, isPlayer: false);

                // по правилам: свин может продолжать, но
                // для простоты теперь ход отдаём игроку
                _currentTurn = TurnOwner.Player;
            }
            else
            {
                // у нас НЕТ таких карт — говорим "у меня нет"
                await PlaySoundResourceAsync(GetMyDontSound());

                // по правилам Go Fish свин должен взять карту из колоды
                var card = _deck.Draw();
                if (card != null)
                {
                    _pigHand.Add(card);
                    UpdateDeckVisual();
                    CheckAndCollectChests(_pigHand, isPlayer: false);
                }

                _currentTurn = TurnOwner.Player;
            }

            if (_gameOver) return;
        }

        // Отрисовка 7 карт игрока на столе
        private void RenderPlayerHand()
        {
            // сначала убираем старые картинki, если были
            foreach (var pb in _playerCardBoxes)
            {
                this.Controls.Remove(pb);
                pb.Dispose();
            }
            _playerCardBoxes.Clear();

            if (_playerHand == null || _playerHand.Count == 0)
                return;

            // группируем карты по РАНГУ (Two, Three, ...)
            var groups = _playerHand
                .GroupBy(c => c.Rank)
                .ToList();

            int columns = groups.Count;          // сколько столбцов по рангу
            int cardWidth = 60;
            int cardHeight = 90;
            int yBase = 320;                     // базовый Y для "нижней" карты
            int gapX = 5;                        // расстояние между столбцами
            int stackOffsetY = 10;               // на сколько выше рисовать каждый дубликат

            // общая ширина всех столбцов
            int totalWidth = columns * cardWidth + (columns - 1) * gapX;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            if (startX < 0) startX = 0;

            for (int col = 0; col < columns; col++)
            {
                int x = startX + col * (cardWidth + gapX);
                var group = groups[col].ToList();    // все карты этого ранга

                for (int k = 0; k < group.Count; k++)
                {
                    var card = group[k];

                    int y = yBase - k * stackOffsetY;

                    var pb = new PictureBox();
                    pb.Size = new Size(cardWidth, cardHeight);
                    pb.Location = new Point(x, y);
                    pb.BackColor = Color.Transparent;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Image = GetCardImage(card);

                    // сохраняем связь картинка <-> карта и исходная позиция
                    pb.Tag = new PlayerCardVisual
                    {
                        Card = card,
                        OriginalLocation = pb.Location
                    };

                    // события наведения и клика
                    pb.MouseEnter += PlayerCard_MouseEnter;
                    pb.MouseLeave += PlayerCard_MouseLeave;
                    pb.Click += PlayerCard_Click;

                    this.Controls.Add(pb);

                    if (k == 0)
                        pb.BringToFront();
                    else
                        pb.SendToBack();

                    _playerCardBoxes.Add(pb);
                }
            }

            // вернуть поверх важных элементов (чтобы их не перекрыли карты)
            pictureBoxSouvenir.BringToFront();
            pictureBoxPigSouvenir.BringToFront();
            pictureBoxHand.BringToFront();
            pictureBoxLeftSvinHand.BringToFront();
            pictureBoxLeftSvinHand1.BringToFront();
            pictureBoxLeftSvinHand2.BringToFront();
            pictureBoxRightSvinHand.BringToFront();
            buttonFinish.BringToFront();
        }

        private void PlayerCard_MouseEnter(object sender, EventArgs e)
        {
            if (_gameOver) return;

            var pb = sender as PictureBox;
            if (pb?.Tag is PlayerCardVisual data)
            {
                // поднимаем карту чуть вверх
                pb.Location = new Point(pb.Location.X, data.OriginalLocation.Y - 10);
            }
        }

        private void PlayerCard_MouseLeave(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb?.Tag is PlayerCardVisual data)
            {
                // возвращаем в исходную позицию
                pb.Location = data.OriginalLocation;
            }
        }

        private async void PlayerCard_Click(object sender, EventArgs e)
        {
            if (_gameOver) return;
            if (_currentTurn != TurnOwner.Player) return;
            if (_waitingPlayerDraw) return;
            if (_playerActionInProgress) return;

            var pb = sender as PictureBox;
            if (pb == null) return;

            var data = pb.Tag as PlayerCardVisual;
            if (data == null) return;

            _playerActionInProgress = true;
            try
            {
                await PlayerTurnAsync(data.Card.Rank);
            }
            finally
            {
                _playerActionInProgress = false;
            }
        }

        // Сейчас возвращаем одну тестовую рубашку.
        private Image GetCardImage(Card card)
        {
            // Индекс ранга от 0 до 12 (Two=2 → 0, ... Ace=14 → 12)
            int rankIndex = (int)card.Rank - 2; // Rank.Two = 2

            int suitBase;
            switch (card.Suit)
            {
                case Suit.Spades:    // Пики
                    suitBase = 0;    // s_all_card_0..12
                    break;
                case Suit.Clubs:     // Трефы
                    suitBase = 13;   // s_all_card_13..25
                    break;
                case Suit.Hearts:    // Червы
                    suitBase = 26;   // s_all_card_26..38
                    break;
                case Suit.Diamonds:  // Бубны
                    suitBase = 39;   // s_all_card_39..51
                    break;
                default:
                    suitBase = 0;
                    break;
            }

            int index = suitBase + rankIndex;

            // Защита от выхода за границы массива
            if (index < 0 || index >= _cardSprites.Length)
                return Properties.Resources.s_deck_0; // fallback — рубашка

            return _cardSprites[index];
        }

        private async void pictureBox1StackCard_Click(object sender, EventArgs e)
        {
            if (!_waitingPlayerDraw)
                return; // сейчас не наш этап "берите карту"

            _waitingPlayerDraw = false;
            pictureBoxDrawArrow.Visible = false;

            // берём одну карту из колоды
            var card = _deck.Draw();
            if (card != null)
            {
                _playerHand.Add(card);
                RenderPlayerHand();
                CheckAndCollectChests(_playerHand, isPlayer: true);
                UpdateDeckVisual();
            }

            // после того, как мы взяли карту, ход переходит к свину
            _currentTurn = TurnOwner.Pig;
            await PigTurnAsync();

            if (_gameOver) return;
        }

        private async void buttonSkip_Click(object sender, EventArgs e)
        {
            if (_gameStarted) return;
            if (_introSkipped) return; // чтобы не запускать дважды

            _introSkipped = true;

            // Остановить текущие звуки интро
            _introPlayer?.Stop();

            // Остановить текущие интро-анимации (если шли)
            _handTimer.Stop();
            _pigHandTimer.Stop();

            // временно скрываем
            pictureBoxHand.Visible = false;
            pictureBoxSouvenir.Visible = false;
            pictureBoxPigSouvenir.Visible = false;

            // разблокировать ожидающие TCS, если они ещё ждут,
            // чтобы PlayIntroSequenceAsync мог корректно выйти по _introSkipped
            _playerHandAnimationTcs?.TrySetResult(true);
            _pigHandAnimationTcs?.TrySetResult(true);

            // запускаем короткую последовательность: сувениры + GO + игра
            await PlaySkipIntroSequenceAsync();
        }
    }
}
