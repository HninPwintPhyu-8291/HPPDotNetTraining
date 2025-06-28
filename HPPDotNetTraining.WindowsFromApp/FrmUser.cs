using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPPDotNetTraining.WindowsFromApp
{
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                UserName = txtUserName.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                MessageBox.Show("UserName and Password are required.");
                return;
            }
            var _userRepository = new UserRepository();
            _userRepository.AddUser(user);
            MessageBox.Show("User added successfully.");
            ClearForm();
            LoadUsers();
        }
        private int selectUserID = 0;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


        }

        private void btnLoadUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            var _userRepository = new UserRepository();
            var users = _userRepository.GetAllUsers();
            dgvUserData.DataSource = _userRepository.GetAllUsers();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClearForm()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            if (selectUserID == 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            var user = new User
            {
                UserId = selectUserID,
                UserName = txtUserName.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };
            var _userRepository = new UserRepository();
            _userRepository.UpdateUser(user);
            MessageBox.Show("User updated successfully.");
            ClearForm();
            LoadUsers();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (selectUserID == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }
            var confirm = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                var _repo = new UserRepository();
                _repo.DeleteUser(selectUserID);
                MessageBox.Show("User deleted successfully.");
                ClearForm();
                LoadUsers();
                selectUserID = 0;
            }
        }

        private void dgvUserData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUserData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUserData.Rows[e.RowIndex];
                selectUserID = Convert.ToInt32(row.Cells["UserID"].Value);
                txtUserName.Text = row.Cells["UserName"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }
    }
    public class User
    {
        public int UserId { get; set; }        // Primary Key
        public string UserName { get; set; }   // Unique or required
        public string Password { get; set; }   // Plain or hashed
    }
}
