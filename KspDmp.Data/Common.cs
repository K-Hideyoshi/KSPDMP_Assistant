using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KspDmp.Data
{
	public class CommonResult
	{
		/// <summary>
		/// 是否成功
		/// </summary>
		public bool IsSuccess { get; set; }
		/// <summary>
		/// 信息
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// 数据
		/// </summary>
		public object Data { get; set; }
	}
}
