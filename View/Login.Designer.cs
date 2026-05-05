namespace OOAD
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            txtPass = new TextBox();
            btnOK = new Button();
            btnReset = new Button();
            btnCancel = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(31, 41, 55);
            label1.Location = new Point(122, 188);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 28);
            label1.TabIndex = 0;
            label1.Text = "Gmail";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(31, 41, 55);
            label2.Location = new Point(122, 255);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(102, 28);
            label2.TabIndex = 1;
            label2.Text = "Password:";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.ForeColor = Color.FromArgb(31, 41, 55);
            txtName.Location = new Point(287, 188);
            txtName.Margin = new Padding(4, 2, 2, 2);
            txtName.MaxLength = 35000;
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Nhập gmail của bạn vào đây";
            txtName.Size = new Size(382, 29);
            txtName.TabIndex = 2;
            // 
            // txtPass
            // 
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.ForeColor = Color.FromArgb(31, 41, 55);
            txtPass.Location = new Point(287, 257);
            txtPass.Margin = new Padding(2);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.PlaceholderText = "Nhập mật khẩu tài khoản";
            txtPass.Size = new Size(382, 29);
            txtPass.TabIndex = 3;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(37, 99, 235);
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnOK.Cursor = Cursors.Hand;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(121, 373);
            btnOK.Margin = new Padding(2);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(132, 54);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(37, 99, 235);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(329, 373);
            btnReset.Margin = new Padding(2);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(132, 54);
            btnReset.TabIndex = 5;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(37, 99, 235);
            btnCancel.Location = new Point(537, 373);
            btnCancel.Margin = new Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(132, 54);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Elephant", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(37, 99, 235);
            label3.Location = new Point(329, 74);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(145, 51);
            label3.TabIndex = 7;
            label3.Text = "Login";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(817, 526);
            Controls.Add(label3);
            Controls.Add(btnCancel);
            Controls.Add(btnReset);
            Controls.Add(btnOK);
            Controls.Add(txtPass);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(31, 41, 55);
            Margin = new Padding(2);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtName;
        private TextBox txtPass;
        private Button btnOK;
        private Button btnReset;
        private Button btnCancel;
        private Label label3;
    }
}
