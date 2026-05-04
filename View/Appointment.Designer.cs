namespace OOAD
{
    partial class Appointment
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
            label1 = new Label();
            txtName = new TextBox();
            label2 = new Label();
            lable3 = new Label();
            txtLocation = new TextBox();
            dtpStart = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            dtpEnd = new DateTimePicker();
            btnOK = new Button();
            btnCancel = new Button();
            listView1 = new ListView();
            label3 = new Label();
            comboBox1 = new ComboBox();
            btnAddReminder = new Button();
            btnDeleteReminder = new Button();
            groupBox1 = new GroupBox();
            rBtnGroupMeeting = new RadioButton();
            rBtnAppointment = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(164, 24);
            label1.Name = "label1";
            label1.Size = new Size(361, 45);
            label1.TabIndex = 0;
            label1.Text = "Calendar Appointment";
            // 
            // txtName
            // 
            txtName.Location = new Point(155, 102);
            txtName.Name = "txtName";
            txtName.Size = new Size(491, 39);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 102);
            label2.Name = "label2";
            label2.Size = new Size(83, 32);
            label2.TabIndex = 2;
            label2.Text = "Name:";
            // 
            // lable3
            // 
            lable3.AutoSize = true;
            lable3.Location = new Point(12, 166);
            lable3.Name = "lable3";
            lable3.Size = new Size(109, 32);
            lable3.TabIndex = 4;
            lable3.Text = "Location:";
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(155, 166);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(491, 39);
            txtLocation.TabIndex = 3;
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(155, 239);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(491, 39);
            dtpStart.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 239);
            label4.Name = "label4";
            label4.Size = new Size(127, 32);
            label4.TabIndex = 6;
            label4.Text = "Start Time:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 308);
            label5.Name = "label5";
            label5.Size = new Size(119, 32);
            label5.TabIndex = 8;
            label5.Text = "End Time:";
            // 
            // dtpEnd
            // 
            dtpEnd.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(155, 308);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(491, 39);
            dtpEnd.TabIndex = 7;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(146, 881);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(150, 46);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(365, 881);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 46);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(29, 570);
            listView1.Name = "listView1";
            listView1.Size = new Size(618, 228);
            listView1.TabIndex = 12;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 520);
            label3.Name = "label3";
            label3.Size = new Size(116, 32);
            label3.TabIndex = 13;
            label3.Text = "Reminder";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Trước 15 phút", "Trước 30 phút", "Trước 1 tiếng", "Trước 2 tiếng", "Trước 1 ngày", "Trước 2 ngày", "Trước 1 tuần", "Trước 2 tuần", "Khác" });
            comboBox1.Location = new Point(155, 520);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(332, 40);
            comboBox1.TabIndex = 14;
            comboBox1.Text = "Chọn thời gian";
            // 
            // btnAddReminder
            // 
            btnAddReminder.Location = new Point(498, 517);
            btnAddReminder.Name = "btnAddReminder";
            btnAddReminder.Size = new Size(150, 46);
            btnAddReminder.TabIndex = 15;
            btnAddReminder.Text = "Add";
            btnAddReminder.UseVisualStyleBackColor = true;
            // 
            // btnDeleteReminder
            // 
            btnDeleteReminder.Location = new Point(497, 816);
            btnDeleteReminder.Name = "btnDeleteReminder";
            btnDeleteReminder.Size = new Size(150, 46);
            btnDeleteReminder.TabIndex = 16;
            btnDeleteReminder.Text = "Delete";
            btnDeleteReminder.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rBtnGroupMeeting);
            groupBox1.Controls.Add(rBtnAppointment);
            groupBox1.Location = new Point(45, 377);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(594, 121);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Loại lịch hẹn";
            // 
            // rBtnGroupMeeting
            // 
            rBtnGroupMeeting.AutoSize = true;
            rBtnGroupMeeting.Location = new Point(366, 53);
            rBtnGroupMeeting.Name = "rBtnGroupMeeting";
            rBtnGroupMeeting.Size = new Size(112, 36);
            rBtnGroupMeeting.TabIndex = 1;
            rBtnGroupMeeting.TabStop = true;
            rBtnGroupMeeting.Text = "Nhóm";
            rBtnGroupMeeting.UseVisualStyleBackColor = true;
            // 
            // rBtnAppointment
            // 
            rBtnAppointment.AutoSize = true;
            rBtnAppointment.Location = new Point(101, 53);
            rBtnAppointment.Name = "rBtnAppointment";
            rBtnAppointment.Size = new Size(133, 36);
            rBtnAppointment.TabIndex = 0;
            rBtnAppointment.TabStop = true;
            rBtnAppointment.Text = "Cá nhân";
            rBtnAppointment.UseVisualStyleBackColor = true;
            // 
            // Appointment
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 947);
            Controls.Add(groupBox1);
            Controls.Add(btnDeleteReminder);
            Controls.Add(btnAddReminder);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(listView1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(label5);
            Controls.Add(dtpEnd);
            Controls.Add(label4);
            Controls.Add(dtpStart);
            Controls.Add(lable3);
            Controls.Add(txtLocation);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            Name = "Appointment";
            Text = "Appointment";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private Label label2;
        private Label lable3;
        private TextBox txtLocation;
        private DateTimePicker dtpStart;
        private Label label4;
        private Label label5;
        private DateTimePicker dtpEnd;
        private Button btnOK;
        private Button btnCancel;
        private ListView listView1;
        private Label label3;
        private ComboBox comboBox1;
        private Button btnAddReminder;
        private Button btnDeleteReminder;
        private GroupBox groupBox1;
        private RadioButton rBtnGroupMeeting;
        private RadioButton rBtnAppointment;
    }
}