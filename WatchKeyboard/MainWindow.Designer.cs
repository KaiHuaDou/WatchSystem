namespace WatchKeyboard
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Clear = new System.Windows.Forms.Button();
            this.AllKeys = new System.Windows.Forms.ListBox();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(12, 719);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 30);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "清空";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // AllKeys
            // 
            this.AllKeys.ItemHeight = 20;
            this.AllKeys.Location = new System.Drawing.Point(10, 10);
            this.AllKeys.Name = "AllKeys";
            this.AllKeys.Size = new System.Drawing.Size(255, 684);
            this.AllKeys.TabIndex = 2;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(190, 719);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 30);
            this.Save.TabIndex = 3;
            this.Save.Text = "保存";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 761);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.AllKeys);
            this.Controls.Add(this.Clear);
            this.Font = new System.Drawing.Font("黑体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(285, 800);
            this.MinimumSize = new System.Drawing.Size(285, 800);
            this.Name = "MainWindow";
            this.Text = "全局键盘监控";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ListBox AllKeys;
        private System.Windows.Forms.Button Save;
    }
}

