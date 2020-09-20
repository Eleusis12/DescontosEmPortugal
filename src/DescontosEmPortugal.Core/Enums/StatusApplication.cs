using System;
using System.Collections.Generic;
using System.Text;

namespace DescontosEmPortugal.Core.Enums
{
	public enum StatusApplication : ushort
	{
		Started,
		StartedScrapping,
		NoWebsiteFound,
		WebsitesFound,
		ConnectionLost,
		DataBaseUpdated,
		FinishedScrapping,
		Terminated,
		Other
	}
}