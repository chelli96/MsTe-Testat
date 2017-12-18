using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
	[DataContract]
	public class AutoUnavailableException
	{
		public AutoUnavailableException(String message)
		{
			Message = message;
		}

		[DataMember]
		public String Message { get; set; }

		public override string ToString()
		{
			return Message;
		}

	}
}
