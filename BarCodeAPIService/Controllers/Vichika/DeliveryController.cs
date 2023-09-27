using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Odbc;
using SAPbobsCOM;
using BarCodeLibrary.Respones.SAP;
using System.Collections.Generic;
using System;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService Delivery;

        public DeliveryController(IDeliveryService Delivery)
        {
            this.Delivery = Delivery;
        }

        [HttpGet(APIRoute.Delivery.GetSO+ "{cardCode}/{typeShow}")]
        public async Task<IActionResult> GetDeliveryAsyc(string cardCode, string typeShow)
        {
            var a = await Delivery.responseGetORDR(cardCode, typeShow);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Delivery.GetBatch + "{itemCode}/{WhsCode}")]
        public async Task<IActionResult> GetBatchAsync(string itemCode,string WhsCode)
        {
            var a = await Delivery.responseGetBatch(itemCode,WhsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Delivery.GetSerial + "{itemCode}/{WhsCode}")]
        public async Task<IActionResult> GetSerialAsyc(string itemCode, string WhsCode)
        {
            var a = await Delivery.responseGetSerial(itemCode, WhsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost(APIRoute.Delivery.POSTDelivery)]
        public async Task<IActionResult> PostDeliveryAsync(SendDelivery sendDelivery)
        {
            var a = await Delivery.PostDelivery(sendDelivery);
            if (a.ErrorCode == 0)
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                string DBName = ConnectionString.CompanyDB;

                string Fld1 = "DocEntry";
                string Fld2 = "LineNum";
                string Fld3 = "ItemCode";
                string Fld4 = "PriceAfVAT";
                string Fld5 = "GPBefDisc";

                string PRODUCTS_TABLE = "DLN1";
                var dt = new DataTable();
                string strSQL = "SELECT \"" + Fld1 + "\", \"" + Fld2 + "\", \"" + Fld3 + "\", \"" + Fld4 + "\", \"" + Fld5 + "\"" +
                    " FROM \"" + ConnectionString.CompanyDB + "\".\"" + PRODUCTS_TABLE + "\" " +
                    " WHERE \"" + Fld1 + "\" = " + a.DocEntry + " ";
                login.AD = new OdbcDataAdapter(strSQL, login.CN);
                login.AD.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    string DocEntry = row["DocEntry"].ToString();
                    string LineNum = row["LineNum"].ToString();
                    string ItemCode = row["ItemCode"].ToString();
                    string PriceAfVAT = row["PriceAfVAT"].ToString();
                    string GPBefDisc = row["GPBefDisc"].ToString();

                    string cPriceAfter = "";
                    cPriceAfter = string.Format("{0:F2}", PriceAfVAT);
                    double dblPriceAfter = System.Math.Round(Convert.ToDouble(cPriceAfter), 2);

                    string UPD = "UPDATE \"" + ConnectionString.CompanyDB + "\".\"" + PRODUCTS_TABLE + "\" SET " +
                                " \"" + Fld4 + "\" = " + dblPriceAfter + ", \"" + Fld5 + "\" = " + dblPriceAfter + "" +
                                " WHERE \"" + Fld1 + "\"=" + DocEntry + " AND \"" + Fld2 + "\"=" + LineNum + " AND \"" + Fld3 + "\"='" + ItemCode + "' ";
                    try
                    {
                        login.AD = new OdbcDataAdapter(UPD, login.CN);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }                
        }

        [HttpGet(APIRoute.Delivery.GetSaleOrderList)]
        public async Task<IActionResult> GetSaleOrderListAsync()
        {
            var a = await Delivery.responseGetSaleOrder();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetSODelivery/{cardCode}")]
        public async Task<IActionResult> GetSODeliveryAsyc(string cardCode)
        {
            var a = await Delivery.responseGetSO(cardCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetSONew/{cardCode}/{typeShow}")]
        public async Task<IActionResult> GetSONewAsyc(string cardCode, string typeShow)
        {
            var a = await Delivery.responseGetSONew(cardCode, typeShow);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }

    }
}