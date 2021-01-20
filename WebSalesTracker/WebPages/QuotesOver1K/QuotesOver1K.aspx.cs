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
using System.Xml;
using System.Text;

namespace WebSalesMine.WebPages.QuotesOver1K
{
    public partial class QuotesOver1K : System.Web.UI.Page
    {

        private int Name = 1000;
        private int WeightedIndex = 1000;
        private int ProbabliltyIndex = 1000;
        private int QuoteCostIndex = 1000;
        private int GMPercIndx = 1000;
        private int QuoteValueIndex = 1000;
        private int QuoteDayIndex = 1000;

        public string userRule = "";

        public class AddTemplateToGridView : ITemplate
        {
            ListItemType _type;
            string _colName;

            public AddTemplateToGridView(ListItemType type, string colname)
            {
                _type = type;

                _colName = colname;

            }
            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {

                switch (_type)
                {
                    case ListItemType.Item:
                        LinkButton ht = new LinkButton();
                        ht.ID = "lnkQuoteDoc";
                        ht.ClientIDMode = ClientIDMode.Static;
                        ht.PostBackUrl = "~/WebPages/Quotes/Quotes.aspx";
                        container.Controls.Add(ht);
                        break;
                }

            }
        }

        protected void grdQuoteOver1K_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                int col_indx = 0;
                //int Weighted_indx = 0;
                //int Probablilty_indx = 0;
                //int GM_perc_indx = 0;
                //int QuoteCost = 0;
                //int QuoteValue = 0;
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                DropDownList ddlTempValue = Master.FindControl("ddlCampaignValue") as DropDownList;


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (string.IsNullOrEmpty(Hdflnkcolindex.Value) && string.IsNullOrEmpty(HdfWeightedindex.Value) && string.IsNullOrEmpty(HdfProbabliltyindex.Value) && string.IsNullOrEmpty(HdfGM_perc_indx.Value) && string.IsNullOrEmpty(HdfQuoteCost.Value) && string.IsNullOrEmpty(HdfQuoteValue.Value))
                    {
                        for (int columnIndex = 0; columnIndex < e.Row.Cells.Count; columnIndex++)
                        {
                            LinkButton Link = grdQuoteOver1K.HeaderRow.Cells[columnIndex].Controls[0] as LinkButton;

                            if (Link.Text == "QUOTE DOC")
                            {
                                Hdflnkcolindex.Value = columnIndex.ToString();
                                col_indx = Convert.ToInt32(Hdflnkcolindex.Value);
                            }

                            //if (Link.Text == "")
                            //{
                            //    Hdflnkcolindex.Value = columnIndex.ToString();
                            //    col_indx = Convert.ToInt32(Hdflnkcolindex.Value);
                            //}

                            

                            //if (Link.Text == "WEIGHTED VALUE")
                            //{
                            //    HdfWeightedindex.Value = columnIndex.ToString();
                            //    Weighted_indx = Convert.ToInt32(HdfWeightedindex.Value);
                            //}


                            //if (Link.Text == "CLOSE PROBABILITY")
                            //{
                            //    HdfProbabliltyindex.Value = columnIndex.ToString();
                            //    Probablilty_indx = Convert.ToInt32(HdfProbabliltyindex.Value);
                            //}


                            //if (Link.Text == "QUOTE GM PERCENTAGE")
                            //{
                            //    HdfGM_perc_indx.Value = columnIndex.ToString();
                            //    GM_perc_indx = Convert.ToInt32(HdfGM_perc_indx.Value);
                            //}


                            //if (Link.Text == "QUOTE COST")
                            //{
                            //    HdfQuoteCost.Value = columnIndex.ToString();
                            //    QuoteCost = Convert.ToInt32(HdfQuoteCost.Value);
                            //}


                            //if (Link.Text == "QUOTE VALUE")
                            //{
                            //    HdfQuoteValue.Value = columnIndex.ToString();
                            //    QuoteValue = Convert.ToInt32(HdfQuoteValue.Value);
                            //}

                            //if (Link.Text == "ACCOUNT NAME")
                            //{
                            //    HdfAccountName.Value = columnIndex.ToString();
                            //    AccoutnName = Convert.ToInt32(HdfAccountName.Value);
                            //}

                        }
                    }

