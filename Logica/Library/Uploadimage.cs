using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class Uploadimage
    {
        private OpenFileDialog fd = new OpenFileDialog();
        public void CargarImagen(PictureBox pictureBox)
        {
            //cargar la img de forma sincronica
            pictureBox.WaitOnLoad = true;
            fd.Filter = "Imagenes|*.jpg;*.gif;*.png;*¨.bmp";
            fd.ShowDialog();
            if (fd.FileName != String.Empty)
            {
                pictureBox.ImageLocation = fd.FileName;
            }
        }

        public byte[] ImageToByte(Image image)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }


        public Image byteToImage(byte[] byteArr) {
            MemoryStream ms = new MemoryStream(byteArr);
                   
            return  Image.FromStream(ms);    
        }
    }
}
