﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Subs.Controllers
{
    public class SubsController:Controller
    {
        public IActionResult StartPage()
        {
            return View();
        }
    }
}
