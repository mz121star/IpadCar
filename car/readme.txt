因为我自己建的WebService引用了一些关联性的逻辑层组件，所以发给你也运行不起来，所以我把这个服务的文件给你，你在本地建一个名为Redf.Business.WebService的Web项目，然后建一个名为iPadService.asmx的服务，再用我给你这两个文件覆盖一下

1、方法：Authentication

   方法说明：
   /// <summary>
   /// 用户登录验证
   /// 输入参数包括：登录名（）、密码（）
   /// 返回结果：职员名称（EMPLOYEENAME）、职员编码（EMPLOYEEID）
   /// 备注：可缓存于iPad前端用于在新增账单时进行检验输入牌照是否有效

   参数：admin、111111
   验证通过结果：{"RESULT":[{"EMPLOYEEID":"TS_Ece6a1a5747a5cf40","EMPLOYEENAME":"拓天软件"}]}
   
   参数: admin、222222
   验证失败结果：{"RESULT":[]}
   
2、GetAllPlate()
   方法说明：
   /// <summary>
   /// 取所有牌照信息
   /// 返回结果包括：牌照号码（PLATE）
   /// 备注：可缓存于iPad前端用于在新增账单时进行检验输入牌照是否有效
   /// </summary>
   参数：无
   结果：{"RESULT":[{"PLATE":"辽A00008"},{"PLATE":"辽B00002"},{"PLATE":"辽C98888"}]}

3、GetAllGoodsCategory
   方法说明：
   /// <summary>
   /// 获取全部商品一级分类信息
   /// 返回结果：一级分类编码（GOODSCATEGORYID）、一级分类名称（GOODSCATEGORYNAME）
   /// </summary>
   参数：无
   结果：{"RESULT":[{"GOODSCATEGORYID":"TRI_74fb6869be042b80","GOODSCATEGORYNAME":"装饰用品"},{"GOODSCATEGORYID":"TRI_60643b5f6dfb054e","GOODSCATEGORYNAME":"机修服务"},{"GOODSCATEGORYID":"TRI_2af1e3c5b174b960","GOODSCATEGORYNAME":"钣喷服务"},{"GOODSCATEGORYID":"TRI_f59c3cdea63eaaba","GOODSCATEGORYNAME":"汽车配件"}]}


4、GetGoodsTypeByCategoryID(string strCategoryID)
   方法说明：
   /// <summary>
   /// 根据一级分类编码获取商品二级分类信息
   /// 返回结果：二级分类编码（GOODSTYPEID）、二级分类名称（GOODSTYPENAME）
   /// </summary>
   参数：TRI_74fb6869be042b80（20位长，一级大类编码，即调用上一方法所返回的结果中的GOODSCATEGORYID字段）
   结果：{"RESULT":[{"GOODSTYPEID":"TRI_6ca92e09985d2566","GOODSTYPENAME":"香水"},{"GOODSTYPEID":"TRI_b4b6eb9bb6ebe56a","GOODSTYPENAME":"防滑垫"}]}


5、GetGoodsByTypeID(string strTypeID) 
   方法说明：
   /// <summary>
   /// 根据二级分类编码获取商品信息
   /// 返回结果：商品编码（GOODSID）、名称（GOODSNAME）、价格（SALEPRICE）、图片地址（IMAGEURL）、商品说明（REMARKS）
   /// </summary>
   参数：TRI_74fb6869be042b80（20位长，一级大类编码，即调用上一方法所返回的结果中的GOODSCATEGORYID字段）
   结果：{"RESULT":[{"GOODSID":"TRI_ac40c20f9e59ac18","GOODSNAME":"CD香水","SALEPRICE":980.00,"IMAGEURL":"image01.jpg","REMARKS":"关于商品的描述"},{"GOODSID":"TRI_f1bd864e91f106e2","GOODSNAME":"香儿香水","SALEPRICE":1200.00,"IMAGEURL":"image02.jpg","REMARKS":"关于商品的描述"}]}



6、UploadBillData(string strBillData)
   方法说明：
   /// <summary>
   /// 保存账单信息
   /// 上传数据中账单主表包括：单据编码(SALESID)36位GUID、车牌照号(PLATE)、接待人编码(EMPLOYEEID,就是当前登录人)
   /// 上传数据中账单从表包括：商品编码(GOODSID)、数量（SUMNUMBER）、备注（REMARKS）;
   /// 
   /// 返回结果：是否成功（ISSUCCESS：1表示成功、0表示失败）、详细信息（MESSAGE：用详细语言描述，包括：已经结账了等等）
   /// </summary>
   参数：{"MASTER":[{"SALESID":"E512E00F-CA44-6F7A-F9C6-10CCDDB92BB8","PLATE":"辽B00001","EMPLOYEEID":"00000000000000000000"}],"DETAIL":[{"GOODSID":"TRI_74fb6869be042b80","SUMNUMBER":"1.00","REMARKS":"无"}]}
   保存成功结果：{"RESULT":[{"ISSUCCESS":"1","MESSAGE":"保存成功"}]}
   保存失败结果：{"RESULT":[{"ISSUCCESS":"0","MESSAGE":"保存失败，此单已结，请删除本地账单"}]}


7、 DeleteBillData(string strBillID)
    方法说明：
    /// <summary>
    /// 保存账单信息
    /// 上传数据中包括：单据编码(SALESID) 36位GUID
    /// 
    /// 返回结果：是否成功（ISSUCCESS：1表示成功、0表示失败）、详细信息（MESSAGE：用详细语言描述，包括：已经结账了等等）
    /// </summary>
    /// <returns></returns>
    参数：E512E00F-CA44-6F7A-F9C6-10CCDDB92BB8
    删除成功结果：{"RESULT":[{"ISSUCCESS":"1","MESSAGE":"删除成功"}]}
    删除失败结果：{"RESULT":[{"ISSUCCESS":"0","MESSAGE":"删除失败，此单已结，请删除本地账单"}]}