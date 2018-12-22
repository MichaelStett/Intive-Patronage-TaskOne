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
        static bool flag0;
        
        public static void FizzBuzz() //Output if number is divisible by 2 and/or 3 
        {
            funcVal = 0;
            flag0 = false;

            try //User input
            {
                do
                {
                    Console.Write("Enter the number in the range <0, 1000>:");
                    funcVal = Convert.ToInt32(Console.ReadLine());
                } while (!Enumerable.Range(0, 1000).Contains(funcVal));
            }
            catch (Exception e) //This handle all exceptions 
            {
                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", "")); 
                flag0 = true;
            }
            finally { }

            if (flag0 == false) //Checks user input flag
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
            flag0 = false;

            try //User input 
            {
                do
                {
                    Console.Write("Enter subfolders count:");
                    funcVal = Convert.ToInt32(Console.ReadLine());
                } while (funcVal <= 0);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                flag0 = true;
            }
            finally { }

            if (flag0 == false) //Checks user input flag
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
               
                foreach (int index in Enumerable.Range(0, funcVal)) 
                {
                    pathDir.Append(@"\");
                    pathDir.Append(Guid.NewGuid().ToString());

                    GuidList.Add(pathDir.ToString()); //Add new element to GuidList
                    
                    if (flag0 == true)
                    {
                        break; //If creating directory failed once exit loop
                    }

                    try
                    {
                        Directory.CreateDirectory(pathDir.ToString());                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));                        
                        flag0 = true;

                        GuidList.Clear();
                        Console.WriteLine("No directory created.");
                    }
                    finally { }
                }
            }
        }

        public static void DrownItDown() //Create empty file in specific directory 
        {
            flag0 = false;
            funcVal = 0;

            char choice;

            if (GuidList.Count() <= 0) //Check if directory where even created at first place
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
                catch (Exception e)
                {
                    Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                }
                finally { }
            }
            else //If not created at first place 
            {
                try //User input 
                {
                    do
                    {
                        Console.Write("Enter how deep to make file: ");
                        funcVal = Convert.ToInt32(Console.ReadLine());
                    } while (funcVal <= 0);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                    flag0 = true;
                }
                finally { }

                if (GuidList.Count() <= funcVal) //Check if directory isn't empty
                {
                    Console.WriteLine("There are no such subfolder! File can't created.");
                }
                else
                {
                    if (flag0 == false) //Checks user input flag
                    {
                        var fileName = "emptyfile.txt";

                        StringBuilder fileDir = new StringBuilder(); //File directory      
                        fileDir.Append(GuidList.ElementAt(funcVal - 1));
                        fileDir.Append(@"\");
                        fileDir.Append(fileName);

                        if (!File.Exists(fileDir.ToString()))
                        {
                            try
                            {
                                File.Create(fileDir.ToString()).Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                                flag0 = true;

                                Console.WriteLine("No file created.");
                            }
                        }
                        else //If file exit already
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
                            catch (Exception e)
                            {
                                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                            }
                            finally { }
                        }
                    }
                }
            }
        }

        public static void Exit()
        {
            //for debug mode use here -> Console.ReadKey();
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