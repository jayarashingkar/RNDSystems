using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    public class ProcessingMaterialController : BaseController
    {
        /// <summary>
        /// Get Processing Material
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="workStudyID"></param>
        /// <returns></returns>
        public ActionResult ProcessingMaterialList(int recId, string workStudyID)
        {
            _logger.Debug("ProcessingMaterialList");            
            RNDProcessing processing = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Processing?recID=0").ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        RNDProcessing rndProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                       
                    }
                });
                task.Wait();
                processing = new RNDProcessing
                {
                    WorkStudyID = workStudyID
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(processing);
        }

        /// <summary>
        /// Retrieve Processing Material List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workStudyId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SaveProcessingMaterial(int id, string workStudyId)
        {
            RNDProcessing processing = null;

            List<SelectListItem> SHTStartHours = null;
            List<SelectListItem> ArtStartHours = null;
            List<SelectListItem> SHTStartMinutes = null;
            List<SelectListItem> ArtStartMinutes = null;

            List<SelectListItem> ddlMillLotNo = null;
            List<SelectListItem> ddlPieceNo = null;
            List<SelectListItem> ddlHole = null;

            string strValue = string.Empty;
            int intCount = 60;
            int intRowId = 0;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Processing?recID=" + id).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        processing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                        if (processing != null)
                        {                            
                            if (!string.IsNullOrEmpty(workStudyId))
                            {
                                processing.WorkStudyID = workStudyId;                                
                            }
                            ddlMillLotNo = processing.ddMillLotNo;
                            ddlHole = processing.ddMillLotNo;
                            ddlPieceNo = processing.ddMillLotNo;
                        }
                    }
                });
                task.Wait();

                SHTStartHours = new List<SelectListItem>();
                ArtStartHours = new List<SelectListItem>();
                SHTStartMinutes = new List<SelectListItem>();
                ArtStartMinutes = new List<SelectListItem>();

                while (intRowId <= intCount)
                {
                    if (intRowId > 9)
                    {
                        strValue = Convert.ToString(intRowId);
                    }
                    else
                    {
                        strValue = "0" + Convert.ToString(intRowId);
                    }
                    
                    if(intRowId <= 24)
                    {
                        SHTStartHours.Add(new SelectListItem
                        {
                            Value = strValue,
                            Text = strValue,
                            Selected = (Convert.ToString(processing.SHTStartHrs) == Convert.ToString(strValue)) ? true : false,
                        });

                        ArtStartHours.Add(new SelectListItem
                        {
                            Value = strValue,
                            Text = strValue,
                            Selected = (Convert.ToString(processing.ArtStartHrs) == Convert.ToString(strValue)) ? true : false,
                        });
                    }

                    SHTStartMinutes.Add(new SelectListItem
                    {
                        Value = strValue,
                        Text = strValue,
                        Selected = (Convert.ToString(processing.SHTStartMns) == Convert.ToString(strValue)) ? true : false,
                    });

                    ArtStartMinutes.Add(new SelectListItem
                    {
                        Value = strValue,
                        Text = strValue,
                        Selected = (Convert.ToString(processing.ArtStartMns) == Convert.ToString(strValue)) ? true : false,
                    });

                    intRowId += 1;
                }

                ViewBag.ddSHTStartHours = SHTStartHours;
                ViewBag.ddArtStartHours = ArtStartHours;
                ViewBag.ddSHTStartMinutes = SHTStartMinutes;
                ViewBag.ddArtStartMinutes = ArtStartMinutes;

                ViewBag.ddlMillLotNo = ddlMillLotNo;
                ViewBag.ddlHole = ddlHole;
                ViewBag.ddlPieceNo = ddlPieceNo;
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(processing);
        }

        /// <summary>
        /// Save or Update Processing Material List details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveProcessingMaterial(RNDProcessing model)
        {
            var client = GetHttpClient();
            if (model.IsCopy)
            {
                model.RecID = 0;
                model.IsCopy = false;
            }
            var task = client.PostAsJsonAsync(Api + "api/Processing", model).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    RNDProcessing RNDProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);

                }
            });
            task.Wait();
            //if (model.RecID==0)
            //{
            //    var client = GetHttpClient();
            //    var task = client.PostAsJsonAsync(Api + "api/Processing", model).ContinueWith((res) =>
            //    {
            //        if (res.Result.IsSuccessStatusCode)
            //        {
            //            RNDProcessing RNDProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);

            //        }
            //    });
            //    task.Wait();
            //}
            //else
            //{
            //    var client = GetHttpClient();
            //    var task = client.PutAsJsonAsync(Api + "api/Processing", model).ContinueWith((res) =>
            //    {
            //        if (res.Result.IsSuccessStatusCode)
            //        {
            //            RNDProcessing RNDProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);

            //        }
            //    });
            //    task.Wait();
            //}

            return RedirectToAction("ProcessingMaterialList", new { recId = model.RecID, workStudyID = model.WorkStudyID });
        }
    }
}