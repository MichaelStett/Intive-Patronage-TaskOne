using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskOne
{
    class Program
    {
        static int val = 0;
        static bool flag0 = false;

        public static void FizzBuzz()
        {
            try
            {
                do
                {
                    Console.Clear();
                    Console.Write("Enter the number in the range <0, 1000>:");
                    val = Convert.ToInt32(Console.ReadLine());
                } while (!Enumerable.Range(0, 1000).Contains(val));
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException catched.");
                flag0 = true;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException catched.");
                flag0 = true;
            }
            finally { }

            if (flag0 == false)
            {
                if (val % 2 == 0) Console.Write("Fizz");
                if (val % 3 == 0) Console.Write("Buzz");
                Console.WriteLine(" ");
            }

        }        

        public static void Exit()
        {
            Console.WriteLine("\nProgram will close.");
            Console.ReadKey();
            return;
        }

        static void Main(string[] args)
        {
            FizzBuzz();         

            Exit();
        }
    }
}