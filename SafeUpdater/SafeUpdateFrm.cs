using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeUpdater
{
    public partial class SafeUpdateFrm : Form
    {
        SynchronizationContext m_SyncContext = null;
        public SafeUpdateFrm()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
            ReadLog();
        }

        private string historyFile = "history.json";
        Dictionary<string, List<string>> histories = new Dictionary<string, List<string>>();

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;
            var options = new ZipFileUpdaterOptions()
            {
                SourceZipFileName = this.cbSrcFilePath.Text,
                OutputPath = this.cbDestDirPath.Text,
                BackupOutputPath =this.cbBackupPath.Text,
                BackupName=this.cbBackupFileName.Text,
                OnProcessing = UpdateLog,
                OnEnd= OnEnd
            };
            //if (string.IsNullOrEmpty( options.BackupOutputPath))
            //{
            //    options.BackupOutputPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "backup");
            //}

            ZipFileUpdater zipFileUpdater = new ZipFileUpdater(options);

            zipFileUpdater.StartAsync();
            SaveLog();
            this.btnOpenDir.Enabled = false;
            this.btnOpenSrcFile.Enabled = false;
            this.btnStart.Enabled = false;
            
        }

        private bool Check()
        {
            if(string.IsNullOrEmpty(this.cbDestDirPath.Text) || !Directory.Exists(this.cbDestDirPath.Text))
            {
                MessageBox.Show("更新目录无效", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(this.cbSrcFilePath.Text) || !File.Exists(this.cbSrcFilePath.Text))
            {
                MessageBox.Show("更新压缩包无效", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(this.cbBackupPath.Text))
            {
                MessageBox.Show("备份路径无效", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (!Directory.Exists(this.cbBackupPath.Text))
                {
                    try
                    {
                        Directory.CreateDirectory(this.cbBackupPath.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("创建备份目录失败："+ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(this.cbBackupFileName.Text))
            {
                MessageBox.Show("备份文件名不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void OnEnd(UpdateStatus updateStatus)
        {
            if (this.InvokeRequired)
            {
                //this.Invoke(UpdateLog, updateStatus);
                m_SyncContext.Post((obj) => {
                    OnEnd((UpdateStatus)obj);
                }, updateStatus);
                return;
            }

            this.btnOpenDir.Enabled = true;
            this.btnOpenSrcFile.Enabled = true;
            this.btnStart.Enabled = true;
            UpdateLog(updateStatus);
        }

        private void UpdateLog(UpdateStatus updateStatus)
        {
            if(this.InvokeRequired)
            {
                //this.Invoke(UpdateLog, updateStatus);
                m_SyncContext.Post((obj)=>{
                    UpdateLog((UpdateStatus)obj);
                }, updateStatus);
                return;
            }
            //
            this.pbStatus.Value = (int)(updateStatus.CurrentIndex/updateStatus.Total*100);
            this.tbLogs.Text = updateStatus.Logs.ToString();
            this.tbLogs.Select(this.tbLogs.TextLength, 0);//光标定位到文本最后
            this.tbLogs.ScrollToCaret();//滚动到光标处
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            // OpenFileDialog dialog = new OpenFileDialog();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    cbDestDirPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Zip压缩文件(*.zip)|*.zip";
            if(dialog.ShowDialog()== DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    cbSrcFilePath.Text = dialog.FileName;
                }
            }
        }

        private void SaveLog()
        {
            if (histories.TryGetValue("dest", out List<string> dests))
            {
                if(!dests.Any(x=>x== this.cbDestDirPath.Text)){
                    dests.Insert(0,this.cbDestDirPath.Text);
                }
            }
            else
            {
                histories.Add("dest", new List<string>() { this.cbDestDirPath.Text });
            }
            if (histories.TryGetValue("source", out List<string> sources))
            {
                if (!sources.Any(x => x == this.cbSrcFilePath.Text))
                {
                    sources.Insert(0, this.cbSrcFilePath.Text);
                }
            }
            else
            {
                histories.Add("source", new List<string>() { this.cbSrcFilePath.Text });
            }
            if (histories.TryGetValue("backupPath", out List<string> backupPaths))
            {
                if (!backupPaths.Any(x => x == this.cbBackupPath.Text))
                {
                    backupPaths.Insert(0, this.cbSrcFilePath.Text);
                }
            }
            else
            {
                histories.Add("backupPath", new List<string>() { this.cbBackupPath.Text });
            }
            if (histories.TryGetValue("backupName", out List<string> backupNames))
            {
                if (!backupNames.Any(x => x == this.cbBackupFileName.Text))
                {
                    backupNames.Insert(0, this.cbBackupFileName.Text);
                }
            }
            else
            {
                histories.Add("backupName", new List<string>() { this.cbBackupFileName.Text });
            }

            string jsonStr = System.Text.Json.JsonSerializer.Serialize(histories);
            string output = Path.Combine(Directory.GetCurrentDirectory(), historyFile);
            File.WriteAllText(output,jsonStr);
        }

        private void ReadLog()
        {
            string output = Path.Combine(Directory.GetCurrentDirectory(), historyFile);
            if (File.Exists(output))
            {
                string jsonStr= File.ReadAllText(output);

                try
                {
                    histories = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonStr);
                }
                catch (Exception)
                {

                }
            }
        }

        private void InitCtl()
        {
            if(histories.TryGetValue("dest",out List<string> dests))
            {
                this.cbDestDirPath.Items.AddRange(dests.ToArray());
                this.cbDestDirPath.SelectedIndex = 0;
            }
            if (histories.TryGetValue("source", out List<string> sources))
            {
                this.cbSrcFilePath.Items.AddRange(sources.ToArray());
                this.cbSrcFilePath.SelectedIndex = 0;
            }
            if (histories.TryGetValue("backupPath", out List<string> backupPaths))
            {
                this.cbBackupPath.Items.AddRange(backupPaths.ToArray());
                this.cbBackupPath.SelectedIndex = 0;
            }
            if (histories.TryGetValue("backupName", out List<string> backupNames))
            {
                this.cbBackupFileName.Items.AddRange(backupNames.ToArray());
                this.cbBackupFileName.SelectedIndex = 0;
            }

            if (string.IsNullOrWhiteSpace(this.cbBackupPath.Text))
            {
                this.cbBackupPath.Text = Path.Combine(Directory.GetCurrentDirectory(), "backup");
            }
            if (string.IsNullOrWhiteSpace(this.cbBackupFileName.Text))
            {
                this.cbBackupFileName.Text = "backup";
            }
        }

        private void SafeUpdateFrm_Load(object sender, EventArgs e)
        {
            InitCtl();
        }

        private void btnOpenBackupPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    cbBackupPath.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
