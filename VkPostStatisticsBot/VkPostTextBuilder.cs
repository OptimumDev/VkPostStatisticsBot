using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VkNet;
using VkNet.Enums;

namespace VkPostStatisticsBot
{
    public class VkPostTextBuilder : IPostTextBuilder
    {
        private readonly VkApi api;

        public VkPostTextBuilder(VkApi api)
        {
            this.api = api;
        }

        public string BuildPostText(string userId, Dictionary<string, double> statistics)
        {
            var name = GetName(userId);
            var statisticsText = JsonConvert.SerializeObject(statistics);

            return $"{name}, статистика для последних {VkPostsReader.PostsCount} постов: {statisticsText}";
        }

        private string GetName(string id)
        {
            var resolved = api.Utils.ResolveScreenName(id);
            if (resolved == null)
                throw new ArgumentException("ID не найден");

            switch (resolved.Type)
            {
                case VkObjectType.User:
                    var user = api.Users.Get(new[] {id}).First();
                    return $"{user.FirstName} {user.LastName}";
                case VkObjectType.Group:
                    var group = api.Groups.GetById(null, id, null).First();
                    return group.Name;
                default:
                    throw new ArgumentException("ID принадлежит не пользователю или группе");
            }
        }
    }
}