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
				using (var context = new AutoReservationContext())
				{
					List<Auto> allAutos = (from autos in context.Autos
										   select autos)
						.ToList<Auto>();
					return allAutos;

				}

			}
		}

		public Auto getAutoByID(int id)
		{
			using (var context = new AutoReservationContext())
			{

				var query = from auto in context.Autos
							where auto.Id == id
							select auto;
				return query.FirstOrDefault();
			}
		}

		public Auto InsertAuto(Auto auto)
		{
			using (var context = new AutoReservationContext())
			{
				context.Autos.Add(auto);
				context.Entry(auto).State = EntityState.Added;
				context.SaveChanges();

			}
			return auto;
		}

		public Auto UpdateAuto(Auto auto)
		{
			using (var context = new AutoReservationContext())
			{
				try
				{
					context.Autos.Attach(auto);
					context.Entry(auto).State = EntityState.Modified;
					context.SaveChanges();
					return auto;
				}
				catch (DbUpdateConcurrencyException)
				{
					throw CreateOptimisticConcurrencyException<Auto>(context, auto);
				}
			}
		}

		public void DeleteAuto(Auto auto)
		{
			using (var context = new AutoReservationContext())
			{
				var query = (from a in context.Autos
							 where a.Id == auto.Id
							 select a).FirstOrDefault();
				context.Autos.Remove(query);
				context.Entry(query).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

	}
}