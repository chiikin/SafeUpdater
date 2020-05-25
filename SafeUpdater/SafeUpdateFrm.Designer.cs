namespace SafeUpdater
{
    partial class SafeUpdateFrm
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
            this.cbDestDirPath = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.btnOpenSrcFile = new System.Windows.Forms.Button();
            this.cbSrcFilePath = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBackupFileName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBackupPath = new System.Windows.Forms.ComboBox();
            this.btnOpenBackupPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDestDirPath
            // 
            this.cbDestDirPath.FormattingEnabled = true;
            this.cbDestDirPath.Location = new System.Drawing.Point(91, 12);
            this.cbDestDirPath.Name = "cbDestDirPath";
            this.cbDestDirPath.Size = new System.Drawing.Size(362, 25);
            this.cbDestDirPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "更新目录";
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Location = new System.Drawing.Point(459, 12);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(33, 23);
            this.btnOpenDir.TabIndex = 2;
            this.btnOpenDir.Text = "...";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // btnOpenSrcFile
            // 
            this.btnOpenSrcFile.Location = new System.Drawing.Point(459, 43);
            this.btnOpenSrcFile.Name = "btnOpenSrcFile";
            this.btnOpenSrcFile.Size = new System.Drawing.Size(33, 23);
            this.btnOpenSrcFile.TabIndex = 2;
            this.btnOpenSrcFile.Text = "...";
            this.btnOpenSrcFile.UseVisualStyleBackColor = true;
            this.btnOpenSrcFile.Click += new System.EventHandler(this.btnOpenSrcFile_Click);
            // 
            // cbSrcFilePath
            // 
            this.cbSrcFilePath.FormattingEnabled = true;
            this.cbSrcFilePath.Location = new System.Drawing.Point(91, 43);
            this.cbSrcFilePath.Name = "cbSrcFilePath";
            this.cbSrcFilePath.Size = new System.Drawing.Size(362, 25);
            this.cbSrcFilePath.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "更新文件";
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(12, 136);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(413, 23);
            this.pbStatus.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(431, 136);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(61, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbLogs
            // 
            this.tbLogs.Location = new System.Drawing.Point(12, 165);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogs.Size = new System.Drawing.Size(473, 125);
            this.tbLogs.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "备份文件名";
            // 
            // cbBackupFileName
            // 
            this.cbBackupFileName.FormattingEnabled = true;
            this.cbBackupFileName.Location = new System.Drawing.Point(91, 105);
            this.cbBackupFileName.Name = "cbBackupFileName";
            this.cbBackupFileName.Size = new System.Drawing.Size(362, 25);
            this.cbBackupFileName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "备份目录";
            // 
            // cbBackupPath
            // 
            this.cbBackupPath.FormattingEnabled = true;
            this.cbBackupPath.Location = new System.Drawing.Point(91, 74);
            this.cbBackupPath.Name = "cbBackupPath";
            this.cbBackupPath.Size = new System.Drawing.Size(362, 25);
            this.cbBackupPath.TabIndex = 0;
            // 
            // btnOpenBackupPath
            // 
            this.btnOpenBackupPath.Location = new System.Drawing.Point(459, 74);
            this.btnOpenBackupPath.Name = "btnOpenBackupPath";
            this.btnOpenBackupPath.Size = new System.Drawing.Size(33, 23);
            this.btnOpenBackupPath.TabIndex = 2;
            this.btnOpenBackupPath.Text = "...";
            this.btnOpenBackupPath.UseVisualStyleBackColor = true;
            this.btnOpenBackupPath.Click += new System.EventHandler(this.btnOpenBackupPath_Click);
            // 
            // SafeUpdateFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 302);
            this.Controls.Add(this.btnOpenBackupPath);
            this.Controls.Add(this.cbBackupPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbBackupFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSrcFilePath);
            this.Controls.Add(this.btnOpenSrcFile);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDestDirPath);
            this.Name = "SafeUpdateFrm";
            this.Text = "文件安全更新器";
            this.Load += new System.EventHandler(this.SafeUpdateFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDestDirPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.Button btnOpenSrcFile;
        private System.Windows.Forms.ComboBox cbSrcFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBackupFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBackupPath;
        private System.Windows.Forms.Button btnOpenBackupPath;
    }
}

