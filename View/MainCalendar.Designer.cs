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
            ((System.ComponentModel.ISupportInitialize)dgvAppointment).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            monthCalendar1.ForeColor = Color.MidnightBlue;
            monthCalendar1.Location = new Point(62, 186);
            monthCalendar1.Margin = new Padding(7, 7, 7, 7);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            monthCalendar1.TodayDate = new DateTime(2026, 5, 4, 0, 0, 0, 0);
            // 
            // dgvAppointment
            // 
            dgvAppointment.BackgroundColor = SystemColors.ControlLightLight;
            dgvAppointment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppointment.GridColor = SystemColors.HotTrack;
            dgvAppointment.Location = new Point(574, 186);
            dgvAppointment.Margin = new Padding(2, 2, 2, 2);
            dgvAppointment.Name = "dgvAppointment";
            dgvAppointment.ReadOnly = true;
            dgvAppointment.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAppointment.RowHeadersWidth = 82;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Padding = new Padding(4, 0, 0, 0);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAppointment.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvAppointment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointment.Size = new Size(455, 309);
            dgvAppointment.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.CornflowerBlue;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
            btnAdd.ForeColor = SystemColors.ControlLightLight;
            btnAdd.Location = new Point(364, 186);
            btnAdd.Margin = new Padding(2, 2, 2, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 45);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.CornflowerBlue;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
            btnUpdate.ForeColor = SystemColors.ControlLightLight;
            btnUpdate.Location = new Point(364, 288);
            btnUpdate.Margin = new Padding(2, 2, 2, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 45);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Tomato;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
            btnDelete.ForeColor = SystemColors.ControlLightLight;
            btnDelete.Location = new Point(364, 394);
            btnDelete.Margin = new Padding(2, 2, 2, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 45);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(62, 470);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(182, 25);
            label1.TabIndex = 5;
            label1.Text = "Choosing Date: None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ControlLightLight;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(574, 139);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(228, 28);
            label2.TabIndex = 6;
            label2.Text = "Danh sách các buổi hẹn";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = SystemColors.ControlLightLight;
            checkBox1.FlatAppearance.BorderColor = Color.Navy;
            checkBox1.FlatAppearance.BorderSize = 3;
            checkBox1.FlatAppearance.CheckedBackColor = Color.LightSkyBlue;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = Color.RoyalBlue;
            checkBox1.Location = new Point(574, 521);
            checkBox1.Margin = new Padding(2, 2, 2, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(242, 25);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Hiển thị tất cả cuộc hẹn của tôi";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Elephant", 19.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(364, 38);
            label3.Name = "label3";
            label3.Size = new Size(332, 51);
            label3.TabIndex = 8;
            label3.Text = "Main Calender";
            // 
            // MainCalendar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1098, 592);
            Controls.Add(label3);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvAppointment);
            Controls.Add(monthCalendar1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "MainCalendar";
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
    }
}