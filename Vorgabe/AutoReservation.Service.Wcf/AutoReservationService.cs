using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private AutoManager serviceAuto;
        private KundeManager serviceKunde;
        private ReservationManager serviceReservation;
        private AutoManager ServiceAuto
        {
            get
            {
                if (serviceAuto == null)
                {
                    serviceAuto = new AutoManager();
                }
                return serviceAuto;
            }
        }
        private KundeManager ServiceKunde
        {
            get
            {
                if (serviceKunde == null)
                {
                    serviceKunde = new KundeManager();
                }
                return serviceKunde;
            }
        }
        private ReservationManager ServiceReservation
        {
            get
            {
                if (serviceReservation == null)
                {
                    serviceReservation = new ReservationManager();
                }
                return serviceReservation;
            }
        }

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(ServiceAuto.Autos);
            }
        }

        public List<KundeDto> Kunde
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(ServiceKunde.Kunden);
            }
        }

        public List<ReservationDto> Reservation
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(ServiceReservation.Reservationen);
            }
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(ServiceAuto.getAutoByID(id));
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(ServiceKunde.getKundeByID(id));
        }

        public ReservationDto GetReservationByReservationsNr(int reservationsnr)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(ServiceReservation.getReservationByReservationsNr(reservationsnr));
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            try
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDto(ServiceAuto.UpdateAuto(DtoConverter.ConvertToEntity(auto)));
            }
            catch (OptimisticConcurrencyException<Auto> ex)
            {
                throw new FaultException<AutoDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            try
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDto(ServiceKunde.UpdateKunde(DtoConverter.ConvertToEntity(kunde)));
            }
            catch (OptimisticConcurrencyException<Kunde> ex)
            {
                throw new FaultException<KundeDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }

        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDto(ServiceReservation.UpdateReservation(DtoConverter.ConvertToEntity(reservation)));
            }
            catch (OptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException<ReservationDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
            catch (BusinessLayer.Exceptions.InvalidDateRangeException ex)
            {
                var invEx = new Common.Interfaces.Faults.InvalidDateRangeException(ex.Message);
                throw new FaultException<Common.Interfaces.Faults.InvalidDateRangeException>(invEx);
            }
            catch (BusinessLayer.Exceptions.AutoUnavailableException ex)
            {
                var unavEx = new Common.Interfaces.Faults.AutoUnavailableException(ex.Message);
                throw new FaultException<Common.Interfaces.Faults.AutoUnavailableException>(unavEx);
            }

        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            ServiceAuto.DeleteAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            ServiceKunde.DeleteKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ServiceReservation.DeleteReservation(DtoConverter.ConvertToEntity(reservation));
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(ServiceAuto.InsertAuto(DtoConverter.ConvertToEntity(auto)));
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(ServiceKunde.InsertKunde(DtoConverter.ConvertToEntity(kunde)));
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(ServiceReservation.InstertReservation(DtoConverter.ConvertToEntity(reservation)));
            }
            catch (OptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException<ReservationDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
            catch (BusinessLayer.Exceptions.InvalidDateRangeException ex)
            {
                var invEx = new Common.Interfaces.Faults.InvalidDateRangeException(ex.Message);
                throw new FaultException<Common.Interfaces.Faults.InvalidDateRangeException>(invEx);
            }
            catch (BusinessLayer.Exceptions.AutoUnavailableException ex)
            {
                var unavEx = new Common.Interfaces.Faults.AutoUnavailableException(ex.Message);
                throw new FaultException<Common.Interfaces.Faults.AutoUnavailableException>(unavEx);
            }

        }

        public bool IsAutoAvailable(ReservationDto reservation)
        {
            {
                WriteActualMethod();
                return ServiceReservation.IsAutoAvailable(reservation.ConvertToEntity());
            }
        }
    }
}