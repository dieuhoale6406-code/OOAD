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
            btnJoin = new Button();
            btnNoThanks = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(275, 32);
            label1.Name = "label1";
            label1.Size = new Size(183, 45);
            label1.TabIndex = 0;
            label1.Text = "Thông báo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(98, 109);
            label2.Name = "label2";
            label2.Size = new Size(585, 32);
            label2.TabIndex = 1;
            label2.Text = "Có cuộc họp nhóm tương tự, bạn có muốn tham gia?";
            // 
            // btnJoin
            // 
            btnJoin.Location = new Point(165, 197);
            btnJoin.Name = "btnJoin";
            btnJoin.Size = new Size(150, 46);
            btnJoin.TabIndex = 2;
            btnJoin.Text = "Join";
            btnJoin.UseVisualStyleBackColor = true;
            // 
            // btnNoThanks
            // 
            btnNoThanks.Location = new Point(467, 197);
            btnNoThanks.Name = "btnNoThanks";
            btnNoThanks.Size = new Size(150, 46);
            btnNoThanks.TabIndex = 3;
            btnNoThanks.Text = "No Thanks";
            btnNoThanks.UseVisualStyleBackColor = true;
            // 
            // GroupMeetingSugestion
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 304);
            Controls.Add(btnNoThanks);
            Controls.Add(btnJoin);
            Controls.Add(label2);
            Controls.Add(label1);
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