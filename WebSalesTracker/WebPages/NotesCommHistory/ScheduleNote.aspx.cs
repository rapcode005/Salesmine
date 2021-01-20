using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLogic;
using System.Data;

namespace WebSalesMine.WebPages.NotesCommHistory
{
    public partial class ScheduleNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowSchduleNote();

            grdSchduleNote.UseAccessibleHeader = true;
            if (grdSchduleNote.HeaderRow != null)
            {
                //This will tell ASP.NET to render the <thead> for the header row 
                //using instead of the simple <tr>
                grdSchduleNote.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        #region Schdule Note
        public DataSet BindSchduleNote()
        {
            try
            {
                //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cNotesCommHistory objSchduleNote = new cNotesCommHistory();

                objSchduleNote.CampaignName = SessionFacade.CampaignName;
                objSchduleNote.CreatedBy = SessionFacade.LoggedInUserName;
                objSchduleNote.CurentDate = GetCurrentDate();

                return objSchduleNote.GetScheduleNotes();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During:BindSchduleNote",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }

        public void ShowSchduleNote()
        {
            try
            {
                DataSet dsSchduleNote = new DataSet();

                dsSchduleNote = BindSchduleNote();

                if (dsSchduleNote != null && dsSchduleNote.Tables.Count > 0)
                {
                    grdSchduleNote.DataSource = dsSchduleNote;
                    grdSchduleNote.DataBind();
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During:BindSchduleNote",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private string GetCurrentDate()
        {
            string Month,Day,Year;

            //Month
            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 9)
                Month = "0" + DateTime.Now.Month.ToString();
            else
                Month = DateTime.Now.Month.ToString();

            //Day
            if (DateTime.Now.Day >= 1 && DateTime.Now.Day <= 9)
                Day = "0" + DateTime.Now.Day.ToString();
            else
                Day = DateTime.Now.Day.ToString();

            //Year
            Year = DateTime.Now.Year.ToString();

            return Month + "/" + Day + "/" + Year;
        }
        #endregion
    }
}