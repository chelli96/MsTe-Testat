using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationAvailabilityTest
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
            Auto a1 = new LuxusklasseAuto();
	        a1.Marke = "Mercedes-Maybach 6";
	        a1.Tagestarif = 450;
			

			Kunde k1 = new Kunde();
	        k1.Nachname = "Blocher";
	        k1.Vorname = "Christoph";
			k1.Geburtsdatum = new DateTime(1921, 8, 1);

			Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
			r1.Von = new DateTime(2018, 7, 21);
			r1.Bis = new DateTime(2018, 8, 2);

	        Target.InstertReservation(r1);

			Kunde k2 = new Kunde();
	        k2.Nachname = "Glättli";
	        k2.Vorname = "Balthasar";
			k2.Geburtsdatum = new DateTime(1993, 11, 1);

			Reservation r2 = new Reservation();
	        r2.Auto = a1;
	        r2.Kunde = k2;
			r2.Von = new DateTime(2018, 8, 3);
			r2.Bis = new DateTime(2018, 8, 5);

	        Target.InstertReservation(r2);
		

			Assert.IsTrue(r2.ReservationsNr > 0);


		}

        [TestMethod]
        public void ScenarioOkay02Test()
        {
			Auto a1 = new StandardAuto();
	        a1.Marke = "VW Polo";
	        a1.Tagestarif = 150;

	        Kunde k1 = new Kunde();
	        k1.Nachname = "Del Curto";
	        k1.Vorname = "Arno";
	        k1.Geburtsdatum = new DateTime(1921, 12, 3);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2017, 12, 21);
	        r1.Bis = new DateTime(2018, 2, 15);

	        Target.InstertReservation(r1);

	        Kunde k2 = new Kunde();
	        k2.Nachname = "Genoni";
	        k2.Vorname = "Leonardo";
	        k2.Geburtsdatum = new DateTime(1989, 3, 21);

	        Reservation r2 = new Reservation();
	        r2.Auto = a1;
	        r2.Kunde = k2;
	        r2.Von = new DateTime(2018, 2, 16);
	        r2.Bis = new DateTime(2018, 8, 5);

	        Target.InstertReservation(r2);

	        Assert.IsTrue(r2.ReservationsNr > 0);

		}
        
        [TestMethod, ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay01Test()
        {
			Auto a1 = new StandardAuto();
	        a1.Marke = "Audi TT";
	        a1.Tagestarif = 150;

	        Kunde k1 = new Kunde();
	        k1.Nachname = "Paris";
	        k1.Vorname = "Hilton";
	        k1.Geburtsdatum = new DateTime(1987, 12, 3);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2017, 12, 21);
	        r1.Bis = new DateTime(2018, 1, 11);

	        Target.InstertReservation(r1);

	        Kunde k2 = new Kunde();
	        k2.Nachname = "Martullo-Blocher";
	        k2.Vorname = "Magdalena";
	        k2.Geburtsdatum = new DateTime(1981, 8, 21);

	        Reservation r2 = new Reservation();
	        r2.AutoId = a1.Id;
	        r2.Kunde = k2;
	        r2.Von = new DateTime(2018, 1, 8);
	        r2.Bis = new DateTime(2018, 2, 5);

	        Target.InstertReservation(r2);
		}

        [TestMethod, ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay02Test()
        {
			Auto a1 = new StandardAuto();
	        a1.Marke = "Ford Mustang";
	        a1.Tagestarif = 275;

	        Kunde k1 = new Kunde();
	        k1.Nachname = "Schiffer";
	        k1.Vorname = "Claudia";
	        k1.Geburtsdatum = new DateTime(1987, 12, 3);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2020, 12, 21);
	        r1.Bis = new DateTime(2020, 1, 11);

	        Target.InstertReservation(r1);

	        Kunde k2 = new Kunde();
	        k2.Nachname = "Oliver";
	        k2.Vorname = "John";
	        k2.Geburtsdatum = new DateTime(1981, 8, 21);

	        Reservation r2 = new Reservation();
	        r2.AutoId = a1.Id;
	        r2.Kunde = k2;
	        r2.Von = new DateTime(2020, 1, 8);
	        r2.Bis = new DateTime(2020, 2, 5);

	        Target.InstertReservation(r2);
		}

        [TestMethod, ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay03Test()
        {
			Auto a1 = new StandardAuto();
	        a1.Marke = "BMW M3";
	        a1.Tagestarif = 150;

	        Kunde k1 = new Kunde();
	        k1.Nachname = "Hintersee";
	        k1.Vorname = "Hansi";
	        k1.Geburtsdatum = new DateTime(1940, 12, 3);

	        Reservation r1 = new Reservation();
	        r1.Auto = a1;
	        r1.Kunde = k1;
	        r1.Von = new DateTime(2017, 12, 21);
	        r1.Bis = new DateTime(2018, 1, 11);

	        Target.InstertReservation(r1);

	        Kunde k2 = new Kunde();
	        k2.Nachname = "Forlin";
	        k2.Vorname = "Jan";
	        k2.Geburtsdatum = new DateTime(1995, 2, 21);

	        Reservation r2 = new Reservation();
	        r2.AutoId = a1.Id;
	        r2.Kunde = k2;
	        r2.Von = new DateTime(2018, 1, 12);
	        r2.Bis = new DateTime(2018, 2, 5);

	        Target.InstertReservation(r2);

			r2.Von = new DateTime(2018, 1, 1);

	        Target.UpdateReservation(r2);
        }
    }
}
