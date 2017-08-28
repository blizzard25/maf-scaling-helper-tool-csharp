namespace MAF_Tuning_Helper_Tool
{
    partial class DataDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pltDataScatterPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvSuggestedValues = new System.Windows.Forms.DataGridView();
            this.clmnMAF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnMult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pltDataScatterPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestedValues)).BeginInit();
            this.SuspendLayout();
            // 
            // pltDataScatterPlot
            // 
            chartArea1.Name = "ChartArea1";
            this.pltDataScatterPlot.ChartAreas.Add(chartArea1);
            this.pltDataScatterPlot.Dock = System.Windows.Forms.DockStyle.Right;
            legend1.Name = "Legend1";
            this.pltDataScatterPlot.Legends.Add(legend1);
            this.pltDataScatterPlot.Location = new System.Drawing.Point(171, 0);
            this.pltDataScatterPlot.Name = "pltDataScatterPlot";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.pltDataScatterPlot.Series.Add(series1);
            this.pltDataScatterPlot.Size = new System.Drawing.Size(932, 587);
            this.pltDataScatterPlot.TabIndex = 0;
            this.pltDataScatterPlot.Text = "chart1";
            // 
            // dgvSuggestedValues
            // 
            this.dgvSuggestedValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSuggestedValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuggestedValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnMAF,
            this.clmnMult});
            this.dgvSuggestedValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuggestedValues.Location = new System.Drawing.Point(0, 0);
            this.dgvSuggestedValues.Name = "dgvSuggestedValues";
            this.dgvSuggestedValues.Size = new System.Drawing.Size(171, 587);
            this.dgvSuggestedValues.TabIndex = 1;
            // 
            // clmnMAF
            // 
            this.clmnMAF.HeaderText = "MAF (V)";
            this.clmnMAF.Name = "clmnMAF";
            this.clmnMAF.ReadOnly = true;
            this.clmnMAF.Width = 70;
            // 
            // clmnMult
            // 
            this.clmnMult.HeaderText = "Multiplier";
            this.clmnMult.Name = "clmnMult";
            this.clmnMult.ReadOnly = true;
            this.clmnMult.Width = 73;
            // 
            // DataDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 587);
            this.Controls.Add(this.dgvSuggestedValues);
            this.Controls.Add(this.pltDataScatterPlot);
            this.Name = "DataDisplayForm";
            this.Text = "DataDisplayForm";
            this.Load += new System.EventHandler(this.DataDisplayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pltDataScatterPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestedValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart pltDataScatterPlot;
        private System.Windows.Forms.DataGridView dgvSuggestedValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMAF;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMult;
    }
}