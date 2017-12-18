using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

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

	    [OperationContract, FaultContract(typeof(ReservationDto))]
	    ReservationDto UpdateReservation(ReservationDto reservation);

	    [OperationContract]
	    void DeleteAuto(AutoDto auto);

	    [OperationContract]
	    void DeleteKunde(KundeDto kunde);

	    [OperationContract]
	    void DeleteReservation(ReservationDto reservation);

	    [OperationContract]
	    void InsertAuto(AutoDto auto);

	    [OperationContract]
	    void InsertKunde(KundeDto kunde);

	    [OperationContract]
	    void InsertReservation(ReservationDto reservation);
	}
}
