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

namespace TestForm1
{
    public partial class ConfirmedTest : Form
    {
        
        String nameFile = "";
        public ConfirmedTest()
        {
            InitializeComponent();
           
        }
        public ConfirmedTest(String nameFiles)
        {
            this.nameFile = nameFiles;
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nowName = Application.StartupPath + @"SaveTests\\";
            nameFile = nameFile.Remove(0, nowName.Length);
            string writePath = @"Confirmed.txt";
            string writeBuff = @"BuffConfirmed.txt";
            FileInfo fileInf = new FileInfo(writePath);
            if (!fileInf.Exists)
            {
                fileInf.Create();
                try
                {
                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(nameFile);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                {
                    using (StreamWriter sw = new StreamWriter(writeBuff, true, System.Text.Encoding.Default))
                    {
                        string line;
                        for (int i = 0; (line = sr.ReadLine()) != null; i++)
                        {
                            if (line == nameFile)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(line);
                            }
                        }
                        sw.WriteLine(nameFile);
                    }
                }
                FileInfo fileInf1 = new FileInfo(writePath);
                if (fileInf1.Exists)
                {
                    fileInf1.Delete();
                }
                File.Move(writeBuff, writePath);
            }

            




            string readPath = @"NotConfirmed.txt";
            string buff = @"NotConfirmedBuff.txt";
            FileInfo fileInf3 = new FileInfo(readPath);
            if (!fileInf3.Exists)
            {
                fileInf3.Create();
            }
            else
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    using (StreamWriter sw = new StreamWriter(buff, true, System.Text.Encoding.Default))
                    {
                        string line;
                        for (int i = 0; (line = sr.ReadLine()) != null; i++)
                        {
                            if (line == nameFile)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
                FileInfo fileInf1 = new FileInfo(readPath);
                if (fileInf1.Exists)
                {
                    fileInf1.Delete();
                }
                File.Move(buff, readPath);
            }


            string buffer = @"SaveTests\" + nameFile;
            string buffer2 = @"ConfirmedTests\" + nameFile;
            FileInfo fileInfnew = new FileInfo(buffer);
            FileInfo fileInfnew3 = new FileInfo(buffer2);
            if (fileInfnew3.Exists)
            {
                fileInfnew3.Delete();
            }
            if (fileInfnew.Exists)
            {
                fileInfnew.CopyTo(buffer2);
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nowName = Application.StartupPath + @"SaveTests\\";
            nameFile = nameFile.Remove(0, nowName.Length);
            string writePath = @"NotConfirmed.txt";
            string writeBuff = @"BuffNotConfirmed.txt";
            FileInfo fileInf4 = new FileInfo(writePath);
            if (!fileInf4.Exists)
            {
                fileInf4.Create();
                try
                {
                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(nameFile);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                {
                    using (StreamWriter sw = new StreamWriter(writeBuff, true, System.Text.Encoding.Default))
                    {
                        string line;
                        for (int i = 0; (line = sr.ReadLine()) != null; i++)
                        {
                            if (line == nameFile)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(line);
                            }
                        }
                        sw.WriteLine(nameFile);
                    }
                }
                FileInfo fileInf1 = new FileInfo(writePath);
                if (fileInf1.Exists)
                {
                    fileInf1.Delete();
                }
                File.Move(writeBuff, writePath);
            }
            string readPath = @"Confirmed.txt";
            string buff = @"ConfirmedBuff.txt";
            FileInfo fileInf = new FileInfo(readPath);
            if (!fileInf.Exists)
            {
                fileInf.Create();
            }
            else
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    using (StreamWriter sw = new StreamWriter(buff, true, System.Text.Encoding.Default))
                    {
                        string line;
                        for (int i = 0; (line = sr.ReadLine()) != null; i++)
                        {
                            if (line == nameFile)
                            {
                                continue;
                            }
                            else
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
                FileInfo fileInf2 = new FileInfo(readPath);
                if (fileInf2.Exists)
                {
                    fileInf2.Delete();
                }
                File.Move(buff, readPath);

                string buffer = @"ConfirmedTests\" + nameFile;
                FileInfo fileInfnew2 = new FileInfo(buffer);
                if (fileInfnew2.Exists)
                {
                    fileInfnew2.Delete();
                }

                this.Close();


               
            }

        }
    }
}
