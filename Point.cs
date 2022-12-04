using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Point
    {
        public double X1;
        public double X2;
        public List<Constraint> Constraints = new List<Constraint>();

        public Point (Constraint c1, Constraint c2)
        {
            double determinant = c1.X1 * c2.X2 - c1.X2 * c2.X1;

            double x1 = (c1.Z * c2.X2 - c1.X2 * c2.Z) / determinant;
            double x2 = (c1.X1 * c2.Z - c1.Z * c2.X1) / determinant;

            this .X1 = x1;
            this .X2 = x2;
            Constraints.Add (c1);
            Constraints.Add (c2);
        }


        public static List<Point> getPoints(List<Constraint> constraints)
        {
            List<Point> points = new List<Point>();
            int i = 1;
            foreach (Constraint constraint1 in constraints.Skip(1))
            {

                foreach (Constraint constraint2 in constraints.Skip(i))
                {
                    if (constraint1 == constraint2)
                        continue;

                    Point newPoint = new Point(constraint1, constraint2);
                    bool isExist = false;
                    Point existPoint = null;
                    foreach (Point point in points)
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
                        points.Add(newPoint);
                        constraint1.Points.Add(newPoint);
                        constraint2.Points.Add(newPoint);

                    }

                }
                i++;
            }
            return points;

        }
    }
}
