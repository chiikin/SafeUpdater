using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SafeUpdater
{
    public class ZipFileUpdater : IDisposable
    {
        public ZipFileUpdaterOptions _options;
        private ZipFile _zipFile = null;
        private ZipFile _zipBackUpFile = null;
        private string _tempOutputPath = string.Empty;
        private string _tempOutputBackupFile = string.Empty;

        public ZipFileUpdater(ZipFileUpdaterOptions options)
        {
            _options = options;
            if (!File.Exists(options.SourceZipFileName))
            {
                throw new FileNotFoundException(options.SourceZipFileName);
            }

            if (!Directory.Exists(_options.BackupOutputPath))
            {
                Directory.CreateDirectory(_options.BackupOutputPath);
            }
            string dateString = DateTime.Now.ToString("yyyyMMddHHmmssff");
            string fileName = $"{_options.BackupName??"backup"}-{dateString}.zip";
            _tempOutputPath = $"temp-{dateString}";
            _tempOutputPath = Path.Combine(_options.BackupOutputPath, _tempOutputPath);
            _tempOutputBackupFile = Path.Combine(_options.BackupOutputPath, fileName);
            //_zipBackUpFile =ZipFile.Create(File.Create(Path.Combine(_options.BackupOutputPath, fileName)));
            _zipFile = new ZipFile(File.Open(options.SourceZipFileName, FileMode.Open));
        }

        public Task StartAsync()
        {
            if (disposedValue)
                throw new Exception("实例已关闭");

            return Task.Factory.StartNew(Update);
        }

        private void Update()
        {
            UpdateStatus updateStatus = new UpdateStatus();
            updateStatus.Total = (int)_zipFile.Count;
            _options.OnProcessing.Invoke(updateStatus);
            for (int i = 0; i < updateStatus.Total; i++)
            {
                updateStatus.CurrentIndex = i + 1;
                var zipEntryFromZippedFile = _zipFile[i];
                string destName = Path.Combine(_options.OutputPath, zipEntryFromZippedFile.Name);
                if (zipEntryFromZippedFile.IsFile)
                {
                    bool isNew = true;
                    FileInfo fi = new FileInfo(destName);
                    if (fi.Exists)
                    {//如果原始文件存在则备份
                        BackToTemp(destName, zipEntryFromZippedFile.Name);
                        isNew = false;
                    }
                    else
                    {
                        if (!fi.Directory.Exists)
                        {
                            fi.Directory.Create();
                            updateStatus.Log($"新建目录 {fi.Directory.FullName}");
                        }
                    }
                    using (var fs = File.Open(destName, FileMode.Create, FileAccess.Write))
                    {
                        using (var srcStream = _zipFile.GetInputStream(i))
                        {
                            srcStream.CopyTo(fs);
                            fs.Flush();
                            //WriteFile(fs, _zipFile);
                        }
                    }
                    updateStatus.Log($"{(isNew ? "新建" : "更新")} {zipEntryFromZippedFile.Name}");
                }
                else if (zipEntryFromZippedFile.IsDirectory)
                {
                    if (!Directory.Exists(destName))
                    {
                        Directory.CreateDirectory(destName);
                        updateStatus.Log($"新建目录 {zipEntryFromZippedFile.Name}");
                    }
                }
                if (i % 10 == 0)
                    _options.OnProcessing.Invoke(updateStatus);
            }
            
            PackageBackFolder();
            updateStatus.Log($"打包备份文件 {_tempOutputBackupFile}");
            updateStatus.Log($"更新结束");
            Dispose();
            _options.OnEnd.Invoke(updateStatus);
        }

        private void PackageBackFolder()
        {
            System.IO.Compression.ZipFile.CreateFromDirectory(_tempOutputPath,_tempOutputBackupFile);
            Directory.Delete(_tempOutputPath, true);
        }

        private void BackToTemp(string fullFileName, string relativeFileName)
        {
            string backFullFileName = Path.Combine(_tempOutputPath, relativeFileName);
            FileInfo fi = new FileInfo(backFullFileName);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            File.Copy(fullFileName, backFullFileName);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (_zipFile != null)
                    {
                        _zipFile.Close();
                        //_zipFile.Dispose();
                        _zipFile = null;
                    }
                    if (_zipBackUpFile != null)
                    {
                        _zipBackUpFile.Close();
                        //_zipBackUpFile.Dispose();
                        _zipBackUpFile = null;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ZipFileUpdater()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
