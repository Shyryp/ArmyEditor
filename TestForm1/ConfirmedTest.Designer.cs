namespace TestForm1
{
    partial class ConfirmedTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmedTest));
            this.label1 = new System.Windows.Forms.Label();
            this.YesConf = new System.Windows.Forms.Button();
            this.NotConf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тест работает и все элементы расположены как надо?";
            // 
            // YesConf
            // 
            this.YesConf.Location = new System.Drawing.Point(66, 65);
            this.YesConf.Name = "YesConf";
            this.YesConf.Size = new System.Drawing.Size(102, 47);
            this.YesConf.TabIndex = 1;
            this.YesConf.Text = "Подтвердить";
            this.YesConf.UseVisualStyleBackColor = true;
            this.YesConf.Click += new System.EventHandler(this.button1_Click);
            // 
            // NotConf
            // 
            this.NotConf.Location = new System.Drawing.Point(270, 65);
            this.NotConf.Name = "NotConf";
            this.NotConf.Size = new System.Drawing.Size(141, 47);
            this.NotConf.TabIndex = 2;
            this.NotConf.Text = "Не подтверждать";
            this.NotConf.UseVisualStyleBackColor = true;
            this.NotConf.Click += new System.EventHandler(this.button2_Click);
            // 
            // ConfirmedTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 129);
            this.Controls.Add(this.NotConf);
            this.Controls.Add(this.YesConf);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 165);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 165);
            this.Name = "ConfirmedTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подтверждение работоспособности теста";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button YesConf;
        private System.Windows.Forms.Button NotConf;
    }
}