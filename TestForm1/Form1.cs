using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace TestForm1
{
    public partial class Form1 : Form
    {
        public String NameNowChoose;
        public SaveTest saveT;
        public String nameFile = "SaveTests\\НазваниеТеста_Цикл_Номер.xml";
        public SerializableForm newFormForSerializable;
        public String[] files;
        public List<NewBuffPictureBox> buffPictures = new List<NewBuffPictureBox>();
        public List<SerButton> serButton = new List<SerButton>(); //лист класса для сериализуемых кнопок
        public List<SerLabel> serLabel = new List<SerLabel>(); //лист класса для сериализуемых кнопок
        public List<SerTextBox> serTextBox = new List<SerTextBox>();
        public List<SerCheckBox> serCheckBox = new List<SerCheckBox>();
        public List<SerRadioButton> serRadioButton = new List<SerRadioButton>();
        public List<SerPictureStaticBox> serPictureStaticBox = new List<SerPictureStaticBox>();
        public List<SerPictureDinamicBox> serPictureDinamicBox = new List<SerPictureDinamicBox>();


        public Form2 child; //дочерняя форма
        public bool formChildIsLive = false; //дочерняя форма не запущена
        public Form1()
        {
            IsMdiContainer = true; //текущая форма может быть контейнером
            InitializeComponent();
            buttonForDelete.Enabled = false; //кнопка удалить
            editTextBox.Enabled = false; //ввод текста
            buttonForEditTextInTheWindow.Enabled = false; //кнопка Редактировать текст
            trackBarForFont.Enabled = false; //ползунок для изменения размера шрифта
            textBoxForHeight.Enabled = false; //изменение высоты
            textBoxForWidth.Enabled = false; //изменение ширины
            TRUECheckBox.Enabled = false;//чекбокс о правильности выбора
            numericUpDownFont.Enabled = false; //изменение размера шрифта
            trackBarHeight.Enabled = false; //изменение высоты
            trackBarWidth.Enabled = false; //изменение ширины
            numericUpDownY.Enabled = false; //изменения положения по вертикали
            numericUpDownX.Enabled = false; //изменение положения по горизонтали
            files = Directory.GetFiles(@"PicturesSource", "*.png", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                
                buffPictures.Add(new NewBuffPictureBox());
                buffPictures[i].Image = Image.FromFile(@files[i]);
                buffPictures[i].SizeMode = PictureBoxSizeMode.Zoom;
                buffPictures[i].Height = 70;
                buffPictures[i].Width = 140;
                buffPictures[i].index = i;
                flowLayoutPanel1.Controls.Add(buffPictures[i]);
                buffPictures[i].Click += new System.EventHandler(this.imageForPicture_Click);

            }
        }




        //кнопка в меню "создать форму"
        private void createFormForTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive == false) //если форма не открыта
            {
                saveT = new SaveTest(this);

                if (saveT.ShowDialog() == DialogResult.OK)
                {
                    child = new Form2(this); //создаём новую форму, передаём ей данные о том, то данная форма её родитель (this)
                    child.MdiParent = this; //зависимость дочерней формы от текущей (т.е. находится внутри родительской)
                    child.Show(); //показать вторую форму
                    for (int i = 0; i < 7; i++)
                    {
                        child.fullAllIndex[i] = 0;
                    }
                    formChildIsLive = true; //обозначаем, что форма включина
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) //при нажатии на первую кнопку в панеле инструментов
        {
            if (formChildIsLive != false)
            {
                child.CreateButton(); //создать кнопку внутри дочерней формы
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreateLabel(); //создать текст внутри дочерней формы
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreateTextBox(); //создать ввод текста внутри дочерней формы
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreateCheckBox(); 
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreateRadioButton(); 
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreatePictureStaicBox(); 
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.CreatePictureDinamicBox(); 
            }
        }

        //нажатие на кнопку Save в меню File
        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formChildIsLive == true)
            {
                saveInFile();
            }
        }
        public void saveInFile()
        {
            //Здесь происходит просто сериализация класса, который хранит в себе часть данных кнопки
            //передаем в конструктор тип класса
            newFormForSerializable = new SerializableForm();
            newFormForSerializable.seButton = new List<SerButton>(serButton);
            newFormForSerializable.seLabel = new List<SerLabel>(serLabel);
            newFormForSerializable.seTextBox = new List<SerTextBox>(serTextBox);
            newFormForSerializable.seCheckBox = new List<SerCheckBox>(serCheckBox);
            newFormForSerializable.seRadioButton = new List<SerRadioButton>(serRadioButton);
            newFormForSerializable.sePictureStaticBox = new List<SerPictureStaticBox>(serPictureStaticBox);
            newFormForSerializable.sePictureDinamicBox = new List<SerPictureDinamicBox>(serPictureDinamicBox);
            
            Array.Copy(child.fullAllIndex, newFormForSerializable.fullAllIndex, 7);
            XmlSerializer formatter = new XmlSerializer(typeof(SerializableForm)); //выбираем, какой класс будем сериализовать
            //XmlSerializer formatterLabel = new XmlSerializer(typeof(List<SerLabel>));
            // получаем поток, куда будем записывать сериализованный объект
            string path = nameFile;
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
            using (FileStream fs = new FileStream(nameFile, FileMode.Create))
            {
                formatter.Serialize(fs, newFormForSerializable); //сериализуем данные кнопки(берём из листа)
                //formatterLabel.Serialize(fs, serLabel);
            }
        }

        private void open_file()
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Cursor Files|*.xml";
            openFileDialog1.Title = "Select a Cursor File";
            openFileDialog1.InitialDirectory= System.IO.Path.Combine(Application.StartupPath, @"SaveTests\");
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                if (formChildIsLive == false) //если форма не открыта
                {
                    nameFile = openFileDialog1.FileName;
                    child = new Form2(this); //создаём новую форму, передаём ей данные о том, то данная форма её родитель (this)
                    child.MdiParent = this; //зависимость дочерней формы от текущей (т.е. находится внутри родительской)
                    child.Show(); //показать вторую форму
                    formChildIsLive = true; //обозначаем, что форма включена

                    newFormForSerializable = new SerializableForm();
                    newFormForSerializable.seButton = new List<SerButton>();
                    newFormForSerializable.seLabel = new List<SerLabel>();
                    newFormForSerializable.seTextBox = new List<SerTextBox>();
                    newFormForSerializable.seCheckBox = new List<SerCheckBox>();
                    newFormForSerializable.seRadioButton = new List<SerRadioButton>();
                    newFormForSerializable.sePictureDinamicBox = new List<SerPictureDinamicBox>();
                    newFormForSerializable.sePictureStaticBox = new List<SerPictureStaticBox>();
                    serButton = new List<SerButton>(); //лист класса для сериализуемых кнопок
                    serLabel = new List<SerLabel>(); //лист класса для сериализуемых кнопок
                    serTextBox = new List<SerTextBox>();
                    serCheckBox = new List<SerCheckBox>();
                    serRadioButton = new List<SerRadioButton>();
                    serPictureStaticBox = new List<SerPictureStaticBox>();
                    serPictureDinamicBox = new List<SerPictureDinamicBox>();
                    XmlSerializer formatter = new XmlSerializer(typeof(SerializableForm));
                    using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate))
                    {
                        //десеарилизуем
                        for (int i = 0; i < 7; i++)
                        {
                            child.fullAllIndex[i] = 0;
                        }
                        newFormForSerializable = new SerializableForm();
                        newFormForSerializable = (SerializableForm)formatter.Deserialize(fs);
                        Array.Copy(newFormForSerializable.fullAllIndex, child.fullAllIndex, 7);
                        for (int i = 0; i < newFormForSerializable.seButton.Count; i++)
                        {
                            serButton = new List<SerButton>(newFormForSerializable.seButton);
                            child.listButton.Add(new NewButton(this)); //создаём кнопку
                            child.listButton[i].Name = newFormForSerializable.seButton[i].Name;
                            child.listButton[i].Text = newFormForSerializable.seButton[i].Text;
                            child.listButton[i].Width = newFormForSerializable.seButton[i].SizeX;
                            child.listButton[i].Height = newFormForSerializable.seButton[i].SizeY;
                            child.listButton[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seButton[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listButton[i].Location = new Point(newFormForSerializable.seButton[i].X, newFormForSerializable.seButton[i].Y);
                            child.listButton[i].TrueOrFalse = newFormForSerializable.seButton[i].TrueOrFalse;
                            child.Controls.Add(child.listButton[i]);
                            child.listButton[i].indexBut = i;
                            newFormForSerializable.seButton[i].indexBut = i;
                            child.listButton[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode buttonNode = new TreeNode(child.listButton[child.indexButton].Text);//
                            buttonNode.Name = child.listButton[child.indexButton].Name;//
                            treeView1.Nodes[0].Nodes.Add(buttonNode);//
                            treeView1.Nodes[0].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[child.indexButton];


                            child.indexButton++;

                        }
                        for (int i = 0; i < newFormForSerializable.seLabel.Count; i++)
                        {
                            serLabel = new List<SerLabel>(newFormForSerializable.seLabel);
                            child.listLabel.Add(new NewLabel(this));
                            child.listLabel[i].Name = newFormForSerializable.seLabel[i].Name;
                            child.listLabel[i].Text = newFormForSerializable.seLabel[i].Text;
                            child.listLabel[i].Width = newFormForSerializable.seLabel[i].SizeX;
                            child.listLabel[i].Height = newFormForSerializable.seLabel[i].SizeY;
                            child.listLabel[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seLabel[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listLabel[i].TextAlign = ContentAlignment.MiddleCenter;
                            child.listLabel[i].Location = new Point(newFormForSerializable.seLabel[i].X, newFormForSerializable.seLabel[i].Y);
                            

                            child.Controls.Add(child.listLabel[i]);
                            child.listLabel[i].indexLab = i;
                            newFormForSerializable.seLabel[i].indexLab = i;
                            child.listLabel[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode labelNode = new TreeNode(child.listLabel[child.indexLabel].Text);//
                            labelNode.Name = child.listLabel[child.indexLabel].Name;//
                            treeView1.Nodes[1].Nodes.Add(labelNode);//
                            treeView1.Nodes[1].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[child.indexLabel];


                            child.indexLabel++;

                        }
                        for (int i = 0; i < newFormForSerializable.seTextBox.Count; i++)
                        {
                            serTextBox = new List<SerTextBox>(newFormForSerializable.seTextBox);
                            child.listTextBox.Add(new NewTextBox(this));
                            child.listTextBox[i].Name = newFormForSerializable.seTextBox[i].Name;
                            child.listTextBox[i].Text = newFormForSerializable.seTextBox[i].Text;
                            child.listTextBox[i].Width = newFormForSerializable.seTextBox[i].SizeX;
                            child.listTextBox[i].Height = newFormForSerializable.seTextBox[i].SizeY;
                            child.listTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seTextBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listTextBox[i].Location = new Point(newFormForSerializable.seTextBox[i].X, newFormForSerializable.seTextBox[i].Y);
                            

                            child.Controls.Add(child.listTextBox[i]);
                            child.listTextBox[i].indexTextB = i;
                            newFormForSerializable.seTextBox[i].indexTextB = i;
                            child.listTextBox[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode textBoxNode = new TreeNode(child.listTextBox[child.indexTextBox].Text);//
                            textBoxNode.Name = child.listTextBox[child.indexTextBox].Name;//
                            treeView1.Nodes[2].Nodes.Add(textBoxNode);//
                            treeView1.Nodes[2].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[2].Nodes[child.indexTextBox];


                            child.indexTextBox++;

                        }
                        for (int i = 0; i < newFormForSerializable.seCheckBox.Count; i++)
                        {
                            serCheckBox = new List<SerCheckBox>(newFormForSerializable.seCheckBox);
                            child.listCheckBox.Add(new NewCheckBox(this));
                            child.listCheckBox[i].Name = newFormForSerializable.seCheckBox[i].Name;
                            child.listCheckBox[i].Text = newFormForSerializable.seCheckBox[i].Text;
                            child.listCheckBox[i].Width = newFormForSerializable.seCheckBox[i].SizeX;
                            child.listCheckBox[i].Height = newFormForSerializable.seCheckBox[i].SizeY;
                            child.listCheckBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seCheckBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listCheckBox[i].Location = new Point(newFormForSerializable.seCheckBox[i].X, newFormForSerializable.seCheckBox[i].Y);
                            child.listCheckBox[i].TrueOrFalse = newFormForSerializable.seCheckBox[i].TrueOrFalse;
                            
                            child.listCheckBox[i].AutoCheck = false;
                            if (newFormForSerializable.seCheckBox[i].TrueOrFalse == 1)
                            {
                                child.listCheckBox[i].Checked = true;
                            }
                            else
                            {
                                child.listCheckBox[i].Checked = false;
                            }
                            child.Controls.Add(child.listCheckBox[i]);
                            child.listCheckBox[i].indexCheckB = i;
                            newFormForSerializable.seCheckBox[i].indexCheckB = i;
                            child.listCheckBox[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode checkBoxNode = new TreeNode(child.listCheckBox[child.indexCheckBox].Text);//
                            checkBoxNode.Name = child.listCheckBox[child.indexCheckBox].Name;//
                            treeView1.Nodes[3].Nodes.Add(checkBoxNode);//
                            treeView1.Nodes[3].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[3].Nodes[child.indexCheckBox];


                            child.indexCheckBox++;

                        }
                        for (int i = 0; i < newFormForSerializable.seRadioButton.Count; i++)
                        {
                            serRadioButton = new List<SerRadioButton>(newFormForSerializable.seRadioButton);
                            child.listRadioButton.Add(new NewRadioButton(this));
                            child.listRadioButton[i].Name = newFormForSerializable.seRadioButton[i].Name;
                            child.listRadioButton[i].Text = newFormForSerializable.seRadioButton[i].Text;
                            child.listRadioButton[i].Width = newFormForSerializable.seRadioButton[i].SizeX;
                            child.listRadioButton[i].Height = newFormForSerializable.seRadioButton[i].SizeY;
                            child.listRadioButton[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.seRadioButton[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listRadioButton[i].Location = new Point(newFormForSerializable.seRadioButton[i].X, newFormForSerializable.seRadioButton[i].Y);
                            child.listRadioButton[i].TrueOrFalse = newFormForSerializable.seRadioButton[i].TrueOrFalse;

                            child.listRadioButton[i].AutoCheck = false;
                            if (newFormForSerializable.seRadioButton[i].TrueOrFalse == 1)
                            {
                                child.listRadioButton[i].Checked = true;
                            }
                            else
                            {
                                child.listRadioButton[i].Checked = false;
                            }
                            child.Controls.Add(child.listRadioButton[i]);
                            child.listRadioButton[i].indexRadioB = i;
                            newFormForSerializable.seRadioButton[i].indexRadioB = i;
                            child.listRadioButton[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode radioButtonNode = new TreeNode(child.listRadioButton[child.indexRadioButton].Text);//
                            radioButtonNode.Name = child.listRadioButton[child.indexRadioButton].Name;//
                            treeView1.Nodes[4].Nodes.Add(radioButtonNode);//
                            treeView1.Nodes[4].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[4].Nodes[child.indexRadioButton];


                            child.indexRadioButton++;

                        }
                        
                        for (int i = 0; i < newFormForSerializable.sePictureStaticBox.Count; i++)
                        {
                            serPictureStaticBox = new List<SerPictureStaticBox>(newFormForSerializable.sePictureStaticBox);
                            child.listPictureStaticBox.Add(new NewPictureStaticBox(this));
                            child.listPictureStaticBox[i].Name = newFormForSerializable.sePictureStaticBox[i].Name;
                            child.listPictureStaticBox[i].Text = newFormForSerializable.sePictureStaticBox[i].Text;
                            child.listPictureStaticBox[i].Width = newFormForSerializable.sePictureStaticBox[i].SizeX;
                            child.listPictureStaticBox[i].Height = newFormForSerializable.sePictureStaticBox[i].SizeY;
                            child.listPictureStaticBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.sePictureStaticBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listPictureStaticBox[i].Location = new Point(newFormForSerializable.sePictureStaticBox[i].X, newFormForSerializable.sePictureStaticBox[i].Y);
                            child.listPictureStaticBox[i].Image = Image.FromFile(newFormForSerializable.sePictureStaticBox[i].wayPictures);
                            

                            child.listPictureStaticBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                            child.Controls.Add(child.listPictureStaticBox[i]);
                            child.listPictureStaticBox[i].indexPictureStaticB = i;
                            newFormForSerializable.sePictureStaticBox[i].indexPictureStatic = i;
                            child.listPictureStaticBox[i].ContextMenuStrip = child.contextMenuStrip1;

                            TreeNode pictureStaticNode = new TreeNode(child.listPictureStaticBox[child.indexPictureStaticBox].Text);//
                            pictureStaticNode.Name = child.listPictureStaticBox[child.indexPictureStaticBox].Name;//
                            treeView1.Nodes[5].Nodes.Add(pictureStaticNode);//
                            treeView1.Nodes[5].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[5].Nodes[child.indexPictureStaticBox];


                            child.indexPictureStaticBox++;

                        }
                        for (int i = 0; i < newFormForSerializable.sePictureDinamicBox.Count; i++)
                        {
                            serPictureDinamicBox = new List<SerPictureDinamicBox>(newFormForSerializable.sePictureDinamicBox);
                            child.listPictureDinamicBox.Add(new NewPictureDinamicBox(this));
                            child.listPictureBox.Add(new NewPictureBox(this));
                            child.listPictureBox[i].Name = newFormForSerializable.sePictureDinamicBox[i].childName;
                            child.listPictureDinamicBox[i].Name = newFormForSerializable.sePictureDinamicBox[i].Name;
                            child.listPictureBox[i].Text = child.listPictureDinamicBox[i].Text = newFormForSerializable.sePictureDinamicBox[i].Text;
                            child.listPictureDinamicBox[i].Width = newFormForSerializable.sePictureDinamicBox[i].SizeX;
                            child.listPictureBox[i].Width = newFormForSerializable.sePictureDinamicBox[i].childSizeX;
                            child.listPictureDinamicBox[i].Height = newFormForSerializable.sePictureDinamicBox[i].SizeY;
                            child.listPictureBox[i].Height = newFormForSerializable.sePictureDinamicBox[i].childSizeY;
                            child.listPictureBox[i].Font = child.listPictureDinamicBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", newFormForSerializable.sePictureDinamicBox[i].fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            child.listPictureDinamicBox[i].Location = new Point(newFormForSerializable.sePictureDinamicBox[i].X, newFormForSerializable.sePictureDinamicBox[i].Y);
                            child.listPictureBox[i].Location = new Point(newFormForSerializable.sePictureDinamicBox[i].childX, newFormForSerializable.sePictureDinamicBox[i].childY);
                            child.listPictureDinamicBox[i].Image = Image.FromFile(newFormForSerializable.sePictureDinamicBox[i].wayPictures);
                            

                            child.listPictureDinamicBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                            child.Controls.Add(child.listPictureDinamicBox[i]);
                            child.Controls.Add(child.listPictureBox[i]);
                            child.listPictureDinamicBox[i].indexPictureDinamicB = i;
                            child.listPictureBox[i].indexPictureDinamicB = i;
                            newFormForSerializable.sePictureDinamicBox[i].indexPictureDin = i;
                            child.listPictureDinamicBox[i].ContextMenuStrip = child.contextMenuStrip1;
                            child.listPictureBox[i].ContextMenuStrip = child.contextMenuStrip1;
                            child.listPictureBox[i].BringToFront();


                            TreeNode pictureDinamicNode = new TreeNode(child.listPictureDinamicBox[child.indexPictureDinamicBox].Text);//
                            pictureDinamicNode.Name = child.listPictureDinamicBox[child.indexPictureDinamicBox].Name;//
                            treeView1.Nodes[6].Nodes.Add(pictureDinamicNode);//
                            treeView1.Nodes[6].Expand();
                            

                            TreeNode pictureNode = new TreeNode(child.listPictureBox[child.indexPictureDinamicBox].Text);//
                            pictureNode.Name = child.listPictureBox[child.indexPictureDinamicBox].Name;//
                            treeView1.Nodes[7].Nodes.Add(pictureNode);//
                            treeView1.Nodes[7].Expand();
                            treeView1.SelectedNode = treeView1.Nodes[6].Nodes[child.indexPictureDinamicBox];
                            treeView1.SelectedNode = treeView1.Nodes[7].Nodes[child.indexPictureDinamicBox];


                            child.indexPictureDinamicBox++;

                        }
                    }
                }
            }

        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (formChildIsLive == true)
            {
                child.Close();
                this.open_file();
            }
            else
            {
                this.open_file();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String[] str = { "Buts", "Labs", "TextsBox","ChecksBox","RadiosBut", "PicturessStatic", "PicturessDinamic", "Pita" };
            for (int i = 0; i < 8; i++)
            {
                if (str[i] == e.Node.Name)
                {
                    buttonForDelete.Enabled = false; //кнопка удалить
                    editTextBox.Enabled = false; //ввод текста
                    buttonForEditTextInTheWindow.Enabled = false; //кнопка Редактировать текст
                    trackBarForFont.Enabled = false; //ползунок для изменения размера шрифта
                    textBoxForHeight.Enabled = false; //изменение высоты
                    textBoxForWidth.Enabled = false; //изменение ширины
                    TRUECheckBox.Enabled = false;//чекбокс о правильности выбора
                    numericUpDownFont.Enabled = false; //изменение размера шрифта
                    trackBarHeight.Enabled = false; //изменение высоты
                    trackBarWidth.Enabled = false; //изменение ширины
                    numericUpDownY.Enabled = false; //изменения положения по вертикали
                    numericUpDownX.Enabled = false; //изменение положения по горизонтали
                    
                    return;
                }
            }
            buttonForDelete.Enabled = true; //кнопка удалить
            editTextBox.Enabled = true; //ввод текста
            buttonForEditTextInTheWindow.Enabled = true; //кнопка Редактировать текст
            trackBarForFont.Enabled = true; //ползунок для изменения размера шрифта
            textBoxForHeight.Enabled = true; //изменение высоты
            textBoxForWidth.Enabled = true; //изменение ширины
            TRUECheckBox.Enabled = true; //чекбокс о правильности выбора
            numericUpDownFont.Enabled = true; //изменение размера шрифта
            trackBarHeight.Enabled = true; //изменение высоты
            trackBarWidth.Enabled = true; //изменение ширины
            numericUpDownY.Enabled = true; //изменения положения по вертикали
            numericUpDownX.Enabled = true; //изменение положения по горизонтали
            
            NameNowChoose = e.Node.Name;
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newButton.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newButton.Height);
                        trackBarHeight.Value = Convert.ToInt32(newButton.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newButton.Width);
                        trackBarWidth.Value = Convert.ToInt32(newButton.Width);

                        if (newButton.TrueOrFalse == 1)
                        {
                            TRUECheckBox.Checked = true;
                        }
                        else if (newButton.TrueOrFalse == 0)
                        {
                            TRUECheckBox.Checked = false;
                        }
                        trackBarForFont.Value = Convert.ToInt32(newButton.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newButton.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newButton.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newButton.Location.X);
                        
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newLabel.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newLabel.Height);
                        trackBarHeight.Value = Convert.ToInt32(newLabel.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newLabel.Width);
                        trackBarWidth.Value = Convert.ToInt32(newLabel.Width);
                        TRUECheckBox.Checked = false;
                        trackBarForFont.Value = Convert.ToInt32(newLabel.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newLabel.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newLabel.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newLabel.Location.X);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newTextBox.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newTextBox.Height);
                        trackBarHeight.Value = Convert.ToInt32(newTextBox.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newTextBox.Width);
                        trackBarWidth.Value = Convert.ToInt32(newTextBox.Width);
                        TRUECheckBox.Checked = false;
                        trackBarForFont.Value = Convert.ToInt32(newTextBox.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newTextBox.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newTextBox.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newTextBox.Location.X);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newCheckBox.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newCheckBox.Height);
                        trackBarHeight.Value = Convert.ToInt32(newCheckBox.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newCheckBox.Width);
                        trackBarWidth.Value = Convert.ToInt32(newCheckBox.Width);
                        if (newCheckBox.TrueOrFalse == 1)
                        {
                            TRUECheckBox.Checked = true;
                        }
                        else if (newCheckBox.TrueOrFalse == 0)
                        {
                            TRUECheckBox.Checked = false;
                        }
                        trackBarForFont.Value = Convert.ToInt32(newCheckBox.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newCheckBox.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newCheckBox.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newCheckBox.Location.X);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newRadioButton.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newRadioButton.Height);
                        trackBarHeight.Value = Convert.ToInt32(newRadioButton.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newRadioButton.Width);
                        trackBarWidth.Value = Convert.ToInt32(newRadioButton.Width);
                        if (newRadioButton.TrueOrFalse == 1)
                        {
                            TRUECheckBox.Checked = true;
                        }
                        else if (newRadioButton.TrueOrFalse == 0)
                        {
                            TRUECheckBox.Checked = false; 
                        }
                        trackBarForFont.Value = Convert.ToInt32(newRadioButton.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newRadioButton.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newRadioButton.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newRadioButton.Location.X);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newPictureStaticBox.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newPictureStaticBox.Height);
                        trackBarHeight.Value = Convert.ToInt32(newPictureStaticBox.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newPictureStaticBox.Width);
                        trackBarWidth.Value = Convert.ToInt32(newPictureStaticBox.Width);
                        TRUECheckBox.Checked = false;
                        trackBarForFont.Value = Convert.ToInt32(newPictureStaticBox.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newPictureStaticBox.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newPictureStaticBox.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newPictureStaticBox.Location.X);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newPictureDinamicBox.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newPictureDinamicBox.Height);
                        trackBarHeight.Value = Convert.ToInt32(newPictureDinamicBox.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newPictureDinamicBox.Width);
                        trackBarWidth.Value = Convert.ToInt32(newPictureDinamicBox.Width);
                        trackBarForFont.Value = Convert.ToInt32(newPictureDinamicBox.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newPictureDinamicBox.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newPictureDinamicBox.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newPictureDinamicBox.Location.X);
                        TRUECheckBox.Checked = false;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        editTextBox.Text = newPictureBox.Text;
                        textBoxForHeight.Value = Convert.ToInt32(newPictureBox.Height);
                        trackBarHeight.Value = Convert.ToInt32(newPictureBox.Height);
                        textBoxForWidth.Value = Convert.ToInt32(newPictureBox.Width);
                        trackBarWidth.Value = Convert.ToInt32(newPictureBox.Width);
                        trackBarForFont.Value = Convert.ToInt32(newPictureBox.Font.Size);
                        numericUpDownFont.Value = Convert.ToInt32(newPictureBox.Font.Size);
                        numericUpDownY.Value = Convert.ToInt32(newPictureBox.Location.Y);
                        numericUpDownX.Value = Convert.ToInt32(newPictureBox.Location.X);
                        TRUECheckBox.Checked = false;
                        break;
                    }
                }
            }
        }

        private void buttonForDelete_Click(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newButton.indexBut + 1; i < child.listButton.Count; i++)
                        {
                            child.listButton[i].indexBut = i - 1;
                            serButton[i].indexBut = i - 1;
                        }
                        treeView1.Nodes[0].Nodes.RemoveAt(newButton.indexBut);
                        child.listButton[newButton.indexBut].Dispose();
                        child.listButton.RemoveAt(newButton.indexBut);
                        serButton.RemoveAt(newButton.indexBut);
                        child.indexButton--;
                        
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newLabel.indexLab + 1; i < child.listLabel.Count; i++)
                        {
                            child.listLabel[i].indexLab = i - 1;
                            serLabel[i].indexLab = i - 1;
                        }
                        treeView1.Nodes[1].Nodes.RemoveAt(newLabel.indexLab);
                        child.listLabel[newLabel.indexLab].Dispose();
                        child.listLabel.RemoveAt(newLabel.indexLab);
                        serLabel.RemoveAt(newLabel.indexLab);
                        child.indexLabel--;

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newTextBox.indexTextB + 1; i < child.listTextBox.Count; i++)
                        {
                            child.listTextBox[i].indexTextB = i - 1;
                            serTextBox[i].indexTextB = i - 1;
                        }
                        treeView1.Nodes[2].Nodes.RemoveAt(newTextBox.indexTextB);
                        child.listTextBox[newTextBox.indexTextB].Dispose();
                        child.listTextBox.RemoveAt(newTextBox.indexTextB);
                        serTextBox.RemoveAt(newTextBox.indexTextB);
                        child.indexTextBox--;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newCheckBox.indexCheckB + 1; i < child.listCheckBox.Count; i++)
                        {
                            child.listCheckBox[i].indexCheckB = i - 1;
                            serCheckBox[i].indexCheckB = i - 1;
                        }
                        treeView1.Nodes[3].Nodes.RemoveAt(newCheckBox.indexCheckB);
                        child.listCheckBox[newCheckBox.indexCheckB].Dispose();
                        child.listCheckBox.RemoveAt(newCheckBox.indexCheckB);
                        serCheckBox.RemoveAt(newCheckBox.indexCheckB);
                        child.indexCheckBox--;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newRadioButton.indexRadioB + 1; i < child.listRadioButton.Count; i++)
                        {
                            child.listRadioButton[i].indexRadioB = i - 1;
                            serRadioButton[i].indexRadioB = i - 1;
                        }
                        treeView1.Nodes[4].Nodes.RemoveAt(newRadioButton.indexRadioB);
                        child.listRadioButton[newRadioButton.indexRadioB].Dispose();
                        child.listRadioButton.RemoveAt(newRadioButton.indexRadioB);
                        serRadioButton.RemoveAt(newRadioButton.indexRadioB);
                        child.indexRadioButton--;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newPictureStaticBox.indexPictureStaticB + 1; i < child.listPictureStaticBox.Count; i++)
                        {
                            child.listPictureStaticBox[i].indexPictureStaticB = i - 1;
                            serPictureStaticBox[i].indexPictureStatic = i - 1;
                        }
                        treeView1.Nodes[5].Nodes.RemoveAt(newPictureStaticBox.indexPictureStaticB);
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Dispose();
                        child.listPictureStaticBox.RemoveAt(newPictureStaticBox.indexPictureStaticB);
                        serPictureStaticBox.RemoveAt(newPictureStaticBox.indexPictureStaticB);
                        child.indexPictureStaticBox--;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newPictureDinamicBox.indexPictureDinamicB + 1; i < child.listPictureDinamicBox.Count; i++)
                        {
                            child.listPictureBox[i].indexPictureDinamicB = i - 1;
                            child.listPictureDinamicBox[i].indexPictureDinamicB = i - 1;
                            serPictureDinamicBox[i].indexPictureDin = i - 1;
                        }
                        treeView1.Nodes[6].Nodes.RemoveAt(newPictureDinamicBox.indexPictureDinamicB);
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Dispose();
                        child.listPictureDinamicBox.RemoveAt(newPictureDinamicBox.indexPictureDinamicB);

                        treeView1.Nodes[7].Nodes.RemoveAt(newPictureDinamicBox.indexPictureDinamicB);
                        child.listPictureBox[newPictureDinamicBox.indexPictureDinamicB].Dispose();
                        child.listPictureBox.RemoveAt(newPictureDinamicBox.indexPictureDinamicB);

                        serPictureDinamicBox.RemoveAt(newPictureDinamicBox.indexPictureDinamicB);
                        child.indexPictureDinamicBox--;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        for (int i = newPictureBox.indexPictureDinamicB + 1; i < child.listPictureBox.Count; i++)
                        {
                            child.listPictureBox[i].indexPictureDinamicB = i - 1;
                            child.listPictureDinamicBox[i].indexPictureDinamicB = i - 1;
                            serPictureDinamicBox[i].indexPictureDin = i - 1;
                        }
                        treeView1.Nodes[6].Nodes.RemoveAt(newPictureBox.indexPictureDinamicB);
                        child.listPictureDinamicBox[newPictureBox.indexPictureDinamicB].Dispose();
                        child.listPictureDinamicBox.RemoveAt(newPictureBox.indexPictureDinamicB);

                        treeView1.Nodes[7].Nodes.RemoveAt(newPictureBox.indexPictureDinamicB);
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Dispose();
                        child.listPictureBox.RemoveAt(newPictureBox.indexPictureDinamicB);

                        serPictureDinamicBox.RemoveAt(newPictureBox.indexPictureDinamicB);
                        child.indexPictureDinamicBox--;
                        break;
                    }
                }
            }
        }

        private void editTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Text = editTextBox.Text;
                        serButton[newButton.indexBut].Text = editTextBox.Text;
                        treeView1.Nodes[0].Nodes[newButton.indexBut].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Text = editTextBox.Text;
                        serLabel[newLabel.indexLab].Text = editTextBox.Text;
                        treeView1.Nodes[1].Nodes[newLabel.indexLab].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Text = editTextBox.Text;
                        serTextBox[newTextBox.indexTextB].Text = editTextBox.Text;
                        treeView1.Nodes[2].Nodes[newTextBox.indexTextB].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Text = editTextBox.Text;
                        serCheckBox[newCheckBox.indexCheckB].Text = editTextBox.Text;
                        treeView1.Nodes[3].Nodes[newCheckBox.indexCheckB].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Text = editTextBox.Text;
                        serRadioButton[newRadioButton.indexRadioB].Text = editTextBox.Text;
                        treeView1.Nodes[4].Nodes[newRadioButton.indexRadioB].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Text = editTextBox.Text;
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Text = editTextBox.Text;
                        treeView1.Nodes[5].Nodes[newPictureStaticBox.indexPictureStaticB].Text = editTextBox.Text;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Text = editTextBox.Text;
                        child.listPictureBox[newPictureDinamicBox.indexPictureDinamicB].Text = editTextBox.Text;//
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Text = editTextBox.Text;
                        treeView1.Nodes[6].Nodes[newPictureDinamicBox.indexPictureDinamicB].Text = editTextBox.Text;
                        treeView1.Nodes[7].Nodes[newPictureDinamicBox.indexPictureDinamicB].Text = editTextBox.Text;
                        break;
                    }
                }
            }

            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureBox.indexPictureDinamicB].Text = editTextBox.Text;
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Text = editTextBox.Text;//
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].Text = editTextBox.Text;
                        treeView1.Nodes[6].Nodes[newPictureBox.indexPictureDinamicB].Text = editTextBox.Text;
                        treeView1.Nodes[7].Nodes[newPictureBox.indexPictureDinamicB].Text = editTextBox.Text;//
                        break;
                    }
                }
            }
        }

        private void textBoxForHeight_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serButton[newButton.indexBut].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serLabel[newLabel.indexLab].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serTextBox[newTextBox.indexTextB].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Height = Convert.ToInt32(textBoxForHeight.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeY = Convert.ToInt32(textBoxForHeight.Value);
                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Height = Convert.ToInt32(textBoxForHeight.Value);//
                        
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeY = Convert.ToInt32(textBoxForHeight.Value);//

                        trackBarHeight.Value = Convert.ToInt32(textBoxForHeight.Value);
                        break;
                    }
                }
            }
        }

        private void textBoxForWidth_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serButton[newButton.indexBut].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serLabel[newLabel.indexLab].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serTextBox[newTextBox.indexTextB].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Width = Convert.ToInt32(textBoxForWidth.Value);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeX = Convert.ToInt32(textBoxForWidth.Value);
                        trackBarWidth.Value = Convert.ToInt32(textBoxForWidth.Value);
                        break;
                    }
                }
            }
        }

        private void TRUECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        if (TRUECheckBox.Checked == true)
                        {
                            for (int i = 0; i < child.listButton.Count; i++)
                            {
                                child.listButton[i].TrueOrFalse = 0;
                                serButton[i].TrueOrFalse = 0;
                                
                            }
                            child.listButton[newButton.indexBut].TrueOrFalse = 1;
                            serButton[newButton.indexBut].TrueOrFalse = 1;
                        }
                        else if (TRUECheckBox.Checked == false)
                        {
                            child.listButton[newButton.indexBut].TrueOrFalse = 0;
                            serButton[newButton.indexBut].TrueOrFalse = 0;
                        }
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        TRUECheckBox.Checked = false;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        TRUECheckBox.Checked = false;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        if (TRUECheckBox.Checked == true)
                        {
                            child.listCheckBox[newCheckBox.indexCheckB].TrueOrFalse = 1;
                            serCheckBox[newCheckBox.indexCheckB].TrueOrFalse = 1;
                            child.listCheckBox[newCheckBox.indexCheckB].Checked = true;
                        }
                        else if (TRUECheckBox.Checked == false)
                        {
                            child.listCheckBox[newCheckBox.indexCheckB].TrueOrFalse = 0;
                            serCheckBox[newCheckBox.indexCheckB].TrueOrFalse = 0;
                            child.listCheckBox[newCheckBox.indexCheckB].Checked = false;
                        }
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        if (TRUECheckBox.Checked == true)
                        {
                            for (int i = 0; i < child.listRadioButton.Count; i++)
                            {
                                child.listRadioButton[i].TrueOrFalse = 0;
                                serRadioButton[i].TrueOrFalse = 0;
                                child.listRadioButton[i].Checked = false;
                            }
                            child.listRadioButton[newRadioButton.indexRadioB].TrueOrFalse = 1;
                            serRadioButton[newRadioButton.indexRadioB].TrueOrFalse = 1;
                            child.listRadioButton[newRadioButton.indexRadioB].Checked = true;
                        }
                        else if (TRUECheckBox.Checked == false)
                        {
                            
                            child.listRadioButton[newRadioButton.indexRadioB].TrueOrFalse = 0;
                            serRadioButton[newRadioButton.indexRadioB].TrueOrFalse = 0;
                            child.listRadioButton[newRadioButton.indexRadioB].Checked = false;
                        }
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        TRUECheckBox.Checked = false;
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        TRUECheckBox.Checked = false;

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        TRUECheckBox.Checked = false;

                        break;
                    }
                }
            }
        }

        private void trackBarForFont_Scroll(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serButton[newButton.indexBut].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serLabel[newLabel.indexLab].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serTextBox[newTextBox.indexTextB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serCheckBox[newCheckBox.indexCheckB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serRadioButton[newRadioButton.indexRadioB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureDinamicBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        child.listPictureDinamicBox[newPictureBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", trackBarForFont.Value, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        numericUpDownFont.Value = Convert.ToInt32(trackBarForFont.Value);
                        break;
                    }
                }
            }
        }

        public void imageForPicture_Click(object sender, EventArgs e)
        {
            if (formChildIsLive == true)
            {
                if(NameNowChoose != null)
                {
                    if (NameNowChoose.Contains("PictureStaticBox"))
                    {
                        foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                        {
                            String NameBuf = newPictureStaticBox.Name;
                            if (NameNowChoose == NameBuf)
                            {
                                child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Image = (sender as NewBuffPictureBox).Image;
                                child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].wayPictures = files[(sender as NewBuffPictureBox).index];
                                serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].wayPictures = files[(sender as NewBuffPictureBox).index];
                                break;
                            }
                        }
                    }
                    if (NameNowChoose.Contains("PictureDinamicBox"))
                    {
                        foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                        {
                            String NameBuf = newPictureDinamicBox.Name;
                            if (NameNowChoose == NameBuf)
                            {
                                child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Image = (sender as NewBuffPictureBox).Image;
                                child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].wayPictures = files[(sender as NewBuffPictureBox).index];
                                serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].wayPictures = files[(sender as NewBuffPictureBox).index];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
                MessageBox.Show(
                    "Программа разработана для некоммерческого использования.\nПрограмма не предназначена для продажи или использования в коммерческих целях\nВерсия программы 1.00 (релиз)\n",
                    "О программе",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
            
        }

        private void обАвторахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "Автор программы- Александр Александрович Приёмко.\nВ сети можно найти по имени-фамилии \"Шурик Приёмко\".\nКонтакты: \nВконтакте: vk.com/shyrikr (время ответа- 1-2 дня).\nEmail: sashapriyomko@mail.ru (время ответа- от нескольких недель до полугода).\nПри обращении помечать сообщение о том, что вы взяли контакты отсюда. Спасибо за понимание.",
                    "Об авторе",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
        }

        private void обучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Training train = new Training();
            train.Show();
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Location = new Point(child.listButton[newButton.indexBut].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serButton[newButton.indexBut].Y = Convert.ToInt32(numericUpDownY.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Location = new Point(child.listLabel[newLabel.indexLab].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serLabel[newLabel.indexLab].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Location = new Point(child.listTextBox[newTextBox.indexTextB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serTextBox[newTextBox.indexTextB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Location = new Point(child.listCheckBox[newCheckBox.indexCheckB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serCheckBox[newCheckBox.indexCheckB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Location = new Point(child.listRadioButton[newRadioButton.indexRadioB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serRadioButton[newRadioButton.indexRadioB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location = new Point(child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location = new Point(child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Location = new Point(child.listPictureBox[newPictureBox.indexPictureDinamicB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childY = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listButton[newButton.indexBut].Location.Y);
                        serButton[newButton.indexBut].X = Convert.ToInt32(numericUpDownX.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listLabel[newLabel.indexLab].Location.Y);
                        serLabel[newLabel.indexLab].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listTextBox[newTextBox.indexTextB].Location.Y);
                        serTextBox[newTextBox.indexTextB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listCheckBox[newCheckBox.indexCheckB].Location.Y);
                        serCheckBox[newCheckBox.indexCheckB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listRadioButton[newRadioButton.indexRadioB].Location.Y);
                        serRadioButton[newRadioButton.indexRadioB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location.Y);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location.Y);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureBox[newPictureBox.indexPictureDinamicB].Location.Y);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childX = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
        }

        private void trackBarHeight_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Height = Convert.ToInt32(trackBarHeight.Value);
                        serButton[newButton.indexBut].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Height = Convert.ToInt32(trackBarHeight.Value);
                        serLabel[newLabel.indexLab].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serTextBox[newTextBox.indexTextB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
        }

        private void trackBarHeight_Scroll(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Height = Convert.ToInt32(trackBarHeight.Value);
                        serButton[newButton.indexBut].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Height = Convert.ToInt32(trackBarHeight.Value);
                        serLabel[newLabel.indexLab].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serTextBox[newTextBox.indexTextB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Height = Convert.ToInt32(trackBarHeight.Value);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeY = Convert.ToInt32(trackBarHeight.Value);
                        textBoxForHeight.Value = Convert.ToInt32(trackBarHeight.Value);
                        break;
                    }
                }
            }
        }

        private void trackBarWidth_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Width = Convert.ToInt32(trackBarWidth.Value);
                        serButton[newButton.indexBut].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Width = Convert.ToInt32(trackBarWidth.Value);
                        serLabel[newLabel.indexLab].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serTextBox[newTextBox.indexTextB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
        }

        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Width = Convert.ToInt32(trackBarWidth.Value);
                        serButton[newButton.indexBut].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Width = Convert.ToInt32(trackBarWidth.Value);
                        serLabel[newLabel.indexLab].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serTextBox[newTextBox.indexTextB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serCheckBox[newCheckBox.indexCheckB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serRadioButton[newRadioButton.indexRadioB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].SizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Width = Convert.ToInt32(trackBarWidth.Value);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childSizeX = Convert.ToInt32(trackBarWidth.Value);
                        textBoxForWidth.Value = Convert.ToInt32(trackBarWidth.Value);
                        break;
                    }
                }
            }
        }

        private void numericUpDownY_ValueChanged(object sender, ScrollEventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Location = new Point(child.listButton[newButton.indexBut].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serButton[newButton.indexBut].Y = Convert.ToInt32(numericUpDownY.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Location = new Point(child.listLabel[newLabel.indexLab].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serLabel[newLabel.indexLab].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Location = new Point(child.listTextBox[newTextBox.indexTextB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serTextBox[newTextBox.indexTextB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Location = new Point(child.listCheckBox[newCheckBox.indexCheckB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serCheckBox[newCheckBox.indexCheckB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Location = new Point(child.listRadioButton[newRadioButton.indexRadioB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serRadioButton[newRadioButton.indexRadioB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location = new Point(child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location = new Point(child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Y = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Location = new Point(child.listPictureBox[newPictureBox.indexPictureDinamicB].Location.X, Convert.ToInt32(numericUpDownY.Value));
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childY = Convert.ToInt32(numericUpDownY.Value);

                        break;
                    }
                }
            }
        }

        private void numericUpDownFont_ValueChanged(object sender, EventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serButton[newButton.indexBut].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serLabel[newLabel.indexLab].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serTextBox[newTextBox.indexTextB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serCheckBox[newCheckBox.indexCheckB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serRadioButton[newRadioButton.indexRadioB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureDinamicBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Font = new System.Drawing.Font("Microsoft Sans Serif", Convert.ToInt32(numericUpDownFont.Value), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].fontSize = Convert.ToInt32(trackBarForFont.Value);
                        trackBarForFont.Value = Convert.ToInt32(numericUpDownFont.Value);
                        break;
                    }
                }
            }
        }

        private void numericUpDownX_Scroll(object sender, ScrollEventArgs e)
        {
            if (NameNowChoose.Contains("Button"))
            {
                foreach (NewButton newButton in child.listButton)
                {
                    String NameBuf = newButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listButton[newButton.indexBut].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listButton[newButton.indexBut].Location.Y);
                        serButton[newButton.indexBut].X = Convert.ToInt32(numericUpDownX.Value);
                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Label"))
            {
                foreach (NewLabel newLabel in child.listLabel)
                {
                    String NameBuf = newLabel.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listLabel[newLabel.indexLab].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listLabel[newLabel.indexLab].Location.Y);
                        serLabel[newLabel.indexLab].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("TextBox"))
            {
                foreach (NewTextBox newTextBox in child.listTextBox)
                {
                    String NameBuf = newTextBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listTextBox[newTextBox.indexTextB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listTextBox[newTextBox.indexTextB].Location.Y);
                        serTextBox[newTextBox.indexTextB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("CheckBox"))
            {
                foreach (NewCheckBox newCheckBox in child.listCheckBox)
                {
                    String NameBuf = newCheckBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listCheckBox[newCheckBox.indexCheckB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listCheckBox[newCheckBox.indexCheckB].Location.Y);
                        serCheckBox[newCheckBox.indexCheckB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("RadioButton"))
            {
                foreach (NewRadioButton newRadioButton in child.listRadioButton)
                {
                    String NameBuf = newRadioButton.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listRadioButton[newRadioButton.indexRadioB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listRadioButton[newRadioButton.indexRadioB].Location.Y);
                        serRadioButton[newRadioButton.indexRadioB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureStaticBox"))
            {
                foreach (NewPictureStaticBox newPictureStaticBox in child.listPictureStaticBox)
                {
                    String NameBuf = newPictureStaticBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureStaticBox[newPictureStaticBox.indexPictureStaticB].Location.Y);
                        serPictureStaticBox[newPictureStaticBox.indexPictureStaticB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("PictureDinamicBox"))
            {
                foreach (NewPictureDinamicBox newPictureDinamicBox in child.listPictureDinamicBox)
                {
                    String NameBuf = newPictureDinamicBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].Location.Y);
                        serPictureDinamicBox[newPictureDinamicBox.indexPictureDinamicB].X = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
            if (NameNowChoose.Contains("Pitta"))
            {
                foreach (NewPictureBox newPictureBox in child.listPictureBox)
                {
                    String NameBuf = newPictureBox.Name;
                    if (NameNowChoose == NameBuf)
                    {
                        child.listPictureBox[newPictureBox.indexPictureDinamicB].Location = new Point(Convert.ToInt32(numericUpDownX.Value), child.listPictureBox[newPictureBox.indexPictureDinamicB].Location.Y);
                        serPictureDinamicBox[newPictureBox.indexPictureDinamicB].childX = Convert.ToInt32(numericUpDownX.Value);

                        break;
                    }
                }
            }
        }

        private void buttonForPicture_Click(object sender, EventArgs e)
        {
            if (buffPictures == null)
            {
                buffPictures = new List<NewBuffPictureBox>();
            }
            files = Directory.GetFiles(@"PicturesSource", "*.png", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                buffPictures.Add(new NewBuffPictureBox());
                buffPictures[i].Image = Image.FromFile(@files[i]);
                buffPictures[i].SizeMode = PictureBoxSizeMode.Zoom;
                buffPictures[i].Height = 70;
                buffPictures[i].Width = 140;
                buffPictures[i].index = i;
                flowLayoutPanel1.Controls.Add(buffPictures[i]);
                buffPictures[i].Click += new System.EventHandler(this.imageForPicture_Click);

            }
        }

        private void openFormToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (formChildIsLive == true)
            {
                child.Close();
                this.open_file();
            }
            else
            {
                this.open_file();
            }
        }

        private void offProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (formChildIsLive != false)
            {
                child.Close();
            }
        }

        private void saveAllStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveT = new SaveTest(this);
            saveT.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string nameF= "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Cursor Files|*.xml";
            openFileDialog1.Title = "Select a Cursor File";
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, @"SaveTests\");
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nameF = openFileDialog1.FileName;
                Form3 testForm = new Form3(nameF);
                testForm.ShowDialog();
            }  
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (formChildIsLive == true)
            {
                saveInFile();
                Form3 testForm = new Form3(nameFile);
                testForm.ShowDialog();
            }
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void buttonForEditTextInTheWindow_Click(object sender, EventArgs e)
        {
            EditTextForm editTextNow = new EditTextForm(this, editTextBox.Text);
            editTextNow.ShowDialog();
        }
    }
}
