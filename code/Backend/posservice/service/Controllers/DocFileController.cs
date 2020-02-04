using posrepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Web.Http;
namespace service.Controllers
{
    public class DocFileController : ApiController
    {
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            string tmpname = PosUtil.ConvertToTimestamp(DateTime.Now).ToString();
            try
            {


                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {

                        var postedFile = httpRequest.Files[file];
                        //var filePath = HttpContext.Current.Server.MapPath("~/images/" + postedFile.FileName);
                        tmpname = tmpname + Path.GetExtension(postedFile.FileName);
                        //var filePath = HttpContext.Current.Server.MapPath("~/images/" + tmpname);
                        //postedFile.SaveAs(filePath);

                        string fname = Path.GetFileName(postedFile.FileName);
                        string filePath = HttpContext.Current.Server.MapPath(Path.Combine("~/images", fname));
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {

                result = Request.CreateResponse(HttpStatusCode.Conflict, ex.Message + ex.InnerException.Message);

            }
            return result;
        }
    }
}
