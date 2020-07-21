using VkNet;
using VkNet.Model.RequestParams;

namespace VkPostStatisticBot
{
    public class VkPostCreator : IPostCreator
    {
        private readonly VkApi api;
        private readonly long wallId;

        public VkPostCreator(VkApi api, long wallId)
        {
            this.api = api;
            this.wallId = wallId;
        }

        public void CreatePost(string postText)
        {
            api.Wall.Post(new WallPostParams
            {
                OwnerId = wallId,
                FromGroup = true,
                Message = postText
            });
        }
    }
}