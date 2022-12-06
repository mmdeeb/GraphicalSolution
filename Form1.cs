using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<Constraint> constraints = new List<Constraint>();
        Solution solution;
        public Form1()
        {
            InitializeComponent();

            dataGridView1.CellMouseUp += DataGridView_CellMouseUp;
            dataGridView1.ColumnCount = 1;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 220;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ClearSelection();
            dataGridView2.CellMouseUp += DataGridView_CellMouseUp;
            dataGridView2.ColumnCount = 1;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.Columns[0].Width = 450;
            dataGridView2.ScrollBars = ScrollBars.Vertical;
            dataGridView2.ClearSelection();
            setScale();


            initFieldOnChart();
            Add(new Constraint(0, 1, true, 0));
            Add(new Constraint(1, 0, true, 0));

        }

        private int rowIndex = 0;
        private void DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
            }
        }

        double chartConstraintValue = 100;

        private void FindS_Click(object sender, EventArgs e)
        {
            try
            {
                double c1 = Convert.ToDouble(this.a1f.Text);
                double c2 = Convert.ToDouble(this.a2f.Text);
                bool isMaxTarget;
                if (minmax.Text.ToLower() == "max")
                    isMaxTarget = true;
                else if (minmax.Text.ToLower() == "min")
                    isMaxTarget = false;
                else
                    throw new ArgumentException("error") ;

                Target target = new Target(c1, c2, isMaxTarget);
                solution = new Solution(constraints, target, isMaxTarget);
                dataGridView2.Rows.Add(solution.ToString());
                dataGridView2.ClearSelection();
                dataGridView2.Rows[0].Cells[0].Style.BackColor = Color.AliceBlue;
                if (solution.HasSolution)
                {
                    drowActivePoint(solution.SolutionAreaPoints);
                }
               



            }
            catch (Exception)
            {

                clearConstraintsTextBoxes();
                a1f.Text = "";
                a2f.Text = "";
                minmax.Text = "";
            }
            


        }

        private void drowActivePoint(List<Point> activePoints)
        {
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = $"Active Points",
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Area

            };
            series.BorderColor = Color.Black;
            series.BorderWidth = 2;
            series.MarkerSize = 10;

            this.chart1.Series.Add(series);
            chart1.Series["Field"].Points.Clear();
            foreach (Point point in solution.getSolutionAreaPointsInAntiClockOrder())
                series.Points.AddXY(point.X1 + 0.01, point.X2 + 0.01);

        }

        void initFieldOnChart()
        {
            var fieldSeries = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Field",
                Color = System.Drawing.Color.Black,
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Line
            };
            fieldSeries.BorderWidth = 1;
            chart1.Series.Add(fieldSeries);
        }
        double currentXMax = 20;
        double currentXMin = 0;
        double currentYMax = 20;
        double currentYMin = 0;
        void setScale()
        {
            if (currentXMax > currentXMin && currentYMax > currentYMin)
            {
                chart1.ChartAreas[0].AxisX.Maximum = currentXMax;
                chart1.ChartAreas[0].AxisX.Minimum = currentXMin;
                chart1.ChartAreas[0].AxisY.Maximum = currentYMax;
                chart1.ChartAreas[0].AxisY.Minimum = currentYMin;
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            double x1, x2, z;
            bool isAbove;
            try
            {
                x1 = Convert.ToDouble(this.X1_text.Text);
                x2 = Convert.ToDouble(this.X2_text.Text);
                z = Convert.ToDouble(this.Z_text.Text);
                if (ISAbove.Text.Equals("<="))
                    isAbove = false;
                else
                    isAbove = true;


                Add(new Constraint(x1, x2, isAbove, z));
            }
            catch (Exception)
            {

                clearConstraintsTextBoxes();
            }

        }

        private void Add(Constraint constraint)
        {
            constraints.Add(constraint);
            clearConstraintsTextBoxes();
            dataGridView1.Rows.Add(constraint.ToString());
            dataGridView1.ClearSelection();
            drawConstraint(constraint);
            chart1.ApplyPaletteColors();
            dataGridView1.Rows[constraints.Count - 1].Cells[0].Style.BackColor = chart1.Series[constraint.ToString()].Color;
        }

        private void clearConstraintsTextBoxes()
        {
            X1_text.Text = "";
            X2_text.Text = "";
            Z_text.Text = "";


        }


        void drawConstraint(Constraint constraint)
        {
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = constraint.ToString(),
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Line

            };
            series.BorderWidth = 2;

            this.chart1.Series.Add(series);
            chart1.Series["Field"].Points.Clear();


            if (constraint.X1 == 0)
            {
                double x2 = constraint.Z / constraint.X2;
                series.Points.AddXY(-chartConstraintValue, x2);
                series.Points.AddXY(chartConstraintValue, x2);
            }
            else if (constraint.X2 == 0)
            {
                double x1 = constraint.Z / constraint.X1;
                series.Points.AddXY(x1, -chartConstraintValue);
                series.Points.AddXY(x1, chartConstraintValue);
            }
            else
            {
                double x1 = (constraint.Z - constraint.X2 * chartConstraintValue) / constraint.X1;
                double x2 = (constraint.Z - constraint.X2 * -chartConstraintValue) / constraint.X1;
                series.Points.AddXY(x1, chartConstraintValue);
                series.Points.AddXY(x2, -chartConstraintValue);

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            currentXMax -= 1;
            currentXMin = 0;
            currentYMax -= 1;
            currentYMin = 0;
            setScale();

        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {

            currentXMax += 1;
            currentXMin = 0;
            currentYMax += 1;
            currentYMin = 0;
            setScale();
        }

    }
}
