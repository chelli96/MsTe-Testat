using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
	[DataContract]
	public class ConcurrencyException
	{
		public ConcurrencyException(String message)
		{
			Message = message;
		}

		[DataMember]
		public string Message { get; set; }

		public override string ToString()
		{
			return Message;
		}
	}
}
