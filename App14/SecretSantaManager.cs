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
using System.Threading.Tasks;

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

        //returns true if the number being passed in already exists in the list
        public bool ContainsNumber(List<Person> p, string num)
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
        //returns the int number of the position of a person in the list that matches with the number passed in
        public int SearchList(List<Person> p, string num)
        {
            string num2 = "1" + num;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Number == num || p[i].Number == num2)
                {
                    return i;
                }
            }
            return -1;
        }
        public int CheckForError(string name, string num)
        {
            int allValid = 0;
            int nameError = 1;
            int numIsEmptyError = 2;
            int numIsInvalid = 3;
            

            //if the name and number field is filled out and the number is of standard length, continue
            if (name != null && name != "" && num != null && num != "")
            {
                if (num.Length == 10)
                {
                    return allValid;
                }
                else if (num.Length < 10 || num.Length > 10)
                {
                    return numIsInvalid;
                }
            }
            //if the name or number fields are blank, alert user
            else if (name == null || name == "")
            {
                return nameError;
            }
            else if (num != null || num == "")
            {
                return numIsEmptyError;
            }
            return numIsEmptyError;
        }
    //does nothing so far
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
        //Sends out messages to everyone in the list being passed in
        public async Task<bool> SendOutSantas(List<Person> p)
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

                    //if the number contains a country code this removes it
                    for (int j = 0; j < p.Count; j++)
                    {
                        if (randomizedList[j].Number.Length == 11)
                        {
                            string num = randomizedList[j].Number;
                            num.Substring(1);
                            randomizedList[j].Number = num;
                        }
                    }
                    var to = new PhoneNumber(+1 + randomizedList[i].Number);

                    await Task.Run(() =>
                    {
                        var message = MessageResource.Create(
       to,
       from: new PhoneNumber("+16319047273"),
       body: randomizedList[i].Name + ", you are " + randomizedList[x].Name + "'s secret santa!");
                    });


                }
                return true;
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