using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace SubtitleRenamer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter your video path: ");
            var path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                Console.Write("Enter video extension name, without dot: ");
                var videoExt = Console.ReadLine();

                Console.Write("Enter subtitle extension: ");
                var subExt = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Calculating now. Please wait some seconds...");

                var di = new DirectoryInfo(path);

                var videoFiles = di.GetFiles($"*.{videoExt}");
                var subFiles = di.GetFiles($"*.{subExt}");

                var videoFinalList = videoFiles.OrderBy(v => v.Name).ToArray();
                var subFinalList = subFiles.OrderBy(v => v.Name).ToArray();

                if (videoFinalList.Length == subFinalList.Length)
                {
                    Console.Clear();
                    Console.WriteLine("Please check a final result before renaming:");
                    Console.WriteLine();

                    for (var i = 0; i < videoFinalList.Length; i++)
                        Console.WriteLine(
                            $"{subFinalList[i].Name} => {videoFinalList[i].Name.Replace($".{videoExt}", $".{subExt}")}");

                    Console.WriteLine("Press [ENTER] to confirm. Any keys to exit...");
                    var enteredKey = Console.ReadKey();

                    if (enteredKey.Key == ConsoleKey.Enter)
                    {
                        for (var i = 0; i < videoFinalList.Length; i++)
                        {
                            var oldPath = subFinalList[i].FullName;
                            var newPath = Path.Combine(videoFinalList[i].DirectoryName,
                                videoFinalList[i].Name.Replace($".{videoExt}", $".{subExt}"));

                            File.Move(oldPath, newPath);
                        }

                        Console.WriteLine();
                        Console.WriteLine("The operation is completed. Please click any key to exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Some more files are there. Please check again. Shutting down in 2 seconds!");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("That directory doesn't exist. Shutting down in 2 seconds!");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
    }
}