using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Resume.Models;
using Resume.Models.Entities;
using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Resume.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
   
            return View();
        }


    }
}
