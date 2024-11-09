using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LR6_TVis
{
    public partial class Form1 : Form
    {
        List<CvRect> ObjsList = new List<CvRect>();
        List<List<double>> objListForPaint = new List<List<double>>();
        int[,] coordinates = new int[6, 3];//массив координат всех маркеров, на теретории поля, 1 столбец - номер маркера, потом х и у 
        IplImage frame = null; // Буфер для хранения изображения
        CvCapture capture = null; // Устройство захвата изображений
        CvWindow windowCapture = new CvWindow("Video_Capture"); // Создаем окно
        public Form1()
        {
            InitializeComponent();
            heightBox.Text = "20";
            rotationBox.Text = "15";
            fShiftBox.Text = "5";
            rShiftBox.Text = "5";
        }

        private void loadIm_Btn_Click(object sender, EventArgs e)
        {
            timer.Stop();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                frame = Cv.LoadImage(openFileDialog.FileName);
                windowCapture.ShowImage(frame);
            }
            else
            {
                MessageBox.Show("[!] Error: can't open image file.");
            }
        }

        private void loadVid_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files|*.mp4;*.avi|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                capture = CvCapture.FromFile(openFileDialog.FileName);
            }
            else
            {
                capture = CvCapture.FromCamera(0); // Открытие камеры по умолчанию
            }
            if (capture != null)
            {
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            frame = capture.QueryFrame();
            if (frame != null)
            {
                windowCapture.ShowImage(objectsFounder());
            }
        }

        private void btn_Outline_Click(object sender, EventArgs e)
        {
            windowCapture.ShowImage(objectsFounder());
        }

        private IplImage objectsFounder()
        {
            objListForPaint.Clear();
            IplImage detected_obj_im = frame.Clone();
            IplImage frameCL = frame.Clone();
            List<List<int>> objects = new List<List<int>>();
            bool good_object = true;

            // Бинаризация изображения
            IplImage black = new IplImage(frameCL.Size, BitDepth.U8, 1);
            Cv.InRangeS(frame, new CvScalar(0, 0, 0), new CvScalar(180, 255, 60), black);

            // Найдите контуры
            CvSeq<CvPoint> contours;
            CvMemStorage storage = new CvMemStorage();
            Cv.FindContours(black, storage, out contours);
            int minSize = 25; // минимальная сторона
            int maxSize = 300; // максимальная сторона
            for (CvSeq<CvPoint> current = contours; current != null; current = current.HNext)
            {
                good_object = true;
                CvRect rect = Cv.BoundingRect(current);
                int x = rect.X + rect.Width / 2;
                int y = rect.Y + rect.Height / 2;

                if (rect.Height < minSize || rect.Height > maxSize || rect.Width > rect.Height * 1.5 || rect.Height > rect.Width * 10)
                    continue;

                foreach (List<int> obj in objects.ToList())
                {
                    if (Math.Abs(obj[0] - x) <= 10 && Math.Abs(obj[1] - y) <= 10)
                    {
                        if (obj[2] < rect.Height) { objects.Remove(obj); }
                        else { good_object = false; }
                    }
                }

                if (good_object)
                {
                    List<int> center = new List<int>(); //координаты центра объекта
                    center.Add(rect.X + rect.Width / 2);
                    center.Add(rect.Y + rect.Height / 2);
                    center.Add(rect.Height);
                    center.Add(rect.Width);
                    objects.Add(center);
                }

                //Cv.DrawContours(detected_obj_im, current, CvColor.Green, CvColor.Red, 1, 1, LineType.Link8);
                //detected_obj_im.Rectangle(rect, CvColor.Blue, 2);
                //CompareWithTemplate(rect, detected_obj_im);
            }

            foreach (List<int> obj in objects)
            {
                CvRect rect = new CvRect(obj[0] - obj[3] / 2, obj[1] - obj[2] / 2, obj[3], obj[2]);
                IplImage frame_clone = frame.Clone();
                CompareWithTemplate(rect, frame_clone);
            }
            foreach (List<double> infObj in objListForPaint)
            {
                int width = (int)infObj[6];
                int height = (int)infObj[7];
                int x = (int)infObj[4];
                int y = (int)infObj[5];
                int x_center = x + width / 2;
                int y_center = y + height / 2;
                double percant = infObj[3];
                detected_obj_im.DrawLine((frame.Width / 2), frame.Height, x_center, y_center, CvColor.DarkGreen, 2); //gipotenuza
                detected_obj_im.DrawLine((frame.Width / 2), frame.Height, (frame.Width / 2), y_center, CvColor.DarkGreen, 2); //vertical
                detected_obj_im.DrawLine((frame.Width / 2), y_center, x_center, y_center, CvColor.DarkGreen, 2); //horizontal
                int i = (int)infObj[0];
                detected_obj_im.PutText($"Match: {percant:F2}%, {i}", new CvPoint(x, y - 10), new CvFont(FontFace.HersheySimplex, 0.4, 0.4, 0, 1, LineType.AntiAlias), CvColor.Red);
                detected_obj_im.Rectangle(new CvRect(x, y, width, height), CvColor.Red, 2);
            }
            /*foreach (CvRect obj in ObjsList)
            {
                detected_obj_im.Rectangle(obj, CvColor.Red, 2);
            }*/

            // Задача 5: Вычисление положения и ориентации камеры по парам маркеров
            //var markerPairs = GetMarkerPairs(objects);
            //CalculateCameraPositionAndOrientation(markerPairs);

            // Задача 7: Отрисовка поля, маркеров, робота и камеры
            using (Graphics gr = mapBox.CreateGraphics())
            {
                if (objListForPaint.Any()) { DrawField(gr, objListForPaint, frame); }
            }

            return detected_obj_im;
            //windowCapture.ShowImage(detected_obj_im);
        }

        private void CompareWithTemplate(CvRect rect, IplImage frameWithTemplate)
        {
            double width = rect.Width;
            double height = rect.Height;
            double x = rect.X + rect.Width / 2;
            double y = rect.Y + rect.Height / 2;
            int best_compare = 0;
            double x_center = x + width / 2;
            double y_center = y + height / 2;
            double gipotenuza = Math.Sqrt(Math.Pow((double)x_center - (frame.Width / 2), 2) + Math.Pow((frame.Height / 2) - (double)y_center, 2));
            double dx = ((double)x_center - (frame.Width / 2)) / (double)height;
            //double gipotenuza = Math.Sqrt(Math.Pow((320 - x), 2) + Math.Pow((480 - y), 2));
            float angle = (float)Math.Acos(((frame.Height / 2) - (double)y_center) / gipotenuza);
            double matchPercentage = 0;
            double best_percentage = 65;
            bool found = false;
            //List<double> percentages = new List<double> ();
            //percentages.Add(50);
            for (int i = 0; i < 6; i++)
            {
                // Установка ROI и копирование фрагмента изображения
                frameWithTemplate.SetROI(rect);
                IplImage fragment = frameWithTemplate.Clone();
                frameWithTemplate.ResetROI();

                IplImage maskImage = Cv.LoadImage($"C:\\Users\\fedot\\source\\repos\\tvision\\LR6_TVis\\temps\\{i}.png");

                // Изменение размера фрагмента до размера шаблона
                IplImage resizedFragment = new IplImage(maskImage.Size, maskImage.Depth, maskImage.NChannels);
                Cv.Resize(fragment, resizedFragment, Interpolation.Linear);

                IplImage black = new IplImage(maskImage.Size, BitDepth.U8, 1);

                // Пороговые значения для красного цвета
                Cv.InRangeS(resizedFragment, new CvScalar(0, 0, 0), new CvScalar(180, 255, 60), black);

                // Преобразование maskImage в grayscale и изменение размера до размера шаблона, если требуется
                IplImage resizedMaskImage = new IplImage(maskImage.Size, BitDepth.U8, 1);
                Cv.CvtColor(maskImage, resizedMaskImage, ColorConversion.BgrToGray);

                // Преобразование в бинарное изображение
                IplImage binaryImage = new IplImage(resizedMaskImage.Size, BitDepth.U8, 1);
                Cv.Threshold(resizedMaskImage, binaryImage, 128, 255, ThresholdType.Binary);

                Bitmap black_im = black.ToBitmap();
                Bitmap binaryImage_im = binaryImage.ToBitmap();
                int compare_pix = 0;
                int all_pix = 0;

                // Попиксельное сравнение с шаблоном с учетом маски
                IplImage result = new IplImage(maskImage.Size, BitDepth.U8, 1);
                Cv.Xor(black, binaryImage, result);
                Cv.Not(result, result);
                // Вычисление количества белых пикселей                
                all_pix = result.Width * result.Height;
                compare_pix = Cv.CountNonZero(result);

                matchPercentage = (double)compare_pix / all_pix * 100;

                // Вывод результата
                if (matchPercentage > best_percentage)
                {
                    best_compare = i;
                    best_percentage = matchPercentage;
                    //percentages.Add(matchPercentage);
                    found = true;
                }
            }
            if (found)
            {
                if (objListForPaint.Any())
                {
                    bool victory = true;
                    foreach (List<double> infObj in objListForPaint.ToList())
                    {
                        if (infObj[0] == best_compare && infObj[3] < best_percentage)
                        {
                            objListForPaint.Remove(infObj);
                            List<double> infoObj = new List<double>();
                        }
                        else if (infObj[0] == best_compare && infObj[3] >= best_percentage) { victory = false; }
                    }
                    if (victory)
                    {
                        List<double> infoObj = new List<double>();
                        infoObj.Add(best_compare); //0
                        infoObj.Add(gipotenuza);
                        infoObj.Add(angle);
                        infoObj.Add(best_percentage); //3
                        infoObj.Add(rect.X);
                        infoObj.Add(rect.Y);
                        infoObj.Add(rect.Width); //6
                        infoObj.Add(rect.Height);
                        infoObj.Add(dx);
                        objListForPaint.Add(infoObj);
                    }
                }
                else
                {
                    List<double> infoObj = new List<double>();
                    infoObj.Add(best_compare); //0
                    infoObj.Add(gipotenuza);
                    infoObj.Add(angle);
                    infoObj.Add(best_percentage); //3
                    infoObj.Add(rect.X);
                    infoObj.Add(rect.Y);
                    infoObj.Add(rect.Width); //6
                    infoObj.Add(rect.Height);
                    infoObj.Add(dx);

                    objListForPaint.Add(infoObj);
                }
            }
        }

        private void DrawField(Graphics gr, List<List<double>> params_objects, IplImage frame)
        {
            int height = int.Parse(heightBox.Text.ToString());
            int rotation = int.Parse(rotationBox.Text.ToString());
            int rShift = int.Parse(rShiftBox.Text.ToString());
            int fShift = int.Parse(fShiftBox.Text.ToString());
            // Отрисовка поля
            gr.Clear(Color.White);

            for (int i = 0; i < 6; i++)
            {
                int x = int.Parse(coordsListBox.Items[i].ToString().Split(' ')[1]);
                int y = int.Parse(coordsListBox.Items[i].ToString().Split(' ')[2]);
                gr.FillEllipse(Brushes.Red, x, y, 6, 6);
            }
            double min_dx = 99999999;
            int n = 0;
            foreach (List<double> obj in params_objects)
            {
                if (Math.Abs(obj[8]) < min_dx)
                {
                    min_dx = obj[8];
                    n = params_objects.IndexOf(obj);
                }
            }
            int ii = (int)params_objects[n][0];
            int x1 = int.Parse(coordsListBox.Items[(int)params_objects[n][0]].ToString().Split(' ')[1]);
            int y1 = int.Parse(coordsListBox.Items[(int)params_objects[n][0]].ToString().Split(' ')[2]);
            double g1 = params_objects[n][1];
            double a1 = params_objects[n][2];
            double dx = params_objects[n][8];
            double g = 5000 / params_objects[n][7];

            if (ii == 0)
            {
                gr.DrawLine(new Pen(Color.Red), x1 + 3, y1, x1 + 3, (int)Math.Round(y1 + dx * 20)); //hor
                gr.DrawLine(new Pen(Color.Red), x1 + 3, (int)Math.Round(y1 + dx * 20), (int)Math.Round(x1 - Math.Cos(a1) * g1), (int)Math.Round(y1 + dx * 20)); //vert
                gr.DrawLine(new Pen(Color.Red), x1 + 3, y1, (int)Math.Round(x1 - Math.Cos(a1) * g1), (int)Math.Round(y1 + dx * 20)); //gip
                gr.FillEllipse(new SolidBrush(Color.DarkBlue), (int)Math.Round(x1 - Math.Cos(a1) * g1), (int)Math.Round(y1 + dx * 20) - 10, 20, 20);
            }
            else if (ii == 4 || ii == 5)
            {
                gr.DrawLine(new Pen(Color.Red), x1 + 3, y1, x1 + 3, (int)Math.Round(y1 - dx * 20)); //hor
                gr.DrawLine(new Pen(Color.Red), x1 + 3, (int)Math.Round(y1 - dx * 20), (int)Math.Round(x1 + Math.Cos(a1) * g1), (int)Math.Round(y1 - dx * 20)); //vert
                gr.DrawLine(new Pen(Color.Red), x1 + 3, y1, (int)Math.Round(x1 + Math.Cos(a1) * g1), (int)Math.Round(y1 - dx * 20)); //gip
                gr.FillEllipse(new SolidBrush(Color.DarkBlue), (int)Math.Round(x1 + Math.Cos(a1) * g1) - 10, (int)Math.Round(y1 - dx * 20) - 10, 20, 20);
            }
            else
            {
                gr.DrawLine(new Pen(Color.Red), x1, y1 + 3, (int)Math.Round(x1 - dx * 20), y1 + 3); //hor
                gr.DrawLine(new Pen(Color.Red), x1 + 3, y1 + 3, (int)Math.Round(x1 - dx * 20), (int)(y1 - Math.Cos(a1) * g1)); // gip
                gr.DrawLine(new Pen(Color.Red), (int)Math.Round(x1 - dx * 20), y1 + 3, (int)Math.Round(x1 - dx * 20), (int)(y1 - Math.Cos(a1) * g1)); //vert
                gr.FillEllipse(new SolidBrush(Color.DarkBlue), (int)Math.Round(x1 - dx * 20) - 10, (int)(y1 - Math.Cos(a1) * g1), 20, 20);
            }
            //int dx = x1 + int.Parse(Math.Round(Math.Sin(a) * g).ToString()); //horizontal
            //int dy = y1 + int.Parse(Math.Round(Math.Cos(a) * g).ToString()); //vertical
            //gr.FillEllipse(Brushes.DarkBlue, dx, dy, 20, 20);
        }
    }
}