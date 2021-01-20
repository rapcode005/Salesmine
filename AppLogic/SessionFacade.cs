using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace AppLogic
{
    public class SessionFacade
    {

        #region Constants

        private const string SortExpression = "Sort Expression";
        private const string SortDirection = "Sort Direction";

        private const string ForPage = "For Page";
        private const string Index = "Index";

        private const string Email_Add = "EmailAddress";
        private const string Password = "Pass_Word";
        private const string PostBack = "Post_Back";
        private const string CampaignNameUnit = "Campaign_Name_Unit";
        private const string CampaignNameValueUnit = "Campaign_Name_Value_Unit";
        

        private const string SelectedUsername = "Selected_Username";

        private const string UpdateDefaultOrNot = "Update_Default_Or_Not";

        private const string View_Website = "ViewWebsite";

        private const string MessagePopup = "PopupMessage";
        private const string AlreadyLoad = "AlreadyLoad";
        private const string UpdateBool = "UpdateBool";

        private const string LetterSearch = "SearchLetter";

        private const string LOGGED_IN_USERNAME = "LoggedInUserName";
        private const string USER_ROLE = "UserRole";


        private const string KAM_NAME = "KamName";
        private const string KAM_ID = "KamId";

        private const string CAMPAIGN_NAME = "CampaignName";
        private const string LOGGED_CAMPAIGN_NAME = "LoggedCampaignName";
        private const string CAMPAIGN_VALUE = "CampaignValue";
        private const string LOGGED_CAMPAIGN_VALUE = "LoggedCampaignValue";

        private const string ACCOUNT_NUMBER = "AccountNo";
        private const string TEMP_ACCOUNT_NUMBER = "TempAccountNo";
        private const string ACCOUNT_NAME = "AccountName";
        private const string TEMP_ACCOUNT_NAME = "TempAccountName";

        private const string SKU_CATEGORY = "SKUCategory";
        private const string Buyer_Ct = "BuyerCt";

        private const string LastAccountName = "LastAccount";
        private const string UserTrigger = "UserTrig";

        private const string Contact_Name = "ContactName";
        private const string TempContact = "ContactNo";
        private const string TempContactN = "Contact_Name";

        private const string SHOW_KAM_WINDOW = "ShowKamWindow";

        private const string TempTxtNote = "TxtNote";
        private const string TempNoteType = "NoteType";
        private const string TempNoteDate = "NoteDate";

        private const string ORDER_LOOKUP = "OrderLookUp";
        private const string CUSTOMER_LOOKUP = "CustomerLookUp";
        private const string LISTVIEWTYPE = "ListVieweType";

        private const string Name_Or_Contacts = "NameOrContacts";

        private const string Page_session = "PageSession";

        private const string CONTACT_LEVEL_VAL = "ContactlevelVal";
        private const string FILTER_VAL = "FilterVal";

        private const string ProductlineDesc = "Description";
        private const string QuotePipeline = "Quote Pipeline";

        private const string ConstProjectID = "ProjectID";
        private const string ConstProjectIDpPrev = "ProjectIDPrev";

        private const string ConstProjectStat = "ProjectStatus";
        private const string GenConID = "GenConID";

        private const string MinOptions = "MinOptions";
        private const string MinID = "MinID";
        private const string QAStat = "QAStat";
        private const string MContactID = "ContID";

        private const string Save_URL = "SaveURL";
        #endregion

        #region Public Methods

        /// <summary>
        /// Removes all session.
        /// </summary>
        public static void RemoveAllSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static string Sort_Expression
        {
            get
            {
                if (HttpContext.Current.Session[SortExpression] != null)
                {
                    return (string)HttpContext.Current.Session[SortExpression];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[SortExpression] = value;
            }
        }

        public static string Sort_Direction
        {
            get
            {
                if (HttpContext.Current.Session[SortDirection] != null)
                {
                    return (string)HttpContext.Current.Session[SortDirection];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[SortDirection] = value;
            }
        }

        public static int For_Page
        {
            get
            {
                if (HttpContext.Current.Session[ForPage] != null)
                {
                    return (int)HttpContext.Current.Session[ForPage];
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[ForPage] = value;
            }
        }

        public static int OrderIndex
        {
            get
            {
                if (HttpContext.Current.Session[Index] != null)
                {
                    return (int)HttpContext.Current.Session[Index];
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[Index] = value;
            }
        }



        public static string Post_Back_Update
        {
            get
            {
                if (HttpContext.Current.Session[PostBack] != null)
                {
                    return (string)HttpContext.Current.Session[PostBack];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[PostBack] = value;
            }
        }


        public static string Email_Address
        {
            get
            {
                if (HttpContext.Current.Session[Email_Add] != null)
                {
                    return (string)HttpContext.Current.Session[Email_Add];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Email_Add] = value;
            }
        }

        public static string Pass_Word
        {
            get
            {
                if (HttpContext.Current.Session[Password] != null)
                {
                    return (string)HttpContext.Current.Session[Password];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Password] = value;
            }
        }

        public static string Campaign_Name_Value_Unit
        {
            get
            {
                if (HttpContext.Current.Session[CampaignNameValueUnit] != null)
                {
                    return (string)HttpContext.Current.Session[CampaignNameValueUnit];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CampaignNameValueUnit] = value;
            }
        }

        public static string Campaign_Name_Unit
        {
            get
            {
                if (HttpContext.Current.Session[CampaignNameUnit] != null)
                {
                    return (string)HttpContext.Current.Session[CampaignNameUnit];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CampaignNameUnit] = value;
            }
        }

        public static string Selected_Username
        {
            get
            {
                if (HttpContext.Current.Session[SelectedUsername] != null)
                {
                    return (string)HttpContext.Current.Session[SelectedUsername];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[SelectedUsername] = value;
            }
        }

        public static string UpdateDefault_Or_Not
        {
            get
            {
                if (HttpContext.Current.Session[UpdateDefaultOrNot] != null)
                {
                    return (string)HttpContext.Current.Session[UpdateDefaultOrNot];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[UpdateDefaultOrNot] = value;
            }
        }

        public static string Quote_Pipeline
        {
            get
            {
                if (HttpContext.Current.Session[QuotePipeline] != null)
                {
                    return (string)HttpContext.Current.Session[QuotePipeline];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[QuotePipeline] = value;
            }
        }

        public static string ViewWebsite
        {
            get
            {
                if (HttpContext.Current.Session[View_Website] != null)
                {
                    return (string)HttpContext.Current.Session[View_Website];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[View_Website] = value;
            }
        }

        public static string PopupMessage
        {
            get
            {
                if (HttpContext.Current.Session[MessagePopup] != null)
                {
                    return (string)HttpContext.Current.Session[MessagePopup];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[MessagePopup] = value;
            }
        }


        public static string LoggedInUserName
        {
            get
            {
                if (HttpContext.Current.Session[LOGGED_IN_USERNAME] != null)
                {
                    return (string)HttpContext.Current.Session[LOGGED_IN_USERNAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[LOGGED_IN_USERNAME] = value;
            }
        }



        public static string SearchLetter
        {
            get
            {
                if (HttpContext.Current.Session[LetterSearch] != null)
                {
                    return (string)HttpContext.Current.Session[LetterSearch];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[LetterSearch] = value;
            }
        }

        public static string NameOrContacts
        {
            get
            {
                if (HttpContext.Current.Session[Name_Or_Contacts] != null)
                {
                    return (string)HttpContext.Current.Session[Name_Or_Contacts];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Name_Or_Contacts] = value;
            }
        }

        public static bool Update_Bool
        {
            get
            {
                if (HttpContext.Current.Session[UpdateBool] != null)
                {
                    return (bool)HttpContext.Current.Session[UpdateBool];
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session[UpdateBool] = value;
            }
        }

        public static bool Already_Load
        {
            get
            {
                if (HttpContext.Current.Session[AlreadyLoad] != null)
                {
                    return (bool)HttpContext.Current.Session[AlreadyLoad];
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session[AlreadyLoad] = value;
            }
        }
        /// <summary>
        /// Sets the name of the logged in user.
        /// </summary>
        /// <param name="loggedInUserName">Name of the logged in user.</param>
        //public static void SetLoggedInUserName(string loggedInUserName)
        //{
        //    HttpContext.Current.Session[LOGGED_IN_USERNAME] = loggedInUserName;
        //}

        ///// <summary>
        ///// Gets the name of the logged in user.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetLoggedInUserName()
        //{
        //    return HttpContext.Current.Session[LOGGED_IN_USERNAME] as string;
        //}

        /// <summary>
        /// Removes the name of the logged in user.
        /// </summary>
        public static void RemoveLoggedInUserName()
        {
            HttpContext.Current.Session.Remove(LOGGED_IN_USERNAME);
        }

        public static string UserRole
        {
            get
            {
                if (HttpContext.Current.Session[USER_ROLE] != null)
                {
                    return (string)HttpContext.Current.Session[USER_ROLE];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[USER_ROLE] = value;
            }
        }

        public static string CampaignName
        {
            get
            {
                if (HttpContext.Current.Session[CAMPAIGN_NAME] != null)
                {
                    return (string)HttpContext.Current.Session[CAMPAIGN_NAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CAMPAIGN_NAME] = value;
            }
        }


        public static string LoggedCampaignName
        {
            get
            {
                if (HttpContext.Current.Session[LOGGED_CAMPAIGN_NAME] != null)
                {
                    return (string)HttpContext.Current.Session[LOGGED_CAMPAIGN_NAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[LOGGED_CAMPAIGN_NAME] = value;
            }
        }
        

        public static string CampaignValue
        {
            get
            {
                if (HttpContext.Current.Session[CAMPAIGN_VALUE] != null)
                {
                    return (string)HttpContext.Current.Session[CAMPAIGN_VALUE];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CAMPAIGN_VALUE] = value;
            }
        }


        public static string KamName
        {
            get
            {
                if (HttpContext.Current.Session[KAM_NAME] != null)
                {
                    return (string)HttpContext.Current.Session[KAM_NAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[KAM_NAME] = value;
            }
        }

        public static string KamId
        {
            get
            {
                if (HttpContext.Current.Session[KAM_ID] != null)
                {
                    return (string)HttpContext.Current.Session[KAM_ID];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[KAM_ID] = value;
            }
        }



        public static string AccountNo
        {
            get
            {
                if (HttpContext.Current.Session[ACCOUNT_NUMBER] != null)
                {
                    return (string)HttpContext.Current.Session[ACCOUNT_NUMBER];
                }
                return string.Empty;
            }
            set
            {
                string FormatedAccountNo = value.Trim().ToString().PadLeft(10, '0');
                
                HttpContext.Current.Session[ACCOUNT_NUMBER] = FormatedAccountNo;
            }
        }

        public static string TempAccountNo
        {
            get
            {
                if (HttpContext.Current.Session[TEMP_ACCOUNT_NUMBER] != null)
                {
                    return (string)HttpContext.Current.Session[TEMP_ACCOUNT_NUMBER];
                }
                return string.Empty;
            }
            set
            {
                string FormatedTempAccountNo = value.Trim().ToString().PadLeft(10, '0');
                HttpContext.Current.Session[TEMP_ACCOUNT_NUMBER] = FormatedTempAccountNo;
            }
        }


        public static string AccountName
        {
            get
            {
                if (HttpContext.Current.Session[ACCOUNT_NAME] != null)
                {
                    return (string)HttpContext.Current.Session[ACCOUNT_NAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ACCOUNT_NAME] = value.Trim();
            }
        }


        public static string TempAccountName
        {
            get
            {
                if (HttpContext.Current.Session[TEMP_ACCOUNT_NAME] != null)
                {
                    return (string)HttpContext.Current.Session[TEMP_ACCOUNT_NAME];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[TEMP_ACCOUNT_NAME] = value.Trim();
            }
        }
        public static string SKUCategory
        {
            get
            {
                if (HttpContext.Current.Session[SKU_CATEGORY] != null)
                {
                    return (string)HttpContext.Current.Session[SKU_CATEGORY];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[SKU_CATEGORY] = value;
            }
        }

        public static string BuyerCt
        {
            get
            {
                if (HttpContext.Current.Session[Buyer_Ct] != null)
                {
                    return (string)HttpContext.Current.Session[Buyer_Ct];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Buyer_Ct] = value;
            }
        }

        public static string PageSession
        {
            get
            {
                if (HttpContext.Current.Session[Page_session] != null)
                {
                    return (string)HttpContext.Current.Session[Page_session];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Page_session] = value;
            }
        }
        public static string[] LastAccount
        {
            get
            {
                if (HttpContext.Current.Session[LastAccountName] != null)
                {
                    return (string[])HttpContext.Current.Session[LastAccountName];
                }
                return new string[6];
            }
            set
            {
                HttpContext.Current.Session[LastAccountName] = value;
            }
        }
        public static string UserTrig
        {
            get
            {
                if (HttpContext.Current.Session[UserTrigger] != null)
                {
                    return (string)HttpContext.Current.Session[UserTrigger];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[UserTrigger] = value;
            }
        }

        public static string ContactName
        {
            get
            {
                if (HttpContext.Current.Session[Contact_Name] != null)
                {
                    return (string)HttpContext.Current.Session[Contact_Name];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Contact_Name] = value;
            }
        }

        public static string ShowKamWindow
        {
            get
            {
                if (HttpContext.Current.Session[SHOW_KAM_WINDOW] != null)
                {
                    return (string)HttpContext.Current.Session[SHOW_KAM_WINDOW];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[SHOW_KAM_WINDOW] = value;
            }
        }

        public static string OrderLookUp
        {
            get
            {
                if (HttpContext.Current.Session[ORDER_LOOKUP] != null)
                {
                    return (string)HttpContext.Current.Session[ORDER_LOOKUP];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ORDER_LOOKUP] = value;
            }
        }

        public static string CustomerLookUp
        {
            get
            {
                if (HttpContext.Current.Session[CUSTOMER_LOOKUP] != null)
                {
                    return (string)HttpContext.Current.Session[CUSTOMER_LOOKUP];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CUSTOMER_LOOKUP] = value;
            }
        }

        public static string ListVieweType
        {
            get
            {
                if (HttpContext.Current.Session[LISTVIEWTYPE] != null)
                {
                    return (string)HttpContext.Current.Session[LISTVIEWTYPE];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[LISTVIEWTYPE] = value;
            }
        }


        public static string ContactlevelVal
        {
            get 
            {
                if (HttpContext.Current.Session[CONTACT_LEVEL_VAL] != null)
                {
                    return (string)HttpContext.Current.Session[CONTACT_LEVEL_VAL];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[CONTACT_LEVEL_VAL] = value;
            }
        }

        public static string FILTERVAL
        {
            get
            {
                if (HttpContext.Current.Session[FILTER_VAL] != null)
                {
                    return (string)HttpContext.Current.Session[FILTER_VAL];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[FILTER_VAL] = value;
            }
        }

        public static string PRODLINE
        {
            get
            {
                if (HttpContext.Current.Session[ProductlineDesc] != null)
                {
                    return (string)HttpContext.Current.Session[ProductlineDesc];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ProductlineDesc] = value;
            }
        }

        public static string SaveUrl
        {
            get
            {
                if (HttpContext.Current.Session[Save_URL] != null)
                {
                    return (string)HttpContext.Current.Session[Save_URL];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[Save_URL] = value;
            }
        }

        #endregion

        #region Notes

        public static string TextNote
        {
            get
            {
                if (HttpContext.Current.Session[TempTxtNote] != null)
                {
                    return (string)HttpContext.Current.Session[TempTxtNote];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[TempTxtNote] = value;
            }
        }


        public static string NoteType
        {
            get
            {
                if (HttpContext.Current.Session[TempNoteType] != null)
                {
                    return (string)HttpContext.Current.Session[TempNoteType];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[TempNoteType] = value;
            }
        }



        public static string NoteDate
        {
            get
            {
                if (HttpContext.Current.Session[TempNoteDate] != null)
                {
                    return (string)HttpContext.Current.Session[TempNoteDate];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[TempNoteDate] = value;
            }
        }


        #endregion

        #region Construction
        public static string PROJECTID
        {
            get
            {
                if (HttpContext.Current.Session[ConstProjectID] != null)
                {
                    return (string)HttpContext.Current.Session[ConstProjectID];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ConstProjectID] = value;
            }
        }



        public static string PROJECTIDPREVIOUS
        {
            get
            {
                if (HttpContext.Current.Session[ConstProjectIDpPrev] != null)
                {
                    return (string)HttpContext.Current.Session[ConstProjectIDpPrev];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ConstProjectIDpPrev] = value;
            }
        }


        public static string PROJECTSTATUS
        {
            get
            {
                if (HttpContext.Current.Session[ConstProjectStat] != null)
                {
                    return (string)HttpContext.Current.Session[ConstProjectStat];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[ConstProjectStat] = value;
            }
        }

        public static string GeneralConID
        {
            get
            {
                if (HttpContext.Current.Session[GenConID] != null)
                {
                    return (string)HttpContext.Current.Session[GenConID];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[GenConID] = value;
            }
        }


        #endregion


        #region Mining

        public static string SelectedOption
        { 
            get
            {
                if (HttpContext.Current.Session[MinOptions] != null)
                {
                    return (string)HttpContext.Current.Session[MinOptions];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[MinOptions] = value;
            }
        }

        public static string MiningID
        {
            get 
            {
                if (HttpContext.Current.Session[MinID] != null)
                {
                    return (string)HttpContext.Current.Session[MinID];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[MinID] = value;
            }
        }

        public static string QASTATUS
        {
            get
            { 
                if (HttpContext.Current.Session[QAStat] != null)
                {
                    return (string)HttpContext.Current.Session[QAStat];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[QAStat] = value;
            }
        }

        public static string ContactID
        {
            get
            {
                if (HttpContext.Current.Session[MContactID] != null)
                {
                    return (string)HttpContext.Current.Session[MContactID];
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[MContactID] = value;
            }
        }
        #endregion
    }
}
