using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using car.DTO;
using car.iPadService;

namespace car
{
    public class Services
    {

        iPadService.iPadServiceSoapClient service = new iPadServiceSoapClient();
        public LoginUserDto Login(string username, string password)
        {

            var a = service.Authentication(username, password);
            var loginuser = JsonConvert.DeserializeObject<JSONLoginUserDto>(a);
            if (loginuser.RESULT.Count > 0)
            {
                return loginuser.RESULT[0];
            }
            else
            {
                return null;
            }
        }

        public PlateList GetAllPlate()
        {
            return JsonConvert.DeserializeObject<PlateList>(service.GetAllPlate());
        }

        public FirstGoodsList GetAllGoodsCategory()
        {
            var list = service.GetAllGoodsCategory();
            return JsonConvert.DeserializeObject<FirstGoodsList>(list);
        }

        public SecondGoodsList GetGoodsTypeByCategoryID(string strCategoryID)
        {
            var list = service.GetGoodsTypeByCategoryID(strCategoryID);
            return JsonConvert.DeserializeObject<SecondGoodsList>(list);
        }

        public GoodsList GetGoodsByTypeID(string strTypeID, string strBrand, string strCars, string strPages)
        {
            var list = service.GetGoodsByTypeID(strTypeID, strBrand, strCars, strPages);
            return JsonConvert.DeserializeObject<GoodsList>(list);
        }

        public Message NewBill(Master master)
        {
            var message = service.NewBill(JsonConvert.SerializeObject(master));
            return JsonConvert.DeserializeObject<MessageList>(message).RESULT.FirstOrDefault();
        }

        public Message DeleteBill(string strSalesID)
        {
            var message = service.DeleteBill(strSalesID);
            return JsonConvert.DeserializeObject<MessageList>(message).RESULT.FirstOrDefault();
        }

        public Message ClosingBill(string strSalesID)
        {
            var message = service.ClosingBill(strSalesID);
            return JsonConvert.DeserializeObject<MessageList>(message).RESULT.FirstOrDefault();
        }

        public Message DeleteGoods(string strSalesID, string strGoodsID)
        {
            var message = service.DeleteGoods(strSalesID, strGoodsID);
            return JsonConvert.DeserializeObject<MessageList>(message).RESULT.FirstOrDefault();
        }

        public DetailList DownloadBill(string strSalesID)
        {
            var detail = service.DownloadBill(strSalesID);
            return JsonConvert.DeserializeObject<DetailList>(detail);
        }

        public Message AddGoods(GoodsDetailList goodsDetailList)
        {
            var message = service.AddGoods(JsonConvert.SerializeObject(goodsDetailList));
            return JsonConvert.DeserializeObject<MessageList>(message).RESULT.FirstOrDefault();
        }

        public GoodsList GetGoodsByTypeIDWithPage(string strTypeID, string strBrand, string strCars, string strPages)
        {
            var list = service.GetGoodsByTypeID(strTypeID, strBrand, strCars, strPages);
            return JsonConvert.DeserializeObject<GoodsList>(list);
        }

        public BrandList GetAllBrand()
        {
            return JsonConvert.DeserializeObject<BrandList>(service.GetAllBrand());
        }

        public CarsList GetAllCars()
        {
            return JsonConvert.DeserializeObject<CarsList>(service.GetAllCars());
        }
    }
}
