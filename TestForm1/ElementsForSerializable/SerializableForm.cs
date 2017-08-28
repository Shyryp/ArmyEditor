using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm1
{
    [Serializable]
    public class SerializableForm
    {
        public List<SerButton> seButton = new List<SerButton>(); //лист класса для сериализуемых кнопок
        public List<SerLabel> seLabel = new List<SerLabel>();
        public List<SerTextBox> seTextBox = new List<SerTextBox>();
        public List<SerCheckBox> seCheckBox = new List<SerCheckBox>();
        public List<SerRadioButton> seRadioButton = new List<SerRadioButton>();
        public List<SerPictureStaticBox> sePictureStaticBox = new List<SerPictureStaticBox>();
        public List<SerPictureDinamicBox> sePictureDinamicBox = new List<SerPictureDinamicBox>();
        public int[] fullAllIndex = { 0, 0, 0, 0, 0, 0, 0 };
        public SerializableForm() { }
    }
}
