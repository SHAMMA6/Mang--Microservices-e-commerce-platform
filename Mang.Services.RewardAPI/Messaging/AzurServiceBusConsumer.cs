
//using System.Text;
//using Newtonsoft.Json;
//using Azure.Messaging.ServiceBus;
//using Mang.Services.RewardAPI.Models;
//using Mang.Services.RewardAPI.Services;
//using Mang.Services.RewardAPI.Messaging;
//using Mang.Services.RewardAPI.Message;

//namespace Mang.Services.RewardAPI.Messaging
//{
//    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
//    {
//        private readonly string serviceBusConnectionString;
//        private readonly string orderCreatedTopic;
//        private readonly string orderCreatedRewardSubscription;
//        private readonly IConfiguration _configuration;
//        private readonly RewardService _rewardService;

//        private ServiceBusProcessor _rewardProcessor;

//        public AzureServiceBusConsumer(IConfiguration configuration, RewardService rewardService)
//        {
//            _rewardService = rewardService;
//            _configuration = configuration;

//            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");

//            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
//            orderCreatedRewardSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

//            var client = new ServiceBusClient(serviceBusConnectionString);
//            _rewardProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedRewardSubscription);
//        }

//        public async Task Start()
//        {
//            _rewardProcessor.ProcessMessageAsync += OnNewOrderRewardsRequestReceived;
//            _rewardProcessor.ProcessErrorAsync += ErrorHandler;
//            await _rewardProcessor.StartProcessingAsync();

//        }



//        public async Task Stop()
//        {
//            await _rewardProcessor.StopProcessingAsync();
//            await _rewardProcessor.DisposeAsync();

//        }

//        private async Task OnNewOrderRewardsRequestReceived(ProcessMessageEventArgs args)
//        {
//            //this is where you will receive message
//            var message = args.Message;
//            var body = Encoding.UTF8.GetString(message.Body);

//            RewardsMessage objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
//            try
//            {
//                //TODO - try to log email
//                await _rewardService.UpdateRewards(objMessage);
//                await args.CompleteMessageAsync(args.Message);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//        }

//        private Task ErrorHandler(ProcessErrorEventArgs args)
//        {
//            Console.WriteLine(args.Exception.ToString());
//            return Task.CompletedTask;
//        }



//    }
//}
