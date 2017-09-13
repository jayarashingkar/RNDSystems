using log4net;
using Newtonsoft.Json;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        #region Log4net

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        public static string Api = string.Empty;

        static LoginController()
        {
            Api = ConfigurationManager.AppSettings["Api"];
        }
        /// <summary>
        /// Login Page
        /// </summary>
        /// <returns></returns>
        // GET: Login
        public ActionResult Index()
        {
            RNDLogin login = new RNDLogin();
            ViewBag.msg = (TempData["msg"] != null) ? TempData["msg"] : null;
            if (Request.Cookies["RNDLogin"] != null)
            {
                var loginCookie = Request.Cookies["RNDLogin"];
                if (loginCookie != null && loginCookie.Values.Count > 0)
                {
                    login.UserName = loginCookie.Values["UserName"];
                    //login.Password = loginCookie.Values["Password"];
                }
            }
            return View(login);
        }

        /// <summary>
        /// Validate the entered user name and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RNDLogin model)
        {
            string msg = string.Empty;
            try
            {
                var client = GetHttpClient();
                var task = client.PostAsJsonAsync(Api + "api/login", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        ApiViewModel VM = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (VM != null)
                        {
                            if (string.IsNullOrEmpty(VM.Message) && VM.Custom != null)
                            {
                                RNDLogin dbUser = JsonConvert.DeserializeObject<RNDLogin>(VM.Custom.ToString());
                                if (dbUser != null)
                                {
                                    CurrentUser currentUser = new CurrentUser
                                    {
                                        UserId = dbUser.UserId,
                                        UserName = dbUser.UserName,
                                        FullName = dbUser.FirstName + " " + dbUser.LastName,
                                        PermissionLevel = dbUser.PermissionLevel,
                                        Token = dbUser.Token,
                                        StatusCode = dbUser.StatusCode
                                    };
                                    this.HttpContext.Session["CurrentUser"] = currentUser;
                                    if (model.IsRememberMe)
                                    {
                                        HttpCookie cookie = new HttpCookie("RNDLogin");
                                        cookie.Values.Add("UserName", currentUser.UserName);
                                        cookie.Expires = DateTime.Now.AddDays(15);
                                        Response.Cookies.Add(cookie);
                                    }
                                    else
                                        Response.Cookies["RNDLogin"].Expires = DateTime.Now.AddDays(-1);
                                }
                            }
                            if (!string.IsNullOrEmpty(VM.Message))
                                msg = VM.Message;
                        }
                    }
                });
                task.Wait();
                if (!string.IsNullOrEmpty(msg))
                {
                    TempData["msg"] = msg;
                    return RedirectToAction("Index"); //View("Index", null, model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return RedirectToAction("WorkSutdyList", "WorkStudy");
        }

        /// <summary>
        /// Load Security
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ActionResult LoadSecurity(string UserName)
        {
            List<SelectListItem> ddSecurityQuestions = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/login?userName=" + UserName).ContinueWith((res) =>
                 {
                     if (res.Result.IsSuccessStatusCode)
                     {
                         RNDUserSecurityAnswer rndUserSecurityAnswer = JsonConvert.DeserializeObject<RNDUserSecurityAnswer>(res.Result.Content.ReadAsStringAsync().Result);
                         if (rndUserSecurityAnswer != null)
                         {
                             ddSecurityQuestions = rndUserSecurityAnswer.RNDSecurityQuestions;
                         }
                     }
                 });
                task.Wait();
                ViewBag.ddSecurityQuestions = ddSecurityQuestions;
                ViewBag.UserName = UserName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Forgot Password home page
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Forgot Password Whether the RNDRegistered users forget the password, It will help to reset 
        /// or retrieve the password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(RNDUserSecurityAnswer model)
        {
            bool isSuccess = false;
            string message = "";
            try
            {
                var client = GetHttpClient();
                var task = client.PutAsJsonAsync(Api + "api/login", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        ApiViewModel VM = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (VM != null)
                        {
                            if (!string.IsNullOrEmpty(VM.Message))
                                message = VM.Message;
                            else if (VM.Custom != null)
                            {
                                message = VM.Custom.Password;
                                isSuccess = true;
                            }
                        }
                    }
                });
                task.Wait();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Json(new { isSuccess = isSuccess, message = message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetHttpClient for security connection
        /// </summary>
        /// <returns></returns>
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}