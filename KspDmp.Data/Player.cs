using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspDmp.Data
{
	/// <summary>
	/// 玩家类
	/// </summary>
	public class Player
	{
		/// <summary>
		/// 昵称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 资金
		/// </summary>
		public double Fund { get; set; }

		/// <summary>
		/// 科研点
		/// </summary>
		public double Science { get; set; }

		/// <summary>
		/// 声望
		/// </summary>
		public double Reputation { get; set; }

		/// <summary>
		/// RSA秘钥
		/// </summary>
		public string RSAKey { get; set; }

		/// <summary>
		/// 是否管理员
		/// </summary>
		public bool IsAdmin { get; set; }

		/// <summary>
		/// 获取所有玩家
		/// </summary>
		/// <returns></returns>
		public static List<Player> GetAllPlayers()
		{
			var playerList = new List<Player>();
			var playerNameList = new List<string>();
			//TODO:添加所有玩家至列表
			var playersDir = Data.Path.GetPlayersFoder();
			var scenariosDir = Data.Path.GetScenariosFoder();

			//玩家名列表
			foreach (var playerName in playersDir.GetFiles())
			{
				playerNameList.Add(playerName.Name.Split('.').FirstOrDefault());
			}

			foreach (var playerName in playerNameList)
			{
				Player player = new Player();

				var FundingScenario = Data.Path.GetScenarioFileContent(playerName, Constants.ScenarioType.Funding);

				player.Fund = Data.ScenarioParser.FundParser.GetFundValue(FundingScenario);
				player.Name = playerName;

				playerList.Add(player);
			}
			return playerList;
		}

		//更新玩家数据
		public static bool UpdatePlayer(Player player)
		{
			try
			{
				var text = Path.GetScenarioFileContent(player.Name, Constants.ScenarioType.Funding);
				//TODO:判断该玩家是否在线 未在线才进行处理

				//设置资金值
				var fileStr = Path.GetScenarioFileContent(player.Name, Constants.ScenarioType.Funding);
				var filePath = Path.GetScenarioFilePath(player.Name, Constants.ScenarioType.Funding);

				Data.ScenarioParser.FundParser.SetFundValue(fileStr, filePath, player.Fund);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
