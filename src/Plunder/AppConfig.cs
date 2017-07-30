using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Plunder.Ioc;

namespace Plunder
{
    public class AppConfig
    {
        public IIocManager IocManager { get; private set; }

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

        public static void Init(IIocManager iocManager)
        {
            _current = new AppConfig();
            _current.IocManager = iocManager;
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

        public ILogger ScheduleLogger => GetLooger("scheduler");

        public ILogger DownloadLogger => GetLooger("downloader");

        public ILogger ProcessLogger => GetLooger("processer");

        public ILogger PipeLogger => GetLooger("resultpipe");

        #endregion



    }
}
