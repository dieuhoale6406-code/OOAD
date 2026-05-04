namespace OOAD
{
    partial class ConflictResolution
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
            label2 = new Label();
            labelConflict = new Label();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            btnConfirm = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(188, 9);
            label1.Name = "label1";
            label1.Size = new Size(368, 45);
            label1.TabIndex = 0;
            label1.Text = "⚠️ Cảnh báo trùng lịch";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(338, 87);
            label2.Name = "label2";
            label2.Size = new Size(0, 32);
            label2.TabIndex = 1;
            // 
            // labelConflict
            // 
            labelConflict.AutoSize = true;
            labelConflict.Location = new Point(188, 64);
            labelConflict.Name = "labelConflict";
            labelConflict.Size = new Size(392, 32);
            labelConflict.TabIndex = 2;
            labelConflict.Text = "Cuộc họp xung đột với các lịch sau:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(776, 151);
            dataGridView1.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(316, 253);
            label3.Name = "label3";
            label3.Size = new Size(135, 32);
            label3.TabIndex = 4;
            label3.Text = "Bạn muốn?";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(56, 288);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(701, 36);
            radioButton2.TabIndex = 5;
            radioButton2.TabStop = true;
            radioButton2.Text = "Thay thế lịch cũ (xóa tất cả các lịch xung đột với lịch đã chọn)";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(56, 330);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(199, 36);
            radioButton3.TabIndex = 6;
            radioButton3.TabStop = true;
            radioButton3.Text = "Chọn giờ khác";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(130, 392);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(150, 46);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(462, 392);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 46);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ConflictResolution
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 469);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(labelConflict);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ConflictResolution";
            Text = "ConflictResolution";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label labelConflict;
        private DataGridView dataGridView1;
        private Label label3;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Button btnConfirm;
        private Button btnCancel;
    }
}