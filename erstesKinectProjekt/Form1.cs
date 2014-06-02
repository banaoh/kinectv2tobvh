using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Manuell hinzugefügt
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Media3D;




namespace Kinect2BVH
{
    public partial class MainWindow : Form
    {

        /// Active Kinect sensor
        private KinectSensor sensor;
        private short fpsEnd = 1;
        private writeBVH BVHFile;
        Bitmap tempColorFrame;
        bool windowClosing = false;
        int initFrames = 1;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (sensor == null)
            {
                // Look through all sensors and start the first connected one.
                // This requires that a Kinect is connected at the time of app startup.
                // To make your app robust against plug/unplug, 
                // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
                foreach (var potentialSensor in KinectSensor.KinectSensors)
                {
                    this.textBox_sensorStatus.Text = "Suche...";
                    if (potentialSensor.Status == KinectStatus.Connected)
                    {
                        this.sensor = potentialSensor;
                        break;
                    }
                }

                if (null != this.sensor)
                {
                    TransformSmoothParameters smoothingParam = new TransformSmoothParameters();
               
                    if (radioButton_smoothDefault.Checked)
                    {
                        // Some smoothing with little latency (defaults).
                        // Only filters out small jitters.
                        // Good for gesture recognition in games.
                        smoothingParam = new TransformSmoothParameters();
                        {
                            smoothingParam.Smoothing = 0.5f;
                            smoothingParam.Correction = 0.5f;
                            smoothingParam.Prediction = 0.5f;
                            smoothingParam.JitterRadius = 0.05f;
                            smoothingParam.MaxDeviationRadius = 0.04f;
                        };
                    }
                    else if (radioButton_smoothModerate.Checked)
                    {

                        // Smoothed with some latency.
                        // Filters out medium jitters.
                        // Good for a menu system that needs to be smooth but
                        // doesn't need the reduced latency as much as gesture recognition does.
                        smoothingParam = new TransformSmoothParameters();
                        {
                            smoothingParam.Smoothing = 0.5f;
                            smoothingParam.Correction = 0.1f;
                            smoothingParam.Prediction = 0.5f;
                            smoothingParam.JitterRadius = 0.1f;
                            smoothingParam.MaxDeviationRadius = 0.1f;
                        };
                    }
                    else if (radioButton_smoothIntense.Checked)
                    {
                        // Very smooth, but with a lot of latency.
                        // Filters out large jitters.
                        // Good for situations where smooth data is absolutely required
                        // and latency is not an issue.
                        smoothingParam = new TransformSmoothParameters();
                        {
                            smoothingParam.Smoothing = 0.7f;
                            smoothingParam.Correction = 0.3f;
                            smoothingParam.Prediction = 1.0f;
                            smoothingParam.JitterRadius = 1.0f;
                            smoothingParam.MaxDeviationRadius = 1.0f;
                        };
                    }
                    groupBox_smooth.Enabled = false;



                    // Turn on the stream to receive frames
                    this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    //this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    this.sensor.SkeletonStream.Enable(smoothingParam);

                    // event handler to be called whenever there is new frame data
                    this.sensor.AllFramesReady += sensor_allFramesReady;


                    // Start the sensor!
                    try
                    {
                        this.sensor.Start();
                        this.textBox_sensorStatus.Text = "Stream gestartet";
                    }
                    catch (Exception)
                    {
                        this.sensor = null;
                        this.textBox_sensorStatus.Text = "Stream konnte nicht gestartet werden";
                    }
                }
                else
                {
                    this.textBox_sensorStatus.Text = "Keine Kinect gefunden";
                }
            }

        }


        private void button_stop_Click(object sender, EventArgs e)
        {
            if (BVHFile != null)
            {
                this.textBox_sensorStatus.Text = "Erst Aufnahme stoppen!";
            }
            else
            {
                if (sensor != null)
                {
                    StopKinect(sensor);
                    this.textBox_sensorStatus.Text = "Stream beendet";
                    this.pictureBox_skeleton.Image = null;
                    groupBox_smooth.Enabled = true;
                }
            }
        }

