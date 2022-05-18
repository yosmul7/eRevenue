using eRevenuesNew.Models;
using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRevenuesNew.Controllers
{
    public class HomeController : Controller
    {
        readonly eRevenuesDbEntities db = new eRevenuesDbEntities();
        public ActionResult YearDetailsList(int? page)
        {
            IPagedList<YearDetail> YearDetailList = db.YearDetails.OrderBy(c => c.FisicalYearId).ToList().ToPagedList(page ?? 1, 3);
            return View(YearDetailList);
        }
        public ActionResult Reports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/YearPlanReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "YearPlanDataSet";
            reportDataSource.Value = db.YearDetails.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "Word";
            }
            else if (reportType == "docx")
            {
                fileNameExtension = "docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "PDF";
            }
            else
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderByte;
            renderByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename = eRevenue_report." + fileNameExtension);
            return File(renderByte, fileNameExtension);
        }
    }
}