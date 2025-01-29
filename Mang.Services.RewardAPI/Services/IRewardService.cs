using Mang.Services.RewardAPI.Models;
using Mang.Services.RewardAPI.Message;

namespace Mang.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
      
    }
}
