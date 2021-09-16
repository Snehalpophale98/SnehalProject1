using System;

namespace Tutlane
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            string result = p.GetUserDetails("Suresh Dasari", 31);
            Console.WriteLine(result);
            Console.WriteLine("Press Enter Key to Exit..");
            Console.ReadLine();
        }
        public string GetUserDetails(string name, int age)
        {
            string info = string.Format("Name: {0}, Age: {1}", name, age);
            return info;
        }
    }
}