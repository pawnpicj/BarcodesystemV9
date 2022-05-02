using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Bank;
using SAPbobsCOM;

namespace BarCodeAPIService.Service.Bank
{
    public class Stock_WhsBinService : IStock_WhsBinService
    {
        public Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin(string whsCode, string binCode)
        {
            var getLineStock = new List<LineStock>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GETSTOCKWB','{whsCode}','{binCode}','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getLineStock.Add(new LineStock
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            WhsCode = oRS.Fields.Item(2).Value.ToString(),
                            WhsName = oRS.Fields.Item(3).Value.ToString(),
                            BinCode = oRS.Fields.Item(4).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(5).Value.ToString()),
                            BatchNo = oRS.Fields.Item(6).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(7).Value.ToString()),
                            UOMEntry = oRS.Fields.Item(8).Value.ToString(),
                            UOMCode = oRS.Fields.Item(9).Value.ToString(),
                            SerialNo = oRS.Fields.Item(10).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockByWhsBin
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = getLineStock
                    });
                }

                return Task.FromResult(new ResponseGetStockByWhsBin
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockByWhsBin
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchBin(string itemCode,
            string batchNumber, string binEntry)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch_Bin','{itemCode}','{batchNumber}','{binEntry}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialBin(string itemCode,
            string serialNumber, string binEntry)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial_Bin','{itemCode}','{serialNumber}','{binEntry}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatch(string itemCode, string batchNumber)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch','{itemCode}','{batchNumber}','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerial(string itemCode, string serialNumber)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial','{itemCode}','{serialNumber}','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchW(string itemCode, string batchNumber,
            string whsCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch_Whs','{itemCode}','{batchNumber}','{whsCode}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialW(string itemCode,
            string serialNumber, string whsCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial_Whs','{itemCode}','{serialNumber}','{whsCode}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemList.Add(new GetStockItemBatchAndSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            UOMCode = oRS.Fields.Item(3).Value.ToString(),
                            WhsEntry = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(6).Value.ToString()),
                            BinCode = oRS.Fields.Item(7).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(8).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}