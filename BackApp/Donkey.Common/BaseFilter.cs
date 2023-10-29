using System.Collections.Generic;

namespace Donkey.Common
{
	public class BaseFilter
	{
		public bool WithCount { get; set; }
		public int? Take { get; set; }
		public int? Skip { get; set; }
		public List<Order> Order { get; set; }
	}

	/// <summary>
	/// Order
	/// </summary>
	public class Order
	{
		/// <summary>
		/// Field
		/// </summary>
		public string Field { get; set; }
		/// <summary>
		/// Sort by desc?
		/// </summary>
		public bool? Desc { get; set; }
	}
}
