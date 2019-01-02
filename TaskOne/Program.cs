using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskOne
{
    class Program
    {
        static List<string> GuidList = new List<string>();
        static Int32 funcVal;
        static bool isFlagged;
        
        public static void FizzBuzz() //Output if number is divisible by 2 and/or 3 
        {
            funcVal = 0;
            isFlagged = false;

            try //User input
            {
                do
                {
                    Console.Write("Enter the number in the range <0, 1000>:");
                    funcVal = Convert.ToInt32(Console.ReadLine());
                } while (!Enumerable.Range(0, 1000).Contains(funcVal));
            }
            catch (Exception ex) //This handle any exception
            {
                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message); 
                isFlagged = true;
            }
            finally { }

            if (isFlagged == false) //User input flag
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
        
        public static void DeepDive() //Creates 'funcVal' number of nested folders 
        {
            funcVal = 0;
            isFlagged = false;

            try //User input 
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

            if (isFlagged == false) //User input flag
            {
                const Int32 nestLimit = 5;
                if (funcVal > nestLimit)
                {
                    Console.WriteLine("Maximum of {0} nested folders can be created.", funcVal = nestLimit);
                }

                StringBuilder pathDir = new StringBuilder(); //Path of Directory
                pathDir.Append(@"C:\Users\");
                pathDir.Append(Environment.UserName);
                pathDir.Append(@"\Desktop");                

                GuidList.Clear(); //Clears List before creating new directory
               
                foreach (Int32 index in Enumerable.Range(0, funcVal)) 
                {
                    pathDir.Append(@"\");
                    pathDir.Append(Guid.NewGuid().ToString());

                    GuidList.Add(pathDir.ToString()); //Add new element to GuidList

                    try
                    {
                        Directory.CreateDirectory(pathDir.ToString());                        
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

        public static void DrownItDown() //Create empty file in specified directory 
        {
            isFlagged = false;
            funcVal = 0;

            char choice;

            if (GuidList.Count() <= 0) //If directory wasn't created
            {
                try //User input
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
                try //User input 
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
                    if (isFlagged == false) //User input flag
                    {
                        string fileName = "emptyfile.txt";

                        StringBuilder fileDir = new StringBuilder(); //File directory
                        fileDir.Append(GuidList.ElementAt(funcVal - 1));
                        fileDir.Append(@"\");
                        fileDir.Append(fileName);

                        if (File.Exists(fileDir.ToString())) //If file exist already
                        {
                            try //User input
                            {
                                Console.WriteLine("File exist already. Overwrite? Y/N");
                                choice = Convert.ToChar(Console.ReadLine());

                                if (choice == 'Y' || choice == 'y')
                                {
                                    File.Delete(fileDir.ToString());
                                    File.Create(fileDir.ToString()).Close();
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
                            try //User input
                            {
                                File.Create(fileDir.ToString()).Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\n{0} catched. \n{1} \n", ex.GetType().FullName.Replace("System.", ""), ex.Message);
                                isFlagged = true;

                                Console.WriteLine("No file created.");
                            }
                            finally { }
                        }
                    }
                }
            }
        }

        public static void Exit()
        {
            //for debug mode use -> Console.ReadKey();
            return;
        }

        static void Main(string[] args)
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