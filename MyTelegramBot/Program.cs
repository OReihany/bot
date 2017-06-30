using System;
using Castle.Windsor.Installer;
using log4net.Appender;
using Microsoft.Owin.Hosting;
using MyTelegramBot.Interfaces;

namespace MyTelegramBot
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Logger.Setup();
            string baseAddress = "http://localhost:9000/";
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                log.Info("web server started");
                Configure.Container.Install(FromAssembly.This());
                {
                    var telegramBot = Configure.Container.Resolve<ITelegramService>();
                    telegramBot.Start();
                    log.Info("telegram bot server started");
                }
                Console.ReadLine();
            }
        }
    }
}

