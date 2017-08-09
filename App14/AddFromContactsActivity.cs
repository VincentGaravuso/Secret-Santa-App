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
        List<string> phoneList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;

            string[] projection = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.CommonDataKinds.Phone.Number };

            var cursor = ManagedQuery(uri, projection, null, null, null);

            contactList = new List<string>();
            phoneList = new List<string>();

            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(cursor.GetString(
                            cursor.GetColumnIndex(projection[1])));
                    phoneList.Add(cursor.GetString(
                         cursor.GetColumnIndex(projection[2])));
                } while (cursor.MoveToNext());
            }

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ContactItemView, contactList);

        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Intent intent = new Intent();
            intent.PutExtra("name", contactList[position]);
            intent.PutExtra("number", phoneList[position]);
            SetResult(Result.Ok, intent);
            //Android.Widget.Toast.MakeText(this, "Added", ToastLength.Short).Show();
            this.Finish();
        }
    }
}