        void sensor_allFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (windowClosing)
            {
                return;
            }
            int value;
            if (int.TryParse(this.textBox_init.Text, out value))
            {
                initFrames = value;
            }

           
            if (fpsEnd == 1)
            {
                //FPS Auswahl. Bei niedrigen Frameraten werden empfangene Frames übersprungen (nicht angezeigt)
                Int16 fps = Convert.ToInt16(this.dropDown_fps.Text);
                switch (fps)
                {
                    case 30:
                        fpsEnd = 1;
                        break;
                    case 15:
                        fpsEnd = 2;
                        break;
                    case 10:
                        fpsEnd = 3;
                        break;
                    case 5:
                        fpsEnd = 6;
                        break;
                    case 1:
                        fpsEnd = 30;
                        break;
                }

                using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
                {

                    if (colorFrame != null)
                    {
                        // Kinect Color Frame to Bitmap
                        tempColorFrame = ColorImageFrameToBitmap(colorFrame);
                        //this.pictureBox_colorPic.Image = new Bitmap(tempColorFrame, this.pictureBox_colorPic.Width, this.pictureBox_colorPic.Height);
                    }
                }
                /*
                                using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                                {
                                    if (depthFrame != null)
                                    {
                                        // Kinect Depth Frame to Bitmap
                                        Bitmap tempDepthFrame = DepthImageFrameToBitmap(depthFrame);
                                        this.pictureBox_depthPic.Image = new Bitmap(tempDepthFrame, this.pictureBox_depthPic.Width, this.pictureBox_depthPic.Height);
                                    }
                                }
                */
                using (SkeletonFrame skelFrame = e.OpenSkeletonFrame())
                {
                    if (skelFrame != null)
                    {
                        Image tempSkeletonFrame = new Bitmap(this.pictureBox_skeleton.Width, this.pictureBox_skeleton.Height);
                        // zeichne Skelette auf schwarzem Hintergrund in der Picturebox
                        this.pictureBox_skeleton.BackColor = Color.Black;

                        if (checkBox_colorCam.Checked)
                        {
                            tempSkeletonFrame = tempColorFrame;
                        }

                        Skeleton[] skeletons = new Skeleton[skelFrame.SkeletonArrayLength];
                        skelFrame.CopySkeletonDataTo(skeletons);
                        if (skeletons.Length != 0)
                        {
                            foreach (Skeleton skel in skeletons)
                            {
                                if (skel.TrackingState == SkeletonTrackingState.Tracked)
                                {
                                    //Zeichne Skelett
                                    DrawSkeletons(tempSkeletonFrame, skel);

                                    if (BVHFile != null)
                                    {
                                        if (BVHFile.isRecording == true && BVHFile.isInitializing == true)
                                        {
                                            BVHFile.Entry(skel);

                                            if (BVHFile.intializingCounter > initFrames)
                                            {
                                                BVHFile.startWritingEntry();
                                            }

                                        }

                                        if (BVHFile.isRecording == true && BVHFile.isInitializing == false)
                                        {
                                            BVHFile.Motion(skel);
                                            this.textBox_sensorStatus.Text = "Record";
                                            this.textBox_sensorStatus.BackColor = Color.Green;
                                        }
                                    }
                                }
                            }
                        }
                        this.pictureBox_skeleton.Image = tempSkeletonFrame;
                    }
                }
            }
            else
            {
                fpsEnd -= 1;
            }
        }

        private Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        {
            byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixelBuffer);
            Bitmap bitmapFrame = ArrayToBitmap(pixelBuffer, colorFrame.Width, colorFrame.Height, PixelFormat.Format32bppRgb);
            return bitmapFrame;
        }

        //wird aktuell nicht gebraucht
        private Bitmap DepthImageFrameToBitmap(DepthImageFrame depthFrame)
        {
            DepthImagePixel[] depthPixels = new DepthImagePixel[depthFrame.PixelDataLength];
            byte[] colorPixels = new byte[depthFrame.PixelDataLength * 4];
            depthFrame.CopyDepthImagePixelDataTo(depthPixels);

            // Get the min and max reliable depth for the current frame
            int minDepth = depthFrame.MinDepth;
            int maxDepth = depthFrame.MaxDepth;

            // Convert the depth to RGB
            int colorPixelIndex = 0;
            for (int i = 0; i < depthPixels.Length; ++i)
            {
                // Get the depth for this pixel
                short depth = depthPixels[i].Depth;

                // To convert to a byte, we're discarding the most-significant
                // rather than least-significant bits.
                // We're preserving detail, although the intensity will "wrap."
                // Values outside the reliable depth range are mapped to 0 (black).

                // NOTE: Using conditionals in this loop could degrade performance.
                // Consider using a lookup table instead when writing production code.
                // See the KinectDepthViewer class used by the KinectExplorer sample
                // for a lookup table example.
                byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                // Write out blue byte
                colorPixels[colorPixelIndex++] = intensity;

                // Write out green byte
                colorPixels[colorPixelIndex++] = intensity;

                // Write out red byte                        
                colorPixels[colorPixelIndex++] = intensity;

                // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                // If we were outputting BGRA, we would write alpha here.
                ++colorPixelIndex;
            }
            Bitmap bitmapFrame = ArrayToBitmap(colorPixels, depthFrame.Width, depthFrame.Height, PixelFormat.Format32bppRgb);
            return bitmapFrame;
        }

        private void DrawSkeletons(Image backgroundImage, Skeleton skel)
        {
            Graphics graphicBox = Graphics.FromImage(backgroundImage);
            float width = (float)(backgroundImage.Width / 640F);
            float height = (float)(backgroundImage.Height / 480F);
            graphicBox.ScaleTransform(width, height);
            this.DrawBonesAndJoints(skel, graphicBox);
        }

        private void button_rec_Click(object sender, EventArgs e)
        {
            if (BVHFile == null && sensor != null)
            {
                this.textBox_sensorStatus.Text = "Initialisiere";
                this.textBox_sensorStatus.BackColor = Color.Yellow;
                DateTime thisDay = DateTime.UtcNow;
                string txtFileName = thisDay.ToString("dd.MM.yyyy_HH.mm");
                BVHFile = new writeBVH(txtFileName);
                BVHFile.setTextFeld(textFelder1);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            windowClosing = true;
            StopKinect(sensor);
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopKinect(sensor);
        }

        //TODO: evtl folgende FUnktionen auslagern
        private void DrawBonesAndJoints(Skeleton skeleton, Graphics graphicBox)
        {
            /// Brush used to draw skeleton center point
            Brush centerPointBrush = Brushes.Blue;

            /// Brush used for drawing joints that are currently tracked
            Pen trackedJointPen = new Pen(Color.GreenYellow);

            /// Brush used for drawing joints that are currently inferred      
            Pen inferredJointPen = new Pen(Color.Yellow);

            // gefundene Punkte als Kreise malen
            foreach (Joint joint in skeleton.Joints)
            {
                Pen drawPen = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawPen = trackedJointPen;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawPen = inferredJointPen;
                }

                if (drawPen != null)
                {
                    graphicBox.DrawEllipse(drawPen, new Rectangle(this.SkeletonPointToScreen(joint.Position), new Size(10, 10)));
                }
            }

            //Verbindungen zwischen Punkten malen
            // Render Torso
            this.DrawBone(skeleton, graphicBox, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, graphicBox, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, graphicBox, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, graphicBox, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, graphicBox, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, graphicBox, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, graphicBox, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, graphicBox, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, graphicBox, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, graphicBox, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, graphicBox, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, graphicBox, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, graphicBox, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, graphicBox, JointType.AnkleRight, JointType.FootRight);

            //Kopf malen
            if (skeleton.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked)
            {
                graphicBox.DrawEllipse(new Pen(Color.GreenYellow), this.SkeletonPointToScreen(skeleton.Joints[JointType.Head].Position).X - 50,
                    this.SkeletonPointToScreen(skeleton.Joints[JointType.Head].Position).Y - 50, 100, 100);
            }


            return;
        }

        private void DrawBone(Skeleton skeleton, Graphics graphicBox, JointType jointType0, JointType jointType1)
        {
            /// Pen used for drawing bones that are currently tracked
            Pen trackedBonePen = new Pen(Brushes.Green, 6);

            /// Pen used for drawing bones that are currently inferred      
            Pen inferredBonePen = new Pen(Brushes.Gray, 1);

            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = trackedBonePen;
            }


            Point startPixel = SkeletonPointToScreen(joint0.Position);
            Point endPixel = SkeletonPointToScreen(joint1.Position);
            double distanceBtw2Joints = Math.Round(calcDistanceBtw2Points(joint0.Position, joint1.Position) * 100) / 100;

            //Linie zwischen 2 Joints wird gezeichnet
            graphicBox.DrawLine(drawPen, startPixel, endPixel);

            //Länge des Bones wird daneben geschrieben
            int textPosPixelX = Convert.ToInt32(Math.Abs(Math.Round(0.5 * (startPixel.X + endPixel.X))));
            int textPosPixelY = Convert.ToInt32(Math.Abs(Math.Round(0.5 * (startPixel.Y + endPixel.Y))));
            PointF textPos = new PointF(textPosPixelX, textPosPixelY);


            //graphicBox.DrawString(distanceBtw2Joints.ToString(), new Font("Arial", 20), new SolidBrush(Color.White), textPos);

            return;
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private double calcDistanceBtw2Points(SkeletonPoint Joint1, SkeletonPoint Joint2)
        {
            double distanceBtwJoints = Math.Sqrt(Math.Pow(Joint1.X - Joint2.X, 2) + Math.Pow(Joint1.Y - Joint2.Y, 2) + Math.Pow(Joint1.Z - Joint2.Z, 2));
            return distanceBtwJoints;
        }

        Bitmap ArrayToBitmap(byte[] array, int width, int height, PixelFormat pixelFormat)
        {
            Bitmap bitmapFrame = new Bitmap(width, height, pixelFormat);

            BitmapData bitmapData = bitmapFrame.LockBits(new Rectangle(0, 0,
            width, height), ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

            IntPtr intPointer = bitmapData.Scan0;
            Marshal.Copy(array, 0, intPointer, array.Length);

            bitmapFrame.UnlockBits(bitmapData);
            return bitmapFrame;
        }

        private void button_recStop_Click(object sender, EventArgs e)
        {
            if (BVHFile != null)
            {
                BVHFile.closeBVHFile();
                this.textBox_sensorStatus.Text = "Aufnahme gespeichert";
                this.textBox_sensorStatus.BackColor = Color.White;
                BVHFile = null;
            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();
                }
            }
        }
    }
}
