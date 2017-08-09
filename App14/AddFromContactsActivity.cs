using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;

namespace App14
{
    [Activity(Label = "AddFromContactsActivity")]
    public class AddFromContactsActivity : ListActivity
    {
        List<string> contactList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var uri = ContactsContract.Contacts.ContentUri;

            string[] projection = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.CommonDataKinds.Phone.Number };

            var cursor = ManagedQuery(uri, projection, null, null, null);

            contactList = new List<string>();

            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(cursor.GetString(
                            cursor.GetColumnIndex(projection[1])));
                } while (cursor.MoveToNext());
            }

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ContactItemView, contactList);
            
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            contactList[position];
        }
    }
}