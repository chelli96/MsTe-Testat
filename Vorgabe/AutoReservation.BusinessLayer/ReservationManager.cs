using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

                return context.Reservationen.SingleOrDefault(r => r.Id == id);
            }
        }

        public Reservation InstertReservation(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Reservationen.Add(reservation);
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
                context.SaveChanges();
            }
        }
    }
}