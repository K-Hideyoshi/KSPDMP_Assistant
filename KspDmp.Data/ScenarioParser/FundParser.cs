using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace KspDmp.Data.ScenarioParser
{
	public static class FundParser
	{
		/// <summary>
		/// 获取资金值
		/// </summary>
		/// <param name="scenarioFileStr"></param>
		/// <returns></returns>
		public static double GetFundValue(string scenarioFileStr)
		{
			return Convert.ToDouble(scenarioFileStr.Split('\n')[2].Split('=')[1].Trim());
		}

		/// <summary>
		/// 设置资金值
		/// </summary>
		/// <returns></returns>
		public static bool SetFundValue(string scenarioFileStr, string scenarioFilePath, double fund)
		{
			try
			{
				//修盖资金值
				string newStr = scenarioFileStr;
				string regPattern = "funds = .*";
				newStr = Regex.Replace(newStr, regPattern, "funds = " + Math.Round(fund,5).ToString());

				//写文件
				var fileSt = new FileStream(scenarioFilePath, FileMode.Create);
				StreamWriter streamWr = new StreamWriter(fileSt, Encoding.UTF8);
				streamWr.Write(newStr);
				streamWr.Close();
				fileSt.Close();
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}
	}
}
