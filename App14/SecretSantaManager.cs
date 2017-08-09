using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace App14
{
    class SecretSantaManager
    {
        
        string accountSid = "ACe434dc8f3997138610ed0d0bd791514b";
        string authToken = "27a520804a4dd915e8fd505437ab3c13";
        //public List<Person> person;


                
        public SecretSantaManager()
        {
            //person = new List<Person>();
            TwilioClient.Init(accountSid, authToken);
        }

        //public bool AddPerson(string name, string number)
        //{
        //    if (ContainsNumber(number) == true)
        //    {
        //        return false;
        //    }
        //    Person p1 = new Person(name, number);
        //    ContainsNumber(number);
        //    person.Add(p1);
        //    return true;
        //}
        public bool ContainsNumber(List<Person>p, string num)
        {
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Number == num)
                {
                    return true;
                }
            }
            return false;
        }
        public void SendNotificationToSquad(List<Person> p)
        {
            string x = "+";
            for (int i = 0; i < p.Count; i++)
            {
                if(p[i].Number.Length < 11)
                {
                    x = x + "1";
                }
                var to = new PhoneNumber(x + p[i].Number);
                var message = MessageResource.Create(
                 to,
                 from: new PhoneNumber("+16319047273"),
                 body: "This is the ship that made the Kessel Run in fourteen parsecs?");

                Console.WriteLine(message.Sid);
            }
        }
        public void SendOutSantas(List<Person> p)
        {
            List<Person> randomizedList = Randomize<Person>(p);
            for(int i = 0;i<randomizedList.Count; i++)
            {
                //This integer is for the last person in the list to be the first person in the lists santa
                //befor the loop hits the last person the x integer assigns the person at index i+1 to i
                int x = i + 1;
                if(i == randomizedList.Count - 1)
                {
                    x = 0;
                }
                //PhoneNumber is a data type provided by twilio
                var to = new PhoneNumber(randomizedList[i].Number);
                var message = MessageResource.Create(
                 to,
                 from: new PhoneNumber("+16319047273"),
                 body: randomizedList[i].Name + ", you are " + randomizedList[x].Name + "'s secret santa!");
            }
        }
        public static List<T> Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            return randomizedList;
        }
        //public void GenerateRandomPeopleAndSend(List<Person> p)
        //{
        //    List<Person> randomizedList = new List<Person>();
        //    Random randomizer = new Random();
        //    int rIndex = 0;

        //    while (p.Count > 0)
        //    {
        //        rIndex = randomizer.Next(0, p.Count);
        //        randomizedList.Add(p[rIndex]);
        //        p.RemoveAt(rIndex);
        //    }

        //    for (int i = 0; i < randomizedList.Count - 1; i++)
        //    {
        //        Person num1;
        //        Person num2;
        //        num1 = randomizedList[i];
        //        num2 = randomizedList[i + 1];
                
        //        //message
        //        if (i == randomizedList.Count - 2)
        //        {
        //            num1 = randomizedList[0];
        //            //message
        //        }
        //    }
        //}
    }
}