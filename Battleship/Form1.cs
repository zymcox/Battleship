using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship {
	public partial class Form1 : Form {

		// Skapa objekt
		Game g = new Game();
		
		public Form1() {
			InitializeComponent();
			g.InitGame(EnemyBox, MyBox);
			MyScore.Text = "0/26";
			EnScore.Text = "0/26";
			Winner.Text =  "";
		}

		private void StartButton_Click(object sender, EventArgs e) {
			g.InitGame(EnemyBox,MyBox);
			MyScore.Text = "0/26";
			EnScore.Text = "0/26";
			Winner.Text = "";
		}

		private void EnemyBox_Click(object sender, EventArgs e) {
			if (g.Winner1 == "") {
				MouseEventArgs me = (MouseEventArgs)e;
				g.Bomba(me.X, me.Y, EnemyBox, MyBox);
			}
			MyScore.Text = g.MyScore1.ToString() + "/26";
			EnScore.Text = g.EnScore1.ToString() + "/26";
			Winner.Text = g.Winner1;
		}

		private void MyBox_Click(object sender, EventArgs e) {

		}

		private void MyScore_TextChanged(object sender, EventArgs e) {

		}

		private void EnScore_TextChanged(object sender, EventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}
	}
}
