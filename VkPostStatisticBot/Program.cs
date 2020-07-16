using System;

namespace VkPostStatisticBot
{
    internal static class Program
    {
        private static void Main()
        {
            while (true)
            {
                var userId = Console.ReadLine();
                if (string.IsNullOrEmpty(userId))
                    break;

                Console.WriteLine("Processing...");
                //postsProcessor.ProcessPosts(userId);
                Console.WriteLine("Done!");
            }
        }
    }
}