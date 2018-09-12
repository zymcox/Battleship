using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship {
	class Game {
		private int GameXsize = 10;
		private int GameYsize = 10;

		private float MySquareX;
		private float MySquareY;
		private float EnSquareX;
		private float EnSquareY;

		private int[] MyShips;
		private int[] EnShips;

		// Number of ships
		private int Huge = 1;
		private int Big = 2;
		private int Medium = 2;
		private int Small = 3;

		// Ship size
		private int HugeS = 6;
		private int BigS = 4;
		private int MediumS = 3;
		private int SmallS = 2;

		private void DrawGrid(PictureBox MyPic, PictureBox EnemyPic) {

			Bitmap DrawArea;
			DrawArea = new Bitmap(MyPic.Size.Width, MyPic.Size.Height);
			MyPic.Image = DrawArea;
			DrawArea = new Bitmap(EnemyPic.Size.Width, EnemyPic.Size.Height);
			EnemyPic.Image = DrawArea;

			int mx = MyPic.Image.Width;
			int my = MyPic.Image.Height;
			int ex = EnemyPic.Image.Width;
			int ey = EnemyPic.Image.Height;

			MySquareX = mx / (float)(GameXsize + 1);
			MySquareY = my / (float)(GameYsize + 1);
			EnSquareX = ex / (float)(GameXsize + 1);
			EnSquareY = ey / (float)(GameYsize + 1);

			Graphics gm, ge;
			gm = Graphics.FromImage(MyPic.Image);
			ge = Graphics.FromImage(EnemyPic.Image);
			Pen PenWhite = new Pen(Brushes.White);
			Font f = new Font("Tahoma", 11);
			SolidBrush b = new SolidBrush(Color.White);
			SolidBrush bluebrush = new SolidBrush(Color.Blue);						// Ships
			SolidBrush redbrush = new SolidBrush(Color.Red);						// Miss
			SolidBrush greenbrush = new SolidBrush(Color.Green);					// Hit
			SolidBrush orangebrush = new SolidBrush(Color.Orange);					// enemy miss
			SolidBrush greenyellwbrush = new SolidBrush(Color.GreenYellow);			// enemy hit

			//Rita ut skepp, träffar, missar och annat
			for (int i=0; i < GameXsize * GameYsize; i++) {

			}




			//Rita spelplanen
			string[] labX = { " ", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
			string[] labY = { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };

			//Lodrät
			for (int i = 0; i < GameXsize + 1; i++) {
				gm.DrawString(labX[i], f, b, new Rectangle((int)(i * MySquareX), 0, (int)(i * MySquareX + MySquareX), (int)MySquareY));
				ge.DrawString(labX[i], f, b, new Rectangle((int)(i * EnSquareX), 0, (int)(i * EnSquareX + EnSquareX), (int)EnSquareY));
				gm.DrawLine(PenWhite, i * mx / (GameXsize + 1), 0, i * mx / (GameXsize + 1), my);
				ge.DrawLine(PenWhite, i * ex / (GameXsize + 1), 0, i * ex / (GameXsize + 1), ey);
			}

			//Vågrät
			for (int i = 0; i < GameYsize + 1; i++) {
				gm.DrawString(labY[i], f, b, new Rectangle(0, (int)(i * MySquareY), (int)MySquareX, (int)(i * MySquareY + MySquareY)));
				ge.DrawString(labY[i], f, b, new Rectangle(0, (int)(i * EnSquareY), (int)EnSquareX, (int)(i * EnSquareY + EnSquareY)));
				gm.DrawLine(PenWhite, 0, i * my / (GameYsize + 1), mx, i * my / (GameYsize + 1));
				ge.DrawLine(PenWhite, 0, i * ey / (GameYsize + 1), ex, i * ey / (GameYsize + 1));
			}
		}



		private void RandomizeShips() {
			int Orientation;
			bool TryAgain;

			MyShips = new int[GameXsize * GameYsize];
			EnShips = new int[GameXsize * GameYsize];
			Random rnd = new Random();

			TryAgain = true;
			while (TryAgain) {
				TryAgain = false;

				int StartX;
				int StartY;

				Orientation = (rnd.Next(1) == 0 ? 1 : GameXsize);
				if (Orientation == GameXsize) {
					StartX = rnd.Next(GameXsize - HugeS);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - HugeS);
				}
				for (int i = 0; i < HugeS; i++) {
					int ii = i * Orientation;
					if (MyShips[StartX + ii + StartY * StartX + ii] == 0) {
						MyShips[StartX + ii + StartY * StartX + ii] = 1;
					} else {
						TryAgain = true;
					}

				}

			}

		}

		public void InitGame(PictureBox MyPic, PictureBox EnemyPic) {
			RandomizeShips();
			DrawGrid(MyPic, EnemyPic);

		}

	}
}
