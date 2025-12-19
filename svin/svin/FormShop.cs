using System;
using System.Drawing;
using System.Windows.Forms;

namespace svin
{
    public class FormShop : Form
    {
        public FormShop()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(1000, 500);
            this.Text = "Магазин карт";

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.White; 

            // загружаем стили карт
            Image[] styles = new Image[]
            {
                Properties.Resources.s_shop_cards_0,
                Properties.Resources.s_shop_cards_1,
                Properties.Resources.s_shop_cards_2,
                Properties.Resources.s_shop_cards_3,
            };

            int cardWidth = 180;
            int cardHeight = 120;
            int gap = 15;

            int totalWidth = styles.Length * cardWidth + (styles.Length - 1) * gap;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int y = (this.ClientSize.Height - cardHeight) / 2;

            EventHandler closeHandler = (s, e) => this.Close();

            for (int i = 0; i < styles.Length; i++)
            {
                var pb = new PictureBox();
                pb.Size = new Size(cardWidth, cardHeight);
                pb.Location = new Point(startX + i * (cardWidth + gap), y);
                pb.BackColor = Color.Transparent;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = styles[i];
                pb.Cursor = Cursors.Hand;

                pb.Click += closeHandler; // клик по любой карте закрывает магазин

                this.Controls.Add(pb);
            }

            // кнопка-крестик в правом верхнем углу
            var btnClose = new PictureBox();
            btnClose.Size = new Size(32, 32);
            btnClose.Location = new Point(this.ClientSize.Width - 40, 8);
            btnClose.BackColor = Color.Transparent;
            btnClose.SizeMode = PictureBoxSizeMode.StretchImage;
            btnClose.Image = Properties.Resources.x;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += closeHandler;

            this.Controls.Add(btnClose);
            btnClose.BringToFront();
        }
    }
}
