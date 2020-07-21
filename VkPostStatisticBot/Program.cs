using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;

namespace VkPostStatisticBot
{
    internal static class Program
    {
        private const string RussianLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private const string EnglishLetters = "abcdefghijklmnopqrstuvwxyz";

        private static void Main()
        {
            VkApi api;
            VkPostCreator postCreator;

            try
            {
                api = GetVkApi();
                postCreator = GetVkPostCreator(api);
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

            var postsProcessor = GetVkPostsProcessor(api, postCreator);
            RunUserProcessingLoop(postsProcessor);
        }

        private static void RunUserProcessingLoop(PostsProcessor postsProcessor)
        {
            Console.WriteLine("Вводите ID пользователей или групп по одному в строке");
            Console.WriteLine("Чтобы выйти, введите пустую строку");
            while (true)
            {
                var userId = Console.ReadLine();
                if (string.IsNullOrEmpty(userId))
                    break;

                try
                {
                    postsProcessor.ProcessPosts(userId);
                    Console.WriteLine($"Пост со статистикой {userId} создан!");
                }
                catch (Exception e) when (e is VkApiException || e is ArgumentException)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static PostsProcessor GetVkPostsProcessor(VkApi api, VkPostCreator postCreator)
        {
            var postsReader = new VkPostsReader(api);
            const string symbolsToCount = RussianLetters + EnglishLetters;
            var statisticCounter = new PostsSymbolFrequencyCounter(symbolsToCount);
            var textBuilder = new VkPostTextBuilder(api);

            return new PostsProcessor(postsReader, statisticCounter, textBuilder, postCreator);
        }

        private static VkPostCreator GetVkPostCreator(VkApi api)
        {
            var groupIdStr = GetEnvironmentVariable("VK_STATISTIC_GROUP_ID");

            if (!long.TryParse(groupIdStr, out var groupId))
                throw new ArgumentException("Group ID должен быть long числом");

            return new VkPostCreator(api, groupId);
        }

        private static VkApi GetVkApi()
        {
            var appIdStr = GetEnvironmentVariable("VK_STATISTIC_APP_ID");
            var login = GetEnvironmentVariable("VK_STATISTIC_USER_LOGIN");
            var password = GetEnvironmentVariable("VK_STATISTIC_USER_PASSWORD");

            if (!ulong.TryParse(appIdStr, out var appId))
                throw new ArgumentException("App ID должен быть ulong числом");

            var api = new VkApi();
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