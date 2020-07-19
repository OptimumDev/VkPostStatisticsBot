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
                Count = 5
            });

            return posts.WallPosts.Select(p => p.Text);
        }
    }
}