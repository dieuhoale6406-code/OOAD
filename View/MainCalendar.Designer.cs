namespace OOAD
{
    partial class MainCalendar
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            monthCalendar1 = new MonthCalendar();
            dgvAppointment = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            label3 = new Label();
            lblGreeting = new Label();
            btnLogout = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAppointment).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            monthCalendar1.ForeColor = Color.FromArgb(31, 41, 55);
            monthCalendar1.Location = new Point(81, 238);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            monthCalendar1.TodayDate = new DateTime(0L);
            // 
            // dgvAppointment
            // 
            dgvAppointment.AllowUserToAddRows = false;
            dgvAppointment.AllowUserToDeleteRows = false;
            dgvAppointment.BackgroundColor = Color.White;
            dgvAppointment.BorderStyle = BorderStyle.None;
            dgvAppointment.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAppointment.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(37, 99, 235);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(6, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(37, 99, 235);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAppointment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAppointment.ColumnHeadersHeight = 36;
            dgvAppointment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAppointment.EnableHeadersVisualStyles = false;
            dgvAppointment.GridColor = Color.FromArgb(225, 230, 242);
            dgvAppointment.Location = new Point(746, 238);
            dgvAppointment.MultiSelect = false;
            dgvAppointment.Name = "dgvAppointment";
            dgvAppointment.ReadOnly = true;
            dgvAppointment.RowHeadersVisible = false;
            dgvAppointment.RowHeadersWidth = 82;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAppointment.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvAppointment.RowTemplate.Height = 34;
            dgvAppointment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointment.Size = new Size(592, 396);
            dgvAppointment.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(37, 99, 235);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(473, 238);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(156, 58);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(473, 369);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(156, 58);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(473, 504);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(156, 58);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(75, 85, 99);
            label1.Location = new Point(81, 602);
            label1.Name = "label1";
            label1.Size = new Size(243, 32);
            label1.TabIndex = 5;
            label1.Text = "Choosing Date: None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(31, 41, 55);
            label2.Location = new Point(746, 178);
            label2.Name = "label2";
            label2.Size = new Size(306, 37);
            label2.TabIndex = 6;
            label2.Text = "Danh sách các buổi hẹn";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = Color.Transparent;
            checkBox1.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            checkBox1.FlatAppearance.BorderSize = 2;
            checkBox1.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = Color.FromArgb(75, 85, 99);
            checkBox1.Location = new Point(746, 667);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(336, 34);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Hiển thị tất cả cuộc hẹn của tôi";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Elephant", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(37, 99, 235);
            label3.Location = new Point(81, 44);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(655, 103);
            label3.TabIndex = 8;
            label3.Text = "Main Calendar";
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGreeting.ForeColor = Color.FromArgb(31, 41, 55);
            lblGreeting.Location = new Point(1011, 12);
            lblGreeting.Margin = new Padding(4, 0, 4, 0);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(414, 37);
            lblGreeting.TabIndex = 9;
            lblGreeting.Text = "Xin chào Nguyễn Thị Cẩm Tuyền";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(239, 68, 68);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1147, 65);
            btnLogout.Margin = new Padding(4, 4, 4, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(143, 49);
            btnLogout.TabIndex = 10;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // MainCalendar
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1427, 758);
            Controls.Add(btnLogout);
            Controls.Add(lblGreeting);
            Controls.Add(label3);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvAppointment);
            Controls.Add(monthCalendar1);
            Name = "MainCalendar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainCalendar";
            ((System.ComponentModel.ISupportInitialize)dgvAppointment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private DataGridView dgvAppointment;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Label label1;
        private Label label2;
        private CheckBox checkBox1;
        private Label label3;
        private Label lblGreeting;
        private Button btnLogout;
    }
}
