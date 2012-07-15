namespace SignalAnalyzer2
{
   partial class SygnalAnalyzerForm
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
          this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
          this.StartCaptureBtn = new System.Windows.Forms.Button();
          this.SpectrumZGraphCtrl = new ZedGraph.ZedGraphControl();
          this.InputZGraphCtrl = new ZedGraph.ZedGraphControl();
          this.StopBtn = new System.Windows.Forms.Button();
          this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
          this.menuStrip1 = new System.Windows.Forms.MenuStrip();
          this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.selectInputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.tableLayoutPanel1.SuspendLayout();
          this.tableLayoutPanel2.SuspendLayout();
          this.menuStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 1;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
          this.tableLayoutPanel1.Controls.Add(this.InputZGraphCtrl, 0, 0);
          this.tableLayoutPanel1.Controls.Add(this.SpectrumZGraphCtrl, 0, 1);
          this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
          this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
          this.tableLayoutPanel1.Name = "tableLayoutPanel1";
          this.tableLayoutPanel1.RowCount = 3;
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.09381F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.90619F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
          this.tableLayoutPanel1.Size = new System.Drawing.Size(1017, 514);
          this.tableLayoutPanel1.TabIndex = 0;
          // 
          // StartCaptureBtn
          // 
          this.StartCaptureBtn.Location = new System.Drawing.Point(3, 3);
          this.StartCaptureBtn.Name = "StartCaptureBtn";
          this.StartCaptureBtn.Size = new System.Drawing.Size(75, 23);
          this.StartCaptureBtn.TabIndex = 0;
          this.StartCaptureBtn.Text = "Capture";
          this.StartCaptureBtn.UseVisualStyleBackColor = true;
          this.StartCaptureBtn.Click += new System.EventHandler(this.StartCaptureBtn_Click);
          // 
          // SpectrumZGraphCtrl
          // 
          this.SpectrumZGraphCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
          this.SpectrumZGraphCtrl.Location = new System.Drawing.Point(3, 256);
          this.SpectrumZGraphCtrl.Name = "SpectrumZGraphCtrl";
          this.SpectrumZGraphCtrl.ScrollGrace = 0;
          this.SpectrumZGraphCtrl.ScrollMaxX = 0;
          this.SpectrumZGraphCtrl.ScrollMaxY = 0;
          this.SpectrumZGraphCtrl.ScrollMaxY2 = 0;
          this.SpectrumZGraphCtrl.ScrollMinX = 0;
          this.SpectrumZGraphCtrl.ScrollMinY = 0;
          this.SpectrumZGraphCtrl.ScrollMinY2 = 0;
          this.SpectrumZGraphCtrl.Size = new System.Drawing.Size(1011, 218);
          this.SpectrumZGraphCtrl.TabIndex = 0;
          // 
          // InputZGraphCtrl
          // 
          this.InputZGraphCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
          this.InputZGraphCtrl.Location = new System.Drawing.Point(3, 3);
          this.InputZGraphCtrl.Name = "InputZGraphCtrl";
          this.InputZGraphCtrl.ScrollGrace = 0;
          this.InputZGraphCtrl.ScrollMaxX = 0;
          this.InputZGraphCtrl.ScrollMaxY = 0;
          this.InputZGraphCtrl.ScrollMaxY2 = 0;
          this.InputZGraphCtrl.ScrollMinX = 0;
          this.InputZGraphCtrl.ScrollMinY = 0;
          this.InputZGraphCtrl.ScrollMinY2 = 0;
          this.InputZGraphCtrl.Size = new System.Drawing.Size(1011, 247);
          this.InputZGraphCtrl.TabIndex = 0;
          // 
          // StopBtn
          // 
          this.StopBtn.Location = new System.Drawing.Point(103, 3);
          this.StopBtn.Name = "StopBtn";
          this.StopBtn.Size = new System.Drawing.Size(75, 23);
          this.StopBtn.TabIndex = 1;
          this.StopBtn.Text = "Stop";
          this.StopBtn.UseVisualStyleBackColor = true;
          this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
          // 
          // tableLayoutPanel2
          // 
          this.tableLayoutPanel2.ColumnCount = 2;
          this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.tableLayoutPanel2.Controls.Add(this.StartCaptureBtn, 0, 0);
          this.tableLayoutPanel2.Controls.Add(this.StopBtn, 1, 0);
          this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 480);
          this.tableLayoutPanel2.Name = "tableLayoutPanel2";
          this.tableLayoutPanel2.RowCount = 1;
          this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
          this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 31);
          this.tableLayoutPanel2.TabIndex = 2;
          // 
          // menuStrip1
          // 
          this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deviceToolStripMenuItem});
          this.menuStrip1.Location = new System.Drawing.Point(0, 0);
          this.menuStrip1.Name = "menuStrip1";
          this.menuStrip1.Size = new System.Drawing.Size(1017, 24);
          this.menuStrip1.TabIndex = 1;
          this.menuStrip1.Text = "menuStrip1";
          // 
          // fileToolStripMenuItem
          // 
          this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
          this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
          this.fileToolStripMenuItem.Text = "File";
          // 
          // deviceToolStripMenuItem
          // 
          this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectInputDeviceToolStripMenuItem});
          this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
          this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
          this.deviceToolStripMenuItem.Text = "Device";
          // 
          // selectInputDeviceToolStripMenuItem
          // 
          this.selectInputDeviceToolStripMenuItem.Name = "selectInputDeviceToolStripMenuItem";
          this.selectInputDeviceToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
          this.selectInputDeviceToolStripMenuItem.Text = "Select input device";
          // 
          // SygnalAnalyzerForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(1017, 538);
          this.Controls.Add(this.tableLayoutPanel1);
          this.Controls.Add(this.menuStrip1);
          this.MainMenuStrip = this.menuStrip1;
          this.Name = "SygnalAnalyzerForm";
          this.Text = "Simple signal analyzer";
          this.tableLayoutPanel1.ResumeLayout(false);
          this.tableLayoutPanel2.ResumeLayout(false);
          this.menuStrip1.ResumeLayout(false);
          this.menuStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private ZedGraph.ZedGraphControl InputZGraphCtrl;
      private ZedGraph.ZedGraphControl SpectrumZGraphCtrl;
      private System.Windows.Forms.Button StartCaptureBtn;
      private System.Windows.Forms.Button StopBtn;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem selectInputDeviceToolStripMenuItem;

   }
}

