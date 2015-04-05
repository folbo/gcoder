namespace gcoder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.resetButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.fileOpenedLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.autosizeButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dragRadioButton = new System.Windows.Forms.RadioButton();
            this.editRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.renderPanel = new gcoder.ScrollablePanel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(36, 263);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 6);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(67, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Open File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.fileOpenedLabel);
            this.panel1.Controls.Add(this.loadButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(738, 35);
            this.panel1.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(85, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // fileOpenedLabel
            // 
            this.fileOpenedLabel.AutoSize = true;
            this.fileOpenedLabel.Location = new System.Drawing.Point(319, 12);
            this.fileOpenedLabel.Name = "fileOpenedLabel";
            this.fileOpenedLabel.Size = new System.Drawing.Size(75, 13);
            this.fileOpenedLabel.TabIndex = 1;
            this.fileOpenedLabel.Text = "No file loaded.";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.autosizeButton);
            this.panel3.Controls.Add(this.runButton);
            this.panel3.Controls.Add(this.resetButton);
            this.panel3.Controls.Add(this.prevButton);
            this.panel3.Controls.Add(this.nextButton);
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 35);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(246, 481);
            this.panel3.TabIndex = 4;
            // 
            // autosizeButton
            // 
            this.autosizeButton.Location = new System.Drawing.Point(36, 369);
            this.autosizeButton.Name = "autosizeButton";
            this.autosizeButton.Size = new System.Drawing.Size(75, 23);
            this.autosizeButton.TabIndex = 4;
            this.autosizeButton.Text = "Adjust Size";
            this.autosizeButton.UseVisualStyleBackColor = true;
            this.autosizeButton.Click += new System.EventHandler(this.autosizeButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(36, 303);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(36, 225);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 2;
            this.prevButton.Text = "Previous";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(130, 225);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(234, 199);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.dragRadioButton);
            this.flowLayoutPanel2.Controls.Add(this.editRadioButton);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(34, 475);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // dragRadioButton
            // 
            this.dragRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.dragRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("dragRadioButton.Image")));
            this.dragRadioButton.Location = new System.Drawing.Point(3, 3);
            this.dragRadioButton.Name = "dragRadioButton";
            this.dragRadioButton.Size = new System.Drawing.Size(31, 33);
            this.dragRadioButton.TabIndex = 0;
            this.dragRadioButton.TabStop = true;
            this.dragRadioButton.UseVisualStyleBackColor = true;
            this.dragRadioButton.CheckedChanged += new System.EventHandler(this.dragRadioButton_CheckedChanged);
            // 
            // editRadioButton
            // 
            this.editRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.editRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("editRadioButton.Image")));
            this.editRadioButton.Location = new System.Drawing.Point(3, 42);
            this.editRadioButton.Name = "editRadioButton";
            this.editRadioButton.Size = new System.Drawing.Size(31, 35);
            this.editRadioButton.TabIndex = 1;
            this.editRadioButton.TabStop = true;
            this.editRadioButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.renderPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(247, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 481);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // renderPanel
            // 
            this.renderPanel.AutoScroll = true;
            this.renderPanel.BackColor = System.Drawing.Color.MintCream;
            this.renderPanel.Bitmap = null;
            this.renderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Location = new System.Drawing.Point(43, 3);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.renderPanel.Size = new System.Drawing.Size(445, 475);
            this.renderPanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 516);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label fileOpenedLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button autosizeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton dragRadioButton;
        private System.Windows.Forms.RadioButton editRadioButton;
        private ScrollablePanel renderPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

