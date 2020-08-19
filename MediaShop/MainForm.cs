using MediaShop.Controllers;
using MediaShop.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MediaShop
{
    public partial class MainForm : Form
    {
        private static readonly string exportPath = @"..\..\bin\Debug\Export\products.txt";
        private static readonly string importPath = @"..\..\bin\Debug\Import\";

        //private ProductController productController;
        private ReceiptController receiptController;
        // cartProducts är en temporär lista som innehåller de produkter som läggs i cart i kassavyn.
        private List<Product> cartProducts;

        public MainForm()
        {
            InitializeComponent();
            //productController = new ProductController();
            receiptController = new ReceiptController();
            cartProducts = new List<Product>();
            ProductController productController = new ProductController();
            ListProducts(productController.GetAll());
            ListProductTypesInComboBox(ComboBoxSearchProductTypes);
        }

        // Filtrerar listan med produkter baserat på sök-kriterier som användaren angett.
        private void BTNSearch_Click(object sender, EventArgs e)
        {
            string[] productValues = new string[]
            {
                TextBoxSearchName.Text,
                SearchPriceMin.Text,
                SearchPriceMax.Text,
                SearchStockMin.Text,
                SearchStockMax.Text,
                ComboBoxSearchProductTypes.SelectedItem.ToString()
            };
            ListProducts(FindProducts(productValues));
        }

        // Om användaren stänger programmet medans det finns produkter i varukorgen läggs
        // dessa tillbaka till lagret.
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cartProducts.Count > 0)
            {
                foreach (Product product in cartProducts)
                {
                    ProductController productController = new ProductController();
                    Product dbProduct = productController.GetById(product.id);
                    dbProduct.stock++;
                    productController.Update(dbProduct);
                }
            }
        }

        private void BTNTop10_Click(object sender, EventArgs e)
        {
            // För att se hur detta fungerar, gå till StatsForm-klassens dokumentation.
            ProductController productController = new ProductController();
            StatsForm statsForm = new StatsForm();
            statsForm.DrawTop10Graph(productController.GetAll(), receiptController.GetAll());
            statsForm.Show();
        }

        private void BTNViewSales_Click(object sender, EventArgs e)
        {
            // För att se hur detta fungerar, gå till StatsForm-klassens dokumentation.
            Product selectedProduct = GetSelectedProductFromListView(ListViewProducts);
            if (selectedProduct != null)
            {
                List<Receipt> receiptsFound = receiptController.GetAllBetweenDates(
                DateTimePickerFrom.Value.ToString("yyyyMMdd"),
                DateTimePickerTo.Value.ToString("yyyyMMdd"));
                StatsForm statsForm = new StatsForm();
                statsForm.DrawSalesGraph(selectedProduct, receiptsFound);
                statsForm.Show();
            }
            else
            {
                MessageBox.Show("Select a product to view sales.");
            }
        }

        private void BTNExportProducts_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();
            if (productController.ExportProducts(exportPath))
            {
                MessageBox.Show("Products exported to " + exportPath);
            }
            else
            {
                MessageBox.Show("There was a problem exporting the products.");
            }
        }

        private void BTNImportProducts_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();
            productController.ImportProducts(importPath);
            ListProducts(productController.GetAll());
        }


        //--------------- Cashier Tab Interactives ---------------//
        // Denna funktion lägger till en produkt i carten.
        // Detta kräver att användaren vlat en produkt från listan först.
        private void BTNAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                ProductController productController = new ProductController();
                ListViewItem selectedItem = ListViewProducts.SelectedItems[0];
                ListViewItem cartItem = (ListViewItem)selectedItem.Clone();
                int.TryParse(cartItem.SubItems[1].Text, out int id);
                Product selectedProduct = productController.GetById(id);

                // Kolla så att produkten finns i lagret, annars meddela användaren att den är slut.
                if (selectedProduct.stock > 0)
                {
                    ListViewCart.Items.Add(cartItem);
                    cartProducts.Add(selectedProduct);
                    ListProductsInCart();
                    selectedProduct.stock--;
                    productController.Update(selectedProduct);
                    BTNSearch.PerformClick();
                }
                else
                {
                    MessageBox.Show("Product is out of stock.");
                }
            }
            catch (ArgumentOutOfRangeException exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                MessageBox.Show("Select a product to add it to the cart.");
            }
        }

        // Ger användaren bekräftelse på att köpet genomförts, och skapar ett kvitto med de köpta produkterna.
        private void BTNCheckout_Click(object sender, EventArgs e)
        {
            // Kolla först så att carten inte är tom.
            if (cartProducts.Count > 0)
            {
                double totalPrice = 0;
                string products = "";
                foreach (Product product in cartProducts)
                {
                    totalPrice += product.price;
                    products += product.name + ":    " + product.price.ToString() + " SEK" + "\n";
                }
                DialogResult dR = MessageBox.Show(products + "\nTotal price: " + totalPrice.ToString() + " SEK", "Checkout", MessageBoxButtons.OKCancel);
                if (dR == DialogResult.OK)
                {
                    MessageBox.Show("Get receipt?", "Receipt", MessageBoxButtons.OK);

                    // Skapa kvitto
                    Receipt receipt = new Receipt();
                    foreach (Product product in cartProducts)
                    {
                        receipt.products.Add(product);
                    }
                    receiptController.Add(receipt);

                    // Töm carten.
                    cartProducts.Clear();
                    // Uppdatera listan så att vi ser att stock i köpta produkter minskat.
                    BTNSearch.PerformClick();
                    ListProductsInCart();
                    MessageBox.Show("Purchase done!");
                }
            }
            else
            {
                MessageBox.Show("Cart is empty.");
            }
        }

        // Öppnar RefundForm för återköp av produkter. Se  RefundForm för mer detaljer.
        private void BTNRefund_Click(object sender, EventArgs e)
        {
            RefundForm refundForm = new RefundForm();
            refundForm.FormClosed += RefundFormClosed;
            refundForm.ShowDialog();
        }



        //--------------- Storage Tab Interactives ---------------//
        // Öppnar NewProductForm, se NewProductForm för mer detlajer.
        private void BTNNewProduct_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();
            new NewProductForm().ShowDialog();
            // När NewProductForm stängs uppdaterar vi listan så att vi ser den nya produkten.
            ListProducts(productController.GetAll());
        }

        // Tar bort en produkt. Kräver att användaren valt en produkt från listan först.
        private void BTNRemoveProduct_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();
            Product product = GetSelectedProductFromListView(ListViewProducts);
            // Kolla så att användare valt en produkt från listan.
            if (product != null)
            {
                // Kontrollfunktion om produkten verkligen vill tas bort. Om stock > 0 måste användaren
                // bekräfta att produkten ska tas bort. Om stock <= 0 tas produkten bort direkt.
                if (product.stock > 0)
                {
                    if (MessageBox.Show(
                        "The product \"" + product.name + "\" still have units in stock. Are you sure you want to remove it?",
                        "Confirm Removal",
                        MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        RemoveProduct(product);
                        BTNSearch.PerformClick();
                    }
                }
                else
                {
                    RemoveProduct(product);
                    BTNSearch.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Select a product to remove.");
            }
        }

        // Öppnar en inputbox som tillåter användaren att specifiera hur många exemplar
        // som ska läggas till för en produkt.
        // "Leverans från grossit"
        private void BTNAddStock_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();
            Product product = GetSelectedProductFromListView(ListViewProducts);
            // Kolla så att en produkt valts.
            if (product != null)
            {
                string input = Interaction.InputBox("Specify stock increment amount for \"" + product.name + "\"", "Add stock", "1", -1, -1);
                if (int.TryParse(input, out int stock) && stock > 0)
                {
                    if (!(product.stock + stock > 9999))
                    {
                        product.stock += stock;
                        productController.Update(product);
                        BTNSearch.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Max stock is 9999");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a product to increment stock.");
            }
        }



        //=============== Private Methods ===============//
        // Fyller listview för produkter med produkter. Funktionen tar emot en lista eftersom
        // listview kan filtreras med sök-funktionen, eller enkelt bara ta alla produkter från lagret.
        private void ListProducts(List<Product> products)
        {
            ListViewProducts.Items.Clear();
            ListViewProducts.BeginUpdate();
            foreach (Product product in products)
            {
                string[] productValues = new string[5];
                productValues[0] = product.name;
                productValues[1] = product.id.ToString();
                productValues[2] = product.price.ToString();
                productValues[3] = product.stock.ToString();
                productValues[4] = product.productType.ToString();
                ListViewProducts.Items.Add(new ListViewItem(productValues));
            }
            ListViewProducts.EndUpdate();
            ListViewProducts.Sort();
        }

        // Fyller dropdown-menyn i sökfunktionerna med product-types så att användaren kan välja
        // att filtrera produkter efter type.
        private void ListProductTypesInComboBox(ComboBox cB)
        {
            cB.BeginUpdate();
            foreach (Product.ProductType productType in Enum.GetValues(typeof(Product.ProductType)))
            {
                cB.Items.Add(productType);
            }
            cB.Items.Add("ALL");
            cB.EndUpdate();
            cB.SelectedIndex = cB.Items.Count - 1;
        }

        // Fyller cart-listview med produkterna som finns i cartProducts.
        private void ListProductsInCart()
        {
            ListViewCart.Items.Clear();
            ListViewCart.BeginUpdate();
            foreach (Product product in cartProducts)
            {
                string[] productValues = new string[3];
                productValues[0] = product.name;
                productValues[1] = product.price.ToString();
                productValues[2] = product.productType.ToString();
                ListViewCart.Items.Add(new ListViewItem(productValues));
            }
            ListViewCart.EndUpdate();
        }

        // Återger den för tllfäller markerade/valda produkten från listviewen.
        private Product GetSelectedProductFromListView(ListView listView)
        {
            try
            {
                ListViewItem selectedItem = listView.SelectedItems[0];
                if (selectedItem != null)
                {
                    int.TryParse(selectedItem.SubItems[1].Text, out int id);
                    ProductController productController = new ProductController();
                    return productController.GetById(id);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Diagnostics.Debug.WriteLine("There was a problem getting the selected product.\n" + e.Message);
            }
            return null;
        }

        // Funktion som hanterar borttagning av produkt.
        // Kontroll så att det inte var något problem i controller eller repository lagret utförs också.
        private void RemoveProduct(Product product)
        {
            ProductController productController = new ProductController();
            if (productController.Remove(product))
            {
                MessageBox.Show("Product succesfully removed.");
            }
            else
            {
                MessageBox.Show("There was a problem removing the product.");
            }
        }

        // Funktion som uppdaterar listan med produkter efter att ett återköp genomförts.
        // Ifall den returnerade produkten finns i lagret vill vi direkt kunna se att
        // lagerstatus uppdaterats.
        private void RefundFormClosed(object sender, FormClosedEventArgs e)
        {
            ProductController productController = new ProductController();
            ListProducts(productController.GetAll());
        }

        // Skapar en lista med produkter baserat på de värden som skickas till funktionen
        // Och återger en lista med produkter som matchar dessa världen.
        private List<Product> FindProducts(string[] values)
        {
            ProductController productController = new ProductController();
            List<Product> products = productController.GetAll();
            List<Product> productsFound = new List<Product>();

            string searchName = values[0];
            double.TryParse(values[1], out double searchPriceMin);
            double.TryParse(values[2], out double searchPriceMax);
            int.TryParse(values[3], out int searchStockMin);
            int.TryParse(values[4], out int searchStockMax);
            string searchProductType = values[5];

            foreach (Product product in products)
            {
                bool matches = true;
                if (!product.name.ToLower().Contains(searchName.ToLower()))
                {
                    matches = false;
                }
                if (product.price < searchPriceMin || product.price > searchPriceMax)
                {
                    matches = false;
                }
                if (product.stock < searchStockMin || product.stock > searchStockMax)
                {
                    matches = false;
                }
                if (searchProductType != "ALL" && product.productType.ToString() != searchProductType)
                {
                    matches = false;
                }

                if (matches)
                {
                    productsFound.Add(product);
                }
            }

            return productsFound;
        }
    }
}
