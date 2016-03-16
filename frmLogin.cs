using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Everyday
{
    public partial class frmLogin : Form
    {
    
        public frmLogin()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (((UsernameTextBox.Text == "") || (PasswordTextBox.Text == "")))
            {
                MessageBox.Show("Введите логин и пароль!");
            }
            else
            {
                //Everyday everyday = new Everyday(UsernameTextBox.Text,PasswordTextBox.Text);
                Everyday.Login(UsernameTextBox.Text, PasswordTextBox.Text);
                frmMain f = new frmMain();
                f.Show();
                this.Hide();

                //if (everyday.SUCCESS == 1)
                //{
                //}
            }

        }
    }
}
