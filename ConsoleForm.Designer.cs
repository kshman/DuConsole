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
			this.btnDoit = new Du.WinForms.SplitButton();
			this.ctxDoit = new Du.WinForms.DarkContextMenuStrip(this.components);
			this.miRun = new System.Windows.Forms.ToolStripMenuItem();
			this.miClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miRegisterExtension = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.lblAdmin = new System.Windows.Forms.Label();
			this.BadakTopPanel.SuspendLayout();
			this.ctxDoit.SuspendLayout();
			this.SuspendLayout();
			// 
			// BadakTopPanel
			// 
			this.BadakTopPanel.Controls.Add(this.lblAdmin);
			this.BadakTopPanel.Controls.Add(this.btnDoit);
			this.BadakTopPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BadakTopPanel.Size = new System.Drawing.Size(714, 90);
			this.BadakTopPanel.Controls.SetChildIndex(this.BadakMinMaxClosePanel, 0);
			this.BadakTopPanel.Controls.SetChildIndex(this.btnDoit, 0);
			this.BadakTopPanel.Controls.SetChildIndex(this.lblAdmin, 0);
			// 
			// BadakMinMaxClosePanel
			// 
			this.BadakMinMaxClosePanel.Location = new System.Drawing.Point(589, 0);
			// 
			// btnDoit
			// 
			this.btnDoit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDoit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.btnDoit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnDoit.ForeColor = System.Drawing.Color.White;
			this.btnDoit.Location = new System.Drawing.Point(423, 2);
			this.btnDoit.Margin = new System.Windows.Forms.Padding(2);
			this.btnDoit.Menu = this.ctxDoit;
			this.btnDoit.Name = "btnDoit";
			this.btnDoit.Size = new System.Drawing.Size(123, 28);
			this.btnDoit.TabIndex = 0;
			this.btnDoit.Text = "Do IT";
			this.btnDoit.UseVisualStyleBackColor = false;
			this.btnDoit.Click += new System.EventHandler(this.BtnDoit_Click);
			// 
			// ctxDoit
			// 
			this.ctxDoit.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ctxDoit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRun,
            this.miClose,
            this.toolStripSeparator1,
            this.miRegisterExtension,
            this.toolStripSeparator2,
            this.miExit});
			this.ctxDoit.Name = "ctxDoit";
			this.ctxDoit.Size = new System.Drawing.Size(203, 112);
			// 
			// miRun
			// 
			this.miRun.ForeColor = System.Drawing.Color.White;
			this.miRun.Name = "miRun";
			this.miRun.Size = new System.Drawing.Size(202, 24);
			this.miRun.Text = "&Run";
			this.miRun.Click += new System.EventHandler(this.MiRun_Click);
			// 
			// miClose
			// 
			this.miClose.ForeColor = System.Drawing.Color.White;
			this.miClose.Name = "miClose";
			this.miClose.Size = new System.Drawing.Size(202, 24);
			this.miClose.Text = "&Close";
			this.miClose.Click += new System.EventHandler(this.MiClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
			// 
			// miRegisterExtension
			// 
			this.miRegisterExtension.ForeColor = System.Drawing.Color.White;
			this.miRegisterExtension.Name = "miRegisterExtension";
			this.miRegisterExtension.Size = new System.Drawing.Size(202, 24);
			this.miRegisterExtension.Text = "Register extension";
			this.miRegisterExtension.Click += new System.EventHandler(this.MiRegisterExtension_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
			// 
			// miExit
			// 
			this.miExit.ForeColor = System.Drawing.Color.White;
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(202, 24);
			this.miExit.Text = "E&xit";
			this.miExit.Click += new System.EventHandler(this.MiExit_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.BackColor = System.Drawing.Color.FloralWhite;
			this.txtOutput.Location = new System.Drawing.Point(11, 95);
			this.txtOutput.Margin = new System.Windows.Forms.Padding(2);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(692, 614);
			this.txtOutput.TabIndex = 1;
			this.txtOutput.Text = "";
			// 
			// lblAdmin
			// 
			this.lblAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAdmin.AutoSize = true;
			this.lblAdmin.ForeColor = System.Drawing.Color.White;
			this.lblAdmin.Location = new System.Drawing.Point(669, 71);
			this.lblAdmin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblAdmin.Name = "lblAdmin";
			this.lblAdmin.Size = new System.Drawing.Size(43, 15);
			this.lblAdmin.TabIndex = 3;
			this.lblAdmin.Text = "@@@";
			this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(714, 724);
			this.Controls.Add(this.txtOutput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(400, 390);
			this.Name = "ConsoleForm";
			this.Text = "DuConsole";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConsoleForm_FormClosed);
			this.Load += new System.EventHandler(this.ConsoleForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsoleForm_KeyDown);
			this.Controls.SetChildIndex(this.BadakTopPanel, 0);
			this.Controls.SetChildIndex(this.txtOutput, 0);
			this.BadakTopPanel.ResumeLayout(false);
			this.BadakTopPanel.PerformLayout();
			this.ctxDoit.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Du.WinForms.SplitButton btnDoit;
		private System.Windows.Forms.RichTextBox txtOutput;
		private Du.WinForms.DarkContextMenuStrip ctxDoit;
		private System.Windows.Forms.ToolStripMenuItem miRun;
		private System.Windows.Forms.ToolStripMenuItem miClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.Label lblAdmin;
		private System.Windows.Forms.ToolStripMenuItem miRegisterExtension;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}