using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        // Example
        public List<Auto> Autos
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

        public Auto getAutoByID(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {

                return context.Autos.SingleOrDefault(a => a.Id == id);
            }
        }

        public Auto InsertAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Autos.Add(auto);
	            context.Entry(auto).State = EntityState.Added;
				context.SaveChanges();
                return auto;
            }
        }

        public void UpdateAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Autos.Attach(auto);
                    context.Entry(auto).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    CreateOptimisticConcurrencyException(context, auto);
                }
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Autos.Attach(auto);
                context.Autos.Remove(auto);
	            context.Entry(auto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}