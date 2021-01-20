using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;

namespace AppLogic
{

    public class cProductSummaryDB
    {

        string strQuery;

        #region GetProductSummary // used in Product Summary page
        public DataSet GetProductSummary(cProductSummary objProductSummary)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drProductSummary = null;

                SqlParameter[] Parameter = new SqlParameter[3];
                Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 20);
                Parameter[0].Value = objProductSummary.CampaignName; 

                Parameter[1] = new SqlParameter("@SoldTo", SqlDbType.VarChar, 20);
                Parameter[1].Value = objProductSummary.SoldTo;

                Decimal temp;

                if (Decimal.TryParse(objProductSummary.BuyerCt, out temp))
                {
                    Parameter[2] = new SqlParameter("@BuyerCt", SqlDbType.Decimal, 18);
                    Parameter[2].Value = objProductSummary.BuyerCt;
                }
                else
                {
                    Parameter[2] = new SqlParameter("@BuyerCt", SqlDbType.Decimal, 18);
                    Parameter[2].Value = DBNull.Value;
                }

                drProductSummary = DBHelper.ExecuteQueryToDataSet("sp_ProductLineSummary", Parameter);

                return drProductSummary;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region Get All SKU Summary
        public DataSet GetAllSKUSummary(cProductSummary objProductSummary)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drAllSKUSummary = null;

                string SQLMainQuery = string.Empty;
                string SQLOrderByQuery = string.Empty;

