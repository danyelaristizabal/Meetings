using System;
using System.Linq;
using System.Collections.Generic; 
namespace Meetings
{
    internal class MeetingEngine
    {
        internal static List<Person> People { get; set; }
        internal static List<Person> ConnectedOnes { get; set; }
        internal static List<Person> KickedOut { get; set; }
        internal static List<int> peopleInt { get; set; }


        internal MeetingEngine(List<int> _peopleInt)
        {
            peopleInt = _peopleInt;
            ConnectedOnes = new List<Person>();
            KickedOut = new List<Person>();
        }

        internal void Meet()
        {
            ConvertToPersons();
            SaveIndexes();
            Console.WriteLine("Entry:");
            People.ForEach(i => Console.Write($"{ i.MeetingNumber} "));
            while (People.Count > 0)
            {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Run");
                    OrganicePersons();
                    Console.WriteLine("Entry Organiced:");
                    People.ForEach(i => Console.Write($"{ i.MeetingNumber} "));
                    if (CheckAreEnough())
                    {
                        ConnectThePersons();
                        Console.WriteLine();
                        Console.WriteLine("Connected:");
                        People.ForEach(i => Console.Write($"{ i.MeetingNumber} "));
                        Console.WriteLine();
                        ClearUpConnected();
                        Console.WriteLine("ClearedUp:");
                        People.ForEach(i => Console.Write($"{ i.MeetingNumber} "));
                        Console.WriteLine();
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Kicking out number: {People[0].MeetingNumber}");
                        KickOutThisNumber();
                    }
                        UpdateMeetingNumber();
                        Console.WriteLine("Update meeting number:");
                        People.ForEach(i => Console.Write($"{ i.MeetingNumber} "));
                        Console.WriteLine();
                }

        }
        internal List<Person> ReturnKickedOutNumbers()
        {
            return KickedOut;
        }

        internal List<Person> ReturnPeople()
        {
            return People;
        }
        internal List<Person> ReturnConnectedOnes()
        {
            return ConnectedOnes;
        }
        internal static void KickOutThisNumber()
        {

            KickedOut.Add(People[0]);
            People.Remove(People[0]);
        }
        internal static void ConvertToPersons()
        {
            People = new List<Person>();
            peopleInt.ForEach(i => People.Add(new Person(i)));

        }
        internal static void SaveIndexes()
        {
            for (int i = 0; i < People.Count; i++)
            {
                People[i].OriginalIndex = i;
            }
        }
        internal static void OrganicePersons()
        {
            var ordered = People.OrderByDescending(i => i.MeetingNumber);
            List<Person> orderedList = ordered.ToList();
            People = orderedList;
        }

        internal static bool CheckAreEnough()
        {
            return People.Count - 1 >= People[0].MeetingNumber;
        }
        internal static List<Person> Connect(Person person, Person person2)
        {
            List<Person> twoconnected = new List<Person>();
            person.Connect(person2);
            person2.Connect(person);
            twoconnected.Add(person);
            twoconnected.Add(person2);
            return twoconnected;
        }
        internal static List<Person> ConnectWithOne(List<Person> listofpersons , Person first)
        {
            foreach (var person in listofpersons)
            {
                person.Connect(first);
                first.Connect(person); 
            }
            listofpersons.Insert(0, first); 
            return listofpersons;
        }

        internal static void ConnectThePersons() 
        {
            var first = People[0];
            var exceptFirstList = new List<Person>(); 

            for (int i = 0; i <= first.MeetingNumber; i++)
            {
                if (i != 0) 
                {
                    exceptFirstList.Add(People[i]);
                } 
            }

            for (int i = 0; i <= first.MeetingNumber -1 ; i++)
            {
                People.Remove(exceptFirstList[i]); 
            }

            People.Remove(first);

            exceptFirstList = ConnectWithOne(exceptFirstList, first);

            for (int i = first.MeetingNumber ; i >= 0; i--)
            {
                People.Insert(0, exceptFirstList[i]);
            }
        }
        internal static void ClearUpConnected()
        {
            var fullyConnected = from i in People where i.CheckFullyConnected() select i;
            var toList = fullyConnected.ToList();
            toList.ForEach(i => ConnectedOnes.Add(i));
            toList.ForEach(i => People.Remove(i));
        }

        internal static void UpdateMeetingNumber() 
        {

            foreach (var person in People)
            {
                if(person.Combinations.Count(i => i == 1001) != person.OriginalMeetingNumber) 
                {
                    person.MeetingNumber =
                     person.OriginalMeetingNumber - person.Combinations.Count(i => i != 1001);
                }
            }
        }
    }
}
