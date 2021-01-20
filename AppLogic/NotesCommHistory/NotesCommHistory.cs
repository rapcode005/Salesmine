using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.IO;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using BradyCorp.Log;

namespace AppLogic
{
    public class cNotesCommHistory
    {
        public string CampaignName { get; set; }
        public string AccountNum { get; set; }
        public string Contact { get; set; }
        public string CreatedBy { get; set; }
        public string SearchOrderContact { get; set; }
        public string SearchOrderStartDate { get; set; }
        public string SearchOrderEndDate { get; set; }
        public string NotesKAMID { get; set; }
        public string CurentDate { get; set; }
        public string ComputerTime { get; set; }

        public DataSet GetNotesCommHistory()
        {
            cNotesCommHistoryDB objNotesCommHistory = new cNotesCommHistoryDB();
            try
            {
                return objNotesCommHistory.GetNotesCommHistory(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetGoldMineNotesCommHistory()
        {
            cNotesCommHistoryDB objNotesCommHistory = new cNotesCommHistoryDB();
            try
            {
                return objNotesCommHistory.GetGoldMineNotesCommHistory(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetUserNotes()
        {
            cNotesCommHistoryDB objUserNotes = new cNotesCommHistoryDB();

            try
            {
                return objUserNotes.GetUserNotes(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }

        public DataSet GetScheduleNotes()
        {
            cNotesCommHistoryDB objSchedulesNotes = new cNotesCommHistoryDB();

            try
            {
                return objSchedulesNotes.GetScheduleNotes(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //Get All Note Type
        public DataSet GetNoteType()
        {
            cNotesCommHistoryDB objNoteType = new cNotesCommHistoryDB();

            try
            {
                return objNoteType.GetNoteType(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }

        // will be use in Territory
        public DataSet GetNotesCommHistoryT()
        {
            cNotesCommHistoryDB objNotesCommHistory = new cNotesCommHistoryDB();
            try
            {
                return objNotesCommHistory.GetTerritoryNotesCommHistory(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool StatusExcelButton()
        {
            string[] Unit = { "PC-ONT", "ADMIN", "CUS" };
            bool Result = false;
            try
            {
                for (int index = 0; index <= 2; index++)
                {
                    Result = (SessionFacade.UserRole.Trim() == Unit[index]);
                    if (Result) break;
                }

                return (SessionFacade.CampaignName == "PC" && Result == true) ? true : false;

            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex,
                    "Error During StatusExcelButton()",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
            }
        }
    }

    public class cDialerData
    {
        public string SearchOrderCampaignName { get; set; }
        public string SearchOrderAccount { get; set; }
        public string SearchOrderContact { get; set; }
        public string SearchOrderStartDate { get; set; }
        public string SearchOrderEndDate { get; set; }
        public string tempUnit { get; set; }
        public string DialerKAMID { get; set; }

        public DataSet GetDialerData()
        {
            cDialerDataDB objDialerData= new cDialerDataDB();
            try
            { 
                return objDialerData.GetDialerData(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetDialerDataT()
        {
            cDialerDataDB objDialerData = new cDialerDataDB();
            try
            {
                return objDialerData.GetDialerDataTerritory(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

    public class cAddNote
    {
        public string CreatedBy { get; set; }
        public string NoteType { get; set; }
        public string Note { get; set; }
        public string AccountNum { get; set; }
        public string ContactNum { get; set; }
        public string NoteDate { get; set; }
        public string Campaign { get; set; }
        public string Createdon { get; set; }

        public bool AddNote()
        {
            cAddNOteDB objExecAddNote = new cAddNOteDB();
            try
            {
                return objExecAddNote.ExecAddNote(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Add Note");
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error in Add Note", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;

            }

        }

        public bool UpdateNote()
        {
            cAddNOteDB objExecUpdateNote = new cAddNOteDB();
            try
            {
                return objExecUpdateNote.ExecUpdateNote(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Update Note");
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error in Update Note", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;

            }

        }
    }
    
}