                SqlParameter[] Parameter = new SqlParameter[4];
                Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 20);
                Parameter[0].Value = objProductSummary.CampaignName;

                Parameter[1] = new SqlParameter("@SoldTo", SqlDbType.VarChar, 20);
                Parameter[1].Value = objProductSummary.SoldTo;

                Parameter[2] = new SqlParameter("@BuyerCt", SqlDbType.Decimal, 18);
                Decimal temp1;
                if (Decimal.TryParse(objProductSummary.BuyerCt, out temp1))
                {
                    Parameter[2].Value = objProductSummary.BuyerCt;

                }
                else
                    Parameter[2].Value = DBNull.Value;
                
                Parameter[3] = new SqlParameter("@SKU_Family", SqlDbType.VarChar, 100);
                Parameter[3].Value = objProductSummary.SKUCategory;
           
                #region oldcode
                /*
                if (objProductSummary.CampaignName == "PC")
                {
                    //SQLMainQuery = "SELECT SPACE_CODE,SKU_DESCRIPTION,SKU_NUMBER,max(b.revision_date) as last_revision_date,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy,sku_number from PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to = " + objProductSummary.SoldTo + ")";
                    //SQLOrderByQuery = " group by SPACE_CODE,SKU_Description,sku_number,sku_category,sku_family order by SPACE_CODE desc";

                    //SQLMainQuery = "SELECT SPACE_CODE as 'Space Code' " + "\n" +
                    //                       ",SKU_DESCRIPTION as 'SKU Description' " + "\n" +
                    //                       ",SKU_NUMBER as 'SKU Number' " + "\n" +
                    //                       ",max(b.revision_date) as 'Last Revision Date' " +
                    //                       ",sum(sales_3fy_ago) as 'F09 Sales' " +
                    //                       ",sum(sales_2fy_ago) as 'F10 Sales' " +
                    //                       ",sum(sales_1fy_ago) as 'F11 Sales' " +
                    //                       ",sum(sales_currfy) as 'F12 Sales' " +
                    //                       ",sum(Total_sales) as 'Lifetime Sales' " +
                    //                       ",sum(NO_orders) as 'Lifetime Orders' " +
                    //                       ",max(last_order_date) as 'Last Ordered Date' " +
                    //                       ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F09 Units' " +
                    //                       ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F10 Units' " +
                    //                       ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as  'F11 Units' " +
                    //                       ",Cast(sum(units_currfy) as Numeric(10,2)) as 'F12 Units' " +
                    //                "from PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered " +
                    //                "where (sold_to = " + objProductSummary.SoldTo + ")";

                   

                    SQLMainQuery = "SELECT SPACE_CODE as 'SPACE CODE' " + "\n" +
                                        ",SKU_DESCRIPTION as 'PRODUCT DESCRIPTION' " + "\n" +
                                        ",SKU_NUMBER as 'PRODUCT NUMBER' " + "\n" +
                                        ",max(b.revision_date) as 'LAST REVISION DATE' " +
                                        ",sum(sales_3fy_ago) as 'F11 SALES' " +
                                        ",sum(sales_2fy_ago) as 'F12 SALES' " +
                                        ",sum(sales_1fy_ago) as 'F13 SALES' " +
                                        ",sum(sales_currfy) as 'F14 SALES' " +
                                        ",sum(Total_sales) as 'LIFETIME SALES' " +
                                        ",sum(NO_orders) as 'LIFETIME ORDERS' " +
                                        ",max(last_order_date) as 'LAST ORDER DATE' " +
                                        ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F11 UNITS' " +
                                        ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F12 UNITS' " +
                                        ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as  'F13 UNITS' " +
                                        ",Cast(sum(units_currfy) as Numeric(10,2)) as 'F14 UNITS' " +
                                 "from PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered " +
                                 "where (sold_to = " + objProductSummary.SoldTo + ")";

                    SQLOrderByQuery = " group by SPACE_CODE,SKU_Description,sku_number,sku_category,sku_family order by [Space Code] desc";

                    if (objProductSummary.SKUCategory == null && objProductSummary.BuyerCt == null)
                    {
                        SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                    }
                    if (objProductSummary.SKUCategory.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And SKU_FAMILY = '" + objProductSummary.SKUCategory + "'";
                    }
                    if (objProductSummary.BuyerCt.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And BUYERCT = " + objProductSummary.BuyerCt;
                    }
                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }
                else
                {
                    //SQLMainQuery = "SELECT sku_category,sku_family,SKU_Description ,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy,sku_number from " + objProductSummary.CampaignName + ".SKU_VIEW where (sold_to = " + objProductSummary.SoldTo + ")";
                    //SQLOrderByQuery = " group by SKU_Description,sku_number,sku_category,sku_family order by Total_sales desc";

                    //SQLMainQuery = "SELECT sku_category " + "\n" +
                    //                        ",sku_family as 'SKU Family' " + "\n" +
                    //                        ",SKU_Description as 'SKU Description' " + "\n" +
                    //                        ",sku_number as 'SKU Number' " + "\n" +
                    //                        ", sum(sales_3fy_ago) as 'F09 Sales' " + "\n" +
                    //                        ",sum(sales_2fy_ago) as 'F10 Sales' " + "\n" +
                    //                        ",sum(sales_1fy_ago) as 'F11 Sales' " + "\n" +
                    //                        ",sum(sales_currfy) as 'F12 Sales' " + "\n" +
                    //                        ",sum(Total_sales) as 'Lifetime Sales' " + "\n" +
                    //                        ",sum(NO_orders) as 'Lifetime Orders' " + "\n" +
                    //                        ",max(last_order_date) as 'Last Ordered Date' " + "\n" +
                    //                        ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F09 Units' " + "\n" +
                    //                        ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F10 Units' " + "\n" +
                    //                        ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as 'F11 Units' " + "\n" +
                    //                        ",Cast(sum(units_currfy) as Numeric(10,2)) as 'F12 Units' " + "\n" +
                    //                "from " + objProductSummary.CampaignName + ".SKU_VIEW " + "\n" +
                    //                "where (sold_to = " + objProductSummary.SoldTo + ")";

                    SQLMainQuery = "SELECT sku_category as 'PRODUCT CATEGORY' " + "\n" +
                                            ",sku_family as 'PRODUCT FAMILY' " + "\n" +
                                            ",SKU_Description as 'PRODUCT DESCRIPTION' " + "\n" +
                                            ",sku_number as 'PRODUCT NUMBER' " + "\n" +
                                            ", sum(sales_3fy_ago) as 'F11 SALES' " + "\n" +
                                            ",sum(sales_2fy_ago) as 'F12 SALES' " + "\n" +
                                            ",sum(sales_1fy_ago) as 'F13 SALES' " + "\n" +
                                            ",sum(sales_currfy) as 'F14 SALES' " + "\n" +
                                            ",sum(Total_sales) as 'LIFETIME SALES' " + "\n" +
                                            ",sum(NO_orders) as 'LIFETIME ORDERS' " + "\n" +
                                            ",max(last_order_date) as 'LAST ORDER DATE' " + "\n" +
                                            ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F11 UNITS' " + "\n" +
                                            ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F12 UNITS' " + "\n" +
                                            ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as 'F13 UNITS' " + "\n" +
                                            ",Cast(sum(units_currfy) as Numeric(10,2)) as 'F14 UNITS' " + "\n" +
                                    "from " + objProductSummary.CampaignName + ".SKU_VIEW " + "\n" +
                                    "where (sold_to = " + objProductSummary.SoldTo + ")";

                    SQLOrderByQuery = " group by SKU_Description,sku_number,sku_category,sku_family order by [Lifetime Sales] desc";


                    if (objProductSummary.SKUCategory == null && objProductSummary.BuyerCt == null)
                    {
                        SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                    }
                    if (objProductSummary.SKUCategory.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And SKU_CATEGORY = '" + objProductSummary.SKUCategory + "'";
                    }
                    if (objProductSummary.BuyerCt.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And BUYERCT = " + objProductSummary.BuyerCt;
                    }
                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }
                 */
                #endregion

                //SqlParameter[] Parameter = new SqlParameter[4];
                //Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 20);
                //Parameter[0].Value = objProductSummary.CampaignName;

                //Parameter[1] = new SqlParameter("@SoldTo", SqlDbType.VarChar, 20);
                //Parameter[1].Value = objProductSummary.SoldTo;

                //Parameter[2] = new SqlParameter("@SKUCATEGORY", SqlDbType.VarChar, 20);
                //Parameter[2].Value = objProductSummary.SKUCategory;


                //Parameter[3] = new SqlParameter("@BuyerCt", SqlDbType.Decimal, 18);
                //Parameter[3].Value = objProductSummary.BuyerCt;

                //string test = SQLMainQuery;
                drAllSKUSummary = DBHelper.ExecuteQueryToDataSet("[dbo].[GetAllSKUSummary_Prod]", Parameter);

                return drAllSKUSummary;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        //Will be used in Teritory Page

        #region GetProductSummary used in Product Summary Territoty page
        public DataSet GetTeritoryProductSummary(cProductSummary objProductSummary)
        {
            try
            {
                //SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                //DataSet drProductSummary = null;

                //SqlParameter[] Parameter = new SqlParameter[3];
                //Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 20);
                //Parameter[0].Value = objProductSummary.CampaignName;

                //Parameter[1] = new SqlParameter("@SoldTo", SqlDbType.VarChar, 4000);
                //Parameter[1].Value = ReturnKamSoltoAccount();

                //Parameter[2] = new SqlParameter("@BuyerCt", SqlDbType.Decimal, 18);
                //Parameter[2].Value = objProductSummary.BuyerCt;


                //drProductSummary = DBHelper.ExecuteQueryToDataSet("ProductLineSummary_Territory", Parameter);

                //return drProductSummary;

                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drAllSKUSummary = null;

                string SQLMainQuery = string.Empty;
                string SQLOrderByQuery = string.Empty;

                if (objProductSummary.CampaignName == "PC")
                {
                    //SQLMainQuery = "SELECT sku_family,max(b.revision_date) as last_revision_date, sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to = 0) or sold_to in ( select SOLD_TO from PC.SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";
                    //SQLOrderByQuery = " group by sku_family order by total_sales desc";



                    SQLMainQuery = "SELECT  sku_family AS [PRODUCT FAMILY] " + "\n" +
                                            ",max(b.revision_date) as [LAST REVISION DATE] " + "\n" +
                                            ", sum(sales_3fy_ago) as [F12 SALES] " + "\n" +
                                            ",sum(sales_2fy_ago) as [F13 SALES] " + "\n" +
                                            ",sum(sales_1fy_ago) as [F14 SALES] " + "\n" +
                                            ",sum(sales_currfy) as [F15 SALES] " + "\n" +
                                            ",sum(Total_sales) as [TOTAL SALES] " + "\n" +
                                            ",sum(NO_orders) as [LIFETIME ORDERS] " + "\n" +
                                            ",max(last_order_date) as [LAST ORDER DATE] " + "\n" +
                                            ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as [F12 UNITS] " + "\n" +
                                            ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as [F13 UNITS] " + "\n" +
                                            ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as [F14 UNITS] " + "\n" +
                                            ",Cast(sum(units_currfy) as Numeric(10,2)) as [F15 UNITS]  " + "\n" +
                                    "From PC.SKU_VIEW a " + "\n" +
                                            "left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered " + "\n" +
                                            "where (sold_to = 0) or sold_to in ( select SOLD_TO from PC.SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";

                    SQLOrderByQuery = " group by sku_family order by [Lifetime Sales] desc";
                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }
                else
                {
                    //SQLMainQuery = "SELECT sku_category ,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From "  + objProductSummary.CampaignName +".SKU_VIEW where  (sold_to = 0) or sold_to in ( select SOLD_TO from "  + objProductSummary.CampaignName +".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";
                    //SQLOrderByQuery = " group by sku_category order by total_sales desc";



                    SQLMainQuery = "SELECT  sku_category as [SKU CATEGORY] " + "\n" +
                                            ",sum(sales_3fy_ago) as [F12 SALES] " + "\n" +
                                            ",sum(sales_2fy_ago) as [F13 SALES] " + "\n" +
                                            ",sum(sales_1fy_ago) as [F14 SALES] " + "\n" +
                                            ",sum(sales_currfy) as [F15 SALES] " + "\n" +
                                            ",sum(Total_sales) AS [TOTAL SALES] " + "\n" +
                                            ",sum(NO_orders) as [LIFETIME ORDERS] " + "\n" +
                                            ",max(last_order_date) as [LAST ORDER DATE] " + "\n" +
                                            ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as [F12 UNITS] " + "\n" +
                                            ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as [F13 UNITS] " + "\n" +
                                            ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as [F14 UNITS] " + "\n" +
                                            ",Cast(sum(units_currfy) as Numeric(10,2)) as [F15 UNITS] " + "\n" +
                                   "From " + objProductSummary.CampaignName + ".SKU_VIEW " + "\n" +
                                           "where  (sold_to = 0) or sold_to in ( select SOLD_TO from " + objProductSummary.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";

                    SQLOrderByQuery = " group by sku_category order by [TOTAL SALES] desc";

                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }

                string test = SQLMainQuery;
                drAllSKUSummary = DBHelper.ExecuteQueryToDataSet(SQLMainQuery);

                return drAllSKUSummary;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region Get All SKU Summary   //Used in Product Summary Teritory

        public DataSet GetAllTeritorySKUSummary(cProductSummary objProductSummary)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drAllSKUSummary = null;

                string SQLMainQuery = string.Empty;
                string SQLOrderByQuery = string.Empty;

                if (objProductSummary.CampaignName == "PC")
                {
                    //SQLMainQuery = "SELECT SPACE_CODE as 'Space Code',SKU_DESCRIPTION as 'SKU Description',SKU_NUMBER as 'SKU Number',max(b.revision_date) as 'Last Revision Date',sum(sales_3fy_ago) as 'F09 Sales',sum(sales_2fy_ago) as 'F10 Sales',sum(sales_1fy_ago) as 'F11 Sales',sum(sales_currfy) as 'F12 Sales',sum(Total_sales) as 'Lifetime Sales',sum(NO_orders) as 'Lifetime Orders',max(last_order_date) as 'Last Ordered Date',Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F09 Units',Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F10 Units',Cast(sum(units_1fy_ago) as Numeric(10,2)) as  'F11 Units',Cast(sum(units_currfy) as Numeric(10,2)) as 'F12 Units' from PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to =0) or sold_to in ( select SOLD_TO from PC.SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";
                    //SQLOrderByQuery = " group by SPACE_CODE,SKU_Description,sku_number,sku_category,sku_family order by [Space Code] desc";


                    SQLMainQuery = "SELECT SPACE_CODE " + "\n" +
                                        ",SKU_Description' " + "\n" +
                                        ",sku_number " + "\n" +
                                        ",max(b.revision_date) as revision_date" +
                                        ",sum(sales_3fy_ago) as sales_3fy_ago " +
                                        ",sum(sales_2fy_ago) as sales_2fy_ago " +
                                        ",sum(sales_1fy_ago) as sales_1fy_ago " +
                                        ",sum(sales_currfy) as sales_currfy " +
                                        ",sum(Total_sales) as Total_sales " +
                                        ",sum(NO_orders) as NO_orders " +
                                        ",max(last_order_date) as last_order_date " +
                                        ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as units_3fy_ago " +
                                        ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as units_2fy_ago " +
                                        ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as  units_1fy_ago " +
                                        ",Cast(sum(units_currfy) as Numeric(10,2)) as units_currfy " +
                                    "from PC.SKU_VIEW a  " + "\n" +
                                          "left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to =0) or sold_to in ( select SOLD_TO from PC.SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";

                    SQLOrderByQuery = " group by SPACE_CODE,SKU_Description,sku_number,sku_category,sku_family order by SPACE_CODE  desc";

                    if (objProductSummary.SKUCategory == null && objProductSummary.BuyerCt == null)
                    {
                        SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                    }
                    if (objProductSummary.SKUCategory.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And SKU_FAMILY = '" + objProductSummary.SKUCategory + "'";
                    }

                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }
                else
                {
                    //SQLMainQuery = "SELECT sku_category,sku_family as 'SKU Family',SKU_Description as 'SKU Description',sku_number as 'SKU Number', sum(sales_3fy_ago) as 'F09 Sales',sum(sales_2fy_ago) as 'F10 Sales',sum(sales_1fy_ago) as 'F11 Sales',sum(sales_currfy) as 'F12 Sales',sum(Total_sales) as 'Lifetime Sales',sum(NO_orders) as 'Lifetime Orders',max(last_order_date) as 'Last Ordered Date',Cast(sum(units_3fy_ago) as Numeric(10,2)) as 'F09 Units',Cast(sum(units_2fy_ago) as Numeric(10,2)) as 'F10 Units',Cast(sum(units_1fy_ago) as Numeric(10,2)) as 'F11 Units',Cast(sum(units_currfy) as Numeric(10,2)) as 'F12 Units' from " + objProductSummary.CampaignName + ".SKU_VIEW where sold_to = 0 or sold_to in ( select SOLD_TO from " + objProductSummary.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";
                    //SQLOrderByQuery = " group by SKU_Description,sku_number,sku_category,sku_family order by [Lifetime Sales] desc";

                    SQLMainQuery = "SELECT sku_category " + "\n" +
                                            ",sku_family " + "\n" +
                                            ",SKU_Description " + "\n" +
                                            ",sku_number  " + "\n" +
                                            ",sum(sales_3fy_ago) as sales_3fy_ago " + "\n" +
                                            ",sum(sales_2fy_ago) as sales_2fy_ago " + "\n" +
                                            ",sum(sales_1fy_ago) as sales_1fy_ago " + "\n" +
                                            ",sum(sales_currfy) as sales_currfy" + "\n" +
                                            ",sum(Total_sales) as Total_sales " + "\n" +
                                            ",sum(NO_orders) as NO_orders " + "\n" +
                                            ",max(last_order_date) as last_order_date " + "\n" +
                                            ",Cast(sum(units_3fy_ago) as Numeric(10,2)) as units_3fy_ago " + "\n" +
                                            ",Cast(sum(units_2fy_ago) as Numeric(10,2)) as units_2fy_ago " + "\n" +
                                            ",Cast(sum(units_1fy_ago) as Numeric(10,2)) as units_1fy_ago " + "\n" +
                                            ",Cast(sum(units_currfy) as Numeric(10,2)) as units_currfy " + "\n" +
                                    "from " + objProductSummary.CampaignName + ".SKU_VIEW " + "\n" +
                                            "where sold_to = 0 or sold_to in ( select SOLD_TO from " + objProductSummary.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "')";

                    SQLOrderByQuery = " group by SKU_Description,sku_number,sku_category,sku_family order by Total_sales desc";





                    if (objProductSummary.SKUCategory == null && objProductSummary.BuyerCt == null)
                    {
                        SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                    }
                    if (objProductSummary.SKUCategory.Length > 0)
                    {
                        SQLMainQuery = SQLMainQuery + " And SKU_CATEGORY = '" + objProductSummary.SKUCategory + "'";
                    }

                    SQLMainQuery = SQLMainQuery + SQLOrderByQuery;
                }

                string test = SQLMainQuery;
                drAllSKUSummary = DBHelper.ExecuteQueryToDataSet(SQLMainQuery);

                return drAllSKUSummary;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion



        protected string ReturnKamSoltoAccount()
        {
            string ReturnAccNo = string.Empty;

            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            DataSet drProductSummary = null;

            SqlParameter[] Parameter = new SqlParameter[2];
            Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 20);
            Parameter[0].Value = SessionFacade.CampaignValue;

            Parameter[1] = new SqlParameter("@SalesTeam", SqlDbType.VarChar, 20);
            Parameter[1].Value = SessionFacade.KamId;


            drProductSummary = DBHelper.ExecuteQueryToDataSet("usp_Kam_SelectAccountNo", Parameter);

            if (drProductSummary.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in drProductSummary.Tables[0].Rows)
                {
                    ReturnAccNo += dr["SOLD_TO"] + ",";
                }
                ReturnAccNo.Remove(ReturnAccNo.Length - 1, 1);
                return ReturnAccNo;
            }
            else
            {
                return "0";
            }
        }

    }
}