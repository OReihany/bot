using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
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
        public decimal? RateAmount { get; set; }
        public Questions LastQuestion { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<Questions, string> Answers { get; set; }
    }
}