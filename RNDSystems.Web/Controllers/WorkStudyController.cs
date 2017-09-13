using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    /// <summary>
    /// Employee Entities
    /// </summary>
    public class WorkStudyController : BaseController
    {
        /// <summary>
        /// Retrive work study details
        /// </summary>
        /// <returns></returns>
        #region WorkStudy
        public ActionResult WorkSutdyList()
        {
            _logger.Debug("WorkSutdyList");
            List<SelectListItem> studyTypes = null;
            List<SelectListItem> locations = null;
            List<SelectListItem> status = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/workstudy?recID=0").ContinueWith((res) =>
                  {
                      if (res.Result.IsSuccessStatusCode)
                      {
                          RNDWorkStudy workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);
                          if (workStudy != null)
                          {
                              studyTypes = workStudy.StudyTypes;
                              locations = workStudy.Locations;
                              status = workStudy.Status;
                          }
                      }
                  });
                task.Wait();
                ViewBag.ddStatus = status;
                ViewBag.ddStudyTypes = studyTypes;
                ViewBag.ddLocation = locations;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        /// <summary>
        /// Retrieve work study List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveWorkStudy(int id)
        {
            RNDWorkStudy workStudy = null;
            List<SelectListItem> studyTypes = null;
            List<SelectListItem> locations = null;
            List<SelectListItem> status = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/workstudy?recID=" + id).ContinueWith((res) =>
                  {
                      if (res.Result.IsSuccessStatusCode)
                      {
                          workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);
                          if (workStudy != null)
                          {
                              studyTypes = workStudy.StudyTypes;
                              locations = workStudy.Locations;
                              status = workStudy.Status;
                          }
                      }
                  });
                task.Wait();
                ViewBag.ddStatus = status;
                ViewBag.ddStudyTypes = studyTypes;
                ViewBag.ddLocation = locations;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(workStudy);
        }

        /// <summary>
        /// Save or Update work study details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveWorkStudy(RNDWorkStudy model)
        {
            var client = GetHttpClient();
            var task = client.PostAsJsonAsync(Api + "api/workstudy", model).ContinueWith((res) =>
               {
                   if (res.Result.IsSuccessStatusCode)
                   {
                       RNDWorkStudy workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);
                       if (workStudy != null)
                       {

                       }
                   }
               });
            task.Wait();
            return RedirectToAction("WorkSutdyList");
        }
        #endregion

    }
}
