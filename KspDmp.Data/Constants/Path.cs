using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.IO;

namespace KspDmp.Data
{
	/// <summary>
	/// 路径数据获取类
	/// </summary>
	public class Path
	{
		/// <summary>
		/// DMP根目录路径(末尾不带斜杠)
		/// </summary>
		private static string DMPDir { get{ return System.Configuration.ConfigurationManager.AppSettings["dmp_dir"]; } }

		/// <summary>
		/// Vessels路径(末尾不带斜杠)
		/// </summary>
		private static string VesselsDir { get { return DMPDir + "\\Universe\\Vessels"; } }
		/// <summary>
		/// Kerbals路径(末尾不带斜杠)
		/// </summary>
		private static string KerbalsDir { get { return DMPDir + "\\Universe\\Kerbals"; } }
		/// <summary>
		/// Players路径(末尾不带斜杠)
		/// </summary>
		private static string PlayersDir { get { return DMPDir + "\\Universe\\Players"; } }
		/// <summary>
		/// Scenarios路径(末尾不带斜杠)
		/// </summary>
		private static string ScenariosDir { get { return DMPDir + "\\Universe\\Scenarios"; } }

		/// <summary>
		/// 飞船目录
		/// </summary>
		/// <returns></returns>
		public static DirectoryInfo GetVesselsFoder()
		{
			return new DirectoryInfo(VesselsDir);
		}
		/// <summary>
		/// 小绿人目录
		/// </summary>
		/// <returns></returns>
		public static DirectoryInfo GetKerbalsFoder()
		{
			return new DirectoryInfo(KerbalsDir);
		}
		/// <summary>
		/// 玩家目录
		/// </summary>
		/// <returns></returns>
		public static DirectoryInfo GetPlayersFoder()
		{
			return new DirectoryInfo(PlayersDir);
		}
		/// <summary>
		/// 场景目录
		/// </summary>
		/// <returns></returns>
		public static DirectoryInfo GetScenariosFoder()
		{
			return new DirectoryInfo(ScenariosDir);
		}

		/// <summary>
		/// 获取场景文件路径
		/// </summary>
		/// <param name="playerName">玩家名</param>
		/// <param name="scenarioType">场景类型</param>
		/// <returns></returns>
		public static string GetScenarioFilePath(string playerName, Data.Constants.ScenarioType scenarioType)
		{
			return ScenariosDir + "\\" + playerName + "\\" + scenarioType.ToString() + ".txt";
        }

		/// <summary>
		/// 获取某个玩家的场景文本
		/// </summary>
		/// <param name="playerName">玩家名</param>
		/// <param name="scenarioType">场景类型</param>
		/// <returns></returns>
		public static string GetScenarioFileContent(string playerName, Data.Constants.ScenarioType scenarioType)
		{
			return System.IO.File.ReadAllText(GetScenarioFilePath(playerName, scenarioType), Encoding.UTF8);
		}
	}
}
