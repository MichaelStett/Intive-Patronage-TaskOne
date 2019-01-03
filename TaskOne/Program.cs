using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskOne
{
    class Program
    {
        /// <summary>
        /// List of directories.
        /// </summary>
        public static List<string> GuidList = new List<string>();

        /// <summary>
        /// User input value.
        /// </summary>
        public static Int32 funcVal;

        /// <summary>
        /// Exception occurrence flag.
        /// </summary>
        public static bool isFlagged;

        /// <summary>
        /// Output Fizz and/or Buzz if number is divisible by 2 or 3.
        /// </summary>
        /// <exception cref="FormatException">
        /// Thrown when user input is pther than expected.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when user input is greater than MaxValue or smaller than MinValue.
        /// </exception>
        public static void FizzBuzz()
        {
     
            funcVal = 0;
            isFlagged = false;

            try
            {
                do
                {
                    Console.Write("Enter the number in the range <0, 1000>:");
                    funcVal = Convert.ToInt32(Console.ReadLine());
                } while (!Enumerable.Range(0, 1000).Contains(funcVal));
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message); 
                isFlagged = true;
            }
            finally { }

            if (!isFlagged)
            {
                if (funcVal % 2 == 0)
                {
                    Console.Write("Fizz");
                }

                if (funcVal % 3 == 0)
                {
                    Console.Write("Buzz");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates number of nested folders.
        /// </summary>
        /// <exception cref="FormatException">
        /// Thrown when user input is other than expected.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when user input is greater than MaxValue or smaller than MinValue.
        /// </exception>
        public static void DeepDive()
        {
            funcVal = 0;
            isFlagged = false;

            try
            {
                do
                {
                    Console.Write("Enter subfolders count:");
                    funcVal = Convert.ToInt32(Console.ReadLine());
                } while (funcVal <= 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                isFlagged = true;
            }
            finally { }

            if (!isFlagged)
            {
                const Int32 nestLimit = 5;
                if (funcVal > nestLimit)
                {
                    Console.WriteLine("Maximum of {0} nested folders can be created.", funcVal = nestLimit);
                }

                string fullPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Path of Directory

                GuidList.Clear(); //Clears List before creating new directory.

                foreach (Int32 index in Enumerable.Range(0, funcVal)) 
                {
                    fullPath = Path.Combine(fullPath, Guid.NewGuid().ToString());
                    GuidList.Add(fullPath); //Add new element to GuidList

                    try
                    {
                        Directory.CreateDirectory(fullPath);                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                        GuidList.Clear();
                        Console.WriteLine("No directory created.");
                        break;
                    }
                    finally { }                    
                }
            }
        }

        /// <summary>
        /// Create empty file in specified directory.
        /// </summary>
        /// <exception cref="FormatException">
        /// Thrown when user input is other than expected.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when user input is greater than MaxValue or smaller than MinValue.
        /// </exception>
        public static void DrownItDown()
        {
            isFlagged = false;
            funcVal = 0;

            char choice;

            if (GuidList.Count() <= 0) //If directory wasn't created
            {
                try
                {
                    Console.WriteLine("There are no folders. Want to Create some? Y/N");
                    choice = Convert.ToChar(Console.ReadLine());

                    if (choice == 'Y' || choice == 'y')
                    {
                        DeepDive();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                }
                finally { }
            }
            else //If was created
            {
                try
                {
                    do
                    {
                        Console.Write("Enter how deep to make file <1, {0}>: ", GuidList.Count());
                        funcVal = Convert.ToInt32(Console.ReadLine());
                    } while (funcVal <= 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                    isFlagged = true;
                }
                finally { }

                if (GuidList.Count() < funcVal) //If 'funcVal' is out of 'GuidList' range
                {
                    Console.WriteLine("There is no such subfolder! \nFile can't be created.");
                }
                else
                {
                    if (!isFlagged)
                    {
                        string fullPath = Path.Combine(GuidList.ElementAt(funcVal - 1), "emptyfile.txt");

                        if (File.Exists(fullPath)) //If file exist already
                        {
                            try
                            {
                                Console.WriteLine("File exist already. Overwrite? Y/N");
                                choice = Convert.ToChar(Console.ReadLine());

                                if (choice == 'Y' || choice == 'y')
                                {
                                    File.Delete(fullPath);
                                    File.Create(fullPath).Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                            }
                            finally { }                            
                        }
                        else 
                        {
                            try
                            {
                                File.Create(fullPath).Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);                                
                                Console.WriteLine("No file created.");
                            }
                            finally { }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Exit Method.
        /// </summary>
        public static void Exit()
        {
            //for debug mode use -> Console.ReadKey();
            return;
        }

        /// <summary>
        /// Main method.
        /// </summary>
        static void Main()
        {
            FizzBuzz();
            DeepDive();
            DrownItDown();
            DrownItDown();
            GuidList.Clear();
            DrownItDown();
            Exit();
        }
    }
}