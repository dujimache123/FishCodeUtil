namespace TableToLua
{
    partial class Form1
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
            this.textBox_sourceFilePath = new System.Windows.Forms.TextBox();
            this.btn_chooseFile = new System.Windows.Forms.Button();
            this.textBox_luaTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_generate = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_sourceFilePath
            // 
            this.textBox_sourceFilePath.Location = new System.Drawing.Point(13, 13);
            this.textBox_sourceFilePath.Name = "textBox_sourceFilePath";
            this.textBox_sourceFilePath.Size = new System.Drawing.Size(278, 21);
            this.textBox_sourceFilePath.TabIndex = 0;
            // 
            // btn_chooseFile
            // 
            this.btn_chooseFile.Location = new System.Drawing.Point(297, 11);
            this.btn_chooseFile.Name = "btn_chooseFile";
            this.btn_chooseFile.Size = new System.Drawing.Size(93, 23);
            this.btn_chooseFile.TabIndex = 1;
            this.btn_chooseFile.Text = "选择tab文件";
            this.btn_chooseFile.UseVisualStyleBackColor = true;
            this.btn_chooseFile.Click += new System.EventHandler(this.btn_chooseFile_Click);
            // 
            // textBox_luaTableName
            // 
            this.textBox_luaTableName.Location = new System.Drawing.Point(13, 52);
            this.textBox_luaTableName.Name = "textBox_luaTableName";
            this.textBox_luaTableName.Size = new System.Drawing.Size(218, 21);
            this.textBox_luaTableName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lua Table Name";
            // 
            // button_generate
            // 
            this.button_generate.Location = new System.Drawing.Point(197, 114);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(75, 23);
            this.button_generate.TabIndex = 4;
            this.button_generate.Text = "生成lua";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 89);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "format";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 262);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_generate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_luaTableName);
            this.Controls.Add(this.btn_chooseFile);
            this.Controls.Add(this.textBox_sourceFilePath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_sourceFilePath;
        private System.Windows.Forms.Button btn_chooseFile;
        private System.Windows.Forms.TextBox textBox_luaTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

