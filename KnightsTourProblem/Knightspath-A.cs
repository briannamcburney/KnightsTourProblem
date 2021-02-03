using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem {
    public partial class KnightsPath {

        private static ArrayList combos;
        private static int[,] path;

        public static int[,] NonIntelligentMethod(int x, int y) {
            combos = ResetCombos();
            InitBoard();
            Coordinate c1 = new Coordinate(x, y);
            Coordinate c2 = new Coordinate(x, y);
            Random rand = new Random();
            int moveNumb = 1;
            int comboOption;
            bool needNewCoords;

            path[c1.X, c1.Y] = moveNumb;

            // get next set of coordinates
            do {
                do {
                    needNewCoords = true;
                    // check if there are any combos left
                    if (combos.Count > 0) {
                        comboOption = rand.Next(0, combos.Count - 1);
                        Combo combo = (Combo)combos[comboOption];

                        c2.X += combo.Xmove;
                        c2.Y += combo.Ymove;

                        // is the new set of coordinates valid
                        if ((c2.X >= 0 && c2.X <= 7) && (c2.Y >= 0 && c2.Y <= 7)) {
                            // has the Knight already been there?
                            if (path[c2.Y, c2.X] != 0) {
                                // have already been there
                                // have to try different combo
                                combos.RemoveAt(comboOption);
                                c2.X = c1.X;
                                c2.Y = c1.Y;
                                continue;
                            } else {
                                // havent been there yet
                                // so make the move
                                moveNumb++;
                                path[c2.Y, c2.X] = moveNumb;
                                c1.X = c2.X;
                                c1.Y = c2.Y;

                                //reset combos
                                combos = ResetCombos();

                                needNewCoords = false;
                            }
                        } else {
                            // coordinates are off the grid
                            // have to try different combo
                            combos.RemoveAt(comboOption);
                            c2.X = c1.X;
                            c2.Y = c1.Y;
                            continue;
                        }
                    } else {
                        // there are no more combinations to try
                        c1.X = -1;
                        c1.Y = -1;
                        needNewCoords = false;
                    }

                } while (needNewCoords);

            } while (c1.X != -1 && c1.Y != -1);

            return path;
        }

        public static void InitBoard() {
            int i, j;
            path = new int[8, 8];
            for (i = 0; i < 8; i++) {
                for (j = 0; j < 8; j++) {
                    path[i, j] = 0;
                }
            }
        }
        public static ArrayList ResetCombos() {
            ArrayList combos = new ArrayList();

            combos.Add(new Combo(-1, -2));
            combos.Add(new Combo(-1, 2));
            combos.Add(new Combo(1, 2));
            combos.Add(new Combo(1, -2));
            combos.Add(new Combo(-2, -1));
            combos.Add(new Combo(-2, 1));
            combos.Add(new Combo(2, 1));
            combos.Add(new Combo(2, -1));

            return combos;
        }
    }
}
