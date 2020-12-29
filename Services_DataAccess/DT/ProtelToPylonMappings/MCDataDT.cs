using Dapper;
using Hit.Services.Core;
using Hit.Services.DataAccess.DT.SQL;
using Hit.Services.Helpers.Classes;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.QueriesLogistics;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DataAccess.DT.ProtelToPylonMappings
{
    public class MCDataDT
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string ConnString;

        SettingsModel settings;

        public MCDataDT(SettingsModel settings)
        {
            this.settings = settings;

            ConnString = settings.ProtelDB;
        }

        public List<MCDataModel> mcData(eaDateModel model)
        {
            List<MCDataModel> results = new List<MCDataModel>();

            CreateTable tables = new CreateTable();

            tables.DeleteDataFromMCTables(ConnString);

            tables.InsertDataIntoMCTables(ConnString, model);

            tables.GenerateMCDataReference(ConnString, model);

            results = GetMCData(model);

            return results;
        }

        public List<MCDataModel> GetMCData(eaDateModel model)
        {
            List<MCDataModel> results = new List<MCDataModel>(); 
            List<DateTime> days = DateTimeHelper.EachDay(model.dateFrom, model.dateTo).ToList();

            if(days.Count > Convert.ToInt32 (model.Size))
            {
                days.RemoveRange(Convert.ToInt32(model.Size), days.Count - Convert.ToInt32(model.Size) - 1);
            }

            try
            {
                string sqlCustomer = "SELECT kdnr,mpehotel,name1,name2,vorname,strasse,plz,ort,land,telefonnr,contract,vatno,afmno,fibudeb FROM kunden WHERE kdnr=@kdnr";
                string sqlHittopylon = "IF  EXISTS ( SELECT * FROM SYS.TABLES WHERE [name] = 'hit_to_pylon_docs' AND [type] = 'U' ) SELECT kdnr,rechnr, fisccode, mpehotel, is_sent, date_created,Kind FROM hit_to_pylon_docs where is_sent=0 and Kind in (9,10,11,7) and date_created=@day and mpehotel=@mpehotel";
                string sqlMCIncomeLists = "SELECT datum,mpehotel,vatno,department,transaction_id,line,log_account,amount,tax from hit_estia_mc_income WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCCashLists = "SELECT datum,mpehotel,rechnr,fiscalcode,log_account,kdnr,name1,afmno,fibudeb,typ,amount,kind,payment_method from hit_estia_mc_cash WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCCardLists = "SELECT datum,mpehotel,rechnr,fiscalcode,log_account,kdnr,name1,afmno,fibudeb,typ,amount,kind,payment_method from hit_estia_mc_card WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCBankLists = "SELECT datum,mpehotel,rechnr,fiscalcode,log_account,kdnr,name1,afmno,fibudeb,typ,amount,kind,payment_method from hit_estia_mc_bank WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCDebitorLists = "SELECT datum,mpehotel,rechnr,fiscalcode,log_account,kdnr,name1,afmno,fibudeb,typ,plz,strasse,ort,land,guest_id,guest_name,amount,kind,payment_method from hit_estia_mc_debitor WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCDepositNewLists = "SELECT datum,mpehotel,leist_ref,log_account,kdnr,name1,afmno,fibudeb,typ,amount,payment_method,debit from hit_estia_mc_deposit_new WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlMCDepositInhouseLists = "SELECT datum,mpehotel,leist_ref,log_account,kdnr,name1,afmno,fibudeb,typ,amount,payment_method,debit from hit_estia_mc_deposit_inhouse WHERE datum=@day AND mpehotel = @mpehotel";
                string sqlARPayoffLists = "SELECT belegdat,datum,mpehotel,typ,fibukto,rechnr,total_amount,fiscalcode,customer_id,payment_method,name1,name2,afmno,fibudeb,doy,land,strasse,ort,plz from hit_estia_sales_journal_ar WHERE datum=@day AND mpehotel = @mpehotel";
                
                foreach(DateTime day in days)
                {
                    MCDataModel dayResult = new MCDataModel();
                    List<HitToPylonDocsModel> hittopylon = new List<HitToPylonDocsModel>();
                    List<int> customerIDs = new List<int>();
                    List<KundenModel> customersList = new List<KundenModel>();
                    List<MCIncomeModel> mcIncomeList = new List<MCIncomeModel>();
                    List<MCCashModel> mcCashList = new List<MCCashModel>();
                    List<MCCardModel> mcCardList = new List<MCCardModel>();
                    List<MCBankModel> mcBankList = new List<MCBankModel>();
                    List<MCDebitorModel> mcDebitorList = new List<MCDebitorModel>();
                    List<MCDepositNewModel> mcDepositNewList = new List<MCDepositNewModel>();
                    List<MCDepositInhouseModel> mcDepositInhouseList = new List<MCDepositInhouseModel>();
                    List<eaSalesJournalARModel> arPayoffList = new List<eaSalesJournalARModel>();

                    dayResult.date = day;
                    dayResult.mpehotel = Convert.ToInt32(model.mpehotel);

                    using (IDbConnection db = new SqlConnection(ConnString))
                    {
                        hittopylon = db.Query<HitToPylonDocsModel>(sqlHittopylon, new { day = day, mpehotel = model.mpehotel }).ToList();

                        if (hittopylon.Count == 0)
                        {
                            logger.Error("Data collection failed for date: " + day.ToString());
                            return results;
                        }

                        foreach (HitToPylonDocsModel Row in hittopylon)
                        {
                            switch (Convert.ToInt32(Row.Kind))
                            {
                                case 9:
                                    mcIncomeList = db.Query<MCIncomeModel>(sqlMCIncomeLists, new { day = day, mpehotel = model.mpehotel }).ToList();
                                    mcDepositNewList = db.Query<MCDepositNewModel>(sqlMCDepositNewLists, new { day = day, mpehotel = model.mpehotel }).ToList();
                                    mcDepositInhouseList = db.Query<MCDepositInhouseModel>(sqlMCDepositInhouseLists, new { day = day, mpehotel = model.mpehotel }).ToList();

                                    dayResult.mcIncomeList = new List<MCIncomeModel>();
                                    dayResult.mcDepositNewList = new List<MCDepositNewModel>();
                                    dayResult.mcDepositInhouseList = new List<MCDepositInhouseModel>();

                                    foreach (MCIncomeModel row in mcIncomeList) dayResult.mcIncomeList.Add(row);
                                    foreach (MCDepositNewModel row in mcDepositNewList) dayResult.mcDepositNewList.Add(row);
                                    foreach (MCDepositInhouseModel row in mcDepositInhouseList) dayResult.mcDepositInhouseList.Add(row);

                                    break;

                                case 10:
                                    mcCashList = db.Query<MCCashModel>(sqlMCCashLists, new { day = day, mpehotel = model.mpehotel }).ToList();
                                    mcCardList = db.Query<MCCardModel>(sqlMCCardLists, new { day = day, mpehotel = model.mpehotel }).ToList();
                                    mcBankList = db.Query<MCBankModel>(sqlMCBankLists, new { day = day, mpehotel = model.mpehotel }).ToList();

                                    dayResult.mcCashList = new List<MCCashModel>();
                                    dayResult.mcCardList = new List<MCCardModel>();
                                    dayResult.mcBankList = new List<MCBankModel>();

                                    foreach (MCCashModel row in mcCashList) dayResult.mcCashList.Add(row);
                                    foreach (MCCardModel row in mcCardList) dayResult.mcCardList.Add(row);
                                    foreach (MCBankModel row in mcBankList) dayResult.mcBankList.Add(row);

                                    break;

                                case 11:
                                    mcDebitorList = db.Query<MCDebitorModel>(sqlMCDebitorLists, new { day = day, mpehotel = model.mpehotel }).ToList();

                                    dayResult.mcDebitorList = new List<MCDebitorModel>();

                                    foreach (MCDebitorModel row in mcDebitorList) dayResult.mcDebitorList.Add(row);

                                    break;

                                case 7:
                                    arPayoffList = db.Query<eaSalesJournalARModel>(sqlARPayoffLists, new { day = day, mpehotel = model.mpehotel }).ToList();

                                    dayResult.arPayoffList = new List<eaSalesJournalARModel>();

                                    foreach (eaSalesJournalARModel row in arPayoffList) dayResult.arPayoffList.Add(row);

                                    break;
                            }                               
                        }

                        
                        customerIDs.AddRange(mcDepositNewList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(mcDepositInhouseList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(mcCashList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(mcCardList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(mcBankList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(mcDebitorList.Distinct().Select(r => r.kdnr).ToList());
                        customerIDs.AddRange(arPayoffList.Distinct().Select(r => r.customer_id).ToList());
                        
                        dayResult.customersList = new List<KundenModel>();
                        foreach (int customerID in customerIDs)
                        {
                            if (!dayResult.customersList.Where(c => c.kdnr == customerID).Any())
                            {
                                KundenModel cust = db.Query<KundenModel>(sqlCustomer, new { kdnr = customerID }).First();
                                dayResult.customersList.Add(cust);
                            }
                        }
                        //dayResult.customersList = customersList;

                        results.Add(dayResult);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Main Courante data were not fetched successfully. \r\n " + ex.ToString());

                throw new Exception(ex.ToString());
            }

            return results;
        }
    }
}
