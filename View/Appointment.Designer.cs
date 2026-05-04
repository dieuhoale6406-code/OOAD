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
            label1.Font = new Font("Elephant", 19.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(102, 40);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(495, 51);
            label1.TabIndex = 0;
            label1.Text = "Calendar Appointment";
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.ForeColor = SystemColors.MenuHighlight;
            txtName.Location = new Point(212, 125);
            txtName.Margin = new Padding(2, 2, 2, 2);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Nhập tên cuộc họp";
            txtName.Size = new Size(379, 29);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(102, 125);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 25);
            label2.TabIndex = 2;
            label2.Text = "Name:";
            // 
            // lable3
            // 
            lable3.AutoSize = true;
            lable3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lable3.ForeColor = Color.RoyalBlue;
            lable3.Location = new Point(102, 175);
            lable3.Margin = new Padding(2, 0, 2, 0);
            lable3.Name = "lable3";
            lable3.Size = new Size(87, 25);
            lable3.TabIndex = 4;
            lable3.Text = "Location:";
            // 
            // txtLocation
            // 
            txtLocation.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLocation.ForeColor = SystemColors.MenuHighlight;
            txtLocation.Location = new Point(212, 175);
            txtLocation.Margin = new Padding(2, 2, 2, 2);
            txtLocation.Name = "txtLocation";
            txtLocation.PlaceholderText = "Nhập địa điểm họp";
            txtLocation.Size = new Size(379, 29);
            txtLocation.TabIndex = 3;
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(212, 232);
            dtpStart.Margin = new Padding(2, 2, 2, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(379, 31);
            dtpStart.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label4.ForeColor = Color.RoyalBlue;
            label4.Location = new Point(102, 232);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(102, 25);
            label4.TabIndex = 6;
            label4.Text = "Start Time:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label5.ForeColor = Color.RoyalBlue;
            label5.Location = new Point(102, 286);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(93, 25);
            label5.TabIndex = 8;
            label5.Text = "End Time:";
            // 
            // dtpEnd
            // 
            dtpEnd.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(212, 286);
            dtpEnd.Margin = new Padding(2, 2, 2, 2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(379, 31);
            dtpEnd.TabIndex = 7;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.CornflowerBlue;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(102, 697);
            btnOK.Margin = new Padding(2, 2, 2, 2);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(115, 36);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.FlatAppearance.BorderColor = Color.CornflowerBlue;
            btnCancel.FlatAppearance.BorderSize = 2;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.RoyalBlue;
            btnCancel.Location = new Point(291, 697);
            btnCancel.Margin = new Padding(2, 2, 2, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 36);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.BorderStyle = BorderStyle.FixedSingle;
            listView1.ForeColor = Color.DarkBlue;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(102, 495);
            listView1.Margin = new Padding(2, 2, 2, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(489, 179);
            listView1.TabIndex = 12;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(102, 444);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 25);
            label3.TabIndex = 13;
            label3.Text = "Reminder";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.RoyalBlue;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Trước 15 phút", "Trước 30 phút", "Trước 1 tiếng", "Trước 2 tiếng", "Trước 1 ngày", "Trước 2 ngày", "Trước 1 tuần", "Trước 2 tuần", "Khác" });
            comboBox1.Location = new Point(212, 444);
            comboBox1.Margin = new Padding(2, 2, 2, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(256, 29);
            comboBox1.TabIndex = 14;
            comboBox1.Text = "Chọn thời gian";
            // 
            // btnAddReminder
            // 
            btnAddReminder.BackColor = Color.CornflowerBlue;
            btnAddReminder.FlatAppearance.BorderSize = 0;
            btnAddReminder.FlatStyle = FlatStyle.Flat;
            btnAddReminder.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddReminder.ForeColor = SystemColors.ControlLightLight;
            btnAddReminder.Location = new Point(476, 442);
            btnAddReminder.Margin = new Padding(2, 2, 2, 2);
            btnAddReminder.Name = "btnAddReminder";
            btnAddReminder.Size = new Size(115, 36);
            btnAddReminder.TabIndex = 15;
            btnAddReminder.Text = "Add";
            btnAddReminder.UseVisualStyleBackColor = false;
            // 
            // btnDeleteReminder
            // 
            btnDeleteReminder.BackColor = Color.Tomato;
            btnDeleteReminder.FlatAppearance.BorderSize = 0;
            btnDeleteReminder.FlatStyle = FlatStyle.Flat;
            btnDeleteReminder.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnDeleteReminder.ForeColor = Color.White;
            btnDeleteReminder.Location = new Point(476, 697);
            btnDeleteReminder.Margin = new Padding(2, 2, 2, 2);
            btnDeleteReminder.Name = "btnDeleteReminder";
            btnDeleteReminder.Size = new Size(115, 36);
            btnDeleteReminder.TabIndex = 16;
            btnDeleteReminder.Text = "Delete";
            btnDeleteReminder.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rBtnGroupMeeting);
            groupBox1.Controls.Add(rBtnAppointment);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(102, 340);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 2, 2, 2);
            groupBox1.Size = new Size(489, 83);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Loại lịch hẹn";
            // 
            // rBtnGroupMeeting
            // 
            rBtnGroupMeeting.AutoSize = true;
            rBtnGroupMeeting.Location = new Point(293, 37);
            rBtnGroupMeeting.Margin = new Padding(2, 2, 2, 2);
            rBtnGroupMeeting.Name = "rBtnGroupMeeting";
            rBtnGroupMeeting.Size = new Size(88, 29);
            rBtnGroupMeeting.TabIndex = 1;
            rBtnGroupMeeting.TabStop = true;
            rBtnGroupMeeting.Text = "Nhóm";
            rBtnGroupMeeting.UseVisualStyleBackColor = true;
            // 
            // rBtnAppointment
            // 
            rBtnAppointment.AutoSize = true;
            rBtnAppointment.Location = new Point(89, 37);
            rBtnAppointment.Margin = new Padding(2, 2, 2, 2);
            rBtnAppointment.Name = "rBtnAppointment";
            rBtnAppointment.Size = new Size(103, 29);
            rBtnAppointment.TabIndex = 0;
            rBtnAppointment.TabStop = true;
            rBtnAppointment.Text = "Cá nhân";
            rBtnAppointment.UseVisualStyleBackColor = true;
            // 
            // Appointment
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(706, 767);
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
            Margin = new Padding(2, 2, 2, 2);
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