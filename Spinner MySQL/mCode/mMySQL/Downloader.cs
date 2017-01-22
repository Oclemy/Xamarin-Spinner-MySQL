using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Console = System.Console;
using Exception = System.Exception;
using Object = Java.Lang.Object;
using String = System.String;

namespace Spinner_MySQL.mCode.mMySQL
{
    class Downloader : AsyncTask
    {
        private Context c;
        private string urlAddress;
        private Spinner sp;

        private ProgressDialog pd;

        public Downloader(Context c, string urlAddress, Spinner sp)
        {
            this.c = c;
            this.urlAddress = urlAddress;
            this.sp = sp;
        }


        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pd=new ProgressDialog(c);
            pd.SetTitle("Fetch Data");
            pd.SetMessage("Fetching data...Please wait");
            pd.Show();

        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return this.DownloadData();
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);

            pd.Dismiss();

            if (result == null)
            {
                Toast.MakeText(c,"Unsuccessful,Null returned",ToastLength.Short).Show();

            }
            else
            {
                //DATA PARER TO PASS OUR DATA
                DataParser parser=new DataParser(c,sp,result.ToString());
                parser.Execute();
            }


        }

        //DOWNLOAD DATA
        private String DownloadData()
        {
            HttpURLConnection con = Connector.Connect(urlAddress);
            if (con == null)
            {
                return null;
            }

            try
            {
                
                Stream s=new BufferedStream(con.InputStream);
                BufferedReader br=new BufferedReader(new InputStreamReader(s));

                String line = null;
                StringBuffer response=new StringBuffer();

                while ((line=br.ReadLine()) != null)
                {
                    response.Append(line + "\n");
                }

                br.Close();
                s.Close();

                return response.ToString();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return null;
        }
    }
}