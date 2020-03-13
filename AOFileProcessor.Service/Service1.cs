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

namespace AOFileProcessor.Service
{
    public partial class Service1 : ServiceBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Service1()
        {

            logger.Info("Directory watcher Constructor");
            InitializeComponent();
            this.ServiceName = "WindowsServiceAO.NET";
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Directory watcher Initiated");
            Watcher.MonitorDirectory(@"J:\Courses\Capstone\FileUpload\Source\InputFiles");
        }

        protected override void OnStop()
        {
            logger.Info("Directory watcher Stopped");
        }
    }
}
