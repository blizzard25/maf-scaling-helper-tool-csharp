using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MAF_Tuning_Helper_Tool
{
    public partial class DataDisplayForm : Form
    {
        public static List<List<Tuple<double, double>>> plotDataSets = new List<List<Tuple<double, double>>>();
        private List<List<Tuple<double, double>>> dataBins = new List<List<Tuple<double, double>>>();
        private List<Tuple<double, double>> multipliers = new List<Tuple<double, double>>();

        public DataDisplayForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void DataDisplayForm_Load(object sender, EventArgs e)
        {
            pltDataScatterPlot.Series[0].Points.Clear();
            pltDataScatterPlot.Series[0].ChartType = SeriesChartType.Point;
            pltDataScatterPlot.ChartAreas[0].AxisY.Maximum = 125;
            pltDataScatterPlot.ChartAreas[0].AxisY.Minimum = 75;
            if (plotDataSets.Count() > 0)
            {
                pltDataScatterPlot.Series[0].LegendText = "Bank 1";
                pltDataScatterPlot.Series[0].Color = Color.Red;
                plotDataSets[0].ForEach(i => pltDataScatterPlot.Series[0].Points.AddXY(i.Item1, i.Item2));
            }
            if(plotDataSets.Count() > 1)
            {
                Series bank2 = new Series("Bank 2");
                bank2.Color = Color.Blue;
                plotDataSets[1].ForEach(i => bank2.Points.AddXY(i.Item1, i.Item2));
                pltDataScatterPlot.Series.Add(bank2);
                pltDataScatterPlot.Series[1].ChartType = SeriesChartType.Point;
            }
            CreateMultiplierList();
            CreateGridViewRows();
            multipliers.ForEach(i => System.Diagnostics.Debug.WriteLine(String.Format("Value = {0}, Mult = {1}", i.Item1, i.Item2)));
        }

        private void CreateGridViewRows()
        {
            dgvSuggestedValues.ColumnHeadersVisible = false;
            DataGridViewRow dgvr1 = (DataGridViewRow)dgvSuggestedValues.RowTemplate.Clone();
            dgvr1.CreateCells(dgvSuggestedValues, "MAF (V)", "Multiplier");
            dgvr1.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvr1.Cells[0].Style.Font = new Font(dgvSuggestedValues.Font, FontStyle.Bold);
            dgvr1.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvr1.Cells[1].Style.Font = new Font(dgvSuggestedValues.Font, FontStyle.Bold);
            dgvSuggestedValues.Rows.Add(dgvr1);
            foreach(Tuple<double, double> row in multipliers)
            {
                DataGridViewRow dgvr = (DataGridViewRow)dgvSuggestedValues.RowTemplate.Clone();
                dgvr.CreateCells(dgvSuggestedValues, row.Item1, row.Item2);
                dgvr.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvr.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSuggestedValues.Rows.Add(dgvr);
            }
        }

        private List<Tuple<double, double>> CreateSingleDataBin(List<Tuple<double, double>> dataSet, double mafVoltageFloor, double mafVoltageCeiling)
        {
            return plotDataSets[0].Where(i => (i.Item1 > mafVoltageFloor && i.Item1 <= mafVoltageCeiling)).Select(a => new Tuple<double, double>(a.Item1, a.Item2)).ToList();
        }

        private void CreateMultiplierList()
        {
            var mafVolts = CsvDataParser.mafVoltages;
            multipliers.Add(new Tuple<double, double>(mafVolts[0], 1.0));
            int i = 0;
            if (plotDataSets.Count() > 0)
            {
                for (; i < mafVolts.Count() - 1; i++) multipliers.Add(new Tuple<double, double>(mafVolts[i + 1], Math.Round(CalculateMultiplier(CreateSingleDataBin(plotDataSets[0], mafVolts[i], mafVolts[i + 1])), 3)));
            }
            if (plotDataSets.Count() > 1)
            {
                i = 0;
                var b2Multipliers = new List<Tuple<double, double>>();
                for (; i < mafVolts.Count() - 1; i++) b2Multipliers.Add(new Tuple<double, double>(mafVolts[i + 1], Math.Round(CalculateMultiplier(CreateSingleDataBin(plotDataSets[0], mafVolts[i], mafVolts[i + 1])), 2)));
                multipliers = CalculateMultiplierWithTwoBanks(multipliers, b2Multipliers);
            }
        }

        private double CalculateMultiplier(List<Tuple<double, double>> dataBin)
        {
            return dataBin.Count() == 0 ? 1.0 : dataBin.Select(i => i.Item2).ToList().Average() / 100;
        }

        private List<Tuple<double,double>> CalculateMultiplierWithTwoBanks(List<Tuple<double, double>> bank1Mults, List<Tuple<double, double>> bank2Mults)
        {
            var newMultipliers = new List<Tuple<double, double>>();
            for(int i = 0; i < bank2Mults.Count(); i++) newMultipliers.Add(new Tuple<double, double>(CsvDataParser.mafVoltages[i], (bank1Mults[i].Item2 + bank2Mults[i].Item2) / 2));
            return newMultipliers;
        }
    }
}
