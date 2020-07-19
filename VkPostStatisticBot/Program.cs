using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;

namespace VkPostStatisticBot
{
    internal static class Program
    {
        private static void Main()
        {
            VkApi api;
            VkPostCreator postCreator;

            try
            {
                var appIdStr = GetEnvironmentVariable("VK_STATISTIC_APP_ID");
                var login = GetEnvironmentVariable("VK_STATISTIC_USER_LOGIN");
                var password = GetEnvironmentVariable("VK_STATISTIC_USER_PASSWORD");
                var groupIdStr = GetEnvironmentVariable("VK_STATISTIC_GROUP_ID");

                api = GetVkApi(appIdStr, login, password);
                postCreator = GetVkPostCreator(groupIdStr, api);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            catch (VkAuthorizationException e)
            {
                Console.WriteLine($"Ошибка авторизации: {e.Message}");
                return;
            }

            postCreator.CreatePost("Test");

            // while (true)
            // {
            //     var userId = Console.ReadLine();
            //     if (string.IsNullOrEmpty(userId))
            //         break;
            //
            //     Console.WriteLine("Processing...");
            //     //postsProcessor.ProcessPosts(userId);
            //     Console.WriteLine("Done!");
            // }
        }

        private static VkPostCreator GetVkPostCreator(string groupId, VkApi api)
        {
            if (!long.TryParse(groupId, out var id))
                throw new ArgumentException("Group ID должен быть long числом");

            return new VkPostCreator(api, id);
        }

        private static VkApi GetVkApi(string appIdStr, string login, string password)
        {
            var api = new VkApi();

            if (!ulong.TryParse(appIdStr, out var appId))
                throw new ArgumentException("App ID должен быть ulong числом");

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = appId,
                Login = login,
                Password = password,
                Settings = Settings.Wall,
                TwoFactorAuthorization = GetTwoFactorAuthorizationCode
            });

            return api;
        }

        private static string GetEnvironmentVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name);
            if (variable == null)
                throw new ArgumentException($"Переменная \"{name}\" не установлена");
            return variable;
        }

        private static string GetTwoFactorAuthorizationCode()
        {
            Console.Write("Введите код авторизации: ");
            return Console.ReadLine();
        }
    }
}