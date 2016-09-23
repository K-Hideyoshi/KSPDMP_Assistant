using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using KspDmp.Data;

namespace KspDmp.Assistant.Controllers
{
    public class KerbalController : Controller
    {
		/// <summary>
		/// 修复小绿人BUG
		/// </summary>
		/// <returns></returns>
        public ActionResult Fix()
        {
            try
            {
                //获取飞船目录
                DirectoryInfo vesselFolder = KspDmp.Data.Path.GetVesselsFoder();
                var vesselFiles = vesselFolder.GetFiles();

                //读取所有飞船中小绿人的名字
                List<Vessel> vesselList = new List<Vessel>();
                //遍历飞船文件
                foreach (var vesselFile in vesselFiles)
                {
                    var vessel = new Vessel();
                    vessel.Kerbals = new List<string>();
                    string vesselText = System.IO.File.ReadAllText(vesselFile.FullName, Encoding.UTF8);
                    int startIndex = 0;
                    while (startIndex >= 0)
                    {
                        //截取小绿人名字
                        startIndex = vesselText.IndexOf("crew = ", startIndex);
                        if (startIndex >= 0)
                        {
                            startIndex += 7;
                            int endIndex = vesselText.IndexOf("\r", startIndex);
                            string name = vesselText.Substring(startIndex, endIndex - startIndex);
                            vessel.Kerbals.Add(name);
                        }
                    }
                    vesselList.Add(vessel);
                }

                //小绿人目录
                DirectoryInfo kerbalFolder = KspDmp.Data.Path.GetKerbalsFoder();

                //纠正每艘飞船内小绿人的index错误
                var kerbalFiles = kerbalFolder.GetFiles();
                foreach (var vessel in vesselList)
                {
                    foreach (var kerbal in vessel.Kerbals)
                    {
                        var kerbalFile = kerbalFiles.Where(file => file.Name.Contains(kerbal)).FirstOrDefault();
                        string kerbalText = System.IO.File.ReadAllText(kerbalFile.FullName, Encoding.UTF8);
                        if (kerbalText.Contains("idx = -1"))
                        {
                            //重新安排本飞船小绿人index
                            string reg = "idx = .*\r\n";
                            for (var newIndex = 0; newIndex < vessel.Kerbals.Count(); newIndex++)
                            {
                                var toCorrectKerbalFile = kerbalFiles.Where(file => file.Name.Contains(vessel.Kerbals.ElementAt(newIndex))).FirstOrDefault();
                                string toCorrectKerbalText = System.IO.File.ReadAllText(toCorrectKerbalFile.FullName, Encoding.UTF8);
                                var correctedKerbalText = Regex.Replace(toCorrectKerbalText, reg, "idx = " + newIndex + "\r\n");

                                //写文件
                                var fileSt = new FileStream(toCorrectKerbalFile.FullName, FileMode.Create);
                                StreamWriter streamWr = new StreamWriter(fileSt, Encoding.UTF8);
                                streamWr.Write(correctedKerbalText);
                                streamWr.Close();
                                fileSt.Close();
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Msg = ex.Message;
            }

            ViewBag.Success = true;
            return View();
        }
    }

    public class Vessel
    {
        public List<string> Kerbals { set; get; }
    }

}