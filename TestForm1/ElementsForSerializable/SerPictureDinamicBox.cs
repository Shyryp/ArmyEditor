using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm1
{
    [Serializable]
    public class SerPictureDinamicBox
    {
        public string wayPictures;
        public int fontSize = 8;
        public int indexPictureDin = 0;
        public int TrueOrFalse = 0;
        public string Text { get; set; } = "no_name";
        public string Name { get; set; } = "no_name";
        public string childName { get; set; } = "no_name";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int SizeX { get; set; } = 0;
        public int SizeY { get; set; } = 0;
        public int childX { get; set; } = 0;
        public int childY { get; set; } = 0;
        public int childSizeX { get; set; } = 0;
        public int childSizeY { get; set; } = 0;
        public SerPictureDinamicBox() { }
        public SerPictureDinamicBox(string nameForm)
        { }
    }
}
