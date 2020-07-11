using MediaShop.Controllers;
using MediaShop.Models;
using System;
using System.Windows.Forms;

namespace MediaShop
{
    public partial class NewProductForm : Form
    {
        private ProductController productController;

        public NewProductForm()
        {
            InitializeComponent();
            productController = new ProductController();
            ListProductTypes();
        }

        // Denna metod skapar en ny instans av en Produkt baserat på inputs från användaren.
        // Användarens inputs valideras och sedan skickas produkten till controller- och 
        // repository lagret och sparas ned till fil.
        private void BTNAddProduct_Click(object sender, EventArgs e)
        {
            if (IsInputValid())
            {
                if (CheckPriceLimit())
                {
                    Product product = new Product();

                    // Här tas | bort eftersom den används som separator för klassens attributes i
                    // lagringsfilen.
                    // Detta betyder att användaren kan inkludera | i produktens namn, men det kommer
                    // tas bort automatiskt när produkten sparas.
                    product.name = TextBoxName.Text.Replace("|", "");

                    double.TryParse(TextBoxPrice.Text, out double productPrice);
                    product.price = productPrice;
                    product.productType = (Product.ProductType)ComboBoxProductTypes.SelectedItem;

                    // Om productController återger false var det något problem och produkten sparas då inte.
                    if (productController.Add(product))
                    {
                        MessageBox.Show("Product succesfully added.");
                        Form.ActiveForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("There was a problem adding the product.");
                    }

                    Form.ActiveForm.Close();
                }
                else
                {
                    MessageBox.Show("Max price is 99999");
                }
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void BTNCancel_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        // Skapar en dropdown-meny där användaren får välja vilken product-type den nya produkten ska ha.
        private void ListProductTypes()
        {
            ComboBoxProductTypes.Items.Clear();
            ComboBoxProductTypes.BeginUpdate();
            foreach (var productType in Enum.GetValues(typeof(Product.ProductType)))
            {
                ComboBoxProductTypes.Items.Add(productType);
            }
            ComboBoxProductTypes.EndUpdate();
        }

        // Denna metod kollar så att användaren inte matar inte felaktiga värden som t.ex. bokstäver
        // i produktens pris.
        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(TextBoxName.Text) || string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                return false;
            }
            if (ComboBoxProductTypes.SelectedItem == null)
            {
                return false;
            }
            if (!double.TryParse(TextBoxPrice.Text, out double temp))
            {
                return false;
            }
            return true;
        }

        // Denna funktion begränsar en prdukts pris för att förhindra orimliga höga siffror.
        private bool CheckPriceLimit()
        {
            double.TryParse(TextBoxPrice.Text, out double temp);
            if (temp > 99999)
            {
                return false;
            }
            return true;
        }
    }
}
