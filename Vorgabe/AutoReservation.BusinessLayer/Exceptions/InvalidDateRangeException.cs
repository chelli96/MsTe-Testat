using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException<T> : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
        public InvalidDateRangeException(string message, T mergedEntity) : base(message)
        {
            MergedEntity = mergedEntity;
        }

        public T MergedEntity { get; set; }
    }
}