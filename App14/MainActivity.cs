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
        public int resultCode = 100;
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
        }

        private void Generate_Click(object sender, System.EventArgs e)
        {
            sm.SendOutSantas(p);
        }
        private void AddFromContacts_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AddFromContactsActivity));
        }
        private void AddByNum_Click(object sender, System.EventArgs e)
        {
            StartActivityForResult(typeof(AddByNumberActivity), resultCode);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok && requestCode == 100)
            {
                count++;
                string name = data.GetStringExtra("name");
                string number = data.GetStringExtra("number");
                Person addedByNum = new Person(name, number);
                if (sm.ContainsNumber(p,number) == false)
                {
                    //test
                    p.Add(addedByNum);
                    //take out display of number later
                    ((ArrayAdapter)lv.Adapter).Add(count + ". " + name.PadRight(20 - name.Length) + number);
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

        //private void LoadList_Click(object sender, System.EventArgs e)
        //{
        //     allow user to load a previsouly saved list of people
        //     StartActivity(typeof(LoadListActivity));
        //}

    }
}

