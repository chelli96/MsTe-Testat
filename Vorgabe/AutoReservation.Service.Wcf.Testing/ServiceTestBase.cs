using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
			List<AutoDto> autos = Target.Autos;
	        Assert.AreEqual(3, autos.Count);
		}

        [TestMethod]
        public void GetKundenTest()
        {
	        List<KundeDto> kunden = Target.Kunde;
			Assert.AreEqual(4, kunden.Count);
		}

        [TestMethod]
        public void GetReservationenTest()
        {
			List<ReservationDto> reservtionen = Target.Reservation;
	        Assert.AreEqual(3, reservtionen.Count);
		}

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
           Assert.AreEqual(Target.GetAutoById(3).Marke, "Audi S6");
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            Assert.AreEqual(Target.GetKundeById(1).Nachname, "Nass");
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            Assert.AreEqual(Target.GetReservationByReservationsNr(3).Auto.Marke, "Audi S6");
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetAutoById(99));
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
			Assert.IsNull(Target.GetKundeById(99));
		}

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
			Assert.IsNull(Target.GetReservationByReservationsNr(99));
		}

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
			AutoDto neuesAuto = new AutoDto{ Id = 4, Marke = "Porsche 911", Basistarif = 100,  AutoKlasse = 0, Tagestarif = 75};
	        Target.InsertAuto(neuesAuto);
			Assert.AreEqual(Target.GetAutoById(4).Marke, "Porsche 911" );
        }

        [TestMethod]
        public void InsertKundeTest()
        {
	        KundeDto neuerKunde = new KundeDto{Id = 5, Geburtsdatum = new DateTime(1996, 11, 21), Vorname = "Hans", Nachname = "Lustig"};
			Target.InsertKunde(neuerKunde);
			Assert.AreEqual(Target.GetKundeById(5).Nachname, "Lustig");
        }

        [TestMethod]
        public void InsertReservationTest()
        {

			KundeDto kunde = Target.GetKundeById(1);
	        AutoDto auto = Target.GetAutoById(1);
	        ReservationDto reservationdto = new ReservationDto { ReservationsNr = 4, Von = new DateTime(2017, 12, 01), Bis = new DateTime(2018, 01, 01), Auto = auto, Kunde = kunde };
	        Target.InsertReservation(reservationdto);
	        Assert.AreEqual(Target.GetReservationByReservationsNr(4).Auto.Marke, "Fiat Punto");
		}

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
	        AutoDto zulöschendesAuto = Target.GetAutoById(3);
			Target.DeleteAuto(zulöschendesAuto);
            Assert.IsNull(Target.GetAutoById(3));
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
	        KundeDto zulöschenderKunde = Target.GetKundeById(2);
			Target.DeleteKunde(zulöschenderKunde);
            Assert.IsNull(Target.GetKundeById(2));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
	        ReservationDto zulöschendeReservation = Target.GetReservationByReservationsNr(3);
			Target.DeleteReservation(zulöschendeReservation);
	        Assert.IsNull(Target.GetReservationByReservationsNr(3));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
	        AutoDto updateAuto = Target.GetAutoById(1);
	        updateAuto.Marke = "Ferrari Enzo";
	        Target.UpdateAuto(updateAuto);
            Assert.AreEqual(Target.GetAutoById(1).Marke, "Ferrari Enzo");

		}

        [TestMethod]
        public void UpdateKundeTest()
        {
	        KundeDto updateKunde = Target.GetKundeById(1);
	        updateKunde.Nachname = "Superman";
	        Target.UpdateKunde(updateKunde);
	        Assert.AreEqual(Target.GetKundeById(1).Nachname, "Superman");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
	        ReservationDto updateReservation = Target.GetReservationByReservationsNr(2);
			updateReservation.Bis = new DateTime(2027, 3, 21);
	        Target.UpdateReservation(updateReservation);
			Assert.AreEqual(Target.GetReservationByReservationsNr(2).Bis, new DateTime(2027, 3, 21));
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod, ExpectedException(typeof(FaultException<AutoDto>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
			AutoDto updateAuto1 = Target.GetAutoById(3);
	        updateAuto1.Marke = "Bentley Continental";
	        AutoDto updateAuto2 = Target.GetAutoById(3);
	        updateAuto2.Marke = "Tesla Model S";
	        Target.UpdateAuto(updateAuto1);
	        Target.UpdateAuto(updateAuto2);
		}

        [TestMethod, ExpectedException(typeof(FaultException<KundeDto>))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
	        KundeDto updateKunde1 = Target.GetKundeById(1);
	        updateKunde1.Nachname = "Hugetobler";
	        KundeDto updateKunde2 = Target.GetKundeById(1);
	        updateKunde2.Nachname = "Ibisevic";
	        Target.UpdateKunde(updateKunde1);
	        Target.UpdateKunde(updateKunde2);
		}

        [TestMethod, ExpectedException(typeof(FaultException<ReservationDto>))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
	        ReservationDto updateReservation1 = Target.GetReservationByReservationsNr(2);
	        updateReservation1.Kunde.Vorname = "Samuel";
	        ReservationDto updateReservation2 = Target.GetReservationByReservationsNr(2);
	        updateReservation2.Kunde.Nachname = "Peterson";
	        Target.UpdateReservation(updateReservation1);
	        Target.UpdateReservation(updateReservation2);
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
	        KundeDto kunde = Target.GetKundeById(1);
	        AutoDto auto = Target.GetAutoById(1);
	        ReservationDto availabilityCheck = new ReservationDto { ReservationsNr = 7, Von = new DateTime(2018, 03, 19), Bis = new DateTime(2018, 03, 21), Auto = auto, Kunde = kunde };


			Target.InsertReservation(availabilityCheck);
	        Assert.AreEqual(Target.GetReservationByReservationsNr(7).Bis, new DateTime(2018, 3, 21));

        }

        [TestMethod, ExpectedException(typeof(AutoUnavailableException))]
        public void CheckAvailabilityIsFalseTest()
        {
			ReservationDto availabilityCheck = new ReservationDto
	        {
		        Auto = Target.GetAutoById(3),
		        Kunde = Target.GetKundeById(1),
		        Von = new DateTime(2020, 1, 15),
		        Bis = new DateTime(2020, 1, 21)
	        };

	       Target.InsertReservation(availabilityCheck);
		}

        #endregion
    }
}
