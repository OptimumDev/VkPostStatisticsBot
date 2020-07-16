using System.Collections.Generic;

namespace VkPostStatisticBot
{
    public interface IPostsStatisticsCounter
    {
        Dictionary<string, double> CountStatistics(IEnumerable<string> posts);
    }
}