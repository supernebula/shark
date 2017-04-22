using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Plunder
{
    public class AppConfig
    {
        private static AppConfig _current;

        public static AppConfig Current
        {
            get
            {
                if (_current == null)
                    _current = new AppConfig();
                return _current;
            }
        }

        public AppConfig()
        {

        }

        #region Logger

        private Dictionary<string, ILogger> _logDic = new Dictionary<string, ILogger>();

        public ILogger GetLooger(string name)
        {
            ILogger log;
            if (_logDic.ContainsKey(name))
            {
                _logDic.TryGetValue(name, out log);
                if (log == null)
                    _logDic.Remove(name);
                else
                    return log;
            }

            log = LogManager.GetLogger(name);
            _logDic.Add(name, log);
            return log;
        }

        public ILogger EngineLogger => GetLooger("engine");

        public ILogger SchedulerLogger => GetLooger("scheduler");

        public ILogger DownloaderLogger => GetLooger("downloader");

        public ILogger ProcesserLogger => GetLooger("processer");

        public ILogger ResultPipeLogger => GetLooger("resultpipe");

        #endregion



    }
}
