﻿using RNDSystems.API.SQLHelper;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RNDSystems.API.Controllers
{
    public class GridController : UnSecuredController
    {
        /// <summary>
        /// Retrieve the data and assign to Grid
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(DataGridoption option)
        {
            CurrentUser user = ApiUser;
            dynamic ds = null;
            try
            {
                if (option != null)
                {
                    switch (option.Screen)
                    {
                        case "WorkStudy":
                            ds = GetWorkStudies(option);
                            break;
                        case "AssignMaterial":
                            ds = GetAssignMaterial(option);
                            break;
                        case "RegisteredUser":
                            ds = GetRegisteredUser(option);
                            break;
                        case "ProcessingMaterial":
                            ds = GetProcessingMaterial(option);
                            break;
                        default:
                            break;
                    }
                }
                return Serializer.ReturnContent(ds, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve the WorkStudy Details
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private DataSearch<RNDWorkStudy> GetWorkStudies(DataGridoption option)
        {
            AdoHelper ado = new AdoHelper();
            SqlDataReader reader = null;
            List<RNDWorkStudy> lstWorkStudy = new List<RNDWorkStudy>();
            List<SqlParameter> lstSqlParameter = new List<SqlParameter>();
            lstSqlParameter.Add(new SqlParameter("@CurrentPage", option.pageIndex));
            lstSqlParameter.Add(new SqlParameter("@NoOfRecords", option.pageSize));
            AddSearchFilter(option, lstSqlParameter);
            using (reader = ado.ExecDataReaderProc("RNDWorkStudy_Read", "RND", lstSqlParameter.Cast<object>().ToArray()))
            {
                if (reader.HasRows)
                {
                    RNDWorkStudy WS = null;
                    while (reader.Read())
                    {
                        WS = new RNDWorkStudy();
                        WS.total = Convert.ToInt32(reader["total"]);
                        WS.RecId = Convert.ToInt32(reader["RecId"]);
                        WS.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                        WS.StudyType = Convert.ToString(reader["StudyType"]);
                        WS.StudyTypeDesc = Convert.ToString(reader["StudyTypeDesc"]);
                        WS.StudyTitle = Convert.ToString(reader["StudyDesc"]);
                        WS.StudyDesc = Convert.ToString(reader["StudyDesc"]);
                        WS.PlanOSCost = Convert.ToDecimal(reader["PlanOSCost"]);
                        WS.AcctOSCost = Convert.ToDecimal(reader["AcctOSCost"]);
                        WS.StudyStatus = Convert.ToString(reader["StudyStatus"]);
                        WS.StudyStatusDesc = Convert.ToString(reader["StudyStatusDesc"]);
                        WS.StartDate = Convert.ToString(reader["StartDate"]);
                        WS.DueDate = Convert.ToString(reader["DueDate"]);
                        WS.CompleteDate = Convert.ToString(reader["CompleteDate"]);
                        WS.Plant = Convert.ToString(reader["Plant"]);
                        WS.PlantDesc = Convert.ToString(reader["PlantDesc"]);
                        WS.TempID = Convert.ToString(reader["TempID"]);
                        WS.EntryBy = Convert.ToString(reader["EntryBy"]);
                        WS.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                        lstWorkStudy.Add(WS);
                    }
                }
            }
            DataSearch<RNDWorkStudy> ds = new DataSearch<RNDWorkStudy>
            {
                items = lstWorkStudy,
                total = (lstWorkStudy != null && lstWorkStudy.Count > 0) ? lstWorkStudy[0].total : 0
            };
            return ds;
        }

        /// <summary>
        /// Retrieve the Assign Material data and Assign to Grid
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private DataSearch<RNDMaterial> GetAssignMaterial(DataGridoption option)
        {
            AdoHelper ado = new AdoHelper();
            SqlDataReader reader = null;
            List<RNDMaterial> lstAssignMaterial = new List<RNDMaterial>();
            //SqlParameter param1 = new SqlParameter("@CurrentPage", option.pageIndex);
            //SqlParameter param2 = new SqlParameter("@NoOfRecords", option.pageSize);
            List<SqlParameter> lstSqlParameter = new List<SqlParameter>();
            lstSqlParameter.Add(new SqlParameter("@CurrentPage", option.pageIndex));
            lstSqlParameter.Add(new SqlParameter("@NoOfRecords", option.pageSize));
            AddSearchFilter(option, lstSqlParameter);
            using (reader = ado.ExecDataReaderProc("RNDAssignMaterial_Read", "RND", lstSqlParameter.Cast<object>().ToArray()))
            {
                if (reader.HasRows)
                {
                    RNDMaterial AM = null;
                    while (reader.Read())
                    {                        
                        AM = new RNDMaterial();
                        AM.total = Convert.ToInt32(reader["total"]);
                        AM.RecID = Convert.ToInt32(reader["RecID"]);
                        AM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                        AM.SoNum = Convert.ToString(reader["SoNum"]);
                        AM.MillLotNo = Convert.ToInt32(reader["MillLotNo"]);
                        AM.CustPart = Convert.ToString(reader["CustPart"]);
                        AM.UACPart = Convert.ToDecimal(reader["UACPart"]);
                        AM.Alloy = Convert.ToString(reader["Alloy"]);
                        AM.Temper = Convert.ToString(reader["Temper"]);
                        AM.GageThickness = Convert.ToString(reader["GageThickness"]);
                        AM.Location2 = Convert.ToString(reader["Location2"]);
                        AM.Hole = Convert.ToString(reader["Hole"]);
                        AM.PieceNo = Convert.ToString(reader["PieceNo"]);
                        AM.Comment = Convert.ToString(reader["Comment"]);
                        AM.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                        AM.EntryBy = Convert.ToString(reader["EntryBy"]);
                        AM.DBCntry = Convert.ToString(reader["DBCntry"]);
                        AM.RCS = Convert.ToChar(reader["RCS"]);
                        
                        lstAssignMaterial.Add(AM);
                    }
                }
            }


            DataSearch<RNDMaterial> ds = new DataSearch<RNDMaterial>
            {
                items = lstAssignMaterial,
                total = (lstAssignMaterial != null && lstAssignMaterial.Count > 0) ? lstAssignMaterial[0].total : 0
            };
            return ds;
        }

        /// <summary>
        /// Retrieve the Registered User Details data and Assign to Grid
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private DataSearch<RNDLogin> GetRegisteredUser(DataGridoption option)
        {
            AdoHelper ado = new AdoHelper();
            SqlDataReader reader = null;
            List<RNDLogin> lstRNDLogin = new List<RNDLogin>();
            List<SqlParameter> lstSqlParameter = new List<SqlParameter>();
            lstSqlParameter.Add(new SqlParameter("@CurrentPage", option.pageIndex));
            lstSqlParameter.Add(new SqlParameter("@NoOfRecords", option.pageSize));
            AddSearchFilter(option, lstSqlParameter);
            using (reader = ado.ExecDataReaderProc("RNDRegisteredUser_Read", "RND", lstSqlParameter.Cast<object>().ToArray()))
            {
                if (reader.HasRows)
                {
                    RNDLogin UD = null;
                    while (reader.Read())
                    {
                        UD = new RNDLogin();
                        UD.total = Convert.ToInt32(reader["total"]);
                        UD.UserId = Convert.ToInt32(reader["UserId"]);
                        UD.UserName = Convert.ToString(reader["UserName"]);
                        UD.FirstName = Convert.ToString(reader["FirstName"]);
                        UD.LastName = Convert.ToString(reader["LastName"]);
                        UD.PermissionLevel = Convert.ToString(reader["PermissionLevel"]);                        
                        UD.Created_By = Convert.ToString(reader["CreatedBy"]);
                        UD.Created_On = Convert.ToString(reader["CreatedOn"]);
                        UD.StatusCode = Convert.ToString(reader["StatusCode"]);
                        lstRNDLogin.Add(UD);
                    }
                }
            }
            DataSearch<RNDLogin> ds = new DataSearch<RNDLogin>
            {
                items = lstRNDLogin,
                total = (lstRNDLogin != null && lstRNDLogin.Count > 0) ? lstRNDLogin[0].total : 0
            };
            return ds;
        }

        /// <summary>
        /// Retrieve the Processing Material data and Assign to Grid
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private DataSearch<RNDProcessing> GetProcessingMaterial(DataGridoption option)
        {
            _logger.Debug("GetProcessingMaterial");
            
            SqlDataReader reader = null;            
            AdoHelper ado = new AdoHelper();

            List<RNDProcessing> lstProcessingMaterial = new List<RNDProcessing>();            
            List<SqlParameter> lstSqlParameter = new List<SqlParameter>();

            lstSqlParameter.Add(new SqlParameter("@CurrentPage", option.pageIndex));
            lstSqlParameter.Add(new SqlParameter("@NoOfRecords", option.pageSize));
            AddSearchFilter(option, lstSqlParameter);

            using (reader = ado.ExecDataReaderProc("RNDProcessingMaterial_Read", "RND", lstSqlParameter.Cast<object>().ToArray()))
            {
                if (reader.HasRows)
                {
                    RNDProcessing PM = null;
                    while (reader.Read())
                    {
                        PM = new RNDProcessing();

                        PM.RecID = Convert.ToInt32(reader["RecID"]);
                        PM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                        PM.MillLotNo = Convert.ToInt32(reader["MillLotNo"]);
                        PM.Hole = Convert.ToString(reader["Hole"]);
                        PM.PieceNo = Convert.ToString(reader["PieceNo"]);
                        PM.Sonum = Convert.ToString(reader["Sonum"]);
                        PM.ProcessNo = Convert.ToByte(reader["ProcessNo"]);
                        PM.ProcessID = Convert.ToString(reader["ProcessID"]);
                        PM.HTLogNo = Convert.ToInt32(reader["HTLogNo"]);
                        PM.HTLogID = Convert.ToString(reader["HTLogID"]);
                        PM.AgeLotNo = Convert.ToInt32(reader["AgeLotNo"]);
                        PM.AgeLotID = Convert.ToString(reader["AgeLotID"]);

                        PM.SHTTemp = Convert.ToString(reader["SHTTemp"]);
                        PM.SHSoakHrs = Convert.ToString(reader["SHSoakHrs"]);
                        PM.SHSoakMns = Convert.ToString(reader["SHSoakMns"]);
                        PM.SHTStartHrs = Convert.ToString(reader["SHTStartHrs"]);
                        PM.SHTStartMns = Convert.ToString(reader["SHTStartMns"]);
                        //PM.SHTDate = (!string.IsNullOrEmpty(reader["SHTDate"].ToString())) ? Convert.ToDateTime(reader["SHTDate"]) : (DateTime?)null;
                        PM.SHTDate = Convert.ToString(reader["SHTDate"]);

                        PM.StretchPct = Convert.ToString(reader["StretchPct"]);
                        PM.AfterSHTHrs = Convert.ToString(reader["AfterSHTHrs"]);
                        PM.AfterSHTMns = Convert.ToString(reader["AfterSHTMns"]);
                        PM.NatAgingHrs = Convert.ToString(reader["NatAgingHrs"]);
                        PM.NatAgingMns = Convert.ToString(reader["NatAgingMns"]);
                        PM.ArtStartHrs = Convert.ToString(reader["ArtStartHrs"]);
                        PM.ArtStartMns = Convert.ToString(reader["ArtStartMns"]);
                        //PM.ArtAgeDate = (!string.IsNullOrEmpty(reader["ArtAgeDate"].ToString())) ? Convert.ToDateTime(reader["ArtAgeDate"]) : (DateTime?)null;
                        PM.ArtAgeDate = Convert.ToString(reader["ArtAgeDate"]);

                        PM.ArtAgeTemp1 = Convert.ToString(reader["ArtAgeTemp1"]);
                        PM.ArtAgeHrs1 = Convert.ToString(reader["ArtAgeHrs1"]);
                        PM.ArtAgeMns1 = Convert.ToString(reader["ArtAgeMns1"]);
                        PM.ArtAgeTemp2 = Convert.ToString(reader["ArtAgeTemp2"]);
                        PM.ArtAgeHrs2 = Convert.ToString(reader["ArtAgeHrs2"]);
                        PM.ArtAgeMns2 = Convert.ToString(reader["ArtAgeMns2"]);
                        PM.ArtAgeTemp3 = Convert.ToString(reader["ArtAgeTemp3"]);
                        PM.ArtAgeHrs3 = Convert.ToString(reader["ArtAgeHrs3"]);
                        PM.ArtAgeMns3 = Convert.ToString(reader["ArtAgeMns3"]);

                        PM.FinalTemper = Convert.ToString(reader["FinalTemper"]);
                        PM.TargetCount = Convert.ToString(reader["TargetCount"]);
                        PM.ActualCount = Convert.ToString(reader["ActualCount"]);
                        PM.RCS = Convert.ToString(reader["RCS"]);

                        PM.RNDLotID = Convert.ToString(reader["RNDLotID"]);

                        PM.total = Convert.ToInt32(reader["total"]);

                        lstProcessingMaterial.Add(PM);
                    }
                }
            }
            DataSearch<RNDProcessing> ds = new DataSearch<RNDProcessing>
            {
                items = lstProcessingMaterial,
                total = (lstProcessingMaterial != null && lstProcessingMaterial.Count > 0) ? lstProcessingMaterial[0].total : 0
            };
            return ds;
        }

        /// <summary>
        /// Validate the search filter conditions
        /// </summary>
        /// <param name="option"></param>
        /// <param name="lstSqlParameter"></param>
        private static void AddSearchFilter(DataGridoption option, List<SqlParameter> lstSqlParameter)
        {
            if (option != null && !string.IsNullOrEmpty(option.searchBy))
            {
                string[] searchSplit = option.searchBy.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (searchSplit != null && searchSplit.Length > 0)
                {
                    foreach (var item in searchSplit)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var itemSplit = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (itemSplit != null && itemSplit.Length == 2)
                            {
                                if (!string.IsNullOrEmpty(itemSplit[1]) && itemSplit[1] != "-1")
                                    lstSqlParameter.Add(new SqlParameter("@" + itemSplit[0], itemSplit[1]));
                            }
                        }
                    }
                }
            }
        }
    }
}
