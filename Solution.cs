using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp2
{
    public class Solution
    {
        List<Constraint> constraints;
        Target target;
        List<Point> IntersectionPoints = new List<Point> { };
        public List<Point> SolutionAreaPoints = new List<Point> { };
        public bool IsMax, HasSolution = true;
        public Solution(List<Constraint> constraints, Target target, bool isMax)
        {
            this.constraints = constraints;
            this.IsMax = isMax;
            this.target = target;
            calcIntersectionPoints();
            calcSolutionAreaPoints();
        }

        public void calcIntersectionPoints()
        {

            int i = 1;
            foreach (Constraint constraint1 in constraints.SkipLast(1))
            {

                foreach (Constraint constraint2 in constraints.Skip(i))
                {
                    if (constraint1 == constraint2)
                        continue;

                    Point newPoint = new Point(constraint1, constraint2);
                    bool isExist = false;
                    Point existPoint = null;
                    foreach (Point point in IntersectionPoints)
                    {
                        if (newPoint.X1.Equals(point.X1) && newPoint.X2.Equals(point.X2))
                        {
                            isExist = true;
                            existPoint = point;
                            break;

                        }

                    }

                    if (isExist)
                    {
                        existPoint.Constraints.Add(constraint1);
                        existPoint.Constraints.Add(constraint2);

                    }

                    else
                    {
                        IntersectionPoints.Add(newPoint);
                        constraint1.Points.Add(newPoint);
                        constraint2.Points.Add(newPoint);

                    }

                }
                i++;
            }


        }

        public void calcSolutionAreaPoints()
        {

            foreach (Point point in IntersectionPoints)
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
                    SolutionAreaPoints.Add(point);

            }
            if (SolutionAreaPoints.Count() == 0)
                HasSolution = false;

        }

        public IEnumerable<Point> getSolutionAreaPointsInAntiClockOrder()
        {

            double totalX1 = 0, totalX2 = 0;
            foreach (Point point in SolutionAreaPoints)
            {
                totalX1 += point.X1;
                totalX2 += point.X2;
            }
            double centerX = totalX1 / SolutionAreaPoints.Count;
            double centerY = totalX2 / SolutionAreaPoints.Count;

            return SolutionAreaPoints.OrderBy(p => Math.Atan2((p.X2 - centerY), (p.X1 - centerY)) * (180 / Math.PI));

        }

        public override string ToString()
        {
            string s = "";
            if (HasSolution)
            {
                if (IsMax)
                {
                    s = $"best value is {target.maxValue(SolutionAreaPoints).value} " +
                                      $"at point : ({target.maxValue(SolutionAreaPoints).activePointAtMaxValue.X1}" +
                                      $",{target.maxValue(SolutionAreaPoints).activePointAtMaxValue.X2})";
                }

                else
                {
                    s = $"best value is {target.minValue(SolutionAreaPoints).value} " +
                                      $"at point : ({target.minValue(SolutionAreaPoints).activePointAtMinValue.X1}" +
                                      $",{target.minValue(SolutionAreaPoints).activePointAtMinValue.X2})";
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
