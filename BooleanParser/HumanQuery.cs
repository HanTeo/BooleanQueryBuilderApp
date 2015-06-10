using System.Collections.Generic;

namespace BooleanParser
{
	public class HumanQuery
	{
		public string Topic { get; set; }
		public string SubTopic { get; set; }
		public string Tag { get; set; }
		public IEnumerable<string> Brands { get; set; }
		public IEnumerable<string> Qualifiers { get; set; }
		public IEnumerable<string> Exclusions { get; set; }
	}
}
