namespace DuConsole
{
	partial class MainForm
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnDoit = new DuLib.WinForm.SplitButton();
			this.ctxDoit = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miRun = new System.Windows.Forms.ToolStripMenuItem();
			this.miClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.lblAdmin = new System.Windows.Forms.Label();
			this.ctxDoit.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnDoit
			// 
			this.btnDoit.Location = new System.Drawing.Point(12, 12);
			this.btnDoit.Menu = this.ctxDoit;
			this.btnDoit.Name = "btnDoit";
			this.btnDoit.Size = new System.Drawing.Size(262, 34);
			this.btnDoit.TabIndex = 0;
			this.btnDoit.Text = "Do IT";
			this.btnDoit.UseVisualStyleBackColor = true;
			this.btnDoit.Click += new System.EventHandler(this.BtnDoit_Click);
			// 
			// ctxDoit
			// 
			this.ctxDoit.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ctxDoit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRun,
            this.miClose,
            this.toolStripSeparator1,
            this.miExit});
			this.ctxDoit.Name = "ctxDoit";
			this.ctxDoit.Size = new System.Drawing.Size(116, 82);
			// 
			// miRun
			// 
			this.miRun.Name = "miRun";
			this.miRun.Size = new System.Drawing.Size(115, 24);
			this.miRun.Text = "&Run";
			this.miRun.Click += new System.EventHandler(this.MiRun_Click);
			// 
			// miClose
			// 
			this.miClose.Name = "miClose";
			this.miClose.Size = new System.Drawing.Size(115, 24);
			this.miClose.Text = "&Close";
			this.miClose.Click += new System.EventHandler(this.MiClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(112, 6);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(115, 24);
			this.miExit.Text = "E&xit";
			this.miExit.Click += new System.EventHandler(this.MiExit_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.BackColor = System.Drawing.Color.FloralWhite;
			this.txtOutput.Location = new System.Drawing.Point(12, 52);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(701, 467);
			this.txtOutput.TabIndex = 1;
			this.txtOutput.Text = "";
			// 
			// lblAdmin
			// 
			this.lblAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAdmin.AutoSize = true;
			this.lblAdmin.Location = new System.Drawing.Point(663, 19);
			this.lblAdmin.Name = "lblAdmin";
			this.lblAdmin.Size = new System.Drawing.Size(54, 20);
			this.lblAdmin.TabIndex = 3;
			this.lblAdmin.Text = "@@@";
			this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(725, 531);
			this.Controls.Add(this.lblAdmin);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.btnDoit);
			this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainForm";
			this.Text = "DuConsole";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.ctxDoit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DuLib.WinForm.SplitButton btnDoit;
		private System.Windows.Forms.RichTextBox txtOutput;
		private System.Windows.Forms.ContextMenuStrip ctxDoit;
		private System.Windows.Forms.ToolStripMenuItem miRun;
		private System.Windows.Forms.ToolStripMenuItem miClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.Label lblAdmin;
	}
}

