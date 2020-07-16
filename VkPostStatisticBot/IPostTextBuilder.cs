using System.Collections.Generic;

namespace VkPostStatisticBot
{
    public interface IPostTextBuilder
    {
        string BuildPostText(string userId, Dictionary<string, double> statistics);
    }
}