﻿using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{

    public enum AutoKlasse
    {
        [EnumMember]
        Luxusklasse = 0,
        [EnumMember]
        Mittelklasse = 1,
        [EnumMember]
        Standard = 2
    }

}
    

