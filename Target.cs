using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Target
    {
        public double X1, X2, Z;
        public bool IsMax,HasSolution = true;
        public List<Point> activePoints = new List<Point>();
        public Target(double X1, double X2, bool IsMax)
        {
            this.X1 = X1;
            this.X2 = X2;
            this.IsMax = IsMax;

        }

        public void calcSolutionArea(List<Point> points ,List<Constraint> constraints)
        {

            foreach (Point point in points)
            {
                bool b = true;
                foreach (Constraint constraint in constraints)
                {
                    if (constraint.Points.Contains(point))
                        continue;

                    if (!constraint.checkPoint(point))
                        b = false;

                }

                if (b)
                    activePoints.Add(point);

            }
            if(activePoints.Count()==0)
                HasSolution = false;

        }

        public (double value, Point activePointAtMaxValue) maxValue()
        {
            double max = double.MinValue;
            double value = double.MinValue;
            Point activePointAtMaxValue = null;
            foreach (Point activePoint in activePoints)
            {
                value = (activePoint.X1 * X1) + (activePoint.X2 * X2);

                if (value > max)
                {
                    max = value;
                    activePointAtMaxValue = activePoint;

                }

            }

            return (max,activePointAtMaxValue);
        }


        public (double value , Point activePointAtMinValue) minValue()
        {
            double min = double.MaxValue;
            double value = double.MinValue;
            Point activePointAtMinValue = null;
            foreach (Point activePoint in activePoints)
            {
                value = (activePoint.X1 * X1) + (activePoint.X2 * X2);

                if (value < min)
                {
                    min = value;
                    activePointAtMinValue = activePoint;

                }

            }

            return (min,activePointAtMinValue);
        }


        public void printSolutionArea()
        {
            if (!HasSolution)
            {
                Console.WriteLine("No solution");
                return;

            }
                
            
            Console.WriteLine("Active Points :");
            foreach (Point point in activePoints)
            {
                Console.WriteLine($"X1:{point.X1} x2:{point.X2}");
            }
            
            
        }

        public override string ToString()
        {
            string s = "";
            if (HasSolution)
            {
                if (IsMax)
                {
                    s= $"best value is {maxValue().value} " +
                                      $"at point : ({maxValue().activePointAtMaxValue.X1}" +
                                      $",{maxValue().activePointAtMaxValue.X2})";
                }

                else
                {
                    s = $"best value is {minValue().value} " +
                                      $"at point : ({minValue().activePointAtMinValue.X1}" +
                                      $",{minValue().activePointAtMinValue.X2})";
                }


            }
            else
            {
                s = "No solution";
           
            }
            return s;
        }
    }
}
