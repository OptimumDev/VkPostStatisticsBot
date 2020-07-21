using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace VkPostStatisticBot
{
    public class VkPostsReader : IPostsReader
    {
        private readonly VkApi api;

        public const int PostsCount = 5;

        public VkPostsReader(VkApi api)
        {
            this.api = api;
        }

        public IEnumerable<string> ReadPosts(string userId)
        {
            var posts = api.Wall.Get(new WallGetParams
            {
                Domain = userId,
                Filter = WallFilter.Owner,
                Count = PostsCount
            });

            return posts.WallPosts.Select(p => p.Text);
        }
    }
}