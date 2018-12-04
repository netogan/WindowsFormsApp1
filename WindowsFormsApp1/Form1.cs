using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using FaceDetection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opt = new OpenFileDialog();

            if (opt.ShowDialog(this) == DialogResult.OK)
            {
                imageBox1.Image = GetFacePoints(opt.FileName);
            }
        }

        public Image<Bgr, Byte> GetFacePoints(string fileName)
        {
            //var faceDetector = new CascadeClassifier(@"c:\\users\\acdev\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\haarcascade_frontalface_alt2.xml");

            IImage image;

            image = new UMat(fileName, ImreadModes.Color);

            long detectionTime;

            var faces = new List<Rectangle>();
            var eyes = new List<Rectangle>();

            DetectFace.Detect(image, "c:\\users\\acdev\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\haarcascade_frontalface_default.xml",
                "c:\\users\\acdev\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\haarcascade_eye.xml", faces, eyes, out detectionTime);

            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in eyes)
                CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);





            //using (Image<Bgr, byte> nextFrame = cap.QueryFrame())
            //{
            //    if (nextFrame != null)
            //    {
            //        // there's only one channel (greyscale), hence the zero index
            //        //var faces = nextFrame.DetectHaarCascade(haar)[0];
            //        Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();
            //        var faces =
            //            grayframe.DetectHaarCascade(
            //                haar, 1.4, 4,
            //                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
            //                new Size(nextFrame.Width / 8, nextFrame.Height / 8)
            //            )[0];
            //        foreach (var face in faces)
            //        {
            //            nextFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
            //        }
            //        pictureBox1.Image = nextFrame.ToBitmap();
            //    }
            //}


            //var fParams = new FacemarkLBFParams
            //{
            //    NLandmarks = 300,
            //    InitShapeN = 10,
            //    StagesN = 5,
            //    TreeN = 6,
            //    TreeDepth = 5,
            //    ModelFile = @"c:\\users\\acdev\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\lbfmodel.yaml"
            //};

            //var facemark = new FacemarkLBF(fParams);

            //var image = new Image<Bgr, byte>(fileName);
            //var grayImage = image.Convert<Gray, byte>();

            //grayImage._EqualizeHist();

            //var faces = new VectorOfRect(faceDetector.DetectMultiScale(grayImage));

            //var landmarks = new VectorOfVectorOfPointF();

            //facemark.LoadModel(fParams.ModelFile);

            //var success = facemark.Fit(grayImage, faces, landmarks);

            //if (!success) return null;

            //var facesRect = faces.ToArray();

            //foreach (var face in facesRect)
            //{
            //    image.Draw(face, new Bgr(Color.Red), 2);
            //}

            //for (var i = 0; i < facesRect.Length; i++)
            //{
            //    image.Draw(facesRect[i], new Bgr(Color.Red), 2);
            //    //FaceInvoke.DrawFacemarks(image, landmarks[i], new Bgr(Color.Blue).MCvScalar);
            //}

            return new Image<Bgr, byte>(image.Bitmap);
        }
    }
}
