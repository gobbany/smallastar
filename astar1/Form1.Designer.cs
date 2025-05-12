namespace astar1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.stepByStep = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.MazeBut = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlGrid.Location = new System.Drawing.Point(30, 31);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(803, 740);
            this.pnlGrid.TabIndex = 0;
            this.pnlGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGrid_Paint);
            this.pnlGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PnlGrid_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Red;
            this.btnStart.Location = new System.Drawing.Point(964, 56);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(166, 46);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start A*";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Red;
            this.btnClear.Location = new System.Drawing.Point(964, 121);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(166, 55);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Grid";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // stepByStep
            // 
            this.stepByStep.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.stepByStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepByStep.ForeColor = System.Drawing.Color.Red;
            this.stepByStep.Location = new System.Drawing.Point(936, 327);
            this.stepByStep.Name = "stepByStep";
            this.stepByStep.Size = new System.Drawing.Size(219, 47);
            this.stepByStep.TabIndex = 3;
            this.stepByStep.Text = "A* step by step";
            this.stepByStep.UseVisualStyleBackColor = false;
            this.stepByStep.Click += new System.EventHandler(this.stepByStep_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(907, 400);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 27);
            this.textBox1.TabIndex = 101;
            this.textBox1.Text = "Speed of stepping:";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MazeBut
            // 
            this.MazeBut.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MazeBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MazeBut.ForeColor = System.Drawing.Color.Red;
            this.MazeBut.Location = new System.Drawing.Point(960, 529);
            this.MazeBut.Name = "MazeBut";
            this.MazeBut.Size = new System.Drawing.Size(195, 72);
            this.MazeBut.TabIndex = 102;
            this.MazeBut.Text = "Maze Generation";
            this.MazeBut.UseVisualStyleBackColor = false;
            this.MazeBut.Click += new System.EventHandler(this.MazeGen_click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(936, 443);
            this.trackBar1.Maximum = 300;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(236, 56);
            this.trackBar1.TabIndex = 103;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1221, 1053);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.MazeBut);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stepByStep);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "A*";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button stepByStep;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button MazeBut;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

