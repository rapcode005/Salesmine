using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AppLogic;
using System.Collections;
using System.IO;
using ClassLibrary1;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Windows;

namespace WebSalesMine.WebPages.QuoteGuidance
{
    public partial class QuotesGuidance : System.Web.UI.Page
    {
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    LoadCustomerType();
        //    LoadProductLine();
        //    LoadQuoteBucket();
        //    LoadQuoteType();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            string ControlId = string.Empty;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            Label lblTemp = Master.FindControl("lblAccountName") as Label;
            if (!IsPostBack)
            {
                     LoadCustomerType();
                     LoadProductLine();
                     LoadQuoteBucket();
                     LoadQuoteType();
                     DisableAllFilterControl();
                     ResultControl(false, "", ddlTemp.SelectedValue);
            }
            else
            {
                
                ControlId = getPostBackControlID();
                if (ControlId == "ddlCampaign")
                {
                    LoadCustomerType();
                    LoadProductLine();
                    LoadQuoteBucket();
                    LoadQuoteType();

                }
                ResultControl(false, "", ddlTemp.SelectedValue);
            }

            //Hide Account Name
            lblTemp.Visible = false;
        }


        private string getPostBackControlID()
        {
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by       the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }
            return control.ID;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsSelected = new DataSet();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteGuidance objQuoteGuidance = new cQuoteGuidance();
                objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                ClearResultControl();
                if (rdoSearchBy4Categories.Checked == true)
                {
                    objQuoteGuidance.CustType = ddlCustType.SelectedValue;
                    objQuoteGuidance.ProdLine = ddlProdLine.SelectedValue;
                    objQuoteGuidance.QuoteBucket = ddlQuoteBucket.SelectedValue;
                    objQuoteGuidance.QuoteType = ddlQuoteType.SelectedValue;

                    if (ddlTemp.SelectedValue != "EMED" &&
                        ddlTemp.SelectedValue != "CA" &&
                        ddlTemp.SelectedValue != "US" &&
                        ddlTemp.SelectedValue != "CL")
                    {
                        objQuoteGuidance.MaterialEntrd = txtMaterialEntrd.Text;
                    }
                }
                else if (rdoSearchByCustomerNo.Checked == true)
                {
                    if (txtCustomerNumber.Text == "")
                    {
                        lblInstruction.Text = "Fill up the all required fields.";
                    }
                    else
                    {
                        int temp;
                        if (int.TryParse(txtCustomerNumber.Text.ToString(), out temp) == true)
                            txtCustomerNumber.Text = string.Format("{0:0000000000}", int.Parse(txtCustomerNumber.Text.ToString()));
                        else
                            txtCustomerNumber.Text = "0000000000";
                        objQuoteGuidance.Account = txtCustomerNumber.Text.ToString().Trim().PadLeft(10, '0');
                        objQuoteGuidance.CustType = ddlCustType.SelectedValue;
                        objQuoteGuidance.ProdLine = ddlProdLine.SelectedValue;
                        objQuoteGuidance.QuoteBucket = ddlQuoteBucket.SelectedValue;
                        objQuoteGuidance.QuoteType = ddlQuoteType.SelectedValue;
                        if (ddlTemp.SelectedValue != "EMED" &&
                           ddlTemp.SelectedValue != "CA" &&
                           ddlTemp.SelectedValue != "US" &&
                           ddlTemp.SelectedValue != "CL")
                        {
                            objQuoteGuidance.MaterialEntrd = txtMaterialEntrd.Text;
                        }
                    }
                }
                else if (rdoSearchByContactNumber.Checked == true)
                {
                    if (txtContactNumber.Text == "")
                    {
                        lblInstruction.Text = "Fill up the all required fields.";
                    }
                    else
                    {
                        objQuoteGuidance.Contact = txtContactNumber.Text;
                        objQuoteGuidance.CustType = ddlCustType.SelectedValue;
                        objQuoteGuidance.ProdLine = ddlProdLine.SelectedValue;
                        objQuoteGuidance.QuoteBucket = ddlQuoteBucket.SelectedValue;
                        objQuoteGuidance.QuoteType = ddlQuoteType.SelectedValue;
                        if (ddlTemp.SelectedValue != "EMED" &&
                           ddlTemp.SelectedValue != "CA" &&
                           ddlTemp.SelectedValue != "US" &&
                           ddlTemp.SelectedValue != "CL")
                        {
                            objQuoteGuidance.MaterialEntrd = txtMaterialEntrd.Text;
                        }
                    }
                }
                else
                {
                    if (txtQuoteNumber.Text == "")
                        lblInstruction.Text = "Fill up the all required fields.";
                    else
                    {
                        int temp;
                        if (int.TryParse(txtQuoteNumber.Text.ToString(), out temp) == true)
                            txtQuoteNumber.Text = string.Format("{0:0000000000}", int.Parse(txtQuoteNumber.Text.ToString()));
                        else
                            txtQuoteNumber.Text = "0000000000";
                        if (ddlTemp.SelectedValue != "EMED" &&
                           ddlTemp.SelectedValue != "CA" &&
                           ddlTemp.SelectedValue != "US" &&
                           ddlTemp.SelectedValue != "CL")
                        {
                            objQuoteGuidance.MaterialEntrd = txtMaterialEntrd.Text;
                        }
                        objQuoteGuidance.ProdLine = ddlProdLine.SelectedValue;
                        objQuoteGuidance.QuoteNumber = txtQuoteNumber.Text;
                    }
                }

