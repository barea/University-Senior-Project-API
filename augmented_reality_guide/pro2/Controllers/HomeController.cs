/*
 *  ********** SVU **********
 ********** Barea_27786 **********
 *********** Home Controller **********
 *********** displaying Web pages for Object And Exhibition 
 Operation [create, Update, Deledte, Reade] Generate QR and Dtata Encryption **********
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using WebMatrix.WebData;
using System.Web.Security;
namespace pro2.Controllers
{
    public class HomeController : Controller
    {
        //Links for master page
        public ActionResult Links()
        {
           if (!String.IsNullOrEmpty(WebSecurity.CurrentUserName))
            {
                string username = WebSecurity.CurrentUserName;
                string[] role = Roles.GetRolesForUser(username);
                if (role[0] == "Administrator")
                {
                    return PartialView("AdminpartlView");
                }
                else
                {
                    return PartialView("nullPart");
                }
               
            }
            else
            {
                return PartialView("nullPart");
           }
        }
        //Home page Exhibition List
        public ActionResult Index()
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "exhibition", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

           return View();
        }

        //Object Lists
        public ActionResult IndexObject(int id)
        {
            ViewBag.ActUrl = "/api/Objct/GetExhById/" + id.ToString();
            ViewBag.ExId = id;
            return View();
        }

        //Dowmload Android Application
        public ActionResult Download()
        {
            try
            {
                var file = System.IO.File.OpenRead(Server.MapPath("/android/PR2.apk"));
                return File(file, "application/vnd.android.package-archive", "PR2.apk");
            }
           catch
            {
                throw new HttpException(404, "Couldn't find file");
            }
           
        }

        //Create Objects
        [Authorize(Roles = "Administrator")]
        public ActionResult Objct(int id)
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "objct", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            ViewBag.ActUrl = "/api/Objct/GetExhById/"+id.ToString();
            ViewBag.ExId = id;

            return View();
        }

        //Delete and Edit Object
        [Authorize(Roles = "Administrator")]
        public ActionResult ManageObject(int Id)
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "objct", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            ViewBag.ObjId = Id;
            return View();
        }

        //create Exhibition
        [Authorize(Roles = "Administrator")]
        public ActionResult Exhibition()
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "exhibition", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }

        //Edit Exhibition Name
        [Authorize(Roles = "Administrator")]
        public ActionResult EditExhibition(int Id)
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "exhibition", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            ViewBag.ExhId = Id;
            return View();
        }

        //Url encrypt
        [Authorize(Roles = "Administrator")]
        public string cryptUrl(string url)
        {
            Hashtable SymbolTable = new Hashtable();
            SymbolTable.Add("a", "N");
            SymbolTable.Add("b", "1");
            SymbolTable.Add("c", "0");
            SymbolTable.Add("d", "O");
            SymbolTable.Add("e", "2");
            SymbolTable.Add("f", "4");
            SymbolTable.Add("g", "T");
            SymbolTable.Add("h", "R");
            SymbolTable.Add("i", "B");
            SymbolTable.Add("j", "3");
            SymbolTable.Add("k", "A");
            SymbolTable.Add("l", "/");
            SymbolTable.Add("m", "E");
            SymbolTable.Add("n", "C");
            SymbolTable.Add("o", "F");
            SymbolTable.Add("p", "5");
            SymbolTable.Add("q", ":");
            SymbolTable.Add("r", "U");
            SymbolTable.Add("s", "X");
            SymbolTable.Add("t", "6");
            SymbolTable.Add("u", "D");
            SymbolTable.Add("v", "Q");
            SymbolTable.Add("w", "7");
            SymbolTable.Add("x", "G");
            SymbolTable.Add("y", "8");
            SymbolTable.Add("z", "Y");
            SymbolTable.Add("0", "9");
            SymbolTable.Add("1", "L");
            SymbolTable.Add("2", "K");
            SymbolTable.Add("3", "H");
            SymbolTable.Add("4", "V");
            SymbolTable.Add("5", "$");
            SymbolTable.Add("6", "J");
            SymbolTable.Add("7", "M");
            SymbolTable.Add("8", "P");
            SymbolTable.Add("9", "Z");
            SymbolTable.Add(":", "S");
            SymbolTable.Add("/", "W");
            SymbolTable.Add(".", "I");

            string[] spl = new string[url.Length];
            string character;
            string ciphertext = "";

            for(int i = 0; i < url.Length; i++)
            {
                character = url.Substring(i, 1);
                spl[i] = character;
            }

            for(int x = 0; x < spl.Length; x++)
            {
                string curntChar = spl[x];
                string result = (string)SymbolTable[curntChar];
                ciphertext += result;
            }
            return ciphertext;
        }

        [Authorize(Roles = "Administrator")]
       
        //Qr code generate
        public ActionResult QrGenerator(int id)
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "objct", });
            string url = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            string FullUrl = url + "/" + id;
            string EncrpUrl = cryptUrl(FullUrl);

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(EncrpUrl);
             
            Image QR = (Image)img;
            ImageConverter co = new ImageConverter();
            byte[] byteArray = (byte[])co.ConvertTo(img, typeof(byte[]));

            return File(byteArray, "image/png");
           
        }

    }
}
