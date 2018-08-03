
using TuesdayKetchup.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TuesdayKetchup.Controllers
{
    public class CalendarController : Controller
    {
        #region Index method  

        /// <summary>  
        /// GET: Home/Index method.  
        /// </summary>  
        /// <returns>Returns - index view page</returns>   
        public ActionResult CalendarIndex()
        {
            // Info.  
            return this.View();
        }

        #endregion

        #region Get Calendar data method.  

        /// <summary>  
        /// GET: /Home/GetCalendarData  
        /// </summary>  
        /// <returns>Return data</returns>  
        public ActionResult GetCalendarData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();

            try
            {
                // Loading.  
                //List<Event> data = this.LoadData();

                // Processing.  
                //result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }

        #endregion

        #region Helpers  

        #region Load Data  

        /// <summary>  
        /// Load data method.  
        /// </summary>  
        /// <returns>Returns - Data</returns>  
        //private List<Event> LoadData()
        //{
        //    Initialization.
        //   List<Event> lst = new List<Event>();

        //    try
        //    {
        //        Initialization.
        //        string line = string.Empty;
        //        string srcFilePath = "Content/files/Event.txt";
        //        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        //        var fullPath = Path.Combine(rootPath, srcFilePath);
        //        string filePath = new Uri(fullPath).LocalPath;
        //        StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

        //        Read file.  
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            Initialization.
        //           Event infoObj = new Event();
        //            string[] info = line.Split(',');

        //            Setting.
        //           infoObj.Sr = Convert.ToInt32(info[0].ToString());
        //            infoObj.Title = info[1].ToString();
        //            infoObj.Desc = info[2].ToString();
        //            infoObj.Start_Date = info[3].ToString();
        //            infoObj.End_Date = info[4].ToString();

        //            Adding.
        //           lst.Add(infoObj);
        //        }

        //        Closing.
        //       sr.Dispose();
        //        sr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        info.
        //       Console.Write(ex);
        //    }

        //    info.
        //    return lst;
        //}

        #endregion

        #endregion
    }
}