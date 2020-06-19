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

        public static void GuessTheNumber(Thread t)
        {
            Thread.Sleep(100);
            while (Guess != number)
            {
                Random random = new Random();
                Guess = random.Next(1, 101);

                Console.WriteLine(t.Name + " chose the number " + Guess + ". Wrong chois. Try again");

                
            }
            lock (l)
            {
                while (Guess == number)
                {
                    Console.WriteLine("Well done " + t.Name + "! The answer was " + number);
                    Console.ReadLine();
                }
            }
        }


        public static int numUsers;
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

            Console.WriteLine("***WELCOME***\n");
            Console.WriteLine("Enter the number of quiz participants");
            numUsers = AuxiliaryClass.ReadInteger();
            Console.WriteLine("Enter the number to quess");
            number = AuxiliaryClass.ReadInteger();

            Thread tg = new Thread(() => ThreadGenerator());
            tg.Start();
            tg.Join();
            
        }
    }
}

