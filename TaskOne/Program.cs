using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskOne
{
    class Program
    {
        static List<string> guidList = new List<string>();
        static Int32 val = 0;
        static bool flag0 = false;

        public static void FizzBuzz() //Output if number is divisible by 2 and/or 3
        {
            try
            {
                do
                {
                    Console.Write("Enter the number in the range <0, 1000>:");
                    val = Convert.ToInt32(Console.ReadLine());
                } while (!Enumerable.Range(0, 1000).Contains(val));
            }
            catch (Exception e) //This handle all exceptions 
            {
                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", "")); 
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
            catch (Exception e)
            {
                Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
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
                StringBuilder pathDir = new StringBuilder(); //Path of Directory
                pathDir.Append(@"C:\Users\");
                pathDir.Append(Environment.UserName);
                pathDir.Append(@"\Desktop");                

                guidList.Clear(); //Clears List before creating new one

                for (int i = 0; i < val; i++)
                {                    
                    pathDir.Append("\\");
                    pathDir.Append(Guid.NewGuid().ToString());
                    guidList.Add(pathDir.ToString()); //Set List[i] to path
                    
                    if (flag0 == true) break; //If creating directory failed once exit loop

                    try
                    {
                        DirectoryInfo Dir = Directory.CreateDirectory(pathDir.ToString());                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                        guidList.Clear();
                        flag0 = true;
                    }
                    finally { }
                }
            }
        }

        public static void DrownItDown() //Create empty file in specific directory
        {
            char choice;
            
            StringBuilder fileDir = new StringBuilder(); //File directory            

            if (guidList.Count() > 0) //Check if directory where even created at first place
            {
                flag0 = false;
                val = 0;
                try
                {
                    do
                    {                        
                        Console.Write("Enter how deep to make file: ");
                        val = Convert.ToInt32(Console.ReadLine());
                    } while (val < 0);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                    flag0 = true;
                }
                finally { }

                if (guidList.Count() > val) //Check if directory isn't empty
                {
                    if (flag0 == false) //Checks user input flag
                    {
                        string fileName = "emptyfile.txt";                        
                        Console.WriteLine(guidList.ElementAt(val - 1));
                        fileDir.Append(guidList.ElementAt(val - 1));
                        fileDir.Append(@"\");
                        fileDir.Append(fileName);
                        Console.WriteLine(fileDir);

                        if (!File.Exists(fileDir.ToString()))
                        {
                            File.Create(fileDir.ToString()).Close();
                        }
                        else //If file exit already
                        {
                            try
                            {
                                Console.WriteLine("File exist already. Overwrite? Y/N");
                                choice = Convert.ToChar(Console.ReadKey());

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
                else
                {
                    Console.WriteLine("There are no such subfolder!");
                }
            }
            else //If not created at first place 
            {
                try
                {
                    Console.WriteLine("There are no folders. Want to Create some? Y/N");
                    choice = Convert.ToChar(Console.ReadKey());

                    if (choice == 'Y' || choice == 'y') DeepDive();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} catched.", e.GetType().ToString().Replace("System.", ""));
                }
                finally { }
            }
        }

        public static void Exit()
        {
            return;
        }

        static void Main(string[] args)
        {
            //FizzBuzz();
            DeepDive();
            DrownItDown();
            Exit();
        }
    }
}