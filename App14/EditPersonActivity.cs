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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditPerson);
            name = Intent.GetStringExtra("name");
            number = Intent.GetStringExtra("number");
            EditText nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            nameEditText.Text = name;
            EditText numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
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
            userChoice = 2;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}