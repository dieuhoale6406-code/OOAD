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
            monthCalendar1 = new MonthCalendar();
            dgvAppointment = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dgvAppointment).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(39, 45);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            monthCalendar1.TodayDate = new DateTime(2026, 5, 4, 0, 0, 0, 0);
            // 
            // dgvAppointment
            // 
            dgvAppointment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppointment.Location = new Point(510, 83);
            dgvAppointment.Name = "dgvAppointment";
            dgvAppointment.ReadOnly = true;
            dgvAppointment.RowHeadersWidth = 82;
            dgvAppointment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointment.Size = new Size(836, 278);
            dgvAppointment.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(510, 421);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(156, 58);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(877, 421);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(156, 58);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1190, 421);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(156, 58);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 397);
            label1.Name = "label1";
            label1.Size = new Size(243, 32);
            label1.TabIndex = 5;
            label1.Text = "Choosing Date: None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(510, 32);
            label2.Name = "label2";
            label2.Size = new Size(269, 32);
            label2.TabIndex = 6;
            label2.Text = "Danh sách các buổi hẹn";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(510, 367);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(380, 36);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Hiển thị tất cả cuộc hẹn của tôi";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // MainCalendar
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1427, 577);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvAppointment);
            Controls.Add(monthCalendar1);
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
    }
}