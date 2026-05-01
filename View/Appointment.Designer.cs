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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(126, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(274, 32);
            label1.TabIndex = 0;
            label1.Text = "Calendar Appointment";
            // 
            // txtName
            // 
            txtName.Location = new Point(119, 80);
            txtName.Margin = new Padding(2, 2, 2, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(379, 31);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 80);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 2;
            label2.Text = "Name:";
            // 
            // lable3
            // 
            lable3.AutoSize = true;
            lable3.Location = new Point(9, 130);
            lable3.Margin = new Padding(2, 0, 2, 0);
            lable3.Name = "lable3";
            lable3.Size = new Size(83, 25);
            lable3.TabIndex = 4;
            lable3.Text = "Location:";
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(119, 130);
            txtLocation.Margin = new Padding(2, 2, 2, 2);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(379, 31);
            txtLocation.TabIndex = 3;
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(119, 187);
            dtpStart.Margin = new Padding(2, 2, 2, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(379, 31);
            dtpStart.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 187);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(95, 25);
            label4.TabIndex = 6;
            label4.Text = "Start Time:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 241);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(89, 25);
            label5.TabIndex = 8;
            label5.Text = "End Time:";
            // 
            // dtpEnd
            // 
            dtpEnd.CustomFormat = "hh:mm:ss dd/mm/yy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(119, 241);
            dtpEnd.Margin = new Padding(2, 2, 2, 2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(379, 31);
            dtpEnd.TabIndex = 7;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(112, 580);
            btnOK.Margin = new Padding(2, 2, 2, 2);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(115, 36);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(281, 580);
            btnCancel.Margin = new Padding(2, 2, 2, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 36);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(22, 337);
            listView1.Margin = new Padding(2, 2, 2, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(476, 179);
            listView1.TabIndex = 12;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 298);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 13;
            label3.Text = "Reminder";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Trước 15 phút", "Trước 30 phút", "Trước 1 tiếng", "Trước 2 tiếng", "Trước 1 ngày", "Trước 2 ngày", "Trước 1 tuần", "Trước 2 tuần", "Khác" });
            comboBox1.Location = new Point(119, 298);
            comboBox1.Margin = new Padding(2, 2, 2, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(256, 33);
            comboBox1.TabIndex = 14;
            comboBox1.Text = "Chọn thời gian";
            // 
            // btnAddReminder
            // 
            btnAddReminder.Location = new Point(383, 295);
            btnAddReminder.Margin = new Padding(2, 2, 2, 2);
            btnAddReminder.Name = "btnAddReminder";
            btnAddReminder.Size = new Size(115, 36);
            btnAddReminder.TabIndex = 15;
            btnAddReminder.Text = "Add";
            btnAddReminder.UseVisualStyleBackColor = true;
            // 
            // btnDeleteReminder
            // 
            btnDeleteReminder.Location = new Point(382, 529);
            btnDeleteReminder.Margin = new Padding(2, 2, 2, 2);
            btnDeleteReminder.Name = "btnDeleteReminder";
            btnDeleteReminder.Size = new Size(115, 36);
            btnDeleteReminder.TabIndex = 16;
            btnDeleteReminder.Text = "Delete";
            btnDeleteReminder.UseVisualStyleBackColor = true;
            // 
            // Appointment
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 635);
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
            Text = "AddAppointment";
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
    }
}