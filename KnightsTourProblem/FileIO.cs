using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourProblem {
    class FileIO {
        public static void writeToFile(string fileName, string results) {
            string fullPath = @".\" + fileName;

            using (StreamWriter w = File.AppendText(fullPath)) {
                w.Write(results);
            }
        }
    }
}