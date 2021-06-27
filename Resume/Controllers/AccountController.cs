using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Resume.Business.Tools;
using Resume.Models;
using Resume.Models.Context;
using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
namespace Resume.Controllers
{       
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ResumeContext db;
        private readonly GoogleConfigModel _googleConfig;
        private readonly ILogger<AccountController> _logger; // for NLOG

        public AccountController(ResumeContext context, IOptions<GoogleConfigModel> googleConfig, ILogger<AccountController> logger)
        {
            db = context;
            _googleConfig = googleConfig.Value;
            _logger = logger;
            _logger.LogDebug(1, "Nlog injected into AccountController");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDatasViewModel loginDatas)
        {
            loginDatas = loginDatas.ProtectForSQLInjection(loginDatas);
            var isValid = IsReCaptchValidV3(loginDatas.captcha);
            
            if (ModelState.IsValid == true && isValid == true)
            {
                loginDatas.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(loginDatas.Password));

                var user = db.Users.SingleOrDefault(u => u.Email == loginDatas.Email && u.Password == loginDatas.Password && u.Status == true);
                if (user != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginDatas.Email), // İstifadeci adini yaxalamaq ucun
                        //new Claim(ClaimTypes.Role, user.Role) //Role imkanlari yaratmaq ucun 
                    };

                    var useridentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(useridentity);

                    HttpContext.SignInAsync(claimsPrincipal);
                    _logger.LogInformation("Success");
                    return Redirect("/Admin/Info");
                }
            }
            _logger.LogWarning("Login Faild");
            return View();
        }

        private bool IsReCaptchValidV3(string captchaResponse)
        {
            var result = false;
            var secretKey = _googleConfig.Secret;
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }



        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            var users = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
