using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Tengkimleang
{
    public class StockDataService : IStockDataService
    {
        public Task<ResponseGetStockBatchSerial> responseGetStockBatchSerial(string BatchCode, string Serial, string BarCode)
        {
            var getStockBatchSerials = new List<GetStockBatchSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getStockBatchSerials.Add(new GetStockBatchSerial
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SerialNumber = oRS.Fields.Item(2).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(3).Value.ToString()
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
                else
                {
                    return Task.FromResult(new ResponseGetStockBatchSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','{WhsCode}','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string BatchCode, string Serial, string BarCode, string Warehouse,string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','{Warehouse}','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string ItemCode, string WhsCode, string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{ItemCode}','{WhsCode}','{BinCode}','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
        public Task<ResponseGetStockBatchSerialWarehouse> responseGetStockBatchSerialWarehouse(string BatchCode, string Serial, string BarCode, string Warehouse)
        {
            var getStockBatchSerials = new List<GetStockBatchSerialWarehouse>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{BarCode}','{Serial}','{BatchCode}','{Warehouse}','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockBatchSerialWarehouse
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{WhsCode}','','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
        public Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialWarehouseCode(string WhsCode, string BinCode)
        {
            var getStockBatchSerialBinCodes = new List<GetStockBatchSerialBinCode>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_TENGKIMLEANG('GetStcok_Batch_Serial','{WhsCode}','{BinCode}','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStcokBatchSerialBinCode
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
    }
}
