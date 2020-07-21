using System.Collections.Generic;

namespace VkPostStatisticsBot
{
    public interface IPostsReader
    {
        IEnumerable<string> ReadPosts(string userId);
    }
}