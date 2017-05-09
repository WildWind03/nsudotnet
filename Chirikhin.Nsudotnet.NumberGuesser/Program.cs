using System;

namespace Chirikhin.Nsudotnet.NumberGuesser
{
    class Program
    {
        private const int MaxRandomNumber = 100;
        private const int MaxCoundOfFailedAttempts = 1000;
        private const int OffensiveCommentsFrequency = 4;
        private const int CountOfComments = 4;
        private const string AuthoName = "Alexander Chirikhin";
        private const string QuitString = "q";

        public static void Main()
        {
            var startDateTime = DateTime.Now;
            Console.WriteLine("Please, type your name");
            var userName = Console.ReadLine();

            string[] offensiveComments;

            if (userName != AuthoName)
            {
                Console.WriteLine("What's a crappy name! Just type a number and pray that I'll come up with the same!");

                offensiveComments = new[]
                {
                    "It's Roman Posokhin, isn't it? Even if it isn't, your smell and your creepy face make me suppose that your are his sibling! What are you talking?! You are {0}?! Then I'm sorry! You just a piece of crap, nothing more!",
                    "Come on, {0}! Don't be like Roman Posokhin!",
                     "Your 'intellegence' is your major flaw! {0}, you are from Omsk, aren't you?",
                     "{0}, are really so foolish?! It's incredible that you exist despite the evolution!"
                };

            } else
            {
                Console.WriteLine("Welcome, brother! Glad to see you again! Please, type a number to start the game!");
                offensiveComments = new[]
                {
                    "Hold on, {0}! Together we'll win!",
                    "{0}, just believe and you'll make it!",
                     "Don't worry, {0}! I feel that the victory is so near!",
                    "{0}, you are the smartest man I've ever met! So cool!"
            };
            }

            var failedAttempts = new int[MaxCoundOfFailedAttempts];
            var countOfFailedAttempts = 0;
            var random = new Random();

            var randomNumber = random.Next(1, MaxRandomNumber);

            for (var k = 0; ; k++)
            {
                var typedNumberString = Console.ReadLine();

                if (typedNumberString == QuitString)
                {
                    Console.WriteLine("Buzz like never before!");
                    break;
                }

                try
                {
                    var typedNumberInteger = int.Parse(typedNumberString);

                    if (typedNumberInteger == randomNumber)
                    {
                        Console.WriteLine("Congratulations!");
                        Console.WriteLine("Attempts Count: {0}", countOfFailedAttempts + 1);
                        Console.Write("History: ");

                        for (var i = 0; i < countOfFailedAttempts; ++i)
                        {
                            var lessOrGreaterLabel = failedAttempts[i] > randomNumber ? ">" : "<";
                            Console.Write(" {0}({1})", failedAttempts[i], lessOrGreaterLabel);
                        }

                        Console.Write(" {0}({1})", randomNumber, "=");

                        Console.WriteLine();
                        Console.WriteLine("Count of passed seconds: {0}", (DateTime.Now - startDateTime).Seconds);
                        break;
                    }
                    if (countOfFailedAttempts >= MaxCoundOfFailedAttempts - 1)
                    {
                        Console.WriteLine("Too many attempts. You are just a looser!");
                        break;
                    }

                    failedAttempts[countOfFailedAttempts++] = typedNumberInteger;

                    if (typedNumberInteger < randomNumber)
                    {
                        Console.WriteLine("Your number is less");
                    }
                    else
                    {
                        Console.WriteLine("Your number is greater");
                    }

                    if (0 == countOfFailedAttempts % (OffensiveCommentsFrequency - 1))
                    {
                        Console.WriteLine(offensiveComments[random.Next(0, CountOfComments - 1)], userName);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("What kind of boolshit you have typed!? It's even not a number!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Put such numbers into your tremendous butthole!");
                }
            }

            Console.ReadKey();
        }
    }
}
