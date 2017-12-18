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
				using (var context = new AutoReservationContext())
				{
					{
						List<Kunde> allKunden = (from kunden in context.Kunden
												 select kunden)
							.ToList<Kunde>();
						return allKunden;
					}
				}

			}
		}

		public Kunde getKundeByID(int id)
		{
			using (var context = new AutoReservationContext())
			{

				var query = from kunde in context.Kunden
							where kunde.Id == id
							select kunde;
				return query.FirstOrDefault();
			}
		}

		public Kunde InsertKunde(Kunde kunde)
		{
			using (var context = new AutoReservationContext())
			{
				context.Kunden.Add(kunde);
				context.Entry(kunde).State = EntityState.Added;
				context.SaveChanges();
			}
			return kunde;
		}

		public Kunde UpdateKunde(Kunde kunde)
		{
			using (var context = new AutoReservationContext())
			{
				try
				{
					context.Kunden.Attach(kunde);
					context.Entry(kunde).State = EntityState.Modified;
					context.SaveChanges();
					return kunde;
				}
				catch (DbUpdateConcurrencyException)
				{
					throw CreateOptimisticConcurrencyException<Kunde>(context, kunde);
				}
			}
		}

		public void DeleteKunde(Kunde kunde)
		{
			using (var context = new AutoReservationContext())
			{
				var query = (from k in context.Kunden
							 where k.Id == kunde.Id
							 select k).FirstOrDefault();
				context.Kunden.Remove(query);
				context.Entry(query).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}
	}
}