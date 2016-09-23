using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KspDmp.Data;

namespace KspDmp.Assistant.Models
{
	/// <summary>
	/// 向玩家拨款模型
	/// </summary>
	public class FundingModel
	{
		/// <summary>
		/// 拨款类列表
		/// </summary>
		public List<PlayerFunding> Players { get; set; }
	}

	/// <summary>
	/// 向玩家拨款类
	/// </summary>
	public class PlayerFunding
	{
		public Player Player { get; set; }
		public double FundAmount { get; set; }
		/// <summary>
		/// 拨款状态
		/// 0待处理
		/// 1拨款成功
		/// -1拨款失败
		/// </summary>
		public int FundingStatus { get; set; }
	}
}