using System;
using System.Collections.Generic;
using System.Text;

namespace SafeUpdater
{
    public class UpdateStatus
    {
        public int Total { get; set; }
        public int CurrentIndex { get; set; }
        public StringBuilder Logs { get; private set; } = new StringBuilder();

        public int CompleteRate
        {
            get
            {
                if (Total == 0)
                    return 0;
                return (int)(CurrentIndex / Total * 100);
            }
        }
        public void Log(string str)
        {
            Logs.AppendLine(str);
        }
    }
}
