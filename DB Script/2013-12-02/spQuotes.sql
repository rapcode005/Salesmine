USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[spQuotes]    Script Date: 12/02/2013 20:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
-- [dbo].[spQuotes]  @Account='0001076819',@Campaign='EMED'  
ALTER PROCEDURE [dbo].[spQuotes]    
 -- Add the parameters for the stored procedure here    
 @Account varchar(10),    
 @Campaign varchar(100)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     /* Variable Declaration */    
    Declare @SQLQuery AS NVarchar(4000)    
    Declare @ParamDefinition AS NVarchar(2000)     
    
    
 If @Campaign = 'EMED'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from EMED.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join EMED.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
 end 
  End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch  
   
  --         order by a.quote_doc_no desc   
 else If @Campaign = 'CA'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from CA.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join CA.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end     
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch  
  else If @Campaign = 'US'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from US.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join US.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end     
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch  
  else If @Campaign = 'UK'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from UK.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join UK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end     
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch  
  else If @Campaign = 'SUK'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from SUK.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join SUK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end    
  
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch  
   else If @Campaign = 'AT'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from AT.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join SUK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end    
  
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch 
else If @Campaign = 'CH'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from CH.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join SUK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end    
  
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch   
 else If @Campaign = 'DE'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from DE.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join SUK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end    
  
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch   
 else If @Campaign = 'PC'    
 Begin Try  
 begin   
    
  select a.Quote_Doc_No    
        ,a.Quote_Line,a.Quote_Date    
        ,a.Quote_Created_By    
        ,a.Quote_Create_Time    
        ,a.Quote_PO_Type    
        ,a.Quote_PO_Type_Desc    
        ,a.Quote_Item_Categ_Desc    
        ,a.Quote_Reason_Code    
        ,a.Quote_Reason_Code_Desc    
        ,a.Quote_Reason_Rej_Desc    
        ,a.Quote_SlsTeamIN    
        ,a.Quote_Material    
        ,a.Quote_Material_Desc    
        ,a.Quote_Mat_Entrd    
        ,a.Quote_Mat_Entrd_Desc    
        ,a.Product_Line_Desc    
        ,a.Product_Family_Desc    
        ,a.Quote_Coupon_CODE    
        ,a.Quote_Discount    
        ,a.Quote_Net_Sales    
        ,a.Quote_Freight    
        ,a.Quote_Cost    
        ,a.Order_Doc_No    
        ,a.Order_Line    
        ,a.Order_Date    
        ,a.Order_Createdby    
        ,a.Order_PO_Type_Desc    
        ,a.Order_Item_Categ_Desc    
        ,a.Order_Material_Desc    
        ,a.Order_Mat_Entrd_Desc    
        ,a.Order_Discount    
        ,a.Order_Net_Sales    
        ,a.Order_Freight    
        ,a.Order_Cost    
        ,a.Order_Refer_Doc    
        ,a.Order_Refer_Itm    
        ,a.Order_Coupon_CODE    
        ,b.sales_team_name    
        ,c.Surname    
        ,c.Firstname    
        ,a.Quote_Doc_createdon    
        ,a.Quote_Doc_Create_Time    
        ,a.quote_sales_doc    
        ,a.quote_cost_doc    
        ,a.Quote_GM_percent    
        ,a.DM_Product_Line_Desc    
        ,a.Quote_Doc_Reason_Code_Desc    
        ,a.Status    
        ,a.order_date_converted    
        ,a.order_sales_doc    
        ,a.Order_GM_percent    
        ,a.tag    
        ,a.num_quotes_contact    
        ,a.num_quotes_conv_contact    
        ,a.percent_conversion_cont    
        ,a.num_quotes_site    
        ,a.num_quotes_conv_site    
        ,a.percent_conversion_site    
        ,a.Quote_Bucket    
        ,a.Hist_Perc_Conv    
        ,a.Hist_Num_Quotes    
        ,a.Hist_Num_Quotes_Conv    
        ,a.buyerct_SVR    
        ,a.ave_quote_disc    
        ,a.ave_order_disc    
       from PC.Quote a left outer join salesteams b     
        on a.sales_rep=b.sales_team_number  left outer join SUK.CONTINFO c     
         on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT   
           
        where a.cusmerge_num= @Account  
  --         order by a.quote_doc_no desc   
 end    
  
   End Try  
 Begin Catch  
      SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_MESSAGE() AS ErrorMessage;  
 End catch 
   
-- EXECUTE sp_executesql @SQLQuery, N'@Campaign varchar(100),@Account varchar(10)', @Campaign ,@Account   
   

   
   
   
   
       
    /* Specify Parameter Format for all input parameters included     
     in the stmt */    
    --Set @ParamDefinition = ' @Account varchar(10),    
    --    @Campaign varchar(100)'    
            
    /* Execute the Transact-SQL String with all parameter value's     
       Using sp_executesql Command */    
   
 --EXEC(@SQLQuery)    
     
--    If @@ERROR <> 0 GoTo ErrorHandler    
--    Set NoCount OFF    
--    Return(0)    
        
--ErrorHandler:    
--    Return(@@ERROR)    
    
END    