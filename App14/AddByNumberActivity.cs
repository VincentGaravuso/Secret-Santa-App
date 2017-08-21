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
using Newtonsoft.Json;

namespace App14
{
    [Activity(Label = "AddByNumberActivity")]
    public class AddByNumberActivity : Activity
    {
        EditText nameEditText;
        EditText numberEditText;
        SecretSantaManager sm = new SecretSantaManager();
        List<Person> person = new List<Person>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddByNumber);

            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            cancelButton.Click += CancelButton_Click;
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += AddButton_Click;



        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            this.Finish();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            string number = numberEditText.Text;
            string name = nameEditText.Text;

            int check = sm.CheckForError(name, number);
            if (check == 0)
            {
                Person p = new Person(name, number);
                Intent intent = new Intent();
                intent.PutExtra("name", p.Name);
                intent.PutExtra("number", p.Number);
                SetResult(Result.Ok, intent);
                Android.Widget.Toast.MakeText(this, "Added!", ToastLength.Short).Show();
                this.Finish();
            }
            else if (check == 1)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage("You must enter a name.");
                alert.SetButton("OK", (c, ev) =>
                {
                });

                alert.Show();
            }
            else if (check == 2)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage("You must enter a number.");
                alert.SetButton("OK", (c, ev) =>
                {
                });
                alert.Show();
            }
            else if (check == 3)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage("You must enter a valid 10 digit number, including area code");
                alert.SetButton("OK", (c, ev) =>
                {
                });

                alert.Show();
            }

            //Add a save button to main page and ask user what they want to save the preset as
            //var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            //var contactEdit = localContacts.Edit();
            //contactEdit.PutString("Name", name);
            //contactEdit.PutString("Number", number);
            //contactEdit.Commit();

        }
    }
}