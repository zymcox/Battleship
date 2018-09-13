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

		private int MyScore;
		private int EnScore;
		private int MaxScore = 26;
		private string Winner = "";

		// Ship size
		private int HugeS = 6;      //1
		private int BigS = 4;       //2
		private int MediumS = 3;    //2
		private int SmallS = 2;     //3

		// Computer player
		private int HitX = -1;
		private int HitY = -1;
		private int Direction = 0;
		private int FromFirstHit = 0;

		public int MyScore1 {
			get {
				return MyScore;
			}

			set {
				MyScore = value;
			}
		}

		public int EnScore1 {
			get {
				return EnScore;
			}

			set {
				EnScore = value;
			}
		}

		public string Winner1 {
			get {
				return Winner;
			}

			set {
				Winner = value;
			}
		}

		private void DrawGrid(PictureBox EnemyPic, PictureBox MyPic) {

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
			SolidBrush bluebrush = new SolidBrush(Color.Blue);                      // Ships
			SolidBrush redbrush = new SolidBrush(Color.Red);                        // Miss
			SolidBrush greenbrush = new SolidBrush(Color.Green);                    // Hit

			//Rita ut skepp, träffar, missar och annat
			for (int y = 0; y < GameYsize; y++) {
				for (int x = 0; x < GameXsize; x++) {
					int i = x + y * GameXsize;
					int xx = (int)(x * MySquareX + MySquareX);
					int yy = (int)(y * MySquareY + MySquareY);
					int X1 = (int)(MySquareX);
					int Y1 = (int)(MySquareY);
					if (MyShips[i] == 1) {
						gm.FillRectangle(bluebrush, new Rectangle(xx, yy, X1, Y1));
					} else if (MyShips[i] == 2) {
						gm.FillEllipse(redbrush, new Rectangle(xx + 1, yy + 1, X1 - 2, Y1 - 2));
					} else if (MyShips[i] == 3) {
						gm.FillRectangle(bluebrush, new Rectangle(xx, yy, X1, Y1));
						gm.FillEllipse(greenbrush, new Rectangle(xx + 1, yy + 1, X1 - 2, Y1 - 2));
					}
					xx = (int)(x * EnSquareX + EnSquareX);
					yy = (int)(y * EnSquareY + EnSquareY);
					X1 = (int)(EnSquareX);
					Y1 = (int)(EnSquareY);
					if (EnShips[i] == 2) {
						ge.FillEllipse(redbrush, new Rectangle(xx + 1, yy + 1, X1 - 2, Y1 - 2));
					} else if (EnShips[i] == 3) {
						ge.FillEllipse(greenbrush, new Rectangle(xx + 1, yy + 1, X1 - 2, Y1 - 2));
					}
				}
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
			EnScore = 0;
			MyScore = 0;
			Winner = "";
			MyShips = new int[GameXsize * GameYsize];
			EnShips = new int[GameXsize * GameYsize];
			Random rnd = new Random();

			// Mina skepp
			TryAgain = true;
			while (TryAgain) {
				TryAgain = false;

				int StartX;
				int StartY;

				Array.Clear(MyShips, 0, MyShips.Length);

				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - HugeS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - HugeS + 1);
				}
				for (int i = 0; i < HugeS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - BigS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - BigS + 1);
				}
				for (int i = 0; i < BigS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - BigS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - BigS + 1);
				}
				for (int i = 0; i < BigS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - MediumS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - MediumS + 1);
				}
				for (int i = 0; i < MediumS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - MediumS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - MediumS + 1);
				}
				for (int i = 0; i < MediumS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (MyShips[ii + StartY * GameXsize + StartX] == 0) {
						MyShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
			}

			// Enemy ships
			TryAgain = true;
			while (TryAgain) {
				TryAgain = false;

				int StartX;
				int StartY;

				Array.Clear(EnShips, 0, EnShips.Length);

				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - HugeS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - HugeS + 1);
				}
				for (int i = 0; i < HugeS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - BigS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - BigS + 1);
				}
				for (int i = 0; i < BigS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - BigS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - BigS + 1);
				}
				for (int i = 0; i < BigS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - MediumS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - MediumS + 1);
				}
				for (int i = 0; i < MediumS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - MediumS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - MediumS + 1);
				}
				for (int i = 0; i < MediumS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
				Orientation = (rnd.Next(2) == 0 ? 1 : GameXsize);
				if (Orientation == 1) {
					StartX = rnd.Next(GameXsize - SmallS + 1);
					StartY = rnd.Next(GameYsize);
				} else {
					StartX = rnd.Next(GameXsize);
					StartY = rnd.Next(GameYsize - SmallS + 1);
				}
				for (int i = 0; i < SmallS; i++) {
					int ii = i * Orientation;
					if (EnShips[ii + StartY * GameXsize + StartX] == 0) {
						EnShips[ii + StartY * GameXsize + StartX] = 1;
					} else {
						TryAgain = true;
					}
				}
			}

		}

		public void InitGame(PictureBox MyPic, PictureBox EnemyPic) {
			Direction = 0;
			FromFirstHit = 0;
			HitX = -1;
			HitY = -1;
			RandomizeShips();
			DrawGrid(MyPic, EnemyPic);

		}

		public void Bomba(int x, int y, PictureBox MyPic, PictureBox EnemyPic) {
			if (x - EnSquareX < 0 || y - EnSquareY < 0) return;

			int SquareX = (int)(x / EnSquareX) - 1;
			int SquareY = (int)(y / EnSquareY) - 1;
			if (EnShips[GameXsize * SquareY + SquareX] == 0) {
				EnShips[GameXsize * SquareY + SquareX] = 2; // Miss 
			} else if (EnShips[GameXsize * SquareY + SquareX] == 1) {
				EnShips[GameXsize * SquareY + SquareX] = 3; // Träff
				MyScore++;
				if (MyScore == MaxScore) Winner = "-- Me --";
			} else if (EnShips[GameXsize * SquareY + SquareX] == 2 || EnShips[GameXsize * SquareY + SquareX] == 3) {
				return;
			} else {
				return;
			}
			DrawGrid(MyPic, EnemyPic);
			ComputerPlayer();
			DrawGrid(MyPic, EnemyPic);
		}

		private void KassComputerPlayer() {
			int BombX = 0;
			int BombY = 0;
			bool TryAgain = true;

			while (TryAgain) {
				TryAgain = false;
				Random rnd = new Random();
				if (HitX == -1) {
					BombX = rnd.Next(GameXsize);
					BombY = rnd.Next(GameYsize);
				} else {

					// Öster, Väster, Norr, Söder 0,1,2,3 
					if (Direction == 0) {
						BombX = HitX + FromFirstHit;
						BombY = HitY;
						if (BombX >= GameXsize) {
							Direction = 0;
							FromFirstHit = 0;
							HitX = -1;
							HitY = -1;
							TryAgain = true;
						}
					} else if (Direction == 1) {
						BombX = HitX - FromFirstHit;
						BombY = HitY;
						if (BombX < 0) {
							Direction = 0;
							FromFirstHit = 0;
							HitX = -1;
							HitY = -1;
							TryAgain = true;
						}
					} else if (Direction == 2) {
						BombX = HitX;
						BombY = HitY + FromFirstHit;
						if (BombY >= GameYsize) {
							Direction = 0;
							FromFirstHit = 0;
							HitX = -1;
							HitY = -1;
							TryAgain = true;
						}
					} else if (Direction == 3) {
						BombX = HitX;
						BombY = HitY - FromFirstHit;
						if (BombY < 0) {
							Direction = 0;
							FromFirstHit = 0;
							HitX = -1;
							HitY = -1;
							TryAgain = true;
						}
					} else {
						Direction = 0;
						FromFirstHit = 0;
						HitX = -1;
						HitY = -1;
						TryAgain = true;
					}
				}
				if (!TryAgain) {
					if (MyShips[BombX + BombY * GameXsize] == 1) {
						MyShips[BombX + BombY * GameXsize] = 3;
						if (FromFirstHit == 0) {
							HitX = BombX;
							HitY = BombY;

						}
						FromFirstHit++;

					} else if (MyShips[BombX + BombY * GameXsize] == 0) {
						MyShips[BombX + BombY * GameXsize] = 2;
						if (HitX != -1) {
							Direction++;
							FromFirstHit = 1;
						}
					} else {
						Direction++;
						FromFirstHit = 1;
						if (Direction > 4) {
							Direction = 0;
							FromFirstHit = 0;
							HitX = -1;
							HitY = -1;
						}
						TryAgain = true;
					}
				}
			}
		}

		private void ComputerPlayer() {
			Random rnd = new Random();
			bool TryAgain = true;
			int BombX = 0;
			int BombY = 0;
			// Kolla om datorn har en träff med obombad ruta intill
			bool Obombad = false;
			for (int y = 0; y < GameYsize; y++) {
				for (int x = 0; x < GameXsize; x++) {
					if (MyShips[x + y * GameXsize] == 3) {
						BombX = x + 1;
						BombY = y;
						if (BombX < GameXsize) {
							if (MyShips[BombX + BombY * GameXsize] == 0 || MyShips[BombX + BombY * GameXsize] == 1) {
								Obombad = true;
								break;
							}
						}
						BombX = x - 1;
						BombY = y;
						if (BombX >= 0) {
							if (MyShips[BombX + BombY * GameXsize] == 0 || MyShips[BombX + BombY * GameXsize] == 1) {
								Obombad = true;
								break;
							}
						}
						BombX = x;
						BombY = y + 1;
						if (BombY < GameYsize) {
							if (MyShips[BombX + BombY * GameXsize] == 0 || MyShips[BombX + BombY * GameXsize] == 1) {
								Obombad = true;
								break;
							}
						}
						BombX = x;
						BombY = y - 1;
						if (BombY >= 0) {
							if (MyShips[BombX + BombY * GameXsize] == 0 || MyShips[BombX + BombY * GameXsize] == 1) {
								Obombad = true;
								break;
							}
						}
					}
				}
				if (Obombad) break;
			}

			// Om inte, slumpa fram ett nytt bombmål 
			if (!Obombad) {
				while (TryAgain) {
					TryAgain = false;
					BombX = rnd.Next(GameXsize);
					BombY = rnd.Next(GameYsize);
					if (MyShips[BombX + BombY * GameXsize] != 0 && MyShips[BombX + BombY * GameXsize] != 1) {
						TryAgain = true;
					}
				}
			}

			//Var det träff eller inte
			if (MyShips[BombX + BombY * GameXsize] == 1) {
				MyShips[BombX + BombY * GameXsize] = 3;
				EnScore++;
				if (EnScore == MaxScore) Winner = "Computer";
			} else {
				MyShips[BombX + BombY * GameXsize] = 2;
			}
		}
	}
}
