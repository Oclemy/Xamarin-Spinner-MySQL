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
using Java.Net;


namespace Spinner_MySQL.mCode.mMySQL
{
    class Connector
    {
        public static HttpURLConnection Connect(String urlAddress)
        {
            try
            {
                URL url=new URL(urlAddress);
                HttpURLConnection con = (HttpURLConnection) url.OpenConnection();

                //PROPERTIES
                con.RequestMethod = "GET";
                con.ConnectTimeout = 20000;
                con.ReadTimeout = 20000;
                con.DoInput = true;

                return con;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}