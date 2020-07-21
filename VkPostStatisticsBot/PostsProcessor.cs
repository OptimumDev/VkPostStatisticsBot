namespace VkPostStatisticsBot
{
    public class PostsProcessor
    {
        private readonly IPostsReader reader;
        private readonly IPostsStatisticsCounter statisticsCounter;
        private readonly IPostTextBuilder textBuilder;
        private readonly IPostCreator postCreator;

        public PostsProcessor(IPostsReader reader, IPostsStatisticsCounter statisticsCounter,
            IPostTextBuilder textBuilder, IPostCreator postCreator)
        {
            this.reader = reader;
            this.statisticsCounter = statisticsCounter;
            this.textBuilder = textBuilder;
            this.postCreator = postCreator;
        }

        public string ProcessPosts(string userId)
        {
            var posts = reader.ReadPosts(userId);
            var statistics = statisticsCounter.CountStatistics(posts);
            var text = textBuilder.BuildPostText(userId, statistics);
            postCreator.CreatePost(text);
            return text;
        }
    }
}