                if (lblInstruction.Text != "Fill up the all required fields")
                {
                    dsSelected = objQuoteGuidance.SelectQuoteDiscountGuidance();
                    if (dsSelected.Tables.Count > 0)
                    {
                        if (dsSelected.Tables[0].Rows.Count > 0)
                        {
                            if (txtQuoteNumber.Text != "")
                            {
                                txtCustomerNumber.Text = CheckIfColumnExist(dsSelected, "CUSMERGE");
                                lblCustomerNameValue.Text = CheckIfColumnExist(dsSelected, "SiteName");
                                txtContactNumber.Text = CheckIfColumnExist(dsSelected, "buyerct_SVR");
                                lblContactNameValue.Text = CheckIfColumnExist(dsSelected, "ContactName");
                                ddlCustType.SelectedValue = CheckIfColumnExist(dsSelected, "tag");
                                ddlProdLine.Text = CheckIfColumnExist(dsSelected, "dm_product_line_desc");
                                ddlQuoteBucket.Text = CheckIfColumnExist(dsSelected, "Quote_Bucket");
                                ddlQuoteType.Text = CheckIfColumnExist(dsSelected, "Quote_Doc_Reason_Code_Desc");

                                if (ddlTemp.SelectedValue == "US" ||
                                    ddlTemp.SelectedValue == "PC" ||
                                    ddlTemp.SelectedValue == "EMED" ||
                                    ddlTemp.SelectedValue == "CA")
                                {
                                    lblResellerValue.Text = CheckIfColumnExist(dsSelected, "Reseller");
                                    lblGovernmentValue.Text = CheckIfColumnExist(dsSelected, "Government");
                                }
                                float temp;
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_quote_disc"), out temp))
                                    lblAverageQuoteDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_quote_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageQuoteDiscountValue.Text = "0.00 %";

                                lblSuccessRateSiteValue.Text = CheckIfColumnExist(dsSelected, "Percent_Conversion_Site").ToString().Trim();

                                lblSuccessRateContactValue.Text = CheckIfColumnExist(dsSelected, "percent_conversion_cont").ToString().Trim();

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_order_disc"), out temp))
                                    lblAverageOrderDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_order_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageOrderDiscountValue.Text = "0.00 %";

                                lblNumQuotesValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes");
                                lblNumOrdersValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes_Conv");
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv"), out temp))
                                    lblCloseRateValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblCloseRateValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Quote"), out temp))
                                    lblGMQuoteValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Quote")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGMQuoteValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Order"), out temp))
                                    lblGmOrderValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Order")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGmOrderValue.Text = "0.00 %";
                            }
                            else if (txtCustomerNumber.Text != "")
                            {
                                lblCustomerNameValue.Text = CheckIfColumnExist(dsSelected, "SiteName");
                                txtQuoteNumber.Text = CheckIfColumnExist(dsSelected, "Quote_Doc_No");
                                //ddlCustType.Text = CheckIfColumnExist(dsSelected, "tag");
                                //ddlProdLine.Text = CheckIfColumnExist(dsSelected, "dm_product_line_desc");
                                //ddlQuoteBucket.Text = CheckIfColumnExist(dsSelected, "Quote_Bucket");
                                //ddlQuoteType.Text = CheckIfColumnExist(dsSelected, "Quote_Doc_Reason_Code_Desc").Replace("   ", " ");
                                
                                if (ddlTemp.SelectedValue == "US" ||
                                   ddlTemp.SelectedValue == "PC" ||
                                   ddlTemp.SelectedValue == "EMED" ||
                                   ddlTemp.SelectedValue == "CA")
                                {
                                    lblResellerValue.Text = CheckIfColumnExist(dsSelected, "Reseller");
                                    lblGovernmentValue.Text = CheckIfColumnExist(dsSelected, "Government");
                                }
                                float temp;
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_quote_disc"), out temp))
                                    lblAverageQuoteDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_quote_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageQuoteDiscountValue.Text = "0.00 %";

                                lblSuccessRateSiteValue.Text = CheckIfColumnExist(dsSelected, "Percent_Conversion_Site").ToString().Trim();

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_order_disc"), out temp))
                                    lblAverageOrderDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_order_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageOrderDiscountValue.Text = "0.00 %";

                                lblNumQuotesValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes");
                                lblNumOrdersValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes_Conv");
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv"), out temp))
                                    lblCloseRateValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblCloseRateValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Quote"), out temp))
                                    lblGMQuoteValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Quote")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGMQuoteValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Order"), out temp))
                                    lblGmOrderValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Order")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGmOrderValue.Text = "0.00 %";
                            }
                            else if (txtContactNumber.Text != "")
                            {
                                lblContactNameValue.Text = CheckIfColumnExist(dsSelected, "ContactName");
                                lblCustomerNameValue.Text = CheckIfColumnExist(dsSelected, "SiteName");
                                //txtQuoteNumber.Text = CheckIfColumnExist(dsSelected, "Quote_Doc_No");
                                txtCustomerNumber.Text = CheckIfColumnExist(dsSelected, "CUSMERGE");
                                //ddlCustType.Text = CheckIfColumnExist(dsSelected, "tag");
                               // ddlProdLine.Text = CheckIfColumnExist(dsSelected, "dm_product_line_desc");
                               // ddlQuoteBucket.Text = CheckIfColumnExist(dsSelected, "Quote_Bucket");
                               // ddlQuoteType.Text = CheckIfColumnExist(dsSelected, "Quote_Doc_Reason_Code_Desc");
                                if (ddlTemp.SelectedValue == "US" ||
                                ddlTemp.SelectedValue == "PC" ||
                                ddlTemp.SelectedValue == "EMED" ||
                                ddlTemp.SelectedValue == "CA")
                                {
                                    lblResellerValue.Text = CheckIfColumnExist(dsSelected, "Reseller");
                                    lblGovernmentValue.Text = CheckIfColumnExist(dsSelected, "Government");
                                }
                                float temp;
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_quote_disc"), out temp))
                                    lblAverageQuoteDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_quote_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageQuoteDiscountValue.Text = "0.00 %";

                                lblSuccessRateSiteValue.Text = CheckIfColumnExist(dsSelected, "Percent_Conversion_Site").ToString().Trim();

                                lblSuccessRateContactValue.Text = CheckIfColumnExist(dsSelected, "percent_conversion_cont").ToString().Trim();

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_order_disc"), out temp))
                                    lblAverageOrderDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_order_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageOrderDiscountValue.Text = "0.00 %";

                                lblNumQuotesValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes");
                                lblNumOrdersValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes_Conv");
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv"), out temp))
                                    lblCloseRateValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblCloseRateValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Quote"), out temp))
                                    lblGMQuoteValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Quote")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGMQuoteValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Order"), out temp))
                                    lblGmOrderValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Order")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGmOrderValue.Text = "0.00 %";
                            }
                            else
                            {
                                float temp;
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_quote_disc"), out temp))
                                    lblAverageQuoteDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_quote_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageQuoteDiscountValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "ave_order_disc"), out temp))
                                    lblAverageOrderDiscountValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "ave_order_disc")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblAverageOrderDiscountValue.Text = "0.00 %";

                                lblNumQuotesValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes");
                                lblNumOrdersValue.Text = CheckIfColumnExist(dsSelected, "Hist_Num_Quotes_Conv");
                                if (float.TryParse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv"), out temp))
                                    lblCloseRateValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "Hist_Perc_Conv")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblCloseRateValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Quote"), out temp))
                                    lblGMQuoteValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Quote")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGMQuoteValue.Text = "0.00 %";

                                if (float.TryParse(CheckIfColumnExist(dsSelected, "GM_Order"), out temp))
                                    lblGmOrderValue.Text = (float.Parse(CheckIfColumnExist(dsSelected, "GM_Order")) * 100.0f).ToString("0.00") + " % ";
                                else
                                    lblGmOrderValue.Text = "0.00 %";
                            }

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                            "call me", "Message()", true);
                            lblAverageQuoteDiscountValue.Text = "";
                            lblAverageOrderDiscountValue.Text = "";
                            lblNumQuotesValue.Text = "";
                            lblCloseRateValue.Text = "";
                            lblNumOrdersValue.Text = "";
                            lblResellerValue.Text = "";
                            lblGovernmentValue.Text = "";
                            lblSuccessRateContactValue.Text = "";
                            lblSuccessRateSiteValue.Text = "";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "Message()", true);
                        lblAverageQuoteDiscountValue.Text = "";
                        lblAverageOrderDiscountValue.Text = "";
                        lblNumQuotesValue.Text = "";
                        lblCloseRateValue.Text = "";
                        lblNumOrdersValue.Text = "";
                        lblResellerValue.Text = "";
                        lblGovernmentValue.Text = "";
                        lblSuccessRateContactValue.Text = "";
                        lblSuccessRateSiteValue.Text = "";
                    }
                }

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Searching Quote Discount Guidance", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        #region Function

        private void BindTable(DropDownList ddlQuote, string Name, DataSet Source, string TableName)
        {
            try
            {
                DataSet drQuoteGuidance = Source;
                if (drQuoteGuidance.Tables.Count > 0)
                {
                    drQuoteGuidance.Tables[0].TableName = TableName;
                    if (drQuoteGuidance != null && drQuoteGuidance.Tables.Count > 0)
                    {
                        //Writing XML
                        //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        //drQuoteGuidance.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        //xmlSW.Close();
                        DataRow DRQuoteGuidance = drQuoteGuidance.Tables[0].NewRow();
                        DRQuoteGuidance[0] = "";
                        drQuoteGuidance.Tables[0].Rows.Add(DRQuoteGuidance);
                        ddlQuote.DataSource = drQuoteGuidance;
                        ddlQuote.DataTextField = Name;
                        ddlQuote.DataValueField = Name;
                        ddlQuote.DataBind();
                        ddlQuote.SelectedValue = "";
                    }
                    else
                    {
                        //if (File.Exists(Pathname))
                        //    File.Delete(Pathname);
                        ddlQuote.DataSource = null;
                        ddlQuote.DataBind();
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Function named BindTable", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadCustomerType()
        {
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                //    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuoteDGCustomerType" + ".xml";
                cQuoteGuidance objQuoteGuidance = new cQuoteGuidance();
                //if (File.Exists(Pathname))
                //{
                //    DataTable dr = GetDataXML(Pathname);
                //    if (dr.TableName == ddlTemp.SelectedValue && 
                //        File.GetCreationTime(Pathname).ToShortDateString() == DateTime.Now.ToShortDateString())
                //    {
                //        DataRow DRQuoteGuidance = dr.NewRow();
                //        DRQuoteGuidance[0] = "";
                //        dr.Rows.Add(DRQuoteGuidance);
                //        ddlCustType.DataSource = dr;
                //        ddlCustType.DataTextField = "CustomerType";
                //        ddlCustType.DataValueField = "CustomerType";
                //        ddlCustType.DataBind();
                //        ddlCustType.SelectedValue = "";
                //    }
                //    else
                //    {
                //        objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //        BindTable(ddlCustType, "CustomerType", Pathname, objQuoteGuidance.GetCustomerType(),ddlTemp.SelectedValue);
                //    }
                //}
                //else
                //{
                //   objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //   BindTable(ddlCustType, "CustomerType", Pathname, objQuoteGuidance.GetCustomerType(), ddlTemp.SelectedValue);
                //}
                objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                BindTable(ddlCustType, "CustomerType", objQuoteGuidance.GetCustomerType(), ddlTemp.SelectedValue);

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Binding Customer Type", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadProductLine()
        {
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                //    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuoteDGProductLine" + ".xml";
                cQuoteGuidance objQuoteGuidance = new cQuoteGuidance();

                //if (File.Exists(Pathname))
                //{
                //    DataTable dr = GetDataXML(Pathname);
                //    if (dr.TableName == ddlTemp.SelectedValue &&
                //    File.GetCreationTime(Pathname).ToShortDateString() == DateTime.Now.ToShortDateString())
                //    {
                //        DataRow DRQuoteGuidance = dr.NewRow();
                //        DRQuoteGuidance[0] = "";
                //        dr.Rows.Add(DRQuoteGuidance);
                //        ddlProdLine.DataSource = dr;
                //        ddlProdLine.DataTextField = "ProductLine";//drCampaign.Tables[0].Rows[0]["campaignName"].ToString();
                //        ddlProdLine.DataValueField = "ProductLine";//drCampaign.Tables[0].Rows[0]["campaignValue"].ToString();
                //        ddlProdLine.DataBind();
                //        ddlProdLine.SelectedValue = "";
                //    }
                //    else
                //    {
                //        objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //        BindTable(ddlProdLine, "ProductLine", Pathname, objQuoteGuidance.GetProductLine(), ddlTemp.SelectedValue);
                //    }
                //}
                //else
                //{
                //    objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //    BindTable(ddlProdLine, "ProductLine", Pathname, objQuoteGuidance.GetProductLine(), ddlTemp.SelectedValue);
                //}

                objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                BindTable(ddlProdLine, "ProductLine", objQuoteGuidance.GetProductLine(), ddlTemp.SelectedValue);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Binding Product Line", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadQuoteBucket()
        {
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                //    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuoteDGQuoteBucket" + ".xml";
                cQuoteGuidance objQuoteGuidance = new cQuoteGuidance();

                //if (File.Exists(Pathname))
                //{
                //     DataTable dr = GetDataXML(Pathname);
                //     if (dr.TableName == ddlTemp.SelectedValue &&
                //        File.GetCreationTime(Pathname).ToShortDateString() == DateTime.Now.ToShortDateString())
                //     {
                //         DataRow DRQuoteGuidance = dr.NewRow();
                //         DRQuoteGuidance[0] = "";
                //         dr.Rows.Add(DRQuoteGuidance);
                //         ddlQuoteBucket.DataSource = dr;
                //         ddlQuoteBucket.DataTextField = "QuoteBucket";//drCampaign.Tables[0].Rows[0]["campaignName"].ToString();
                //         ddlQuoteBucket.DataValueField = "QuoteBucket";//drCampaign.Tables[0].Rows[0]["campaignValue"].ToString();
                //         ddlQuoteBucket.DataBind();
                //         ddlQuoteBucket.SelectedValue = "";
                //     }
                //     else
                //     {
                //         objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //         BindTable(ddlQuoteBucket, "QuoteBucket", Pathname, objQuoteGuidance.GetQuoteBucket(), ddlTemp.SelectedValue);
                //     }
                //}
                //else
                //{
                //    objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //    BindTable(ddlQuoteBucket, "QuoteBucket", Pathname, objQuoteGuidance.GetQuoteBucket(), ddlTemp.SelectedValue);
                //}

                objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                BindTable(ddlQuoteBucket, "QuoteBucket", objQuoteGuidance.GetQuoteBucket(), ddlTemp.SelectedValue);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Binding Quote Bucket", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadQuoteType()
        {
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                //"XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuoteDGQuoteType" + ".xml";
                cQuoteGuidance objQuoteGuidance = new cQuoteGuidance();

                //if (File.Exists(Pathname))
                //{
                //    DataTable dr = GetDataXML(Pathname);
                //    if (dr.TableName == ddlTemp.SelectedValue &&
                //    File.GetCreationTime(Pathname).ToShortDateString() == DateTime.Now.ToShortDateString())
                //    {
                //        DataRow DRQuoteGuidance = dr.NewRow();
                //        DRQuoteGuidance[0] = "";
                //        dr.Rows.Add(DRQuoteGuidance);
                //        ddlQuoteType.DataSource = dr;
                //        ddlQuoteType.DataTextField = "QuoteType";//drCampaign.Tables[0].Rows[0]["campaignName"].ToString();
                //        ddlQuoteType.DataValueField = "QuoteType";//drCampaign.Tables[0].Rows[0]["campaignValue"].ToString();
                //        ddlQuoteType.DataBind();
                //        ddlQuoteType.SelectedValue = "";
                //    }
                //    else
                //    {
                //        objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //        BindTable(ddlQuoteType, "QuoteType", Pathname, objQuoteGuidance.GetQuoteType(), ddlTemp.SelectedValue);
                //    }
                //}
                //else
                //{
                //    objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                //    BindTable(ddlQuoteType, "QuoteType", Pathname, objQuoteGuidance.GetQuoteType(), ddlTemp.SelectedValue);
                //}

                objQuoteGuidance.Campaign = ddlTemp.SelectedValue;
                BindTable(ddlQuoteType, "QuoteType", objQuoteGuidance.GetQuoteType(), ddlTemp.SelectedValue);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Binding Quote Type", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private string CheckIfColumnExist(DataSet ds, string columnName)
        {
            try
            {
                if (ds.Tables[0].Columns.Contains(columnName))
                {
                    return ds.Tables[0].Rows[0][columnName].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Function named CheckIfColumnExist", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return "";
            }
        }

        private void StatusControl(string Searchby)
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            if (Searchby == "CustomerNo")
            {
                if (ddlTemp.SelectedValue != "EMED" &&
                     ddlTemp.SelectedValue != "CA" &&
                     ddlTemp.SelectedValue != "US" &&
                     ddlTemp.SelectedValue != "CL")
                {
                    lblInstruction.Text = "Enter Customer Number and Material Entered and Select 4 Categories.";
                    txtMaterialEntrd.BackColor = System.Drawing.Color.White;
                    txtMaterialEntrd.Enabled = true;
                    lblMaterialEntrdReq.Visible = true;
                }
                else
                {
                    lblInstruction.Text = "Enter Customer Number and Select 4 Categories.";
                }

                txtCustomerNumber.ReadOnly = false;
                txtCustomerNumber.BackColor = System.Drawing.Color.White;
                lblCustomerNumberReq.Visible = true;
                ddlProdLine.Enabled = true;
                ddlProdLine.BackColor = System.Drawing.Color.White;
                lblProdLineReq.Visible = true;
                ddlCustType.Enabled = true;
                ddlCustType.BackColor = System.Drawing.Color.White;
                lblCustTypeReq.Visible = true;
                ddlQuoteBucket.Enabled = true;
                ddlQuoteBucket.BackColor = System.Drawing.Color.White;
                lblQuoteBucketReq.Visible = true;
                ddlQuoteType.Enabled = true;
                ddlQuoteType.BackColor = System.Drawing.Color.White;
                lblQuoteTypeReq.Visible = true;
                txtQuoteNumber.ReadOnly = true;
                txtQuoteNumber.BackColor = System.Drawing.Color.LightGray;
                lblQuoteNumberReq.Visible = false;
                txtContactNumber.ReadOnly = true;
                txtContactNumber.BackColor = System.Drawing.Color.LightGray;
                lblContactNumberReq.Visible = false;
                lblCustomerNameValue.Visible = true;
                lblContactNameValue.Visible = true;
                ResultControl(true, "C", ddlTemp.SelectedValue);
                ClearResultControl();
            }
            else if (Searchby == "QuoteNo")
            {
                if (ddlTemp.SelectedValue != "EMED" &&
                     ddlTemp.SelectedValue != "CA" &&
                     ddlTemp.SelectedValue != "US" &&
                     ddlTemp.SelectedValue != "CL")
                {
                    ddlProdLine.Enabled = true;
                    ddlProdLine.BackColor = System.Drawing.Color.White;
                    lblProdLineReq.Visible = true;
                    txtMaterialEntrd.BackColor = System.Drawing.Color.White;
                    txtMaterialEntrd.Enabled = true;
                    lblMaterialEntrdReq.Visible = true;
                    lblInstruction.Text = "Enter Quote Number, Product Line and Material Entered.";
                }
                else
                {
                    ddlProdLine.Enabled = false;
                    ddlProdLine.BackColor = System.Drawing.Color.LightGray;
                    lblProdLineReq.Visible = false;
                    lblInstruction.Text = "Enter a Quote Number.";
                }

                txtQuoteNumber.ReadOnly = false;
                txtQuoteNumber.BackColor = System.Drawing.Color.White;
                lblQuoteNumberReq.Visible = true;
                ddlCustType.Enabled = false;
                ddlCustType.BackColor = System.Drawing.Color.LightGray;
                lblCustTypeReq.Visible = false;
                ddlQuoteBucket.Enabled = false;
                ddlQuoteBucket.BackColor = System.Drawing.Color.LightGray;
                lblQuoteBucketReq.Visible = false;
                ddlQuoteType.Enabled = false;
                ddlQuoteType.BackColor = System.Drawing.Color.LightGray;
                lblQuoteTypeReq.Visible = false;
                txtCustomerNumber.BackColor = System.Drawing.Color.LightGray;
                txtCustomerNumber.ReadOnly = true;
                lblCustomerNumberReq.Visible = false;
                txtContactNumber.BackColor = System.Drawing.Color.LightGray;
                txtContactNumber.ReadOnly = true;
                lblContactNumberReq.Visible = false;
                lblCustomerNameValue.Visible = true;
                lblContactNameValue.Visible = true;
                ResultControl(true, "", ddlTemp.SelectedValue);
                ClearResultControl();
            }
            else if (Searchby == "ContactNo")
            {
                if (ddlTemp.SelectedValue != "EMED" &&
                   ddlTemp.SelectedValue != "CA" &&
                   ddlTemp.SelectedValue != "US" &&
                   ddlTemp.SelectedValue != "CL")
                {
                    lblInstruction.Text = "Enter Contact Number and Material Entered and Select 4 Categories.";
                }
                else
                    lblInstruction.Text = "Enter Contact Number and Select 4 Categories.";

                txtContactNumber.ReadOnly = false;
                txtContactNumber.BackColor = System.Drawing.Color.White;
                lblContactNumberReq.Visible = true;
                txtCustomerNumber.ReadOnly = true;
                txtCustomerNumber.BackColor = System.Drawing.Color.LightGray;
                lblCustomerNumberReq.Visible = false;
                ddlCustType.Enabled = true;
                ddlCustType.BackColor = System.Drawing.Color.White;
                lblCustTypeReq.Visible = true;
                ddlProdLine.Enabled = true;
                ddlProdLine.BackColor = System.Drawing.Color.White;
                lblProdLineReq.Visible = true;
                txtMaterialEntrd.BackColor = System.Drawing.Color.White;
                txtMaterialEntrd.Enabled = true;
                lblMaterialEntrdReq.Visible = true;
                ddlQuoteBucket.Enabled = true;
                ddlQuoteBucket.BackColor = System.Drawing.Color.White;
                lblQuoteBucketReq.Visible = true;
                ddlQuoteType.Enabled = true;
                ddlQuoteType.BackColor = System.Drawing.Color.White;
                lblQuoteTypeReq.Visible = true;
                txtQuoteNumber.ReadOnly = true;
                txtQuoteNumber.BackColor = System.Drawing.Color.LightGray;
                lblQuoteNumberReq.Visible = false;
                lblCustomerNameValue.Visible = true;
                lblContactNameValue.Visible = true;
                txtCustomerNumber.ReadOnly = true;
                txtCustomerNumber.BackColor = System.Drawing.Color.LightGray;
                lblCustomerNumberReq.Visible = false;
                ResultControl(true, "", ddlTemp.SelectedValue);
                ClearResultControl();
            }
            else
            {
                if (ddlTemp.SelectedValue != "EMED" &&
                  ddlTemp.SelectedValue != "CA" &&
                  ddlTemp.SelectedValue != "US" &&
                  ddlTemp.SelectedValue != "CL")
                {
                    lblInstruction.Text = "Select 4 Categories and Enter a Material Entered.";
                }
                else
                    lblInstruction.Text = "Select 4 Categories.";

                txtQuoteNumber.ReadOnly = true;
                txtQuoteNumber.BackColor = System.Drawing.Color.LightGray;
                lblQuoteNumberReq.Visible = false;
                txtContactNumber.ReadOnly = true;
                txtContactNumber.BackColor = System.Drawing.Color.LightGray;
                lblContactNumberReq.Visible = false;
                txtCustomerNumber.ReadOnly = true;
                txtCustomerNumber.BackColor = System.Drawing.Color.LightGray;
                lblCustomerNumberReq.Visible = false;
                ddlCustType.Enabled = true;
                ddlCustType.BackColor = System.Drawing.Color.White;
                lblCustTypeReq.Visible = true;
                ddlProdLine.Enabled = true;
                ddlProdLine.BackColor = System.Drawing.Color.White;
                lblProdLineReq.Visible = true;
                txtMaterialEntrd.BackColor = System.Drawing.Color.White;
                txtMaterialEntrd.Enabled = true;
                lblMaterialEntrdReq.Visible = true;
                ddlQuoteBucket.Enabled = true;
                ddlQuoteBucket.BackColor = System.Drawing.Color.White;
                lblQuoteBucketReq.Visible = true;
                ddlQuoteType.Enabled = true;
                ddlQuoteType.BackColor = System.Drawing.Color.White;
                lblQuoteTypeReq.Visible = true;
                lblCustomerNameValue.Visible = false;
                lblContactNameValue.Visible = false;
                ResultControl(false, "", ddlTemp.SelectedValue);
                ClearResultControl();
            }
        }

        private void ResultControl(bool select, string contact = "", string Campaign = "")
        {
            if (select)
            {

                if (contact == "")
                {
                    tdReseller.Style.Remove("display");
                    tdGovernment.Style.Remove("display");
                    tdSuccessRateContact.Style.Remove("display");
                    tdSuccessRateSite.Style.Remove("display");
                }
                else
                {
                    tdReseller.Style.Remove("display");
                    tdGovernment.Style.Remove("display");
                    tdSuccessRateContact.Style.Add("display", "none");
                    tdSuccessRateSite.Style.Remove("display");
                }

                //IF Europe remove Reseller and Government
                if (Campaign == "UK" || Campaign == "SUK" ||
                    Campaign == "DE" || Campaign == "AT" ||
                    Campaign == "FR" || Campaign == "NL" ||
                    Campaign == "BE" || Campaign == "CH")
                {
                    tdReseller.Style.Add("display", "none");
                    tdGovernment.Style.Add("display", "none");
                    tdMaterial.Style.Remove("display");
                }
                else
                {
                    tdReseller.Style.Remove("display");
                    tdGovernment.Style.Remove("display");
                    tdMaterial.Style.Add("display", "none");
                }
            }
            else
            {
                tdReseller.Style.Add("display", "none");
                tdGovernment.Style.Add("display", "none");
                tdSuccessRateContact.Style.Add("display", "none");
                tdSuccessRateSite.Style.Add("display", "none");

                //IF Unit not a europe remove Material
                if (Campaign == "EMED" || Campaign == "US" ||
                    Campaign == "CA" || Campaign == "CL" ||
                    Campaign == "PC")
                    tdMaterial.Style.Add("display", "none");
                else
                    tdMaterial.Style.Remove("display");
            }


        }

        private void ClearResultControl()
        {
            lblAverageOrderDiscountValue.Text = "";
            lblAverageQuoteDiscountValue.Text = "";
            lblCloseRateValue.Text = "";
            lblGmOrderValue.Text = "";
            lblGMQuoteValue.Text = "";
            lblNumOrdersValue.Text = "";
            lblNumQuotesValue.Text = "";
            lblResellerValue.Text = "";
            lblGovernmentValue.Text = "";
            lblSuccessRateContactValue.Text = "";
            lblSuccessRateSiteValue.Text = "";
            if (rdoSearchBy4Categories.Checked == true)
            {
                txtContactNumber.Text = "";
                lblContactNameValue.Text = "";
                txtCustomerNumber.Text = "";
                lblCustomerNameValue.Text = "";
                txtQuoteNumber.Text = "";
            }
            else if (rdoSearchByContactNumber.Checked == true)
            {
                lblContactNameValue.Text = "";
                txtCustomerNumber.Text = "";
                lblCustomerNameValue.Text = "";
                txtQuoteNumber.Text = "";
            }
            else if (rdoSearchByCustomerNo.Checked == true)
            {
                lblCustomerNameValue.Text = "";
                txtContactNumber.Text = "";
                lblContactNameValue.Text = "";
                txtQuoteNumber.Text = "";
            }
            else if (rdoSearchByQuoteNo.Checked == true)
            {
                txtContactNumber.Text = "";
                lblContactNameValue.Text = "";
                txtCustomerNumber.Text = "";
                lblCustomerNameValue.Text = "";
                ddlCustType.SelectedValue = "";
                //ddlProdLine.SelectedValue = "";
                ddlQuoteBucket.SelectedValue = "";
                ddlQuoteType.SelectedValue = "";
            }
        }

        private void ClearFilterControl()
        {
            txtContactNumber.Text = "";
            txtCustomerNumber.Text = "";
            txtQuoteNumber.Text = "";
            ddlCustType.Text = "";
            ddlProdLine.Text = "";
            ddlQuoteBucket.Text = "";
            ddlQuoteType.Text = "";
            lblCustomerNameValue.Text = "";
            lblContactNameValue.Text = "";
            txtMaterialEntrd.Text = "";
        }

        private void DisableAllFilterControl()
        {
            txtCustomerNumber.ReadOnly = true;
            txtCustomerNumber.BackColor = System.Drawing.Color.LightGray;
            lblCustomerNumberReq.Visible = false;
            txtContactNumber.ReadOnly = true;
            txtContactNumber.BackColor = System.Drawing.Color.LightGray;
            lblContactNumberReq.Visible = false;
            txtQuoteNumber.ReadOnly = true;
            txtQuoteNumber.BackColor = System.Drawing.Color.LightGray;
            lblQuoteNumberReq.Visible = false;

            ddlCustType.Enabled = false;
            ddlCustType.BackColor = System.Drawing.Color.LightGray;
            lblCustTypeReq.Visible = false;
            ddlProdLine.Enabled = false;
            ddlProdLine.BackColor = System.Drawing.Color.LightGray;
            lblProdLineReq.Visible = false;
            ddlQuoteBucket.Enabled = false;
            ddlQuoteBucket.BackColor = System.Drawing.Color.LightGray;
            lblQuoteBucketReq.Visible = false;
            ddlQuoteType.Enabled = false;
            ddlQuoteType.BackColor = System.Drawing.Color.LightGray;
            lblQuoteTypeReq.Visible = false;
            txtMaterialEntrd.Enabled = false;
            txtMaterialEntrd.BackColor = System.Drawing.Color.LightGray;
            lblMaterialEntrdReq.Visible = false;

            lblCustomerNameValue.Visible = false;
            lblContactNameValue.Visible = false;
        }

        #endregion

        protected void rdoSearchByQuoteNo_CheckedChanged(object sender, EventArgs e)
        {
            ClearFilterControl();
            StatusControl("QuoteNo");
        }

        protected void rdoSearchByCustomerNo_CheckedChanged(object sender, EventArgs e)
        {
            ClearFilterControl();
            StatusControl("CustomerNo");
        }

        protected void rdoSearchBy4Categories_CheckedChanged(object sender, EventArgs e)
        {
            ClearFilterControl();
            StatusControl("4Categories");
        }

        protected void rdoSearchByContactNo_CheckedChanged(object sender, EventArgs e)
        {
            ClearFilterControl();
            StatusControl("ContactNo");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            DisableAllFilterControl();
            ClearFilterControl();
            ClearResultControl();
            rdoSearchBy4Categories.Checked = false;
            rdoSearchByCustomerNo.Checked = false;
            rdoSearchByQuoteNo.Checked = false;
            lblInstruction.Text = "";
        }
    }
}