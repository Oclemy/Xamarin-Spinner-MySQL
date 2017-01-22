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
using Java.Lang;
using Org.Json;
using Exception = System.Exception;
using Object = Java.Lang.Object;
using String = System.String;

namespace Spinner_MySQL.mCode.mMySQL
{
    class DataParser : AsyncTask
    {
        Context c;
        private Spinner sp;
        private String jsonData;
        JavaList<string> spacecrafts=new JavaList<string>(); 
        private ProgressDialog pd;

        public DataParser(Context c, Spinner sp, string jsonData)
        {
            this.c = c;
            this.sp = sp;
            this.jsonData = jsonData;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            pd = new ProgressDialog(c);
            pd.SetTitle("Parse Data");
            pd.SetMessage("Parsing data...Please wait");
            pd.Show();

        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return this.ParseData();
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);

            pd.Dismiss();

            if (Integer.ParseInt(result.ToString()) == 0)
            {
                Toast.MakeText(c,"Unable To Parse",ToastLength.Short).Show();
            }
            else
            {
                ArrayAdapter<string> adapter=new ArrayAdapter<string>(c,Android.Resource.Layout.SimpleListItem1,spacecrafts);
                sp.Adapter = adapter;

                sp.ItemSelected += sp_ItemSelected;
            }
        }

        void sp_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Toast.MakeText(c, spacecrafts[e.Position], ToastLength.Short).Show();
            
        }

        private int ParseData()
        {
            try
            {
                JSONArray ja=new JSONArray(jsonData);
                JSONObject jo = null;

                spacecrafts.Clear();

                for (int i = 0; i < ja.Length(); i++)
                {
                    jo = ja.GetJSONObject(i);

                    String name = jo.GetString("name");

                    spacecrafts.Add(name);

                }

                return 1;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }
    }
}













