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

            ViewBag.ResultMessage = TempData["ResultMessage"];

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
        [HttpPost]
        public ActionResult Create(Models.Products postback)
        {
            if (this.ModelState.IsValid) //驗證成功
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    db.Products.Add(postback);
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", postback.Name);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ResultMessage = "資料有誤，請檢查";
            return View(postback);
        }
    }
}