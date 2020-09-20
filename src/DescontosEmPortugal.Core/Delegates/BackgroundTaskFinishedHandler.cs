using DescontosEmPortugal.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DescontosEmPortugal.Core.Delegates
{
	public delegate void BackgroundTaskFinishedHandler(StatusApplication status, string message);

	//public delegate void TaskFinishedHandler(StatusApplication message, string additionalMessage);
}