                    if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
                    {
                        if (!string.IsNullOrEmpty(ddlTempValue.SelectedValue))
                        {
                            ddlTempValue.ClearSelection();
                        }
                        else
                        {
                            ddlTempValue.Items.Clear();

                            ddlTempValue.Items.AddRange(ddlTemp.Items.OfType<ListItem>().ToArray());
                        }


                        if (ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue) != null)
                        {
                            ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue).Selected = true;
                        }

                    }



                    DataTable dt = new DataTable();
                    LinkButton lnkButton = new LinkButton();

                    if (lnkButton != null && !string.IsNullOrEmpty(Hdflnkcolindex.Value))
                    {
                        lnkButton.ID = "lnkQuoteDoc";
                        col_indx = Convert.ToInt32(Hdflnkcolindex.Value);
                        lnkButton.PostBackUrl = "~/WebPages/Quotes/Quotes.aspx";
                        e.Row.Cells[col_indx].Controls.Add(lnkButton);
                        lnkButton.Text = e.Row.Cells[col_indx].Text;

                        if (e.Row.Cells[col_indx].Text == txtAccNum.Text)
                        {
                            e.Row.CssClass = "SelectedRowStyle";
                            txtAccNum.Text = "";
                        }
                    }
                    Decimal temp;
                    //if (!string.IsNullOrEmpty(HdfWeightedindex.Value))
                    //{
                    //    Weighted_indx = Convert.ToInt32(HdfWeightedindex.Value);
                    if (WeightedIndex != 1000)
                    {
                        if (Decimal.TryParse(e.Row.Cells[WeightedIndex].Text, out temp))
                            e.Row.Cells[WeightedIndex].Text = FormatCurrency(Convert.ToDecimal(e.Row.Cells[WeightedIndex].Text), ddlTempValue.SelectedItem.Text);
                        else
                            e.Row.Cells[WeightedIndex].Text = "";
                        //e.Row.Cells[Weighted_indx].Text = Convert.ToDecimal(e.Row.Cells[Weighted_indx].Text).ToString("c2");
                    }
                    //}

                    //if (!string.IsNullOrEmpty(HdfProbabliltyindex.Value))
                    //{
                    //    Probablilty_indx = Convert.ToInt32(HdfProbabliltyindex.Value);
                    if (ProbabliltyIndex != 1000)
                    {
                        if (Decimal.TryParse(e.Row.Cells[ProbabliltyIndex].Text, out temp))
                            e.Row.Cells[ProbabliltyIndex].Text = (Convert.ToDecimal(e.Row.Cells[ProbabliltyIndex].Text)).ToString("P2");
                        else
                            e.Row.Cells[ProbabliltyIndex].Text = "";
                    }
                    //}

                    //if (!string.IsNullOrEmpty(HdfGM_perc_indx.Value))
                    //{
                    //    GM_perc_indx = Convert.ToInt32(HdfGM_perc_indx.Value);
                    if (GMPercIndx != 1000)
                    {
                        if (Decimal.TryParse(e.Row.Cells[GMPercIndx].Text, out temp))
                            e.Row.Cells[GMPercIndx].Text = (Convert.ToDecimal(e.Row.Cells[GMPercIndx].Text)).ToString("P2");
                        else
                            e.Row.Cells[GMPercIndx].Text = "";

                        //e.Row.Cells[GM_perc_indx].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[GM_perc_indx].Text)).ToString("P2");
                    }
                    //}

                    //if (!string.IsNullOrEmpty(HdfQuoteCost.Value))
                    //{
                    //    QuoteCost = Convert.ToInt32(HdfQuoteCost.Value);
                    if (QuoteCostIndex != 1000)
                    {
                        if (Decimal.TryParse(e.Row.Cells[QuoteCostIndex].Text, out temp))
                            e.Row.Cells[QuoteCostIndex].Text = FormatCurrency(Convert.ToDecimal(e.Row.Cells[QuoteCostIndex].Text), ddlTempValue.SelectedItem.Text);
                        else
                            e.Row.Cells[QuoteCostIndex].Text = "";

                        //e.Row.Cells[QuoteCost].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[QuoteCost].Text)).ToString("c2");
                    }
                    //}

                    //if (!string.IsNullOrEmpty(HdfQuoteValue.Value) && e.Row.Cells[QuoteValue].Text != "&nbsp;")
                    //{
                    //    QuoteValue = Convert.ToInt32(HdfQuoteValue.Value);
                    if (QuoteValueIndex != 1000)
                    {
                        if (Decimal.TryParse(e.Row.Cells[QuoteValueIndex].Text, out temp))
                            e.Row.Cells[QuoteValueIndex].Text = FormatCurrency(Convert.ToDecimal(e.Row.Cells[QuoteValueIndex].Text), ddlTempValue.SelectedItem.Text);
                        else
                            e.Row.Cells[QuoteValueIndex].Text = "";

                        //    e.Row.Cells[QuoteValue].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[QuoteValue].Text)).ToString("c2");
                    }
                    //}

                    if (QuoteDayIndex != 1000)
                    {
                        DateTime tempdate;
                        if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["QUOTE DAY"].ToString(), out tempdate) == true)
                            e.Row.Cells[QuoteDayIndex].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["QUOTE DAY"]).ToString("MM/dd/yyyy");
                    }

                    if (Name != 1000)
                    {
                        byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["ACCOUNT NAME"].ToString());
                        string output = Encoding.UTF8.GetString(data);

                        e.Row.Cells[Name].Text = output;
                    }


                    if (this.grdQuoteOver1K.Rows.Count > 0)
                    {
                        grdQuoteOver1K.UseAccessibleHeader = true;
                        grdQuoteOver1K.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }


                }

            }
            catch (Exception ex)
            {
            }
        }

        //protected void grdQuoteOver1K_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    try
        //    {
        //        int col_indx = 0;
        //        int Weighted_indx = 0;
        //        int Probablilty_indx = 0;
        //        int GM_perc_indx = 0;
        //        int QuoteCost = 0;
        //        int QuoteValue = 0;

        //            if (e.Row.RowType == DataControlRowType.DataRow)
        //            {

        //                if (string.IsNullOrEmpty(Hdflnkcolindex.Value) && string.IsNullOrEmpty(HdfWeightedindex.Value) && string.IsNullOrEmpty(HdfProbabliltyindex.Value) && string.IsNullOrEmpty(HdfGM_perc_indx.Value) && string.IsNullOrEmpty(HdfQuoteCost.Value) && string.IsNullOrEmpty(HdfQuoteValue.Value))
        //                {
        //                    for (int columnIndex = 0; columnIndex < e.Row.Cells.Count; columnIndex++)
        //                    {
        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "QUOTE DOC")
        //                        {
        //                            Hdflnkcolindex.Value = columnIndex.ToString();
        //                            col_indx = Convert.ToInt32(Hdflnkcolindex.Value);
        //                        }


        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "WEIGHTED VALUE")
        //                        {
        //                            HdfWeightedindex.Value = columnIndex.ToString();
        //                            Weighted_indx = Convert.ToInt32(HdfWeightedindex.Value);
        //                        }


        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "CLOSE PROBABILITY")
        //                        {
        //                            HdfProbabliltyindex.Value = columnIndex.ToString();
        //                            Probablilty_indx = Convert.ToInt32(HdfProbabliltyindex.Value);
        //                        }


        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "QUOTE GM PERCENTAGE")
        //                        {
        //                            HdfGM_perc_indx.Value = columnIndex.ToString();
        //                            GM_perc_indx = Convert.ToInt32(HdfGM_perc_indx.Value);
        //                        }


        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "QUOTE COST")
        //                        {
        //                            HdfQuoteCost.Value = columnIndex.ToString();
        //                            QuoteCost = Convert.ToInt32(HdfQuoteCost.Value);
        //                        }


        //                        if (grdQuoteOver1K.HeaderRow.Cells[columnIndex].Text == "QUOTE VALUE")
        //                        {
        //                            HdfQuoteValue.Value = columnIndex.ToString();
        //                            QuoteValue = Convert.ToInt32(HdfQuoteValue.Value);
        //                        }



        //                    }
        //                }




        //                DataTable dt = new DataTable();
        //                LinkButton lnkButton = new LinkButton();

        //                if (lnkButton != null && !string.IsNullOrEmpty(Hdflnkcolindex.Value))
        //                {
        //                    lnkButton.ID = "lnkQuoteDoc";
        //                    col_indx = Convert.ToInt32(Hdflnkcolindex.Value);
        //                    lnkButton.PostBackUrl = "~/WebPages/Quotes/Quotes.aspx";
        //                    e.Row.Cells[col_indx].Controls.Add(lnkButton);
        //                    lnkButton.Text = e.Row.Cells[col_indx].Text;

        //                    if (e.Row.Cells[col_indx].Text == txtAccNum.Text)
        //                    {
        //                        e.Row.CssClass = "SelectedRowStyle";
        //                        txtAccNum.Text = "";
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(HdfWeightedindex.Value))
        //                {
        //                    Weighted_indx = Convert.ToInt32(HdfWeightedindex.Value);
        //                    if (e.Row.Cells[Weighted_indx].Text != "" && e.Row.Cells[Weighted_indx].Text != "&nbsp;")
        //                    {
        //                        e.Row.Cells[Weighted_indx].Text = Convert.ToDecimal(e.Row.Cells[Weighted_indx].Text).ToString("c2");
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(HdfProbabliltyindex.Value))
        //                {
        //                    Probablilty_indx = Convert.ToInt32(HdfProbabliltyindex.Value);
        //                    if (e.Row.Cells[Probablilty_indx].Text != "" && e.Row.Cells[Probablilty_indx].Text != "&nbsp;")
        //                    {
        //                        e.Row.Cells[Probablilty_indx].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[Probablilty_indx].Text)).ToString("P2");

        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(HdfGM_perc_indx.Value))
        //                {
        //                    GM_perc_indx = Convert.ToInt32(HdfGM_perc_indx.Value);
        //                    if (e.Row.Cells[GM_perc_indx].Text != "&nbsp;" && e.Row.Cells[GM_perc_indx].Text != "")
        //                    {
        //                        e.Row.Cells[GM_perc_indx].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[GM_perc_indx].Text)).ToString("P2");
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(HdfQuoteCost.Value))
        //                {
        //                    QuoteCost = Convert.ToInt32(HdfQuoteCost.Value);
        //                    if (e.Row.Cells[QuoteCost].Text != "" && e.Row.Cells[QuoteCost].Text != "&nbsp;")
        //                    {
        //                        e.Row.Cells[QuoteCost].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[QuoteCost].Text)).ToString("c2");
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(HdfQuoteValue.Value) && e.Row.Cells[QuoteValue].Text != "&nbsp;")
        //                {
        //                    QuoteValue = Convert.ToInt32(HdfQuoteValue.Value);
        //                    if (e.Row.Cells[QuoteValue].Text != "")
        //                    {
        //                        e.Row.Cells[QuoteValue].Text = Convert.ToDecimal(Convert.ToDouble(e.Row.Cells[QuoteValue].Text)).ToString("c2");
        //                    }
        //                }



        //                if (this.grdQuoteOver1K.Rows.Count > 0)
        //                {
        //                    grdQuoteOver1K.UseAccessibleHeader = true;
        //                    grdQuoteOver1K.HeaderRow.TableSection = TableRowSection.TableHeader;
        //                }


        //            }

        //    }
        //    catch(Exception ex)
        //    {
        //        }
        //}

        private void SetOrdinal(DataSet dsTemp)
        {

            try
            {
                if (dsTemp.Tables[0].Columns.Contains("ACCOUNT NAME"))
                {
                    Name = dsTemp.Tables[0].Columns["ACCOUNT NAME"].Ordinal;
                }

                if (dsTemp.Tables[0].Columns.Contains("WEIGHTED VALUE"))
                {
                    WeightedIndex = dsTemp.Tables[0].Columns["WEIGHTED VALUE"].Ordinal;
                }

                if (dsTemp.Tables[0].Columns.Contains("CLOSE PROBABILITY"))
                {
                    ProbabliltyIndex = dsTemp.Tables[0].Columns["CLOSE PROBABILITY"].Ordinal;
                }

                if (dsTemp.Tables[0].Columns.Contains("QUOTE COST"))
                {
                    QuoteCostIndex = dsTemp.Tables[0].Columns["QUOTE COST"].Ordinal;
                }

                if (dsTemp.Tables[0].Columns.Contains("QUOTE GM PERCENTAGE"))
                {
                    GMPercIndx = dsTemp.Tables[0].Columns["QUOTE GM PERCENTAGE"].Ordinal;
                }

                if (dsTemp.Tables[0].Columns.Contains("QUOTE VALUE"))
                {
                    QuoteValueIndex = dsTemp.Tables[0].Columns["QUOTE VALUE"].Ordinal;
                }

                 if (dsTemp.Tables[0].Columns.Contains("QUOTE DAY"))
                {
                    QuoteDayIndex = dsTemp.Tables[0].Columns["QUOTE DAY"].Ordinal;
                }
                


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "SetOrdinal - Quote Pipeline", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Name;
            LDAPAccess LDAPAccess = new LDAPAccess();
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DropDownList ddlTempValue = Master.FindControl("ddlCampaignValue") as DropDownList;
            Label lblTemp = Master.FindControl("lblAccountName") as Label;
            userRule = ddlTemp.SelectedValue;

            //Hide Account Name
            lblTemp.Visible = false;

            //Hide Export to Excel
            lnkExportToExcel.Visible = false;

            //Get Currency Format
            if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
            {
                if (!string.IsNullOrEmpty(ddlTempValue.SelectedValue))
                {
                    ddlTempValue.ClearSelection();
                }
                else
                {
                    ddlTempValue.Items.Clear();

                    ddlTempValue.Items.AddRange(ddlTemp.Items.OfType<ListItem>().ToArray());
                }


                if (ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue) != null)
                {
                    ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue).Selected = true;
                }

                CurrencyCode.Value = ddlTempValue.SelectedItem.Text;

            }

            Name = LDAPAccess.FindName(SessionFacade.LoggedInUserName);
            if (ddlTemp.SelectedValue == "CA" && lblFilterByQuotes1K.Text != "All")
                lblFilterByQuotes1K.Text = Name;

            if (!IsPostBack)
            {
                HttpCookie CNo = new HttpCookie("QuotePipiline");
                CNo.Expires = DateTime.Now;
                Response.Cookies.Add(CNo);
                SessionFacade.Quote_Pipeline = "";


                if (SessionFacade.Quote_Pipeline == "WonorLoss")
                {

                    LoadQuoteOver1KWonorLoss();
                }
                else
                {
                    ShowQuoteOver1K();

                }


            }
            else
            {
                string ControlId = string.Empty;
                ControlId = getPostBackControlID();
                if (ControlId == "ddlCampaign")
                {
                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                    {

                        LoadQuoteOver1KWonorLoss();
                    }
                    else
                    {

                        ShowQuoteOver1K();

                    }
                }

            }

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

        public string FormatCurrency(decimal amount, string currencyCode)
        {
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           let r = new RegionInfo(c.LCID)
                           where r != null
                           && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                           select c).FirstOrDefault();

            if (culture == null)
                return amount.ToString("0.00");

            return string.Format(culture, "{0:C}", amount);
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {

            string Column;
            grdQuoteOver1K.AutoGenerateColumns = true;

            if (SessionFacade.Quote_Pipeline == "WonorLoss")
            {
                LoadQuoteOver1KWonorLoss();

            }
            else
                ShowQuoteOver1K();


        }
        //BtnSav_Click is not used so code is not changed for improvement 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();


                objQuoteOver1K.Campaign = ddlTemp.SelectedValue;
                objQuoteOver1K.AccountNum = AccountNum.Value;
                objQuoteOver1K.Competition = txtCompetition.Text;
                objQuoteOver1K.Createdby = SessionFacade.LoggedInUserName;
                objQuoteOver1K.Notes = txtNotes.Text;
                objQuoteOver1K.Probablilty = ddlProbablilty.SelectedValue;
                objQuoteOver1K.ProposedDate = txtProposedClose.Text;
                objQuoteOver1K.Quote_Doc = QuoteDoc.Value;
                objQuoteOver1K.Weighted = Weighted.Value;
                objQuoteOver1K.WinorLoss = ddlProbablilty.SelectedItem.Text;
                objQuoteOver1K.Status = Status.Value;
                objQuoteOver1K.AccountName = AccountName.Value;
                objQuoteOver1K.QuoteDay = QuoteDay.Value;
                objQuoteOver1K.QSTCurrent = SalesTeamCurrent.Value;
                objQuoteOver1K.QSTIn = SalesTeamIn.Value;
                objQuoteOver1K.QuoteValue = QuoteValue.Value.Replace("$", "");
                objQuoteOver1K.QuoteCost = QuoteCost.Value.Replace("$", "").Replace(",", "");
                objQuoteOver1K.ScheDate = txtScheFollow.Text;
                objQuoteOver1K.Mining = ddlMining.SelectedItem.Text;
                objQuoteOver1K.Construction = ddlConstruction.SelectedItem.Text;
                objQuoteOver1K.ProductLine = txtProductLine.Text;
                objQuoteOver1K.QuoteGMPerc = QuoteGMPerc.Value.Replace(" %", "");



                if (SessionFacade.Quote_Pipeline == "WonorLoss")
                {
                    if (objQuoteOver1K.AddQuoteComputingWinOrLoss())
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Pathname);
                        IEnumerator ie = doc.SelectNodes("/appSettings/add").GetEnumerator();



                        XmlNodeList nodeList = doc.SelectNodes("/CategoryList/Row@ID='01']");

                        // update MainCategory 
                        nodeList[0].ChildNodes[6].InnerText = txtEditQuoteValue.Text;
                        // update Description 
                        nodeList[0].ChildNodes[4].InnerText = txtEditQuoteAssignment.Text;

                        // Don't forget to save the file

                        doc.Save(Pathname);


                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                            "call me", "SaveSuccess()", true);
                    }
                }
                else
                {
                    if (objQuoteOver1K.AddQuoteComputing())
                    {


                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Saving Quote Over 1K", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void btnShowWonOrLoss_Click(object sender, EventArgs e)
        {
            try
            {
                SessionFacade.Quote_Pipeline = "WonorLoss";
                DataSet ds = new DataSet();

                grdQuoteOver1K.Columns.Clear();
                grdQuoteOver1K.AutoGenerateColumns = false;
                grdQuoteOver1K.DataSource = null;
                grdQuoteOver1K.DataBind();

                HttpCookie CNo = new HttpCookie("QuotePipiline");
                CNo.Value = "WonorLoss";
                CNo.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(CNo);

                LoadQuoteOver1KWonorLoss();

                lblTitleQuotes1K.Text = "Won or Loss";
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Showing Quote Pipeline Won or Loss Only", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void btnShowOpenQuotes_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                grdQuoteOver1K.Columns.Clear();

                HttpCookie CNo = new HttpCookie("QuotePipiline");
                CNo.Value = "OpenQuotes";
                CNo.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(CNo);

                SessionFacade.Quote_Pipeline = "OpenQuotes";

                lblTitleQuotes1K.Text = "Open Quotes";

                //ds.Tables.Add(GetDatafromXMLAll());
                ds = new DataSet();
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                ds = objQuoteOver1K.GetQuoteOver1K_New();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SetOrdinal(ds);

                    grdQuoteOver1K.AutoGenerateColumns = true;
                    grdQuoteOver1K.DataSource = ds.Tables[0];
                    grdQuoteOver1K.DataBind();
                }
                else
                {
                    grdQuoteOver1K.DataSource = null;
                    grdQuoteOver1K.DataBind();
                }

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Showing Quote Pipeline Won or Loss Only", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void grdQuoteOver1K_PageChanging(object sender, GridViewPageEventArgs e)
        {
            grdQuoteOver1K.DataSource = null;
            grdQuoteOver1K.DataBind();
            DataSet ds = new DataSet();
            cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
            if (SessionFacade.Quote_Pipeline == "WonorLoss")
            {
                ds = objQuoteOver1K.GetQuoteOver1KWonorLoss_New();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdQuoteOver1K.DataSource = ds.Tables[0];
                    grdQuoteOver1K.PageIndex = e.NewPageIndex;
                }
                else
                {
                    grdQuoteOver1K.DataSource = null;
                }


            }
            else
            {



                ds = objQuoteOver1K.GetQuoteOver1K_New();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    grdQuoteOver1K.DataSource = ds.Tables[0];
                    grdQuoteOver1K.PageIndex = e.NewPageIndex;

                }
                else
                {
                    grdQuoteOver1K.DataSource = null;
                }



                grdQuoteOver1K.DataBind();

            }


        }

        protected void btnSaveNewQuote_Click(object sender, EventArgs e)
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataSet dsNewQuote = new DataSet();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objNewQuote = new cQuoteOver1K();
                DataView dv = new DataView();
                int temp;

                objNewQuote.AccountName = txtAddAccountName.Text;
                if (int.TryParse(txtAddAccountNumber.Text, out temp))
                    objNewQuote.AccountNum = string.Format("{0:0000000000}", int.Parse(txtAddAccountNumber.Text));
                else
                    objNewQuote.AccountNum = "0000000000";
                objNewQuote.QuoteDay = txtAddQuoteDay.Text;
                objNewQuote.Quote_Doc = txtAddQuoteDoc.Text;
                objNewQuote.QSTIn = txtAddQuoteSalesTeamIn.Text;
                objNewQuote.QSTCurrent = txtAddQuoteSalesTeamCurrent.Text;
                objNewQuote.QuoteValue = txtAddQuoteValue.Text;
                objNewQuote.Campaign = ddlTemp.SelectedValue;

                if (objNewQuote.AddNewQuote())
                {

                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                    {
                        //LoadQuoteOver1KWonorLoss();
                        if (System.IO.File.Exists(Pathname))
                        {
                            //Reading XML
                            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                            dsNewQuote.ReadXml(fsReadXml);
                            fsReadXml.Close();

                            DataRow drNewQuote = dsNewQuote.Tables[0].NewRow();

                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT NAME"))
                                drNewQuote["ACCOUNT NAME"] = objNewQuote.AccountName;
                            if (dsNewQuote.Tables[0].Columns.Contains("SOURCE"))
                                drNewQuote["SOURCE"] = "Manual";
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE DAY"))
                                drNewQuote["QUOTE DAY"] = objNewQuote.QuoteDay;
                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT NO."))
                                drNewQuote["ACCOUNT NO."] = objNewQuote.AccountNum;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE DOC"))
                                drNewQuote["QUOTE DOC"] = objNewQuote.Quote_Doc;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE VALUE"))
                                drNewQuote["QUOTE VALUE"] = objNewQuote.QuoteValue;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE ASSIGNMENT"))
                                drNewQuote["QUOTE ASSIGNMENT"] = objNewQuote.QSTIn;
                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT ASSIGNMENT"))
                                drNewQuote["ACCOUNT ASSIGNMENT"] = objNewQuote.QSTCurrent;

                            dsNewQuote.Tables[0].Rows.Add(drNewQuote);

                            dv = new DataView(dsNewQuote.Tables[0]);
                            dv.Sort = "SOURCE" + " " + "ASC";
                            dsNewQuote = new DataSet();
                            dsNewQuote.Tables.Add(dv.ToTable());

                            //Write XML
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dsNewQuote.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            SetOrdinal(dsNewQuote);

                            grdQuoteOver1K.DataSource = dsNewQuote;
                            grdQuoteOver1K.DataBind();
                        }

                    }
                    else
                    {
                        if (System.IO.File.Exists(Pathname))
                        {
                            //Reading XML
                            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                            dsNewQuote.ReadXml(fsReadXml);
                            fsReadXml.Close();

                            DataRow drNewQuote = dsNewQuote.Tables[0].NewRow();

                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT NAME"))
                                drNewQuote["ACCOUNT NAME"] = objNewQuote.AccountName;
                            if (dsNewQuote.Tables[0].Columns.Contains("SOURCE"))
                                drNewQuote["SOURCE"] = "Manual";
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE DAY"))
                                drNewQuote["QUOTE DAY"] = objNewQuote.QuoteDay;
                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT NO."))
                                drNewQuote["ACCOUNT NO."] = objNewQuote.AccountNum;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE DOC"))
                                drNewQuote["QUOTE DOC"] = objNewQuote.Quote_Doc;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE VALUE"))
                                drNewQuote["QUOTE VALUE"] = objNewQuote.QuoteValue;
                            if (dsNewQuote.Tables[0].Columns.Contains("QUOTE ASSIGNMENT"))
                                drNewQuote["QUOTE ASSIGNMENT"] = objNewQuote.QSTIn;
                            if (dsNewQuote.Tables[0].Columns.Contains("ACCOUNT ASSIGNMENT"))
                                drNewQuote["ACCOUNT ASSIGNMENT"] = objNewQuote.QSTCurrent;
                            
                            dsNewQuote.Tables[0].Rows.Add(drNewQuote);

                            dv = new DataView(dsNewQuote.Tables[0]);
                            dv.Sort = "SOURCE ASC,[QUOTE DAY] DESC";
                            dsNewQuote = new DataSet();
                            dsNewQuote.Tables.Add(dv.ToTable());

                            //Write XML
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dsNewQuote.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            SetOrdinal(dsNewQuote);

                            grdQuoteOver1K.DataSource = dsNewQuote;
                            grdQuoteOver1K.DataBind();

                        }

                    }
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    //    "call me", "onSuccesAddNewQuote()", true);
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Saving Add New Quote", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void ArrangeColumn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
              "call me", "window.open('../Home/ArrangeColumns.aspx?Data=lvwQuotePipeline','mywindow','width=700,height=400,scrollbars=yes')", true);
            grdQuoteOver1K.Columns.Clear();
            cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
            DataSet ds = new DataSet();

        }

        #region Function
        // Function not in use before taken for improvement.
        public void ExportExcelFunction()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
            try
            {
                DataSet ds = new DataSet();
                if (File.Exists(Pathname))
                {
                    ds.Tables.Add(GetDatafromXMLAll());
                }

                string UserFileName = SessionFacade.LoggedInUserName + "QuotesOver1K" + ".xls";
                if (ds != null)
                {


                    if (ds.Tables[0].Columns.Contains("uvals"))
                        ds.Tables[0].Columns.Remove("uvals");
                    if (ds.Tables[0].Columns.Contains("Row"))
                        ds.Tables[0].Columns.Remove("Row");
                    if (ds.Tables[0].Columns.Contains("WinorLoss"))
                        ds.Tables[0].Columns.Remove("WinorLoss");
                    ds.AcceptChanges();

                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/QuotesOver1K.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));


                    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);


                    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                    //true means Excel File has been written
                    if (exportToEx == true)
                    {
                        if (Request.Browser.Type == "Desktop") //For chrome
                        {
                            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=QuotesOver1K&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=QuotesOver1K&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                        }
                    }
                    else
                    {
                        //litErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                        // Response.Write("Data not Exported to Excel File");
                    }
                }



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Quotes Over 1K - Error in Export to Excel" + err.ToString());
            }
        }

        public void ExportToExcel()
        {
            try
            {
                DataSet ds = new DataSet();
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                LDAPAccess LDAPAccess = new LDAPAccess();

                string attachment = "attachment; filename=QuotePipeline.xls";
                Response.Clear();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                grdQuoteOver1K.AllowPaging = false;
                grdQuoteOver1K.AllowSorting = false;

                if (rdoSelectQuote1K.SelectedValue == "A")
                {
                    //dt = GetDatafromXMLDataRange();

                    //dt.Columns["QUOTE DAY"].Convert(val => DateTime.Parse(val.ToString()).ToString("mm/dd/yyyy"));

                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                    {

                        LoadQuoteOver1KWonorLoss();
                    }
                    else
                    {
                        ShowQuoteOver1K();

                    }

                    //ds.Tables.Add(GetDatafromXMLDataRange());
                    //SetOrdinal(ds);
                    //grdQuoteOver1K.DataSource = ds;
                }
                else
                {
                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                    {
                        objQuoteOver1K.Campaign = ddlTemp.SelectedValue.ToString().Trim();
                        SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                        objQuoteOver1K.StartDate = txtStartDateQuote1K.Text;
                        objQuoteOver1K.EndDate = txtEndDateQuote1K.Text;

                        if (ddlTemp.SelectedValue == "CA")
                        {
                            if (btnFilterbyReps.Text == "Show All Quotes")
                                objQuoteOver1K.RepName = LDAPAccess.FindName(SessionFacade.LoggedInUserName);
                        }

                        ds = objQuoteOver1K.GetQuoteOver1KWonorLoss_New();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            SetOrdinal(ds);
                            grdQuoteOver1K.DataSource = ds.Tables[0];
                            //grdQuoteOver1K.DataBind();
                        }
                        //LoadQuoteOver1KWonorLoss();
                    }
                    else
                    {

                        if (ddlTemp.SelectedValue == "CA")
                        {
                            if (btnFilterbyReps.Text == "Show All Quotes")
                                objQuoteOver1K.RepName = LDAPAccess.FindName(SessionFacade.LoggedInUserName);
                        }

                        objQuoteOver1K.Campaign = ddlTemp.SelectedValue.ToString().Trim();
                        SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                        objQuoteOver1K.StartDate = txtStartDateQuote1K.Text;
                        objQuoteOver1K.EndDate = txtEndDateQuote1K.Text;
                        ds = objQuoteOver1K.GetQuoteOver1K_New();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            SetOrdinal(ds);
                            grdQuoteOver1K.DataSource = ds.Tables[0];
                            //grdQuoteOver1K.DataBind();
                        }
                    }
                   // ds.Tables.Add(GetDatafromXMLDataRange(txtStartDateQuote1K.Text, txtEndDateQuote1K.Text));
                   // SetOrdinal(ds);
                   //// dt = GetDatafromXMLDataRange(txtStartDateQuote1K.Text, txtEndDateQuote1K.Text);
                   // grdQuoteOver1K.DataSource = ds;
                }
                //grdQuoteOver1K.DataBind();
                grdQuoteOver1K.DataBind();
                for (int i = 0; i < grdQuoteOver1K.Rows.Count; i++)
                {

                    GridViewRow row = grdQuoteOver1K.Rows[i];

                    row.Attributes.Add("class", "textmode");

                }
                
                //Get the HTML for the control.
                grdQuoteOver1K.RenderControl(hw);
                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                Response.Output.Write(tw.ToString());
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Close();

                //grdQuoteOver1K.AllowPaging = true;
                //grdQuoteOver1K.AllowSorting = true;
                
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Quote Pipeline Won or Loss", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }

        private void ShowQuoteOver1K()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            try
            {
                if (Page.PreviousPage != null)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    if (url == Page.PreviousPage.Request.Url.ToString())
                    {
                        if (ddlTemp.SelectedValue != SessionFacade.CampaignValue)
                        {
                            LoadQuoteOver1k();
                        }
                    }
                    else
                    {
                        LoadQuoteOver1k();
                    }
                }
                else
                {
                    LoadQuoteOver1k();
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Quote Over 1K", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadQuoteOver1k()
        {
            try
            {
                LDAPAccess LDAPAccess = new LDAPAccess();
                XmlDocument doc = new XmlDocument();
                string host = HttpContext.Current.Request.Url.Host;
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataSet dsExisting = new DataSet();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                DataTable dtAll = new DataTable();

                //For CA only
                if (ddlTemp.SelectedValue == "CA")
                {
                    if (btnFilterbyReps.Text == "Show All Quotes")
                        objQuoteOver1K.RepName = LDAPAccess.FindName(SessionFacade.LoggedInUserName);

                    objQuoteOver1K.Campaign = ddlTemp.SelectedValue;
                    SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                    dsExisting = objQuoteOver1K.GetQuoteOver1K_New();
                }
                else
                {
                    objQuoteOver1K.Campaign = ddlTemp.SelectedValue.ToString().Trim();
                    SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                    dsExisting = objQuoteOver1K.GetQuoteOver1K_New();
                }

                if (dsExisting.Tables.Count > 0 && dsExisting.Tables[0].Rows.Count > 0)
                {
                    grdQuoteOver1K.AutoGenerateColumns = true;

                    SetOrdinal(dsExisting);

                    grdQuoteOver1K.DataSource = dsExisting.Tables[0];
                    grdQuoteOver1K.DataBind();


                    Hdflnkcolindex.Value = "";
                    HdfWeightedindex.Value = "";
                    HdfProbabliltyindex.Value = "";
                    HdfGM_perc_indx.Value = "";
                    HdfQuoteCost.Value = "";
                    HdfQuoteValue.Value = "";

                    //Write XML
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    dsExisting.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                }
                else
                {
                    grdQuoteOver1K.DataSource = null;
                    grdQuoteOver1K.DataBind();
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Quote Over 1K", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadQuoteOver1KWonorLoss()
        {
            try
            {
                LDAPAccess LDAPAccess = new LDAPAccess();
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataSet dsExisting = new DataSet();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();

                //For CA only
                if (ddlTemp.SelectedValue == "CA")
                {
                    if (btnFilterbyReps.Text == "Show All Quotes")
                        objQuoteOver1K.RepName = LDAPAccess.FindName(SessionFacade.LoggedInUserName);

                    objQuoteOver1K.Campaign = ddlTemp.SelectedValue;
                    SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                    dsExisting = objQuoteOver1K.GetQuoteOver1KWonorLoss_New();
                }
                else
                {
                    objQuoteOver1K.Campaign = ddlTemp.SelectedValue.ToString().Trim();
                    SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                    dsExisting = objQuoteOver1K.GetQuoteOver1KWonorLoss_New();
                }


                //objQuoteOver1K.Campaign = ddlTemp.SelectedValue.ToString().Trim();
                //SessionFacade.CampaignName = objQuoteOver1K.Campaign;
                
                if (dsExisting.Tables.Count > 0 && dsExisting.Tables[0].Rows.Count > 0)
                {
                    grdQuoteOver1K.AutoGenerateColumns = true;

                    if (dsExisting.Tables[0].Columns.Contains("WinorLoss"))
                    {
                        dsExisting.Tables[0].Columns.Remove("WinorLoss");
                        dsExisting.AcceptChanges();
                    }

                    SetOrdinal(dsExisting);


                    grdQuoteOver1K.DataSource = dsExisting.Tables[0];
                    grdQuoteOver1K.DataBind();


                    Hdflnkcolindex.Value = "";
                    HdfWeightedindex.Value = "";
                    HdfProbabliltyindex.Value = "";
                    HdfGM_perc_indx.Value = "";
                    HdfQuoteCost.Value = "";
                    HdfQuoteValue.Value = "";

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    dsExisting.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                }
                else
                {
                    grdQuoteOver1K.DataSource = null;
                    grdQuoteOver1K.DataBind();
                }

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Quote Pipeline Won or Loss", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        //not in use any more 
        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXML(string AccountNum, string Date, string QuoteDoc = "")
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    if (QuoteDoc != "")
                    {
                        Query += " and CUSMERGE='" + AccountNum + "' and Quote_Doc_No='" +
                        QuoteDoc + "' and Quote_Doc_createdon='" + Date + "'";
                    }
                    else
                    {
                        Query += " and CUSMERGE='" + AccountNum +
                            "' and Quote_Doc_createdon='" + Date + "'";
                    }

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        //Not in use any more 
        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDocNumber(string QuoteDoc)
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    Query += " and QUOTEDOC='" +
                        QuoteDoc + "'";

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        //Not in use any more 
        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLRow(string Row)
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                if (SessionFacade.Quote_Pipeline == "WonorLoss")
                {
                    Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" +
                        SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    ds.ReadXml(fsReadXml);
                    fsReadXml.Close();
                }
                else
                {
                    cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                    ds = objQuoteOver1K.GetQuoteOver1K_New();
                    //   grdQuoteOver1K.DataBind();
                    //   Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" +   //    SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                }



                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    Query += " and Row=" +
                        Row;

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        //Showing Open Quotes Only
        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLAllStatic()
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        //Showing Won or Loss Only 
        // Not in use any more after direct contact to database.
        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLWonorLossStatic()
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        //Showing Open Quotes Only
        // //Not in use any more 
        public DataTable GetDatafromXMLAll()
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesOver1K" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        //Showing Won or Loss Only
        //Not in use any more 
        public DataTable GetDatafromXMLWonorLoss()
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        private void GetTotalSales(DataTable dt)
        {

            try
            {

                FunctionVb Function = new FunctionVb();
                decimal sum = 0;
                string query;
                DataRow[] results;
                DataTable dtTemp = new DataTable();

                query = "1=1 ";

                dtTemp = dt.Clone();

                results = dt.Select(query);

                foreach (DataRow dr in results)
                    dtTemp.ImportRow(dr);

                try
                {
                    //Total Quote Value

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        if (dtTemp.Columns.Contains("Weighted"))
                        {
                            sum += Function.ConvertDecimal(dr["Weighted"].ToString().Trim());
                        }

                    }
                }
                catch (Exception ex)
                {
                    BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Total Sales", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                }


            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Total Sales and Orders", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }
        
        public DataTable GetDatafromXMLDataRange(string StartDate="",string EndDate="")
        {
            DataSet ds = new DataSet();
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    if (dtTemp.Columns.Contains("QUOTE DAY"))
                        dtTemp.Columns["QUOTE DAY"].DataType = typeof(string);

                    if (rdoSelectQuote1K.SelectedValue == "DR")
                    {
                        Query += " and ([QUOTE DAY] >= '" + StartDate + "' and [QUOTE DAY] <= '" + EndDate + "')";
                        
                    }

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                    {
                        DateTime dt = DateTime.Parse(dr["QUOTE DAY"].ToString());
                        dr["QUOTE DAY"] = dt.ToShortDateString();
                        dtTemp.ImportRow(dr);
                        
                    }

               
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        private int GetColumnIndexByName(GridViewRow row, string SearchColumnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(SearchColumnName))
                    {
                        break;
                    }
                }
                columnIndex++;
            }
            return columnIndex;
        }

        public DataSet GetDatafromXMLColumn()
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-QuotePipelinecolumn" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                DataSet ds = new DataSet();
                string Query;

                Query = " 1=1";

                if (System.IO.File.Exists(Pathname))
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    ds.ReadXml(fsReadXml);
                    fsReadXml.Close();

                    //To Copy the Schema.
                    if (ds.Tables.Count > 0)
                    {
                        dtTemp = ds.Tables[0].Clone();

                        results = ds.Tables[0].Select(Query);

                        foreach (DataRow dr in results)
                            dtTemp.ImportRow(dr);
                    }
                    else
                        dtTemp = null;

                    ds = new DataSet();

                    ds.Tables.Add(dtTemp);

                    return ds;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static void QuoteComputing(string Campaign, string AccountNum, string Competition,
            string Notes, string Probablilty, string ProposedDate,
            string Quote_Doc, string Weighted, string WinorLoss, string Status,
            string AccountName, string QuoteDay, string QSTCurrent, string QSTIn,
            string QuoteValue, string QuoteCost, string ScheDate, string Mining,
            string Construction, string ProductLine, string QuoteGMPerc)
        {
            try
            {
                string Pathname;
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();

                objQuoteOver1K.Campaign = Campaign;
                objQuoteOver1K.AccountNum = AccountNum;
                objQuoteOver1K.Competition = Competition;
                objQuoteOver1K.Createdby = SessionFacade.LoggedInUserName;
                objQuoteOver1K.Notes = Notes;
                objQuoteOver1K.Probablilty = Probablilty;
                objQuoteOver1K.ProposedDate = ProposedDate;
                objQuoteOver1K.Quote_Doc = Quote_Doc;
                objQuoteOver1K.Weighted = Weighted;
                objQuoteOver1K.WinorLoss = WinorLoss;
                objQuoteOver1K.Status = Status;
                objQuoteOver1K.AccountName = AccountName;
                objQuoteOver1K.QuoteDay = QuoteDay;
                objQuoteOver1K.QSTCurrent = QSTCurrent;
                objQuoteOver1K.QSTIn = QSTIn;
                objQuoteOver1K.QuoteValue = QuoteValue;
                objQuoteOver1K.QuoteCost = QuoteCost;
                objQuoteOver1K.ScheDate = ScheDate;
                objQuoteOver1K.Mining = Mining;
                objQuoteOver1K.Construction = Construction;
                objQuoteOver1K.ProductLine = ProductLine;

                float temp;
                if (float.TryParse(QuoteGMPerc.Replace(" %", ""), out temp))
                    objQuoteOver1K.QuoteGMPerc = (float.Parse(QuoteGMPerc.Replace(" %", "")) / 100.0f).ToString();
                else
                    objQuoteOver1K.QuoteGMPerc = "0";

                if (SessionFacade.Quote_Pipeline == "WonorLoss")
                {
                    if (objQuoteOver1K.AddQuoteComputingWinOrLoss())
                    {


                    }
                }
                else
                {
                    if (objQuoteOver1K.AddQuoteComputing())
                    {



                    }
                }


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Saving Quote Over 1K", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());

            }
        }

        [System.Web.Services.WebMethod]
        public static bool UpdateQuoteValue(string Campaign, string AccountNum, string QuoteValue,
            string QuoteDoc, string lblWeightedValue, string QuoteAssignment)
        {
            try
            {

                DataTable dtSaving = new DataTable();
                string Pathname = "";
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                objQuoteOver1K.QuoteValue = QuoteValue;
                objQuoteOver1K.AccountNum = AccountNum;
                objQuoteOver1K.Quote_Doc = QuoteDoc;
                objQuoteOver1K.Campaign = Campaign;
                objQuoteOver1K.Weighted = lblWeightedValue;
                objQuoteOver1K.QSTIn = QuoteAssignment;

                if (objQuoteOver1K.EditQuoteValue())
                {
                    try
                    {

                        if (SessionFacade.Quote_Pipeline == "WonorLoss")
                        {

                        }
                        else
                        {
                        }


                    }
                    catch (Exception err)
                    {
                        BradyCorp.Log.LoggerHelper.LogException(err, "Error During Updating Quote Value2", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                        return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Updating Quote Value3", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
            }
        }

        [System.Web.Services.WebMethod]
        //Check if the string is valid Number
        public static float CheckIfValidNumber(string value)
        {
            float temp;
            if (float.TryParse(value, out temp))
                return float.Parse(value);
            else
                return 0.0f;
        }

        #endregion

        protected void btnSaveEditValue_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objQuoteOver1K = new cQuoteOver1K();
                objQuoteOver1K.QuoteValue = txtEditQuoteValue.Text;
                objQuoteOver1K.AccountNum = AccountNum.Value;
                objQuoteOver1K.Quote_Doc = QuoteDoc.Value;
                objQuoteOver1K.Campaign = ddlTemp.SelectedValue;
                objQuoteOver1K.Weighted = lblWeightedValue.Text;
                if (objQuoteOver1K.EditQuoteValue())
                {

                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Updating Quote Value", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void grdQuoteOver1K_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
            DataTable dtQuotePipeLine = new DataTable();
            DataSet ds = new DataSet();
            if (System.IO.File.Exists(Pathname))
            {
                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                dtQuotePipeLine.ReadXml(fsReadXml);
                fsReadXml.Close();

                ds.Tables.Add(dtQuotePipeLine);

                SetOrdinal(ds);

                grdQuoteOver1K.DataSource = ds;
                grdQuoteOver1K.PageIndex = e.NewPageIndex;
                grdQuoteOver1K.DataBind();
            }


        }

        #region Sorting
        protected SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridQuoteOpen1K_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridQuoteOpen1K(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridQuoteOpen1K(sortExpression, "ASC");
            }
        }

        private void SortGridQuoteOpen1K(string sortExpression, string direction)
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataSet ds = new DataSet();
                DataTable dtAllColumns = new DataTable();
                DataView dv = new DataView();
                DataTable dtQuotePipeLine = new DataTable();
                if (System.IO.File.Exists(Pathname))
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dtQuotePipeLine.ReadXml(fsReadXml);
                    fsReadXml.Close();

                    if (dtQuotePipeLine.Rows.Count > 0)
                    {
                        dv = new DataView(dtQuotePipeLine);
                        dv.Sort = sortExpression + " " + direction;
                        ds.Tables.Add(dv.ToTable());

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            SetOrdinal(ds);

                            grdQuoteOver1K.DataSource = ds;
                            grdQuoteOver1K.DataBind();
                        }
                        else
                        {
                            grdQuoteOver1K.DataSource = null;
                            grdQuoteOver1K.DataBind();
                        }

                    }


                }

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        protected void btnFilterbyReps_Click(object sender, EventArgs e)
        {
            try
            {

                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-QuotesPipelineWonorLoss" + ".xml";
                DataSet ds = new DataSet();
                LDAPAccess LDAPAccess = new LDAPAccess();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cQuoteOver1K objFilterbyReps = new cQuoteOver1K();

                if (btnFilterbyReps.Text == "Show My Quotes")
                {
                    objFilterbyReps.Campaign = ddlTemp.SelectedValue;
                    objFilterbyReps.RepName = LDAPAccess.FindName(SessionFacade.LoggedInUserName);

                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                        ds = objFilterbyReps.GetQuoteOver1KWonorLoss_New();
                    else
                        ds = objFilterbyReps.GetQuoteOver1K_New();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        grdQuoteOver1K.AutoGenerateColumns = true;

                        SetOrdinal(ds);

                        grdQuoteOver1K.DataSource = ds.Tables[0];
                        grdQuoteOver1K.DataBind();


                        Hdflnkcolindex.Value = "";
                        HdfWeightedindex.Value = "";
                        HdfProbabliltyindex.Value = "";
                        HdfGM_perc_indx.Value = "";
                        HdfQuoteCost.Value = "";
                        HdfQuoteValue.Value = "";

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.Tables[0].WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        btnFilterbyReps.Text = "Show All Quotes";
                        btnFilterbyReps.ToolTip = "Click to show all quotes.";
                        lblFilterByQuotes1K.Text = LDAPAccess.FindName(SessionFacade.LoggedInUserName);
                    }

                }
                else
                {
                    btnFilterbyReps.Text = "Show My Quotes";
                    btnFilterbyReps.ToolTip = "Click to filter by your quotes.";
                    lblFilterByQuotes1K.Text = "All";

                    if (SessionFacade.Quote_Pipeline == "WonorLoss")
                        LoadQuoteOver1KWonorLoss();
                    else
                        LoadQuoteOver1k();
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Binding by Quote Rep.", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }
}