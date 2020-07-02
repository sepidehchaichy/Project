using Business.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace Business.Controller
{
    [RoutePrefix("api/magfactrl")]
    public class MagfaController : ApiController
    {

        [HttpPost]
        [Route("GLNsCount")]
        public ResultModel<List<MagfaModel>> GLNsCount(ServicesReportModel model)
        {

            try
            {
                    var DX = new EntitiesContext();

                    var dt = new DataTable();
                    var _FromDate = new DateTime?();
                    var _ToDate = new DateTime?();

                    if (model != null && model.FromDate != null && model.FromDate != "")
                    {
                        _FromDate = Convert.ToDateTime(Convert.ToDateTime(_FromDate).ToShortDateString());
                    }
                    if (model != null && model.FromDate != null && model.FromDate != "")
                    {
                        _ToDate = Convert.ToDateTime(Convert.ToDateTime(_ToDate).ToShortDateString());
                    }

                    dt = DX.ExecuteStoredProcedure(DX, "GLNsCountReport", new[] {
                    new SqlParameter("@FromDate" ,_FromDate),
                    //new SqlParameter("@StatusID", model.StatusID),
                    new SqlParameter("@ToDate",_ToDate)});

                    if (dt.Rows.Count == 0)
                    {
                        return new ResultModel<List<MagfaModel>>
                        {
                            Succeeded = false,
                            Message = "اطلاعات موجود نمی باشد."
                        };
                    }

                    var query = dt.Rows.OfType<DataRow>().Select(x => new MagfaModel
                    {
                        Title = x["Title"].ToString(),
                        CountGLN = x["CountGLN"] != null ? Convert.ToInt32(x["CountGLN"]) : 0,
                        ExternalCodeCount = Convert.ToInt32(x["ExternalCodeCount"]),
                        InternalCodeCount = Convert.ToInt32(x["InternalCodeCount"])
                        //            RecpayID = (int)x["ID"],
                        //            TypeTitle = x["TypeTitle"].ToString(),
                        //            RecpayPrice = (decimal)x["RecpayPrice"],
                        //            RecpayDate = (DateTime)x["RecpayDate"],
                        //            RegisterSystemDate = (DateTime)x["RegisterSystemDate"],
                        //            OwnerUserDisplayName = x["OwnerUserDisplayName"].ToString(),
                        //            Status = Convert.ToInt32(x["Status"]),
                        //            RecPayConfrimDate = Convert.IsDBNull(x["RecPayConfrimDate"]) ? (DateTime?)null : (DateTime)x["RecPayConfrimDate"],
                    }).ToList();

                    return new ResultModel<List<MagfaModel>>
                    {
                        Succeeded = true,
                        Result = query
                    };
   
            }
            catch (Exception x)
            {

                return new ResultModel<List<MagfaModel>>
                {
                    Succeeded = false,
                    Message = "خطا در عملیات - لطفا دوباره تلاش نمائید."
                };
            }

        }


        [HttpGet]
        [Route("GLNsCount2")]
        public ResultModel<List<MagfaModel>> GLNsCount2()
        {

            try
            {
                var DX = new EntitiesContext();

                return new ResultModel<List<MagfaModel>>
                {
                    Succeeded = true,
                    Result = null
                };

            }
            catch (Exception x)
            {

                return new ResultModel<List<MagfaModel>>
                {
                    Succeeded = false,
                    Message = "خطا در عملیات - لطفا دوباره تلاش نمائید."
                };
            }

        }



        //[HttpPost]
        //[Route("GLNsCount")]
        //public ResultModel<List<MagfaModel>> GLNsCount(MagfaModel model)
        //{

        //    try
        //    {
        //        var Context = new EntitiesContext();

        //            DateTime? BeginDate = null;
        //            DateTime? TodDate = null;

        //            //if (!string.IsNullOrEmpty(model.FromDate))
        //            //{
        //            //    BeginDate = Convert.ToDateTime(_FromDate.ToDate());
        //            //}

        //            //if (!string.IsNullOrEmpty(model.ToDate))
        //            //{

        //            //    TodDate = Convert.ToDateTime(_EndDate.ToDate());
        //            //}

        //        var dt = Context.ExecuteStoredProcedure(Context, "GetOfflinePayment",
        //         new[] { new SqlParameter("@FromDate", BeginDate),
        //                    new SqlParameter("@ToDate", TodDate),
        //                    new SqlParameter("@StatusID", model.StatusID),
        //                    new SqlParameter("@UserID", model.UserID)});

        //        var Data = dt.Rows.OfType<DataRow>().Select(x => new
        //        {
        //            RecpayID = (int)x["ID"],
        //            TypeTitle = x["TypeTitle"].ToString(),
        //            RecpayPrice = (decimal)x["RecpayPrice"],
        //            RecpayDate = (DateTime)x["RecpayDate"],
        //            RegisterSystemDate = (DateTime)x["RegisterSystemDate"],
        //            OwnerUserDisplayName = x["OwnerUserDisplayName"].ToString(),
        //            Status = Convert.ToInt32(x["Status"]),
        //            RecPayConfrimDate = Convert.IsDBNull(x["RecPayConfrimDate"]) ? (DateTime?)null : (DateTime)x["RecPayConfrimDate"],
        //            ConfirmUserDisplayName = x["ConfirmUserDisplayName"].ToString(),
        //        }).ToList();

        //        var Data = dt.Rows.OfType<DataRow>().Select(x => new
        //            {
        //                ID = (int)(x["ID"]),
        //                RecPayTypeID = (int)(x["RecPayTypeID"]),
        //                TypeTitle = x["TypeTitle"].ToString(),
        //                RecPayDate = ((DateTime)x["RecPayDate"]).ToPersianDate(),
        //                Status = x["Status"].ToString(),
        //                RecPayPrice = x["RecPayPrice"].ToString(),
        //                CreateDate = ((DateTime)x["CreateDate"]).ToPersianDate(),
        //                ConfirmDate = x["ConfirmDate"].ToString() != "" ? (DateTime?)x["ConfirmDate"] : null,
        //                OwnerUserID = (int)(x["OwnerUserID"]),
        //                OwnerUserName = x["UserName"].ToString(),
        //                PaymentCode = x["PaymentCode"].ToString(),
        //            }).ToList();

        //            if (Data.Count() == 0)
        //            {
        //                return new ResultModel<List<MagfaModel>>
        //                {
        //                    Succeeded = false,
        //                    Message = "اطلاعات در این بازه زمانی موجود نمی باشد"
        //                };
        //            }

        //            var _Result = Data.Select((x, index) => new MagfaModel
        //            {
        //                Index = index + 1,
        //                ID = x.ID,
        //                OwnerUserName = x.OwnerUserName,
        //                PaymentCode = x.PaymentCode,
        //                TypeTitle = x.TypeTitle,
        //                RecPayPrice = x.RecPayPrice,
        //                Status = x.Status == "1" ? "تایید شده" : "تایید نشده",
        //                RecPayDate = x.RecPayDate,
        //                CreateDate = x.CreateDate,
        //                ConfirmDate = x.ConfirmDate != null ? x.ConfirmDate.ToPersianDate() : "--",

        //            }).ToList();

        //            return new ResultModel<List<MagfaModel>>
        //            {
        //                Succeeded = true,
        //                // .Select((v, index) => new RequestModel
        //                Result = _Result
        //            };

        //    }
        //    catch (Exception x)
        //    {


        //        return new ResultModel<List<MagfaModel>>
        //        {
        //            Succeeded = false,
        //            Message = "خطای عملیاتی -  لطفا دوباره تلاش نمائید.",
        //        };
        //    }


        //    //return new ResultModel<List<MagfaModel>>
        //    //{
        //    //    Result = null,
        //    //    Success = true,
        //    //    Message = null
        //    //};
        //}


    }
}