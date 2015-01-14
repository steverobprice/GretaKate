using GretaKate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Web.Configuration;
using GretaKate.Core.Consts;
using GretaKate.Services;
using System.IO;

namespace GretaKate.Web.Controllers
{
    public class EnquiryController : SurfaceController
    {
        private readonly ITemplateService _templateService;
        private readonly IEmailService _emailService;

        public EnquiryController(ITemplateService templateService, IEmailService emailService)
        {
            _templateService = templateService;
            _emailService = emailService;
        }

        [HttpPost]
        public JsonResult Submit(EnquiryModel enquiryModel)
        {
            if (ModelState.IsValid)
            {
                var filePath = Server.MapPath(WebConfigurationManager.AppSettings[Settings.FilePath]);
                var emailTemplatePath = WebConfigurationManager.AppSettings[Settings.EmailTemplatePath];
                var enquiryEmailTempalte = WebConfigurationManager.AppSettings[Settings.EnquiryEmailTemplate];
                var templateFullPath = Path.Combine(filePath, emailTemplatePath, enquiryEmailTempalte);

                var template = _templateService.Load(templateFullPath);
                template = _templateService.Parse(template, enquiryModel);

                if (!string.IsNullOrEmpty(template))
                {
                    var toEmailAddress = WebConfigurationManager.AppSettings[Settings.EnquiryEmailAddress];
                    var fromEmailAddress = enquiryModel.Email;

                    if (_emailService.SendEmail(toEmailAddress, toEmailAddress, fromEmailAddress, "Web site enquiry", template))
                    {
                        return Json(new { IsSuccess = true, Message = "Enquiry sent" });
                    }
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "Enquiry failed" });
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Unable to load template" });
                }
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "Validation failed" });
            }
        }
    }
}