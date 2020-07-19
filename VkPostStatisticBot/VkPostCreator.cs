using VkNet;
using VkNet.Model.RequestParams;

namespace VkPostStatisticBot
{
    public class VkPostCreator : IPostCreator
    {
        private readonly VkApi api;
        private readonly long groupId;

        public VkPostCreator(VkApi api, long groupId)
        {
            this.api = api;
            this.groupId = groupId;
        }

        public void CreatePost(string postText)
        {
            api.Wall.Post(new WallPostParams
            {
                OwnerId = -groupId,
                FromGroup = true,
                Message = postText
            });
        }
    }
}