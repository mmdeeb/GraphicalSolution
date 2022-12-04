using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Constraint
    {
        public double X1, X2, Z;
        public bool IsAbove;
        public List<Point> Points = new List<Point>();
        
        public Constraint(double X1,double X2 ,bool IsAbove ,double Z)
        {
            this.X1 = X1;
            this.X2 = X2;
            this.Z = Z;
            this.IsAbove = IsAbove;
            
        }
        public override string ToString()
        {
            string typeString ;
            if (!IsAbove)
                typeString = "<=";
            else
                typeString = ">=";
            string a2String = X2 >= 0 ? "+ " + X2 : X2.ToString();
            string desc = $"{X1}x1 {a2String}x2 {typeString} {Z}";
            return desc;
        }

        public bool checkPoint(Point point)
        {


            if ((((point.X1 * X1) + (point.X2 * X2) >= Z) && IsAbove) || ((point.X1 * X1) + (point.X2 * X2) <= Z) && !IsAbove)
            {
                return true;

            }
            else
                return false;
        }
    }
}
