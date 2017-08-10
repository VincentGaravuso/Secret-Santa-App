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
            this.Finish();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            string number = numberEditText.Text;
            string name = nameEditText.Text;
            
            if (CheckForError(name, number) == true)
            {
                Person p = new Person(name, number);
                Intent intent = new Intent();
                intent.PutExtra("name", p.Name);
                intent.PutExtra("number", p.Number);
                SetResult(Result.Ok, intent);
                Android.Widget.Toast.MakeText(this, "Added", ToastLength.Short).Show();
                this.Finish();
            }
            else
            {
                SetResult(Result.Canceled);
            }
            
            
            


            // Jesse code
            //Person p = new Person(name,number);
            //Intent intent = new Intent();
            //intent.PutExtra("person", JsonConvert.SerializeObject(p));
            
            //Add a save button to main page and ask user what they want to save the preset as
            //var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            //var contactEdit = localContacts.Edit();
            //contactEdit.PutString("Name", name);
            //contactEdit.PutString("Number", number);
            //contactEdit.Commit();

        }
        private bool CheckForError(string name, string num)
        {

            //if the name and number field is filled out and the number is of standard length, continue
            if (name != null && name != "" && num != null && num != "")
            {
                if (num.Length == 10)
                {
                    return true;
                }
                else if(num.Length < 10 || num.Length > 10)
                {

                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Error");
                    alert.SetMessage("You must enter a valid 10 digit number, including area code");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // 
                    });

                    alert.Show();
                    return false;
                }
            }
            //if the name or number fields are blank, alert user
            else if(name == null || name == "")
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage("You must enter a name.");
                alert.SetButton("OK", (c, ev) =>
                {
                    // 
                });

                alert.Show();
                return false;
            }
            else if(num != null || num == "")
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage("You must enter a number.");
                alert.SetButton("OK", (c, ev) =>
                {
                    // 

                });
                alert.Show();
                return false;
            }
                return false;
        }
    }
}