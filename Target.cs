using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public class Target
    {
        public double X1, X2, Z;
        public Target(double X1, double X2, bool IsMax)
        {
            this.X1 = X1;
            this.X2 = X2;

        }


        public (double value, Point activePointAtMaxValue) maxValue(List<Point> solutionAreaPoints)
        {
            double max = double.MinValue;
            double value = double.MinValue;
            Point activePointAtMaxValue = null;
            foreach (Point point in solutionAreaPoints)
            {
                value = (point.X1 * X1) + (point.X2 * X2);

                if (value > max)
                {
                    max = value;
                    activePointAtMaxValue = point;

                }

            }

            return (max, activePointAtMaxValue);
        }


        public (double value, Point activePointAtMinValue) minValue(List<Point> solutionAreaPoints)
        {
            double min = double.MaxValue;
            double value = double.MinValue;
            Point activePointAtMinValue = null;
            foreach (Point point in solutionAreaPoints)
            {
                value = (point.X1 * X1) + (point.X2 * X2);

                if (value < min)
                {
                    min = value;
                    activePointAtMinValue = point;

                }

            }

            return (min, activePointAtMinValue);
        }

        /*
                public string printSolutionArea()
                {
                    string s = "";
                    if (!HasSolution)
                    {
                        s += ("No solution");
                        return s;

                    }


                    s = "Active Points : ";
                    foreach (Point point in activePoints)
                    {
                        s += ($"X1:{point.X1} x2:{point.X2} ,");
                    }
                    return s;


                }

        */





    }
}
