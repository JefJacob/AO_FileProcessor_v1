using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AOFileProcessor.Repository;

namespace AOFileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var subDirectory = DataReader.GetInputPaths();

            foreach (var dir in subDirectory)
            {
                DataReader.ProcessFile(dir);
            }

            //Console.WriteLine(ClubRepo.GetClub("ANIA").Name);
            //DateTime dob= DateTime.Parse("10-02-1971 00:00:00");
            //Console.WriteLine(dob);
            //Console.WriteLine(dob);
            //Console.WriteLine(dob.ToString("dd-MM-yyyy"));
            //string fileName = "GenericData-Athletics Ontario Outdoor Championship Series #3- U20 - Open Championships-12Jul2019-002.mdb";
            //Console.WriteLine(fileName.Replace(".mdb", ""));
            //string[] words = "Men Open 1500 Meter Run Open".Split(' ');
            //List<string> wList = new List<string>(words);
            //Console.WriteLine(string.Join(" ", wList));
            //wList.RemoveAt(0);
            //Console.WriteLine(string.Join(" ", wList));
            //wList.RemoveAt(wList.Count - 1);
            //Console.WriteLine(string.Join(" ", wList));
            //Console.WriteLine(wList.Count);

            Console.WriteLine("Task Completed");
            Console.ReadLine();
        }
    }
}
