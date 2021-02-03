using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourProblem {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e) {
            int numAttempts = Convert.ToInt32(nupNumTries.Value);
            int[] attemptTotals = new int[numAttempts];
            txtBxResults.Text = "";
            string results = "";

            int x = Convert.ToInt32(xCoordinate.Value);
            int y = Convert.ToInt32(yCoordinate.Value);
            if (cmbMethod.SelectedItem.ToString() == null) {
                MessageBox.Show("Missing Info", "Please select a method to use.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {

                // validate the starting point
                if (x < 0 || x > 7 || y < 0 || y > 7) {
                    MessageBox.Show("Invalid Info", "X and Y coordinates must both be between 0 and 7", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else {
                    string selectedMethod = cmbMethod.SelectedItem.ToString();
                    
                    int i = 0;
                    while (i < numAttempts) {
                        int[,] attempt = new int[8,8];

                        // gets an 8 x 8 array, of entire board
                        if (selectedMethod.Equals("Non-intelligent")) {

                            attempt = KnightsPath.NonIntelligentMethod(x, y);

                        } else if (selectedMethod.Equals("Heuristic")) {

                            attempt = KnightsPath.HeuristicMethod(x, y);

                        }

                        int numSquares = displayAttempt(attempt);
                        attemptTotals[i] = numSquares;

                        txtBxResults.Text += "Trial " + (i + 1) + ": " + numSquares + " squares\n";

                        results += "Trial " + (i + 1) + ": The knight was able to sucessfully touch " + numSquares + " squares.\n";

                        i++;

                        MessageBox.Show("Knight has ran out of moves.", "Round Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (numAttempts > 1) {
                        // calc standard deviation
                        double stdDevi = calcSandardDeviation(attemptTotals);
                        txtBxResults.Text += "Standard Dev.: " + stdDevi + "\n";

                        // calculate average
                        double mean = calcMean(attemptTotals);
                        txtBxResults.Text += "\nAverage: " + mean + "\n";
                    }
                    txtBxResults.Text += "-------------------------\n";
                    results += "\n";

                    if (selectedMethod.Equals("Non-intelligent")) {

                        FileIO.writeToFile("BriannaNonIntelligentMethod.txt", results);

                    } else if (selectedMethod.Equals("Heuristic")) {

                        FileIO.writeToFile("BriannaHeuristicsMethod.txt", results);

                    }
                } 
            }
        }

        private double  calcSandardDeviation(int[] attemptTotals) {

            // calculate the mean
            double mean = calcMean(attemptTotals);

            // (val-mean)^2
            double[] newVals = new double[attemptTotals.Length];
            for (int i = 0; i < attemptTotals.Length; i++) {
                newVals[i] = (Convert.ToDouble(attemptTotals[i]) - mean) * (Convert.ToDouble(attemptTotals[i]) - mean);
            }

            // mean of those squared differences
            double newSum = 0;
            for (int i = 0; i < attemptTotals.Length; i++) {
                newSum += newVals[i];
            }
            double newMean = newSum / attemptTotals.Length;

            // sqaure root of new sum
            double stdDevi = Math.Round(Math.Sqrt(newMean), 2);

            return stdDevi;
        }
        private double calcMean(int[] nums) {
            int sum = 0;
            // calculate mean of attemptTotals[]
            for (int i = 0; i < nums.Length; i++) {
                sum += nums[i];
            }
            double mean = Math.Round(((double)sum) / nums.Length, 2);

            return mean;
        }

        private int displayAttempt(int[,] attempt) {
            chessBoard.Controls.Clear();
            int numSquares = 0;
            int i, j;
            // loop through 8 x 8 array and display each value accordingly on the table
            for (i = 0; i < 8; i++) { // row
                for (j = 0; j < 8; j++) { // col
                    Label lbl =new  Label();
                    lbl.AutoSize = true;
                    lbl.Anchor = AnchorStyles.None;
                    lbl.Font = new Font("Book Antiqua", 12);

                    lbl.Text = attempt[i, j].ToString();

                    chessBoard.Controls.Add(lbl, j, i);

                    if(attempt[i, j] > numSquares) {
                        numSquares = attempt[i, j];
                    }
                }
            }
            return numSquares;
        }
    }
}
