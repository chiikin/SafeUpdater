using System;
using System.Collections.Generic;
using System.Text;

namespace SafeUpdater
{
    public class ZipFileUpdaterOptions
    {
        /// <summary>
        /// 更新源压缩包
        /// </summary>
        public string SourceZipFileName { get; set; }

        /// <summary>
        /// 备份输出目录
        /// </summary>
        public string BackupOutputPath { get; set; }
        /// <summary>
        /// 输出目录
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 备份文件名
        /// </summary>
        public string BackupName { get; set; }

        public Action<UpdateStatus> OnProcessing { get; set; }
        public Action<UpdateStatus> OnEnd { get; set; }
    }
}
