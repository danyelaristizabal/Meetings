using System;
using System.Collections.Generic;
using System.Linq; 
namespace Meetings
{
    internal class Person
    {
        public int MeetingNumber {get;  set;}
        public int OriginalIndex {get;   set;}
        public  int OriginalMeetingNumber { get; private set; }

        public List<int> Combinations {get; set;}

        internal Person(int _meetingNumber)
        {
            MeetingNumber = _meetingNumber;
            OriginalMeetingNumber = _meetingNumber;
            Combinations = new List<int>(); 
            for (int i = 0; i < MeetingNumber; i++)
            {
                Combinations.Add(1001);
            }
        }
        internal void Connect(Person person ) 
        {
            for (int i = 0; i < Combinations.Count; i++)
            {
                if(Combinations[i] == 1001) 
                {
                    Combinations[i] = person.OriginalIndex;
                    break; 
                }
            }
        }
        internal bool CheckFullyConnected() 
        {
            return !Combinations.Contains(1001); 
        }      

    }
}
