using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationDateRangeTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioOkay01Test()
        {
			Auto a1 = new MittelklasseAuto();
	        a1.Marke = "Ford Fiesta";
	        a1.Tagestarif = 230;


	        Kunde k1 = new Kunde();
	        k1.Nachname = "Trump";
	        k1.Vorname = "Donald";
	        k1.Geburtsdatum = new DateTime(1942, 8, 1);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2018, 7, 21);
	        r1.Bis = new DateTime(2018, 8, 2);

	        Target.InstertReservation(r1);
			Assert.IsTrue(r1.ReservationsNr > 0);
		}

        [TestMethod]
        public void ScenarioOkay02Test()
        {
			Auto a1 = new MittelklasseAuto();
	        a1.Marke = "SmartFor2";
	        a1.Tagestarif = 195;


	        Kunde k1 = new Kunde();
	        k1.Nachname = "Merkel";
	        k1.Vorname = "Angel";
	        k1.Geburtsdatum = new DateTime(1978, 8, 1);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2019, 12, 30);
	        r1.Bis = new DateTime(2020, 1, 1);

	        Target.InstertReservation(r1);
	        Assert.IsTrue(r1.ReservationsNr > 0);
		}

        [TestMethod, ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay01Test()
        {
			Auto a1 = new StandardAuto();
	        a1.Marke = "Nissan Leaf";
	        a1.Tagestarif = 69;


	        Kunde k1 = new Kunde();
	        k1.Nachname = "Elon";
	        k1.Vorname = "Musk";
	        k1.Geburtsdatum = new DateTime(1942, 8, 1);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2018, 7, 21);
	        r1.Bis = new DateTime(2018, 5, 2);

	        Target.InstertReservation(r1);

		}

        [TestMethod, ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay02Test()
        {
			Auto a1 = new MittelklasseAuto();
	        a1.Marke = "Range Rover";
	        a1.Tagestarif = 125;


	        Kunde k1 = new Kunde();
	        k1.Nachname = "Simpson";
	        k1.Vorname = "Homer Jay";
	        k1.Geburtsdatum = new DateTime(1935, 7, 11);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2018, 7, 21);
	        r1.Bis = new DateTime(2018, 7, 21);

	        Target.InstertReservation(r1);
		}

        [TestMethod, ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay03Test()
        {
			Auto a1 = new LuxusklasseAuto();
	        a1.Marke = "Aston Martin DB9";
	        a1.Tagestarif = 175;


	        Kunde k1 = new Kunde();
	        k1.Nachname = "Bond";
	        k1.Vorname = "James";
	        k1.Geburtsdatum = new DateTime(1975, 11, 30);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2018, 7, 21);
	        r1.Bis = new DateTime(2010, 5, 2);

	        Target.InstertReservation(r1);
		}
    }
}
