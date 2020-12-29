using Dapper;
using Hit.Services.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.QueriesLogistics;
using System.Data.SqlTypes;
using Hit.Services.Helpers.Classes;
using Hit.Services.DataAccess.DT.SQL;

namespace Hit.Services.MainLogic.Tasks.ProtelMappings
{
    public class PostDataToPylonDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string ConnString;
        SettingsModel settings;

        public PostDataToPylonDT(SettingsModel settings)
        {
            this.settings = settings;
            ConnString = settings.ProtelDB;
        }

        public List<eaSalesJournalARMain> GetDebtorsSalesJournalAR(eaDateModel model)
        {
            Delete_hit_estia_sales_journal_ar();
            Save_SalesJournalAA_To_hit_estia_sales_journal_ar(model);

            string sqlarjournal = @"	SET NOCOUNT ON 
            IF NOT EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'hit_to_pylon_docs' AND type = 'U')
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
            INSERT INTO hit_to_pylon_docs
           SELECT distinct
                -1, -1,mpehotel,0, datum,7,null
            FROM
                hit_estia_sales_journal_ar aaj
            WHERE datum
                BETWEEN @date_from AND @date_to AND mpehotel = @mpehotel
            AND NOT EXISTS(
                SELECT rechnr, fisccode, mpehotel
                FROM hit_to_pylon_docs t
                WHERE t.kind = 7 AND t.mpehotel = aaj.mpehotel AND t.date_created = aaj.datum)
                GROUP BY datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sqlarjournal, new { date_from = (model.dateFrom), date_to = (model.dateTo), mpehotel = model.mpehotel }, null, 600);
            }

            List<eaSalesJournalARMain> results = new List<eaSalesJournalARMain>();
            List<eaSalesJournalARModel> temp = new List<eaSalesJournalARModel>();
            List<HitToPylonDocsModel> hittopylon = new List<HitToPylonDocsModel>();

            string hookarray = "select Top " + model.Size + " * from hit_to_pylon_docs where kind=7 and is_sent = 0 and date_created between @dfrom and @dto and is_sent=0";
            string sql= "Select belegdat,datum,mpehotel,typ,fibukto,rechnr,total_amount,fiscalcode,customer_id,payment_method,name1,name2,afmno,fibudeb,doy,land,strasse,ort,plz from hit_estia_sales_journal_ar  where datum  =@datecreated";

            try
            {
                using (IDbConnection db = new SqlConnection(ConnString))
                {

                    hittopylon = db.Query<HitToPylonDocsModel>(hookarray, new { dfrom = model.dateFrom, dto = model.dateTo }, null, true, 600).ToList();
                    if (hittopylon.Count == 0)
                    {
                        logger.Warn("Sales Journal AR Date Range were not fetched successfully " + hookarray.ToString() + " \r\n");
                        return results;
                    }
                    foreach (HitToPylonDocsModel Row in hittopylon)
                    {
                        eaSalesJournalARMain result = new eaSalesJournalARMain();
                        result.SalesJournalAR = new List<eaSalesJournalARModel>();
                        temp = db.Query<eaSalesJournalARModel>(sql, new { datecreated = Row.date_created }, null, true, 600).ToList();
                        foreach (eaSalesJournalARModel row in temp)
                        {
                            result.SalesJournalAR.Add(row);

                        }
                        results.Add(result);

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("SalesJournalAA were not fetched successfully " + sql.ToString() + " \r\n" + e.ToString());
                throw new Exception(e.ToString());
            }
            return results;
        }
        public List<SalesJournalAAMain> GetSalesJournalAADateRange(eaDateModel model)
        {
            Delete_hit_estia_sales_journal_aa();
            Save_SalesJournalAA_To_hit_estia_sales_journal_aa(model);

            string sqljournal = @"	SET NOCOUNT ON 
            IF NOT EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'hit_to_pylon_docs' AND type = 'U')
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
            INSERT INTO hit_to_pylon_docs
           SELECT distinct
                0, 0,mpehotel,0, datum,6,null
            FROM
                hit_estia_sales_journal_aa aaj
            WHERE datum
                BETWEEN @date_from AND @date_to AND mpehotel = @mpehotel
            AND NOT EXISTS(
                SELECT rechnr, fisccode, mpehotel
                FROM hit_to_pylon_docs t
                WHERE t.kind = 6 AND t.mpehotel = aaj.mpehotel AND t.date_created = aaj.datum)
                GROUP BY datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sqljournal, new { date_from = (model.dateFrom), date_to = (model.dateTo), mpehotel = model.mpehotel }, null, 600);
            }

            List<SalesJournalAAMain> results = new List<SalesJournalAAMain>();
            List<SalesJournalAAModel> temp = new List<SalesJournalAAModel>();
            List<HitToPylonDocsModel> hittopylon = new List<HitToPylonDocsModel>();
           
            string hookarray = "select Top "+ model.Size +" * from hit_to_pylon_docs where kind=6 and is_sent = 0 and date_created between @datefrom and @dateto";
            string sql = "Select datum,bankdatum,mpehotel,typ,fibukto,payment_method,total_amount ,customer_id,name1,name2,afmno,agent_id,agent_name1,agent_name2,agent_afmno,company_id,company_name1,company_name2,company_afmno from hit_estia_sales_journal_aa where datum =@datecreated";
            
            try
            {
                using (IDbConnection db = new SqlConnection(ConnString))
                {

                    hittopylon = db.Query<HitToPylonDocsModel>(hookarray, new { datefrom = model.dateFrom, dateto = model.dateTo }, null, true, 600).ToList();
                    if (hittopylon.Count == 0)
                    {
                        logger.Warn("Sales Journal AA Date Range were not fetched successfully " + hookarray.ToString() + " \r\n");
                        return results;
                    }
                    foreach (HitToPylonDocsModel Row in hittopylon)
                    {
                        SalesJournalAAMain result = new SalesJournalAAMain();
                        result.salesjournalsublist = new List<SalesJournalAAModel>();
                        temp = db.Query<SalesJournalAAModel>(sql, new { datecreated = Row.date_created }, null, true, 600).ToList();
                        foreach (SalesJournalAAModel row in temp)
                        {
                            result.salesjournalsublist.Add(row);
                        }

                        results.Add(result);

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("SalesJournalAA were not fetched successfully " + sql.ToString() + " \r\n" + e.ToString());
                throw new Exception(e.ToString());
            }
            return results;

        }

        public List<eaSalesJournalARModel> GetsalesJournalAR()
        {
            DateTime yesterday;
            yesterday = DateTime.Now.AddDays(-1);
            yesterday = yesterday.Date;
            String.Format("{0:s}", yesterday);
            StringBuilder SQL = new StringBuilder();
            string check = "select * from hit_to_pylon_docs where Kind=7 and is_sent=0 and date_created ='"+ String.Format("{0:s}", yesterday) + " ' " ;
            string sql = "Select belegdat,datum,mpehotel,typ,fibukto,rechnr,total_amount,fiscalcode,customer_id,payment_method,name1,name2,afmno,fibudeb,doy,land,strasse,ort,plz from hit_estia_sales_journal_ar  where datum='" + String.Format("{0:s}", yesterday) + " '";


            List<eaSalesJournalARModel> salesjournalar = new List<eaSalesJournalARModel>();
            HitToPylonDocsModel testmodel = null;
            try
            {
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    testmodel = db.Query<HitToPylonDocsModel>(check, null, null, true, 600).FirstOrDefault();
                    if (testmodel != null)
                        salesjournalar = db.Query<eaSalesJournalARModel>(sql, null, null, true, 600).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error("SalesJournalAR were not fetched successfully " + sql.ToString() + " \r\n" + e.ToString());
                throw new Exception(e.ToString());
            }
            return salesjournalar;
        }
        public List<SalesJournalAAModel> GetsalesJournalAA()
        {
            DateTime yesterday;
            yesterday = DateTime.Now.AddDays(-1);
            yesterday = yesterday.Date;
            String.Format("{0:s}", yesterday);
            StringBuilder SQL = new StringBuilder();
            string check = "select * from hit_to_pylon_docs where Kind=6 and is_sent=0 and date_created='"+ String.Format("{0:s}", yesterday) + " ' " ;
            string sql = "Select datum,bankdatum,mpehotel,typ,fibukto,payment_method,total_amount ,customer_id,name1,name2,afmno,agent_id,agent_name1,agent_name2,agent_afmno,company_id,company_name1,company_name2,company_afmno from hit_estia_sales_journal_aa  where datum' " + String.Format("{0:s}", yesterday) + " '";


            List<SalesJournalAAModel> salesjournalaa = new List<SalesJournalAAModel>();
            HitToPylonDocsModel testmodel = null;
            try
            {
                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    testmodel = db.Query<HitToPylonDocsModel>(check, null, null, true, 600).FirstOrDefault();
                    if (testmodel != null)
                        salesjournalaa = db.Query<SalesJournalAAModel>(sql, null, null, true, 600).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error("SalesJournalAA were not fetched successfully " + sql.ToString() + " \r\n" + e.ToString());
                throw new Exception(e.ToString());
            }
            return salesjournalaa;
        }
        public bool FlagSentRecords(List<eaMessagesStatusModel> listtobeflaged)
        {
            DateTime yesterday;
            yesterday = DateTime.Now.AddDays(-1);
            yesterday = yesterday.Date;

            string depositsflag = "UPDATE hit_to_pylon_docs SET is_sent = 1 WHERE rechnr = @rechnr AND fisccode = @fisccode and date_created=@date and kind=@kind ";
            string UpdateSentFlag = "UPDATE hit_to_pylon_docs SET is_sent = 1 WHERE rechnr = @rechnr AND fisccode = @fisccode";
            string UpdateErrorFlag = "UPDATE hit_to_pylon_docs SET is_sent = -1 WHERE rechnr = @rechnr AND fisccode = @fisccode";
            string customersSql = "UPDATE hit_to_pylon_docs SET is_sent = @status WHERE kdnr = @kdnr AND mpehotel = @mpehotel AND kind = 1";
            string mainCouranteSql = "UPDATE hit_to_pylon_docs SET is_sent = @status WHERE date_created = @date AND mpehotel = @mpehotel AND kind IN (7,9,10,11)";

            using (IDbConnection db = new SqlConnection(ConnString))
            {

                foreach (eaMessagesStatusModel row in listtobeflaged)
                {
                    if (row.Kind == MatchingKindEnum.MainCourantePay)
                    {
                        db.Execute(mainCouranteSql, new { status = row.status, date = row.datum, mpehotel = row.fisccode}, null, 600);
                        continue;
                    }

                    if (row.Kind == MatchingKindEnum.Customers)
                    {
                        db.Execute(customersSql, new { status = row.status, kdnr = row.rechnr, mpehotel = row.fisccode}, null, 600);
                        continue;
                    }

                    if (row.Kind == MatchingKindEnum.Deposits)
                    {
                        db.Execute(depositsflag, new { rechnr = row.rechnr, fisccode = row.fisccode, date = row.datum, kind = MatchingKindEnum.Deposits }, null, 600);
                        continue;
                    }

                    if (row.rechnr == 0 && row.fisccode == 0 && row.datum == yesterday)
                    {
                        db.Execute(depositsflag, new { rechnr = row.rechnr, fisccode = row.fisccode, date = row.datum, kind = row.Kind }, null, 600);
                        break;
                    }

                    if(row.rechnr == -1 && row.fisccode ==-1 )
                    {
                        db.Execute(depositsflag, new { rechnr = row.rechnr, fisccode = row.fisccode, date = row.datum, kind = row.Kind }, null, 600);
                        break;
                    }

                    if (row.status == 1)
                    {
                        db.Execute(UpdateSentFlag, new { rechnr = row.rechnr, fisccode = row.fisccode }, null, 600);
                    }

                    else if (row.status == -1)
                    {
                        db.Execute(UpdateErrorFlag, new { rechnr = row.rechnr, fisccode = row.fisccode }, null, 600);
                    }
                }
            }

            return true;
        }

        public List<PostDataToPylonModel> PostDataToPylon(eaDateModel model)
        {
            DeleteDataFromTemporaryTables();
            SaveLogisticsQueriesToTemporaryTables(model);
            GetDocsForSpecifiedRange(model); //Fills hit_to_pylon_docs_with records for deposits,debtors and docs

            StringBuilder SQL = new StringBuilder();

            List<PDateModel> pDates = new List<PDateModel>();

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                pDates = db.Query<PDateModel>("SELECT mpehotel, pdate FROM datum", null, null, true, 600).ToList();
            }

            List<KundenModel> KundenList = new List<KundenModel>();
            List<LeisthisModel> LeistungHistoryList = new List<LeisthisModel>();
            List<RechhistModel> RechnungHistoryList = new List<RechhistModel>();
            List<HitToPylonDocsModel> hittopylon = new List<HitToPylonDocsModel>();

            //QUERIES LISTS
            List<SalesJournalModel> SalesJournalList = new List<SalesJournalModel>();
            List<SalesJournalAAModel> SalesJournalAAList = new List<SalesJournalAAModel>();
            List<SalesJournalBankModel> SalesJournalBankList = new List<SalesJournalBankModel>();
            List<SalesJournalCardModel> SalesJournalCardList = new List<SalesJournalCardModel>();
            List<SalesJournalCashModel> SalesJournalCashList = new List<SalesJournalCashModel>();
            List<SalesJournalCreditModel> SalesJournalCreditList = new List<SalesJournalCreditModel>();
            List<SalesJournalDepositOut> SalesJournalDepositOutList = new List<SalesJournalDepositOut>();
            List<SalesJournalDepositOutSoftModel> SalesJournalDepositOutSoftList = new List<SalesJournalDepositOutSoftModel>();
            //List<SalesJournalARModel> SalesJournalARModelList = new List<SalesJournalARModel>();

            //string ifexists = "IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE [name] = 'hit_to_pylon_docs' AND [type] = 'U' )";
            string hookarray = "IF  EXISTS ( SELECT * FROM SYS.TABLES WHERE [name] = 'hit_to_pylon_docs' AND [type] = 'U' ) SELECT Top " + model.Size + " kdnr,rechnr, fisccode, mpehotel, is_sent, date_created,Kind FROM hit_to_pylon_docs where is_sent=0 and Kind=2 and date_created between @dfrom and @dto";
            List<PostDataToPylonModel> results = new List<PostDataToPylonModel>();
            try
            {

                string sqlKunden = "SELECT kdnr,mpehotel,name1,name2,vorname,strasse,plz,ort,land,telefonnr,contract,vatno,afmno,fibudeb FROM kunden  WHERE kdnr IN (SELECT distinct kdnr FROM rechhist WHERE rechnr = @rechnr AND fisccode = @fisccode AND mpehotel = @mpehotel AND datum = @date_created )";
                string sqlRechHistory = "SELECT Ref,mpehotel,rechnr,resno,invindex,voidinv,fisccode,kdnr,formnr,Void,deposit,name,username,datum,sum_zahl,sum_belast FROM rechhist WHERE rechnr = @rechnr AND fisccode = @fisccode AND mpehotel = @mpehotel AND datum = @date_created   ";
                string sqlLeistung = "SELECT datum,uhrzeit, mpehotel, rechnr, fisccode, Ref, kundennr, epreis,anzahl,zimmer,rechnung,mwstsatz,vatno,tax1,rkz,ukto,voidref,voidreason,deposit,deposituse, buchref FROM leisthis WHERE rechnr = @rechnr AND fisccode = @fisccode AND mpehotel = @mpehotel ";
                //Select Data from the temporary tables
                string sql_salesjournal = "Select  datum,rechnr,mpehotel,fiscalcode,invoice_status,void_rechnr,log_account,transaction_id ,vat_id,total_amount ,total_net_amount ,total_vat_amount,city_tax_percent ,city_tax_amount,customer_id,name1,name2,vorname,kepyo,afmno,doy,land,strasse ,ort,plz,fibudeb from   hit_estia_sales_journal WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel AND datum = @date_created ";
                string sql_journalcash = "Select datum ,leistdatum,rechnr,mpehotel,fiscalcode,invoice_status,void_rechnr,fibukto,payment_method,total_amount,zimmer,customer_id,name1,name2,vorname,kepyo,afmno,doy,land,strasse,ort,plz,fibudeb from hit_estia_sales_journal_cash  WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel  ";
                string sql_journalcard = "Select datum,leistdatum,rechnr,mpehotel,fiscalcode,invoice_status,void_rechnr,fibukto,payment_method,total_amount,zimmer,customer_id,name1,name2,vorname,kepyo,afmno,doy,land,strasse,ort,plz,fibudeb from hit_estia_sales_journal_card  WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel";
                string sql_journalcredit = "Select datum ,leistdatum,rechnr,mpehotel,fiscalcode,invoice_status,void_rechnr,fibukto,payment_method,total_amount,zimmer,customer_id,name1,name2,vorname,kepyo,afmno,doy,land,strasse,ort,plz,fibudeb from hit_estia_sales_journal_credit WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel ";
                // string sql_journaldepositout = "Select datum ,leistdatum,rechnr,mpehotel,fiscalcode,fibukto,total_amount,customer_id,name1,name2,afmno,agent_id,agent_name1,agent_name2,agent_afmno,company_id,company_name1,company_name2,company_afmno from hit_estia_sales_journal_deposit_out WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel ";
                string sql_journalbank = "Select datum, leistdatum,bankdatum,rechnr,mpehotel,fiscalcode,invoice_status,void_rechnr,fibukto,payment_method,total_amount,zimmer,customer_id,name1,name2,vorname,kepyo,afmno,doy,land,strasse,ort,plz from hit_estia_sales_journal_bank WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel  ";
                //string sql_salesjournalAA = "Select datum,bankdatum,mpehotel,typ,fibukto,payment_method,total_amount ,customer_id,name1,name2,afmno,agent_id,agent_name1,agent_name2,agent_afmno,company_id,company_name1,company_name2,company_afmno from hit_estia_sales_journal_aa WHERE mpehotel = @mpehotel";
                string sql_journaldepositoutsoft = "Select datum,leistdatum,rechnr,mpehotel,fiscalcode,fibukto,payment_method,total_amount,customer_id,name1,invoice_id,invoice_name1,invoice_name2,invoice_afmno,invoice_land from hit_estia_sales_journal_deposit_out_soft WHERE rechnr = @rechnr AND fiscalcode = @fisccode AND mpehotel = @mpehotel";
                // string sql_journalAR = "SELECT    belegdat as 'datum',datum as 'bankdatum',d.mpehotel,rechno as 'rechnr',z.fibukto as 'fibukto',paid as 'amount', f.ref as 'fiscalcode',d.austext as 'comments',z.zanr as 'payment_method',  z1.typ,  k.kdnr as 'customer_id',  k.name1, k.name2, k.afmno, k.vatno as 'doy',  k.strasse,   k.ort,   k.land,  k.plz  FROM proteluser.debitore d      left outer join proteluser.fiscalcd f on f.ref=d.fisccode     left outer join proteluser.zahlart z on z.zanr=d.buchzahl      left outer join proteluser.kunden k on k.kdnr=d.kundennr   left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel where paid<>0 and z1.typ=2 and d.buchzahl>0 and d.datum between @dfrom and  @dto AND z.typ<>1    group by d.mpehotel ,l.hotel ,paid ,belegdat ,datum ,rechno ,f.code ,z.bez ,z.fibukto ,  k.kdnr ,k.name1 ,k.name2 ,k.afmno,k.vatno,D.austext, k.strasse , k.ort, k.land  ,K.plz ,K.telefonnr,K.landkz ,Z1.TYP,f.ref,z.zanr";


                using (IDbConnection db = new SqlConnection(ConnString))
                {


                         hittopylon = db.Query<HitToPylonDocsModel>(hookarray,new { dfrom=model.dateFrom,dto=model.dateTo }, null, true, 600).ToList();
             

                    if (hittopylon.Count == 0)
                    {
                        logger.Error("PostToPylonData were not fetched successfully " + SQL.ToString() + " \r\n");
                        return results;
                    }
                    foreach (HitToPylonDocsModel Row in hittopylon)
                    {
                        PostDataToPylonModel result = new PostDataToPylonModel();
                        KundenList = db.Query<KundenModel>(sqlKunden, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        LeistungHistoryList = db.Query<LeisthisModel>(sqlLeistung, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel }, null, true, 600).ToList();
                        RechnungHistoryList = db.Query<RechhistModel>(sqlRechHistory, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();

                        //Sales Journal Lists Start
                        if (KundenList.Count == 0 || LeistungHistoryList.Count == 0 || RechnungHistoryList.Count==0)
                        {
                            string sqlhookforfalserecords = @"update hit_to_pylon_docs set is_sent=-1  where rechnr = @rechnr AND fisccode = @fisccode and mpehotel = @mpehotel ";
                            db.Execute(sqlhookforfalserecords, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel }, null, 600);
                            continue;
                        }

                        SalesJournalList = db.Query<SalesJournalModel>(sql_salesjournal, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        SalesJournalBankList = db.Query<SalesJournalBankModel>(sql_journalbank, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        SalesJournalCardList = db.Query<SalesJournalCardModel>(sql_journalcard, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        SalesJournalCashList = db.Query<SalesJournalCashModel>(sql_journalcash, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        SalesJournalCreditList = db.Query<SalesJournalCreditModel>(sql_journalcredit, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();
                        SalesJournalDepositOutSoftList = db.Query<SalesJournalDepositOutSoftModel>(sql_journaldepositoutsoft, new { rechnr = Row.rechnr, fisccode = Row.fisccode, mpehotel = Row.mpehotel, date_created = Row.date_created }, null, true, 600).ToList();


                        /// Sales Journal Lists End
                        result.Kunden = new List<KundenModel>();
                        result.Leisthis = new List<LeisthisModel>();
                        result.Rechhist = new List<RechhistModel>();
                        result.SalesJournal = new List<SalesJournalModel>();
                        result.SalesJournalBank = new List<SalesJournalBankModel>();
                        result.SalesJournalCard = new List<SalesJournalCardModel>();
                        result.SalesJournalCash = new List<SalesJournalCashModel>();
                        result.SalesJournalCredit = new List<SalesJournalCreditModel>();
                        result.SalesJournalDepositOutSoft = new List<SalesJournalDepositOutSoftModel>();

                      
                        foreach (KundenModel customer in KundenList)
                        {
                            result.Kunden.Add(customer);
                        }
                        foreach (LeisthisModel leistung in LeistungHistoryList)
                        {
                            result.Leisthis.Add(leistung);
                        }
                        foreach (RechhistModel rechnunghistory in RechnungHistoryList)
                        {
                            result.Rechhist.Add(rechnunghistory);
                        }
                        //////LOGISTICS QUERIES
                        foreach (SalesJournalModel row1 in SalesJournalList)
                        {
                            result.SalesJournal.Add(row1);
                        }
                        foreach (SalesJournalBankModel row3 in SalesJournalBankList)
                        {
                            result.SalesJournalBank.Add(row3);
                        }
                        foreach (SalesJournalCardModel row4 in SalesJournalCardList)
                        {
                            result.SalesJournalCard.Add(row4);
                        }
                        foreach (SalesJournalCashModel row5 in SalesJournalCashList)
                        {
                            result.SalesJournalCash.Add(row5);
                        }
                        foreach (SalesJournalCreditModel row6 in SalesJournalCreditList)
                        {
                            result.SalesJournalCredit.Add(row6);
                        }
                        foreach (SalesJournalDepositOutSoftModel row8 in SalesJournalDepositOutSoftList)
                        {
                            result.SalesJournalDepositOutSoft.Add(row8);
                        }

                        results.Add(result);
                    }
                }
            }

            catch (Exception ex)
            {
                logger.Error("PostToPylonData were not fetched successfully " + SQL.ToString() + " \r\n" + ex.ToString());
                throw new Exception(ex.ToString());
            }

            return results;
        }

        public void DeleteDataFromTemporaryTables()
        {
            Delete_hit_estia_sales_journal_deposit_out_soft();
            Delete_hit_estia_sales_journal_aa();
            Delete_hit_estia_sales_journal_ar();
            Delete_hit_estia_sales_journal_bank();
            Delete_hit_estia_sales_journal_deposit_out();
            Delete_hit_estia_sales_journal_credit();
            Delete_hit_estia_sales_journal_card();
            Delete_hit_estia_sales_journal_cash();
            Delete_hit_estia_sales_journal();
        }

        public void Delete_hit_estia_sales_journal_deposit_out_soft()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_deposit_out_soft";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_aa()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_aa";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_ar()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_ar";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_bank()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_bank";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_deposit_out()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_deposit_out";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_credit()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_credit";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_card()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_card";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal_cash()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal_cash";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void Delete_hit_estia_sales_journal()
        {
            string Sql = @"DELETE FROM hit_estia_sales_journal";
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, null, null, 600);
            }
        }

        public void SaveLogisticsQueriesToTemporaryTables(eaDateModel model)
        {
            Save_SalesDepositOutSoft_To_hit_estia_sales_journal_deposit_out_soft(model);
            Save_SalesJournalAA_To_hit_estia_sales_journal_aa(model);
            Save_SalesJournalBank_To_hit_estia_sales_journal_bank(model);
            Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(model);
            Save_SalesJournalCredit_To_hit_estia_sales_journal_credit(model);
            Save_SalesJournalCard_To_hit_estia_sales_journal_card(model);
            Save_SalesJournalCash_To_hit_estia_sales_journal_cash(model);
            Save_SalesJournal_To_hit_estia_sales_journal(model);
        }

     

            public void GetDocsForSpecifiedRange(eaDateModel model)
        {
      

            string Sql = @"	SET NOCOUNT ON 
	
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
			INSERT INTO hit_to_pylon_docs
			SELECT 
				rechnr, fisccode, mpehotel, 0, datum,2,null
			FROM 
				rechhist r
			WHERE
				datum BETWEEN @date_from AND @date_to AND fisccode > 0 AND rechnr > 0 AND autogen = 0
			AND NOT EXISTS ( 
				SELECT rechnr, fisccode, mpehotel 
				FROM hit_to_pylon_docs t 
				WHERE t.rechnr = r.rechnr AND t.fisccode = r.fisccode AND t.mpehotel = r.mpehotel AND t.date_created = r.datum )			
			SET NOCOUNT OFF ";

            string sqljournal = @"	SET NOCOUNT ON 
            IF NOT EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'hit_to_pylon_docs' AND type = 'U')
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
            INSERT INTO hit_to_pylon_docs
           SELECT distinct
                0, 0,mpehotel,0, datum,6,null
            FROM
                hit_estia_sales_journal_aa aaj
            WHERE datum
                BETWEEN @date_from AND @date_to AND mpehotel = @mpehotel
            AND NOT EXISTS(
                SELECT rechnr, fisccode, mpehotel
                FROM hit_to_pylon_docs t
                WHERE t.mpehotel = aaj.mpehotel AND t.date_created = aaj.datum)
                GROUP BY datum,mpehotel";
            string sqlarjournal = @"	SET NOCOUNT ON 
            IF NOT EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'hit_to_pylon_docs' AND type = 'U')
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
            INSERT INTO hit_to_pylon_docs
           SELECT distinct
                -1, -1,mpehotel,0, datum,7,null
            FROM
                hit_estia_sales_journal_ar aaj
            WHERE datum
                BETWEEN @date_from AND @date_to AND mpehotel = @mpehotel
            AND NOT EXISTS(
                SELECT rechnr, fisccode, mpehotel
                FROM hit_to_pylon_docs t
                WHERE t.mpehotel = aaj.mpehotel AND t.date_created = aaj.datum)
                GROUP BY datum,mpehotel";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                
                db.Execute(Sql, new { date_from = (model.dateFrom), date_to =(model.dateTo) }, null, 600);
                db.Execute(sqljournal, new { date_from = (model.dateFrom), date_to = (model.dateTo), mpehotel = model.mpehotel }, null, 600);
                db.Execute(sqlarjournal, new { date_from = (model.dateFrom), date_to = (model.dateTo), mpehotel = model.mpehotel }, null, 600);
            }

        }
        public void Save_SalesJournal_To_hit_estia_sales_journal(eaDateModel model)
        {
            string Sql = @"INSERT INTO hit_estia_sales_journal
		SELECT *
       FROM (
       SELECT 
	       r.datum, 
	       r.rechnr, 
	       r.mpehotel, 
	       f.ref AS 'fiscalcode',
	       CASE 
		     WHEN r.void > 0 THEN 'Canceled Invoice' 
		     WHEN r.void < 1 AND r.voidinv > 0 THEN 'Void Invoice' 
		     ELSE 'Normal Invoice' 
	       END AS 'invoice_status', 
	       r.voidinv AS 'void_rechnr', 
	       u.exTAXtext AS 'log_account', 
	       u.ktonr AS 'transaction_id', 
	       l.vatno AS 'vat_id', 
	       sum(l.epreis*l.anzahl) AS 'total_amount',
	       (( sum(l.epreis*l.anzahl))-(( ( sum(l.epreis*l.anzahl))- ((((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz)))- ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1))) +
	       ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1))))+ (((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz)))- 
	       ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1))))) as 'total_net_amount', 
	       ( ( sum(l.epreis*l.anzahl))- ((((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz)))- ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1))) +
	       ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1)))) as 'total_vat_amount', 
	       l.tax1 AS 'city_tax_percent',
	       (((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz)))- ((sum(l.epreis*l.anzahl)*100 / (100+l.mwstsatz))*100 / (100+l.TAX1))) AS 'city_tax_amount',
	       k.kdnr AS 'customer_id',k.name1, k.name2, k.vorname, K.[contract] AS 'kepyo', k.afmno, K.vatno AS 'doy', k.land, K.strasse, K.ort, K.plz, K.fibudeb
       FROM proteluser.rechhist r
	       INNER JOIN proteluser.leisthis l ON r.rechnr = l.rechnr AND r.fisccode = l.fisccode  AND l.rkz = 0
	       left outer  join proteluser.ukto u on u.ktonr=l.ukto
	       INNER JOIN proteluser.fiscalcd f ON f.ref = r.fisccode
	       left outer JOIN proteluser.kunden k ON k.kdnr = r.kdnr
	       INNER JOIN PROTELUSER.LIZENZ LI ON LI.MPEHOTEL=R.MPEHOTEL
	       left outer join proteluser.natcode nat on nat.codenr=k.landkz
		inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
       WHERE 
	       r.fisccode > 0 AND 
	       r.rechnr > 0 AND 
	       r.autogen < 1
		and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
       GROUP BY 
	       r.datum,r.rechnr,r.mpehotel,f.ref,r.voidinv,u.exTAXtext,u.ktonr,l.vatno,l.tax1,k.kdnr,k.name1,k.name2,k.vorname,k.[CONTRACT],k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,r.void,l.mwstsatz
       --ORDER BY 
	       --r.rechnr
       ) as salesjournal";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesJournalCash_To_hit_estia_sales_journal_cash(eaDateModel model)
        {
            string Sql = @"INSERT INTO hit_estia_sales_journal_cash
                                        SELECT *
                                        FROM (
                                        SELECT 
	                                    r.datum,
	                                    l.datum AS 'leistdatum',
	                                    r.rechnr, 
	                                    r.mpehotel,
	                                    f.ref AS 'fiscalcode',
	                                    CASE 
		                                WHEN r.void > 0 THEN 'Canceled Invoice' 
		                                WHEN r.void < 1 AND r.voidinv > 0 THEN 'Void Invoice' 
		                                ELSE 'Normal Invoice' 
	                                    END AS 'invoice_status', 
	                                    r.voidinv AS 'void_rechnr',
	                                    z.fibukto,
	                                    z.zanr AS 'payment_method',
	                                    (( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',
	                                    l.zimmer,
	                                    k.kdnr AS 'customer_id', k.name1, k.name2, k.vorname, k.[contract] AS 'kepyo', k.afmno, k.vatno AS 'doy', k.land, k.strasse, k.ort, k.plz, k.fibudeb
                                        FROM proteluser.rechhist r
	                                        INNER JOIN proteluser.leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
	                                        INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	                                        inner join proteluser.fiscalcd f on f.ref=r.fisccode
	                                        INNER JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
	                                        inner join proteluser.lizenz li on li.mpehotel=R.mpehotel
	                                        left outer join proteluser.natcode nat on nat.codenr=k.landkz
		                                    inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
                                        WHERE 
	                                        l.rkz > 0 AND
	                                        z.typ = 0 AND 
	                                        l.datum = r.DATUM
		                                    and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
                                        GROUP BY 
	                                        r.datum,l.datum,r.rechnr,r.mpehotel,f.ref,r.voidinv,z.fibukto,z.zanr,l.vatno,l.tax1,l.zimmer,k.kdnr,k.name1,k.name2,k.vorname,k.[CONTRACT],k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,r.void,l.mwstsatz
                                        --ORDER BY 
	                                        --r.rechnr
                                        ) as salesjournalcash";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesJournalCard_To_hit_estia_sales_journal_card(eaDateModel model)
        {
            string Sql = @"
		INSERT INTO hit_estia_sales_journal_card
		SELECT *
       FROM (
     SELECT
	r.datum,
	l.datum AS 'leistdatum',
	r.rechnr,
	r.mpehotel,
	f.ref AS 'fiscalcode',
	CASE 
		WHEN r.void > 0 THEN 'Canceled Invoice' 
		WHEN r.void < 1 AND r.voidinv > 0 THEN 'Void Invoice' 
		ELSE 'Normal Invoice' 
	END AS 'invoice_status', 
	r.voidinv AS 'void_rechnr',
	z.fibukto,
	z.zanr AS 'payment_method',
	(( sum(l.epreis*l.anzahl))*-1) 'total_amount', 
	l.zimmer,   
	k.kdnr AS 'customer_id', k.name1, k.name2, k.vorname, k.[contract] AS 'kepyo', k.afmno, k.vatno AS 'doy', k.land, k.strasse, k.ort, k.plz, k.fibudeb
FROM PROTELUSER.rechhist R
	INNER JOIN proteluser.leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
	INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	inner join proteluser.fiscalcd f on f.ref=r.fisccode
	INNER JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
	inner join proteluser.lizenz li on li.mpehotel=R.mpehotel
	left outer join proteluser.natcode nat on nat.codenr=k.landkz
		inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
WHERE
	l.rkz > 0 AND
	z.typ = 3 AND 
	l.datum = r.DATUM 
		and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
GROUP BY 
	r.datum,l.datum,r.rechnr,r.mpehotel,f.ref,r.voidinv,z.fibukto,z.zanr,l.vatno,l.tax1,l.zimmer,k.kdnr,k.name1,k.name2,k.vorname,k.[CONTRACT],k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,r.void,l.mwstsatz
--ORDER BY 
	--r.rechnr
       ) as salesjournalcard";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesJournalCredit_To_hit_estia_sales_journal_credit(eaDateModel model)
        {
            string Sql = @"
		INSERT INTO hit_estia_sales_journal_credit
		SELECT *
       FROM (
    SELECT
	r.datum, 
	l.datum as 'leistdatum',
	r.rechnr,
	r.mpehotel,
	f.ref as 'fiscalcode',
	CASE 
		WHEN r.void > 0 THEN 'Canceled Invoice' 
		WHEN r.void < 1 AND r.voidinv > 0 THEN 'Void Invoice' 
		ELSE 'Normal Invoice' 
	END AS 'invoice_status', 
	r.voidinv AS 'void_rechnr',
	z.fibukto,
	z.zanr AS 'payment_method',
	(( sum(l.epreis*l.anzahl))*-1) as 'total_amount',
	l.zimmer,   
	k.kdnr AS 'customer_id', k.name1, k.name2, k.vorname, k.[contract] AS 'kepyo', k.afmno, k.vatno AS 'doy', k.land, k.strasse, k.ort, k.plz, k.fibudeb
FROM rechhist R
	INNER JOIN leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
	INNER JOIN zahlart z on z.zanr=l.rkz
	inner join fiscalcd f on f.ref=r.fisccode
	INNER JOIN KUNDEN K ON K.KDNR=R.KDNR
	inner join lizenz li on li.mpehotel=R.mpehotel
	left outer join natcode nat on nat.codenr=k.landkz
		inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
WHERE 
	l.rkz > 0 AND
	z.typ = 2 AND
	l.datum = r.DATUM 
		and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
GROUP BY 
	r.datum,l.datum,r.rechnr,r.mpehotel,f.ref,r.voidinv,z.fibukto,z.zanr,l.vatno,l.tax1,l.zimmer,k.kdnr,k.name1,k.name2,k.vorname,k.[CONTRACT],k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,r.void,l.mwstsatz
--ORDER BY 
	--r.rechnr
       ) as salesjournalcredit";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }


        public void Save_SalesJournalDepositOut_To_hit_estia_sales_journal_deposit_out(eaDateModel model)
        {
            string Sql = @"
		         INSERT INTO hit_estia_sales_journal_deposit_out
		         SELECT *
                FROM (   
            SELECT 
	            r.datum,
	            l.datum AS 'leistdatum',
	            r.rechnr,
	            r.mpehotel,
	            f.ref AS 'fiscalcode',
	            z.fibukto, 
	            (( sum(l.epreis*l.anzahl))*-1) AS 'total_amount', 
	            k.kdnr AS 'customer_id',k.name1,k.name2,k.afmno,
	            k1.kdnr AS 'agent_id',k1.name1 AS 'agent_name1',k1.name2 AS 'agent_name2',k1.afmno AS 'agent_afmno',
	            k2.kdnr AS 'company_id',k2.name1 AS 'company_name1',k2.name2 AS 'company_name2',k2.afmno AS 'company_afmno'
            FROM rechhist R
	            INNER JOIN leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr and l.mpehotel=r.mpehotel
	            INNER JOIN zahlart z on z.zanr=l.rkz
	            inner join fiscalcd f on f.ref=l.fisccode
	            inner join lizenz li on li.mpehotel=r.mpehotel
	            LEFT OUTER JOIN BUCHOLD b on b.buchnr=l.buchref
	            left outer JOIN KUNDEN K ON K.KDNR=b.kundennr
	            left outer JOIN KUNDEN K1 ON K1.KDNR=b.reisenr
	            left outer JOIN KUNDEN K2 ON K2.KDNR=b.firmennr
		        inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
            WHERE
	            l.fisccode > 0 AND
	            r.datum > l.datum AND
	            l.rechnr > 0 AND
	            arout = 0 AND
	            withdrawl = 0  
		        and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
            GROUP BY 
	            r.datum,l.datum,r.rechnr,r.mpehotel,f.ref,z.fibukto,l.vatno,l.zimmer,k.kdnr,k.name1,k.name2,k.vorname,k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,
	            k1.kdnr,k1.name1,k1.name2,k1.afmno,k2.kdnr,k2.name1,k2.name2,k2.afmno
            --ORDER BY 
            --	r.rechnr
            ) as salesjournaldepositout";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesJournalBank_To_hit_estia_sales_journal_bank(eaDateModel model)
        {
            string Sql = @"
                                    INSERT INTO hit_estia_sales_journal_bank
                                    SELECT *
                                     FROM (
                                    SELECT
	                                    r.datum,
	                                    l.datum AS 'leistdatum',
	                                    l.rdatum 'bankdatum',
	                                    r.rechnr, 
	                                    r.mpehotel,
	                                    f.ref AS 'fiscalcode',
	                                    CASE 
		                                 WHEN r.void > 0 THEN 'Canceled Invoice' 
		                                 WHEN r.void < 1 AND r.voidinv > 0 THEN 'Void Invoice' 
		                                 ELSE 'Normal Invoice' 
	                                    END AS 'invoice_status', 
	                                    r.voidinv AS 'void_rechnr',
	                                    z.fibukto,
	                                    z.zanr AS 'payment_method',
	                                    (( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',
	                                    l.zimmer,
	                                    k.kdnr AS 'customer_id', k.name1, k.name2, k.vorname, k.[contract] AS 'kepyo', k.afmno, k.vatno AS 'doy', k.land, k.strasse, k.ort, k.plz
                                    FROM PROTELUSER.rechhist r
	                                    INNER JOIN proteluser.leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr
	                                    INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
	                                    inner join proteluser.fiscalcd f on f.ref=r.fisccode
	                                    INNER JOIN PROTELUSER.KUNDEN K ON K.KDNR=R.KDNR
	                                    inner join proteluser.lizenz li on li.mpehotel=R.mpehotel
	                                    left outer join proteluser.natcode nat on nat.codenr=k.landkz
		                                inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
                                    WHERE
	                                    l.rkz > 0 AND 
	                                    z.typ = 1 AND 
	                                    l.datum = r.DATUM
		                                and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
                                    GROUP BY 
	                                    r.datum,l.datum,l.rdatum,r.rechnr,r.mpehotel,f.ref,r.voidinv,z.fibukto,z.zanr,l.vatno,l.tax1,l.zimmer,k.kdnr,k.name1,k.name2,k.vorname,k.[CONTRACT],k.afmno,k.vatno,k.land,k.strasse,k.ort,k.plz,k.fibudeb,r.void,l.mwstsatz
                                    --ORDER BY 
                                    --	r.rechnr
                                    ) as salesjournalbank";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesJournalAA_To_hit_estia_sales_journal_ar(eaDateModel model)
        {
            string sql = @"
INSERT INTO hit_estia_sales_journal_ar
SELECT * FROM (
	SELECT 
		belegdat ,
		datum ,
		d.mpehotel as 'mpehotel',
		z1.typ as typ,
		z.fibukto as 'fibukto',
		rechno as 'rechnr',
		paid as 'total_amount',
		f.ref as 'fiscalcode',
		k.kdnr as 'customer_id',
		z.zanr as 'payment_method',
		k.name1,
		k.name2,
		k.afmno,
        k.fibudeb,
		k.vatno as 'doy',
		k.strasse,
		k.ort,
		k.land,
		k.plz
	FROM proteluser.debitore d
		left outer join proteluser.fiscalcd f on f.ref=d.fisccode
		left outer join proteluser.zahlart z on z.zanr=d.buchzahl
		left outer join proteluser.kunden k on k.kdnr=d.kundennr
		left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
		INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
	where paid <> 0 and z1.typ = 2 and d.buchzahl > 0 and d.datum between @dfrom and @dto and z.typ <> 1
	group by 
		belegdat,datum,d.mpehotel,rechno,z.fibukto,paid,f.ref,d.austext,z.zanr,z1.typ,
		k.kdnr,k.name1,k.name2,k.afmno,k.fibudeb,k.vatno,k.strasse,k.ort,k.land,k.plz

	Union All

	SELECT * FROM (
		SELECT 
			belegdat ,
		datum ,
		d.mpehotel as 'mpehotel',
		z1.typ as typ,
		z.fibukto as 'fibukto',
		rechno as 'rechnr',
		paid as 'total_amount',
		f.ref as 'fiscalcode',
		k.kdnr as 'customer_id',
		z.zanr as 'payment_method',
		k.name1,
		k.name2,
		k.afmno,
        k.fibudeb,
		k.vatno as 'doy',
		k.strasse,
		k.ort,
		k.land,
		k.plz
		FROM proteluser.debitore d
			left outer join proteluser.fiscalcd f on f.ref=d.fisccode
			left outer join proteluser.zahlart z on z.zanr=d.buchzahl
			left outer join proteluser.kunden k on k.kdnr=d.kundennr
			left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
			INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
		where paid <> 0 and z1.typ <> 2 and d.buchzahl > 0 and l.mpehotel = 1 and z.typ <> 1 and d.datum between @dfrom and @dto		
		group by
			belegdat,datum,d.mpehotel,rechno,z.fibukto,paid,f.ref,d.austext,z.zanr,z1.typ,
			k.kdnr,k.name1,k.name2,k.afmno,k.fibudeb,k.vatno,k.strasse,k.ort,k.land,k.plz

		Union All

		SELECT * FROM (
			SELECT 
				belegdat ,
		datum ,
		d.mpehotel as 'mpehotel',
		z1.typ as typ,
		z.fibukto as 'fibukto',
		rechno as 'rechnr',
		paid as 'total_amount',
		f.ref as 'fiscalcode',
		k.kdnr as 'customer_id',
		z.zanr as 'payment_method',
		k.name1,
		k.name2,
		k.afmno,
        k.fibudeb,
		k.vatno as 'doy',
		k.strasse,
		k.ort,
		k.land,
		k.plz
			FROM proteluser.debitore d
				left outer join proteluser.fiscalcd f on f.ref=d.fisccode
				left outer join proteluser.zahlart z on z.zanr=d.buchzahl
				left outer join proteluser.kunden k on k.kdnr=d.kundennr
				left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
				INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
			where paid <> 0 and z1.typ = 2 and d.buchzahl > 0  and d.datum between @dfrom and  @dto AND z.typ = 1
			group by
				belegdat,datum,d.mpehotel,rechno,z.fibukto,paid,f.ref,d.austext,z.zanr,z1.typ,
				k.kdnr,k.name1,k.name2,k.afmno,k.fibudeb,k.vatno,k.strasse,k.ort,k.land,k.plz

			Union All

			SELECT * FROM 
			(
				SELECT 
					belegdat ,
		datum ,
		d.mpehotel as 'mpehotel',
		z1.typ as typ,
		z.fibukto as 'fibukto',
		rechno as 'rechnr',
		paid as 'total_amount',
		f.ref as 'fiscalcode',
		k.kdnr as 'customer_id',
		z.zanr as 'payment_method',
		k.name1,
		k.name2,
		k.afmno,
        k.fibudeb,
		k.vatno as 'doy',
		k.strasse,
		k.ort,
		k.land,
		k.plz
				FROM proteluser.debitore d
					left outer join proteluser.fiscalcd f on f.ref=d.fisccode
					left outer join proteluser.zahlart z on z.zanr=d.buchzahl
					left outer join proteluser.kunden k on k.kdnr=d.kundennr
					left outer join proteluser.zahlart z1 on z1.zanr=d.zahlart
					INNER JOIN proteluser.lizenz l on l.mpehotel=d.mpehotel
				where paid <> 0 and z1.typ <> 2 and d.buchzahl > 0 and d.datum between @dfrom and @dto and z.typ = 1			
				group by
					belegdat,datum,d.mpehotel,rechno,z.fibukto,paid,f.ref,d.austext,z.zanr,z1.typ,
					k.kdnr,k.name1,k.name2,k.afmno,k.fibudeb,k.vatno,k.strasse,k.ort,k.land,k.plz
			) payoff1
		) payoff2
	) payoff3 
) payoff4 WHERE typ = 2 ";
            
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

            public void Save_SalesJournalAA_To_hit_estia_sales_journal_aa(eaDateModel model)
        {
            string Sql = @"

INSERT INTO hit_estia_sales_journal_aa
		SELECT *
       FROM (
       SELECT * FROM 
(
	SELECT * FROM 
	(
		SELECT 
			l.datum,
			l.rdatum  as 'bankdatum',
			l.mpehotel,  
			z.typ AS 'typ', 
			z.fibukto,
			z.zanr AS 'payment_method', 
			(( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',	
			k.kdnr AS 'customer_id',k.name1,k.name2,k.afmno,
			k1.kdnr AS 'agent_id',k1.name1 AS 'agent_name1',k1.name2 AS 'agent_name2',k1.afmno AS 'agent_afmno',
			k2.kdnr AS 'company_id',k2.name1 AS 'company_name1',k2.name2 AS 'company_name2',k2.afmno AS 'company_afmno'
		FROM PROTELUSER.leisthis l 
			INNER JOIN proteluser.rechhist R ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr and l.mpehotel=r.mpehotel 
			INNER JOIN proteluser.zahlart z on z.zanr=l.rkz
			inner join proteluser.fiscalcd f on f.ref=l.fisccode 
			inner join proteluser.lizenz li on li.mpehotel=l.mpehotel 
			left outer join proteluser.buch b on b.leistacc=l.buchref 
			left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=b.kundennr  
			left outer JOIN PROTELUSER.KUNDEN K1 ON K1.KDNR=b.reisenr 
			left outer JOIN PROTELUSER.KUNDEN K2 ON K2.KDNR=b.firmennr  
		inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
		WHERE 
			l.fisccode > 0 AND
			r.datum > l.datum AND
			l.rechnr > 0 AND
			arout = 0 AND
			withdrawl = 0 AND
			z.typ <> 1 
		    and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
		GROUP BY 
			l.datum,l.rdatum,l.mpehotel,z.typ,z.fibukto,z.zanr,l.rechnr,k.kdnr,k.name1,k.name2,k.afmno,k1.kdnr,k1.name1,k1.name2,k1.afmno,k2.kdnr,k2.name1,k2.name2,k2.afmno
		
		Union All  

		SELECT * FROM 
		(
			SELECT 
				l.datum,
				l.rdatum  as 'bankdatum',
				l.mpehotel,  
				z.typ AS 'typ', 
				z.fibukto,
				z.zanr AS 'payment_method', 
				(( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',	
				k.kdnr AS 'customer_id',k.name1,k.name2,k.afmno,
				k1.kdnr AS 'agent_id',k1.name1 AS 'agent_name1',k1.name2 AS 'agent_name2',k1.afmno AS 'agent_afmno',
				k2.kdnr AS 'company_id',k2.name1 AS 'company_name1',k2.name2 AS 'company_name2',k2.afmno AS 'company_afmno'
			FROM PROTELUSER.leisthis L 
				INNER JOIN proteluser.zahlart z on z.zanr=l.rkz 
				inner join proteluser.lizenz li on li.mpehotel=l.mpehotel 
				left outer join proteluser.buch b on b.leistacc=l.buchref  
				left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=b.kundennr 
				left outer JOIN PROTELUSER.KUNDEN K1 ON K1.KDNR=b.reisenr 
				left outer JOIN PROTELUSER.KUNDEN K2 ON K2.KDNR=b.firmennr
			WHERE
				l.fisccode < 1 AND
				arout = 0 AND
				withdrawl = 0 AND
				z.typ <> 1 
			GROUP BY 
				l.datum,l.rdatum,l.mpehotel,z.typ,z.fibukto,z.zanr,k.kdnr,k.name1,k.name2,k.afmno,k1.kdnr,k1.name1,k1.name2,k1.afmno,k2.kdnr,k2.name1,k2.name2,k2.afmno

			Union All 
  
			SELECT * FROM 
			(
				SELECT 
					l.datum,
					l.rdatum  as 'bankdatum',
					l.mpehotel,  
					z.typ AS 'typ', 
					z.fibukto,
					z.zanr AS 'payment_method', 
					(( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',	
					k.kdnr AS 'customer_id',k.name1,k.name2,k.afmno,
					k1.kdnr AS 'agent_id',k1.name1 AS 'agent_name1',k1.name2 AS 'agent_name2',k1.afmno AS 'agent_afmno',
					k2.kdnr AS 'company_id',k2.name1 AS 'company_name1',k2.name2 AS 'company_name2',k2.afmno AS 'company_afmno'
				FROM PROTELUSER.leisthis l INNER JOIN proteluser.rechhist R ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr   and l.mpehotel=r.mpehotel
					INNER JOIN proteluser.zahlart z on z.zanr=l.rkz inner join proteluser.fiscalcd f on f.ref=l.fisccode 
					inner join proteluser.lizenz li on li.mpehotel=l.mpehotel 
					left outer join proteluser.buch b on b.leistacc=l.buchref  
					left outer JOIN PROTELUSER.KUNDEN K ON K.KDNR=b.kundennr 
					left outer JOIN PROTELUSER.KUNDEN K1 ON K1.KDNR=b.reisenr   
					left outer JOIN PROTELUSER.KUNDEN K2 ON K2.KDNR=b.firmennr 
				WHERE 
					l.fisccode > 0 AND
					r.datum > l.datum AND
					l.rechnr > 0 AND
					arout = 0 AND
					withdrawl = 0 AND 
					z.typ = 1 
				GROUP BY 
					l.datum,l.rdatum,l.mpehotel,z.typ,z.fibukto,z.zanr,l.rechnr,k.kdnr,k.name1,k.name2,k.afmno,k1.kdnr,k1.name1,k1.name2,k1.afmno,k2.kdnr,k2.name1,k2.name2,k2.afmno
			) sss

			Union All 

			SELECT * FROM 
			(
				SELECT 
					l.datum,
					l.rdatum  as 'bankdatum',
					l.mpehotel,  
					z.typ AS 'typ', 
					z.fibukto,
					z.zanr AS 'payment_method', 
					(( sum(l.epreis*l.anzahl))*-1) AS 'total_amount',	
					k.kdnr AS 'customer_id',k.name1,k.name2,k.afmno,
					k1.kdnr AS 'agent_id',k1.name1 AS 'agent_name1',k1.name2 AS 'agent_name2',k1.afmno AS 'agent_afmno',
					k2.kdnr AS 'company_id',k2.name1 AS 'company_name1',k2.name2 AS 'company_name2',k2.afmno AS 'company_afmno'
				FROM leisthis L  INNER JOIN zahlart z on z.zanr=l.rkz        
					inner join lizenz li on li.mpehotel=l.mpehotel
					left outer join buch b on b.leistacc=l.buchref 
					left outer JOIN KUNDEN K ON K.KDNR=b.kundennr
					left outer JOIN KUNDEN K1 ON K1.KDNR=b.reisenr
					left outer JOIN KUNDEN K2 ON K2.KDNR=b.firmennr
				WHERE 
					l.fisccode < 1 AND
					arout = 0 AND
					withdrawl = 0 AND
					z.typ = 1
				GROUP BY 
					l.datum,l.rdatum,l.mpehotel,z.typ,z.fibukto,z.zanr,k.kdnr,k.name1,k.name2,k.afmno,k1.kdnr,k1.name1,k1.name2,k1.afmno,k2.kdnr,k2.name1,k2.name2,k2.afmno
			)DEBADVANCE
		) DEBCASH
	)DEBADVANCE
) DEBCASH

       ) as salesjournal WHERE datum BETWEEN @dfrom AND @dto";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }

        public void Save_SalesDepositOutSoft_To_hit_estia_sales_journal_deposit_out_soft(eaDateModel model)
        {
            string Sql = @"

INSERT INTO hit_estia_sales_journal_deposit_out_soft
		SELECT *
       FROM  (SELECT 
	r.datum,
	l.datum AS 'leistdatum',
	r.rechnr,
	r.mpehotel,
	f.ref AS 'fiscalcode',
	z.fibukto, 
	z.zanr AS 'payment_method',
	(( sum(l.epreis*l.anzahl))*-1) AS 'total_amount', 
	k.kdnr AS 'customer_id', k.name1, 
	k1.kdnr AS 'invoice_id', k1.name1 AS 'invoice_name1', k1.name2 AS 'invoice_name2', k1.afmno AS 'invoice_afmno',k1.land AS 'invoice_land'
FROM rechhist R
	INNER JOIN leisthis l ON L.FISCCODE=R.fisccode AND L.rechnr=R.rechnr and l.mpehotel=r.mpehotel
	INNER JOIN zahlart z on z.zanr=l.rkz
	inner join fiscalcd f on f.ref=l.fisccode
	inner join lizenz li on li.mpehotel=r.mpehotel
	LEFT OUTER JOIN BUCHOLD b on b.buchnr=l.buchref
	left outer JOIN KUNDEN K ON K.KDNR=b.kundennr
	left outer JOIN KUNDEN K1 ON K1.KDNR=R.kdnr
	left outer join natcode nat on nat.codenr=k1.landkz
		inner join proteluser.datum dt on dt.mpehotel = r.mpehotel
WHERE
	l.fisccode > 0 AND
	r.datum > l.datum AND
	l.rechnr > 0 AND
	arout = 0 AND
	withdrawl = 0
		and r.datum BETWEEN @dfrom AND @dto -- = DATEADD(d, -1, dt.pdate)
GROUP BY 
	r.datum ,R.mpehotel,l.rechnr,K.NAME1,z.fibukto,k.kdnr,K1.NAME1,k1.kdnr,
	K1.LAND,K1.name2,f.ref,z.zanr,K1.AFMNO, R.RECHNR, l.datum ) aa";

            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute(Sql, new { dfrom = model.dateFrom, dto = model.dateTo }, null, 600);
            }
        }



        public void GetMainCourante()
        {


        }

        public List<KundenModel> GetCustomers(eaDateModel model)
        {
            List<KundenModel> results = new List<KundenModel>();
            
            CreateTable tables = new CreateTable();

            List<HitToPylonDocsModel> hittopylon = new List<HitToPylonDocsModel>();

            StringBuilder SQL = new StringBuilder();

            tables.GenerateCustomerDataReference(ConnString, model);

            string sqlCustomerRef = @"
                IF  EXISTS ( SELECT * FROM SYS.TABLES WHERE [name] = 'hit_to_pylon_docs' AND [type] = 'U' ) 
                SELECT Top " + model.Size + @" kdnr, rechnr, fisccode, mpehotel, is_sent, date_created, Kind 
                FROM hit_to_pylon_docs where is_sent = 0 and Kind = 1";

            try
            {
                string sqlCustomerData = @"
                    SELECT kdnr,mpehotel,name1,name2,vorname,strasse,plz,ort,land,telefonnr,contract,vatno,afmno,fibudeb 
                    FROM kunden 
                    WHERE kdnr = @kdnr AND typ = 1";

                using (IDbConnection db = new SqlConnection(ConnString))
                {
                    hittopylon = db.Query<HitToPylonDocsModel>(sqlCustomerRef, null, null, 600).ToList();
                    
                    if (hittopylon.Count == 0)
                    {
                        logger.Error("Customer were not fetched successfully " + SQL.ToString() + " \r\n");

                        return results;
                    }

                    foreach (HitToPylonDocsModel Row in hittopylon)
                    {
                        KundenModel result = db.Query<KundenModel>(sqlCustomerData, new { kdnr = Row.kdnr }, null, true, 600).FirstOrDefault();

                        results.Add(result);
                    }
                }
            }

            catch (Exception ex)
            {
                logger.Error("Main Courante data were not fetched successfully. \r\n " + SQL.ToString() + " \r\n " + ex.ToString());

                throw new Exception(ex.ToString());
            }

            return results;
        }
    }
}
