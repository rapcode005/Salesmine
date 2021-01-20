using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Web.UI;
using System.Text;

//public class FunctionNum
//{
//    public string Number { get; set; }

//    public decimal Amount{ get; set; }
    
//    public FunctionNum FormatAccountNum(FunctionNum Num)
//    {
//        string NewAccountN;
//        long temp;
//        if (long.TryParse(Num.Number, out temp) == true)
//        {
//            NewAccountN = string.Format("{0:0000000000}",
//                long.Parse(Num.Number));
//        }
//        else
//            NewAccountN = "";

//        Num.Number = NewAccountN;

//        return Num;
//    }

//    public static string FormatAccountNum(string Num)
//    {
//        string NewAccountN;
//        if (Num.Length == 10)
//        {
//            NewAccountN = Num;
//        }
//        else
//        {
//            long temp;
//            if (long.TryParse(Num, out temp) == true)
//            {
//                NewAccountN = string.Format("{0:0000000000}",
//                    long.Parse(Num));
//            }
//            else
//                NewAccountN = "";
//        }

//        return NewAccountN;
//    }

//    public FunctionNum FormatCurrency(FunctionNum Num, string currencyCode)
//    {

//        var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
//                       let r = new RegionInfo(c.LCID)
//                       where r != null
//                       && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
//                       select c).FirstOrDefault();

//        if (culture == null)
//        {
//            Num.Number = Num.Amount.ToString("0.00");
//        }
//        else
//        {
//            Num.Number = string.Format(culture, "{0:C}", Num.Amount);
//        }
//        return Num;
//    }

//    public FunctionNum FormatCurrency(decimal dec, string currencyCode)
//    {
//        FunctionNum Num = new FunctionNum();
//        var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
//                       let r = new RegionInfo(c.LCID)
//                       where r != null
//                       && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
//                       select c).FirstOrDefault();

//        if (culture == null)
//        {
//            Num.Number = dec.ToString("0.00");
//        }
//        else
//        {
//            Num.Number = string.Format(culture, "{0:C}", dec);
//        }
//        return Num;
//    }

//    public static implicit operator string(FunctionNum Num)
//    {
//        return Num.Number;
//    }

//}

public static class InClauseObjectExtensions
{
	public static bool In<T>(this T @object, params T[] values)
	{
		// this is LINQ expression. If you don't want to use LINQ,
		// you can use a simple foreach and return true 
		// if object is found in the array
		return values.Contains(@object);
	}
 
	public static bool In<T>(this T @object, IEnumerable<T> valueList)
	{
		// this is LINQ expression. If you don't want to use LINQ,
		// you can use a simple foreach and return true if object 
		// is found in the array
		return valueList.Contains(@object);
	}

    public static string FormatAccountNumber<Num>(this Num @N)
    {
        string NewAccountN,str;
        str = @N.ToString();
        if (str.Length == 10)
        {
            NewAccountN = str;
        }
        else
        {
            long temp;
            if (long.TryParse(str, out temp) == true)
            {
                NewAccountN = string.Format("{0:0000000000}",
                    long.Parse(str));
            }
            else
                NewAccountN = "";
        }

        return NewAccountN;
    }

    public static string FormatMoney(this decimal @N, string currencyCode)
    {
        var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                       let r = new RegionInfo(c.LCID)
                       where r != null
                       && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                       select c).FirstOrDefault();

        return (culture == null) ? @N.ToString("0.00") : string.Format(culture, "{0:C}", @N);

    }

    public static string CheckValidColumn(this DataTable @N, string[] List)
    {
        int Index;
        bool Result = false;
        //Check Column
        for (Index = 0; Index < List.Length; Index++)
        {
            if (@N.Columns.Contains(List[Index]))
            {
                Result = true;
                break;
            }
        }

        return (Result) ? List[Index] : string.Empty; 
    }

    //public static bool CheckDataRecords(this DataSet @N)
    //{
    //    return @N.Tables.Cast<DataTable>()
    //        .Any(table => table.Rows.Count != 0);
    //}

    public static bool CheckDataRecords(this DataTable @N)
    {
        return (@N != null && @N.Rows.Count > 0) ? true : false; 
    }

    public static bool CheckDataRecords(this DataSet @N)
    {
        return (@N != null) && @N.Tables.Cast<DataTable>()
            .Any(table => table.Rows.Count != 0) ? true : false;
    }

}


