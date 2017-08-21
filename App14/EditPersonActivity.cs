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

namespace App14
{
    [Activity(Label = "EditPersonActivity")]
    public class EditPersonActivity : Activity
    {
        string name;
        string number;
        int userChoice;
        EditText nameEditText;
        EditText numberEditText;
        SecretSantaManager sm = new SecretSantaManager();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditPerson);
            name = Intent.GetStringExtra("name");
            number = Intent.GetStringExtra("number");
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            nameEditText.Text = name;
            numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
            numberEditText.Text = number;


            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            cancelButton.Click += CancelButton_Click;
            Button updateButton = FindViewById<Button>(Resource.Id.updateButton);
            updateButton.Click += UpdateButton_Click;
            Button deleteButton = FindViewById<Button>(Resource.Id.deleteButton);
            deleteButton.Click += DeleteButton_Click;
        }

            private void DeleteButton_Click(object sender, EventArgs e)
        {
            userChoice = 1;
            Intent intent = new Intent();
            intent.PutExtra("number", number);
            intent.PutExtra("userChoice", userChoice);
            SetResult(Result.Ok, intent);
            Android.Widget.Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
            this.Finish();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string updatedName = nameEditText.Text;
            string updatedNum = numberEditText.Text;
            int check = sm.CheckForError(updatedName, updatedNum);
            if (check == 0)
            {
                userChoice = 2;
                Intent intent = new Intent();
                intent.PutExtra("name", updatedName);
                intent.PutExtra("number", updatedNum);
                intent.PutExtra("userChoice", userChoice);
                SetResult(Result.Ok, intent);
                Android.Widget.Toast.MakeText(this, "Updated!", ToastLength.Short).Show();
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
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            this.Finish();
        }

    }
}