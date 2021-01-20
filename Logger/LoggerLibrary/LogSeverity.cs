using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BradyCorp.Log
{
	/// <summary>
	/// This class encapsulates the TraceEventType class 
	/// </summary>

	public enum LogSeverity
	{
		// Summary:
		//     Fatal error or application crash.
		Critical = 1,
		//
		// Summary:
		//     Recoverable error.
		Error = 2,
		//
		// Summary:
		//     Noncritical problem.
		Warning = 4,
		//
		// Summary:
		//     Informational message.
		Information = 8,
		//
		// Summary:
		//     Debugging trace.
		Verbose = 16,
	}
}
