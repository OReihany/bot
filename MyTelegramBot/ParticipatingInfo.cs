using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using Telegram.Bot.Types.Enums;

namespace MyTelegramBot
{
    public class ParticipatingInfo
    {
        public ParticipatingInfo()
        {
            Answers = new Dictionary<Questions, string>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ChatType ChatType { get; set; }
        public string ChatId { get; set; }
        public string Name { get; set; }
        public MajorType? Major { get; set; }
        public decimal? EnglishPercent { get; set; }
        public decimal? MathPercent { get; set; }
        public decimal? CircutePercent { get; set; }
        public decimal? ElectronicsPercent { get; set; }
        public decimal? SignalPercent { get; set; }
        public decimal? ControllPercent { get; set; }
        public decimal? MachinePercent { get; set; }
        public decimal? MagneticPercent { get; set; }
        public decimal? RateAmount { get; set; }
        public Questions LastQuestion { get; set; }
        public int Version { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<Questions, string> Answers { get; set; }
        public bool IsArchived { get; set; }

        public bool Completed()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && Major.HasValue
                && EnglishPercent.HasValue
                && MathPercent.HasValue
                && CircutePercent.HasValue
                && ElectronicsPercent.HasValue
                && SignalPercent.HasValue
                && ControllPercent.HasValue
                && MachinePercent.HasValue
                && MagneticPercent.HasValue
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && RateAmount.HasValue;
        }

        public string GetPredictedRank(MajorType major)
        {
            var rate = GetTotalRate(major);
            if (rate == 0)
                return "";
            var ranks=Configure.Container.Resolve<IMongoDatabase>().GetCollection<DecisionData>("RateRankMapping");
            var rank2 = ranks.AsQueryable().Where(it=>it.Balance> rate).OrderBy(it=>it.Balance).FirstOrDefault();
            //var rank = DecisionTable.GetRateRankMapping().FirstOrDefault(it => it.FromRate <= rate && it.ToRate >= rate && it.MajorType== major);
            if (rank2 == null)
                return "";
            return $"رتبه {rank2.Rank}";
        }

        public decimal GetTotalRate(MajorType major)
        {
            if (!Completed())
                return 0;
            switch (major)
            {
                case MajorType.Electronic:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 4 + MachinePercent.Value * 1 + ControllPercent.Value * 1 +
                           SignalPercent.Value * 2 + MagneticPercent.Value * 2) / (decimal)18) / (decimal)65.23) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.PowerSystems:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 1 + MachinePercent.Value * 4 + ControllPercent.Value * 2 +
                           SignalPercent.Value * 1 + MagneticPercent.Value * 2) / (decimal)18) / (decimal)50) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.Machine:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 1 + MachinePercent.Value * 4 + ControllPercent.Value * 2 +
                           SignalPercent.Value * 2 + MagneticPercent.Value * 1) / (decimal)18) / (decimal)57.5) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.FieldComunication:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 2 + MachinePercent.Value * 1 + ControllPercent.Value * 1 +
                           SignalPercent.Value * 2 + MagneticPercent.Value * 4) / (decimal)18) / (decimal)63.9) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.SystemComunication:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 2 + MachinePercent.Value * 1 + ControllPercent.Value * 1 +
                           SignalPercent.Value * 4 + MagneticPercent.Value * 2) / (decimal)18) / (decimal)63.9) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.Control:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 1 + MachinePercent.Value * 2 + ControllPercent.Value * 4 +
                           SignalPercent.Value * 2 + MagneticPercent.Value * 1) / (decimal)18) / (decimal)59.52) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.MedicalEngineering:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 3 + MachinePercent.Value * 1 + ControllPercent.Value * 4 +
                           SignalPercent.Value * 4 + MagneticPercent.Value * 1) / (decimal)21) / (decimal)59.5) * (decimal)80) + RateAmount.Value) * 100;
                case MajorType.Mecatronic:
                    return (((((EnglishPercent.Value * 2 + MathPercent.Value * 3 + CircutePercent.Value * 3 +
                           ElectronicsPercent.Value * 4 + MachinePercent.Value * 4 + ControllPercent.Value * 4 +
                           SignalPercent.Value * 1 + MagneticPercent.Value * 1) / (decimal)22) / (decimal)45.2) * (decimal)80) + RateAmount.Value) * 100;
                default:
                    return 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("اطلاعات شما ");
            sb.AppendLine($"نام و نام خانوادگی : {Name}");
            sb.AppendLine($"شماره تماس : {PhoneNumber}");
            sb.AppendLine($"گرایش : {Configure.GetEnumDescription(Major)}");
            sb.AppendLine($"درصد زبان خارجه : {EnglishPercent}");
            sb.AppendLine($"درصد ریاضیات : {MathPercent}");
            sb.AppendLine($"درصد مدارهای الکتریکی : {CircutePercent}");
            sb.AppendLine($" درصد الکترونیک و مدار منطقی: {ElectronicsPercent}");
            sb.AppendLine($"درصد تجزیه و تحلیل سیگنال : {SignalPercent}");
            sb.AppendLine($"درصد ماشین و بررسی : {MachinePercent}");
            sb.AppendLine($"درصد کنترل خطی : {ControllPercent}");
            sb.AppendLine($"درصد الکترومغناطیس : {MagneticPercent}");
            sb.AppendLine($"معدل : {RateAmount}");
            sb.AppendLine();
            sb.AppendLine($"رتبه تخمینی شما در گرایش {Configure.GetEnumDescription(Major)} : {GetPredictedRank(this.Major.Value)}");
            sb.AppendLine("سایر گرایش ها");
            foreach (var otherMajor in Enum.GetValues(typeof(MajorType)).Cast<MajorType>().Where(it=>it!=this.Major))
            {
                sb.AppendLine($"رتبه تخمینی شما در گرایش {Configure.GetEnumDescription(otherMajor)} : {GetPredictedRank(otherMajor)}");
            }
            return sb.ToString();
        }
        
    }
}