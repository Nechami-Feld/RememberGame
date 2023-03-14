using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Size size;
        int top = 0, left = 0, currentOpenCards = 0;
        List<LabelExtension> labels = new List<LabelExtension>();
        List<PictureBoxExtension> pictureBoxes = new List<PictureBoxExtension>();

        List<PictureBoxExtension> currentOpenPictures = new List<PictureBoxExtension>();

        List<PictureBox> winnerList = new List<PictureBox>();
        
            
        public Form1()
        {
            InitializeComponent();

            

            size = new Size(100, 120);

            List<string> files = GetFiles();
            
            IEnumerable<string> pictures = files.Where(file => new string[]{ "PNG", "JPG"}.Contains( file.ToUpper().Split('.')[file.ToUpper().Split('.').Length - 1]));

            List<BitmapGame> res = InitListGame(pictures);
            InitGameBoard(res);

        }


        private void InitGameBoard(List<BitmapGame> pictures)
        {
            int index = 0;
            Random rnd = new Random();
            while (pictures.Count > 0)
            {
                int indexBoard = rnd.Next(pictures.Count);

                PictureBoxExtension p = new PictureBoxExtension(size, index);
                p.Image = pictures[indexBoard].bitmap;

                p.Top = top;
                p.Left = left;

                p.indexGame = pictures[indexBoard].indexGame;

                p.Click += new EventHandler(pictureBox_Click);
                
                LabelExtension lbl = new LabelExtension(size, index);
                lbl.Click += new EventHandler(label_Click);                

                lbl.Top = top;
                lbl.Left = left;

                CalculateLocation();

                Controls.Add(p);
                pictureBoxes.Add(p);
                Controls.Add(lbl);
                labels.Add(lbl);


                pictures.RemoveAt(indexBoard);
                index++;
            }
            
        }

        void label_Click(object sender, EventArgs e)
        {
            PictureBoxExtension pictureBox = null;

            if (currentOpenCards < 2)
            {
                ((Control)sender).Visible = false;
                pictureBox = pictureBoxes.Find(item => item.index == ((LabelExtension)sender).index);
                pictureBox.Visible = true;
                currentOpenCards++;
                currentOpenPictures.Add(pictureBox);
            }

            if (currentOpenCards == 2)
            {
                if (currentOpenPictures[0].indexGame == currentOpenPictures[1].indexGame)
                {
                    winnerList.Add(currentOpenPictures[0]);
                    currentOpenPictures[0].Visible = false; 
                    currentOpenPictures[1].Visible = false;
                    currentOpenPictures = new List<PictureBoxExtension>();

                    pictureBox1.Image = pictureBox.Image ;

                    currentOpenCards = 0;
                    if (winnerList.Count == 20)
                    {
                        timer1.Stop();
                        label2.Text = "Game Finished At: " + label2.Text;
                        MessageBox.Show("You Are The Winner");   
                    }
                }
            }
            
        }

        void pictureBox_Click(object sender, EventArgs e)
        {
            ((Control)sender).Visible = false;
            LabelExtension label = labels.Find(item => item.index == ((PictureBoxExtension)sender).index);

            label.Visible = true;

            currentOpenCards--;
            currentOpenPictures.RemoveAll(item => item.index == ((PictureBoxExtension)sender).index);
        }

        private List<BitmapGame> InitListGame(IEnumerable<string> pictures)
        {
            List<BitmapGame> res = new List<BitmapGame>();

            int count = 0;
            foreach (string file in pictures)
            {
                count++;

                if (count > 20)
                {
                    break;
                }
                BitmapGame bitmap = new BitmapGame(count, file);


                res.Add(bitmap);
                res.Add(bitmap);
            }

            return res;
        }

        private void CalculateLocation()
        {
            if (left + size.Width < this.Width)
            {
                left += size.Width + 10;
            }
            else
            {
                top += size.Height + 10;
                left = 0;
            }
        }

        public List<string> GetFiles()
        {
            FolderBrowserDialog f = new FolderBrowserDialog();

            if (f.ShowDialog() == DialogResult.OK)
            {
                timer1.Start();

                label1.Text = "Game Started At: " + DateTime.Now.ToLongTimeString();
                label2.Text = label1.Text;

                return SearchImages(f.SelectedPath);
            }
            throw new Exception();
        }

        public List<string> SearchImages(string SelectedPath)
        {
            List<string> images = new List<string>();

            images.AddRange(Directory.GetFiles(SelectedPath).ToList());

            foreach (string directoryName in Directory.GetDirectories(SelectedPath))
            {
                images.AddRange(SearchImages(directoryName));
            }

            return images;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

    }
}
