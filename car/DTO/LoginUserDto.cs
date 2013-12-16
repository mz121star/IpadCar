using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace car.DTO
{
    public class JSONLoginUserDto
    {
        public IList<LoginUserDto> RESULT { get; set; }
    }

    public class LoginUserDto
    {
        public string EMPLOYEEID { get; set; }
        public string EMPLOYEENAME { get; set; }
    }

    public class PlateList
    {
        public IList<Plate> RESULT { get; set; }
    }

    public class Plate
    {
        public string PLATE { get; set; }
        public string SALESID { get; set; }
    }

    public class FirstGoodsList
    {
        public IList<FirstGoods> RESULT { get; set; }

    }

    public class FirstGoods
    {
        public string GOODSCATEGORYID { get; set; }
        public string GOODSCATEGORYNAME { get; set; }
    }

    public class SecondGoodsList
    {
        public IList<SecondGoods> RESULT { get; set; }

    }

    public class SecondGoods
    {
        public string GOODSTYPEID { get; set; }
        public string GOODSTYPENAME { get; set; }

        public string FIRSTID { get; set; }
    }

    public class GoodsList
    {
        public IList<Goods> RESULT { get; set; }

    }

    public class Goods
    {
        public string GOODSID { get; set; }
        public string GOODSNAME { get; set; }
        public string SALEPRICE { get; set; }
        public string IMAGEURL { get; set; }
        public string REMARKS { get; set; }
    }

    public class MasterDetail
    {
        public IList<Master> MASTER { get; set; }
        public IList<Detail> DETAIL { get; set; }
    }

    public class Master
    {
        public string SALESID { get; set; }
        public string PLATE { get; set; }
        public string EMPLOYEEID { get; set; }
    }

    public class Detail
    {
        public string GOODSID { get; set; }
        public string GOODSNAME { get; set; }
        public string SALEPRICE { get; set; }
        public string SUMNUMBER { get; set; }
        public string REMARKS { get; set; }
        public string SALESMAN { get; set; }
        public string TEMPSALEPRICE { get; set; }
    }

    public class DetailList
    {
        public IList<Detail> DETAIL { get; set; }
    }

    public class Message
    {
        public string ISSUCCESS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class MessageResult
    {
        public Message MESSAGE { get; set; }
        public Plate PLATE { get; set; }
    }

    public class MessageList
    {
        public IList<Message> RESULT { get; set; }
    }

    public class GoodsDetail
    {
        public string SALESID { get; set; }
        public string GOODSID { get; set; }
        public string SUMNUMBER { get; set; }
        public string REMARKS { get; set; }
        public string SALESMAN { get; set; }
        public string TEMPSALEPRICE { get; set; }
    }

    public class GoodsDetailList
    {
        public IList<GoodsDetail> DETAIL { get; set; }
    }

    public class Brand
    {
        public string BRAND { get; set; }
    }

    public class BrandList
    {
        public IList<Brand> RESULT { get; set; }
    }

    public class Cars
    {
        public string CARS { get; set; }
    }

    public class CarsList
    {
        public IList<Cars> RESULT { get; set; }
    }
}