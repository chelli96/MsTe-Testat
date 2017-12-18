using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
	[TestClass]
	public class AutoUpdateTests
	{
		private AutoManager target;
		private AutoManager Target => target ?? (target = new AutoManager());


		[TestInitialize]
		public void InitializeTestData()
		{
			TestEnvironmentHelper.InitializeTestData();
		}

		[TestMethod]
		public void UpdateAutoTest()
		{
			Auto myAuto = Target.getAutoByID(1);
			myAuto.Marke = "VW Polo";
			Target.UpdateAuto(myAuto);

			Auto myUpdatedAuto = Target.getAutoByID(1);
			Assert.AreEqual(myUpdatedAuto.Marke, "VW Polo");
		}
	}
}
