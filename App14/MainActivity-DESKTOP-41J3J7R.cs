using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;

namespace App14
{
    [Activity(Label = "App14", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public int resultCodeAddByNumber = 100;
        public int resultCodeAddByContact = 200;
        public int resultCodeEditPerson = 300;
        Android.App.ProgressDialog progress;
        List<Person> p = new List<Person>();
        SecretSantaManager sm = new SecretSantaManager();
        ArrayAdapter ad;
        ListView lv;
        int count = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button addByNum = FindViewById<Button>(Resource.Id.addByNumButton);
            addByNum.Click += AddByNum_Click;

            Button addFromContacts = FindViewById<Button>(Resource.Id.addContactsButton);
            addFromContacts.Click += AddFromContacts_Click;

            Button generate = FindViewById<Button>(Resource.Id.generateSantaButton);
            generate.Click += Generate_Click;

            lv = FindViewById<ListView>(Resource.Id.personListView);
            ad = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, p);
            lv.Adapter = ad;

            lv.ItemClick += Lv_ItemClick;
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string name = p[e.Position].Name;
            string number = p[e.Position].Number;

            var editPerson = new Intent(this, typeof(EditPersonActivity));
            editPerson.PutExtra("name", name);
            editPerson.PutExtra("number", number);
            StartActivityForResult(editPerson, resultCodeEditPerson);
        }

        private async void Generate_Click(object sender, System.EventArgs e)
        {

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);
            progress.Show();

            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            if (await sm.SendOutSantas(p) == true)
            {


                alert.SetTitle("Complete");
                alert.SetMessage("The Secret Santas have been sent! Don't tell anyone who you have!");
                alert.SetButton("OK", (c, ev) =>
                {
                    // 
                });

                alert.Show();
            }
            else
            {
                alert.SetTitle("Error");
                alert.SetMessage("Please enter at least 1 number!");
                alert.SetButton("OK", (c, ev) =>
                {
                    // 
                });

                alert.Show();
            }
            progress.Hide();

        }
        private void AddFromContacts_Click(object sender, System.EventArgs e)
        {
            StartActivityForResult(typeof(AddFromContactsActivity), resultCodeAddByContact);
        }
        private void AddByNum_Click(object sender, System.EventArgs e)
        {
            StartActivityForResult(typeof(AddByNumberActivity), resultCodeAddByNumber);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == resultCodeAddByNumber || requestCode == resultCodeAddByContact)
            {
                if (resultCode == Result.Ok)
                {

                    string name = data.GetStringExtra("name");
                    string number = data.GetStringExtra("number");
                    Person addedByNum = new Person(name, number);
                    if (sm.ContainsNumber(p, number) == false)
                    {
                        count++;
                        p.Add(addedByNum);
                        ((ArrayAdapter)lv.Adapter).Add(count + ". " + name);
                    }
                    else
                    {
                        AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Error");
                        alert.SetMessage("This number has already been added!");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            // 
                        });

                        alert.Show();
                    }


                    //jesse code
                    //Person p2 = JsonConvert.DeserializeObject<Person>(data.GetStringExtra("person"));
                    //p.Add(p2);
                    //((ArrayAdapter)lv.Adapter).Add(p2);
                }
            }
            else if (requestCode == resultCodeEditPerson)
            {
                if (resultCode == Result.Ok)
                {

                    string name = data.GetStringExtra("name");
                    string number = data.GetStringExtra("number");
                    int userChoice = data.GetIntExtra("choice", 0);
                    //userChoice defines what the user intended to do with the data:
                    //1 = delete
                    //2 = update
                    if (userChoice == 0)
                    {
                    }
                    else if (userChoice == 1)
                    {
                        int pos = sm.Delete(p, number);
                        if (pos > -1)
                        {
                            p.RemoveAt(pos);
                            refreshList();
                            //Create loop to add all people in the p list to the listview (use array adapter)
                            //Esentially just reloading the listview
                        }
                    }
                    else if (userChoice == 2)
                    {
                        //Figure out how to update a users information
                    }
                    {
                    }


                }
            }

        }
        public void refreshList()
        {
            for(int i = 0; i < p.Count; i++)
            {
                ((ArrayAdapter)lv.Adapter).Add(i + ". " + p[i].Name);
            }
        }

        //private void LoadList_Click(object sender, System.EventArgs e)
        //{
        //     allow user to load a previsouly saved list of people
        //     StartActivity(typeof(LoadListActivity));
        //}

    }
}