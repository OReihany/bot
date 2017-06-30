using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using log4net;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MyTelegramBot.AnswerHandlers;
using MyTelegramBot.Interfaces;
using MyTelegramBot.QuestionHandlers;
using Telegram.Bot;

namespace MyTelegramBot
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            //container.Register(Component.For<IMongoClient>().Instance(new MongoClient("mongodb://localhost:27017")).LifestyleScoped());
            container.Register(Component.For<IMongoDatabase>().Instance(new MongoClient("mongodb://localhost:27017").GetDatabase("ParticipantsDatabase")).LifestylePerThread());
            container.Register(Component.For<ITelegramBotClient>().Instance(new TelegramBotClient("433979939:AAH4jlYVMEse1DvjiAdYOD5I1iBntjdzU2Y")));
            container.Register(Component.For<ITelegramService>().ImplementedBy<TelegramService>());
            container.Register(Component.For<IParticipantsRepository>().ImplementedBy<ParticipantsRepository>().LifestylePerThread());
            foreach (var source in typeof(IAnswerHandler).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IAnswerHandler).IsAssignableFrom(t)))
            {
                container.Register(Component.For<IAnswerHandler>().ImplementedBy(source).LifestylePerThread());
            }
            foreach (var source in typeof(IQuestionHandler).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(IQuestionHandler).IsAssignableFrom(t)))
            {
                container.Register(Component.For<IQuestionHandler>().ImplementedBy(source).LifestylePerThread());
            }
            //container.Kernel.Register(Classes
            //.FromThisAssembly()
            //.Pick().If(t => !t.IsAbstract && typeof(IAnswerHandler).IsAssignableFrom(t))
            //.Configure(configurer => configurer.Named(configurer.Implementation.Name))
            //.WithService.FromInterface()
            //.LifestylePerThread());
            //container.Register(Classes
            //.FromThisAssembly()
            //.Pick().If(t => !t.IsAbstract && typeof(IQuestionHandler).IsAssignableFrom(t))
            //.Configure(configurer => configurer.Named(configurer.Implementation.Name))
            //.LifestylePerThread());
            



            BsonClassMap.RegisterClassMap<ParticipatingInfo>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}