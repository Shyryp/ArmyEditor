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
    public partial class Form3 : Form
    {
        private List<DeButton> listButton = new List<DeButton>();
        private List<Label> listLabel = new List<Label>();
        private List<TextBox> listTextBox = new List<TextBox>();
        private List<DeCheckBox> listCheckBox = new List<DeCheckBox>();
        private List<DeRadioButton> listRadioButton = new List<DeRadioButton>();
        private List<PictureBox> listPictureStaticBox = new List<PictureBox>();
        private List<DePictureDinamicBox> listPictureDinamicBox = new List<DePictureDinamicBox>();
        public List<DePictureBox> listPictureBox = new List<DePictureBox>();
        private int TrueTest = 0;
        private int FinalTrue = 0;
        private String nameFile;
        private SerializableForm newFormForSerializable;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string nameFiles)
        {
            InitializeComponent();
            this.nameFile = nameFiles;
            this.openFile();
        }

        private void openFile()
        {
            newFormForSerializable = new SerializableForm();
            XmlSerializer formatter = new XmlSerializer(typeof(SerializableForm));
            using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                newFormForSerializable = (SerializableForm)formatter.Deserialize(fs);
                for (int i = 0; i < newFormForSerializable.seButton.Count; i++)
                {
                    this.listButton.Add(new DeButton()); //создаём кнопку
                    this.listButton[i].Name = newFormForSerializable.seButton[i].Name;
                    this.listButton[i].Text = newFormForSerializable.seButton[i].Text;
                    this.listButton[i].Width = newFormForSerializable.seButton[i].SizeX;
                    this.listButton[i].Height = newFormForSerializable.seButton[i].SizeY;
                    this.listButton[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seButton[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listButton[i].Location = new Point(newFormForSerializable.seButton[i].X, newFormForSerializable.seButton[i].Y);
                    this.listButton[i].index = i;
                    this.listButton[i].trueAnswer = newFormForSerializable.seButton[i].TrueOrFalse;
                    TrueTest += newFormForSerializable.seButton[i].TrueOrFalse;
                    this.listButton[i].Click += new System.EventHandler(this.buttonInTest_Click);
                    this.Controls.Add(listButton[i]);
                    //indexButton++;

                }
                for (int i = 0; i < newFormForSerializable.seLabel.Count; i++)
                {
                    this.listLabel.Add(new Label());
                    this.listLabel[i].Name = newFormForSerializable.seLabel[i].Name;
                    this.listLabel[i].Text = newFormForSerializable.seLabel[i].Text;
                    this.listLabel[i].Width = newFormForSerializable.seLabel[i].SizeX;
                    this.listLabel[i].Height = newFormForSerializable.seLabel[i].SizeY;
                    this.listLabel[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seLabel[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listLabel[i].Location = new Point(newFormForSerializable.seLabel[i].X, newFormForSerializable.seLabel[i].Y);
                    this.Controls.Add(this.listLabel[i]);
                    //this.indexLabel++;

                }
                for (int i = 0; i < newFormForSerializable.seTextBox.Count; i++)
                {
                    this.listTextBox.Add(new TextBox());
                    this.listTextBox[i].Name = newFormForSerializable.seTextBox[i].Name;
                    //this.listTextBox[i].Text = newFormForSerializable.seTextBox[i].Text;
                    this.listTextBox[i].Width = newFormForSerializable.seTextBox[i].SizeX;
                    this.listTextBox[i].Height = newFormForSerializable.seTextBox[i].SizeY;
                    this.listTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seTextBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listTextBox[i].Location = new Point(newFormForSerializable.seTextBox[i].X, newFormForSerializable.seTextBox[i].Y);
                    TrueTest++;
                    this.Controls.Add(this.listTextBox[i]);
                    //this.indexTextBox++;

                }
                for (int i = 0; i < newFormForSerializable.seCheckBox.Count; i++)
                {
                    this.listCheckBox.Add(new DeCheckBox());
                    this.listCheckBox[i].Name = newFormForSerializable.seCheckBox[i].Name;
                    this.listCheckBox[i].Text = newFormForSerializable.seCheckBox[i].Text;
                    this.listCheckBox[i].Width = newFormForSerializable.seCheckBox[i].SizeX;
                    this.listCheckBox[i].Height = newFormForSerializable.seCheckBox[i].SizeY;
                    this.listCheckBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seCheckBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listCheckBox[i].Location = new Point(newFormForSerializable.seCheckBox[i].X, newFormForSerializable.seCheckBox[i].Y);
                    this.listCheckBox[i].index = i;
                    this.listCheckBox[i].trueAnswer = newFormForSerializable.seCheckBox[i].TrueOrFalse;

                    TrueTest += newFormForSerializable.seCheckBox[i].TrueOrFalse;
                    this.listCheckBox[i].Click += new System.EventHandler(this.checkBox_Click);
                    this.Controls.Add(this.listCheckBox[i]);

                    //this.indexCheckBox++;

                }
                for (int i = 0; i < newFormForSerializable.seRadioButton.Count; i++)
                {
                    this.listRadioButton.Add(new DeRadioButton());
                    this.listRadioButton[i].Name = newFormForSerializable.seRadioButton[i].Name;
                    this.listRadioButton[i].Text = newFormForSerializable.seRadioButton[i].Text;
                    this.listRadioButton[i].Width = newFormForSerializable.seRadioButton[i].SizeX;
                    this.listRadioButton[i].Height = newFormForSerializable.seRadioButton[i].SizeY;
                    this.listRadioButton[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seRadioButton[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listRadioButton[i].Location = new Point(newFormForSerializable.seRadioButton[i].X, newFormForSerializable.seRadioButton[i].Y);
                    this.listRadioButton[i].index = i;
                    this.listRadioButton[i].trueAnswer = newFormForSerializable.seRadioButton[i].TrueOrFalse;
                    TrueTest += newFormForSerializable.seRadioButton[i].TrueOrFalse;
                    this.listRadioButton[i].CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
                    this.Controls.Add(this.listRadioButton[i]);
                    //this.indexRadioButton++;

                }
                
                for (int i = 0; i < newFormForSerializable.sePictureStaticBox.Count; i++)
                {

                    this.listPictureStaticBox.Add(new PictureBox());
                    
                    this.listPictureStaticBox[i].Name = newFormForSerializable.sePictureStaticBox[i].Name;
                    this.listPictureStaticBox[i].Text = newFormForSerializable.sePictureStaticBox[i].Text;
                    this.listPictureStaticBox[i].Width = newFormForSerializable.sePictureStaticBox[i].SizeX;
                    this.listPictureStaticBox[i].Height = newFormForSerializable.sePictureStaticBox[i].SizeY;
                    this.listPictureStaticBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.sePictureStaticBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listPictureStaticBox[i].Location = new Point(newFormForSerializable.sePictureStaticBox[i].X, newFormForSerializable.sePictureStaticBox[i].Y);
                    this.listPictureStaticBox[i].Image = Image.FromFile(@newFormForSerializable.sePictureStaticBox[i].wayPictures);
                    this.listPictureStaticBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                    this.Controls.Add(this.listPictureStaticBox[i]);

                    //this.indexPictureStaticBox++;

                }
                for (int i = 0; i < newFormForSerializable.sePictureDinamicBox.Count; i++)
                {
                    this.listPictureDinamicBox.Add(new DePictureDinamicBox(this));
                    this.listPictureBox.Add(new DePictureBox());
                    this.listPictureBox[i].Name = newFormForSerializable.sePictureDinamicBox[i].childName;
                    this.listPictureDinamicBox[i].Name = newFormForSerializable.sePictureDinamicBox[i].Name;
                    this.listPictureBox[i].Text = this.listPictureDinamicBox[i].Text = newFormForSerializable.sePictureDinamicBox[i].Text;
                    this.listPictureDinamicBox[i].Width = newFormForSerializable.sePictureDinamicBox[i].SizeX;
                    this.listPictureDinamicBox[i].Height = newFormForSerializable.sePictureDinamicBox[i].SizeY;
                    this.listPictureBox[i].Width = newFormForSerializable.sePictureDinamicBox[i].childSizeX;
                    this.listPictureBox[i].Height = newFormForSerializable.sePictureDinamicBox[i].childSizeY;
                    this.listPictureBox[i].Font = this.listPictureDinamicBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.sePictureDinamicBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.listPictureDinamicBox[i].Location = new Point(newFormForSerializable.sePictureDinamicBox[i].X, newFormForSerializable.sePictureDinamicBox[i].Y);
                    this.listPictureDinamicBox[i].Image = Image.FromFile(@newFormForSerializable.sePictureDinamicBox[i].wayPictures);
                    this.listPictureDinamicBox[i].Yfrom = newFormForSerializable.sePictureDinamicBox[i].Y;
                    this.listPictureDinamicBox[i].Xfrom = newFormForSerializable.sePictureDinamicBox[i].X;
                    this.listPictureBox[i].Location = new Point(newFormForSerializable.sePictureDinamicBox[i].childX, newFormForSerializable.sePictureDinamicBox[i].childY);
                    this.listPictureBox[i].SizeMode = this.listPictureDinamicBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                    this.listPictureDinamicBox[i].indexPictureDinamicB = i;
                    this.listPictureBox[i].index = i;
                    this.listPictureBox[i].Click += new System.EventHandler(this.pictureBoxFrom_Click);
                    this.Controls.Add(this.listPictureBox[i]);
                    this.Controls.Add(this.listPictureDinamicBox[i]);
                    this.listPictureBox[i].BringToFront();
                    TrueTest++;
                    this.listPictureDinamicBox[i].BringToFront();
                    //this.indexPictureDinamicBox++;

                }
            }
        }

        private void Form3_FormClosing(object sender, CancelEventArgs e)
        {
            ConfirmedTest confirmedTest = new ConfirmedTest(nameFile);
            confirmedTest.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listPictureBox.Count; i++)
            {
                this.FinalTrue += listPictureBox[i].trueOrfal;
            }
            for (int i = 0; i < listTextBox.Count; i++)
            {
                if (string.Compare(listTextBox[i].Text, newFormForSerializable.seTextBox[i].Text, true)==0)
                this.FinalTrue += 1;
            }

            if (TrueTest == FinalTrue)
            {
                labelTest.Text = "Результат: ВЕРНО\n" + FinalTrue + " из " + TrueTest;
            }
            else
            {
                labelTest.Text = "Результат: НЕ ВЕРНО\n" + FinalTrue + " из " + TrueTest;
            }
            for (int i = 0; i < listPictureBox.Count; i++)
            {
                this.FinalTrue -= listPictureBox[i].trueOrfal;
            }
            for (int i = 0; i < listTextBox.Count; i++)
            {
                if (string.Compare(listTextBox[i].Text, newFormForSerializable.seTextBox[i].Text, true) == 0)
                    this.FinalTrue -= 1;
            }

        }

        private void pictureBoxFrom_Click(object sender, EventArgs e)
        {
            DePictureBox picBox = sender as DePictureBox;
            if (picBox.activity == true)
            {
                picBox.Image = null;
                picBox.trueOrfal = 0;
            }
        }

        private void buttonInTest_Click(object sender, EventArgs e) //при нажатии на первую кнопку в панеле инструментов
        {
            DeButton buffButton = sender as DeButton;
            if (buffButton.active == 0)
            {
                FinalTrue += buffButton.trueAnswer;
                listButton[buffButton.index].active = 1;
            }
            else {
                FinalTrue -= buffButton.trueAnswer;
                listButton[buffButton.index].active = 0;
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            DeCheckBox buffCheck = sender as DeCheckBox;
            if (buffCheck.Checked == true)
            {
                if (buffCheck.trueAnswer == 0)
                {
                    FinalTrue -= 1;
                }
                else
                {
                    FinalTrue += buffCheck.trueAnswer;
                }
            }
            else
            {
                if (buffCheck.trueAnswer == 0)
                {
                    FinalTrue += 1;
                }
                else
                {
                    FinalTrue -= buffCheck.trueAnswer;
                }
                
            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            DeRadioButton buffRadioButton = sender as DeRadioButton;
            if (buffRadioButton.Checked == true)
            {
                FinalTrue += buffRadioButton.trueAnswer;
            }
            else
            {
                FinalTrue -= buffRadioButton.trueAnswer;
            }
        }
    }
}

                
  

