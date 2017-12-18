using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> Kunden
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    {
                        return context.Kunden.ToList();
                    }
                }

            }
        }

        public Kunde getKundeByID(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {

                return context.Kunden.SingleOrDefault(k => k.Id == id);
            }
        }

        public Kunde InsertKunde(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Kunden.Add(kunde);
	            context.Entry(kunde).State = EntityState.Added;
				context.SaveChanges();
                return kunde;
            }
        }

        public void UpdateKunden(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Kunden.Attach(kunde);
                    context.Entry(kunde).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    CreateOptimisticConcurrencyException(context, kunde);
                }
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Kunden.Attach(kunde);
                context.Kunden.Remove(kunde);
	            context.Entry(kunde).State = EntityState.Deleted;
				context.SaveChanges();
            }
        }
    }
}