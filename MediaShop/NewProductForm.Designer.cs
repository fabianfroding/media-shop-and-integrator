namespace MediaShop
{
    partial class NewProductForm
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
            this.TextBoxPrice = new System.Windows.Forms.TextBox();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.LabelProductType = new System.Windows.Forms.Label();
            this.LabelNewProduct = new System.Windows.Forms.Label();
            this.BTNAdd = new System.Windows.Forms.Button();
            this.BTNCancel = new System.Windows.Forms.Button();
            this.ComboBoxProductTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TextBoxPrice
            // 
            this.TextBoxPrice.Location = new System.Drawing.Point(124, 72);
            this.TextBoxPrice.Name = "TextBoxPrice";
            this.TextBoxPrice.Size = new System.Drawing.Size(100, 26);
            this.TextBoxPrice.TabIndex = 8;
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(124, 37);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(100, 26);
            this.TextBoxName.TabIndex = 7;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.ForeColor = System.Drawing.Color.White;
            this.LabelName.Location = new System.Drawing.Point(8, 40);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(114, 20);
            this.LabelName.TabIndex = 10;
            this.LabelName.Text = "Product Name:";
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.ForeColor = System.Drawing.Color.White;
            this.LabelPrice.Location = new System.Drawing.Point(8, 75);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(48, 20);
            this.LabelPrice.TabIndex = 11;
            this.LabelPrice.Text = "Price:";
            // 
            // LabelProductType
            // 
            this.LabelProductType.AutoSize = true;
            this.LabelProductType.ForeColor = System.Drawing.Color.White;
            this.LabelProductType.Location = new System.Drawing.Point(8, 104);
            this.LabelProductType.Name = "LabelProductType";
            this.LabelProductType.Size = new System.Drawing.Size(106, 20);
            this.LabelProductType.TabIndex = 12;
            this.LabelProductType.Text = "Product Type:";
            // 
            // LabelNewProduct
            // 
            this.LabelNewProduct.AutoSize = true;
            this.LabelNewProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNewProduct.ForeColor = System.Drawing.Color.White;
            this.LabelNewProduct.Location = new System.Drawing.Point(7, 9);
            this.LabelNewProduct.Name = "LabelNewProduct";
            this.LabelNewProduct.Size = new System.Drawing.Size(134, 25);
            this.LabelNewProduct.TabIndex = 13;
            this.LabelNewProduct.Text = "New Product";
            // 
            // BTNAdd
            // 
            this.BTNAdd.Location = new System.Drawing.Point(12, 139);
            this.BTNAdd.Name = "BTNAdd";
            this.BTNAdd.Size = new System.Drawing.Size(108, 28);
            this.BTNAdd.TabIndex = 14;
            this.BTNAdd.Text = "Add Product";
            this.BTNAdd.UseVisualStyleBackColor = true;
            this.BTNAdd.Click += new System.EventHandler(this.BTNAddProduct_Click);
            // 
            // BTNCancel
            // 
            this.BTNCancel.Location = new System.Drawing.Point(126, 139);
            this.BTNCancel.Name = "BTNCancel";
            this.BTNCancel.Size = new System.Drawing.Size(86, 28);
            this.BTNCancel.TabIndex = 15;
            this.BTNCancel.Text = "Cancel";
            this.BTNCancel.UseVisualStyleBackColor = true;
            this.BTNCancel.Click += new System.EventHandler(this.BTNCancel_Click);
            // 
            // ComboBoxProductTypes
            // 
            this.ComboBoxProductTypes.FormattingEnabled = true;
            this.ComboBoxProductTypes.Location = new System.Drawing.Point(124, 104);
            this.ComboBoxProductTypes.Name = "ComboBoxProductTypes";
            this.ComboBoxProductTypes.Size = new System.Drawing.Size(121, 28);
            this.ComboBoxProductTypes.TabIndex = 16;
            // 
            // NewProductForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(265, 186);
            this.Controls.Add(this.ComboBoxProductTypes);
            this.Controls.Add(this.BTNCancel);
            this.Controls.Add(this.BTNAdd);
            this.Controls.Add(this.LabelNewProduct);
            this.Controls.Add(this.LabelProductType);
            this.Controls.Add(this.LabelPrice);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.TextBoxPrice);
            this.Controls.Add(this.TextBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Shop - New Product";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBoxPrice;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelPrice;
        private System.Windows.Forms.Label LabelProductType;
        private System.Windows.Forms.Label LabelNewProduct;
        private System.Windows.Forms.Button BTNAdd;
        private System.Windows.Forms.Button BTNCancel;
        private System.Windows.Forms.ComboBox ComboBoxProductTypes;
    }
}