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
            label1.Location = new Point(217, 105);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(137, 32);
            label1.TabIndex = 0;
            label1.Text = "Thông báo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(80, 165);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(439, 25);
            label2.TabIndex = 1;
            label2.Text = "Có cuộc họp nhóm tương tự, bạn có muốn tham gia?";
            // 
            // btnJoin
            // 
            btnJoin.Location = new Point(132, 234);
            btnJoin.Margin = new Padding(2, 2, 2, 2);
            btnJoin.Name = "btnJoin";
            btnJoin.Size = new Size(115, 36);
            btnJoin.TabIndex = 2;
            btnJoin.Text = "Join";
            btnJoin.UseVisualStyleBackColor = true;
            // 
            // btnNoThanks
            // 
            btnNoThanks.Location = new Point(364, 234);
            btnNoThanks.Margin = new Padding(2, 2, 2, 2);
            btnNoThanks.Name = "btnNoThanks";
            btnNoThanks.Size = new Size(115, 36);
            btnNoThanks.TabIndex = 3;
            btnNoThanks.Text = "No Thanks";
            btnNoThanks.UseVisualStyleBackColor = true;
            // 
            // GroupMeetingSugestion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 314);
            Controls.Add(btnNoThanks);
            Controls.Add(btnJoin);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "GroupMeetingSugestion";
            Padding = new Padding(2, 50, 2, 2);
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