using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem {
    class AccessibleCoordinate : Coordinate {
        public int AccessibleScore { get; set; }
        public AccessibleCoordinate(int accessibleScore, int x, int y) {
            this.AccessibleScore = accessibleScore;
            this.X = x;
            this.Y = y;
        }
    }
}
