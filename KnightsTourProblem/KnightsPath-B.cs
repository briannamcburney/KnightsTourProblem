using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem {
    public partial class KnightsPath {
        private static AccessibleCoordinate[,] accessCoords;

        public static int[,] HeuristicMethod(int x, int y) {
            combos = ResetCombos();
            InitBoard();
            SetAccessibleCoordinates();
            Coordinate c = new Coordinate(x, y);
            int comboOption;
            int moveNumb = 1;

            path[c.Y, c.X] = moveNumb;

            // get next set of coordinates
            do {
                comboOption = NextMove(c.X, c.Y);

                if (comboOption >= 0) {
                    Combo combo = (Combo)combos[comboOption];

                    c.X += combo.Xmove;
                    c.Y += combo.Ymove;

                    moveNumb++;
                    path[c.Y, c.X] = moveNumb;

                } else {
                    // there are no more combinations to try
                    c.X = -1;
                    c.Y = -1;
                }

            } while (c.X != -1 && c.Y != -1);

            return path;
        }

        public static int NextMove(int x, int y) {
            int comboOption = -1;
            int lowestAccessScore = 10; // default to make it always the larger score
            Coordinate c = new Coordinate(x, y);
            ArrayList sameAccessScores = new ArrayList();

            // try all combos and find the square with the lowest accessibility score
            for (int i = 0; i < combos.Count; i++) {
                Combo combo = (Combo)combos[i];
                c.X += combo.Xmove;
                c.Y += combo.Ymove;

                // is the new coordinate valid
                if ((c.X >= 0 && c.X <= 7) && (c.Y >= 0 && c.Y <= 7)) {
                    // has the castle been there yet
                    if (path[c.Y, c.X] == 0) {
                        int currentAccessScore = accessCoords[c.Y, c.X].AccessibleScore;
                        if (lowestAccessScore > currentAccessScore) {
                            // these cooridnates are the greater priority
                            lowestAccessScore = currentAccessScore;
                            comboOption = i;

                            // reset sameAccessScore & j bc new lowest score has been found
                            sameAccessScores.Clear();
                            sameAccessScores.Add(i);
                        } else if (lowestAccessScore == currentAccessScore) {
                            // if same access score, add to list bc then have to pick a random option
                            sameAccessScores.Add(i);
                        }
                    }
                }
                // reset coordinates
                c.X = x;
                c.Y = y;
            }

            int j = 0;
            if (sameAccessScores.Count > 1) {
                Random rand = new Random();
                j = rand.Next(0, sameAccessScores.Count);
                comboOption = (int)sameAccessScores[j];
            }

            return comboOption;
        }

        public static void SetAccessibleCoordinates() {
            int x, y, accessibleScore = 0;
            accessCoords = new AccessibleCoordinate[8, 8];
            for (y = 0; y < 8; y++) {
                for (x = 0; x < 8; x++) {

                    if ((x == 0 && y == 0) || (x == 0 && y == 7) || (x == 7 && y == 0) || (x == 7 && y == 7)) {
                        accessibleScore = 2;
                    } else if ((x == 0 && y == 1) || (x == 1 && y == 0) || (x == 6 && y == 0) || (x == 7 && y == 1) || (x == 0 && y == 6) || (x == 1 && y == 7) || (x == 6 && y == 7) || (x == 7 && y == 6)) {
                        accessibleScore = 3;
                    } else if (((x == 0) && (2 <= y && y <= 5)) || ((x == 7) && (2 <= y && y <= 5)) || ((2 <= x && x <= 5) && (y == 0)) || ((2 <= x && x <= 5) && (y == 7)) ||
                        (x == 1 && y == 1) || (x == 6 && y == 1) || (x == 1 && y == 6) || (x == 6 && y == 6)) {
                        accessibleScore = 4;
                    } else if (((x == 1) && (2 <= y && y <= 5)) || ((x == 6) && (2 <= y && y <= 5)) || ((2 <= x && x <= 5) && (y == 1)) || ((2 <= x && x <= 5) && (y == 6))) {
                        accessibleScore = 6;
                    } else if ((2 <= x && x <= 5) && (2 <= y && y <= 5)) {
                        accessibleScore = 8;
                    } else {
                        accessibleScore = 0;
                    }

                    AccessibleCoordinate ac = new AccessibleCoordinate(accessibleScore, x, y);

                    accessCoords[y, x] = ac;
                }
            }
        }
    }
}
