using BarCodeAPIService.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;

namespace BarCodeAPIService.Service.Bank
{
    public class Stock_WhsBinService : IStock_WhsBinService
    {
        public Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin(string whsCode, string binCode)
        {
            var getLineStock = new List<LineStock>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GETSTOCKWB','{whsCode}','{binCode}','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                            Quantity = Convert.ToDouble(oRS.Fields.Item(7).Value.ToString()),                            
                            UOMCode = oRS.Fields.Item(6).Value.ToString(),
                            BatchYN = oRS.Fields.Item(8).Value.ToString(),
                            SerialYN = oRS.Fields.Item(9).Value.ToString()
                            //BatchNo = oRS.Fields.Item(6).Value.ToString(),
                            //SerialNo = oRS.Fields.Item(10).Value.ToString()


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
                else
                {
                    return Task.FromResult(new ResponseGetStockByWhsBin
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchBin(string itemCode, string batchNumber, string binEntry)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch_Bin','{itemCode}','{batchNumber}','{binEntry}','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialBin(string itemCode, string serialNumber, string binEntry)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial_Bin','{itemCode}','{serialNumber}','{binEntry}','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch','{itemCode}','{batchNumber}','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial','{itemCode}','{serialNumber}','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchW(string itemCode, string batchNumber, string whsCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Batch_Whs','{itemCode}','{batchNumber}','{whsCode}','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialW(string itemCode, string serialNumber, string whsCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetStock_Serial_Whs','{itemCode}','{serialNumber}','{whsCode}','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseScanItemsInIM> responseScanItemsInIM(string docEntry, string itemCode, string batchSerialNo)
        {
            var getItemsLine = new List<GetItemsLine>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetItemInIM','{docEntry}','{itemCode}','{batchSerialNo}','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getItemsLine.Add(new GetItemsLine
                        {

                            DocEntry = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
                            ItemCode = oRS.Fields.Item(1).Value.ToString(),
                            ItemName = oRS.Fields.Item(2).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(3).Value.ToString()),
                            UOMCode = oRS.Fields.Item(6).Value.ToString(),

                            FWhsCode = oRS.Fields.Item(5).Value.ToString(),
                            FBinEntry = Convert.ToInt32(oRS.Fields.Item(8).Value.ToString()),
                            FBinCode = oRS.Fields.Item(7).Value.ToString(),

                            TWhsCode = oRS.Fields.Item(4).Value.ToString(),
                            TBinEntry = Convert.ToInt32(oRS.Fields.Item(10).Value.ToString()),
                            TBinCode = oRS.Fields.Item(9).Value.ToString(),

                            BatchNumber = oRS.Fields.Item(11).Value.ToString(),
                            SerialNumber = oRS.Fields.Item(12).Value.ToString(),
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseScanItemsInIM
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemsLine
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseScanItemsInIM
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseScanItemsInIM
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetItemByBarcode(string barCode, string itemCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetBatchNumber','{barCode}','{itemCode}','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);

                    if (oRS.BoF && oRS.EoF)
                    {
                        string sqlStr2 = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetSerialNumber','{barCode}','{itemCode}','','','')";
                        oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRS.DoQuery(sqlStr2);
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
                                SerialNumber = oRS.Fields.Item(8).Value.ToString(),
                                ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString()),
                                FDA = oRS.Fields.Item(10).Value.ToString()
                            });
                            oRS.MoveNext();
                        }
                    }
                    else
                    {
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
                                ExpDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString()),
                                FDA = oRS.Fields.Item(10).Value.ToString()
                            });
                            oRS.MoveNext();
                        }
                    }

                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetOUOM> responseGetOUOM()
        {
            var getUOMList = new List<OUOMList>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetPriceUnit','','','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);

                    while (!oRS.EoF)
                    {
                        getUOMList.Add(new OUOMList
                        {
                            UomCode = oRS.Fields.Item(0).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetOUOM
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = getUOMList
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetOUOM
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetOUOM
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetDataConfig> responseGetDataConfig()
        {
            var ListData = new List<ListData>();
            SAPbobsCOM.Company oCompany;
            try
            {
                //GetData

                Login login = new();
                if (login.LErrCode == 0)
                {
                    ListData.Add(new ListData
                    {
                        CompanyDB = ConnectionString.CompanyDB,
                        UserNameSAP = ConnectionString.UserName
                    });

                    return Task.FromResult(new ResponseGetDataConfig
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = ListData
                    });

                }
                else
                {
                    return Task.FromResult(new ResponseGetDataConfig
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }

                //End
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetDataConfig
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            

        }

        public Task<ResponseGetStockItemBatchAndSerial> responseGetItemByBinCode(string itemCode, string binCode)
        {
            var getItemList = new List<GetStockItemBatchAndSerial>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetItemByBinCode','{itemCode}','{binCode}','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    
                        while (!oRS.EoF)
                        {
                            getItemList.Add(new GetStockItemBatchAndSerial
                            {
                                ItemCode = oRS.Fields.Item(0).Value.ToString(),
                                ItemName = oRS.Fields.Item(1).Value.ToString(),
                                Quantity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                                WhsEntry = Convert.ToInt32(oRS.Fields.Item(3).Value.ToString()),
                                WhsCode = oRS.Fields.Item(4).Value.ToString(),
                                BinEntry = Convert.ToInt32(oRS.Fields.Item(5).Value.ToString()),
                                BinCode = oRS.Fields.Item(6).Value.ToString(),
                                UOMCode = oRS.Fields.Item(7).Value.ToString(),
                                ExpDate = Convert.ToDateTime(oRS.Fields.Item(8).Value.ToString()),
                                FDA = oRS.Fields.Item(9).Value.ToString()
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
                else
                {
                    return Task.FromResult(new ResponseGetStockItemBatchAndSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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

        public Task<ResponseGetListItemMaster> responseGetListItemMaster()
        {
            var getItemList = new List<lMaster>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('GetListItemMaster','','','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);

                    while (!oRS.EoF)
                    {
                        getItemList.Add(new lMaster
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            UOM = oRS.Fields.Item(2).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(3).Value.ToString()),
                            BatchYN = oRS.Fields.Item(4).Value.ToString(),
                            SerialYN = oRS.Fields.Item(5).Value.ToString(),
                            Status = oRS.Fields.Item(6).Value.ToString(),                            
                        });
                        oRS.MoveNext();
                    }


                    return Task.FromResult(new ResponseGetListItemMaster
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getItemList
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetListItemMaster
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetListItemMaster
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
