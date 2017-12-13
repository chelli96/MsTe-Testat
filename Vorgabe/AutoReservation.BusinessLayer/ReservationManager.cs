using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
	public class ReservationManager
		: ManagerBase
	{
		public List<Reservation> Reservationen
		{
			get
			{
				using (AutoReservationContext context = new AutoReservationContext())
				{
					return context.Reservationen.
						Include(r => r.Auto).
						Include(r => r.Kunde).
						ToList();
				}

			}
		}

		public Reservation getReservationByID(int id)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{

				return context.Reservationen.SingleOrDefault(r => r.ReservationsNr == id);
			}
		}

		public Reservation InstertReservation(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				if (reservation.Von > reservation.Bis && (reservation.Bis.Hour - reservation.Von.Hour) < 24)
				{
					throw new InvalidDateRangeException<Reservation>("Datums Fehler");
				}

				//if (reservation.Auto.Reservationen.)
				//{
				//	throw new AutoUnavailableException<Reservation>("Auto ist nicht verfügbar");
				//}

				context.Reservationen.Add(reservation);
				context.Entry(reservation).State = EntityState.Added;
				context.SaveChanges();
				return reservation;
			}
		}

		public void UpdateReservationen(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				try
				{
					context.Reservationen.Attach(reservation);
					context.Entry(reservation).State = EntityState.Modified;
					context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					CreateOptimisticConcurrencyException(context, reservation);
				}
			}
		}

		public void DeleteReservation(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				context.Reservationen.Attach(reservation);
				context.Reservationen.Remove(reservation);
				context.Entry(reservation).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}
	}
}