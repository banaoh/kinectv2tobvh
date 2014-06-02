namespace Kinect2BVH
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.textBox_sensorStatus = new System.Windows.Forms.TextBox();
            this.dropDown_fps = new System.Windows.Forms.ComboBox();
            this.txtLabel_fps = new System.Windows.Forms.Label();
            this.pictureBox_skeleton = new System.Windows.Forms.PictureBox();
            this.button_rec = new System.Windows.Forms.Button();
            this.button_recStop = new System.Windows.Forms.Button();
            this.checkBox_colorCam = new System.Windows.Forms.CheckBox();
            this.groupBox_smooth = new System.Windows.Forms.GroupBox();
            this.radioButton_smoothIntense = new System.Windows.Forms.RadioButton();
            this.radioButton_smoothModerate = new System.Windows.Forms.RadioButton();
            this.radioButton_smoothDefault = new System.Windows.Forms.RadioButton();
            this.textFelder1 = new Kinect2BVH.TextFelder();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_init = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skeleton)).BeginInit();
            this.groupBox_smooth.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(721, 264);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(721, 297);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 3;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // textBox_sensorStatus
            // 
            this.textBox_sensorStatus.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_sensorStatus.Location = new System.Drawing.Point(698, 398);
            this.textBox_sensorStatus.Name = "textBox_sensorStatus";
            this.textBox_sensorStatus.Size = new System.Drawing.Size(134, 20);
            this.textBox_sensorStatus.TabIndex = 4;
            this.textBox_sensorStatus.Text = "Status";
            this.textBox_sensorStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dropDown_fps
            // 
            this.dropDown_fps.Cursor = System.Windows.Forms.Cursors.Default;
            this.dropDown_fps.FormattingEnabled = true;
            this.dropDown_fps.Items.AddRange(new object[] {
            "30",
            "15",
            "10",
            "5",
            "1"});
            this.dropDown_fps.Location = new System.Drawing.Point(748, 50);
            this.dropDown_fps.Name = "dropDown_fps";
            this.dropDown_fps.Size = new System.Drawing.Size(75, 21);
            this.dropDown_fps.TabIndex = 7;
            this.dropDown_fps.Text = "30";
            // 
            // txtLabel_fps
            // 
            this.txtLabel_fps.AutoSize = true;
            this.txtLabel_fps.Location = new System.Drawing.Point(718, 53);
            this.txtLabel_fps.Name = "txtLabel_fps";
            this.txtLabel_fps.Size = new System.Drawing.Size(24, 13);
            this.txtLabel_fps.TabIndex = 9;
            this.txtLabel_fps.Text = "fps:";
            // 
            // pictureBox_skeleton
            // 
            this.pictureBox_skeleton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_skeleton.Location = new System.Drawing.Point(51, 28);
            this.pictureBox_skeleton.Name = "pictureBox_skeleton";
            this.pictureBox_skeleton.Size = new System.Drawing.Size(641, 407);
            this.pictureBox_skeleton.TabIndex = 10;
            this.pictureBox_skeleton.TabStop = false;
            // 
            // button_rec
            // 
            this.button_rec.Location = new System.Drawing.Point(721, 340);
            this.button_rec.Name = "button_rec";
            this.button_rec.Size = new System.Drawing.Size(75, 23);
            this.button_rec.TabIndex = 12;
            this.button_rec.Text = "Record";
            this.button_rec.UseVisualStyleBackColor = true;
            this.button_rec.Click += new System.EventHandler(this.button_rec_Click);
            // 
            // button_recStop
            // 
            this.button_recStop.Location = new System.Drawing.Point(721, 369);
            this.button_recStop.Name = "button_recStop";
            this.button_recStop.Size = new System.Drawing.Size(75, 23);
            this.button_recStop.TabIndex = 13;
            this.button_recStop.Text = "Record stop";
            this.button_recStop.UseVisualStyleBackColor = true;
            this.button_recStop.Click += new System.EventHandler(this.button_recStop_Click);
            // 
            // checkBox_colorCam
            // 
            this.checkBox_colorCam.AutoSize = true;
            this.checkBox_colorCam.Location = new System.Drawing.Point(721, 120);
            this.checkBox_colorCam.Name = "checkBox_colorCam";
            this.checkBox_colorCam.Size = new System.Drawing.Size(89, 17);
            this.checkBox_colorCam.TabIndex = 27;
            this.checkBox_colorCam.Text = "Video Stream";
            this.checkBox_colorCam.UseVisualStyleBackColor = true;
            // 
            // groupBox_smooth
            // 
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothIntense);
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothModerate);
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothDefault);
            this.groupBox_smooth.Location = new System.Drawing.Point(712, 158);
            this.groupBox_smooth.Name = "groupBox_smooth";
            this.groupBox_smooth.Size = new System.Drawing.Size(111, 87);
            this.groupBox_smooth.TabIndex = 29;
            this.groupBox_smooth.TabStop = false;
            this.groupBox_smooth.Text = "Smooth";
            // 
            // radioButton_smoothIntense
            // 
            this.radioButton_smoothIntense.AutoSize = true;
            this.radioButton_smoothIntense.Location = new System.Drawing.Point(6, 65);
            this.radioButton_smoothIntense.Name = "radioButton_smoothIntense";
            this.radioButton_smoothIntense.Size = new System.Drawing.Size(60, 17);
            this.radioButton_smoothIntense.TabIndex = 2;
            this.radioButton_smoothIntense.Text = "Intense";
            this.radioButton_smoothIntense.UseVisualStyleBackColor = true;
            // 
            // radioButton_smoothModerate
            // 
            this.radioButton_smoothModerate.AutoSize = true;
            this.radioButton_smoothModerate.Location = new System.Drawing.Point(6, 42);
            this.radioButton_smoothModerate.Name = "radioButton_smoothModerate";
            this.radioButton_smoothModerate.Size = new System.Drawing.Size(70, 17);
            this.radioButton_smoothModerate.TabIndex = 1;
            this.radioButton_smoothModerate.Text = "Moderate";
            this.radioButton_smoothModerate.UseVisualStyleBackColor = true;
            // 
            // radioButton_smoothDefault
            // 
            this.radioButton_smoothDefault.AutoSize = true;
            this.radioButton_smoothDefault.Checked = true;
            this.radioButton_smoothDefault.Location = new System.Drawing.Point(6, 19);
            this.radioButton_smoothDefault.Name = "radioButton_smoothDefault";
            this.radioButton_smoothDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButton_smoothDefault.TabIndex = 0;
            this.radioButton_smoothDefault.TabStop = true;
            this.radioButton_smoothDefault.Text = "Default";
            this.radioButton_smoothDefault.UseVisualStyleBackColor = true;
            // 
            // textFelder1
            // 
            this.textFelder1.Location = new System.Drawing.Point(32, 438);
            this.textFelder1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.textFelder1.Name = "textFelder1";
            this.textFelder1.setTextBoxAngles = "";
            this.textFelder1.setTextBoxCapturedFrames = "";
            this.textFelder1.setTextBoxElapsedTime = "";
            this.textFelder1.setTextBoxFrameRate = "";
            this.textFelder1.setTextBoxLength = "";
            this.textFelder1.setTextPosition = "";
            this.textFelder1.Size = new System.Drawing.Size(745, 247);
            this.textFelder1.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(728, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Initialisierungen:";
            // 
            // textBox_init
            // 
            this.textBox_init.Location = new System.Drawing.Point(748, 90);
            this.textBox_init.Name = "textBox_init";
            this.textBox_init.Size = new System.Drawing.Size(50, 20);
            this.textBox_init.TabIndex = 31;
            this.textBox_init.Text = "100";
            this.textBox_init.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 655);
            this.Controls.Add(this.textBox_init);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_smooth);
            this.Controls.Add(this.pictureBox_skeleton);
            this.Controls.Add(this.textFelder1);
            this.Controls.Add(this.checkBox_colorCam);
            this.Controls.Add(this.button_recStop);
            this.Controls.Add(this.button_rec);
            this.Controls.Add(this.txtLabel_fps);
            this.Controls.Add(this.dropDown_fps);
            this.Controls.Add(this.textBox_sensorStatus);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Kinect Hauptfenster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skeleton)).EndInit();
            this.groupBox_smooth.ResumeLayout(false);
            this.groupBox_smooth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.TextBox textBox_sensorStatus;
        private System.Windows.Forms.ComboBox dropDown_fps;
        private System.Windows.Forms.Label txtLabel_fps;
        private System.Windows.Forms.PictureBox pictureBox_skeleton;
        private System.Windows.Forms.Button button_rec;
        private System.Windows.Forms.Button button_recStop;
        private System.Windows.Forms.CheckBox checkBox_colorCam;
        private TextFelder textFelder1;
        private System.Windows.Forms.GroupBox groupBox_smooth;
        private System.Windows.Forms.RadioButton radioButton_smoothIntense;
        private System.Windows.Forms.RadioButton radioButton_smoothModerate;
        private System.Windows.Forms.RadioButton radioButton_smoothDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_init;
    }
}

