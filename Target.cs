using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIConvexHull;

namespace WindowsFormsApp2
{
    internal class Target
    {
        public double X1, X2, Z;
        public bool IsMax,HasSolution = true;
        public static List<Point> activePoints = new List<Point>();
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
            calcAngelBetweenVectors();

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

        public static void calcAngelBetweenVectors()
        {

            double totalX1 = 0, totalX2 = 0;
            foreach (Point point in activePoints)
            {
                totalX1 += point.X1;
                totalX2 += point.X2;
            }
            double centerX = totalX1 / activePoints.Count;
            double centerY = totalX2 / activePoints.Count;
            foreach (Point point in activePoints)
            {
                point.angle = Math.Atan2((point.X2 - centerY), (point.X1 - centerY)) * (double)(180 / Math.PI);
            }

        }
    }
}
