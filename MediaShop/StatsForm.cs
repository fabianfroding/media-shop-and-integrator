using MediaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MediaShop
{
    // Denna klass visar grafer som reflekterar top 10 mest sålda produkter eller
    // produkter sålda under en viss tidsintervall.
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
        }

        private void BTNExit_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        // Skapar en graf som illustrerar de 10 mest sålda produkterna.
        public void DrawTop10Graph(List<Product> products, List<Receipt> receipts)
        {
            // För varje produkt i lagret, kollar vid om den produkten finns i något av alla kvitton
            // Om den produkten finns i lagret, uppdaterar vi den produktens unitsSold.
            foreach (Product p in products)
            {
                foreach (Receipt r in receipts)
                {
                    foreach (Product rP in r.products)
                    {
                        if (p.id == rP.id)
                        {
                            p.unitsSold++;
                        }
                    }
                }
            }
            // Listan med produkter sorteras.
            products = products.OrderByDescending(p => p.unitsSold).ToList<Product>();

            // Förbered grafens egenskaper.
            StatsChart.Series[0].Name = "";
            StatsChart.ChartAreas[0].AxisX.Title = "Products";
            StatsChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            StatsChart.ChartAreas[0].AxisX.Interval = 1;
            StatsChart.ChartAreas[0].AxisY.Title = "Units Sold";
            StatsChart.ChartAreas[0].AxisY.Interval = 1;
            StatsChart.ChartAreas[0].AxisY.IntervalOffset = 1;
            StatsChart.ChartAreas[0].AxisY.ScaleView.Size = 10;

            // Lista de 10 första produkterna i listan till grafen.
            int count = 0;
            foreach(Product p in products)
            {
                StatsChart.Series[0].Points.AddXY(p.name, p.unitsSold);
                count++;
                if (count > 9)
                {
                    break;
                }
            }
        }

        // Skapar en graf som visar produkter sålda under en viss tidsintervall.
        public void DrawSalesGraph(Product product, List<Receipt> receipts)
        {
            // Förbered grafens egenskaper.
            StatsChart.Series[0].Name = product.name;
            StatsChart.ChartAreas[0].AxisX.Title = "YYYYMM";
            StatsChart.ChartAreas[0].AxisY.Title = "Units Sold";
            StatsChart.ChartAreas[0].AxisY.Interval = 1;
            StatsChart.ChartAreas[0].AxisY.IntervalOffset = 1;
            StatsChart.ChartAreas[0].AxisY.ScaleView.Size = 10;

            // Här generar vi en kopia av alla kvitton.
            // Om kvitton har samma datum (yy,mm,dd) så sätter vi det ena kvittot till null
            // och kopierar alla det kvittots produkter till det första kvittot
            // Detta fr att göra oss av med duplicates datum i grafen.
            Receipt[] receiptsCopy = receipts.ToArray();
            for (int i = 0; i < receiptsCopy.Length; i++)
            {
                if (receiptsCopy[i] != null)
                {
                    for (int j = i + 1; j < receiptsCopy.Length; j++)
                    {
                        if (receiptsCopy[i].date.Substring(0, 6) == receiptsCopy[j].date.Substring(0, 6))
                        {
                            receiptsCopy[i].products.AddRange(receiptsCopy[j].products);
                            receiptsCopy[j] = null;
                        }
                    }
                    int numSales = 0;
                    foreach (Product p in receiptsCopy[i].products)
                    {
                        if (p.id == product.id)
                        {
                            numSales++;
                        }
                    }

                    // För att göra y.axeln dynamisk beroede på mängden sålda varor baserar vid y axeln
                    // på max antal sålda varor.
                    if (numSales > StatsChart.ChartAreas[0].AxisY.ScaleView.Size)
                    {
                        StatsChart.ChartAreas[0].AxisY.Interval = 1 + (int)(numSales * 0.1);
                        StatsChart.ChartAreas[0].AxisY.IntervalOffset = 1 + (int)(numSales * 0.1);
                        StatsChart.ChartAreas[0].AxisY.ScaleView.Size = numSales + (int)(numSales * 0.1);
                    }
                    if (numSales > 0)
                    {
                        StatsChart.Series[0].Points.AddXY(receiptsCopy[i].date.Substring(0, 6), numSales);
                    }
                }
            }
        }

    }
}
