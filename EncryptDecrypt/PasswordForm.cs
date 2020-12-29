using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptDecrypt;

namespace Hit.Services.EncryptDecrypt
{
    public partial class PasswordForm : Form
    {
        int tries = 1;
        JobsConfig jobsConfig;
        public PasswordForm()
        {
            InitializeComponent();
           jobsConfig = new JobsConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == jobsConfig.Scheduler.MainPassword || textBox1.Text == jobsConfig.Scheduler.GuestPassword)
            {
                this.Hide();
                Form1 f1;
               if (textBox1.Text == jobsConfig.Scheduler.MainPassword)
                    f1 = new Form1(true);
               else
                    f1 = new Form1(false);
                f1.ShowDialog();
               this.Show();
                this.Close();
            }
            else
            {
                System.Threading.Thread.Sleep(tries*1500);
                MessageBox.Show("Wrong Password", "Error");
                tries++;
            }

            if (tries >= 5)
            {
                this.Close();
            }
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            this.ActiveControl = textBox1;
            
           // this.Hide();
           // Form1 f1 = new Form1();
           // f1.ShowDialog();
           //this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
