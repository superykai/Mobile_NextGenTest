using System;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Widget;
using Android_app1;
using Java.IO;
using Newtonsoft.Json;
using Console = System.Console;
using Context = System.Runtime.Remoting.Contexts.Context;
using Encoding = System.Text.Encoding;

namespace firstAndroidApp
{
    [Activity(Label = "Add New Test")]
    public class AddActivity: Activity
    {
        //public static readonly int PickImageId = 1000;
        //private ImageView _imageView;
        private Android.Net.Uri _uri;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "addNew" layout resource 
            SetContentView(Resource.Layout.addNew);

            TextView addrequId = FindViewById<TextView>(Resource.Id.AddTxtReqNum);
            TextView addmodelyear = FindViewById<TextView>(Resource.Id.AddTxtModelYear);
            TextView partNum = FindViewById<TextView>(Resource.Id.AddTxtPartNum);
            TextView productCode = FindViewById<TextView>(Resource.Id.AddTxtProductCode);
            TextView vehProgram = FindViewById<TextView>(Resource.Id.AddTxtvehProgram);
            TextView vehArchitecture = FindViewById<TextView>(Resource.Id.AddTxtvehArchitecture);
            TextView vehMake = FindViewById<TextView>(Resource.Id.AddTxtvehMake);
            TextView upc = FindViewById<TextView>(Resource.Id.AddTxtupc);
            TextView vppsLevel1 = FindViewById<TextView>(Resource.Id.AddTxtvppsLevel1); 
            TextView functionalArea = FindViewById<TextView>(Resource.Id.AddTxtfunctionalArea);
            TextView createdByGmin = FindViewById<TextView>(Resource.Id.AddTxtcreatedByGmin);
            TextView createdDate = FindViewById<TextView>(Resource.Id.AddTxtcreatedDate);
            TextView updatedDate = FindViewById<TextView>(Resource.Id.AddTxtupdatedDate);

            Button button = FindViewById<Button>(Resource.Id.myButton);
            
            //_imageView.SetImageResource(Resource.Drawable.allFood);

            button.Click += (sender, e) =>
            {

                string url = "https://test-request-service.apps.pcfepgwi.gm.com/testRequests";

                NewTest test = new NewTest();
                test.requestNum = int.Parse(addrequId.Text);
                test.modelYear = addmodelyear.Text;
                test.partNum = partNum.Text;
                test.productCode = productCode.Text;
                test.vehProgram = vehProgram.Text;
                test.vehArchitecture = vehArchitecture.Text;
                test.vehMake = vehMake.Text;
                test.upc = upc.Text;
                test.vppsLevel1 = vppsLevel1.Text;
                test.functionalArea = functionalArea.Text;
                test.createdByGmin = createdByGmin.Text;
                test.createdDate = createdDate.Text;
                test.updatedDate = updatedDate.Text;

                //string data = JsonConvert.SerializeObject(test);
                //byte[] bytes;

                //Bitmap bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(ContentResolver, _uri);
                //using (var stream = new MemoryStream())
                //{

                //    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                //    bytes = stream.ToArray();
                //}
                ////Binary bit= new Binary();
                //picture bit = new picture();
                //bit.Bytes = bytes;

                //Category newcCategory = new Category();
                //newcCategory.CategoryName = addName.Text;
                //newcCategory.Description = addDescription.Text;
                //newcCategory.Picture = bit;

                ////string content = JsonConvert.SerializeObject(newcCategory);
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Category)); 
                //MemoryStream mem = new MemoryStream();
                //ser.WriteObject(mem, newcCategory);
                //string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NewTest));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, test);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                using (WebClient client = new WebClient())
                {
                    try
                    {
                        client.Headers["Content-type"] = "application/json";
                        //client.Encoding = Encoding.UTF8;
                        client.UploadString(url, "POST", data);
                        Toast.MakeText(ApplicationContext,"Your new test successfully added!",ToastLength.Long).Show();
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("Error: {0}", ex.Message);
                    }
                }
            };
            
        }

        //private void ButtonOnClick(object sender, EventArgs eventArgs)
        //{
        //    Intent = new Intent();
        //    Intent.SetType("image/*");
        //    Intent.SetAction(Intent.ActionGetContent);
        //    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        //}

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
        //    {
        //        _uri = data.Data;
        //        _imageView.SetImageURI(_uri);
        //    }
        //}
    }

    public class NewTest
    {
        public int requestNum { get; set; }
        public string modelYear { get; set; }
        public string partNum { get; set; }
        public string productCode { get; set; }
        public string vehProgram { get; set; }
        public string vehArchitecture { get; set; }
        public string vehMake { get; set; }
        public string upc { get; set; }
        public string vppsLevel1 { get; set; }
        public string functionalArea { get; set; }
        public string createdByGmin { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
   
}
