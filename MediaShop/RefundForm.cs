using MediaShop.Controllers;
using MediaShop.Models;
using System;
using System.Windows.Forms;

namespace MediaShop
{
    public partial class RefundForm : Form
    {
        ReceiptController receiptController;
        ProductController productController;
        Receipt selectedReceipt;

        public RefundForm()
        {
            InitializeComponent();
            receiptController = new ReceiptController();
            productController = new ProductController();
            ListReceipts();
        }

        private void BTNBack_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        // Denna funktion kräver att användaren först har valt ett kvitto från receipt-listview, och
        // sedan även valt en produkt från det valda kvittot.
        // När användaren sedan klickar Refund bekräftas återköpet och produkten tas bort från kvittot och
        // produktens lagerstatus(stock) uppdateras (OM produkten inte tagits bort från lagret).
        private void BTNRefund_Click(object sender, EventArgs e)
        {
            ListViewItem selectedProductItem = null;
            try
            {
                selectedProductItem = ListViewReceiptProducts.SelectedItems[0];
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine("Error when getting selected product item.");
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            if (selectedReceipt != null && selectedProductItem != null)
            {
                int.TryParse(selectedProductItem.Tag.ToString(), out int id);
                double price = 0.0;

                foreach (Product p in selectedReceipt.products)
                {
                    if (p.id == id)
                    {
                        price = p.price;
                        selectedReceipt.products.Remove(p);
                        break;
                    }
                }
                receiptController.Update(selectedReceipt);

                Product product = productController.GetById(id);
                if (product != null)
                {
                    product.stock++;
                    productController.Update(product);
                }

                if (selectedReceipt.products.Count > 0)
                {
                    ListReceiptProducts();
                }
                else
                {
                    receiptController.Remove(selectedReceipt);
                    selectedReceipt = null;
                    ListViewReceiptProducts.Clear();
                    ListReceipts();
                }

                MessageBox.Show(selectedProductItem.SubItems[0].Text + " was refunded for " + price.ToString() + " SEK.");
            }
            else
            {
                MessageBox.Show("Select a product from receipt to refund it.");
            }
        }

        // Funktion som anropas varje gång användaren väljer ett kvitto.
        // När ett kvitto valts uppdateras listview med produkter från det valda kvittot automatiskt.
        private void ListViewReceipts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListReceiptProducts();
        }

        // Fyller listview för kvitton med kvitton.
        private void ListReceipts()
        {
            ListViewReceipts.Items.Clear();
            ListViewReceipts.BeginUpdate();
            foreach (Receipt receipt in receiptController.GetAll())
            {
                string[] receiptValues = new string[1];
                receiptValues[0] = receipt.date;
                ListViewReceipts.Items.Add(new ListViewItem(receiptValues));
            }
            ListViewReceipts.EndUpdate();
            ListViewReceipts.Sort();
        }

        // Fyller listview för kvittots produkter med kvittots produkter.
        private void ListReceiptProducts()
        {
            ListViewItem selectedItem = null;
            try
            {
                selectedItem = ListViewReceipts.SelectedItems[0];
            }
            catch (ArgumentOutOfRangeException exc)
            {
                System.Diagnostics.Debug.WriteLine("Error when selecting receipt.");
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }

            if (selectedItem != null)
            {
                string receiptDate = selectedItem.SubItems[0].Text;
                Receipt receipt = receiptController.GetByDate(receiptDate);
                selectedReceipt = receipt;

                ListViewReceiptProducts.Items.Clear();
                ListViewReceiptProducts.BeginUpdate();
                foreach (Product product in receipt.products)
                {
                    try
                    {
                        string[] productValues = new string[1];
                        productValues[0] = product.name;
                        ListViewItem item = new ListViewItem(productValues);
                        item.Tag = product.id;
                        ListViewReceiptProducts.Items.Add(item);
                    }
                    catch (Exception exc)
                    {
                        System.Diagnostics.Debug.WriteLine(exc.Message);
                    }
                }

                ListViewReceiptProducts.EndUpdate();
                ListViewReceiptProducts.Sort();
            }
        }

    }
}
