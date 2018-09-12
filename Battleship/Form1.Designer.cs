﻿namespace Battleship {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.StartButton = new System.Windows.Forms.Button();
			this.MyBox = new System.Windows.Forms.PictureBox();
			this.EnemyBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.MyBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EnemyBox)).BeginInit();
			this.SuspendLayout();
			// 
			// StartButton
			// 
			this.StartButton.Location = new System.Drawing.Point(349, 12);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(150, 23);
			this.StartButton.TabIndex = 0;
			this.StartButton.Text = "Start new game";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// MyBox
			// 
			this.MyBox.BackColor = System.Drawing.Color.Black;
			this.MyBox.Location = new System.Drawing.Point(388, 141);
			this.MyBox.Name = "MyBox";
			this.MyBox.Size = new System.Drawing.Size(440, 440);
			this.MyBox.TabIndex = 1;
			this.MyBox.TabStop = false;
			this.MyBox.Click += new System.EventHandler(this.MyBox_Click);
			// 
			// EnemyBox
			// 
			this.EnemyBox.BackColor = System.Drawing.Color.Black;
			this.EnemyBox.Location = new System.Drawing.Point(46, 73);
			this.EnemyBox.Name = "EnemyBox";
			this.EnemyBox.Size = new System.Drawing.Size(264, 264);
			this.EnemyBox.TabIndex = 2;
			this.EnemyBox.TabStop = false;
			this.EnemyBox.Click += new System.EventHandler(this.EnemyBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(43, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Enemy ships";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(385, 125);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "My ships";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 620);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.EnemyBox);
			this.Controls.Add(this.MyBox);
			this.Controls.Add(this.StartButton);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.MyBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EnemyBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.PictureBox MyBox;
		private System.Windows.Forms.PictureBox EnemyBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

