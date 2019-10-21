
using System.Collections.Generic;

namespace Korea.Shared.Models
{
	public class FilterModel
	{
		public int Page { get; set; } = 1;
		public int Take { get; set; } = 2;
	}

	public class PaginationModel
	{		

		public int Page { get; set; } = 1;
		public int LastPage { get; set; } = 1;

		public bool EnabledNext => Page < LastPage;
		public bool EnabledPrev => Page > 1;
	}	

	public class Result<T>
	{
		public List<T> Data {get; set;} = new List<T>();
		public int Total { get; set; }
	}
}