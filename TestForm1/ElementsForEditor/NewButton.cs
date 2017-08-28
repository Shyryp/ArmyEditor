using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm1
{
    public class NewButton:Button
    {
        public int indexBut = 0;
        public int TrueOrFalse = 0;
        //точка перемещения
        Point DownPoint;
        //нажата ли кнопка мыши
        bool IsDragMode;
        public Form1 formParent;
        public NewButton(Form1 formPar)
        {
            formParent = formPar;
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            DownPoint = mevent.Location;
            IsDragMode = true;
            formParent.treeView1.SelectedNode = formParent.treeView1.Nodes[0].Nodes[indexBut];
            
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            IsDragMode = false;
            if (Location.Y < 0)
            {
                formParent.numericUpDownY.Value = 0;
                Location = new Point(Location.X, 0);
            }
            else if (Location.Y > 519)
            {
                formParent.numericUpDownY.Value = 510;
                Location = new Point(Location.X, 510);
            }
            else
            {
                formParent.numericUpDownY.Value = Location.Y;
            }

            if (Location.X < 0)
            {
                formParent.numericUpDownX.Value = 0;
                Location = new Point(0, Location.Y);
            }
            else if (Location.X > 769)
            {
                formParent.numericUpDownX.Value = 760;
                Location = new Point(760, Location.Y);
            }
            else
            {
                formParent.numericUpDownX.Value = Location.X;
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            //если кнопка мыши нажата
            if (IsDragMode)
            {
                Point p = mevent.Location;
                //вычисляем разницу в координатах между положением курсора и "нулевой" точкой кнопки
                Point dp = new Point(p.X - DownPoint.X, p.Y - DownPoint.Y);
                Location = new Point(Location.X + dp.X, Location.Y + dp.Y);
            }
            base.OnMouseMove(mevent);
            formParent.serButton[indexBut].X = Location.X;
            formParent.serButton[indexBut].Y = Location.Y;
        }
    }
}
