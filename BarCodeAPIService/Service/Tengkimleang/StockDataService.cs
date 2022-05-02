using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using SAPbobsCOM;

namespace BarCodeAPIService.Service.Tengkimleang
{
    public class StockDataService : IStockDataService
    {
        public Task<ResponseGetStockBatchSerial> responseGetStockBatchSerial(string BatchCode, string Serial,
            string BarCode)
        {
            var getStockBatchSerials = new List<GetStockBatchSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerials.Add(new GetStockBatchSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString(),
                            ItemName = oRS.Fields.Item(4).Value.ToString(),
                            UomCode = oRS.Fields.Item(5).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockBatchSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerials
                    });
                }

                return Task.FromResult(new ResponseGetStockBatchSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockBatchSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerial(string ItemCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerial(string ItemCode, string WhsCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','{WhsCode}','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string BatchCode,
            string Serial, string BarCode, string Warehouse, string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','{Warehouse}','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string ItemCode,
            string WhsCode, string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','{WhsCode}','{BinCode}','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockBatchSerialWarehouse> responseGetStockBatchSerialWarehouse(string BatchCode,
            string Serial, string BarCode, string Warehouse)
        {
            var getStockBatchSerials = new List<GetStockBatchSerialWarehouse>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','{Warehouse}','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerials.Add(new GetStockBatchSerialWarehouse
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockBatchSerialWarehouse
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerials
                    });
                }

                return Task.FromResult(new ResponseGetStockBatchSerialWarehouse
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockBatchSerialWarehouse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialWarehouseCode(string WhsCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{WhsCode}','','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialWarehouseCode(string WhsCode,
            string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{WhsCode}','{BinCode}','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerialBinCodes.Add(new GetStockBatchSerialBinCode
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getStockBatchSerialBinCodes
                    });
                }

                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchSerial> responseGetStockItemBatchSerial(string ItemCode, string BatchCode,
            string Serial)
        {
            var oLine = new List<GetStockItemBatchSerial>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStock_Batch_Serial_2','{ItemCode}','{Serial}','{BatchCode}','','')";
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        oLine.Add(new GetStockItemBatchSerial
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
                            SerialNumber = oRS.Fields.Item(9).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(10).Value.ToString())
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oLine
                    });
                }

                return Task.FromResult(new ResponseGetStockItemBatchSerial
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockItemBatchSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}