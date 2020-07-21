using System.Collections.Generic;

namespace VkPostStatisticsBot
{
    public interface IPostTextBuilder
    {
        string BuildPostText(string userId, Dictionary<string, double> statistics);
    }
}