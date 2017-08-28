using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm1
{
    public partial class EditTextForm : Form
    {
        public Form1 fatherForm;
        public EditTextForm(Form1 father, string textbuff)
        {
            InitializeComponent();
            textBox1.Text = textbuff;
            fatherForm = father;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fatherForm.editTextBox.Text = this.textBox1.Text;
            this.Close();
        }
    }
}
