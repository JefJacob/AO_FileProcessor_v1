using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.IO;
using System.Configuration;

namespace AOFileProcessor.Service
{
    public partial class Service1 : ServiceBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Service1()
        {

            logger.Info("Directory watcher Constructor");
            InitializeComponent();
            this.ServiceName = "WindowsServiceAO2.NET";
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Directory watcher Initiated");
            var monitorFilePath = ConfigurationManager.AppSettings["datapath"];
            Watcher.MonitorDirectory(monitorFilePath);
        }

        protected override void OnStop()
        {
            logger.Info("Directory watcher Stopped");
        }
    }
}
