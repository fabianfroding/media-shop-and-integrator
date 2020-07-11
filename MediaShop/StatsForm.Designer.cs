namespace MediaShop
{
    partial class StatsForm
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
            this.BTNExit = new System.Windows.Forms.Button();
            this.StatsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.StatsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNExit
            // 
            this.BTNExit.BackColor = System.Drawing.Color.Firebrick;
            this.BTNExit.FlatAppearance.BorderSize = 0;
            this.BTNExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNExit.ForeColor = System.Drawing.Color.White;
            this.BTNExit.Location = new System.Drawing.Point(1166, 12);
            this.BTNExit.Name = "BTNExit";
            this.BTNExit.Size = new System.Drawing.Size(36, 36);
            this.BTNExit.TabIndex = 5;
            this.BTNExit.Text = "X";
            this.BTNExit.UseVisualStyleBackColor = false;
            this.BTNExit.Click += new System.EventHandler(this.BTNExit_Click);
            // 
            // StatsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.StatsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.StatsChart.Legends.Add(legend1);
            this.StatsChart.Location = new System.Drawing.Point(12, 12);
            this.StatsChart.Name = "StatsChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Product Name";
            this.StatsChart.Series.Add(series1);
            this.StatsChart.Size = new System.Drawing.Size(1135, 716);
            this.StatsChart.TabIndex = 6;
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1214, 786);
            this.Controls.Add(this.StatsChart);
            this.Controls.Add(this.BTNExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StatsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatsForm";
            ((System.ComponentModel.ISupportInitialize)(this.StatsChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTNExit;
        private System.Windows.Forms.DataVisualization.Charting.Chart StatsChart;
    }
}