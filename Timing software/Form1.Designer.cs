namespace Timing_software
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventDatePicker = new System.Windows.Forms.DateTimePicker();
            this.participantsgrid = new System.Windows.Forms.DataGridView();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.clearparticipants = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataTableTags = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.startingrfid = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFilterGender = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMaxAge = new System.Windows.Forms.TextBox();
            this.textBoxMinAge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.filterclear = new System.Windows.Forms.Button();
            this.publish = new System.Windows.Forms.Button();
            this.eventname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newtagcheck = new System.Windows.Forms.Button();
            this.autoupdate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.participantsgrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingrfid)).BeginInit();
            this.SuspendLayout();
            // 
            // eventDatePicker
            // 
            this.eventDatePicker.Location = new System.Drawing.Point(12, 12);
            this.eventDatePicker.Name = "eventDatePicker";
            this.eventDatePicker.Size = new System.Drawing.Size(200, 20);
            this.eventDatePicker.TabIndex = 0;
            // 
            // participantsgrid
            // 
            this.participantsgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.participantsgrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.participantsgrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.participantsgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.participantsgrid.Location = new System.Drawing.Point(253, 12);
            this.participantsgrid.Name = "participantsgrid";
            this.participantsgrid.Size = new System.Drawing.Size(602, 130);
            this.participantsgrid.TabIndex = 1;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(253, 164);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadData.TabIndex = 2;
            this.btnLoadData.Text = "Import";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // clearparticipants
            // 
            this.clearparticipants.Location = new System.Drawing.Point(334, 164);
            this.clearparticipants.Name = "clearparticipants";
            this.clearparticipants.Size = new System.Drawing.Size(75, 23);
            this.clearparticipants.TabIndex = 3;
            this.clearparticipants.Text = "Clear";
            this.clearparticipants.UseVisualStyleBackColor = true;
            this.clearparticipants.Click += new System.EventHandler(this.clearparticipants_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataTableTags
            // 
            this.dataTableTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataTableTags.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataTableTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTableTags.Location = new System.Drawing.Point(253, 207);
            this.dataTableTags.Name = "dataTableTags";
            this.dataTableTags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataTableTags.Size = new System.Drawing.Size(602, 150);
            this.dataTableTags.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(253, 363);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Load Results";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(360, 363);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "HTML Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // startingrfid
            // 
            this.startingrfid.Location = new System.Drawing.Point(12, 84);
            this.startingrfid.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.startingrfid.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.startingrfid.Name = "startingrfid";
            this.startingrfid.Size = new System.Drawing.Size(120, 20);
            this.startingrfid.TabIndex = 8;
            this.startingrfid.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "RFID Starting Number";
            // 
            // comboBoxFilterGender
            // 
            this.comboBoxFilterGender.FormattingEnabled = true;
            this.comboBoxFilterGender.Items.AddRange(new object[] {
            "All",
            "Male",
            "Female"});
            this.comboBoxFilterGender.Location = new System.Drawing.Point(11, 232);
            this.comboBoxFilterGender.Name = "comboBoxFilterGender";
            this.comboBoxFilterGender.Size = new System.Drawing.Size(120, 21);
            this.comboBoxFilterGender.TabIndex = 11;
            this.comboBoxFilterGender.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterGender_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Sex Filter";
            // 
            // textBoxMaxAge
            // 
            this.textBoxMaxAge.Location = new System.Drawing.Point(67, 291);
            this.textBoxMaxAge.Name = "textBoxMaxAge";
            this.textBoxMaxAge.Size = new System.Drawing.Size(41, 20);
            this.textBoxMaxAge.TabIndex = 13;
            this.textBoxMaxAge.TextChanged += new System.EventHandler(this.textBoxMaxAge_TextChanged);
            // 
            // textBoxMinAge
            // 
            this.textBoxMinAge.Location = new System.Drawing.Point(67, 262);
            this.textBoxMinAge.Name = "textBoxMinAge";
            this.textBoxMinAge.Size = new System.Drawing.Size(41, 20);
            this.textBoxMinAge.TabIndex = 12;
            this.textBoxMinAge.TextChanged += new System.EventHandler(this.textBoxMinAge_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Max Age";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Min Age";
            // 
            // filterclear
            // 
            this.filterclear.Location = new System.Drawing.Point(15, 317);
            this.filterclear.Name = "filterclear";
            this.filterclear.Size = new System.Drawing.Size(75, 23);
            this.filterclear.TabIndex = 14;
            this.filterclear.Text = "Clear Filters";
            this.filterclear.UseVisualStyleBackColor = true;
            this.filterclear.Click += new System.EventHandler(this.filterclear_Click);
            // 
            // publish
            // 
            this.publish.Location = new System.Drawing.Point(780, 363);
            this.publish.Name = "publish";
            this.publish.Size = new System.Drawing.Size(75, 23);
            this.publish.TabIndex = 17;
            this.publish.Text = "Publish";
            this.publish.UseVisualStyleBackColor = true;
            this.publish.Click += new System.EventHandler(this.publish_Click);
            // 
            // eventname
            // 
            this.eventname.Location = new System.Drawing.Point(84, 38);
            this.eventname.Name = "eventname";
            this.eventname.Size = new System.Drawing.Size(128, 20);
            this.eventname.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Event Name";
            // 
            // newtagcheck
            // 
            this.newtagcheck.Location = new System.Drawing.Point(15, 415);
            this.newtagcheck.Name = "newtagcheck";
            this.newtagcheck.Size = new System.Drawing.Size(108, 23);
            this.newtagcheck.TabIndex = 20;
            this.newtagcheck.Text = "Check New Tags";
            this.newtagcheck.UseVisualStyleBackColor = true;
            this.newtagcheck.Click += new System.EventHandler(this.newtagcheck_Click);
            // 
            // autoupdate
            // 
            this.autoupdate.AutoSize = true;
            this.autoupdate.Location = new System.Drawing.Point(253, 392);
            this.autoupdate.Name = "autoupdate";
            this.autoupdate.Size = new System.Drawing.Size(86, 17);
            this.autoupdate.TabIndex = 21;
            this.autoupdate.Text = "Auto Update";
            this.autoupdate.UseVisualStyleBackColor = true;
            this.autoupdate.CheckedChanged += new System.EventHandler(this.autoupdate_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 450);
            this.Controls.Add(this.autoupdate);
            this.Controls.Add(this.newtagcheck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eventname);
            this.Controls.Add(this.publish);
            this.Controls.Add(this.filterclear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMinAge);
            this.Controls.Add(this.textBoxMaxAge);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFilterGender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startingrfid);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataTableTags);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.clearparticipants);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.participantsgrid);
            this.Controls.Add(this.eventDatePicker);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.participantsgrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingrfid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker eventDatePicker;
        private System.Windows.Forms.DataGridView participantsgrid;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button clearparticipants;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView dataTableTags;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown startingrfid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFilterGender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMaxAge;
        private System.Windows.Forms.TextBox textBoxMinAge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button filterclear;
        private System.Windows.Forms.Button publish;
        private System.Windows.Forms.TextBox eventname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button newtagcheck;
        private System.Windows.Forms.CheckBox autoupdate;
    }
}

