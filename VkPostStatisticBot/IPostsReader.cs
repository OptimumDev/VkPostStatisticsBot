using System.Collections.Generic;

namespace VkPostStatisticBot
{
    public interface IPostsReader
    {
        IEnumerable<string> ReadPosts(string userId);
    }
}