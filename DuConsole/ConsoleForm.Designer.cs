namespace DuConsole
{
	partial class ConsoleForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleForm));
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.lblAdmin = new System.Windows.Forms.Label();
			this.TopPanel = new System.Windows.Forms.Panel();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.btnDoit = new Du.WinForms.SplitButton();
			this.ctxDoit = new Du.WinForms.BadakContextMenuStrip(this.components);
			this.miRun = new System.Windows.Forms.ToolStripMenuItem();
			this.miClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miRegisterExtension = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.TopPanel.SuspendLayout();
			this.ctxDoit.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.BackColor = System.Drawing.Color.FloralWhite;
			this.txtOutput.Location = new System.Drawing.Point(11, 87);
			this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(628, 334);
			this.txtOutput.TabIndex = 1;
			this.txtOutput.Text = "";
			// 
			// lblAdmin
			// 
			this.lblAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAdmin.AutoSize = true;
			this.lblAdmin.ForeColor = System.Drawing.Color.White;
			this.lblAdmin.Location = new System.Drawing.Point(2, 0);
			this.lblAdmin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblAdmin.Name = "lblAdmin";
			this.lblAdmin.Size = new System.Drawing.Size(53, 18);
			this.lblAdmin.TabIndex = 3;
			this.lblAdmin.Text = "@@@";
			this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// TopPanel
			// 
			this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.TopPanel.Controls.Add(this.SystemButton);
			this.TopPanel.Controls.Add(this.TitleLabel);
			this.TopPanel.Controls.Add(this.lblAdmin);
			this.TopPanel.Controls.Add(this.btnDoit);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Margin = new System.Windows.Forms.Padding(4);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(650, 80);
			this.TopPanel.TabIndex = 2;
			this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(500, 0);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = true;
			this.SystemButton.ShowMinimize = true;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 1;
			this.SystemButton.CloseOrder += new System.EventHandler(this.SystemButton_CloseOrder);
			// 
			// TitleLabel
			// 
			this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(10, 27);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(455, 40);
			this.TitleLabel.TabIndex = 0;
			this.TitleLabel.Text = "이름!";
			this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// btnDoit
			// 
			this.btnDoit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDoit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.btnDoit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnDoit.ForeColor = System.Drawing.Color.White;
			this.btnDoit.Location = new System.Drawing.Point(489, 30);
			this.btnDoit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.btnDoit.Menu = this.ctxDoit;
			this.btnDoit.Name = "btnDoit";
			this.btnDoit.Size = new System.Drawing.Size(159, 47);
			this.btnDoit.TabIndex = 0;
			this.btnDoit.Text = "합시다";
			this.btnDoit.UseVisualStyleBackColor = false;
			this.btnDoit.Click += new System.EventHandler(this.BtnDoit_Click);
			// 
			// ctxDoit
			// 
			this.ctxDoit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.ctxDoit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRun,
            this.miClose,
            this.toolStripSeparator1,
            this.miRegisterExtension,
            this.toolStripSeparator2,
            this.miExit});
			this.ctxDoit.Name = "ctxDoit";
			this.ctxDoit.Size = new System.Drawing.Size(199, 104);
			// 
			// miRun
			// 
			this.miRun.ForeColor = System.Drawing.Color.White;
			this.miRun.Name = "miRun";
			this.miRun.Size = new System.Drawing.Size(198, 22);
			this.miRun.Text = "실행(&R)";
			this.miRun.Click += new System.EventHandler(this.MiRun_Click);
			// 
			// miClose
			// 
			this.miClose.ForeColor = System.Drawing.Color.White;
			this.miClose.Name = "miClose";
			this.miClose.Size = new System.Drawing.Size(198, 22);
			this.miClose.Text = "닫기(&C)";
			this.miClose.Click += new System.EventHandler(this.MiClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
			// 
			// miRegisterExtension
			// 
			this.miRegisterExtension.ForeColor = System.Drawing.Color.White;
			this.miRegisterExtension.Name = "miRegisterExtension";
			this.miRegisterExtension.Size = new System.Drawing.Size(198, 22);
			this.miRegisterExtension.Text = "Register extension";
			this.miRegisterExtension.Click += new System.EventHandler(this.MiRegisterExtension_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
			// 
			// miExit
			// 
			this.miExit.ForeColor = System.Drawing.Color.White;
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(198, 22);
			this.miExit.Text = "끝내기(&X)";
			this.miExit.Click += new System.EventHandler(this.MiExit_Click);
			// 
			// ConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(650, 433);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.txtOutput);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.MinimumSize = new System.Drawing.Size(343, 433);
			this.Name = "ConsoleForm";
			this.Text = "DuConsole";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConsoleForm_FormClosed);
			this.Load += new System.EventHandler(this.ConsoleForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsoleForm_KeyDown);
			this.TopPanel.ResumeLayout(false);
			this.TopPanel.PerformLayout();
			this.ctxDoit.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Du.WinForms.SplitButton btnDoit;
		private System.Windows.Forms.RichTextBox txtOutput;
		private Du.WinForms.BadakContextMenuStrip ctxDoit;
		private System.Windows.Forms.ToolStripMenuItem miRun;
		private System.Windows.Forms.ToolStripMenuItem miClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.Label lblAdmin;
		private System.Windows.Forms.ToolStripMenuItem miRegisterExtension;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.Label TitleLabel;
		private Du.WinForms.BadakSystemButton SystemButton;
	}
}