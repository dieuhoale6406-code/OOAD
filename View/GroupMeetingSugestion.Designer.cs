namespace OOAD
{
    partial class GroupMeetingSugestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be 
        /// 
        /// d; otherwise, false.</param>
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
            btnJoin = new Button();
            btnNoThanks = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(37, 99, 235);
            label1.Location = new Point(230, 27);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(137, 32);
            label1.TabIndex = 0;
            label1.Text = "Thông báo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(31, 41, 55);
            label2.Location = new Point(61, 87);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(476, 28);
            label2.TabIndex = 1;
            label2.Text = "Có cuộc họp nhóm tương tự, bạn có muốn tham gia?";
            // 
            // btnJoin
            // 
            btnJoin.BackColor = Color.FromArgb(37, 99, 235);
            btnJoin.FlatAppearance.BorderSize = 0;
            btnJoin.Cursor = Cursors.Hand;
            btnJoin.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnJoin.FlatStyle = FlatStyle.Flat;
            btnJoin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnJoin.ForeColor = Color.White;
            btnJoin.Location = new Point(127, 154);
            btnJoin.Margin = new Padding(2, 2, 2, 2);
            btnJoin.Name = "btnJoin";
            btnJoin.Size = new Size(115, 36);
            btnJoin.TabIndex = 2;
            btnJoin.Text = "Join";
            btnJoin.UseVisualStyleBackColor = false;
            // 
            // btnNoThanks
            // 
            btnNoThanks.BackColor = Color.White;
            btnNoThanks.Cursor = Cursors.Hand;
            btnNoThanks.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnNoThanks.FlatAppearance.BorderSize = 1;
            btnNoThanks.FlatStyle = FlatStyle.Flat;
            btnNoThanks.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNoThanks.ForeColor = Color.FromArgb(37, 99, 235);
            btnNoThanks.Location = new Point(359, 154);
            btnNoThanks.Margin = new Padding(2, 2, 2, 2);
            btnNoThanks.Name = "btnNoThanks";
            btnNoThanks.Size = new Size(115, 36);
            btnNoThanks.TabIndex = 3;
            btnNoThanks.Text = "No Thanks";
            btnNoThanks.UseVisualStyleBackColor = false;
            // 
            // GroupMeetingSugestion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(615, 238);
            Controls.Add(btnNoThanks);
            Controls.Add(btnJoin);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "GroupMeetingSugestion";
            Text = "GroupMeetingSugestion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnJoin;
        private Button btnNoThanks;
    }
}