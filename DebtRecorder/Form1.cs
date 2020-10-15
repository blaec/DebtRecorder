using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebtRecorder
{
    public partial class Form1 : Form
    {
        Guy joe;
        Guy bob;
        int bank = 100;
        public Form1()
        {
            InitializeComponent();
            joe = new Guy() { Cash = 50, Name = "Joe" };
            bob = new Guy() { Cash = 50, Name = "Bob" };
            UpdateForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bank >= 10)
            {
                bank -= joe.ReceiveCash(10);
                UpdateForm();
            }
            else
            {
                MessageBox.Show("The bank is out of money");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bank += bob.GiveCash(5);
            UpdateForm();
        }

        public void UpdateForm()
        {
            joesCashLabel.Text = $"{joe.Name} has ${joe.Cash}";
            bobsCashLabel.Text = $"{bob.Name} has ${bob.Cash}";
            bankCashLabel.Text = $"The bank has ${bank}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (joe.Cash >= 10)
            {
                bob.ReceiveCash(joe.GiveCash(10));
                UpdateForm();
            }
            else
            {
                MessageBox.Show("Sorry, I'm broke! 😪");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bob.Cash >= 5)
            {
                joe.ReceiveCash(bob.GiveCash(5));
                UpdateForm();
            }
            else
            {
                MessageBox.Show("Sorry, I'm broke! 😪");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using(Stream output = File.Create("Guy_File.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, joe);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (Stream input = File.OpenRead("Guy_File.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                joe = (Guy)formatter.Deserialize(input);
            }
            UpdateForm();
        }
    }
}
