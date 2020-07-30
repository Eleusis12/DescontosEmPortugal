using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKuantoKustaScrapper.Models
{
	public class ChartJS
	{
		public string type { get; set; }
		public Data data { get; set; }
		public Options options { get; set; }
	}

	public class Data
	{
		public string[] labels { get; set; }
		public Dataset[] datasets { get; set; }
	}

	public class Dataset
	{
		public string label { get; set; }
		public float[] data { get; set; }
		public string[] backgroundColor { get; set; }
		public string[] borderColor { get; set; }
		public int borderWidth { get; set; }
	}

	public class Options
	{
		public bool responsive { get; set; }
	}
}