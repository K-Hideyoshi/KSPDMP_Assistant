using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KspDmp.Assistant.Models;
using KspDmp.Data;

namespace KspDmp.Assistant.Controllers
{
    public class FundController : Controller
    {
		/// <summary>
		/// 财政拨款
		/// </summary>
		/// <returns></returns>
		public ActionResult Funding()
		{
			FundingModel model = new FundingModel();
			//获取所有玩家
			var playerList = Player.GetAllPlayers();
			//TODO:筛选合适条件的玩家

			//将玩家加入模型
			model.Players = new List<PlayerFunding>();
			foreach (var player in playerList)
			{
				var funding = new PlayerFunding();
				funding.Player = player;
				funding.FundAmount = 0;
				funding.FundingStatus = 0;
				model.Players.Add(funding);
			}

            return View(model);
		}

		/// <summary>
		/// 财政拨款提交
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ActionName("Funding")]
		public ActionResult FundingPost(FundingModel model)
		{
			var originPlayerList = Player.GetAllPlayers();
			var newPlayerList = Player.GetAllPlayers();
			try
			{
				foreach (var player in newPlayerList)
				{
					
					var modelPlayer = model.Players.Find(m => m.Player.Name == player.Name);

					if (modelPlayer.FundAmount == 0)
						continue;

					player.Fund = (player.Fund + modelPlayer.FundAmount) < 0 ? 0 : player.Fund + modelPlayer.FundAmount;
					var updateResult = Player.UpdatePlayer(player);

					modelPlayer.FundingStatus = updateResult ? 1 : -1;
					modelPlayer.Player = player;
				}
			}
			catch (Exception ex)
			{
				//TODO:回滚还原数据
				foreach (var player in originPlayerList)
				{
					Player.UpdatePlayer(player);
                }
			}
			return View(model);
		}
    }
}