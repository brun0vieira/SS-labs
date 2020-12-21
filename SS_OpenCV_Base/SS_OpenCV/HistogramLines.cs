<<<<<<< Updated upstream
﻿using System;
=======
<<<<<<< HEAD
﻿/*using System;
=======
﻿using System;
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class HistogramLines : Form
    {
        public HistogramLines(int[,] histogram, int components)
        {
            InitializeComponent();
            if(components == 4)
                HistogramLines4(histogram);
            else
            {
                if (components == 3)
                    HistogramLines3(histogram);
            }
        }

        public void HistogramLines3(int[,] histogram)
        {
            var chart = chart1.ChartAreas[0];

<<<<<<< Updated upstream
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
=======
<<<<<<< HEAD
           // chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
=======
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 0;
            chart.AxisY.Minimum = 0;

            chart.AxisX.Interval = 0;
            chart.AxisY.Interval = 0;

            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series.Add("Blue");
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
           // chart1.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Blue"].Color = Color.Blue;

            chart1.Series.Add("Green");
           // chart1.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Green"].Color = Color.LightGreen;

            chart1.Series.Add("Red");
           // chart1.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
=======
>>>>>>> Stashed changes
            chart1.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Blue"].Color = Color.Blue;

            chart1.Series.Add("Green");
            chart1.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Green"].Color = Color.LightGreen;

            chart1.Series.Add("Red");
            chart1.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
<<<<<<< Updated upstream
=======
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes
            chart1.Series["Red"].Color = Color.Red;

            for (int j = 0; j < 256; j++)
            {
                chart1.Series["Blue"].Points.AddXY(j, histogram[0, j]);
                chart1.Series["Green"].Points.AddXY(j, histogram[1, j]);
                chart1.Series["Red"].Points.AddXY(j, histogram[2, j]);
            }

        }

        public void HistogramLines4(int[,] histogram)
        {
            var chart = chart1.ChartAreas[0];

<<<<<<< Updated upstream
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
=======
<<<<<<< HEAD
         //   chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
=======
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 0;
            chart.AxisY.Minimum = 0;

            chart.AxisX.Interval = 0;
            chart.AxisY.Interval = 0;

            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series.Add("Gray");
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
          //  chart1.Series["Gray"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Gray"].Color = Color.Gray;

            chart1.Series.Add("Blue");
          //  chart1.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Blue"].Color = Color.Blue;

            chart1.Series.Add("Green");
         //   chart1.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Green"].Color = Color.LightGreen;

            chart1.Series.Add("Red");
         //   chart1.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
=======
>>>>>>> Stashed changes
            chart1.Series["Gray"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Gray"].Color = Color.Gray;

            chart1.Series.Add("Blue");
            chart1.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Blue"].Color = Color.Blue;

            chart1.Series.Add("Green");
            chart1.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Green"].Color = Color.LightGreen;

            chart1.Series.Add("Red");
            chart1.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
<<<<<<< Updated upstream
=======
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes
            chart1.Series["Red"].Color = Color.Red;

            for(int j = 0; j < 256; j++)
            {
                chart1.Series["Gray"].Points.AddXY(j, histogram[0, j]);
                chart1.Series["Blue"].Points.AddXY(j, histogram[1, j]);
                chart1.Series["Green"].Points.AddXY(j, histogram[2, j]);
                chart1.Series["Red"].Points.AddXY(j, histogram[3, j]);
            }
            
        }

    }
}
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
*/
=======
>>>>>>> b0517d1e7271c8b96e26529b2322c954d90c597b
>>>>>>> Stashed changes
