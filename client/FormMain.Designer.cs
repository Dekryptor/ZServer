namespace client
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tb_chat_history = new System.Windows.Forms.TextBox();
			this.tb_chat_message = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ss_main = new System.Windows.Forms.StatusStrip();
			this.tss_status_label = new System.Windows.Forms.ToolStripStatusLabel();
			this.tss_status = new System.Windows.Forms.ToolStripStatusLabel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.ss_main.SuspendLayout();
			this.SuspendLayout();
			// 
			// tb_chat_history
			// 
			this.tb_chat_history.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_chat_history.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_chat_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tb_chat_history.Location = new System.Drawing.Point(21, 19);
			this.tb_chat_history.Margin = new System.Windows.Forms.Padding(5);
			this.tb_chat_history.MaxLength = 0;
			this.tb_chat_history.Multiline = true;
			this.tb_chat_history.Name = "tb_chat_history";
			this.tb_chat_history.ReadOnly = true;
			this.tb_chat_history.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_chat_history.Size = new System.Drawing.Size(1214, 502);
			this.tb_chat_history.TabIndex = 0;
			this.tb_chat_history.TabStop = false;
			// 
			// tb_chat_message
			// 
			this.tb_chat_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_chat_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tb_chat_message.Location = new System.Drawing.Point(106, 565);
			this.tb_chat_message.Margin = new System.Windows.Forms.Padding(5);
			this.tb_chat_message.Name = "tb_chat_message";
			this.tb_chat_message.Size = new System.Drawing.Size(1138, 39);
			this.tb_chat_message.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 568);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Chat:";
			// 
			// ss_main
			// 
			this.ss_main.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ss_main.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ss_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tss_status_label,
            this.tss_status});
			this.ss_main.Location = new System.Drawing.Point(0, 631);
			this.ss_main.Name = "ss_main";
			this.ss_main.Size = new System.Drawing.Size(1258, 33);
			this.ss_main.TabIndex = 3;
			this.ss_main.Text = "statusStrip1";
			// 
			// tss_status_label
			// 
			this.tss_status_label.Name = "tss_status_label";
			this.tss_status_label.Size = new System.Drawing.Size(74, 28);
			this.tss_status_label.Text = "Status: ";
			// 
			// tss_status
			// 
			this.tss_status.Name = "tss_status";
			this.tss_status.Size = new System.Drawing.Size(0, 28);
			this.tss_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 250;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 32F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1258, 664);
			this.Controls.Add(this.ss_main);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tb_chat_message);
			this.Controls.Add(this.tb_chat_history);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MinimumSize = new System.Drawing.Size(1280, 720);
			this.Name = "FormMain";
			this.Text = "Client";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.ss_main.ResumeLayout(false);
			this.ss_main.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tb_chat_history;
		private System.Windows.Forms.TextBox tb_chat_message;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip ss_main;
		private System.Windows.Forms.ToolStripStatusLabel tss_status_label;
		private System.Windows.Forms.ToolStripStatusLabel tss_status;
		private System.Windows.Forms.Timer timer1;
	}
}

