using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class LabelExtension: Label
    {
        public int index { get; set; }
        public int indexGame { get; set; }

        public LabelExtension(Size size, int index)
        {
            this.index = index;

            Size = size;
            AutoSize = false;
            BorderStyle = BorderStyle.Fixed3D;

            Visible = true;

        }
    }

    public class PictureBoxExtension : PictureBox
    {
        public int index { get; set; }
        public int indexGame { get; set; }

        public PictureBoxExtension(Size size, int index)
        {
            this.index = index;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = size; 
            Visible = false;

        }
    }

    public class BitmapGame
    {
        public int indexGame { get; set; }
        public Bitmap bitmap { get; set; }

        public BitmapGame(int indexGame, string file)
        {
            this.indexGame = indexGame;
            this.bitmap = new Bitmap(file);
        }
    }
}
