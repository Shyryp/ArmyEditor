using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TestForm1
{
    public partial class Form2 : Form
    {
        public int formLive = 1;
        public int[] fullAllIndex = { 0,0,0,0,0,0,0 };
        public List<NewButton> listButton = new List<NewButton>();
        public List<NewLabel> listLabel = new List<NewLabel>();
        public List<NewTextBox> listTextBox = new List<NewTextBox>();
        public List<NewCheckBox> listCheckBox = new List<NewCheckBox>();
        public List<NewRadioButton> listRadioButton = new List<NewRadioButton>();
        public List<NewPictureStaticBox> listPictureStaticBox = new List<NewPictureStaticBox>();
        public List<NewPictureDinamicBox> listPictureDinamicBox = new List<NewPictureDinamicBox>();
        public List<NewPictureBox> listPictureBox = new List<NewPictureBox>();
        public int indexButton = 0;
        public int indexLabel = 0;
        public int indexTextBox = 0;
        public int indexCheckBox = 0;
        public int indexRadioButton = 0;
        public int indexPictureStaticBox = 0;
        public int indexPictureDinamicBox = 0;
        public Form1 formParent;
        public ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Удалить");
        

        //Ниже код для запрета перемещения формы пользователем
        //private Image picture;
        //private Point pictureLocation;
        const int SC_CLOSE = 0xF010;
        const int MF_BYCOMMAND = 0;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int WM_NCHITTEST = 0x0084;
        const int HTCAPTION = 2;
        [DllImport("User32.dll")]
        static extern int SendMessage(IntPtr hWnd,
        int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32.dll")]
        static extern bool RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDOWN)
            {
                int result = SendMessage(m.HWnd, WM_NCHITTEST,
                IntPtr.Zero, m.LParam);
                if (result == HTCAPTION)
                    return;
            }
            base.WndProc(ref m);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            IntPtr hMenu = GetSystemMenu(Handle, false);
            RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
        }
        //конец кода о запрете перемещения 

        public Form2(Form1 form1)
        {
            formLive = 1;
            formParent = form1;
            InitializeComponent();
            formParent.toolStripStatusLabel1.Text = "Status form: on";
            contextMenuStrip1.Items.AddRange(new[] {deleteMenuItem});
            deleteMenuItem.Click += deleteMenuItem_Click;
            //this.AllowDrop = true;
            //this.DragDrop += new DragEventHandler(this.Form2_DragDrop);
            //this.DragEnter += new DragEventHandler(this.Form2_DragEnter);
        }

        public void deleteMenuItem_Click(object sender, EventArgs e)
        {
            // если выделен текст в текстовом поле, то копируем его в буфер
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NewButton btn = sourceControl as NewButton;
                    if (btn != null)
                    {
                        for (int i = btn.indexBut+1; i < listButton.Count; i++)
                        {
                            listButton[i].indexBut = i - 1;
                            formParent.serButton[i].indexBut = i - 1;
                        }
                        formParent.treeView1.Nodes[0].Nodes.RemoveAt(btn.indexBut);
                        listButton[btn.indexBut].Dispose();
                        listButton.RemoveAt(btn.indexBut);
                        formParent.serButton.RemoveAt(btn.indexBut);
                        indexButton--;
                    }
                    NewLabel lbl = sourceControl as NewLabel;
                    if (lbl != null)
                    {
                        for (int i = lbl.indexLab + 1; i < listLabel.Count; i++)
                        {
                            listLabel[i].indexLab = i - 1;
                            formParent.serLabel[i].indexLab = i - 1;
                        }
                        formParent.treeView1.Nodes[1].Nodes.RemoveAt(lbl.indexLab);
                        listLabel[lbl.indexLab].Dispose();
                        listLabel.RemoveAt(lbl.indexLab);
                        formParent.serLabel.RemoveAt(lbl.indexLab);
                        indexLabel--;
                    }
                    NewTextBox txtb = sourceControl as NewTextBox;
                    if (txtb != null)
                    {
                        for (int i = txtb.indexTextB + 1; i < listTextBox.Count; i++)
                        {
                            listTextBox[i].indexTextB = i - 1;
                            formParent.serTextBox[i].indexTextB = i - 1;
                        }
                        formParent.treeView1.Nodes[2].Nodes.RemoveAt(txtb.indexTextB);
                        listTextBox[txtb.indexTextB].Dispose();
                        listTextBox.RemoveAt(txtb.indexTextB);
                        formParent.serTextBox.RemoveAt(txtb.indexTextB);
                        indexTextBox--;
                    }
                    NewCheckBox chb = sourceControl as NewCheckBox;
                    if (chb != null)
                    {
                        for (int i = chb.indexCheckB + 1; i < listCheckBox.Count; i++)
                        {
                            listCheckBox[i].indexCheckB = i - 1;
                            formParent.serCheckBox[i].indexCheckB = i - 1;
                        }
                        formParent.treeView1.Nodes[3].Nodes.RemoveAt(chb.indexCheckB);
                        listCheckBox[chb.indexCheckB].Dispose();
                        listCheckBox.RemoveAt(chb.indexCheckB);
                        formParent.serCheckBox.RemoveAt(chb.indexCheckB);
                        indexCheckBox--;
                    }
                    NewRadioButton rab = sourceControl as NewRadioButton;
                    if (rab != null)
                    {
                        for (int i = rab.indexRadioB + 1; i < listRadioButton.Count; i++)
                        {
                            listRadioButton[i].indexRadioB = i - 1;
                            formParent.serRadioButton[i].indexRadioB = i - 1;
                        }
                        formParent.treeView1.Nodes[4].Nodes.RemoveAt(rab.indexRadioB);
                        listRadioButton[rab.indexRadioB].Dispose();
                        listRadioButton.RemoveAt(rab.indexRadioB);
                        formParent.serRadioButton.RemoveAt(rab.indexRadioB);
                        indexRadioButton--;
                    }
                    NewPictureStaticBox psb = sourceControl as NewPictureStaticBox;
                    if (psb != null)
                    {
                        for (int i = psb.indexPictureStaticB + 1; i < listPictureStaticBox.Count; i++)
                        {
                            listPictureStaticBox[i].indexPictureStaticB = i - 1;
                            formParent.serPictureStaticBox[i].indexPictureStatic = i - 1;
                        }
                        formParent.treeView1.Nodes[5].Nodes.RemoveAt(psb.indexPictureStaticB);
                        listPictureStaticBox[psb.indexPictureStaticB].Dispose();
                        listPictureStaticBox.RemoveAt(psb.indexPictureStaticB);
                        formParent.serPictureStaticBox.RemoveAt(psb.indexPictureStaticB);
                        indexPictureStaticBox--;
                    }
                    NewPictureDinamicBox pdb = sourceControl as NewPictureDinamicBox;
                    if (pdb != null)
                    {
                        for (int i = pdb.indexPictureDinamicB + 1; i < listPictureDinamicBox.Count; i++)
                        {
                            listPictureDinamicBox[i].indexPictureDinamicB = i - 1;
                            listPictureBox[i].indexPictureDinamicB = i - 1;//
                            formParent.serPictureDinamicBox[i].indexPictureDin= i - 1;
                        }
                        formParent.treeView1.Nodes[6].Nodes.RemoveAt(pdb.indexPictureDinamicB);
                        formParent.treeView1.Nodes[7].Nodes.RemoveAt(pdb.indexPictureDinamicB);//
                        listPictureDinamicBox[pdb.indexPictureDinamicB].Dispose();
                        listPictureBox[pdb.indexPictureDinamicB].Dispose();//
                        listPictureDinamicBox.RemoveAt(pdb.indexPictureDinamicB);
                        listPictureBox.RemoveAt(pdb.indexPictureDinamicB);//
                        formParent.serPictureDinamicBox.RemoveAt(pdb.indexPictureDinamicB);
                        indexPictureDinamicBox--;
                    }
                    NewPictureBox ptt = sourceControl as NewPictureBox;
                    if (ptt != null)
                    {
                        for (int i = ptt.indexPictureDinamicB + 1; i < listPictureBox.Count; i++)
                        {
                            listPictureDinamicBox[i].indexPictureDinamicB = i - 1;
                            listPictureBox[i].indexPictureDinamicB = i - 1;//
                            formParent.serPictureDinamicBox[i].indexPictureDin = i - 1;
                        }
                        formParent.treeView1.Nodes[6].Nodes.RemoveAt(ptt.indexPictureDinamicB);
                        formParent.treeView1.Nodes[7].Nodes.RemoveAt(ptt.indexPictureDinamicB);//
                        listPictureDinamicBox[ptt.indexPictureDinamicB].Dispose();
                        listPictureBox[ptt.indexPictureDinamicB].Dispose();//
                        listPictureDinamicBox.RemoveAt(ptt.indexPictureDinamicB);
                        listPictureBox.RemoveAt(ptt.indexPictureDinamicB);//
                        formParent.serPictureDinamicBox.RemoveAt(ptt.indexPictureDinamicB);
                        indexPictureDinamicBox--;
                    }
                }
            }
            
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            /////////////////КОД ДЛЯ ПЕРЕТАСКИВАНИЯ НА ФОРМУ ИЗОБРАЖЕНИЙ - ВРЕМЕННО ОТКЛЮЧЁН///
            //protected override void OnPaint(PaintEventArgs e)
            //{
            //    base.OnPaint(e);
            //    if (this.picture != null && this.pictureLocation != Point.Empty)
            //    {
            //        e.Graphics.DrawImage(this.picture, this.pictureLocation);
            //    }
            //}

            //private void Form2_DragDrop(object sender, DragEventArgs e)
            //{
            //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //    {
            //        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //        try
            //        {
            //            this.picture = Image.FromFile(files[0]);
            //            this.pictureLocation = this.PointToClient(new Point(e.X, e.Y));
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            return;
            //        }
            //    }

            //    if (e.Data.GetDataPresent(DataFormats.Bitmap))
            //    {
            //        try
            //        {
            //            this.picture = (Image)e.Data.GetData(DataFormats.Bitmap);
            //            this.pictureLocation = this.PointToClient(new Point(e.X, e.Y));
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            return;
            //        }
            //    }
            //    this.Invalidate();
            //}

            //private void Form2_DragEnter(object sender, DragEventArgs e)
            //{
            //    // If the data is a file or a bitmap, display the copy cursor.
            //    if (e.Data.GetDataPresent(DataFormats.Bitmap) ||
            //       e.Data.GetDataPresent(DataFormats.FileDrop))
            //    {
            //        e.Effect = DragDropEffects.Copy;
            //    }
            //    else
            //    {
            //        e.Effect = DragDropEffects.None;
            //    }
            //}
        }

        public void CreateButton()
        {
            listButton.Add(new NewButton(formParent));
            //Меняем текст на новой кнопке

            listButton[indexButton].Text = "Кнопка "+(fullAllIndex[0] + 1);//
            listButton[indexButton].Name = "Button" + (fullAllIndex[0] + 1);//
            listButton[indexButton].Width = 80;
            listButton[indexButton].Height = 30;
            listButton[indexButton].Location = new Point(20, 20);
            listButton[indexButton].indexBut = indexButton;
            listButton[indexButton].AutoSize = false;
            listButton[indexButton].BringToFront();
            this.Controls.Add(listButton[indexButton]);
            formParent.serButton.Add(new SerButton());
            formParent.serButton[indexButton].Name = listButton[indexButton].Name;
            formParent.serButton[indexButton].SizeX = listButton[indexButton].Width;
            formParent.serButton[indexButton].SizeY = listButton[indexButton].Height;
            formParent.serButton[indexButton].Text = listButton[indexButton].Text;
            formParent.serButton[indexButton].X = listButton[indexButton].Location.X;
            formParent.serButton[indexButton].Y = listButton[indexButton].Location.Y;
            formParent.serButton[indexButton].indexBut = indexButton;
            listButton[indexButton].ContextMenuStrip = contextMenuStrip1;
            TreeNode buttonNode = new TreeNode(listButton[indexButton].Text);//
            buttonNode.Name = listButton[indexButton].Name;//
            formParent.treeView1.Nodes[0].Nodes.Add(buttonNode);//
            formParent.treeView1.Nodes[0].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[0].Nodes[indexButton];
            indexButton++;
            fullAllIndex[0]++;
        }

        public void CreateLabel()
        {
            listLabel.Add(new NewLabel(formParent));

            listLabel[indexLabel].Text = "Текст " + (fullAllIndex[1] + 1);
            listLabel[indexLabel].Name = "Label" + (fullAllIndex[1] + 1);
            listLabel[indexLabel].Width = 100;
            listLabel[indexLabel].Height = 50;
            listLabel[indexLabel].AutoSize = false;
            listLabel[indexLabel].Location = new Point(50, 20);
            listLabel[indexLabel].indexLab = indexLabel;
            listLabel[indexLabel].BringToFront();

            this.Controls.Add(listLabel[indexLabel]);
            formParent.serLabel.Add(new SerLabel());
            formParent.serLabel[indexLabel].Name = listLabel[indexLabel].Name;
            formParent.serLabel[indexLabel].SizeX = listLabel[indexLabel].Width;
            formParent.serLabel[indexLabel].SizeY = listLabel[indexLabel].Height;
            formParent.serLabel[indexLabel].Text = listLabel[indexLabel].Text;
            formParent.serLabel[indexLabel].X = listLabel[indexLabel].Location.X;
            formParent.serLabel[indexLabel].Y = listLabel[indexLabel].Location.Y;
            formParent.serLabel[indexLabel].indexLab = indexLabel;
            listLabel[indexLabel].ContextMenuStrip = contextMenuStrip1;
            TreeNode labelNode = new TreeNode(listLabel[indexLabel].Text);//
            labelNode.Name = listLabel[indexLabel].Name;//
            formParent.treeView1.Nodes[1].Nodes.Add(labelNode);//
            formParent.treeView1.Nodes[1].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[1].Nodes[indexLabel];
            indexLabel++;
            fullAllIndex[1]++;
        }


        public void CreateTextBox()
        {
            listTextBox.Add(new NewTextBox(formParent));

            listTextBox[indexTextBox].Text = "Окно ввода " + (fullAllIndex[2] + 1);
            listTextBox[indexTextBox].Name = "TextBox" + (fullAllIndex[2] + 1);
            listTextBox[indexTextBox].Width = 150;
            listTextBox[indexTextBox].Height = 20;
            listTextBox[indexTextBox].AutoSize = false;
            listTextBox[indexTextBox].Location = new Point(50, 20);
            listTextBox[indexTextBox].indexTextB = indexTextBox;
            listTextBox[indexTextBox].ReadOnly = true;
            listTextBox[indexTextBox].BringToFront();

            this.Controls.Add(listTextBox[indexTextBox]);
            formParent.serTextBox.Add(new SerTextBox());
            formParent.serTextBox[indexTextBox].Name = listTextBox[indexTextBox].Name;
            formParent.serTextBox[indexTextBox].SizeX = listTextBox[indexTextBox].Width;
            formParent.serTextBox[indexTextBox].SizeY = listTextBox[indexTextBox].Height;
            formParent.serTextBox[indexTextBox].Text = listTextBox[indexTextBox].Text;
            formParent.serTextBox[indexTextBox].X = listTextBox[indexTextBox].Location.X;
            formParent.serTextBox[indexTextBox].Y = listTextBox[indexTextBox].Location.Y;
            formParent.serTextBox[indexTextBox].indexTextB = indexTextBox;
            listTextBox[indexTextBox].ContextMenuStrip = contextMenuStrip1;
            TreeNode textBoxNode = new TreeNode(listTextBox[indexTextBox].Text);//
            textBoxNode.Name = listTextBox[indexTextBox].Name;//
            formParent.treeView1.Nodes[2].Nodes.Add(textBoxNode);//
            formParent.treeView1.Nodes[2].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[2].Nodes[indexTextBox];
            indexTextBox++;
            fullAllIndex[2]++;
        }

        public void CreateCheckBox()
        {
            listCheckBox.Add(new NewCheckBox(formParent));

            listCheckBox[indexCheckBox].Text = "Ответ "+(fullAllIndex[3] + 1);
            listCheckBox[indexCheckBox].Name = "CheckBox" + (fullAllIndex[3] + 1);
            listCheckBox[indexCheckBox].Width = 100;
            listCheckBox[indexCheckBox].Height = 30;
            listCheckBox[indexCheckBox].AutoSize = false;
            listCheckBox[indexCheckBox].Location = new Point(50, 20);
            listCheckBox[indexCheckBox].AutoCheck = false;
            listCheckBox[indexCheckBox].indexCheckB = indexCheckBox;
            listCheckBox[indexCheckBox].AutoCheck = false;
            listCheckBox[indexCheckBox].BringToFront();

            this.Controls.Add(listCheckBox[indexCheckBox]);
            formParent.serCheckBox.Add(new SerCheckBox());
            formParent.serCheckBox[indexCheckBox].Name = listCheckBox[indexCheckBox].Name;
            formParent.serCheckBox[indexCheckBox].SizeX = listCheckBox[indexCheckBox].Width;
            formParent.serCheckBox[indexCheckBox].SizeY = listCheckBox[indexCheckBox].Height;
            formParent.serCheckBox[indexCheckBox].Text = listCheckBox[indexCheckBox].Text;
            formParent.serCheckBox[indexCheckBox].X = listCheckBox[indexCheckBox].Location.X;
            formParent.serCheckBox[indexCheckBox].Y = listCheckBox[indexCheckBox].Location.Y;
            formParent.serCheckBox[indexCheckBox].indexCheckB = indexCheckBox;
            listCheckBox[indexCheckBox].ContextMenuStrip = contextMenuStrip1;
            TreeNode checkBoxNode = new TreeNode(listCheckBox[indexCheckBox].Text);//
            checkBoxNode.Name = listCheckBox[indexCheckBox].Name;//
            formParent.treeView1.Nodes[3].Nodes.Add(checkBoxNode);//
            formParent.treeView1.Nodes[3].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[3].Nodes[indexCheckBox];
            indexCheckBox++;
            fullAllIndex[3]++;
        }

        public void CreateRadioButton()
        {
            listRadioButton.Add(new NewRadioButton(formParent));

            listRadioButton[indexRadioButton].Text = "Ответ " + (fullAllIndex[4] + 1);
            listRadioButton[indexRadioButton].Name = "RadioButton" + (fullAllIndex[4] + 1);
            listRadioButton[indexRadioButton].Width = 100;
            listRadioButton[indexRadioButton].Height = 30;
            listRadioButton[indexRadioButton].AutoSize = false;
            listRadioButton[indexRadioButton].Location = new Point(50, 20);
            listRadioButton[indexRadioButton].AutoCheck = false;
            listRadioButton[indexRadioButton].indexRadioB = indexRadioButton;
            listRadioButton[indexRadioButton].BringToFront();

            this.Controls.Add(listRadioButton[indexRadioButton]);
            formParent.serRadioButton.Add(new SerRadioButton());
            formParent.serRadioButton[indexRadioButton].Name = listRadioButton[indexRadioButton].Name;
            formParent.serRadioButton[indexRadioButton].SizeX = listRadioButton[indexRadioButton].Width;
            formParent.serRadioButton[indexRadioButton].SizeY = listRadioButton[indexRadioButton].Height;
            formParent.serRadioButton[indexRadioButton].Text = listRadioButton[indexRadioButton].Text;
            formParent.serRadioButton[indexRadioButton].X = listRadioButton[indexRadioButton].Location.X;
            formParent.serRadioButton[indexRadioButton].Y = listRadioButton[indexRadioButton].Location.Y;
            formParent.serRadioButton[indexRadioButton].indexRadioB = indexRadioButton;
            listRadioButton[indexRadioButton].ContextMenuStrip = contextMenuStrip1;
            TreeNode radioButtonNode = new TreeNode(listRadioButton[indexRadioButton].Text);//
            radioButtonNode.Name = listRadioButton[indexRadioButton].Name;//
            formParent.treeView1.Nodes[4].Nodes.Add(radioButtonNode);//
            formParent.treeView1.Nodes[4].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[4].Nodes[indexRadioButton];
            indexRadioButton++;
            fullAllIndex[4]++;
        }
        public void CreatePictureStaicBox()
        {
            listPictureStaticBox.Add(new NewPictureStaticBox(formParent));

            listPictureStaticBox[indexPictureStaticBox].Text = "Картинка статичная " + (fullAllIndex[5] + 1);
            listPictureStaticBox[indexPictureStaticBox].Name = "PictureStaticBox" + (fullAllIndex[5] + 1);
            listPictureStaticBox[indexPictureStaticBox].Width = 200;
            listPictureStaticBox[indexPictureStaticBox].Height = 150;
            listPictureStaticBox[indexPictureStaticBox].AutoSize = false;
            listPictureStaticBox[indexPictureStaticBox].Location = new Point(50, 20);
            listPictureStaticBox[indexPictureStaticBox].indexPictureStaticB = indexPictureStaticBox;
            listPictureStaticBox[indexPictureStaticBox].Load("dev/picture1.png");
            listPictureStaticBox[indexPictureStaticBox].SizeMode = PictureBoxSizeMode.Zoom;
            listPictureStaticBox[indexPictureStaticBox].BringToFront();

            this.Controls.Add(listPictureStaticBox[indexPictureStaticBox]);
            formParent.serPictureStaticBox.Add(new SerPictureStaticBox());
            formParent.serPictureStaticBox[indexPictureStaticBox].Name = listPictureStaticBox[indexPictureStaticBox].Name;
            formParent.serPictureStaticBox[indexPictureStaticBox].SizeX = listPictureStaticBox[indexPictureStaticBox].Width;
            formParent.serPictureStaticBox[indexPictureStaticBox].SizeY = listPictureStaticBox[indexPictureStaticBox].Height;
            formParent.serPictureStaticBox[indexPictureStaticBox].Text = listPictureStaticBox[indexPictureStaticBox].Text;
            formParent.serPictureStaticBox[indexPictureStaticBox].X = listPictureStaticBox[indexPictureStaticBox].Location.X;
            formParent.serPictureStaticBox[indexPictureStaticBox].Y = listPictureStaticBox[indexPictureStaticBox].Location.Y;
            formParent.serPictureStaticBox[indexPictureStaticBox].indexPictureStatic = indexPictureStaticBox;
            formParent.serPictureStaticBox[indexPictureStaticBox].wayPictures = "dev/picture1.png";
            listPictureStaticBox[indexPictureStaticBox].ContextMenuStrip = contextMenuStrip1;
            TreeNode pictureStaticNode = new TreeNode(listPictureStaticBox[indexPictureStaticBox].Text);//
            pictureStaticNode.Name = listPictureStaticBox[indexPictureStaticBox].Name;//
            formParent.treeView1.Nodes[5].Nodes.Add(pictureStaticNode);//
            formParent.treeView1.Nodes[5].Expand();
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[5].Nodes[indexPictureStaticBox];
            indexPictureStaticBox++;
            fullAllIndex[5]++;
        }
        public void CreatePictureDinamicBox()
        {
            listPictureDinamicBox.Add(new NewPictureDinamicBox(formParent));
            listPictureBox.Add(new NewPictureBox(formParent));

            listPictureBox[indexPictureDinamicBox].Text = listPictureDinamicBox[indexPictureDinamicBox].Text = "Картинка перемещаемая " + (fullAllIndex[6] + 1);
            listPictureDinamicBox[indexPictureDinamicBox].Name = "PictureDinamicBox" + (fullAllIndex[6] + 1);
            listPictureBox[indexPictureDinamicBox].Name = "Pitta" + (fullAllIndex[6] + 1);
            listPictureBox[indexPictureDinamicBox].Width= listPictureDinamicBox[indexPictureDinamicBox].Width = 200;
            listPictureBox[indexPictureDinamicBox].Height= listPictureDinamicBox[indexPictureDinamicBox].Height = 150;
            listPictureBox[indexPictureDinamicBox].AutoSize= listPictureDinamicBox[indexPictureDinamicBox].AutoSize = false;
            listPictureBox[indexPictureDinamicBox].Location= listPictureDinamicBox[indexPictureDinamicBox].Location = new Point(50, 20);
            listPictureBox[indexPictureDinamicBox].indexPictureDinamicB = listPictureDinamicBox[indexPictureDinamicBox].indexPictureDinamicB = indexPictureDinamicBox;
            listPictureDinamicBox[indexPictureDinamicBox].Load("dev/picture2.png");
            listPictureDinamicBox[indexPictureDinamicBox].SizeMode = PictureBoxSizeMode.Zoom;
            
            

            this.Controls.Add(listPictureBox[indexPictureDinamicBox]);
            this.Controls.Add(listPictureDinamicBox[indexPictureDinamicBox]);
            listPictureBox[indexPictureDinamicBox].BringToFront();
            listPictureDinamicBox[indexPictureDinamicBox].BringToFront();
            
            formParent.serPictureDinamicBox.Add(new SerPictureDinamicBox());
            formParent.serPictureDinamicBox[indexPictureDinamicBox].childName = "Pitta" + (fullAllIndex[6] + 1);
            formParent.serPictureDinamicBox[indexPictureDinamicBox].Name = listPictureDinamicBox[indexPictureDinamicBox].Name;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].childSizeX = formParent.serPictureDinamicBox[indexPictureDinamicBox].SizeX = listPictureDinamicBox[indexPictureDinamicBox].Width;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].childSizeY = formParent.serPictureDinamicBox[indexPictureDinamicBox].SizeY = listPictureDinamicBox[indexPictureDinamicBox].Height;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].Text = listPictureDinamicBox[indexPictureDinamicBox].Text;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].childX = formParent.serPictureDinamicBox[indexPictureDinamicBox].X = listPictureDinamicBox[indexPictureDinamicBox].Location.X;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].childY = formParent.serPictureDinamicBox[indexPictureDinamicBox].Y = listPictureDinamicBox[indexPictureDinamicBox].Location.Y;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].indexPictureDin = indexPictureDinamicBox;
            formParent.serPictureDinamicBox[indexPictureDinamicBox].wayPictures = "dev/picture2.png";
            listPictureDinamicBox[indexPictureDinamicBox].ContextMenuStrip = contextMenuStrip1;
            listPictureBox[indexPictureDinamicBox].ContextMenuStrip = contextMenuStrip1;

            TreeNode pictureDinamicNode = new TreeNode(listPictureDinamicBox[indexPictureDinamicBox].Text);//
            pictureDinamicNode.Name = listPictureDinamicBox[indexPictureDinamicBox].Name;//
            formParent.treeView1.Nodes[6].Nodes.Add(pictureDinamicNode);//
            formParent.treeView1.Nodes[6].Expand();

            TreeNode pictureNode = new TreeNode(listPictureBox[indexPictureDinamicBox].Text);//
            pictureNode.Name = listPictureBox[indexPictureDinamicBox].Name;//
            formParent.treeView1.Nodes[7].Nodes.Add(pictureNode);//
            formParent.treeView1.Nodes[7].Expand();

            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[6].Nodes[indexPictureDinamicBox];
            indexPictureDinamicBox++;
            fullAllIndex[6]++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formParent.toolStripStatusLabel1.Text = "Status form: work";
        }

        private void Form2_FormClosing(object sender, CancelEventArgs e)
        {
            NewMassageBox mesBox = new NewMassageBox(this, formParent);
            mesBox.ShowDialog();
            if (formLive == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    fullAllIndex[i] = 0;
                }
                 for (int i = 0; i < 8; i++)
                {
                    formParent.treeView1.Nodes[i].Nodes.Clear();
                }
                formParent.newFormForSerializable = new SerializableForm();
                formParent.newFormForSerializable.seButton = new List<SerButton>();
                formParent.newFormForSerializable.seLabel = new List<SerLabel>();
                formParent.newFormForSerializable.seTextBox = new List<SerTextBox>();
                formParent.newFormForSerializable.seCheckBox = new List<SerCheckBox>();
                formParent.newFormForSerializable.seRadioButton = new List<SerRadioButton>();
                formParent.newFormForSerializable.sePictureDinamicBox = new List<SerPictureDinamicBox>();
                formParent.newFormForSerializable.sePictureStaticBox = new List<SerPictureStaticBox>();
                formParent.serButton = new List<SerButton>(); //лист класса для сериализуемых кнопок
                formParent.serLabel = new List<SerLabel>(); //лист класса для сериализуемых кнопок
                formParent.serTextBox = new List<SerTextBox>();
                formParent.serCheckBox = new List<SerCheckBox>();
                formParent.serRadioButton = new List<SerRadioButton>();
                formParent.serPictureStaticBox = new List<SerPictureStaticBox>();
                formParent.serPictureDinamicBox = new List<SerPictureDinamicBox>();
                formParent.buttonForDelete.Enabled = false; //кнопка удалить
                formParent.editTextBox.Enabled = false; //ввод текстa
                formParent.buttonForEditTextInTheWindow.Enabled = false; //кнопка Редактировать текст
                formParent.trackBarForFont.Enabled = false; //ползунок для изменения размера шрифта
                formParent.textBoxForHeight.Enabled = false; //изменение высоты
                formParent.textBoxForWidth.Enabled = false; //изменение ширины
                formParent.TRUECheckBox.Enabled = false;//чекбокс о правильности выбора
                formParent.numericUpDownFont.Enabled = false; //изменение размера шрифта
                formParent.trackBarHeight.Enabled = false; //изменение высоты
                formParent.trackBarWidth.Enabled = false; //изменение ширины
                formParent.numericUpDownY.Enabled = false; //изменения положения по вертикали
                formParent.numericUpDownX.Enabled = false; //изменение положения по горизонтали
                formParent.formChildIsLive = false;
                formParent.toolStripStatusLabel1.Text = "Status form: off";
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
