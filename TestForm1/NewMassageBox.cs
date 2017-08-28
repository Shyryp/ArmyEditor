using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TestForm1
{
    public partial class NewMassageBox : Form
    {
        public Form2 formParent2;
        public Form1 formParent1;
        public NewMassageBox(Form2 form2, Form1 form1)
        {
            InitializeComponent();
            formParent1 = form1;
            formParent2 = form2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            formParent2.formLive = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formParent1.saveInFile();
            formParent2.formLive = 0;
            this.Close();
        }
    }
}
