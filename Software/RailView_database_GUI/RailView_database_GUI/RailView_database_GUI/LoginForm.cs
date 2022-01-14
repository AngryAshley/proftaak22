using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    public partial class LoginForm : Form
    {
        // Fields
        private string username;
        private string password;

        // Properties
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txbPassword.KeyDown += new KeyEventHandler(tb_KeyDown);
            txbUsername.KeyDown += new KeyEventHandler(tb_KeyDown);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=" + txbUsername.Text + ";Pwd=" + txbPassword.Text + ";Convert Zero Datetime=true;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            Username = txbUsername.Text;
            Password = txbPassword.Text;

            try
            {
                //Open dashboard with Username and Password
                this.Hide();
                conn.Open();
                DashboardForm dashboard = new DashboardForm(Username, Password);
                dashboard.ShowDialog();
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
