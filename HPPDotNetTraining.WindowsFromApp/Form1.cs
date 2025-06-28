using System.Windows.Forms;

namespace HPPDotNetTraining.WindowsFromApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadProductsData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Name = txtName.Text.Trim(),
                Quantity = int.Parse(txtQuantity.Text.Trim()),
                Price = decimal.Parse(txtPrice.Text.Trim()),
                CreateDateTime = DateTime.Now,
                ModifiedDateTime = null
            };
            var _repo = new ProductRepository();
            _repo.AddProduct(product);
            MessageBox.Show("Product saved successfully.");
            ClearControl();
            LoadProductsData();
        }

        private void ClearControl()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtName.Focus();
        }

        private void LoadProductsData()
        {
            var _repo = new ProductRepository();
            var products = _repo.GetAllProducts();
            dgvData.DataSource = _repo.GetAllProducts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to update");
                return;
            }
            var product = new Product
            {
                Id = selectedProductId,
                Name = txtName.Text.Trim(),
                Quantity = int.Parse(txtQuantity.Text.Trim()),
                Price = decimal.Parse(txtPrice.Text.Trim()),
                ModifiedDateTime = DateTime.Now
            };
            var _repo = new ProductRepository();
            _repo.UpdateProduct(product);
            MessageBox.Show("Product updated successfully.");
            ClearControl();
            LoadProductsData();
            selectedProductId = 0;

        }
        private int selectedProductId = 0;
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvData.Rows[e.RowIndex];
                selectedProductId = Convert.ToInt32(row.Cells["Id"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId==0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }
            var confirm = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo);
            if(confirm==DialogResult.Yes)
            {
                var _repo=new ProductRepository();
                _repo.DeleteProduct(selectedProductId);
                MessageBox.Show("Product deleted successfully.");
                ClearControl();
                LoadProductsData();
                selectedProductId = 0;
            }

        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

    }
}
