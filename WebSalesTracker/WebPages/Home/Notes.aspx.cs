using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AppLogic;
using System.Collections;
using System.IO;


namespace WebSalesMine.WebPages.Home
{
    public partial class Notes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                Set();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
           
        }

        protected void AddNote_TextChanged(object sender, EventArgs e)
        {
            Set();

           //sessionfTextNote
        }

          
        public static void NoteType()
          
          {
              
              //var notetxt=SessionFacade.TextNote;
              //var notedate = SessionFacade.NoteDate;
              //var notetype = SessionFacade.NoteType;

             // SessionFacade.TextNote

          }

        private void Set()
        {
            SessionFacade.NoteDate = txtStartDate.Text;
            SessionFacade.NoteType = NoteTyped.Text.ToString();
            SessionFacade.TextNote = AddNote.Text;
        }

        protected void imgstartCal_Click(object sender, ImageClickEventArgs e)
        {
           
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            Set();
        }

        protected void NoteTyped_SelectedIndexChanged(object sender, EventArgs e)
        {

            Set();
        }


    }
}