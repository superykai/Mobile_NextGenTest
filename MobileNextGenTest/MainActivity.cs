using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using Android.Graphics;
using System.Linq;
using Android_app1;


namespace firstAndroidApp
{
    [Activity(Label = "MobileNextGenTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource 
            SetContentView(Resource.Layout.Main);

            // Get the latitude/longitude EditBox and button resources:
            EditText requestnum = FindViewById<EditText>(Resource.Id.requestText);
            Button button = FindViewById<Button>(Resource.Id.getGeoButton);
            Button button1 = FindViewById<Button>(Resource.Id.button1);

            // When the user clicks the button ...
            button.Click += async (sender, e) => {

               
                //string url = "http://10.0.2.2:9002/SampleServices/Service/REST/Category/"+ Country.Text;
                string url =
                    "https://test-request-service.apps.pcfepgwi.gm.com/testRequests/" + requestnum.Text;


                JsonValue json = await FetchGEOAsync(url);
                ParseAndDisplay(json);
            };

            button1.Click +=  delegate {
                StartActivity(typeof(AddActivity));
            };
        }

        // Gets weather data from the passed URL.
        private async Task<JsonValue> FetchGEOAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            WebResponse response =  request.GetResponse();
            Stream stream = response.GetResponseStream();
            JsonValue jsonDoc = JsonObject.Load(stream);
            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
            return jsonDoc;

            // Send the request to the server and wait for the response:
            //using (WebResponse response = await request.GetResponseAsync())
            //{
            //    // Get a stream representation of the HTTP web response:
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        // Use this stream to build a JSON document object:
            //        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            //        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //        // Return the JSON document:
            //        return jsonDoc;
            //    }
            //}
        }

        // Parses the weather data, then writes temperature, humidity, conditions, and 
        // location to the screen.
        private void ParseAndDisplay(JsonValue json)
        {
            // Get the weather reporting fields from the layout resource: 
            TextView ModelYear = FindViewById<TextView>(Resource.Id.resultModelYearText);
            TextView PartNumber = FindViewById<TextView>(Resource.Id.resultpartText);
            TextView ProductCode = FindViewById<TextView>(Resource.Id.resultproductCodeText);
            TextView VehProg = FindViewById<TextView>(Resource.Id.resultvehProgText);
            TextView VehArchit = FindViewById<TextView>(Resource.Id.resultvehArchitText);
            TextView VehMake = FindViewById<TextView>(Resource.Id.resultvehmakeText);
            TextView upc = FindViewById<TextView>(Resource.Id.resultupcText);
            TextView vppsLevel1 = FindViewById<TextView>(Resource.Id.resultvppsLevel1Text);
            TextView functionalArea = FindViewById<TextView>(Resource.Id.resultfunctionalAreaText);
            TextView createdByGmin = FindViewById<TextView>(Resource.Id.resultcreatedByGminText);
            TextView createdDate = FindViewById<TextView>(Resource.Id.resultcreatedDateText);
            TextView updatedDate = FindViewById<TextView>(Resource.Id.resultupdatedDateText);
            //ImageView image = FindViewById<ImageView>(Resource.Id.imageView1);

            // Extract the array of name/value results for the field name "weatherObservation":
            // Note that there is no exception handling for when this field is not found.
            JsonValue mResults = json;

            
            //string mm = json["Picture"]["Bytes"].ToString();
            //Byte[] picData = mm.Trim('[', ']')
            //  .Split(',')
            //  .Select(x => byte.Parse(x))
            //  .ToArray();

            //int offSet = 0;
            //if (picData[0].ToString().Equals("21"))
            //    offSet = 78;
            //Bitmap bmp = BitmapFactory.DecodeByteArray(picData, offSet, picData.Length - offSet);

            //image.SetImageBitmap(Bitmap.CreateScaledBitmap(bmp, image.Width, image.Height, false));


            // Draw your image
            //Bitmap bmp = Bitmap.CreateBitmap(200, 200, Bitmap.Config.Argb8888);
            //image.SetImageBitmap(Bitmap.CreateScaledBitmap(bmp, image.Width, image.Height, false));


            // Extract the "stationName" (location string) and write it to the location TextBox:
            ModelYear.Text = mResults["modelYear"].ToString();
            PartNumber.Text = mResults["partNum"];
            ProductCode.Text = mResults["productCode"];
            VehProg.Text = mResults["vehProgram"];
            VehArchit.Text = mResults["vehArchitecture"];
            VehMake.Text = mResults["vehMake"];
            upc.Text = mResults["upc"];
            vppsLevel1.Text = mResults["vppsLevel1"];
            functionalArea.Text = mResults["functionalArea"];
            createdByGmin.Text = mResults["createdByGmin"];
            createdDate.Text = mResults["createdDate"];
            updatedDate.Text = mResults["updatedDate"];
            //capitol.Text = mResults["capital"];


        }
    }
}

