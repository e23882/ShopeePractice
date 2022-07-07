using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopeeCrawler
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult GetData() 
        {
            string jsonData = "";
            try 
            {
                List<Stock> list = new List<Stock>();

                using (var connection = new MySqlConnection("Server=localhost;Database=stockdb;User Id=root;Password=;"))
                {
                    var result = connection.Query<Stock>($"Select StockNo, StockName, ClosePrice from stock limit 100");
                    foreach (var item in result)
                    {
                        list.Add(item);
                    }
                }

                //1.定義list模擬資料庫取得資料

                //list.Add(new myData()
                //{
                //     id = "1",
                //     name = "原子筆",
                //     price = "100"

                //});

                //2.把list轉成json
                var instance = new JavaScriptSerializer();
                jsonData = instance.Serialize(list);
            }
            catch(Exception ex) 
            {
                jsonData = "";
            }
            

            //3.回傳JSON給前端
            return this.Content(jsonData, "application/json");
        }
    }
}