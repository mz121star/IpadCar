using car.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace car.Controllers
{
    public class HomeController : Controller
    {
        private Services services = new Services();
        [SessionUserParameter]
        //[Authorize]
        public ActionResult Index(int? viewType)
        {

            var categoryList = services.GetAllGoodsCategory();
            var typeList = new List<SecondGoods>();

            foreach (var item in categoryList.RESULT)
            {
                var type = services.GetGoodsTypeByCategoryID(item.GOODSCATEGORYID);
                type.RESULT.ToList().ForEach(x => x.FIRSTID = item.GOODSCATEGORYID);
                typeList.AddRange(type.RESULT);
            }

            dynamic tree = new ExpandoObject();
            tree.FirstList = categoryList.RESULT;
            tree.SecondList = typeList;
            tree.ViewType = null;
            if (Session["user"] != null)
            {
                tree.User = ((LoginUserDto)Session["user"]).EMPLOYEENAME;
            }
            else
            {
                tree.User = "";
            }
            tree.Brands = services.GetAllBrand().RESULT;
            tree.Cars = services.GetAllCars().RESULT;

            tree.Brand = null;
            tree.Car = null;
            return View(tree);
        }

        public ActionResult LoadGoods(string typeId, string brand, string car, int? viewType, int? pageIndex)
        {
            var pIndex = pageIndex ?? 1;
            dynamic tree = new ExpandoObject();
            viewType = viewType ?? 1;
            //if (viewType == 2)
            //{
            //    pIndex = 0;
            //}
            brand = brand ?? "";
            car = car ?? "";
            var b = services.GetGoodsByTypeIDWithPage(typeId, brand, car, pIndex.ToString());
            tree.ViewType = viewType;
            tree.Goods = b;
            tree.PageIndex = pIndex;
            tree.TypeId = typeId;
            tree.Brand = brand;
            tree.Car = car;
            tree.User = ((LoginUserDto)Session["user"]).EMPLOYEENAME;
            return PartialView("Partial/_RightViewGrid", tree);
        }

        [SessionUserParameter]
        public ActionResult Car()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginMethod(string name, string password)
        {
            var user = services.Login(name, password);
            Session["Plate"] = null;
            if (user != null)
            {
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("<script>alert('用户名或者密码错误!');</script>");
                return View("Login");
            }
        }

        [SessionUserParameter]
        public ActionResult LogOff()
        {
            //FormsAuthentication.SignOut();
            Session["user"] = null;
            Session["Plate"] = null;
            Session["SalesId"] = null;
            return View("Login");
        }

        [SessionUserParameter]
        public JsonResult GetAllPlate()
        {
            return Json(services.GetAllPlate().RESULT, JsonRequestBehavior.AllowGet);
        }

        [SessionUserParameter]
        public JsonResult SetPlate(string plate, string salesId)
        {
            var detail = services.DownloadBill(salesId);
            Session["Plate"] = plate;
            Session["SalesId"] = salesId;
            return Json(detail.DETAIL, JsonRequestBehavior.AllowGet);
        }

        [SessionUserParameter]
        public JsonResult DeleteGoods(string goodsId)
        {
            var salesId = Session["SalesId"].ToString();
            var message = services.DeleteGoods(salesId, goodsId);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [SessionUserParameter]
        public ActionResult AddGoods(string goodsId, decimal tempSalePrice, string salesMan, string operaNum)
        {
            GoodsDetailList list = new GoodsDetailList();
            list.DETAIL = new List<GoodsDetail>();
            list.DETAIL.Add(new GoodsDetail { GOODSID = goodsId, TEMPSALEPRICE = tempSalePrice.ToString(), SALESMAN = salesMan, SALESID = Session["SalesId"].ToString(), MASTERSALESID = operaNum });
            var message = services.AddGoods(list);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [SessionUserParameter]
        public ActionResult DeleteBill()
        {
            try
            {
                var salesId = Session["SalesId"].ToString();
                var message = services.DeleteBill(salesId);
                if (message.ISSUCCESS == "1")
                {
                    Session["SalesId"] = null;
                    Session["Plate"] = null;
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Message { ISSUCCESS = "0", MESSAGE = "更新失败，请先选择车牌号！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NewBill(string plate)
        {
            var user = (LoginUserDto)Session["user"];
            var master = new Master();
            master.EMPLOYEEID = user.EMPLOYEEID;
            master.SALESID = Guid.NewGuid().ToString();
            master.PLATE = plate;
            var message = services.NewBill(master);

            MessageResult mr = new MessageResult();
            mr.MESSAGE = message;
            if (message.ISSUCCESS == "1")
            {
                mr.PLATE = new Plate() { PLATE = plate, SALESID = master.SALESID };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClosingBill()
        {
            try
            {
                var salesId = Session["SalesId"].ToString();
                var message = services.ClosingBill(salesId);
                if (message.ISSUCCESS == "1")
                {
                    Session["SalesId"] = null;
                    Session["Plate"] = null;
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Message { ISSUCCESS = "0", MESSAGE = "更新失败，请先选择车牌号！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModifyGoods(GoodsDetail goods)
        {
            try
            {
                GoodsDetailList list = new GoodsDetailList();
                goods.SALESID = Session["SalesId"].ToString();
                list.DETAIL = new List<GoodsDetail>() { };
                list.DETAIL.Add(goods);
                var message = services.AddGoods(list);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Message { ISSUCCESS = "0", MESSAGE = "更新失败，请先选择车牌号！" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
