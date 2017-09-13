using Newtonsoft.Json;
using RNDSystems.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    public class AdminController : BaseController
    {
        /// <summary>
        /// Retrieve user security details
        /// </summary>
        /// <returns></returns>
        public ActionResult SecuityConfig()
        {
            List<SelectListItem> securityQuestions = null;
            var client = GetHttpClient();
            var task = client.GetAsync(Api + "api/UserSecurity?recID=0").ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    RNDUserSecurityAnswer security = JsonConvert.DeserializeObject<RNDUserSecurityAnswer>(res.Result.Content.ReadAsStringAsync().Result);
                    if (security != null)
                    {
                        securityQuestions = security.RNDSecurityQuestions;
                    }
                }
            });
            task.Wait();
            ViewBag.ddSecurityQuestions = securityQuestions;
            return View();
        }

        /// <summary>
        /// Once registered the new user after first time login, It will asked for the secerect questions with answers.
        /// User need to select the question and they need to provide the appropriate answer. These details will be saved in to database.
        /// whether the RNDRegistered user forget the password, It will help to reset or retrieve the password.
        /// Retrieve user security question details and the user need to provide the same answer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SecuityConfig(RNDUserSecurityAnswer model)
        {
            var client = GetHttpClient();
            var task = client.PostAsJsonAsync(Api + "api/UserSecurity", model).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    RNDUserSecurityAnswer workStudy = JsonConvert.DeserializeObject<RNDUserSecurityAnswer>(res.Result.Content.ReadAsStringAsync().Result);
                    if (workStudy != null && workStudy.RNDUserSecurityAnswerId > 0)
                    {
                        if (this.HttpContext.Session["CurrentUser"] != null)
                        {
                            CurrentUser currentUser = (CurrentUser)this.HttpContext.Session["CurrentUser"];
                            currentUser.StatusCode = "A";
                            this.HttpContext.Session["CurrentUser"] = currentUser;
                        }
                    }
                }
            });
            task.Wait();
            return RedirectToAction("WorkSutdyList", "WorkStudy");
        }
    }
}