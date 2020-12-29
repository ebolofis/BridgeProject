using AutoMapper;
using Dapper;
using Hit.Scheduler.Jobs.Corissia;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.ProtelExport;
using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.MainLogic.Flows.Corissia;
using Hit.Services.MainLogic.Tasks.SQLJobs;
using Hit.Services.Models.Models;
using Hit.Services.Models.Models.Protel;
using HitTestWebApi.Models.Gorissia;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class CorissiaAPITest
    {
        CorissiaDataRetrievingJob Api;
        public CorissiaAPITest()
        {

        }

        ConfigHelper ch;
        string basePath = @"c:\logs\";

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        [Test, Order(200)]
        public void CorissiaDataRetrieving_Execute()
        {
            //1. create setting  file
            string Settingsfile = "Corissia";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";
            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. insert test data to [proteluser].[HitServices_kunden], [proteluser].[HitServices_Buch], [proteluser].[HitService_leist]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                int count = db.Execute(@"INSERT INTO [proteluser].[HitServices_kunden] ([protelId],[typeId],[actionId]) VALUES (23,1,1)");
                Assert.AreEqual(1, count);
            }

            //3. run job
            Api = new CorissiaDataRetrievingJob();
            Assert.DoesNotThrow(() => Api.Execute(Settingsfile, "CorissiaProfile", "TestInstallation"));

            //4. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);

        }

        [Test, Order(201)]
        public void CorissiaExecute()
        {
            //1. create setting  file
            string Settingsfile = "Corissia";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";
            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. run job
            Api = new CorissiaDataRetrievingJob();
            Assert.DoesNotThrow(() => Api.Execute(Settingsfile, "CorissiaProfile", "TestInstallation"));

            //3. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);
        }

        [Test, Order(202)]
        public void CorissiaReservations_CheckNewTables()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            ReservationsExportDTs Tasks;

            //Call Main Logic
            Tasks = new ReservationsExportDTs(sm);
            Tasks.CreateTables();
        }


        [Test, Order(203)]
        public void CorissiaReservations_Execute()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            //Create Tables
            ReservationsExportDTs Tasks;
            Tasks = new ReservationsExportDTs(sm);
            Tasks.CreateTables();

            //2. insert test data to [proteluser].[HitServices_Buch]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                int count = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151861,0,1,'2016/11/12',551,6,'',20702,0)");
                Assert.AreEqual(1, count);
                int count2 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151873,0,0,'2016/11/11',550,6,'',15388,0)");
                Assert.AreEqual(1, count2);
                int count3 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151905,0,1,'2016/10/25',436,5,'BB.',3378,0)");
                int count4 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151857,0,1,'2017/01/01',267,2,'',1539,0)");
                Assert.AreEqual(1, count4);
                int count5 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (999999999,0,1,'2018/01/01',999,2,'',99523,0)");
                Assert.AreEqual(1, count5);
                int count6 = db.Execute(@"UPDATE [proteluser].[HitServices_Leistacc] SET Leistacc = 151856");
                Assert.AreEqual(1, count6);
            }

            //3. run
            //get Settings and script
            ExportToCleverApiFlows Flows;

            Flows = new ExportToCleverApiFlows(sm);
            Flows.ExportReservationsFlow();
        }

        [Test, Order(204)]
        public void CorissiaReservations()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            //Create Tables
            ReservationsExportDTs Tasks;
            Tasks = new ReservationsExportDTs(sm);
            Tasks.CreateTables();

            //2. insert test data to [proteluser].[HitServices_Buch]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                List<ReservationsExportModel> Reservations = new List<ReservationsExportModel>();
                Reservations = db.Query<ReservationsExportModel>(@"SELECT TOP 10 b.leistacc AS Id, b.buchstatus AS BookingStatus, b.reschar AS [Status], b.datumbis AS Departure,
		                                                  b.zimmernr AS RoomId, b.mpehotel AS HotelId, proteluser.GetReservationsBoards (b.leistacc) AS Βoard,
		                                                  b.reisenr AS AgentId FROM [proteluser].[Buch] AS b").ToList();

                List<ReservationsExportModel> res = Reservations.OrderBy(i => i.Id).Take(3).ToList();
                foreach (ReservationsExportModel item in res)
                {
                    item.ModificationType = 2;
                    item.ExternalId = "123456";
                    string insertQuery = @"INSERT INTO HitServices_Buch(Leistacc, ExternalId, BookingStatus, Status, Departure, RoomId, HotelId, Βoard, AgentId, ForceInsert)
                                                         VALUES (@leisstacc, @externalId, @bookingStatus, @status, @departure, @roomId, @hotelId, @board, @agentId, @ForceInsert)";

                    db.Query(insertQuery, new
                    {
                        leisstacc = item.Id,
                        externalId = "123456",
                        bookingStatus = 0,
                        status = item.Status,
                        departure = item.Departure,
                        roomId = item.RoomId,
                        hotelId = item.Hotel,
                        board = item.Board,
                        agentId = item.AgentId,
                        ForceInsert = 0
                    });
                }

                List<ReservationsExportModel> Reservations1 = new List<ReservationsExportModel>();
                Reservations1 = db.Query<ReservationsExportModel>(@"SELECT TOP 10 b.leistacc AS Id, b.buchstatus AS BookingStatus, b.reschar AS [Status], b.datumbis AS Departure,
		                                                  b.zimmernr AS RoomId, b.mpehotel AS HotelId, proteluser.GetReservationsBoards (b.leistacc) AS Βoard,
		                                                  b.reisenr AS AgentId FROM [proteluser].[Buch] AS b ORDER BY b.leistacc DESC").ToList();

                int minLeistacc = 999999999;
                foreach (ReservationsExportModel leis in Reservations1)
                {
                    if (minLeistacc > leis.Id)
                    {
                        minLeistacc = leis.Id;
                    }
                }

                string insert = @"INSERT INTO HitServices_Buch(Leistacc, ExternalId, BookingStatus, Status, Departure, RoomId, HotelId, Βoard, AgentId, ForceInsert)
                                                         VALUES (@leisstacc, @externalId, @bookingStatus, @status, @departure, @roomId, @hotelId, @board, @agentId, @ForceInsert)";

                db.Query(insert, new
                {
                    leisstacc = 99999999,
                    externalId = "123456",
                    bookingStatus = 0,
                    status = 1,
                    departure = "2018-09-07",
                    roomId = 999999,
                    hotelId = 2,
                    board = "",
                    agentId = 95874,
                    ForceInsert = 0
                });

                db.Execute(@"UPDATE [proteluser].[HitServices_Leistacc] SET Leistacc = " + --minLeistacc + "");

                //Run Service
                ExportToCleverApiFlows Flows;

                Flows = new ExportToCleverApiFlows(sm);
                Flows.ExportReservationsFlow();
            }
        }

        [Test, Order(205)]
        public void CorissiaReservationsTestAutoMapper()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            //Create Tables
            ReservationsExportDTs Tasks;
            Tasks = new ReservationsExportDTs(sm);
            Tasks.CreateTables();

            ReservationsExportModel Reservations = new ReservationsExportModel();
            CorissiaAPICheckinDataModel CorissiaModel = new CorissiaAPICheckinDataModel();
            //2. insert test data to [proteluser].[HitServices_Buch]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                Reservations = db.Query<ReservationsExportModel>(@"SELECT top 1 b.leistacc AS Id, b.buchstatus AS BookingStatus, b.reschar AS [Status], b.anzerw AS Adults, 
	                                                                       b.anzkin1 + b.anzkin2 + b.anzkin3 + b.anzkin4 AS Children, b.zbett AS ExtraBeds, b.kbett AS Babycot,
	                                                                       b.datumvon AS Arrival, b.datumbis AS Departure, DATEDIFF(d, b.globdvon, b.globdbis) AS StayDays, 
	                                                                       b.mpehotel AS Hotel, proteluser.GetReservationsBoards (b.leistacc) AS Βoard, b.zimmernr AS RoomId,
	                                                                       z.ziname AS RoomNo, k.name1 AS Αgent, b.reisenr AS AgentId, b.not1txt + '' + b.not2txt AS Remarks,
	                                                                       b.resuser AS [User], b.katnr AS RoomType, k2.kat AS RoomTypeName, b.preis AS Price1, b.voucher AS Voucher,
	                                                                       b.preistypgr AS MasterPriceList, p.gruppe AS RateCodeName, b.firmennr AS Company, k3.name1 AS CompanyName,
	                                                                       b.gruppennr AS [Group], k1.name1 AS GroupName, m.nr AS Market, m.bezeich AS MarketName, b.source AS Source, 
	                                                                       s.bezeich AS SourceName, b.kontinnr AS [Contract], k4.kontname AS ContractName
                                                                    FROM buch AS b 
                                                                    LEFT OUTER JOIN zimmer AS z ON b.zimmernr = z.zinr
                                                                    LEFT OUTER JOIN kunden AS k ON b.reisenr = k.kdnr
                                                                    INNER JOIN kat AS k2 ON b.katnr = k2.katnr
                                                                    LEFT OUTER JOIN ptypgrp AS p ON b.preistypgr = p.ptgnr
                                                                    LEFT OUTER JOIN kunden AS k1 ON b.gruppennr = k1.kdnr
                                                                    LEFT OUTER JOIN kunden AS k3 ON b.firmennr = k3.kdnr
                                                                    LEFT OUTER JOIN market AS m ON b.market = m.nr
                                                                    LEFT OUTER JOIN source AS s ON b.source = s.nr
                                                                    LEFT OUTER JOIN kontinge AS k4 ON b.kontinnr = k4.kontinnr ORDER BY b.buchnr DESC").FirstOrDefault();
            }

            CorissiaModel = Mapper.Map<ReservationsExportModel, CorissiaAPICheckinDataModel>(Reservations);

            Assert.AreEqual(Reservations.Adults + Reservations.Children, CorissiaModel.pax);
            Assert.AreEqual(Reservations.Arrival, CorissiaModel.arrivalDate);
            Assert.AreEqual(Reservations.StayDays, CorissiaModel.stayDays);
            Assert.AreEqual(Reservations.Hotel.ToString(), CorissiaModel.hotel);
            Assert.AreEqual(Reservations.Board, CorissiaModel.board);
            Assert.AreEqual(Reservations.RoomNo, CorissiaModel.roomNumber);
            Assert.AreEqual(Reservations.Agent, CorissiaModel.agent);
            Assert.AreEqual(Reservations.AgentId.ToString(), CorissiaModel.bookingID);
            Assert.AreEqual(Reservations.Remarks, CorissiaModel.remarks);
        }

        [Test, Order(206)]
        public void CorissiaCashFlows_CheckNewTables()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            CashflowsExportDTs Tasks;

            //Call Main Logic
            Tasks = new CashflowsExportDTs(sm);
            Tasks.CreateTable();
        }


        [Test, Order(207)]
        public void CorissiaCashFlowsTestAutoMapper()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            //Create Tables
            CashflowsExportDTs Tasks;
            Tasks = new CashflowsExportDTs(sm);
            Tasks.CreateTable();

            ProtelCashFlowsModel cashFlows = new ProtelCashFlowsModel();
            CorissiaAPICashflowDataModel CorissiaModel = new CorissiaAPICashflowDataModel();
            //2. insert test data to [proteluser].[HitServices_Buch]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                cashFlows = db.Query<ProtelCashFlowsModel>(@"SELECT top 1 l.ref AS id, b.leistacc AS StayID, l.datum AS CreateDate, CASE WHEN l.rkz = 0 THEN 'Debit' ELSE z.bez END AS Method,
                                                                    (l.anzahl * l.epreis) AS Amount, l.ukto AS DepartmentId, l.text AS Department, l.rkz AS [Type], l2.fisccode AS ΙnvoiceTypeID, f.code AS ΙnvoiceType,
                                                                    l2.rechnr AS ΙnvoiceΝο, l.umbtext AS Remarks, l.bediener AS [User], l.gast AS Guest, l.kundennr AS GuestId, l.zimmer AS Room, l.mwstsatz AS Mwstsatz, l.tax1 AS Tax1, l.deposit AS Deposit,
                                                                    l. deposituse AS Deposituse, l.ifc AS ExternalInt
                                                             FROM leist AS l 
                                                                LEFT outer JOIN leisthis AS l2 ON l.ref = l2.ref 
                                                                LEFT outer JOIN Fiscalcd AS f ON l2.fisccode = f.ref
                                                                INNER JOIN buch AS b ON b.leistacc = l.buchnr
                                                                INNER JOIN zahlart AS z ON z.zanr = l.rkz
                                                             WHERE b.buchstatus = 1").FirstOrDefault();
            }

            CorissiaModel = Mapper.Map<ProtelCashFlowsModel, CorissiaAPICashflowDataModel>(cashFlows);

            Assert.AreEqual(cashFlows.id, CorissiaModel.id);
            Assert.AreEqual(cashFlows.StayID, CorissiaModel.stayID);
            Assert.AreEqual(cashFlows.Amount.ToString(), CorissiaModel.amount);
            Assert.AreEqual(cashFlows.Method, CorissiaModel.method);
            Assert.AreEqual(cashFlows.DepartmentId, CorissiaModel.department);
            Assert.AreEqual(cashFlows.ΙnvoiceΝο.ToString(), CorissiaModel.invoiceID);
            Assert.AreEqual(cashFlows.Remarks, CorissiaModel.remarks);
            Assert.AreEqual(cashFlows.User, CorissiaModel.user);
        }

        [Test, Order(208)]
        public void CorissiaCashFlows()
        {
            //1. create setting  file
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";

            //Create Tables
            CashflowsExportDTs Tasks;
            Tasks = new CashflowsExportDTs(sm);
            Tasks.CreateTable();

            //2. insert test data to [proteluser].[HitServices_Buch]
            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                List<ProtelCashFlowsModel> cashFlows = new List<ProtelCashFlowsModel>();
                cashFlows = db.Query<ProtelCashFlowsModel>(@"SELECT TOP 10 l.ref AS id, b.leistacc AS StayID,  l.kundennr AS GuestId, l.datum AS CreateDate, l.rkz AS [Type], 
                                                                           CASE WHEN l.grptext LIKE '%arrangement%' THEN 1 ELSE 0 END IsArrangment , l2.rechnr AS ΙnvoiceΝο, 
                                                                           f.code AS ΙnvoiceType
                                                                    FROM leist AS l 
                                                                    LEFT outer JOIN leisthis AS l2 ON l.ref = l2.ref 
                                                                    LEFT outer JOIN Fiscalcd AS f ON l2.fisccode = f.ref
                                                                    INNER JOIN buch AS b ON b.leistacc = l.buchnr
                                                                    INNER JOIN zahlart AS z ON z.zanr = l.rkz").ToList();

                List<ProtelCashFlowsModel> cash = cashFlows.OrderBy(i => i.id).Take(3).ToList();
                foreach (ProtelCashFlowsModel item in cash)
                {
                    item.ModificationType = 2;
                    item.ExternalId = "123456";
                    string insertQuery = @"INSERT INTO hitservice_leist(ref, ExternalId, leistacc, kundennr, datum, type, IsArrangment, ΙnvoiceΝο, ΙnvoiceType, ForceInsert)
                                                         VALUES (@_ref, @externalId, @leistacc, @kundennr, @datum, @type, @IsArrangment, @ΙnvoiceΝο, @ΙnvoiceType, @ForceInsert)";

                    db.Query(insertQuery, new
                    {
                        _ref = item.id,
                        externalId = "123456",
                        leistacc = item.StayID,
                        kundennr = item.GuestId,
                        datum = item.CreateDate,
                        type = item.Type,
                        IsArrangment = item.IsArrangment,
                        ΙnvoiceΝο = 1234,
                        ΙnvoiceType = item.InvoiceType,
                        ForceInsert = 0
                    });
                }

                //string insert = @"INSERT INTO hitservice_leist(ref, ExternalId, leistacc, kundennr, datum, type, IsArrangment, ΙnvoiceΝο, ΙnvoiceType, ForceInsert)
                //                                         VALUES (@_ref, @externalId, @leistacc, @kundennr, @datum, @type, @IsArrangment, @ΙnvoiceΝο, @ΙnvoiceType, @ForceInsert)";

                //db.Query(insert, new
                //{
                //    _ref = 9999999,
                //    externalId = "123456",
                //    leistacc = 9988888,
                //    kundennr = 79562,
                //    datum = "2018-09-06",
                //    type = 2,
                //    IsArrangment = 0,
                //    ΙnvoiceΝο = 1234,
                //    ΙnvoiceType = "",
                //    ForceInsert = 0
                //});

                //List<ProtelCashFlowsModel> cash1 = new List<ProtelCashFlowsModel>();
                //cash1 = db.Query<ProtelCashFlowsModel>(@"SELECT TOP 10 l.ref AS id, b.leistacc AS StayID,  l.kundennr AS GuestId, l.datum AS CreateDate, l.rkz AS [Type], 
                //                                                           CASE WHEN l.grptext LIKE '%arrangement%' THEN 1 ELSE 0 END IsArrangment , l2.rechnr AS ΙnvoiceΝο, 
                //                                                           f.code AS ΙnvoiceType
                //                                                    FROM leist AS l 
                //                                                    LEFT outer JOIN leisthis AS l2 ON l.ref = l2.ref 
                //                                                    LEFT outer JOIN Fiscalcd AS f ON l2.fisccode = f.ref
                //                                                    INNER JOIN buch AS b ON b.leistacc = l.buchnr
                //                                                    INNER JOIN zahlart AS z ON z.zanr = l.rkz ORDER BY l.ref DESC").ToList();

                //int minref = 999999999;
                //foreach (ProtelCashFlowsModel leis in cash1)
                //{
                //    if (minref > leis.id)
                //    {
                //        minref = leis.id;
                //    }
                //}

                //db.Execute(@"UPDATE [proteluser].[HitServices_Leistacc] SET Leistacc = " + --minref + "");

                //Run Service
                ExportToCleverApiFlows Flows;

                Flows = new ExportToCleverApiFlows(sm);
                Flows.ExportCashFlowsFlow();
            }
        }


        [Test, Order(209)]
        public void CorissiaRetrievingData_Execute()
        {
            //1. create setting  file
            string Settingsfile = "Corissia";
            if (ch.SettingsFileExist(Settingsfile)) ch.DeleteSettingsFile(Settingsfile);
            SettingsModel sm = new SettingsModel();
            sm.ProtelDB = ch.ConnectionString("sisifos", "protel", "proteluser", "protel915930");
            sm.RestServerUrl = "http://sisifos:9899/api/Gorissia";
            sm.RestServerHttpMethod = "POST";
            sm.RestServerMediaType = "application/json";
            sm.MpeHotel = "1";
            ch.WriteSettingsFile(Settingsfile, sm, true);

            //2. Create Table for Profilew and insert test data to [proteluser].[HitServices_kunden], [proteluser].[HitServices_Buch], [proteluser].[HitService_leist]
            ProfileExportDTs Tasks0;
            Tasks0 = new ProfileExportDTs(sm);
            Tasks0.CreateTables();

            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                int count = db.Execute(@"INSERT INTO [proteluser].[HitServices_kunden] ([protelId],[typeId],[actionId]) VALUES (23,1,1)");
                Assert.AreEqual(1, count);
            }

            //3. Create Reservations Tables and insert test data to [proteluser].[HitServices_Buch]
            ReservationsExportDTs Tasks;
            Tasks = new ReservationsExportDTs(sm);
            Tasks.CreateTables();

            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                int count = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151861,0,1,'2016/11/12',551,6,'',20702,0)");
                Assert.AreEqual(1, count);
                int count2 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151873,0,0,'2016/11/11',550,6,'',15388,0)");
                Assert.AreEqual(1, count2);
                int count3 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151905,0,1,'2016/10/25',436,5,'BB.',3378,0)");
                int count4 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (151857,0,1,'2017/01/01',267,2,'',1539,0)");
                Assert.AreEqual(1, count4);
                int count5 = db.Execute(@"INSERT INTO [proteluser].[HitServices_Buch] (
                                        [Leistacc],[BookingStatus],[Status],[Departure],[RoomId],[HotelId],[Βoard],[AgentId],[ForceInsert]) 
                                        VALUES (999999999,0,1,'2018/01/01',999,2,'',99523,0)");
                Assert.AreEqual(1, count5);
                int count6 = db.Execute(@"UPDATE [proteluser].[HitServices_Leistacc] SET Leistacc = 151856");
                Assert.AreEqual(1, count6);
            }

            //Create Table for Cashflows and insert test data to [proteluser].[HitServices_Buch]
            CashflowsExportDTs Tasks1;
            Tasks1 = new CashflowsExportDTs(sm);
            Tasks1.CreateTable();

            using (IDbConnection db = new SqlConnection(sm.ProtelDB))
            {
                List<ProtelCashFlowsModel> cashFlows = new List<ProtelCashFlowsModel>();
                cashFlows = db.Query<ProtelCashFlowsModel>(@"SELECT TOP 10 l.ref AS id, b.leistacc AS StayID,  l.kundennr AS GuestId, l.datum AS CreateDate, l.rkz AS [Type], 
                                                                           CASE WHEN l.grptext LIKE '%arrangement%' THEN 1 ELSE 0 END IsArrangment , l2.rechnr AS ΙnvoiceΝο, 
                                                                           f.code AS ΙnvoiceType
                                                                    FROM leist AS l 
                                                                    LEFT outer JOIN leisthis AS l2 ON l.ref = l2.ref 
                                                                    LEFT outer JOIN Fiscalcd AS f ON l2.fisccode = f.ref
                                                                    INNER JOIN buch AS b ON b.leistacc = l.buchnr
                                                                    INNER JOIN zahlart AS z ON z.zanr = l.rkz").ToList();

                List<ProtelCashFlowsModel> cash = cashFlows.OrderBy(i => i.id).Take(3).ToList();
                foreach (ProtelCashFlowsModel item in cash)
                {
                    item.ModificationType = 2;
                    item.ExternalId = "123456";
                    string insertQuery = @"INSERT INTO hitservice_leist(ref, ExternalId, leistacc, kundennr, datum, type, IsArrangment, ΙnvoiceΝο, ΙnvoiceType, ForceInsert)
                                                         VALUES (@_ref, @externalId, @leistacc, @kundennr, @datum, @type, @IsArrangment, @ΙnvoiceΝο, @ΙnvoiceType, @ForceInsert)";

                    db.Query(insertQuery, new
                    {
                        _ref = item.id,
                        externalId = "123456",
                        leistacc = item.StayID,
                        kundennr = item.GuestId,
                        datum = item.CreateDate,
                        type = item.Type,
                        IsArrangment = item.IsArrangment,
                        ΙnvoiceΝο = 1234,
                        ΙnvoiceType = item.InvoiceType,
                        ForceInsert = 0
                    });
                }
            }

            //3. run job
            Api = new CorissiaDataRetrievingJob();
            Assert.DoesNotThrow(() => Api.Execute(Settingsfile, "CorissiaDataRetrieving", "TestInstallation"));

            //4. clean setting file and records added in DB
            ch.DeleteSettingsFile(Settingsfile);

        }

    }
}
