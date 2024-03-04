using System.Diagnostics;

namespace Cryptid
{
    public static class Utility
    {
        public static byte[] GetStreamBytes(FileStream fs)
        {
            fs.Seek(0, SeekOrigin.Begin);
            int length = (int)fs.Length;

            byte[] content = new byte[length];
            fs.Read(content, 0, length);

            return content;
        }

        public static void EmptyStream(FileStream fs)
        {
            int length = (int)fs.Length;
            fs.Seek(0, SeekOrigin.Begin);
            byte[] emptyBuffer = new byte[length];
            fs.Write(emptyBuffer, 0, length);
            fs.Seek(0, SeekOrigin.Begin);
        }

        public static void HexDump(string file)
        {
            using (Stream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                int length = (int)fs.Length;
                byte[] buffer = new byte[length];

                fs.Read(buffer, 0, length);
                for (int i = 0; i < length; i++)
                {
                    Console.Write("{0:X}", "0x" + buffer[i] + " ");
                    if (i == 0)
                        continue;
                    if ((i % 5) == 0)
                        Console.WriteLine("\n");
                }
            }
        }

        public static char[] GenerateRandomKey()
        {
            Random rnd = new Random();
            char[] lowerChars = new char[26]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
                'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            char[] upperChars = new char[26]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            char[] special = new char[9]
            {
                '!', '@', '#', '$', '%', '&', '*', '(', ')'
            };
            char[] numbers = new char[9]
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

            char[] key = new char[32];
            int counter = 0;
            while (counter < 32)
            {
                int decider = rnd.Next(1, 5);
                if (decider == 1)
                    key[counter] = lowerChars[rnd.Next(0, 25)];
                else if (decider == 2)
                    key[counter] = upperChars[rnd.Next(0, 25)];
                else if (decider == 3)
                    key[counter] = numbers[rnd.Next(0, 9)];
                else if (decider == 4)
                    key[counter] = special[rnd.Next(0, 9)];

                counter++;
            }
            Console.WriteLine(key);
            return key;
        }

        public static void BruteForceKey()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var original = GenerateRandomKey();

            while (true)
            {
                var newk = GenerateRandomKey();
                if (newk == original)
                {
                    Console.WriteLine("Key matched: " + original);
                    break;
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.Minutes);
        }

        public static void CreateTestFile()
        {
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "/file.txt", "This is the content of the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
