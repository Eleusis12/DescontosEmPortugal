using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Web.Models
{
	public class ChartJS
	{
		public string type { get; set; }
		public Data data { get; set; }
		public Options options { get; set; }
	}

	public class Data
	{
		public Dataset[] datasets { get; set; }
	}

	public class Dataset
	{
		public string label { get; set; }
		public Datum[] data { get; set; }
		public string[] backgroundColor { get; set; }
		public string[] borderColor { get; set; }
		public int borderWidth { get; set; }
	}

	public class Datum
	{
		public string x { get; set; }

		public float y { get; set; }
	}

	public class Options
	{
		public Scales scales { get; set; }
		public bool responsive { get; set; }
	}

	public class Scales
	{
		public Xax[] xAxes { get; set; }
	}

	public class Xax
	{
		public string type { get; set; }

		public Time time { get; set; }
	}

	public class Time
	{
		public string unit { get; set; }
		public int unitStepSize { get; set; }
		public DisplayFormats displayFormats { get; set; }
	}

	public class DisplayFormats
	{
		public string day { get; set; }
	}
}