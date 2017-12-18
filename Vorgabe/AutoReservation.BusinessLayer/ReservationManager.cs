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
				using (var context = new AutoReservationContext())
				{
					List<Reservation> allReservationen = (from reservationen in context.Reservationen
								.Include(r => r.Auto)
								.Include(r => r.Kunde)
														  select reservationen)
						.ToList<Reservation>();
					return allReservationen;
				}

			}
		}

		public Reservation getReservationByReservationsNr(int reservationsnr)
		{
			using (var context = new AutoReservationContext())
			{

				Reservation reservation = context.Reservationen
					.Include(r => r.Auto)
					.Include(r => r.Kunde)
					.SingleOrDefault(r => r.ReservationsNr == reservationsnr);
				return reservation;
			}
		}

		public void CheckDateRange(Reservation reservation)
		{
			if (reservation.Von.AddDays(1) > reservation.Bis)
			{
				throw new InvalidDateRangeException("Mindestdauer der Reservation ist 24 Studnen");
			}

			else if (reservation.Von > reservation.Bis)
			{
				throw new InvalidDateRangeException("Das Bis Datum muss grösser sein als das Von Datum");
			}
		}

		public void CheckAutoAvailability(AutoReservationContext context, Reservation reservation)
		{
			if ((from r in context.Reservationen
				where ((r.Von <= reservation.Von && r.Bis > reservation.Von) ||
				       (r.Von < reservation.Bis && r.Bis > reservation.Bis)) &&
						r.AutoId.Equals(reservation.AutoId) && (r.ReservationsNr != reservation.ReservationsNr)
				select r).Any())
			{
				throw new AutoUnavailableException("Das gewünschte Auto ist in diesem Datumsbereich bereits vergeben");
			}
		}

		public Reservation InstertReservation(Reservation reservation)
		{
			CheckDateRange(reservation);
			using (var context = new AutoReservationContext())
			{
				CheckAutoAvailability(context, reservation);

				context.Reservationen.Add(reservation);
				context.Entry(reservation).State = EntityState.Added;
				context.SaveChanges();
			}
			return reservation;
		}

		public Reservation UpdateReservation(Reservation reservation)
		{
			CheckDateRange(reservation);
			using (var context = new AutoReservationContext())
			{
				CheckAutoAvailability(context, reservation);

				try
				{
					context.Reservationen.Attach(reservation);
					context.Entry(reservation).State = EntityState.Modified;
					context.SaveChanges();
					return reservation;
				}
				catch (DbUpdateConcurrencyException)
				{
					throw CreateOptimisticConcurrencyException<Reservation>(context, reservation);
				}
			}
		}

		public void DeleteReservation(Reservation reservation)
		{
			using (var context = new AutoReservationContext())
			{
				var query = (from r in context.Reservationen
							 where r.ReservationsNr == reservation.ReservationsNr
							 select r).FirstOrDefault();
				context.Reservationen.Remove(query);
				context.Entry(query).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public bool IsAvailable(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				try
				{
					CheckAutoAvailability(context, reservation);
					return true;
				}
				catch (AutoUnavailableException)
				{
					return false;
				}
			}
		}
	}
}