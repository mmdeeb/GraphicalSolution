using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public class Point
    {
        public double X1;
        public double X2;
        public List<Constraint> Constraints = new List<Constraint>();

        public Point(Constraint c1, Constraint c2)
        {
            double determinant = c1.X1 * c2.X2 - c1.X2 * c2.X1;

            double x1 = (c1.Z * c2.X2 - c1.X2 * c2.Z) / determinant;
            double x2 = (c1.X1 * c2.Z - c1.Z * c2.X1) / determinant;

            this.X1 = x1;
            this.X2 = x2;
            Constraints.Add(c1);
            Constraints.Add(c2);
        }




    }
}
