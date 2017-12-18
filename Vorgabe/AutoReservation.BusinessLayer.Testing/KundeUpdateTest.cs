using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
	[TestClass]
	public class KundeUpdateTest
	{
		private KundeManager target;
		private KundeManager Target => target ?? (target = new KundeManager());


		[TestInitialize]
		public void InitializeTestData()
		{
			TestEnvironmentHelper.InitializeTestData();
		}

		[TestMethod]
		public void UpdateKundeTest()
		{
			Kunde kunde = Target.getKundeByID(1);
			kunde.Nachname = "Müller";
			Target.UpdateKunde(kunde);
			Kunde updatedKunde = Target.getKundeByID(1);
			Assert.AreEqual("Müller", updatedKunde.Nachname);
		}
	}
}
