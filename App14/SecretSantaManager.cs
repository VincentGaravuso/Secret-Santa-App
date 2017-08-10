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
        public bool ContainsNumber(List<Person>p, string num)
        {
            string num2 = "1" + num;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Number == num || p[i].Number == num2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool SendNotificationToSquad(List<Person> p)
        {
            if (p.Count > 0)
            {

                string x = "+";
                for (int i = 0; i < p.Count; i++)
                {
                    if (p[i].Number.Length < 11)
                    {
                        x = x + "1";
                    }
                    var to = new PhoneNumber(x + p[i].Number);
                    var message = MessageResource.Create(
                     to,
                     from: new PhoneNumber("+16319047273"),
                     body: "This is the ship that made the Kessel Run in fourteen parsecs?");
                    return true;
                }
            }
            return false;
        }
        public bool SendOutSantas(List<Person> p)
        {
            if (p.Count > 0)
            {
                List<Person> randomizedList = Randomize<Person>(p);
                for (int i = 0; i < randomizedList.Count; i++)
                {
                    //This integer is for the last person in the list to be the first person in the lists santa
                    //befor the loop hits the last person the x integer assigns the person at index i+1 to i
                    int x = i + 1;
                    if (i == randomizedList.Count - 1)
                    { x = 0; }

                    //string y = "+";
                    //for (int j = 0; j < p.Count; j++)
                    //{
                    //    if (p[j].Number.Length < 11)
                    //    {
                    //        y = y + "1";
                    //    }
                    //}
                    var to = new PhoneNumber("+1" + randomizedList[i].Number);
                    var message = MessageResource.Create(
                     to,
                     from: new PhoneNumber("+16319047273"),
                     body: randomizedList[i].Name + ", you are " + randomizedList[x].Name + "'s secret santa!");
                    if(i == randomizedList.Count-1)
                    {
                        return true;
                    }
                }
            }
            return false;
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
    }
}