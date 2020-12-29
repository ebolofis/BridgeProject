using Dapper;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DataAccess.DT.SQL
{
    public class CreateTable
    {
        public string createHitToPylonDocsSQL;

        public CreateTable()
        {
            createHitToPylonDocsSQL = @"
                IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE name = 'hit_to_pylon_docs' AND type = 'U' )
			    BEGIN
				    CREATE TABLE hit_to_pylon_docs 
				    (
					    rechnr INT,
					    fisccode INT,
					    mpehotel INT,
					    is_sent INT DEFAULT 0,
					    date_created DATE,
					    kind INT,
					    kdnr INT
				    ) 
                END
            ";
        }


        #region MainCourante

        public void DropMCTables(string ConnString)
        {
            string sql1 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_bank' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_bank";
            string sql2 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_cash' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_cash";
            string sql3 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_card' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_card";
            string sql4 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_debitor' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_debitor";
            string sql5 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_income' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_income";
            string sql6 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_deposit_new' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_deposit_new";
            string sql7 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_deposit_inhouse' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_mc_deposit_inhouse";
            string sql8 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_ar' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_ar";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql1);
                db.Execute(sql2);
                db.Execute(sql3);
                db.Execute(sql4);
                db.Execute(sql5);
                db.Execute(sql6);
                db.Execute(sql7);
                db.Execute(sql8);
            }
        }

        #region CreateMCTables

        public void CreateMCTables(string ConnString)
        {
            DropTables(ConnString);
            DropMCTables(ConnString);

            CreateTables(ConnString);

            CreateMCBank(ConnString);
            CreateMCCash(ConnString);
            CreateMCCard(ConnString);
            CreateMCDebitor(ConnString);
            CreateMCIncome(ConnString);
            CreateMCDepositNew(ConnString);
            CreateMCDepositInhouse(ConnString);
        }

        public void CreateMCBank(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_bank' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_bank
                (
                    datum DATETIME,
	                mpehotel INT,
                    rechnr INT,
	                fiscalcode INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
	                typ INT,
	                amount DECIMAL(18,2),
	                kind INT,
	                payment_method INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCCash(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_cash' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_cash
                (
                    datum DATETIME,
	                mpehotel INT,
                    rechnr INT,
	                fiscalcode INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
	                typ INT,
	                amount DECIMAL(18,2),
	                kind INT,
	                payment_method INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCCard(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_card' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_card
                (
                    datum DATETIME,
	                mpehotel INT,
                    rechnr INT,
	                fiscalcode INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
	                typ INT,
	                amount DECIMAL(18,2),
	                kind INT,
	                payment_method INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCDebitor(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_debitor' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_debitor
                (
                    datum DATETIME,
	                mpehotel INT,
                    rechnr INT,
	                fiscalcode INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
                    typ INT,
	                plz NVARCHAR(100),
	                strasse NVARCHAR(100),
	                ort NVARCHAR(100),
	                land NVARCHAR(100),
	                guest_id INT,
	                guest_name NVARCHAR(100),
	                amount DECIMAL(18,2),
	                kind INT,
	                payment_method INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCIncome(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_income' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_income
                (
                    datum DATETIME,
	                mpehotel INT,
                    vatno INT,
	                department NVARCHAR(100),
                    transaction_id INT,   
                    line INT,
	                log_account NVARCHAR(100),
	                amount DECIMAL(18,2),
	                tax DECIMAL(18,2)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCDepositNew(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_deposit_new' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_deposit_new
                (
                    datum DATETIME,
	                mpehotel INT,
                    leist_ref INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
	                typ INT,
	                amount DECIMAL(18,2),
	                payment_method INT,
	                debit INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateMCDepositInhouse(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_mc_deposit_inhouse' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_mc_deposit_inhouse
                (
                    datum DATETIME,
	                mpehotel INT,
                    leist_ref INT,
	                log_account NVARCHAR(100),
                    kdnr INT,  
	                name1 NVARCHAR(100),
	                afmno NVARCHAR(100), 
                    fibudeb NVARCHAR(100),
	                typ INT,
	                amount DECIMAL(18,2),
	                payment_method INT,
	                debit INT
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }
       
        #endregion CreateMCTables


        #region DeleteFromMCTables

        public void DeleteDataFromMCTables(string ConnString)
        {
            DeleteDataFromMCIncome(ConnString);
            DeleteDataFromMCCash(ConnString);
            DeleteDataFromMCCard(ConnString);
            DeleteDataFromMCBank(ConnString);
            DeleteDataFromMCDebitor(ConnString);
            DeleteDataFromMCDepositNew(ConnString);
            DeleteDataFromMCDepositInhouse(ConnString);
        }

        public void DeleteDataFromMCIncome(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_income";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCCash(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_cash";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCCard(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_card";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCBank(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_bank";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCDebitor(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_debitor";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCDepositNew(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_deposit_new";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        public void DeleteDataFromMCDepositInhouse(string ConnString)
        {
            string Sql = @"DELETE FROM hit_estia_mc_deposit_inhouse";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql);
            }
        }

        #endregion DeleteFromMCTables


        #region InsertIntoMCTables

        public void InsertDataIntoMCTables(string ConnString, eaDateModel model)
        {
            InsertDataIntoMCIncome(ConnString, model);
            InsertDataIntoMCCash(ConnString, model);
            InsertDataIntoMCCard(ConnString, model);
            InsertDataIntoMCBank(ConnString, model);
            InsertDataIntoMCDebitor(ConnString, model);
            InsertDataIntoMCDepositNew(ConnString, model);
            InsertDataIntoMCDepositInhouse(ConnString, model);

            InsertDataIntoARPayoff(ConnString, model);
        }

        public void InsertDataIntoMCIncome(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_income
                SELECT * FROM 
                (
	                SELECT 
		                l.datum,
		                l.mpehotel,
		                l.vatno,
		                'income' AS 'department',
		                -1 AS 'transaction_id',  
		                0 AS 'line', 
		                'AccountInhouse' AS 'log_account',  
		                SUM((l.epreis * l.anzahl)) AS 'amount',
		                0 AS 'tax'
	                FROM 
		                proteluser.leisthis l
		                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
	                WHERE 
		                l.datum BETWEEN @date_from AND @date_to AND 
		                l.rkz = 0 AND 
		                l.mpehotel = @mpehotel AND  
		                l.sorq = 0
	                GROUP BY 
		                l.datum,l.mpehotel,l.vatno

	                UNION ALL
	
	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel, 
			                l.vatno,
			                u.bez AS 'department', 
			                u.ktonr AS 'transaction_id',  
			                1 AS 'line', 
			                u.exTAXtext AS 'log_account', 
			                ((SUM(l.epreis * l.anzahl)) -
			                (((SUM(l.epreis * l.anzahl)) - ((((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz))) - ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1))) +
			                ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1)))) + (((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz))) - 
			                ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1))))) AS 'amount',
			                0 AS 'tax'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.ukto u ON  u.ktonr = l.ukto
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND 
			                l.rkz = 0 AND 
			                l.mpehotel = @mpehotel AND  
			                l.sorq = 0
		                GROUP BY 
			                l.datum,l.mpehotel,l.vatno,l.mwstsatz,u.ktonr,u.exTAXtext,l.TAX1,u.bez
	                ) uktoPOSO

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel, 
			                l.vatno,
			                'vat' AS 'department',
			                u.ktonr AS 'transaction_id', 
			                2 AS 'line', 
			                u.exTAXtext2 AS 'log_account',
			                ((SUM(l.epreis * l.anzahl)) - ((((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz))) - ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1))) +
			                ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1)))) AS 'amount',
			                0 AS 'tax'
			
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.ukto u ON  u.ktonr = l.ukto
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE
			                l.datum BETWEEN @date_from AND @date_to AND
			                l.rkz = 0 AND 
			                l.mpehotel = @mpehotel AND
			                l.sorq = 0
		                GROUP BY 
			                l.datum,l.mpehotel,l.vatno,l.mwstsatz,u.ktonr,u.exTAXtext2,l.TAX1
	                ) Vat

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel, 
			                l.vatno,
			                'tax' AS 'department',
			                u.ktonr AS 'transaction_id',
			                3 AS 'line', 
			                u.exTAXtext3 AS 'log_account',
			                (((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz))) - ((SUM(l.epreis * l.anzahl) * 100 / (100 + l.mwstsatz)) * 100 / (100 + l.TAX1))) AS 'amount',
			                l.TAX1 AS 'tax'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.ukto u ON  u.ktonr = l.ukto
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE
			                l.datum BETWEEN @date_from AND @date_to AND
			                l.rkz = 0 AND 
			                l.mpehotel = @mpehotel AND
			                l.sorq=0
		                GROUP BY
			                l.datum,l.mpehotel,l.vatno,l.mwstsatz,u.ktonr,u.exTAXtext3,l.TAX1
	                ) df
                ) Arthro
                ORDER BY 
	                Arthro.transaction_id, Arthro.line                
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCCash(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_cash
                SELECT * FROM 
                (
	                SELECT 
		                l.datum,
		                l.mpehotel,
		                l.rechnr,
		                l.fisccode AS 'fiscalcode', 
		                z.fibukto AS 'log_account',
		                k.kdnr,k.name1,k.afmno,k.fibudeb,
		                k.typ,
		                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
		                0 AS 'kind',
		                z.zanr AS 'payment_method'
	                FROM 
		                proteluser.leisthis l
		                INNER JOIN proteluser.rechhist r ON l.fisccode = r.fisccode AND l.rechnr = r.rechnr
		                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
		                INNER JOIN proteluser.fiscalcd f ON f.ref = l.fisccode
		                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = r.kdnr
		                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
	                WHERE  
		                l.datum BETWEEN @date_from AND @date_to AND
		                z.typ = 0 AND
		                l.fisccode > 0 AND
		                r.datum = l.datum AND
		                arout = 0 AND
		                withdrawl = 0 AND
		                l.mpehotel = @mpehotel
	                GROUP BY 
		                l.datum,l.mpehotel,l.rechnr,l.fisccode,z.fibukto,k.kdnr,k.name1,z.zanr,k.afmno,k.fibudeb, k.typ

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                0 AS 'rechnr', 
			                ''  AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,
			                k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                1 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND
			                l.rkz > 0 AND
			                z.typ = 0 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND l.deposituse <= l.datum))  AND 
			                l.buchref > 0 AND
			                l.fisccode < 1 AND
			                l.arout = 0 AND
			                l.withdrawl = 0 AND 
			                l.mpehotel = @mpehotel
		                GROUP BY 
		                l.datum,l.mpehotel,l.rechnr,l.fisccode,z.fibukto,k.kdnr,k.name1,z.zanr,k.afmno,k.fibudeb, k.typ
	                ) ADVANCECASH

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                l.rechnr,
			                ''  AS 'fiscalcode',
			                Z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,
			                k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                2 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z on z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li on li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND
			                l.rkz > 0 AND
			                z.typ = 0 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND l.deposituse <= l.datum))  AND 
			                l.buchref < 1 AND
			                l.fisccode < 1 AND
			                l.arout = 0 AND
			                l.withdrawl = 0 AND 
			                l.mpehotel = @mpehotel
		                GROUP BY 
		                l.datum,l.mpehotel,l.rechnr,l.fisccode,z.fibukto,k.kdnr,k.name1,z.zanr,k.afmno,k.fibudeb, k.typ			
	                ) PAIDOUT

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 			
			                l.datum,
			                l.mpehotel,
			                -1 AS 'rechnr', 
			                ''  AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,
			                k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                3 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.rechhist r ON l.fisccode = r.fisccode AND l.rechnr = r.rechnr
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                INNER JOIN proteluser.fiscalcd f ON f.ref = l.fisccode
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = r.kdnr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE
			                l.datum BETWEEN @date_from AND @date_to AND
			                z.typ = 0 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse = l.datum)) AND 
			                l.fisccode > 0 AND 
			                r.datum <> l.datum AND
			                l.arout = 0 AND
			                l.withdrawl = 0 AND 
			                l.mpehotel = @mpehotel
		                GROUP BY 
		                l.datum,l.mpehotel,l.rechnr,l.fisccode,z.fibukto,k.kdnr,k.name1,z.zanr,k.afmno,k.fibudeb, k.typ
	                ) ADVANCE
                ) CASHTIMOLOGIA
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCCard(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_card
                SELECT * FROM 
                (
	                SELECT 
		                l.datum,
		                l.mpehotel,
		                l.rechnr,
		                l.fisccode AS 'fiscalcode', 
		                z.fibukto AS 'log_account',
		                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
		                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
		                0 AS 'kind',
		                z.zanr AS 'payment_method'
	                FROM 
		                proteluser.leisthis l
		                INNER JOIN proteluser.rechhist r ON l.fisccode = r.fisccode AND l.rechnr = r.rechnr
		                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
		                INNER JOIN proteluser.fiscalcd f ON f.ref = l.fisccode
		                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = r.kdnr
		                INNER JOIN proteluser.lizenz li ON li.mpehotel=l.mpehotel
	                WHERE 
		                l.datum BETWEEN @date_from AND @date_to AND 
		                z.typ = 3 AND
		                l.fisccode > 0 AND 
		                r.datum = l.datum AND
		                arout = 0 AND
		                withdrawl = 0 AND
		                l.mpehotel = @mpehotel
	                GROUP BY  
		                l.datum,l.mpehotel,l.rechnr,l.fisccode,z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,z.zanr

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                0 AS 'rechnr', 
			                ''  AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                1 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND 
			                l.rkz > 0 AND
			                z.typ = 3 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse <= l.datum)) AND
			                l.buchref > 0 AND
			                l.fisccode < 1 AND 
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY  
			                l.datum,l.mpehotel,z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,z.zanr
	                ) ADVANCECASH

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                l.rechnr,
			                '' AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                2 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND 
			                l.rkz > 0 AND
			                z.typ = 3 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse <= l.datum)) AND
			                l.buchref < 1 AND
			                l.fisccode < 1 AND 
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY  
			                l.datum,l.mpehotel,l.rechnr,z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,z.zanr
	                ) PAIDOUT

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                -1 AS 'rechnr',
			                '' AS 'fiscalcode', 
			                z.fibukto AS 'log_account',
			                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                3 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.rechhist r ON l.fisccode = r.fisccode AND l.rechnr = r.rechnr
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                INNER JOIN proteluser.fiscalcd f ON f.ref = l.fisccode
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = r.kdnr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE  
			                l.datum BETWEEN @date_from AND @date_to AND
			                z.typ = 3 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse = l.datum)) AND
			                l.fisccode > 0 AND
			                r.datum <> l.datum AND
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY  
			                l.datum,l.mpehotel,z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,z.zanr
	                ) ADVANCE
                ) CASHTIMOLOGIA
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCBank(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_bank
                SELECT * FROM 
                (
	                SELECT 
		                L.datum,
		                l.mpehotel,
		                l.rechnr, 
		                l.FISCCODE AS 'fiscalcode',
		                Z.fibukto AS 'log_account', 
		                k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,
		                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
		                0 AS 'kind',
		                z.zanr AS 'payment_method'
	                FROM 
		                PROTELUSER.leisthis l
		                INNER JOIN proteluser.rechhist R ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                INNER JOIN proteluser.fiscalcd f on f.ref=l.fisccode
		                LEFT OUTER JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
		                INNER JOIN proteluser.lizenz li on li.mpehotel=l.mpehotel
	                where 
		                z.typ=1 and
		                l.datum between @date_from and @date_to and 
		                l.fisccode > 0 and 
		                r.datum = l.datum and 
		                arout = 0 and 
		                withdrawl = 0 and 
		                l.mpehotel = @mpehotel
	                group by 
		                l.datum ,l.mpehotel,l.FISCCODE,l.rechnr,Z.fibukto,k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,Z.zanr

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                0 AS 'rechnr', 
			                ''  AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                1 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND 
			                l.rkz > 0 AND
			                z.typ = 1 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse <= l.datum)) AND
			                l.buchref > 0 AND
			                l.fisccode < 1 AND
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY 
			                l.datum ,l.mpehotel,l.FISCCODE,l.rechnr,Z.fibukto,k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,Z.zanr
	                ) ADVANCECASH

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                l.rechnr,
			                '' AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                2 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.zahlart z on z.zanr = l.rkz
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = l.kundennr
			                INNER JOIN proteluser.lizenz li on li.mpehotel = l.mpehotel
		                WHERE 
			                l.datum BETWEEN @date_from AND @date_to AND 
			                l.rkz > 0 AND
			                z.typ = 1 AND			
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse <= l.datum)) AND
			                l.buchref < 1 AND
			                l.fisccode < 1 AND
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY
			                l.datum,l.mpehotel,l.rechnr,z.fibukto, k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,z.zanr
	                ) PAIDOUT

	                UNION ALL

	                SELECT * FROM 
	                (
		                SELECT 
			                l.datum,
			                l.mpehotel,
			                -1 AS 'rechnr', 
			                ''  AS 'fiscalcode',
			                z.fibukto AS 'log_account',
			                k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,
			                ((SUM(l.epreis * l.anzahl)) * -1) AS 'amount',
			                3 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM 
			                proteluser.leisthis l
			                INNER JOIN proteluser.rechhist r ON l.fisccode = r.fisccode AND l.rechnr = r.rechnr
			                INNER JOIN proteluser.zahlart z ON z.zanr = l.rkz
			                INNER JOIN proteluser.fiscalcd f ON f.ref = l.fisccode
			                LEFT OUTER JOIN proteluser.kunden k ON k.kdnr = r.kdnr
			                INNER JOIN proteluser.lizenz li ON li.mpehotel = l.mpehotel
		                WHERE  
			                l.datum BETWEEN @date_from AND @date_to AND
			                z.typ=1 AND
			                (l.deposit = 0 OR (l.deposit = 2 AND deposituse = l.datum)) AND
			                l.fisccode > 0 AND 
			                r.datum <> l.datum AND
			                arout = 0 AND
			                withdrawl = 0 AND
			                l.mpehotel = @mpehotel
		                GROUP BY  
			                l.datum,l.mpehotel,z.fibukto, k.kdnr, K.name1, k.afmno, k.fibudeb,k.typ,z.zanr
	                ) ADVANCE
                ) CASHTIMOLOGIA
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCDebitor(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_debitor
                SELECT * FROM 
                (
	                SELECT 
		                L.datum,
		                L.mpehotel,
		                L.rechnr, 
		                l.FISCCODE AS 'fiscalcode',
		                k.fibudeb AS 'log_account',
		                K.kdnr,K.name1,K.afmno,k.fibudeb,k.typ,K.plz,K.strasse,K.ort,K.land,
		                l.kundennr AS 'guest_id',
		                L.GAST AS 'guest_name',
		                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
		                0  AS 'kind',
		                z.zanr AS 'payment_method'
	                FROM PROTELUSER.leisthis l
	                INNER JOIN proteluser.rechhist R ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
	                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	                inner join proteluser.fiscalcd f on f.ref=L.fisccode
	                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
	                inner join proteluser.buchold b on b.buchnr=l.buchref
	                inner join proteluser.lizenz li on li.mpehotel=R.mpehotel
	                WHERE 
		                l.datum between @date_from and @date_to and 
		                R.DATUM=L.DATUM AND 
		                l.rkz > 0 and 
		                z.typ=2 and 
		                l.withdrawl=0  AND 
		                (L.DEPOSIT=0 OR (L.DEPOSIT=2 AND L.DEPOSITUSE<=L.DATUM)) AND 
		                L.mpehotel = @mpehotel
	                group by 
		                L.datum ,L.mpehotel,l.kundennr,l.FISCCODE,L.rechnr,k.fibudeb,k.typ,K.NAME1,Z.zanr,K.KDNR , K.AFMNO,k.fibudeb, K.PLZ, K.STRASSE, K.ORT, K.LAND, L.GAST

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                L.datum,
			                L.mpehotel,
			                L.rechnr, 
			                '' AS 'fiscalcode',
			                k.fibudeb AS 'log_account',
			                K.kdnr,K.name1,K.afmno,k.fibudeb,k.typ,K.plz,K.strasse,K.ort,K.land,
			                l.kundennr AS 'guest_id',
			                L.GAST AS 'guest_name',
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                1  AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM PROTELUSER.LEISTHIS L
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
		                inner join proteluser.buchold b on b.buchnr=l.buchref
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE 
			                l.datum between @date_from and @date_to and  
			                l.rkz > 0 and 
			                z.typ=2 AND 
			                (L.DEPOSIT=0 OR (L.DEPOSIT=2 AND L.DEPOSITUSE<=L.DATUM)) AND 
			                L.buchref>0 AND 
			                L.mpehotel = @mpehotel AND 
			                L.FISCCODE<1 AND 
			                L.AROUT=0 and 
			                l.withdrawl=0
		                group by 
			                L.datum ,L.mpehotel,l.kundennr,L.FISCCODE,L.rechnr,k.fibudeb,k.typ,K.NAME1,Z.zanr,K.KDNR , K.AFMNO, k.fibudeb,K.PLZ, K.STRASSE, K.ORT, K.LAND, L.GAST
	                ) ADVANCEDEBTOR

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                L.datum date,
			                L.mpehotel,
			                L.rechnr,
			                '' AS 'fiscalcode',
			                k.fibudeb AS 'log_account',
			                K.kdnr,K.name1,K.afmno,k.fibudeb,k.typ,K.plz,K.strasse,K.ort,K.land,
			                l.kundennr AS 'guest_id',
			                L.GAST AS 'guest_name',
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                2 AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM PROTELUSER.LEISTHIS L
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
		                inner join proteluser.buchold b on b.buchnr=l.buchref
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE
			                l.datum between @date_from and @date_to and 
			                l.rkz > 0 and 
			                z.typ=2 AND 
			                (L.DEPOSIT=0 OR (L.DEPOSIT=2 AND L.DEPOSITUSE<=L.DATUM)) AND 
			                L.buchref>0 AND 
			                L.mpehotel =@mpehotel AND 
			                L.FISCCODE<1 AND 
			                L.AROUT=0 and 
			                l.withdrawl=0
		                group by 
			                L.datum ,L.mpehotel,l.kundennr,L.FISCCODE,L.rechnr,k.fibudeb,k.typ,K.NAME1,Z.zanr,K.KDNR , K.AFMNO, k.fibudeb,K.PLZ, K.STRASSE, K.ORT, K.LAND, L.GAST
	                ) PAIDOUT

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                L.datum,
			                L.mpehotel,
			                L.rechnr, 
			                l.FISCCODE AS 'fiscalcode',
			                k.fibudeb AS 'log_account',
			                K.kdnr,K.name1,K.afmno,k.fibudeb,k.typ,K.plz,K.strasse,K.ort,K.land,
			                l.kundennr AS 'guest_id',
			                L.GAST AS 'guest_name',
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                3  AS 'kind',
			                z.zanr AS 'payment_method'
		                FROM PROTELUSER.leisthis l
		                INNER JOIN proteluser.rechhist R ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                inner join proteluser.fiscalcd f on f.ref=L.fisccode
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
		                inner join proteluser.buchold b on b.buchnr=l.buchref
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE 
			                l.datum between @date_from and @date_to and 
			                R.DATUM<>L.DATUM AND 
			                l.rkz > 0 and 
			                l.withdrawl=0 and 
			                z.typ=2 AND 
			                (L.DEPOSIT=0 OR (L.DEPOSIT=2 AND L.DEPOSITUSE<=L.DATUM)) AND 
			                R.mpehotel = @mpehotel
		                group by 
			                L.datum ,L.mpehotel,l.kundennr,l.FISCCODE,L.rechnr,k.fibudeb,k.typ,K.NAME1,Z.zanr,K.KDNR , K.AFMNO,k.fibudeb, K.PLZ, K.STRASSE, K.ORT, K.LAND, L.GAST
	                )PAID
                ) DEBTORTIMOLOGIA
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCDepositNew(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_deposit_new
                SELECT * FROM 
                (
	                SELECT 
		                L.datum, 
		                L.mpehotel,
		                L.REF AS 'leist_ref',
		                Z.fibukto AS 'log_account', 
		                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
		                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
		                z.zanr AS 'payment_method',
		                0 AS xp
	                FROM PROTELUSER.LEISTHIS L
	                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
	                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
	                WHERE 
		                l.datum between @date_from and @date_to and 
		                l.rkz > 0 AND 
		                L.DEPOSIT=1 AND 
		                L.DEPOSITUSE=L.DATUM AND 
		                L.mpehotel = @mpehotel AND 
		                L.buchref>0 AND 
		                L.AROUT=0
	                group by 
		                L.datum ,L.mpehotel,Z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,Z.zanr,L.REF,Z.TYP

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                L.datum,
			                L.mpehotel,
			                L.REF AS 'leist_ref',
			                Z.fibukto AS 'log_account', 
			                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                z.zanr AS 'payment_method',
			                0 AS xp
		                FROM PROTELUSER.LEISTHIS L
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE 
			                l.datum between @date_from and @date_to and 
			                l.rkz > 0 AND 
			                L.mpehotel = @mpehotel AND 
			                L.DEPOSIT=2 AND 
			                L.DEPOSITUSE>L.DATUM AND 
			                L.AROUT=0
		                group by 
			                L.datum ,L.mpehotel,Z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,Z.zanr,L.REF,Z.TYP
	                ) DEPOSIT2

	                Union All

	                SELECT * FROM 
	                (
		                SELECT  
			                L.datum,
			                L.mpehotel,
			                0 AS 'leist_ref',
			                'AccountDeposit' AS 'log_account',
			                k.kdnr,'NEW DEPOSITS' AS name1,k.afmno,k.fibudeb,k.typ,
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                z.zanr AS 'payment_method',
			                1 AS xp
		                FROM PROTELUSER.LEISTHIS L
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE 
			                l.datum between @date_from and @date_to and 
			                l.rkz > 0 AND 
			                L.mpehotel = @mpehotel AND 
			                (L.DEPOSIT=1 OR ( L.DEPOSIT=2 AND L.DEPOSITUSE>L.DATUM )) AND 
			                L.AROUT=0
		                group by 
			                L.datum ,L.mpehotel,Z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,Z.zanr,L.REF,Z.TYP
	                ) DEPOSIT3
                ) DEPOSIT
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertDataIntoMCDepositInhouse(string ConnString, eaDateModel model)
        {
            string Sql = @"
                INSERT INTO hit_estia_mc_deposit_inhouse
                SELECT * FROM 
                (
	                SELECT 
		                L.dEPOSITUSE AS 'datum',
		                L.mpehotel,
		                L.REF AS 'leist_ref',
		                'AccountDeposit' AS 'log_account', 
		                k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,
		                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
		                z.zanr AS 'payment_method',
		                0 AS xp
	                FROM PROTELUSER.LEISTHIS L
	                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
	                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
	                WHERE 
		                l.datum between @date_from and @date_to and 
		                l.rkz > 0 AND 
		                L.mpehotel = @mpehotel AND 
		                L.DEPOSIT=2 AND 
		                L.DEPOSITUSE>L.DATUM AND 
		                L.AROUT=0
	                group by 
		                L.dEPOSITUSE ,L.REF ,L.mpehotel,Z.fibukto,k.kdnr,k.name1,k.afmno,k.fibudeb,k.typ,Z.zanr

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                L.dEPOSITUSE AS 'datum',
			                L.mpehotel,
			                0 AS 'leist_ref',
			                'AccountInhouse' AS 'log_account',
			                k.kdnr,'DEPOSIT IN' AS name1,k.afmno,k.fibudeb,k.typ,
			                (( sum(l.epreis*l.anzahl))*-1) AS 'amount',
			                z.zanr AS 'payment_method',
			                1 AS xp
		                FROM PROTELUSER.LEISTHIS L
		                INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
		                left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=L.kundennr
		                inner join proteluser.lizenz li on li.mpehotel=L.mpehotel
		                WHERE 
			                l.datum between @date_from and @date_to and 
			                l.rkz > 0 AND 
			                L.mpehotel = @mpehotel AND 
			                L.DEPOSIT=2 AND 
			                L.DEPOSITUSE>L.DATUM AND 
			                L.AROUT=0
		                group by 
			                L.DEPOSITUSE ,L.mpehotel,Z.zanr,k.kdnr,k.afmno,k.fibudeb,k.typ
	                ) PARAMENONTES
                ) DEPOSITIN
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }
        
        #endregion InsertIntoMCTables


        #region GenerateMCDataReference

        public void GenerateMCDataReference(string ConnString, eaDateModel model)
        {
            CreateHitToPylonDocsTable(ConnString);
            InsertMCRevenueReference(ConnString, model);
            InsertMCPayReference(ConnString, model);
            InsertMCDebInvReference(ConnString, model);
            InsertARPayoffReference(ConnString, model);
        }

        public void InsertMCRevenueReference(string ConnString, eaDateModel model)
        {
            InsertMCIncomeReference(ConnString, model);
            InsertMCDepositNewReference(ConnString, model);
            InsertMCDepositInhouseReference(ConnString, model);            
        }

        public void InsertMCIncomeReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,9,null
                FROM
                    hit_estia_mc_income mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 9)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCDepositNewReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,9,null
                FROM
                    hit_estia_mc_deposit_new mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 9)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCDepositInhouseReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,9,null
                FROM
                    hit_estia_mc_deposit_inhouse mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 9)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCPayReference(string ConnString, eaDateModel model)
        {
            InsertMCCashReference(ConnString, model);
            InsertMCCardReference(ConnString, model);
            InsertMCBankReference(ConnString, model);
        }

        public void InsertMCCashReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,10,null
                FROM
                    hit_estia_mc_cash mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 10)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCCardReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,10,null
                FROM
                    hit_estia_mc_card mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 10)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCBankReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,10,null
                FROM
                    hit_estia_mc_bank mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 10)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertMCDebInvReference(string ConnString, eaDateModel model)
        {
            InsertMCDebitorReference(ConnString, model);
        }

        public void InsertMCDebitorReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,11,null
                FROM
                    hit_estia_mc_debitor mc
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = mc.mpehotel AND t.date_created = mc.datum AND t.kind = 11)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        public void InsertARPayoffReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT distinct
                    -1, -1,mpehotel,0, datum,7,null
                FROM
                    hit_estia_sales_journal_ar ar
                WHERE datum
                    BETWEEN @date_from AND @date_to AND 
                    mpehotel = @mpehotel AND 
                    NOT EXISTS(
                        SELECT datum, mpehotel
                        FROM hit_to_pylon_docs t
                        WHERE t.mpehotel = ar.mpehotel AND t.date_created = ar.datum AND t.kind = 7)
                GROUP BY 
                    datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }

        #endregion GenerateMCDataReference

        #endregion MainCourante
        


        #region SalesJournal

        public void DropTables(string ConnString)
        {
            string sql = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal";
            string sql1 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_cash' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_cash";
            string sql2 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_card' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_card";
            string sql3 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_credit' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_credit";
            string sql4 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_deposit_out' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_deposit_out";
            string sql5 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_bank' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_bank";
            string sql6 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_aa' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_aa";
            string sql7 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_deposit_out_soft' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_deposit_out_soft";
            string sql8 = @"IF EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_ar' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) ) drop table hit_estia_sales_journal_ar";

           using (IDbConnection db = new SqlConnection(ConnString)) {
                db.Execute(sql);
                db.Execute(sql1);
                db.Execute(sql2);
                db.Execute(sql3);
                db.Execute(sql4);
                db.Execute(sql5);
                db.Execute(sql6);
                db.Execute(sql7);
                db.Execute(sql8);
            }
        }

        #region CreateSJTables

        public void CreateTables(string ConnString)
        {
            DropTables(ConnString);
            CreateSalesJournal(ConnString);
            CreateSalesJournalCash(ConnString);
            CreateSalesJournalCard(ConnString);
            CreateSalesJournalBank(ConnString);
            CreateSalesJournalCredit(ConnString);
            CreateSalesJournalDepositOut(ConnString);
            CreateSalesJournalDepositOutSoft(ConnString);
            CreateSalesJournalAA(ConnString);
            CreateSalesJournalAR(ConnString);
        }

        public void CreateSalesJournalAR(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_ar' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_ar
                (
                    belegdat DATETIME, --l.rdatum
	                datum DATETIME, --rechnr.datum
	                mpehotel INT,
	                typ int, --zahlart type
	                fibukto NVARCHAR(100),
                     rechnr INT,  
	                total_amount DECIMAL(18,2),
                    fiscalcode INT, --doctype
	                customer_id INT, --kdnr
                    payment_method INT, --zanr
	                name1 NVARCHAR(100),
	                name2 NVARCHAR(100),
	                afmno NVARCHAR(100),
	                fibudeb NVARCHAR(100),
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100), 
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournal(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal
                (
	                datum DATETIME, --rechnr.datum 
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                invoice_status NVARCHAR(50), 
	                void_rechnr INT, --rechnr of voiding 
	                log_account NVARCHAR(50), --u.exTAXtext
	                transaction_id INT, --service
	                vat_id INT, --vat
	                total_amount DECIMAL(18,2),
	                total_net_amount DECIMAL(18,2), 
                    total_vat_amount DECIMAL(18,2), 
	                city_tax_percent DECIMAL(18,2),
	                city_tax_amount DECIMAL(18,2), 
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100), 
	                vorname NVARCHAR(100),  
	                kepyo NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100), 
	                fibudeb NVARCHAR(100)
                )";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalCash(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_cash' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_cash
                (
	                datum DATETIME, --rechnr.datum 
	                leistdatum DATETIME,
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                invoice_status NVARCHAR(50),
	                void_rechnr INT, --rechnr of voiding 
	                fibukto  NVARCHAR(100),
	                payment_method INT,
	                total_amount DECIMAL(18,2),
	                zimmer NVARCHAR(100),  
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100), 
	                vorname NVARCHAR(100),  
	                kepyo NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100), 
	                fibudeb NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalCard(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_card' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_card
                (
	                datum DATETIME, --rechnr.datum 
	                leistdatum DATETIME,
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                invoice_status NVARCHAR(50),
	                void_rechnr INT, --rechnr of voiding 
	                fibukto  NVARCHAR(100),
	                payment_method INT, --zanr
	                total_amount DECIMAL(18,2),
	                zimmer NVARCHAR(100),  
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100), 
	                vorname NVARCHAR(100),  
	                kepyo NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100), 
	                fibudeb NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalBank(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_bank' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_bank
                (
	                datum DATETIME, --rechnr.datum 
	                leistdatum DATETIME,
	                bankdatum DATETIME,
	                rechnr INT,  
	                mpehotel INT, 
	                fiscalcode INT, --doctype
	                invoice_status NVARCHAR(50),
	                void_rechnr INT, --rechnr of voiding 
	                fibukto  NVARCHAR(100), 
	                payment_method INT, --zanr
	                total_amount DECIMAL(18,2),
	                zimmer NVARCHAR(100),  
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100), 
	                vorname NVARCHAR(100),  
	                kepyo NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalCredit(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_credit' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_credit
                (
	                datum DATETIME, --rechnr.datum 
	                leistdatum DATETIME,
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                invoice_status NVARCHAR(50),
	                void_rechnr INT, --rechnr of voiding 
	                fibukto  NVARCHAR(100),
	                payment_method INT, --zanr
	                total_amount DECIMAL(18,2),
	                zimmer NVARCHAR(100),  
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100), 
	                vorname NVARCHAR(100),  
	                kepyo NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                doy NVARCHAR(100), --k.vatno
	                land NVARCHAR(100), 
	                strasse NVARCHAR(100), 
	                ort NVARCHAR(100), 
	                plz NVARCHAR(100), 
	                fibudeb NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalDepositOut(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_deposit_out' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_deposit_out
                (
	                datum DATETIME, --rechnr.datum 
	                leistdatum DATETIME,
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                fibukto  NVARCHAR(100),
	                total_amount DECIMAL(18,2),
	                customer_id INT, --kdnr	
	                name1 NVARCHAR(100), 
	                name2 NVARCHAR(100),
	                afmno NVARCHAR(100), 
	                agent_id INT, --kdnr	
	                agent_name1 NVARCHAR(100), 
	                agent_name2 NVARCHAR(100),
	                agent_afmno NVARCHAR(100), 
	                company_id INT, --kdnr	
	                company_name1 NVARCHAR(100), 
	                company_name2 NVARCHAR(100),
	                company_afmno NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalDepositOutSoft(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_deposit_out_soft' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_deposit_out_soft
                (
	                datum DATETIME, --rechnr.datum
	                leistdatum DATETIME, --l.rdatum
	                rechnr INT,  
	                mpehotel INT,
	                fiscalcode INT, --doctype
	                fibukto NVARCHAR(100), --z.fibukto
	                payment_method INT, --zanr
	                total_amount DECIMAL(18,2),
	                customer_id INT, --kdnr
	                name1 NVARCHAR(100),
	                invoice_id INT, --kdnr
	                invoice_name1 NVARCHAR(100),
	                invoice_name2 NVARCHAR(100),
	                invoice_afmno NVARCHAR(100),
	                invoice_land NVARCHAR(100)
                )
                ";
            
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        public void CreateSalesJournalAA(string ConnString)
        {
            string sql = @"
                IF NOT EXISTS( SELECT * FROM sys.tables WHERE NAME = 'hit_estia_sales_journal_aa' AND schema_id = ( SELECT schema_id FROM sys.schemas WHERE name = 'proteluser' ) )
                CREATE TABLE hit_estia_sales_journal_aa
                (
	                datum DATETIME, --rechnr.datum
	                bankdatum DATETIME, --l.rdatum
	                mpehotel INT,
	                typ int, --zahlart type
	                fibukto NVARCHAR(100),
	                payment_method INT, --zanr
	                total_amount DECIMAL(18,2),
	                customer_id INT, --kdnr
	                name1 NVARCHAR(100),
	                name2 NVARCHAR(100),
	                afmno NVARCHAR(100),
	                agent_id INT, --kdnr
	                agent_name1 NVARCHAR(100),
	                agent_name2 NVARCHAR(100),
	                agent_afmno NVARCHAR(100),
	                company_id INT, --kdnr
	                company_name1 NVARCHAR(100),
	                company_name2 NVARCHAR(100),
	                company_afmno NVARCHAR(100)
                )
                ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        #endregion CreateSJTables

        #endregion SalesJournal



        #region Customers

        public void GenerateCustomerDataReference(string ConnString, eaDateModel model)
        {
            CreateHitToPylonDocsTable(ConnString);
            InsertCustomerReference(ConnString, model);
        }

        public void InsertCustomerReference(string ConnString, eaDateModel model)
        {
            string sql = @"
                INSERT INTO hit_to_pylon_docs
                SELECT TOP " + model.Size + @"  
                    -1, -1, mpehotel, 0, null, 1, kdnr
                FROM
                    kunden k
                WHERE  
                    typ = 1 AND
                    NOT EXISTS(
                        SELECT kdnr
                        FROM hit_to_pylon_docs t
                        WHERE t.kdnr = k.kdnr AND t.kind = 1)";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql);
            }
        }

        #endregion Customers



        #region GenericCode

        public void CreateHitToPylonDocsTable(string ConnString)
        {
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(createHitToPylonDocsSQL);
            }
        }

        public void InsertDataIntoARPayoff(string ConnString, eaDateModel model)
        {
            CreateSalesJournalAR(ConnString);

            string Sql = @"
                INSERT INTO hit_estia_sales_journal_ar
                SELECT * FROM 
                (
	                SELECT 
		                belegdat,
		                datum,
		                d.mpehotel,
		                z.typ,
		                z.fibukto, --'payment_log_account'
		                rechno as 'rechnr',
		                paid as 'total_amount',
		                f.ref as 'fiscalcode',
		                --k.fibudeb as 'log_account', --'customer_log_account'
		                k.kdnr AS 'customer_id',
		                z.zanr AS 'payment_method',
		                k.name1,k.name2,k.afmno,k.fibudeb,k.vatno as 'doy',k.land,k.strasse,k.ort,K.plz
	                FROM proteluser.debitore d
	                left outer join proteluser.fiscalcd f on f.ref=d.fisccode
	                left outer join proteluser.zahlart z on z.zanr=d.buchzahl
	                left outer join proteluser.kunden k on k.kdnr=d.kundennr
	                left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
	                INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
	                where 
		                paid<>0 and 
		                z1.typ=2 and 
		                d.buchzahl>0 AND
		                d.datum BETWEEN @date_from AND @date_to AND 
		                d.mpehotel = @mpehotel AND 
		                z.typ<>1
	                group by 
		                d.mpehotel,z.typ,paid,belegdat,datum,rechno,f.ref,z.zanr ,z.fibukto,k.kdnr,k.fibudeb,k.name1,k.name2,k.afmno,k.vatno,k.strasse ,k.ort,k.land,K.plz

	                Union All

	                SELECT * FROM 
	                (
		                SELECT 
			                belegdat,
			                datum,
			                d.mpehotel,
			                z.typ,
			                z.fibukto, --'payment_log_account'
			                rechno as 'rechnr',
			                paid as 'total_amount',
			                f.ref as 'fiscalcode',
			                --k.fibudeb as 'log_account', --'customer_log_account'
			                k.kdnr AS 'customer_id',
			                z.zanr AS 'payment_method',
			                k.name1,k.name2,k.afmno,k.fibudeb,k.vatno as 'doy',k.land,k.strasse,k.ort,K.plz
		                FROM proteluser.debitore d
		                left outer join proteluser.fiscalcd f on f.ref=d.fisccode
		                left outer join proteluser.zahlart z on z.zanr=d.buchzahl
		                left outer join proteluser.kunden k on k.kdnr=d.kundennr
		                left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
		                INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
		                where 
			                paid<>0 and 
			                z1.typ<>2 and 
			                d.buchzahl>0 AND
			                d.datum BETWEEN @date_from AND @date_to AND
			                l.mpehotel =  @mpehotel AND 
			                z.typ <> 1
		                group by 
			                d.mpehotel,z.typ,paid,belegdat,datum,rechno,f.ref,z.zanr ,z.fibukto,k.kdnr,k.fibudeb,k.name1,k.name2,k.afmno,k.vatno,k.strasse ,k.ort,k.land,K.plz

		                Union All

		                SELECT * FROM 
		                (
			                SELECT 
				                belegdat,
				                datum,
				                d.mpehotel,
				                z.typ,
				                z.fibukto, --'payment_log_account'
				                rechno as 'rechnr',
				                paid as 'total_amount',
				                f.ref as 'fiscalcode',
				                --k.fibudeb as 'log_account', --'customer_log_account'
				                k.kdnr AS 'customer_id',
				                z.zanr AS 'payment_method',
				                k.name1,k.name2,k.afmno,k.fibudeb,k.vatno as 'doy',k.land,k.strasse,k.ort,K.plz
			                FROM proteluser.debitore d
			                left outer join proteluser.fiscalcd f on f.ref=d.fisccode
			                left outer join proteluser.zahlart z on z.zanr=d.buchzahl
			                left outer join proteluser.kunden k on k.kdnr=d.kundennr
			                left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart 
			                INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
			                where 
				                paid<>0 and 
				                z1.typ=2 and 
				                d.buchzahl>0  and
				                d.datum BETWEEN @date_from AND @date_to AND
				                d.mpehotel = @mpehotel AND 
				                z.typ=1
			                group by 
				                d.mpehotel,z.typ,paid,belegdat,datum,rechno,f.ref,z.zanr ,z.fibukto,k.kdnr,k.fibudeb,k.name1,k.name2,k.afmno,k.vatno,k.strasse ,k.ort,k.land,K.plz

			                Union All

			                SELECT * FROM 
			                (
				                SELECT 
					                belegdat,
					                datum,
					                d.mpehotel,
					                z.typ,
					                z.fibukto, --'payment_log_account'
					                rechno as 'rechnr',
					                paid as 'total_amount',
					                f.ref as 'fiscalcode',
					                --k.fibudeb as 'log_account', --'customer_log_account'
					                k.kdnr AS 'customer_id',
					                z.zanr AS 'payment_method',
					                k.name1,k.name2,k.afmno,k.fibudeb,k.vatno as 'doy',k.land,k.strasse,k.ort,K.plz
				                FROM proteluser.debitore d
				                left outer join proteluser.fiscalcd f on f.ref=d.fisccode
				                left outer join proteluser.zahlart z on z.zanr=d.buchzahl
				                left outer join proteluser.kunden k on k.kdnr=d.kundennr
				                left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
				                INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
				                where 
					                paid<>0 and 
					                z1.typ<>2 and 
					                d.buchzahl>0  and
					                d.datum BETWEEN @date_from AND @date_to AND
					                l.mpehotel = @mpehotel AND 
					                z.typ = 1
				                group by 
					                d.mpehotel,z.typ,paid,belegdat,datum,rechno,f.ref,z.zanr ,z.fibukto,k.kdnr,k.fibudeb,k.name1,k.name2,k.afmno,k.vatno,k.strasse ,k.ort,k.land,K.plz
			                ) payoff1
		                ) payoff2
	                ) payoff1
                ) payoff2
            ";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { date_from = model.dateFrom, date_to = model.dateTo, mpehotel = model.mpehotel });
            }
        }
               
        #endregion GenericCode
    }
}


