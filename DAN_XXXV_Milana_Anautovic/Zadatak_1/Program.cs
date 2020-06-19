using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        public static object l = new object();
        static int number;
        static int Guess;
        public static Random random = new Random();

        /// <summary>
        /// Method for guessing numbers
        /// </summary>
        /// <param name="t"></param>
        public static void GuessTheNumber(Thread t)
        {

            while (Guess != number)
            {
                lock (l)
                {
                    Guess = random.Next(1, 101);
                    if (Guess != number)
                    {
                        if ((Guess % 2 == 0 && number % 2 == 0) || (Guess % 2 == 1 && number % 2 == 1))
                        {
                            Console.WriteLine(t.Name + " chose the number " + Guess + ". Wrong chois. The participant hit parity.");
                        }

                        else
                        {
                            Console.WriteLine(t.Name + " chose the number " + Guess + ". Wrong chois.");
                        }

                    }
                    else
                    {

                        Console.WriteLine("Well done " + t.Name + "! The requested number is " + number);
                        Console.ReadLine();
                    }
                    Thread.Sleep(100);
                }
                
            }
        }



        public static int numUsers;
        /// <summary>
        /// Method for creating threads
        /// </summary>
        public static void ThreadGenerator()
        {
            Thread[] thr = new Thread[numUsers];

            for (int i = 0; i < numUsers; i++)
            {

                Thread t = new Thread(() => GuessTheNumber(Thread.CurrentThread)) //creating threads
                {

                    Name = string.Format("Participant_{0} ", i + 1) //naming threads

                };

                thr[i] = t;

            }

            foreach (var i in thr) i.Start();
            Console.ReadLine();

        }

        static void Main(string[] args)
        {
            bool guessBool;
            Console.WriteLine("***WELCOME***\n");
            Console.WriteLine("Enter the number of game participants");
            numUsers = AuxiliaryClass.ReadInteger();
            do
            {
                Console.Write("Enter the number needed to guess (1 to 100): ");
                string guessNumber = Console.ReadLine();
               
                guessBool = int.TryParse(guessNumber, out number);
                if (number < 1 || number >= 100)
                {
                    
                   Console.WriteLine("Please enter a number between 1 and 100.");
                   guessBool = false;
                }
            } while (!guessBool);
            

            Thread tg = new Thread(() => ThreadGenerator()); //creating thread
            tg.Name = "Thread_Generator"; //naming threads
            tg.Start();
            tg.Join();

        }
    }
}

