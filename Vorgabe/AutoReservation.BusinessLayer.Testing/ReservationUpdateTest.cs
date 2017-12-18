using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
	[TestClass]
	public class ReservationUpdateTest
	{
		private ReservationManager target;
		private ReservationManager Target => target ?? (target = new ReservationManager());


		[TestInitialize]
		public void InitializeTestData()
		{
			TestEnvironmentHelper.InitializeTestData();
		}

		[TestMethod]
		public void UpdateReservationTest()
		{
			Reservation reservation = Target.getReservationByReservationsNr(3);
			reservation.Bis = new DateTime(2020, 01, 22);
			Target.UpdateReservation(reservation);
			Reservation updatedReservation = Target.getReservationByReservationsNr(3);
			Assert.AreEqual(new DateTime(2020, 01, 22), updatedReservation.Bis);
		}
	}
}
