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
    public partial class Training : Form
    {
        public PictureBox[] picture = new PictureBox[10];
        public int ind = 0;
        public Training()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                this.picture[i] = new PictureBox();
                this.picture[i].Dock = System.Windows.Forms.DockStyle.Fill;
                this.picture[i].Location = new System.Drawing.Point(0, 0);
                this.picture[i].Name = "picture" + i;
                this.picture[i].Load(@"dev\training"+ (i+1) +".png");
                this.picture[i].Size = new System.Drawing.Size(1134, 699);
                this.picture[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                this.picture[i].TabIndex = 2;
                this.picture[i].TabStop = false;
                this.Controls.Add(picture[i]);
            }
            this.picture[0].BringToFront();
            back.Enabled = false;
        }

        private void next_Click(object sender, EventArgs e)
        {
            ind++;
            this.picture[ind].BringToFront();
            if (ind > 0)
            {
                back.Enabled = true;
            }
            if (ind == 4)
            {
                next.Enabled = false;
            }
            
        }

        private void back_Click(object sender, EventArgs e)
        {
            ind--;
            this.picture[ind].BringToFront();
            if (ind == 0)
            {
                back.Enabled = false;
            }
            if (ind < 4)
            {
                next.Enabled = true;
            }
        }
    }
}
