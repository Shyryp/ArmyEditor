using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm1
{
    [Serializable]
    public class SerLabel
    {
        public int fontSize = 8;
        public int indexLab = 0;
        public int TrueOrFalse = 0;
        public string Text { get; set; } = "no_name";
        public string Name { get; set; } = "no_name";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int SizeX { get; set; } = 0;
        public int SizeY { get; set; } = 0;
        public SerLabel() { }
        public SerLabel(string nameForm)
        { }
    }
}
