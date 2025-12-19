namespace svin
{
    partial class FormGame
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.buttonFinish = new System.Windows.Forms.Button();
            this.pictureBoxHand = new System.Windows.Forms.PictureBox();
            this.pictureBoxSouvenir = new System.Windows.Forms.PictureBox();
            this.pictureBoxHead = new System.Windows.Forms.PictureBox();
            this.pictureBoxGlaza = new System.Windows.Forms.PictureBox();
            this.pictureBox1StackCard = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeftSvinHand = new System.Windows.Forms.PictureBox();
            this.pictureBoxRightSvinHand = new System.Windows.Forms.PictureBox();
            this.pictureBoxPigSouvenir = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeftSvinHand1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeftSvinHand2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxDrawArrow = new System.Windows.Forms.PictureBox();
            this.pictureBoxMyPit = new System.Windows.Forms.PictureBox();
            this.pictureBoxSvinPit = new System.Windows.Forms.PictureBox();
            this.buttonSkip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSouvenir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlaza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1StackCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightSvinHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPigSouvenir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDrawArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMyPit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSvinPit)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFinish
            // 
            this.buttonFinish.BackColor = System.Drawing.Color.Transparent;
            this.buttonFinish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonFinish.BackgroundImage")));
            this.buttonFinish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFinish.FlatAppearance.BorderSize = 0;
            this.buttonFinish.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonFinish.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFinish.Location = new System.Drawing.Point(865, 12);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(92, 64);
            this.buttonFinish.TabIndex = 0;
            this.buttonFinish.UseVisualStyleBackColor = false;
            this.buttonFinish.Click += new System.EventHandler(this.button1_Click);
            this.buttonFinish.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.buttonFinish.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // pictureBoxHand
            // 
            this.pictureBoxHand.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxHand.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHand.Image")));
            this.pictureBoxHand.Location = new System.Drawing.Point(-85, 225);
            this.pictureBoxHand.Name = "pictureBoxHand";
            this.pictureBoxHand.Size = new System.Drawing.Size(279, 279);
            this.pictureBoxHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHand.TabIndex = 1;
            this.pictureBoxHand.TabStop = false;
            this.pictureBoxHand.Visible = false;
            // 
            // pictureBoxSouvenir
            // 
            this.pictureBoxSouvenir.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSouvenir.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSouvenir.Image")));
            this.pictureBoxSouvenir.Location = new System.Drawing.Point(98, 273);
            this.pictureBoxSouvenir.Name = "pictureBoxSouvenir";
            this.pictureBoxSouvenir.Size = new System.Drawing.Size(76, 38);
            this.pictureBoxSouvenir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSouvenir.TabIndex = 2;
            this.pictureBoxSouvenir.TabStop = false;
            this.pictureBoxSouvenir.Visible = false;
            // 
            // pictureBoxHead
            // 
            this.pictureBoxHead.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxHead.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBoxHead.Image = global::svin.Properties.Resources.s_ehead_idle_0;
            this.pictureBoxHead.Location = new System.Drawing.Point(346, 12);
            this.pictureBoxHead.Name = "pictureBoxHead";
            this.pictureBoxHead.Size = new System.Drawing.Size(254, 98);
            this.pictureBoxHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHead.TabIndex = 3;
            this.pictureBoxHead.TabStop = false;
            // 
            // pictureBoxGlaza
            // 
            this.pictureBoxGlaza.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGlaza.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBoxGlaza.Image = global::svin.Properties.Resources.s_glaza_idle_0;
            this.pictureBoxGlaza.Location = new System.Drawing.Point(429, 27);
            this.pictureBoxGlaza.Name = "pictureBoxGlaza";
            this.pictureBoxGlaza.Size = new System.Drawing.Size(84, 32);
            this.pictureBoxGlaza.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGlaza.TabIndex = 4;
            this.pictureBoxGlaza.TabStop = false;
            // 
            // pictureBox1StackCard
            // 
            this.pictureBox1StackCard.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1StackCard.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBox1StackCard.Image = global::svin.Properties.Resources.s_deck_0;
            this.pictureBox1StackCard.Location = new System.Drawing.Point(472, 210);
            this.pictureBox1StackCard.Name = "pictureBox1StackCard";
            this.pictureBox1StackCard.Size = new System.Drawing.Size(128, 78);
            this.pictureBox1StackCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1StackCard.TabIndex = 5;
            this.pictureBox1StackCard.TabStop = false;
            this.pictureBox1StackCard.Click += new System.EventHandler(this.pictureBox1StackCard_Click);
            // 
            // pictureBoxLeftSvinHand
            // 
            this.pictureBoxLeftSvinHand.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLeftSvinHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLeftSvinHand.Image = global::svin.Properties.Resources.s_left_have_0;
            this.pictureBoxLeftSvinHand.Location = new System.Drawing.Point(290, 105);
            this.pictureBoxLeftSvinHand.Name = "pictureBoxLeftSvinHand";
            this.pictureBoxLeftSvinHand.Size = new System.Drawing.Size(69, 41);
            this.pictureBoxLeftSvinHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeftSvinHand.TabIndex = 6;
            this.pictureBoxLeftSvinHand.TabStop = false;
            // 
            // pictureBoxRightSvinHand
            // 
            this.pictureBoxRightSvinHand.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxRightSvinHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxRightSvinHand.Image = global::svin.Properties.Resources.s_right_idle_0;
            this.pictureBoxRightSvinHand.Location = new System.Drawing.Point(576, 103);
            this.pictureBoxRightSvinHand.Name = "pictureBoxRightSvinHand";
            this.pictureBoxRightSvinHand.Size = new System.Drawing.Size(69, 41);
            this.pictureBoxRightSvinHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRightSvinHand.TabIndex = 7;
            this.pictureBoxRightSvinHand.TabStop = false;
            // 
            // pictureBoxPigSouvenir
            // 
            this.pictureBoxPigSouvenir.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPigSouvenir.Image = global::svin.Properties.Resources.s_suvenirs_1;
            this.pictureBoxPigSouvenir.Location = new System.Drawing.Point(325, 173);
            this.pictureBoxPigSouvenir.Name = "pictureBoxPigSouvenir";
            this.pictureBoxPigSouvenir.Size = new System.Drawing.Size(34, 27);
            this.pictureBoxPigSouvenir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPigSouvenir.TabIndex = 8;
            this.pictureBoxPigSouvenir.TabStop = false;
            this.pictureBoxPigSouvenir.Visible = false;
            // 
            // pictureBoxLeftSvinHand1
            // 
            this.pictureBoxLeftSvinHand1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLeftSvinHand1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLeftSvinHand1.Image = global::svin.Properties.Resources.s_left_have_2;
            this.pictureBoxLeftSvinHand1.Location = new System.Drawing.Point(254, 105);
            this.pictureBoxLeftSvinHand1.Name = "pictureBoxLeftSvinHand1";
            this.pictureBoxLeftSvinHand1.Size = new System.Drawing.Size(95, 66);
            this.pictureBoxLeftSvinHand1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeftSvinHand1.TabIndex = 9;
            this.pictureBoxLeftSvinHand1.TabStop = false;
            this.pictureBoxLeftSvinHand1.Visible = false;
            // 
            // pictureBoxLeftSvinHand2
            // 
            this.pictureBoxLeftSvinHand2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLeftSvinHand2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLeftSvinHand2.Image = global::svin.Properties.Resources.s_left_have_3;
            this.pictureBoxLeftSvinHand2.Location = new System.Drawing.Point(283, 105);
            this.pictureBoxLeftSvinHand2.Name = "pictureBoxLeftSvinHand2";
            this.pictureBoxLeftSvinHand2.Size = new System.Drawing.Size(91, 107);
            this.pictureBoxLeftSvinHand2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeftSvinHand2.TabIndex = 10;
            this.pictureBoxLeftSvinHand2.TabStop = false;
            this.pictureBoxLeftSvinHand2.Visible = false;
            // 
            // pictureBoxDrawArrow
            // 
            this.pictureBoxDrawArrow.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.pictureBoxDrawArrow.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxDrawArrow.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBoxDrawArrow.Image = global::svin.Properties.Resources.s_drop_0;
            this.pictureBoxDrawArrow.Location = new System.Drawing.Point(498, 151);
            this.pictureBoxDrawArrow.Name = "pictureBoxDrawArrow";
            this.pictureBoxDrawArrow.Size = new System.Drawing.Size(69, 73);
            this.pictureBoxDrawArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDrawArrow.TabIndex = 11;
            this.pictureBoxDrawArrow.TabStop = false;
            this.pictureBoxDrawArrow.Visible = false;
            // 
            // pictureBoxMyPit
            // 
            this.pictureBoxMyPit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxMyPit.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBoxMyPit.Image = global::svin.Properties.Resources.s_card_done_0;
            this.pictureBoxMyPit.Location = new System.Drawing.Point(669, 225);
            this.pictureBoxMyPit.Name = "pictureBoxMyPit";
            this.pictureBoxMyPit.Size = new System.Drawing.Size(134, 112);
            this.pictureBoxMyPit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMyPit.TabIndex = 12;
            this.pictureBoxMyPit.TabStop = false;
            this.pictureBoxMyPit.Visible = false;
            // 
            // pictureBoxSvinPit
            // 
            this.pictureBoxSvinPit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSvinPit.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pictureBoxSvinPit.Image = global::svin.Properties.Resources.s_card_done_0;
            this.pictureBoxSvinPit.Location = new System.Drawing.Point(625, 142);
            this.pictureBoxSvinPit.Name = "pictureBoxSvinPit";
            this.pictureBoxSvinPit.Size = new System.Drawing.Size(77, 66);
            this.pictureBoxSvinPit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSvinPit.TabIndex = 13;
            this.pictureBoxSvinPit.TabStop = false;
            this.pictureBoxSvinPit.Visible = false;
            // 
            // buttonSkip
            // 
            this.buttonSkip.BackColor = System.Drawing.Color.Transparent;
            this.buttonSkip.BackgroundImage = global::svin.Properties.Resources.s_triangle_0;
            this.buttonSkip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSkip.FlatAppearance.BorderSize = 0;
            this.buttonSkip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSkip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSkip.Location = new System.Drawing.Point(12, 12);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(92, 64);
            this.buttonSkip.TabIndex = 14;
            this.buttonSkip.UseVisualStyleBackColor = false;
            this.buttonSkip.Visible = false;
            this.buttonSkip.Click += new System.EventHandler(this.buttonSkip_Click);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(969, 516);
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.pictureBoxSvinPit);
            this.Controls.Add(this.pictureBoxMyPit);
            this.Controls.Add(this.pictureBoxDrawArrow);
            this.Controls.Add(this.pictureBoxLeftSvinHand2);
            this.Controls.Add(this.pictureBoxLeftSvinHand1);
            this.Controls.Add(this.pictureBoxPigSouvenir);
            this.Controls.Add(this.pictureBoxRightSvinHand);
            this.Controls.Add(this.pictureBoxLeftSvinHand);
            this.Controls.Add(this.pictureBox1StackCard);
            this.Controls.Add(this.pictureBoxGlaza);
            this.Controls.Add(this.pictureBoxHead);
            this.Controls.Add(this.pictureBoxHand);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.pictureBoxSouvenir);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.Name = "FormGame";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSouvenir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlaza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1StackCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightSvinHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPigSouvenir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftSvinHand2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDrawArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMyPit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSvinPit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.PictureBox pictureBoxHand;
        private System.Windows.Forms.PictureBox pictureBoxSouvenir;
        private System.Windows.Forms.PictureBox pictureBoxHead;
        private System.Windows.Forms.PictureBox pictureBoxGlaza;
        private System.Windows.Forms.PictureBox pictureBox1StackCard;
        private System.Windows.Forms.PictureBox pictureBoxLeftSvinHand;
        private System.Windows.Forms.PictureBox pictureBoxRightSvinHand;
        private System.Windows.Forms.PictureBox pictureBoxPigSouvenir;
        private System.Windows.Forms.PictureBox pictureBoxLeftSvinHand1;
        private System.Windows.Forms.PictureBox pictureBoxLeftSvinHand2;
        private System.Windows.Forms.PictureBox pictureBoxDrawArrow;
        private System.Windows.Forms.PictureBox pictureBoxMyPit;
        private System.Windows.Forms.PictureBox pictureBoxSvinPit;
        private System.Windows.Forms.Button buttonSkip;
    }
}

