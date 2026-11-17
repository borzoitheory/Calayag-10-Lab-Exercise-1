using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Inventory.ProductClass;

namespace Inventory
{

    public partial class frmAddProduct : Form
    {
        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }

        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList;
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        private void gridViewProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = {
        "Beverages",
        "Bread/Bakery",
        "Canned/Jarred Goods",
        "Dairy",
        "Frozen Goods",
        "Meat",
        "Personal Care",
        "Other" };

            foreach (string variableName in ListOfProductCategory)
            {
                cbCategory.Items.Add(variableName);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                throw new StringFormatException("Invalid Product Name, alphabetical characters only.");
            }
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
            {
                throw new NumberFormatException("Invalid Quantity, numeric values only.");
            }
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Invalid Selling Price, please enter a decimal number.");
            }
            return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                    _ExpDate, _SellPrice, _Quantity, _Description));

                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductName.Clear();
                txtProductName.Focus();
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuantity.Clear();
                txtQuantity.Focus();
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSellPrice.Clear();
                txtSellPrice.Focus();
            }
        }
    }
}
