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
         this.tableLayoutPanel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 2;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.45377F));
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.54623F));
         this.tableLayoutPanel1.Controls.Add(this.StartCaptureBtn, 0, 1);
         this.tableLayoutPanel1.Controls.Add(this.SpectrumZGraphCtrl, 1, 0);
         this.tableLayoutPanel1.Controls.Add(this.InputZGraphCtrl, 0, 0);
         this.tableLayoutPanel1.Controls.Add(this.StopBtn, 1, 1);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 2;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.03797F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.962025F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(941, 474);
         this.tableLayoutPanel1.TabIndex = 0;
         // 
         // StartCaptureBtn
         // 
         this.StartCaptureBtn.Location = new System.Drawing.Point(3, 443);
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
         this.SpectrumZGraphCtrl.Location = new System.Drawing.Point(505, 3);
         this.SpectrumZGraphCtrl.Name = "SpectrumZGraphCtrl";
         this.SpectrumZGraphCtrl.ScrollGrace = 0;
         this.SpectrumZGraphCtrl.ScrollMaxX = 0;
         this.SpectrumZGraphCtrl.ScrollMaxY = 0;
         this.SpectrumZGraphCtrl.ScrollMaxY2 = 0;
         this.SpectrumZGraphCtrl.ScrollMinX = 0;
         this.SpectrumZGraphCtrl.ScrollMinY = 0;
         this.SpectrumZGraphCtrl.ScrollMinY2 = 0;
         this.SpectrumZGraphCtrl.Size = new System.Drawing.Size(433, 434);
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
         this.InputZGraphCtrl.Size = new System.Drawing.Size(496, 434);
         this.InputZGraphCtrl.TabIndex = 0;
         // 
         // StopBtn
         // 
         this.StopBtn.Location = new System.Drawing.Point(505, 443);
         this.StopBtn.Name = "StopBtn";
         this.StopBtn.Size = new System.Drawing.Size(75, 23);
         this.StopBtn.TabIndex = 1;
         this.StopBtn.Text = "Stop";
         this.StopBtn.UseVisualStyleBackColor = true;
         this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
         // 
         // SygnalAnalyzerForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(941, 474);
         this.Controls.Add(this.tableLayoutPanel1);
         this.Name = "SygnalAnalyzerForm";
         this.Text = "Form1";
         this.tableLayoutPanel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private ZedGraph.ZedGraphControl InputZGraphCtrl;
      private ZedGraph.ZedGraphControl SpectrumZGraphCtrl;
      private System.Windows.Forms.Button StartCaptureBtn;
      private System.Windows.Forms.Button StopBtn;

   }
}

