using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txbUsername.Text = "Username";
            txbPassword.PasswordChar = '*';
            btnLogin.TabStop = false;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            txbPassword.KeyDown += new KeyEventHandler(tb_KeyDown);
            txbUsername.KeyDown += new KeyEventHandler(tb_KeyDown);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + txbUsername.Text + ";Pwd=" + txbPassword.Text + ";Convert Zero Datetime=true;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                Dashboard dahsboard = new Dashboard();
                this.Hide();
                dahsboard.ShowDialog();
                conn.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("#" + ex.Number.ToString() + ": " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }


        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
