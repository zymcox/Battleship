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
			
		}

		private void StartButton_Click(object sender, EventArgs e) {
			g.InitGame(EnemyBox,MyBox);
		}

		private void EnemyBox_Click(object sender, EventArgs e) {

		}

		private void MyBox_Click(object sender, EventArgs e) {

		}
	}
}
