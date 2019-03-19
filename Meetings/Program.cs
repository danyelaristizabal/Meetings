using System;
using System.Collections.Generic; 
using System.Linq; 
namespace Meetings
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arrange = new List<int>{};
            for (int i = 0; i < 50; i++)
            {
                arrange.Add(4); 
            }
            for (int i = 940; i > 0; i--)
            {
                arrange.Add(i); 
            }
            var n = arrange.Count(); 
            //for (int i = 0; i < n; i++)
            //{
            //    arrange.Add(int.Parse(Console.ReadLine())); 
            //}

            MeetingEngine myengine = new MeetingEngine(arrange);
            myengine.Meet();
            Console.WriteLine();
            var persons = myengine.ReturnPeople();
            var connected = myengine.ReturnConnectedOnes();
            var notConnected = myengine.ReturnKickedOutNumbers(); 

            Console.WriteLine("CONNECTIONS VIEWER ");
            if (connected.Count == 0)
            {
                Console.WriteLine("No connections avaiable");

            }

            Console.WriteLine();
            var ordered = connected.OrderBy(i => i.OriginalIndex);
            var orderedList = ordered.ToList();
            Console.WriteLine("Initial Arrange");
            arrange.ForEach(i => Console.Write($"{i} ")); 
            Console.WriteLine();
            Console.WriteLine();
            if (notConnected.Count > 0)
            {
                Console.WriteLine("Failed to connect number(s):");
                notConnected.ForEach(i => Console.Write($"{i.OriginalMeetingNumber} "));
            }
            Console.WriteLine( );
            Console.WriteLine("Successfully connected number(s):");
            orderedList.ForEach(i => Console.Write($"{i.OriginalMeetingNumber} "));

            int k = 0;
            
            foreach (var person in orderedList)
            {
                Console.WriteLine();
                Console.Write($"Index:{k},  needed meetings:");
                person.Combinations.ForEach(j => Console.Write($" {arrange[j]}"));
                k++; 
            }
           

        }
    }
}
