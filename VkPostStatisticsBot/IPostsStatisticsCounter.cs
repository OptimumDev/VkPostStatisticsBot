using System.Collections.Generic;

namespace VkPostStatisticsBot
{
    public interface IPostsStatisticsCounter
    {
        Dictionary<string, double> CountStatistics(IEnumerable<string> posts);
    }
}