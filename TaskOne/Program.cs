using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskOne
{
    class Program
    {
        static List<string> guidArray = new List<string>();
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

        public static void DeepDive()
        {
            val = 0;
            flag0 = false;
            try
            {
                do
                {
                    Console.Write("Enter subfolders count:");
                    val = Convert.ToInt32(Console.ReadLine());
                } while (val <= 0);

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
                if (val > 5)
                {
                    Console.WriteLine("Maximum of 5 nested folders can be created.");
                    val = 5;
                }
                string userName = Environment.UserName;
                string path = @"C:\Users\"; path += userName;
                string afterPath = @"\Desktop\"; path += afterPath;
                string guidPath = " ";

                guidArray.Clear(); //Clears before using 

                for (int i = 0; i < val; i++)
                {
                    guidPath = Guid.NewGuid().ToString();
                    if (i != 0) path += @"\";
                    path += guidPath;
                    guidArray.Add(path); //Set List[i] to  path
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                    finally { }
                }
            }
        }

        public static void DrownItDown()
        {
            char choice = ' ';
            string fileDir = " ";
            if (guidArray.Count() != 0) //Check if folders where even created at first place
            {
                val = 0;
                flag0 = false;

                try
                {
                    do
                    {
                        Console.Write("Enter how deep to make file: ");
                        val = Convert.ToInt32(Console.ReadLine());
                    } while (val < 0);
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

                if (guidArray.Count() >= val)
                {
                    if (flag0 == false)
                    {
                        string fileName = "emptyfile";

                        fileDir = guidArray[val - 1] + "\\" + fileName;
                        if (!File.Exists(fileDir))
                        {
                            File.Create(fileDir).Close();
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("File exist already. Overwrite? Y/N");
                                choice = Convert.ToChar(Console.ReadLine());
                                if (choice == 'Y')
                                {
                                    File.Delete(fileDir);
                                    File.Create(fileDir).Close();
                                }

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("FormatException catched.");
                            }
                            finally { }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no such subfolder!");
                }

            }
            else
            {
                try
                {
                    Console.WriteLine("There are no folders. Want to Create some? Y/N");
                    choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y')
                    {
                        DeepDive();
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("FormatException catched.");
                }
                finally { }
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
            DeepDive();
            Exit();
        }
    }
}