using System;
using System.Data;
using Castle.Windsor.Installer;
using log4net.Appender;
using Microsoft.Owin.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using MyTelegramBot.Interfaces;

namespace MyTelegramBot
{
    class Program
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Logger.Setup();
            var baseAddress = "http://localhost:9000/";
           
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                log.Info("web server started");
                Configure.Container.Install(FromAssembly.This());
                {
                    Console.WriteLine("import excel for decision or not?y:YES,n:NO");
                    var key = Console.ReadLine();
                    if (key.Trim().ToLower() == "y")
                    {
                        try
                        {
                            Import();
                            log.Info("decision table import successfully");
                        }
                        catch (Exception ex)
                        {

                            log.Error("excel import failed", ex);
                        }
                        
                    }
                    var telegramBot = Configure.Container.Resolve<ITelegramService>();
                    telegramBot.Start();
                    log.Info("telegram bot server started");
                }
                Console.ReadLine();
            }
        }

        private static void Import()
        {
            try
            {
                var telegramBot = Configure.Container.Resolve<IMongoDatabase>().GetCollection<DecisionData>("RateRankMapping");
                telegramBot.DeleteMany(new BsonDocument());
                foreach (DataRow row in SSS.ImportExcel().Rows)
                {
                    telegramBot.InsertOne(new DecisionData()
                    {
                        Balance = decimal.Parse(row["تراز"].ToString()),
                        Rank = decimal.Parse(row["رتبه"].ToString()),
                        Major = row["گرایش"].ToString(),
                        MajorType = GetMajorType(row["گرایش"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        private static MajorType GetMajorType(string major)
        {
            switch (major.Trim())
            {
                case "الکترونیک": return MajorType.Electronic;
                case "سیستمهایقدرت": return MajorType.PowerSystems;
                case "ماشینهایالکتریکی": return MajorType.Machine;
                case "مخابراتمیدان": return MajorType.FieldComunication;
                case "مخابراتسیستم": return MajorType.SystemComunication;
                case "مهندسیپزشکی": return MajorType.MedicalEngineering;
                case "کنترل": return MajorType.Control;
                case "مکاترونیک": return MajorType.Mecatronic;
                default: return MajorType.Electronic;
            }
        }
    }

    public class DecisionData
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Rank { get; set; }
        public string Major { get; set; }
        public MajorType MajorType { get; set; }
    }
}

