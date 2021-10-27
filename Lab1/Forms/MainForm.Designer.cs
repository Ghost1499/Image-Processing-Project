namespace Lab1
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.locusRGBbutton = new System.Windows.Forms.Button();
            this.saveResultButton = new System.Windows.Forms.Button();
            this.saveSourceButton = new System.Windows.Forms.Button();
            this.correlationButton = new System.Windows.Forms.Button();
            this.wavePatternButton = new System.Windows.Forms.Button();
            this.convolutionButton = new System.Windows.Forms.Button();
            this.openSourceButton = new System.Windows.Forms.Button();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.locusRGBbutton);
            this.panel1.Controls.Add(this.saveResultButton);
            this.panel1.Controls.Add(this.saveSourceButton);
            this.panel1.Controls.Add(this.correlationButton);
            this.panel1.Controls.Add(this.wavePatternButton);
            this.panel1.Controls.Add(this.convolutionButton);
            this.panel1.Controls.Add(this.openSourceButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1139, 87);
            this.panel1.TabIndex = 0;
            // 
            // locusRGBbutton
            // 
            this.locusRGBbutton.Location = new System.Drawing.Point(357, 23);
            this.locusRGBbutton.Name = "locusRGBbutton";
            this.locusRGBbutton.Size = new System.Drawing.Size(108, 44);
            this.locusRGBbutton.TabIndex = 6;
            this.locusRGBbutton.Text = "LocusRGB";
            this.locusRGBbutton.UseVisualStyleBackColor = true;
            this.locusRGBbutton.Click += new System.EventHandler(this.LocusRGBbutton_Click);
            // 
            // saveResultButton
            // 
            this.saveResultButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveResultButton.Location = new System.Drawing.Point(1001, 23);
            this.saveResultButton.Name = "saveResultButton";
            this.saveResultButton.Size = new System.Drawing.Size(126, 41);
            this.saveResultButton.TabIndex = 5;
            this.saveResultButton.Text = "Save result";
            this.saveResultButton.UseVisualStyleBackColor = true;
            this.saveResultButton.Click += new System.EventHandler(this.saveResultButton_Click);
            // 
            // saveSourceButton
            // 
            this.saveSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSourceButton.Location = new System.Drawing.Point(869, 23);
            this.saveSourceButton.Name = "saveSourceButton";
            this.saveSourceButton.Size = new System.Drawing.Size(126, 41);
            this.saveSourceButton.TabIndex = 4;
            this.saveSourceButton.Text = "Save source";
            this.saveSourceButton.UseVisualStyleBackColor = true;
            this.saveSourceButton.Click += new System.EventHandler(this.saveSourceButton_Click);
            // 
            // correlationButton
            // 
            this.correlationButton.Location = new System.Drawing.Point(112, 23);
            this.correlationButton.Name = "correlationButton";
            this.correlationButton.Size = new System.Drawing.Size(110, 44);
            this.correlationButton.TabIndex = 3;
            this.correlationButton.Text = "Correlate";
            this.correlationButton.UseVisualStyleBackColor = true;
            this.correlationButton.Click += new System.EventHandler(this.correlationButton_Click);
            // 
            // wavePatternButton
            // 
            this.wavePatternButton.Location = new System.Drawing.Point(228, 23);
            this.wavePatternButton.Name = "wavePatternButton";
            this.wavePatternButton.Size = new System.Drawing.Size(123, 44);
            this.wavePatternButton.TabIndex = 2;
            this.wavePatternButton.Text = "Wave pattern";
            this.wavePatternButton.UseVisualStyleBackColor = true;
            this.wavePatternButton.Click += new System.EventHandler(this.wavePatternButton_Click);
            // 
            // convolutionButton
            // 
            this.convolutionButton.Location = new System.Drawing.Point(12, 22);
            this.convolutionButton.Name = "convolutionButton";
            this.convolutionButton.Size = new System.Drawing.Size(94, 45);
            this.convolutionButton.TabIndex = 1;
            this.convolutionButton.Text = "Convolute";
            this.convolutionButton.UseVisualStyleBackColor = true;
            this.convolutionButton.Click += new System.EventHandler(this.convolutionButton_Click);
            // 
            // openSourceButton
            // 
            this.openSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openSourceButton.Location = new System.Drawing.Point(723, 23);
            this.openSourceButton.Name = "openSourceButton";
            this.openSourceButton.Size = new System.Drawing.Size(140, 41);
            this.openSourceButton.TabIndex = 0;
            this.openSourceButton.Text = "Open source";
            this.openSourceButton.UseVisualStyleBackColor = true;
            this.openSourceButton.Click += new System.EventHandler(this.openSourceButton_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitContainer.Location = new System.Drawing.Point(12, 93);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.AutoScroll = true;
            this.mainSplitContainer.Panel1.Controls.Add(this.sourcePictureBox);
            this.mainSplitContainer.Panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mainSplitContainer_Scroll);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.AutoScroll = true;
            this.mainSplitContainer.Panel2.Controls.Add(this.resultPictureBox);
            this.mainSplitContainer.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mainSplitContainer_Scroll);
            this.mainSplitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.mainSplitContainer_Panel2_Paint);
            this.mainSplitContainer.Size = new System.Drawing.Size(1115, 345);
            this.mainSplitContainer.SplitterDistance = 555;
            this.mainSplitContainer.TabIndex = 1;
            this.mainSplitContainer.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mainSplitContainer_Scroll);
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(0, 0);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(100, 50);
            this.sourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.sourcePictureBox.TabIndex = 0;
            this.sourcePictureBox.TabStop = false;
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Location = new System.Drawing.Point(0, 0);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(100, 50);
            this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.resultPictureBox.TabIndex = 1;
            this.resultPictureBox.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Сохранить изображение";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 450);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Обработка изображений";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.PictureBox sourcePictureBox;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.Button openSourceButton;
        private System.Windows.Forms.Button convolutionButton;
        private System.Windows.Forms.Button wavePatternButton;
        private System.Windows.Forms.Button correlationButton;
        private System.Windows.Forms.Button saveSourceButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button saveResultButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button locusRGBbutton;
    }
}

