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
    public partial class SaveTest : Form
    {
        public Form1 Father;
        public SaveTest(Form1 father)
        {
            Father = father;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Father.nameFile = Application.StartupPath+ @"\SaveTests\"+textBox1.Text+".xml";
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
