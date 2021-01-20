using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class cAddPreferences
{
    public string Username { get; set; }
	public string Account { get; set; }
	public int Contact { get; set; }
	public string Phone { get; set; }
	public string ContactName { get; set; }
	public string Campaign { get; set; }
	public string Mail { get; set; }
	public string Fax { get; set; }
    public string Email { get; set; }
    

    public bool AddPreferences()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.AddPreferences(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public DataSet SelectPreferences()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.SelectPreferences(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

public class cAddAccountChanges
{
    public string Username { get; set; }
    public string Account { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string AccountName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Comment { get; set; }
    public string Campaign { get; set; }

    public bool AddAccountChanges()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.AddAccountChanges(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public DataSet SelectAccountChanges()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.SelectAccountChanges(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

public class cAddContactChanges
{
    public string Username { get; set; }
    public string Account { get; set; }
    public int Contact { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Status { get; set; }
    public string Function { get; set; }
    public string Title { get; set; }
    public string Phone { get; set; }
    public string PhoneExt { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public string Campaign { get; set; }

    public bool AddContactChanges()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.AddContactChanges(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public DataSet SelectContactChanges()
    {
        cSubmitMasterDataChangeDB objSubmitMasteDataChanges = new cSubmitMasterDataChangeDB();
        try
        {
            return objSubmitMasteDataChanges.SelectContactChanges(this);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}