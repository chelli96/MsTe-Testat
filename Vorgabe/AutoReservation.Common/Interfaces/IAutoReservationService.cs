using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces.Faults;

namespace AutoReservation.Common.Interfaces
{

	[ServiceContract]
	public interface IAutoReservationService
    {

	    List<AutoDto> Autos { [OperationContract] get; }

	    List<KundeDto> Kunde { [OperationContract] get; }

		List<ReservationDto> Reservation { [OperationContract] get; }

	    [OperationContract]
	    AutoDto GetAutoById(int id);

	    [OperationContract]
	    KundeDto GetKundeById(int id);

	    [OperationContract]
	    ReservationDto GetReservationByReservationsNr(int reservationsnr);

	    [OperationContract, FaultContract(typeof(AutoDto))]
	    AutoDto UpdateAuto(AutoDto auto);

	    [OperationContract, FaultContract(typeof(KundeDto))]
	    KundeDto UpdateKunde(KundeDto kunde);

	    [OperationContract]
		[FaultContract(typeof(ReservationDto))]
	    [FaultContract(typeof(InvalidDateRangeException))]
	    [FaultContract(typeof(AutoUnavailableException))]
		ReservationDto UpdateReservation(ReservationDto reservation);

	    [OperationContract]
	    void DeleteAuto(AutoDto auto);

	    [OperationContract]
	    void DeleteKunde(KundeDto kunde);

	    [OperationContract]
	    void DeleteReservation(ReservationDto reservation);

	    [OperationContract]
	    AutoDto InsertAuto(AutoDto auto);

	    [OperationContract]
	    KundeDto InsertKunde(KundeDto kunde);

	    [OperationContract]

	    ReservationDto InsertReservation(ReservationDto reservation);

	    [FaultContract(typeof(InvalidDateRangeException))]
	    [FaultContract(typeof(AutoUnavailableException))]

	    [OperationContract]
	    bool IsAutoAvailable(ReservationDto reservation);


	}
}
