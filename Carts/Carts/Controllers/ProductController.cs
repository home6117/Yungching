using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            //宣告回傳商品列表result
            List<Models.Products> result = new List<Models.Products>();

            //使用CartsEntities類別，名稱為db
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                //使用LinQ語法抓取目前Products資料庫中所有資料
                result = (from s in db.Products select s).ToList();

                //將result傳送給檢視
                return View(result);
            }
        }

        //建立商品頁面
        public ActionResult Create()
        {
            return View();
        }

        //建立商品頁面 - 資料傳回處理
        [HttpPost] //指定只有使用POST方法才可進入
        public ActionResult Create(Models.Products postback)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                //將回傳資料postback加入至Products
                db.Products.Add(postback);

                //儲存異動資料
                db.SaveChanges();
            }
            return View();
        }
    }
}