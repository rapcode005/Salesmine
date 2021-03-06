USE [SASManila]
GO
/****** Object:  StoredProcedure [dbo].[SelectAccountChanges]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectAccountChanges]
	-- Add the parameters for the stored procedure here
	@Account varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
    -- Insert statements for procedure here
	Set @SqlQuery = 'Select sold_to,Createdby,createdon,AccountName,Phone,Fax,Address1,Address2,City,State,Zip,Country,Comment 
				 from Accounts_Chg where sold_to=' + @Account + ' order by createdon desc'
            
    Set @ParamDefinition = '@Account varchar(10)'
            
    PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
END
GO
/****** Object:  StoredProcedure [dbo].[SecondaryQuestion]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SecondaryQuestion]
	-- Add the parameters for the stored procedure here
		@Account varchar(10),
		@Campaign varchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
	if @Campaign <> 'PC'
	begin
		set @SqlQuery = 'Select a.sold_to, b.surname + '', '' + b.firstname as ''Name'', a.q1, a.q2, 
		a.q3,a.q4, a.q5,a.createdon,a.createdby from SECONDARY_QUESTIONS
		 a left join ' +  @Campaign + '.CONTINFO b on a.sold_to=b.customer and a.buyerct=b.contact ' +
		 ' where a.sold_to=' + @Account + ' and campaign=''' + @Campaign + ''' order by a.createdon'
	end
	
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(16)'
							
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
	
END
GO
/****** Object:  StoredProcedure [dbo].[Quotes]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Quotes] 
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

	If @Campaign <> 'PC'
	begin
		Set @SQLQuery = 'select a.Quote_Doc_No,a.Quote_Line,a.Quote_Date
						,a.Quote_Created_By,a.Quote_Create_Time,a.Quote_PO_Type,a.Quote_PO_Type_Desc,
						a.Quote_Item_Categ_Desc,a.Quote_Reason_Code,a.Quote_Reason_Code_Desc,a.Quote_Reason_Rej_Desc
						,a.Quote_SlsTeamIN,a.Quote_Material,a.Quote_Material_Desc,a.Quote_Mat_Entrd,
						a.Quote_Mat_Entrd_Desc,a.Product_Line_Desc,a.Product_Family_Desc,a.Quote_Coupon_CODE,
						a.Quote_Discount,a.Quote_Net_Sales,a.Quote_Freight,a.Quote_Cost,a.Order_Doc_No
						,a.Order_Line,a.Order_Date,a.Order_Createdby,a.Order_PO_Type_Desc,a.Order_Item_Categ_Desc
						,a.Order_Material_Desc,a.Order_Mat_Entrd_Desc,a.Order_Discount,a.Order_Net_Sales,a.Order_Freight
						,a.Order_Cost,a.Order_Refer_Doc,a.Order_Refer_Itm,a.Order_Coupon_CODE,b.sales_team_name,c.surname,
						 c.firstname,a.Quote_Doc_createdon,a.Quote_Doc_Create_Time,a.quote_sales_doc,a.quote_cost_doc
						 ,a.Quote_GM_percent,a.DM_Product_Line_Desc,a.Quote_Doc_Reason_Code_Desc,
						 a.Status,a.order_date_converted,a.order_sales_doc,
						 a.Order_GM_percent,a.tag,a.num_quotes_contact,a.num_quotes_conv_contact,a.percent_conversion_cont
						 ,a.num_quotes_site,a.num_quotes_conv_site,a.percent_conversion_site,a.Quote_Bucket,a.Hist_Perc_Conv,a.Hist_Num_Quotes,a.Hist_Num_Quotes_Conv,buyerct_SVR from ' +
						 @Campaign + '.Quote a left join salesteams b on a.sales_rep=b.sales_team_number ' +
						 ' left join ' + @Campaign + '.CONTINFO c on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT ' +
						 ' where a.cusmerge_num=' + @Account
	end 
	
	Set @SQLQuery = @SQLQuery + ' order by a.quote_doc_no desc'
	
	   /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition = ' @Account varchar(10),
							 @Campaign varchar(100)'
							 
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
  	PRINT @SQLQuery
	EXEC(@SQLQuery)
	
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
    
ErrorHandler:
    Return(@@ERROR)

END
GO
/****** Object:  StoredProcedure [dbo].[ProductLineSummary_Territory]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[ProductLineSummary_Territory]
    /* Input Parameters */
  (  @CampaignName varchar(100),
    @SoldTo Varchar(4000),
    @BuyerCt DECIMAL(18))
           
AS
BEGIN
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS Varchar(4000)
    Declare @ParamDefinition AS Varchar(2000) 
   
   IF @CampaignName = 'PC' 
		BEGIN
		
				Set @SQLQuery = 'SELECT sku_family,max(b.revision_date) as last_revision_date, sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to = 0) or sold_to in (' + @SoldTo + ')' 
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_family order by total_sales desc'
			         
				
				
		   END
    ELSE
		   BEGIN
				Set @SQLQuery = 'SELECT sku_category ,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From  ' +  @CampaignName +'.SKU_VIEW where  (sold_to = 0) or sold_to in (' + @SoldTo + ')'
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_category order by total_sales desc'
			         
		   END
		   
		   
   			
			         
	   
       Set @ParamDefinition =      ' @CampaignName char(10),
											  @SoldTo varchar(4000),
												@BUYERCT DECIMAL(18)'
PRINT @SQLQuery
EXEC(@SQLQuery)

--Execute sp_Executesql     @SQLQuery, 
--                @ParamDefinition, 
--				@CampaignName,
--                @SoldTo, 
--				@BUYERCT										
   
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
    end
GO
/****** Object:  StoredProcedure [dbo].[OrderHistory]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[OrderHistory] 
	-- Add the parameters for the stored procedure here
	@Account varchar(10),@CampaignName varchar(100)
	--@Account varchar(10),@Contact varchar(10),@StartDate varchar(12)
	--,@EndDate varchar(12),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)

    IF @CampaignName = 'PC' 
    Begin
		Set @SQLQuery = 'select cusmerge as [Account Number],itemcreatedon as [Order Date],mat_entrd as [Part Number],mat_entrd_txt as [Description]
												,''$'' + CONVERT(varchar(12), CAST(subtot_oc4/NULLIF(cml_or_qty,0) as money), 1) AS [Unit Price] ,CAST(cml_or_qty as int)as [QTY],subtot_oc4 as [Ext Price]
												,doc_number as [Order Number],s_ord_item as [Line],doc_type_txt as [Order Type], CONVERT(VARCHAR(12), date_converted, 107) as [Converted Date],REASON_REJ as [Reason Rejection],del_block as [Order Block],bstkd as [Customer PO],name as [Customer Name],customer as [Orig Account],buyerct as [Contact Number],surname as [Last Name],firstname as [First Name],email_addr as [Email Address],phone as [Phone],ship_name as [Ship Name],ship_ext_st_2 as [Ship Mailing],ship_city as [Ship City],ship_zip as [Ship Zip],ship_state as [Ship State],uvals from PC_orders where cusmerge_num=' + @Account
    end
    else
    begin
		Set @SQLQuery = 'select cusmerge as [Account Number],itemcreatedon as [Order Date],mat_entrd as [Part Number],mat_entrd_txt as [Description]
												,''$'' + CONVERT(varchar(12), CAST(subtot_oc4/NULLIF(cml_or_qty,0) as money), 1) AS [Unit Price] ,CAST(cml_or_qty as int)as [QTY],subtot_oc4 as [Ext Price]
												,doc_number as [Order Number],s_ord_item as [Line],doc_type_txt as [Order Type], CONVERT(VARCHAR(12), date_converted, 107) as [Converted Date],REASON_REJ as [Reason Rejection],del_block as [Order Block],bstkd as [Customer PO],name as [Customer Name],customer as [Orig Account],buyerct as [Contact Number],surname as [Last Name],firstname as [First Name],email_addr as [Email Address],phone as [Phone],ship_name as [Ship Name],ship_ext_st_2 as [Ship Mailing],ship_city as [Ship City],ship_zip as [Ship Zip],ship_state as [Ship State],uvals from ' + @CampaignName + '.orders where cusmerge_num=' +  @Account
	end
	
	--If @Contact <> 0
	--	Set @SQLQuery = @SQLQuery + ' and buyerct=' + @Contact
	--if @StartDate <> ''
	--	set @SQLQuery = @SQLQuery + ' and itemcreatedon between ''' + @StartDate + ''' and ''' + @EndDate + ''''
	
	set @SQLQuery = @SQLQuery + ' order by itemcreatedon desc, doc_number, s_ord_item '
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
END
GO
/****** Object:  StoredProcedure [dbo].[NotesCommHistTerritory]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NotesCommHistTerritory]
	@ACCOUNT nvarchar(10),
	@UNIT nvarchar(10)
AS
BEGIN
	 Set NoCount ON
	--DECLARE @ACCOUNT AS nvarchar(10)
	--DECLARE @UNIT AS nvarchar(10)
	Declare @ParamDefinition AS Varchar(2000) 
    Declare @SQLQuery AS NVarchar(4000)
	--	SET @ACCOUNT='0000001074'
	--	SET @UNIT='EMED'
    set @SQLQuery = 'SELECT createdon as [Created_on],createdby as [Created_by],upper([type]) as [Note_Type],notes as [Notes],date as [Date]
				,customer as [Account_Number],contact as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] 
				From ' + @UNIT + '.ALL_NOTES  where customer in ( select SOLD_TO from EMED.SITEINFO where SOLD_TO <> '''' AND salesteam=''EMED022'') UNION 
				select CALDAY as [Created_on], SLTEAMIN as [Created_by], 
				upper(Note_type) as [Note_Type], NOTES as [Notes],null as [Date],customer as [Account_Number],contact as [Contact_Number],a.DISP_CD as [Disposition_Code],
				DESCRIPTION from DISP_HISTORY a left join disp_desc b on (a.DISP_CD=b.Code)  
				where customer in ( select SOLD_TO from EMED.SITEINFO where SOLD_TO <> '''' AND salesteam=''EMED022'') and UNIT = ''' + @UNIT + ''' 
				UNION 
				select createdon as [Created_on],createdby as [Created_by],''PROJECT'' as [Note_Type], 
				(''Project Type:'' + RTRIM(LTRIM(project_type)) 
				+ ''  '' + ''Chance:'' + RTRIM(LTRIM(chance)) + ''  '' 
				+ ''Estimated Amount:'' + Cast(estimated_amt as varchar(50))) 
				as [Notes],Project_Date as [Date],Sold_to as [Account_Number],CONVERt(float,buyerct) 
				as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] from CUSTOMER_PROJECTS  
				where sold_to = ' + @ACCOUNT  + ' 
				or sold_to in ( select SOLD_TO from EMED.SITEINFO where SOLD_TO <> '''' AND salesteam=''EMED022'') order by createdon desc';
	--SELECT @SQLQuery
	
Set @ParamDefinition =      '@ACCOUNT nvarchar(10),
							@UNIT nvarchar(10)'
Execute(@SQLQuery)
PRINT @SQLQuery

   If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
END
GO
/****** Object:  StoredProcedure [dbo].[NotesCommHist_User]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NotesCommHist_User]
	@CREATEDBY nvarchar(10),
	@UNIT nvarchar(10)
AS
BEGIN
	 Set NoCount ON
	--DECLARE @CREATEDBYAS nvarchar(10)
	--DECLARE @UNIT AS nvarchar(10)
	Declare @ParamDefinition AS Varchar(2000) 
    Declare @SQLQuery AS NVarchar(4000)
	--	SET @ACCOUNT='0000001074'
	--	SET @UNIT='EMED'
    set @SQLQuery = 'SELECT createdon as [Created_on],createdby as [Created_by],upper([type]) as [Note_Type],notes as [Notes],date as [Date]
				,customer as [Account_Number],contact as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] 
				From ' + @UNIT + '.ALL_NOTES  where CREATEDBY = '''+ @CREATEDBY+ 
				''' UNION 
				select createdon as [Created_on],createdby as [Created_by],''PROJECT'' as [Note_Type], 
				(''Project Type:'' + RTRIM(LTRIM(project_type)) 
				+ ''  '' + ''Chance:'' + RTRIM(LTRIM(chance)) + ''  '' 
				+ ''Estimated Amount:'' + Cast(estimated_amt as varchar(50))) 
				as [Notes],Project_Date as [Date],Sold_to as [Account_Number],CONVERt(float,buyerct) 
				as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] from CUSTOMER_PROJECTS  
				where CREATEDBY = ''' + @CREATEDBY + ''' order by createdon desc';
	--SELECT @SQLQuery
	
Set @ParamDefinition =      '@CREATEDBY nvarchar(10),
							@UNIT nvarchar(10)'
Execute(@SQLQuery)
PRINT @SQLQuery

   If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
END
GO
/****** Object:  StoredProcedure [dbo].[NotesCommHist_AddNote]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[NotesCommHist_AddNote] 
    @CreatedBy varchar(10),
    @NoteType varchar(17),
    @Note varchar(500),
    @AccountNum varchar(10),
    @ContactNum varchar(10),
    @NoteDate varchar(20),
    @Campaign varchar(6),
    @Createdon varchar(35)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	
    Declare @SQLQuery AS NVarchar(4000)
	Set @SQLQuery = 'INSERT INTO ' + @Campaign + '.CAPTURED_NOTES(createdon,createdby,[type],notes,customer,contact,DATE) values(''' + @Createdon + ''',''' 
					 + @CreatedBy + ''',''' + @NoteType  + ''',''' + @Note + ''',' + @AccountNum + ',' + @ContactNum + ',''' + @NoteDate + ''')'				
		
	Execute(@SQLQuery)    
	
	
	-- Begin Return Select <- do not remove

	declare @SqlCheck as nvarchar(2000)
	set @SqlCheck = 'select * from ' + @Campaign + '.CAPTURED_NOTES WHERE [Createdon] = ''' + @Createdon + ''' and [Createdby]= ''' + @CreatedBy + ''''
	Execute(@SqlCheck)
	
    -- End Return Select <- do not remove           
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[NotesCommHist]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NotesCommHist]
	@ACCOUNT nvarchar(10),
	@UNIT nvarchar(10)
AS
BEGIN
	 Set NoCount ON
	--DECLARE @ACCOUNT AS nvarchar(10)
	--DECLARE @UNIT AS nvarchar(10)
	Declare @ParamDefinition AS Varchar(2000) 
    Declare @SQLQuery AS NVarchar(4000)
	--	SET @ACCOUNT='0000001074'
	--	SET @UNIT='EMED'
    set @SQLQuery = 'SELECT createdon as [Created_on],createdby as [Created_by],upper([type]) as [Note_Type],notes as [Notes],date as [Date]
				,customer as [Account_Number],contact as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] 
				From ' + @UNIT + '.ALL_NOTES  where customer = '''+ @ACCOUNT + 
				''' UNION 
				select CALDAY as [Created_on], SLTEAMIN as [Created_by], 
				upper(Note_type) as [Note_Type], NOTES as [Notes],null as [Date],customer as [Account_Number],contact as [Contact_Number],a.DISP_CD as [Disposition_Code],
				DESCRIPTION from DISP_HISTORY a left join disp_desc b on (a.DISP_CD=b.Code)  
				where customer = ''' + @ACCOUNT + ''' and UNIT = ''' + @UNIT + ''' 
				UNION 
				select createdon as [Created_on],createdby as [Created_by],''PROJECT'' as [Note_Type], 
				(''Project Type:'' + RTRIM(LTRIM(project_type)) 
				+ ''  '' + ''Chance:'' + RTRIM(LTRIM(chance)) + ''  '' 
				+ ''Estimated Amount:'' + Cast(estimated_amt as varchar(50))) 
				as [Notes],Project_Date as [Date],Sold_to as [Account_Number],CONVERt(float,buyerct) 
				as [Contact_Number],null as [Disposition_Code],null as [Disposition_Description] from CUSTOMER_PROJECTS  
				where sold_to = ' + @ACCOUNT  + ' order by createdon desc';
	--SELECT @SQLQuery
	
Set @ParamDefinition =      '@ACCOUNT nvarchar(10),
							@UNIT nvarchar(10)'
Execute(@SQLQuery)
PRINT @SQLQuery

   If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
END
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProducts]
	@WhereClause	nvarchar(4000),
	@SortExpression	nvarchar(128),
	@RowIndex		int,
	@NoOfRows		int
AS
BEGIN 

DECLARE @SQL nvarchar(4000)

IF (@WhereClause != '')
BEGIN
	SET @WhereClause = 'WHERE ' + char(13) + @WhereClause
END

IF (@SortExpression != '')
BEGIN
	SET @SortExpression = 'ORDER BY ' + @SortExpression
END

SET @SQL = 'WITH ProductRows AS (
						SELECT ROW_NUMBER() OVER ('+ @SortExpression +')AS Row,											
						[Id],
						[Name],
						[Description],
						[Unit],
						[UnitPrice],
						[CreateDate]
				FROM 
						[Product]	
				'+ @WhereClause +'
				)
				 SELECT
						[Id],
						[Name],
						[Description],
						[Unit],
						[UnitPrice],
						[CreateDate]
						FROM
							ProductRows
				WHERE 
						Row between '+ CONVERT(nvarchar(10), @RowIndex) +'And ('+ CONVERT(nvarchar(10), @RowIndex) +' + '+ CONVERT(nvarchar(10), @NoOfRows) +')'

EXEC sp_executesql @SQL

SET @SQL = 'SELECT COUNT([Id])
				FROM 
						[Product]
				' + @WhereClause

EXEC sp_executesql @SQL

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteColumnReorder]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteColumnReorder] 
    /* Input Parameters */
    @CAMPAIGN varchar(10),@USERNAME varchar(10),@LISTVIEW  Varchar(30)
    
      
           
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    
    
    set @SQLQuery = 'delete  from ' + @CAMPAIGN + '.Column_Reorder where username =''' +
						@USERNAME + ''' and listview = ''' + @LISTVIEW  + ''''
	 
    Set @ParamDefinition =      '  @CAMPAIGN char(10),
									@USERNAME char(10),
									@LISTVIEW char(30)'
    
   
    Execute sp_Executesql     @SQLQuery
               
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
GO
/****** Object:  StoredProcedure [dbo].[ContactLevel]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ContactLevel]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
	Set @SQLQuery = 'select distinct Cast(contmerg as varchar(10)) as contmerg, SURNAME + '', '' + FIRSTNAME as name,contact_type, rec_mo,Sales12M,
	CONTACT_STATUS,[Function],REP_CONTSTAT,REP_JOBAREA from 
	' + @CampaignName + '.CONTACTS where cusmerge=''' + @Account + ''' order by contmerg' 
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
END
GO
/****** Object:  StoredProcedure [dbo].[TRIMME]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TRIMME] 
    /* Input Parameters */
AS
BEGIN
DECLARE @SQL VARCHAR(MAX)

DECLARE @TableName NVARCHAR(128)
SET @TableName = 'EMED.Column_Reorder'

SELECT @SQL = COALESCE(@SQL + ',[', '[') + 
              COLUMN_NAME + ']=RTRIM([' + COLUMN_NAME + '])'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @TableName
    AND DATA_TYPE = 'varchar'

SET @SQL = 'UPDATE [' + @TableName + '] SET ' + @SQL
PRINT @SQL

END
GO
/****** Object:  StoredProcedure [dbo].[spTotalOrdersSales]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTotalOrdersSales]
	-- Add the parameters for the stored procedure here
		@Account varchar(10),@Contact varchar(10),@StartDate varchar(12)
		,@EndDate varchar(12),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
    
    IF @CampaignName = 'PC' 
    Begin
		Set @SQLQuery = 'select count(distinct doc_number) as total_orders,ISNULL(SUM(SUBTOT_OC4),0) as total_sales,itemcreatedon,BSTKD,DOC_NUMBER From PC_ORDERS whEre uvals=''C'' and REASON_REJ Is Null and doc_type_txt = ''Standard Order'' and cusmerge_num=' + @Account
    end
    else
    begin
		Set @SQLQuery = 'select count(distinct doc_number) as total_orders,ISNULL(SUM(SUBTOT_OC4),0) as total_sales,itemcreatedon,BSTKD,DOC_NUMBER From ' + @CampaignName + '.ORDERS whEre uvals=''C'' and REASON_REJ Is Null and doc_type_txt = ''Standard Order'' and cusmerge_num=' + @Account
	end
	
	If @Contact <> 0
		Set @SQLQuery = @SQLQuery + ' and buyerct=' + @Contact
	if @StartDate <> NULL
		set @SQLQuery = @SQLQuery + ' and itemcreatedon between ''' + @StartDate + ''' and ''' + @EndDate + ''''
	
	Set @ParamDefinition = '@Account float,
							@Contact float,
							@StartDate varchar(12)
							,@EndDate varchar(12),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
    
    
END
GO
/****** Object:  StoredProcedure [dbo].[spQuotes]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spQuotes] 
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

	If @Campaign <> 'PC'
	begin
		Set @SQLQuery = 'select a.Quote_Doc_No,a.Quote_Line,a.Quote_Date
						,a.Quote_Created_By,a.Quote_Create_Time,a.Quote_PO_Type,a.Quote_PO_Type_Desc,
						a.Quote_Item_Categ_Desc,a.Quote_Reason_Code,a.Quote_Reason_Code_Desc,a.Quote_Reason_Rej_Desc
						,a.Quote_SlsTeamIN,a.Quote_Material,a.Quote_Material_Desc,a.Quote_Mat_Entrd,
						a.Quote_Mat_Entrd_Desc,a.Product_Line_Desc,a.Product_Family_Desc,a.Quote_Coupon_CODE,
						a.Quote_Discount,a.Quote_Net_Sales,a.Quote_Freight,a.Quote_Cost,a.Order_Doc_No
						,a.Order_Line,a.Order_Date,a.Order_Createdby,a.Order_PO_Type_Desc,a.Order_Item_Categ_Desc
						,a.Order_Material_Desc,a.Order_Mat_Entrd_Desc,a.Order_Discount,a.Order_Net_Sales,a.Order_Freight
						,a.Order_Cost,a.Order_Refer_Doc,a.Order_Refer_Itm,a.Order_Coupon_CODE,b.sales_team_name,c.Surname,
						 c.Firstname,a.Quote_Doc_createdon,a.Quote_Doc_Create_Time,a.quote_sales_doc,a.quote_cost_doc
						 ,a.Quote_GM_percent,a.DM_Product_Line_Desc,a.Quote_Doc_Reason_Code_Desc,
						 a.Status,a.order_date_converted,a.order_sales_doc,
						 a.Order_GM_percent,a.tag,a.num_quotes_contact,a.num_quotes_conv_contact,a.percent_conversion_cont
						 ,a.num_quotes_site,a.num_quotes_conv_site,a.percent_conversion_site,a.Quote_Bucket,a.Hist_Perc_Conv,a.Hist_Num_Quotes,a.Hist_Num_Quotes_Conv,buyerct_SVR 
						 from ' +
						 @Campaign + '.Quote a left join salesteams b on a.sales_rep=b.sales_team_number ' +
						 ' left join ' + @Campaign + '.CONTINFO c on a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT ' +
						 ' where a.cusmerge_num=' + @Account
	end 
	
	Set @SQLQuery = @SQLQuery + ' order by a.quote_doc_no desc'
	
	   /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition = ' @Account varchar(10),
							 @Campaign varchar(100)'
							 
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
  	PRINT @SQLQuery
	EXEC(@SQLQuery)
	
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
    
ErrorHandler:
    Return(@@ERROR)

END
GO
/****** Object:  StoredProcedure [dbo].[spOrders]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spOrders] 
	-- Add the parameters for the stored procedure here
	@Account varchar(10),@CampaignName varchar(100)
	--@Account varchar(10),@Contact varchar(10),@StartDate varchar(12)
	--,@EndDate varchar(12),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)

    IF @CampaignName = 'PC' 
    Begin
		Set @SQLQuery = 'select cusmerge,itemcreatedon,mat_entrd,mat_entrd_txt
												,CAST(subtot_oc4/NULLIF(cml_or_qty,0) as money) as ''Unit Price'',cml_or_qty,subtot_oc4 
												,doc_number,s_ord_item,doc_type_txt, date_converted,REASON_REJ,del_block,bstkd,name,customer,buyerct,surname,firstname,email_addr,phone,ship_name,ship_ext_st_2,ship_city,ship_zip,ship_state,uvals from PC_orders where cusmerge_num=' + @Account
    end
    else
    begin
		Set @SQLQuery = 'select cusmerge,itemcreatedon,mat_entrd,mat_entrd_txt
												,CAST(subtot_oc4/NULLIF(cml_or_qty,0) as money) as ''Unit Price'',cml_or_qty,subtot_oc4 
												,doc_number,s_ord_item,doc_type_txt, date_converted,REASON_REJ,del_block,bstkd,name,customer,buyerct,surname,firstname,email_addr,phone,ship_name,ship_ext_st_2,ship_city,ship_zip,ship_state,uvals from ' + @CampaignName + '.orders where cusmerge_num=' +  @Account
	end
	
	--If @Contact <> 0
	--	Set @SQLQuery = @SQLQuery + ' and buyerct=' + @Contact
	--if @StartDate <> ''
	--	set @SQLQuery = @SQLQuery + ' and itemcreatedon between ''' + @StartDate + ''' and ''' + @EndDate + ''''
	
	set @SQLQuery = @SQLQuery + ' order by itemcreatedon desc, doc_number, s_ord_item '
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
			 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserSelect]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[sp_UserSelect]
    /* Input Parameters */
    @USERNAME varchar(100),
    @CAMPAIGN varchar(100)
           
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    
    Set @SQLQuery = 'select USERNAME,Campaign as CampaignName,KamId,KamName,valid_from as ValidFrom, valid_to as ValidTo,USERROLE from user_profile where 1=1 '
    /* check for the condition and build the WHERE clause accordingly */
    
    If @USERNAME Is Not Null
         Set @SQLQuery = @SQLQuery + ' And username LIKE '''+ '%' + @USERNAME + '%' + ''''  
  
    If @CAMPAIGN Is Not Null
          Set @SQLQuery = @SQLQuery + ' And CAMPAIGN LIKE '''+ '%' + @CAMPAIGN + '%' + '''' 
  
  Set @SQLQuery = @SQLQuery + ' order by username asc'
         
    /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition =      '  @USERNAME char(10),
									@CAMPAIGN char(10)'
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
    Execute sp_Executesql     @SQLQuery, 
                @ParamDefinition, 
                @USERNAME, 
                @CAMPAIGN
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
GO
/****** Object:  StoredProcedure [dbo].[sp_QuoteNumber]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[sp_QuoteNumber]
    /* Input Parameters */
    @Account float,
    @Contact float,
    @Campaign varchar(100)
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    
    If @Campaign <> 'PC'
    begin
		Set @SQLQuery = 'select a.*,b.*,c.surname,c.firstname from ' +
			@Campaign + '.Quote a left join salesteams b on ' + 
			'a.sales_rep=b.sales_team_number ' +
			' left join ' + @Campaign + '.CONTINFO c on ' +
			'a.sold_to_svr=c.CUSTOMER and a.buyerct_svr = c.CONTACT ' +
			' where a.cusmerge_num=' + cast(@Account as varchar(10))	
		If @Contact <> 0
			Set @SQLQuery = @SQLQuery + ' and buyerct=' + cast(@Contact as varchar(10))	
    end
    /* check for the condition and build the WHERE clause accordingly */
  
    Set @SQLQuery = @SQLQuery + ' order by a.quote_doc_no desc'
         
    /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition = ' @Account float,
							 @Contact float,
							 @Campaign varchar(100)'
							 
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
    Execute sp_Executesql   @SQLQuery, 
                @ParamDefinition, 
                @Account,
                @Contact, 
                @Campaign
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
    
ErrorHandler:
    Return(@@ERROR)
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductLineSummary]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[sp_ProductLineSummary]
    /* Input Parameters */
  (  @CampaignName varchar(100),
    @SoldTo Varchar(100),
    @BuyerCt DECIMAL(18))
           
AS
BEGIN
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS Varchar(4000)
    Declare @ParamDefinition AS Varchar(2000) 
   
   IF @CampaignName = 'PC' 
		BEGIN
		
				Set @SQLQuery = 'SELECT sku_family,max(b.revision_date) as last_revision_date, sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From PC.SKU_VIEW a left join PC.REVISION_DATE b on a.SKU_Number = b.mat_entered where (sold_to = ' + @SoldTo + ')' 
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_family order by total_sales desc'
			         
				
				
		   END
    ELSE
		   BEGIN
				Set @SQLQuery = 'SELECT sku_category ,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy From  ' +  @CampaignName +'.SKU_VIEW where (sold_to = ' + @SoldTo + ')' 
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_category order by total_sales desc'
			         
		   END
		   
		   
   			
			         
	   
       Set @ParamDefinition =      ' @CampaignName char(10),
											  @SoldTo char(10),
												@BUYERCT DECIMAL(18)'
PRINT @SQLQuery
EXEC(@SQLQuery)

--Execute sp_Executesql     @SQLQuery, 
--                @ParamDefinition, 
--				@CampaignName,
--                @SoldTo, 
--				@BUYERCT										
   
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
    end
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductLineSKUSummary]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[sp_ProductLineSKUSummary]
    /* Input Parameters */
    /* Input Parameters */
    @CampaignName varchar(100),
    @SoldTo Varchar(100),
    @SKUCATEGORY Varchar(100),
    @BuyerCt DECIMAL(18)
           
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    
    print @CampaignName
    IF @CampaignName = 'PC' 
		BEGIN
				Set @SQLQuery = 'SELECT SPACE_CODE,SKU_DESCRIPTION,SKU_NUMBER,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy,sku_number from ' +  @CampaignName +'.SKU_VIEW where (sold_to = ' + @SoldTo + ')' 
			
			If @SKUCATEGORY Is Not Null
					   Set @SQLQuery = @SQLQuery + ' And SKU_FAMILY = ''' + @SKUCATEGORY + ''''
					   
			If @BuyerCt Is Not Null
					   Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt) 
			
			Set @SQLQuery = @SQLQuery + ' group by SPACE_CODE,SKU_Description,sku_number,sku_category,sku_family order by SPACE_CODE desc'
        		
		   END
    ELSE
		   BEGIN
				 Set @SQLQuery = 'SELECT sku_category,sku_family,SKU_Description ,sum(sales_3fy_ago) as sales_3fy_ago,sum(sales_2fy_ago) as sales_2fy_ago,sum(sales_1fy_ago) as sales_1fy_ago,sum(sales_currfy) as sales_currfy,sum(Total_sales) as Total_sales,sum(NO_orders) as NO_orders,max(last_order_date) as last_order_date,sum(units_3fy_ago) as units_3fy_ago,sum(units_2fy_ago) as units_2fy_ago,sum(units_1fy_ago) as units_1fy_ago,sum(units_currfy) as units_currfy,sku_number from ' +  @CampaignName +'.SKU_VIEW where (sold_to = ' + @SoldTo + ')' 
			
			If @SKUCATEGORY Is Not Null
					   Set @SQLQuery = @SQLQuery + ' And SKU_CATEGORY = ''' + @SKUCATEGORY + ''''
					   
			If @BuyerCt Is Not Null
				     Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
  
			Set @SQLQuery = @SQLQuery + ' group by SKU_Description,sku_number,sku_category,sku_family order by Total_sales desc'
			         
		   END
    
   
    /* Specify Parameter Format for all input parameters included 
     in the stmt */
      Set @ParamDefinition =      ' @CampaignName char(10),
											  @SoldTo char(10),
											  @SKUCATEGORY char(50),
												@BUYERCT DECIMAL(18)'
PRINT @SQLQuery
EXEC(@SQLQuery)
GO
/****** Object:  StoredProcedure [dbo].[sp_KamWindow]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[sp_KamWindow]
    /* Input Parameters */
  (  @CampaignName varchar (100),
	 @SalesTeam varchar(100),
    @Name Varchar(100),
    @MgName Varchar(100))
           
AS
BEGIN
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS Varchar(4000)
    Declare @ParamDefinition AS Varchar(2000) 
   
   
		BEGIN
			print @SalesTeam
			Set @SQLQuery = 'Select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from ' + @CampaignName +'.SITEINFO where (SOLD_TO <> '''') and salesteam = ''' + @SalesTeam + '''' 
			
			print @SQLQuery
			
			
			 
			If @Name Is Not Null
			    Set @SQLQuery = @SQLQuery + ' and NAME like '''+ '%' + @Name + '%' + '''' 
		  
			   If @MgName Is Not Null
			  Set @SQLQuery = @SQLQuery + ' And MG_NAME LIKE '''+ '%' + @MgName + '%' + '''' 
			         
			  Set @SQLQuery = @SQLQuery + ' order by NAME asc'	
				
		   END

		   
   			
			         
	   
       Set @ParamDefinition =      ' @CampaignName char(10),
											  @SalesTeam varchar(10),
												@Name varchar(35),
												@MGName varchar(40)'
												
PRINT @SQLQuery
EXEC(@SQLQuery)

--Execute sp_Executesql     @SQLQuery, 
--                @ParamDefinition, 
--				@CampaignName,
--                @SoldTo, 
--				@BUYERCT										
   
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
    end
GO
/****** Object:  StoredProcedure [dbo].[sp_CustomerLookup]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_CustomerLookup]

@filterby nvarchar(max),
@filtertype nvarchar(max),
@filtertxt nvarchar(max)
as

Declare @lookUpQuery nvarchar(max)

set @lookUpQuery='select
	   [CONTACT_STATUS]
	  ,[cusmerge] as ACCOUNT_NUM
	  ,[NAME] as ACCOUNT_NAME
	  ,[MG_NAME] as MANAGE_GROUP
	  ,[BuyerOrg_Desc] as BUYER_ORG
	  ,[KAM_Mgr] as KAM_NAME
      ,[contmerg] as CONTACT_NUM
      ,[SURNAME] + '', '' + [FIRSTNAME] as CONTACT_NAME
      ,[contact_type] as CONTACT_TYPE
      ,[PHONE] as CONTACT_PHONE
      ,[rec_mo] as CONTACT_RECENCY
      ,[Sales12M] as CONTACT_SALES_12M
      ,[Function] as [FUNCTION]
      ,convert(nvarchar, [last_disp_dt],107) as LAST_DISP_DATE
      ,[NOTES] as LAST_DISP_NOTE
      ,convert(nvarchar, [lpdcont],107) as LAST_PURCHASE_DATE
      ,[EMAIL_ADDR] as EMAIL_ADDRESS
      ,[REP_CONTSTAT] as REP_CONTACT_STATUS
      ,[REP_JOBAREA] as REP_JOB_AREA
      
from ' + @filterby + '.CONTACTS Where '


if @filtertype='Customer Name'
	begin
 
 	set  @filtertype='name like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Phone Number'
	begin
	
	set  @filtertype='phone like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Email Address'
	begin
	
	set  @filtertype='email_addr like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Contact Name'
	begin
	
	set  @filtertype='surname + '', '' + firstname like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Managed Group'
	begin
	
	set  @filtertype='MG_Name like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Buyer Org'
	begin
	
	set  @filtertype='BuyerOrg_Desc like ''%'+ @filtertxt + '%'''

	end

set @lookUpQuery=@lookUpQuery + @filtertype

print @lookUpQuery
exec (@lookUpQuery)
GO
/****** Object:  StoredProcedure [dbo].[SiteContactInfo]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SiteContactInfo]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	Declare @budgetTag as NVarchar(50)
	Declare @NegBudgetTag as NVarchar(50)
	Declare @catTalogTag as NVarchar(50)
	
    -- Insert statements for procedure here
	if @CampaignName <> 'CL'
	begin
		
		if @CampaignName = 'PC'
		begin
			set @budgetTag = 'q4 as [Repdata Budget]'
			set @NegBudgetTag = 'q3 as ''q4q3'''
			set @catTalogTag = 'b.q13 as ''q91113'',b.q14 as ''q101214'''
			
			set @SQLQuery = 'select a.contmerg as [Contact Number] ,a.firstname as [First Name],a.surname as [Last Name],'''' as [Contact Type],'''' as [Contact Status], 
            B.q1 as [Repdata Contact Status],isnull(B.q2,a.title) as [Repdata Function],B.' +  @budgetTag + ',B.UPDATEDBY,B.VALID_FROM,B.VALID_TO,0 as [Recency],'''' as [Department],'''' as [Job Function],a.title as [Title],'''' as [Direct Phone],a.phone as [Site Phone],a.email_addr as [Email Address],
            '''' as [Do Not Mail],'''' as [Do Not Fax],'''' as [Do Not Email],'''' as [Do Not Call],0 as [Lifetime Sales],0 as [Last 12M Sales],0 as [Lifetime Orders], 0 as [last 12M Orders], null as [Last Purchased Date],c.[FSM],c.[FAE],c.[LBL], c.[LO],c.[OS], c.[PVM], c.[PPE], c.[PI], c.[SFS], 
            b.q5 as [SP],c.[SLS],c.[SCP], c.[SC],c.[TAGS],c.[TPS],c.[TC],c.[W],c.[ETO],c.updatedby as P_assign_updatedby,c.valid_from as P_assign_updatedon,b.' + @NegBudgetTag +
            ' ,b.q6,b.q7,b.q8,' + @catTalogTag + ',d.Createdby,d.Createdon,d.SuppressDetails,d.mail,d.fax,d.email,d.phone,d.comment, 
            '' '' as ''OHIE'','' '' as ''HSE'','' '' as ''ES'',
            '' '' as ''PC_update_by'','' '' as ''PC_update_on'',a.new_contact_key from ' + @CampaignName + '.captured_contacts a left join (select * from CONT_REPDATA where VALID_TO = ''9999-12-31'') b 
             on a.cusmerge=b.sold_to and a.new_contact_key=b.BUYERCT left join (select * from PRODUCT_PURCHASE where VALID_TO = ''12-31-9999'') c on a.CUSmerge = c.sold_to and a.new_contact_key = c.buyerct
            left join (select * from CustomerContactPre where ValidTo = ''12-31-9999'') d on a.CUSMERGE = d.sold_to and a.new_contact_key = d.buyerct 
             where a.cusmerge=' + @Account + ' Union All 
			SELECT A.contact as [Contact Number],A.FIRSTNAME as [First Name],A.SURNAME as [Last Name],A.CONTACT_TYPE as [Contact Type],a.CONTACT_STATUS as CONTACT_STATUS,B.q1 as [Repdata Contact Status],
            B.q2 as [Repdata Function],B.' + @budgetTag + ',B.UPDATEDBY,B.VALID_FROM,B.VALID_TO,A.REC_MO as [Recency],A.DEPARTMENT as [Department],a.[Function] as [Job Function],A.TITLE as [Title],A.PHONE_CONT as [Direct Phone],A.PHONE as [Site Phone],A.EMAIL_ADDR as [Email Address],
            A.CONT_MAIL_PREF as [Do Not Mail],A.CONT_FAX_PREF as [Do Not Fax],A.CONT_EMAIL_PREF as [Do Not Email],A.CONT_PHONE_PREF as [Do Not Call],A.LTSALES as [Lifetime Sales],A.SALES12M as [Last 12M Sales],
            A.LTORDERS as [Lifetime Orders],A.ORD12M as [last 12M Orders],A.LPDCONT as [Last Purchased Date],c.[FSM],
            c.[FAE], c.[LBL], c.[LO] , c.[OS] , c.[PVM], c.[PPE], c.[PI] , c.[SFS], b.q5 as [SP] , c.[SLS],
            c.[SCP], c.[SC] , c.[TAGS],c.[TPS], c.[TC] , c.[W]   ,c.[ETO],c.updatedby as P_assign_updatedby,c.valid_from as P_assign_updatedon,b.' + @NegBudgetTag +
            ',b.q6,b.q7,b.q8,' + @catTalogTag + ',d.Createdby,d.Createdon,
            d.SuppressDetails,d.mail,d.fax,d.email,d.phone,d.comment,
            e.q1 as ''OHIE'',e.q2 as ''HSE'',e.q3 as ''ES'',
            e.updatedby as ''PC_update_by'',e.valid_from as ''PC_update_on'',null as new_contact_key
             from ' + @CampaignName  + '.continfo A left join 
            (select * from CONT_REPDATA where VALID_TO=''12/31/9999'') b 
            on a.customer = b.SOLD_TO and a.contact=b.buyerct 
            left join (select * from PRODUCT_PURCHASE where VALID_TO = ''12-31-9999'') c on a.CUSTOMER = c.sold_to and a.CONTACT = c.buyerct 
            left join (select * from CustomerContactPre where ValidTo = ''12-31-9999'') d on a.CUSTOMER = d.sold_to and a.CONTACT = d.buyerct 
            left join (select * from PC.ACCT_REPDATA  where VALID_TO=''12/31/9999'' 
            and sold_to=' + @Account + ') e on a.Customer = e.sold_to where  a.customer=' + @Account
		end
		else
		begin
			set @NegBudgetTag = 'q4 as ''q4q3'''
			set @budgetTag = 'q3 as [Repdata Budget]'
			if @CampaignName = 'EMED'
			begin
				set @catTalogTag = 'b.q9 as ''q91113'',b.q10 as ''q101214'''
			end
			else if @CampaignName ='US'
			begin
				set @catTalogTag = 'b.q11 as ''q91113'',b.q12 as ''q101214'''
			end
			else
			begin
				set @catTalogTag = 'b.q13 as ''q91113'',b.q14 as ''q101214'''
			end 
			set @SQLQuery = 'select a.contmerg as [Contact Number] ,a.firstname as [First Name],a.surname as [Last Name],'''' as [Contact Type],'''' as [Contact Status], 
            B.q1 as [Repdata Contact Status],isnull(B.q2,a.title) as [Repdata Function],B.' +  @budgetTag + ',B.UPDATEDBY,B.VALID_FROM,B.VALID_TO,0 as [Recency],'''' as [Department],'''' as [Job Function],a.title as [Title],'''' as [Direct Phone],a.phone as [Site Phone],a.email_addr as [Email Address],
            '''' as [Do Not Mail],'''' as [Do Not Fax],'''' as [Do Not Email],'''' as [Do Not Call],0 as [Lifetime Sales],0 as [Last 12M Sales],0 as [Lifetime Orders], 0 as [last 12M Orders], null as [Last Purchased Date],c.[FSM],c.[FAE],c.[LBL], c.[LO],c.[OS], c.[PVM], c.[PPE], c.[PI], c.[SFS], 
            b.q5 as [SP],c.[SLS],c.[SCP], c.[SC],c.[TAGS],c.[TPS],c.[TC],c.[W],c.[ETO],c.updatedby as P_assign_updatedby,c.valid_from as P_assign_updatedon,b.' + @NegBudgetTag +
            ' ,b.q6,b.q7,b.q8,' + @catTalogTag + ',d.Createdby,d.Createdon,d.SuppressDetails,d.mail,d.fax,d.email,d.phone,d.comment, 
            '' '' as ''OHIE'','' '' as ''HSE'','' '' as ''ES'',
            '' '' as ''PC_update_by'','' '' as ''PC_update_on'',a.new_contact_key from ' + @CampaignName + '.captured_contacts a left join (select * from CONT_REPDATA where VALID_TO = ''9999-12-31'') b 
             on a.cusmerge=b.sold_to and a.new_contact_key=b.BUYERCT left join (select * from PRODUCT_PURCHASE where VALID_TO = ''9999-12-31'') c on a.CUSmerge = c.sold_to and a.new_contact_key = c.buyerct
            left join (select * from CustomerContactPre where ValidTo = ''12-31-9999'') d on a.CUSMERGE = d.sold_to and a.new_contact_key = d.buyerct 
             where a.cusmerge=' + @Account + ' Union All 
			 SELECT A.contact as [Contact Number],A.FIRSTNAME as [First Name],A.SURNAME as [Last Name],A.CONTACT_TYPE as [Contact Type],a.CONTACT_STATUS as [Contact Status],B.q1 as [Repdata Contact Status],
            B.q2 as [Repdata Function],B.' + @budgetTag + ',B.UPDATEDBY,B.VALID_FROM,B.VALID_TO,A.REC_MO as [Recency],A.DEPARTMENT as [Department],a.[Function] as [Job Function],A.TITLE as [Title],A.PHONE_CONT as [Direct Phone],A.PHONE as [Site Phone],A.EMAIL_ADDR as [Email Address],
            A.CONT_MAIL_PREF as [Do Not Mail],A.CONT_FAX_PREF as [Do Not Fax],A.CONT_EMAIL_PREF as [Do Not Email],A.CONT_PHONE_PREF as [Do Not Call],A.LTSALES as [Lifetime Sales],A.SALES12M as [Last 12M Sales],
            A.LTORDERS as [Lifetime Orders],A.ORD12M as [last 12M Orders],A.LPDCONT as [Last Purchased Date],c.[FSM],
            c.[FAE], c.[LBL], c.[LO] , c.[OS] , c.[PVM], c.[PPE], c.[PI] , c.[SFS], b.q5 as [SP] , c.[SLS],
            c.[SCP], c.[SC] , c.[TAGS],c.[TPS], c.[TC] , c.[W]   ,c.[ETO],c.UPDATEDBY as P_assign_updatedby,c.Valid_from as P_assign_updatedon,b.' + @NegBudgetTag +
            ',b.q6,b.q7,b.q8,' + @catTalogTag + ',d.Createdby,d.Createdon,d.SuppressDetails,d.mail,d.fax,d.email,d.phone,d.comment, 
            '' '' as ''OHIE'','' '' as ''HSE'','' '' as ''ES'',
            '' '' as ''PC_update_by'','' '' as ''PC_update_on'',null as new_contact_key from ' + @CampaignName  + '.continfo A left join 
            (select * from CONT_REPDATA where VALID_TO=''12/31/9999'') b 
            on a.customer = b.SOLD_TO and a.contact=b.buyerct 
            left join (select * from PRODUCT_PURCHASE where VALID_TO = ''9999-12-31'') c on a.CUSTOMER = c.sold_to and a.CONTACT = c.buyerct 
            left join (select * from CustomerContactPre where ValidTo = ''12-31-9999'') d on a.CUSTOMER = d.sold_to and a.CONTACT = d.buyerct 
            where  a.customer=' + @Account 
            
		end
		
		--a.notes,a.new_contact_key
	end
	else
	begin
		set @SQLQuery = 'SELECT A.contact,A.FIRSTNAME,A.SURNAME,A.CONTACT_TYPE,
		a.CONTACT_STATUS,'','','','','','',A.REC_MO,A.DEPARTMENT,a.[Function],
		A.TITLE,A.PHONE_CONT,A.PHONE,A.EMAIL_ADDR,A.CONT_MAIL_PREF,A.CONT_FAX_PREF,
		A.CONT_EMAIL_PREF,A.CONT_PHONE_PREF,A.LTSALES,A.SALES12M,
		A.LTORDERS,A.ORD12M,A.LPDCONT,'','','','','','','','','',
		'','','','','','','','','','','','','','','','','' ,'','','','','','','',''
		from CL_continfo A where a.customer=' + @Account
	end
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
	
END
GO
/****** Object:  StoredProcedure [dbo].[SelectSafetyProducts]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectSafetyProducts]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Campaign varchar(16) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
    -- Insert statements for procedure here
	
	set @SQLQuery = 'Select CONTACT,FIRSTNAME,SURNAME,Q5 from ' + @Campaign + '.CONTINFO a 
            left join (Select Q5,BUYERCT from cont_repdata where VALID_TO=''9999-12-31'') b on (a.CONTACT=b.BUYERCT)
            where CUSTOMER=' + @Account
	
	
	Set @ParamDefinition = '@Account varchar(10),
						@CampaignName varchar(16)'
							
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
	
	ErrorHandler:
    Return(@@ERROR)
	
END
GO
/****** Object:  StoredProcedure [dbo].[SelectProjects]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectProjects]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Campaign varchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
    -- Insert statements for procedure here
	set @SQLQuery = 'Select a.sold_to, b.surname + '', '' + b.firstname as name, a.PROJECT_DATE, a.PROJECT_TYPE, A.CHANCE, A.ESTIMATED_AMT, a.createdon,a.createdby from 
             CUSTOMER_PROJECTS a inner join ' + @Campaign + '.CONTINFO b on a.sold_to=b.customer and a.buyerct=b.contact
             where sold_to=' + @Account + ' union all Select a.sold_to,c.surname + '', '' + c.firstname as name,a.PROJECT_DATE, a.PROJECT_TYPE, A.CHANCE, A.ESTIMATED_AMT, a.createdon,a.createdby from
             CUSTOMER_PROJECTS a inner join ' + @Campaign +  '.Captured_Contacts c on a.buyerct = c.new_contact_key 
             where sold_to=' + @Account
			
	  Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(16)'
	
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPreferences]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectPreferences]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Campaign varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
    
    set @SQLQuery = 'select sold_to,[createdby],[createdon],Buyerct,ContactName,[Mail],[Fax],
			[Email],[Phone] from Preferences where sold_to=' + @Account + ' and Campaign=''' +
			@Campaign + ''' order by createdon desc'
			
	
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(16)'
	
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[SelectOtherVendors]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectOtherVendors] 
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Campaign varchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)

    -- Insert statements for procedure here
	Set @SqlQuery =  'Select a.sold_to, b.surname + '', '' + b.firstname as contactName, a.vendorname, a.other, a.createdon,a.createdby from 
             OTHER_VENDORS a inner join ' +  @Campaign + '.CONTINFO b on a.sold_to=b.customer and a.buyerct=b.contact
             where sold_to =' + @Account + ' Union All 
             Select a.sold_to, b.surname + '', '' + b.firstname as contactName,  a.vendorname, a.other, a.createdon,a.createdby from 
             OTHER_VENDORS a inner join ' + @Campaign + '.Captured_Contacts b on a.buyerct=b.new_contact_key 
             where sold_to =' + @Account
             
     Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(16)'
							
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
END
GO
/****** Object:  StoredProcedure [dbo].[SelectContactChanges]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectContactChanges]
	-- Add the parameters for the stored procedure here
	@Account varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
	
	Set @SqlQuery = 'Select Sold_to,[Createdby],[Createdon],[Buyerct],[Firstname],[Lastname],[Status],[Function],[Title],[Phone],[PhoneExt],[Department]
                    ,[Email_Address],comment FROM [Contacts_Chg] where sold_to=' + @Account + ' order by createdon desc'
	
	Set @ParamDefinition = '@Account varchar(10)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)

    -- Insert statements for procedure here
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Kam_SelectAccountNo]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
CREATE Procedure [dbo].[usp_Kam_SelectAccountNo]
    /* Input Parameters */
  (  @CampaignName varchar (100),
     @SalesTeam varchar(100)
	 )
           
AS
BEGIN
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS Varchar(4000)
    Declare @ParamDefinition AS Varchar(2000) 
   
   
		BEGIN
			print @SalesTeam
			Set @SQLQuery = 'Select SOLD_TO from ' + @CampaignName +'.SITEINFO where (SOLD_TO <> '''') and salesteam = ''' + @SalesTeam + '''' 
			
			print @SQLQuery
			END
       Set @ParamDefinition =      ' @CampaignName char(10),
									 @SalesTeam varchar(10)'
												
PRINT @SQLQuery
EXEC(@SQLQuery)

--Execute sp_Executesql     @SQLQuery, 
--                @ParamDefinition, 
--				@CampaignName,
--                @SoldTo, 
--				@BUYERCT										
   
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
    end
GO
/****** Object:  StoredProcedure [dbo].[spGetRecords]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetRecords]
	@campaign varchar(50)
AS
BEGIN
	IF @campaign = 'US'
		BEGIN
			SELECT * FROM US_CONTACTS 
		END
	IF @campaign = 'CANADA'
		BEGIN
			SELECT * FROM  CA_CONTACTS 
		END
	IF @campaign = 'Emedco'
		BEGIN
			SELECT * FROM EMED_CONTACTS  
		END
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILESelect]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILESelect] 
    /* Input Parameters */
    @USERNAME NVarchar(100),
    @CAMPAIGN  NVarchar(100),
    @VALID_TO datetime,
    @KamId  NVarchar(100),
    @KamName  NVarchar(100)
           
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    /* Build the Transact-SQL String with the input parameters */ 
    Set @SQLQuery = 'SELECT  * FROM USER_PROFILE WHERE(1=1) '
    /* check for the condition and build the WHERE clause accordingly */
    
    
    If @USERNAME Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (USERNAME = @USERNAME)' 
  
    If @VALID_TO Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (VALID_TO = @VALID_TO)'
  
    If @KamName Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (KamName = @KamName)'
   
         
    If @KamId Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (KamId = @KamId)'
         
    If @KamName Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (KamName = @KamName)'
         
    /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition =      '  @USERNAME char(10),
									@CAMPAIGN char(10),
									@VALID_TO datetime,
									@KamId char(12),
									@KamName char(64)'
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
    Execute sp_Executesql     @SQLQuery, 
                @ParamDefinition, 
                @USERNAME, 
				@CAMPAIGN, 
                @VALID_TO, 
                @KamId,
                @KamName
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Acct_Info]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Acct_Info]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),@CampaignName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)

    -- Insert statements for procedure here
	set @SQLQuery = 'select * from ' + @CampaignName + '.siteinfo where sold_to=' +
						@Account
	Set @ParamDefinition = '@Account varchar(10),
							@CampaignName varchar(100)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILEUpdate]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILEUpdate] 
    @USERNAME char(10),
    @CAMPAIGN char(10),
   
    @KamId char(12),
    @KamName char(64),
    @USERROLE char(10),
    @CREATEDEDITEDBY char(10) 
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	DECLARE @UpdateUserID Int
	
	update [dbo].[USER_PROFILE] set [VALID_TO] = GETDATE(),[Created_Edited_By] = @CREATEDEDITEDBY where [USERNAME] = @USERNAME and  VALID_TO = '9999-12-31 00:00:00.000'
	
	 INSERT INTO [dbo].[USER_PROFILE] ([USERNAME], [CAMPAIGN], [VALID_FROM], [VALID_TO], [admPass], [KamId], [KamName], [USERROLE], [CreatedDate],[Created_Edited_By])
	SELECT @USERNAME, @CAMPAIGN, GETDATE(), '12/31/9999', '', @KamId, @KamName, @USERROLE, GETDATE(),@CREATEDEDITEDBY	
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [USERNAME], [CAMPAIGN], [VALID_FROM], [VALID_TO], [admPass], [KamId], [KamName], [USERROLE],[CreatedDate],[Created_Edited_By] 
	FROM   [dbo].[USER_PROFILE]
	WHERE  [UserID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILESelectRole]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILESelectRole] 
   
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
		select distinct RoleName,RoleID from UserRoles  WHERE 1=1

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILESelectCampaign]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILESelectCampaign] 
   
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
		select distinct campaignName=
case when campaign = 'CA' then 'Seton Canada'
 when campaign = 'EMED' then 'Emedco'
 when campaign = 'US' then 'Seton US'
 when campaign = 'PC' then 'Personal Concepts'
end

,
campaignValue=
case when campaign = 'CA' then 'CA'
 when campaign = 'EMED' then 'EMED'
 when campaign = 'US' then 'US'
 when campaign = 'PC' then 'PC'
end

 from USER_PROFILE  WHERE 1=1 and campaign <> 'Admin'

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILEInsert]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILEInsert] 
    @USERNAME char(10),
    @CAMPAIGN char(10),
   
    @KamId char(12),
    @KamName char(64),
    @USERROLE char(10),
    @CREATEDEDITEDBY char(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[USER_PROFILE] ([USERNAME], [CAMPAIGN], [VALID_FROM], [VALID_TO], [admPass], [KamId], [KamName], [USERROLE], [CreatedDate],[Created_Edited_By])
	SELECT @USERNAME, @CAMPAIGN, GETDATE(), '12/31/9999', '', @KamId, @KamName, @USERROLE, GETDATE(),@CREATEDEDITEDBY
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [USERNAME], [CAMPAIGN], [VALID_FROM], [VALID_TO], [admPass], [KamId], [KamName], [USERROLE],[CreatedDate],[Created_Edited_By] 
	FROM   [dbo].[USER_PROFILE]
	WHERE  [UserID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILEDelete]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_USER_PROFILEDelete] 
    @UserName char(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[USER_PROFILE]
	WHERE  UPPER(UserName) = @UserName

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_testmeSelect]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_testmeSelect] 
   
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [name] 
	FROM   [dbo].[testme] 
	

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_testmeDelete]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_testmeDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[testme]
	WHERE  [id] = @id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UpdateSafetyProducts]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSafetyProducts]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Contact int,
	@SP int,
	@Username varchar(16)
	
	
AS
BEGIN
	BEGIN TRAN
	Declare @Count as Numeric(18,0)
	
	Select @Count = COUNT(*) from cont_repdata where valid_to = '12-31-9999'
	and SOLD_TO=@Account and BUYERCT=@Contact
	
	if @Count > 0
	begin
		update CONT_REPDATA SET Q5=@SP where valid_to = '12-31-9999'
		and BUYERCT =@Contact and SOLD_TO= @Account
	end
	else
	begin
		insert into CONT_REPDATA(SOLD_TO,BUYERCT,valid_from,valid_to,q5,UPDATEDBY)
		values(@Account,@Contact,GETDATE(),'12-31-9999',@SP,@Username)
	end
	
	commit
	
	SET NOCOUNT OFF;
	Select * from CONT_REPDATA where SOLD_TO=@Account and BUYERCT=@Contact
	and VALID_TO='12-31-9999'
    -- Insert statements for procedure here
	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateQQPC]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateQQPC]
	@Account varchar(10),
	@Contact int,
	@Campaign varchar(100),
	@Spanish varchar(32),
	@EmployeeSize varchar(32),
	@health varchar(32),
	@Username varchar(10),
	@ContStats varchar(64),
	@ContFunction varchar(64),
	@ContBudgets varchar(64),
	@Qx varchar(64)
	
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	BEGIN TRAN
	
	Update PC.ACCT_REPDATA set VALID_TO = GETDATE() where
	SOLD_TO = @Account and VALID_TO = '12/31/9999'
	
	insert into PC.ACCT_REPDATA(Sold_to,q1,q2,q3,updatedby,
	valid_from,valid_to) values(@Account,@health,@Spanish,
	@EmployeeSize,@Username,GETDATE(),'12/31/9999')
	
	update [dbo].cont_repdata set Valid_to = GETDATE() where 
	sold_to = @Account and BUYERCT = @Contact
	
	Insert into [dbo].cont_repdata(SOLD_TO,BUYERCT,q1,q2,q4,
	q3,UPDATEDBY,VALID_FROM,VALID_TO) values(@Account,@Contact,
	@ContStats,@ContFunction,@ContBudgets,@Qx,@Username,GETDATE(),
	'12/31/9999')
	
	COMMIT
	
	Set NoCount OFF;
	Select * from cont_repdata where SOLD_TO=@Account and BUYERCT=@Contact and
		Valid_to ='12-31-9999'

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateQQ]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateQQ]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Contact int,
	@Function varchar(25),
	@ContactStatus varchar(25),
	@Purchasing varchar(10),
	@ContBudget varchar(20),
	@Factor varchar(10),
	@SpVendor int,
	@Sp int,
	@Username varchar(10),
	@Other varchar(300),
	@Campaign varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET XACT_ABORT ON  

    -- Insert statements for procedure here
	BEGIN TRAN
	
	update [dbo].cont_repdata set Valid_to = GETDATE() where 
	sold_to = @Account and BUYERCT = @Contact
	
		if @Campaign = 'EMED'
		begin
			INSERT INTO [dbo].cont_repdata(SOLD_TO,BUYERCT,UPDATEDBY,VALID_FROM,VALID_TO,
			q1,q2,q5,q3,q6,q7,q8,q9) 
			values(@Account,@Contact,@Username,GETDATE(),'12-31-9999',@ContactStatus,
			@Function,@Sp,@ContBudget,@Factor,@Other,@Purchasing,@SpVendor)
		end
		else if @Campaign ='US'
		begin
			INSERT INTO [dbo].cont_repdata(SOLD_TO,BUYERCT,UPDATEDBY,VALID_FROM,VALID_TO,
			q1,q2,q5,q3,q6,q7,q8,q11) 
			values(@Account,@Contact,@Username,GETDATE(),'12-31-9999',@ContactStatus,
			@Function,@Sp,@ContBudget,@Factor,@Other,@Purchasing,@SpVendor)
		end
		else if @Campaign = 'CA'
		begin
			INSERT INTO [dbo].cont_repdata(SOLD_TO,BUYERCT,UPDATEDBY,VALID_FROM,VALID_TO,
			q1,q2,q5,q3,q6,q7,q8,q13) 
			values(@Account,@Contact,@Username,GETDATE(),'12-31-9999',@ContactStatus,
			@Function,@Sp,@ContBudget,@Factor,@Other,@Purchasing,@SpVendor)
		end
		
		Insert into Contacts_Chg(Status,Sold_to,Buyerct,[Createdon],[Createdby],[Function]) 
		Values(@ContactStatus,@Account,@Contact,GETDATE(),@Username,@Function)

		COMMIT
		Set NoCount OFF
		-- Begin Return Select <- do not remove
		Select * from cont_repdata where SOLD_TO=@Account and BUYERCT=@Contact and
		Valid_to ='12-31-9999'
		-- End Return Select <- do not remove
		
		
		
		
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProducts]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProducts]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Contact int,
	@FSM int,
	@FAE int,
	@LBL int,
	@LO int,
	@OS int,
	@PVM int,
	@PPE int,
	@PI int,
	@SFS int,
	@SP int,
	@SLS int,
	@SCP int,
	@SC int,
	@TAGS int,
	@TPS int,
	@TC int,
	@W int,
	@ETO int,
	@Username varchar(10)
AS
BEGIN
	BEGIN TRAN
	
	Declare @Count as Numeric(18,0)
	
	Update PRODUCT_PURCHASE set valid_to= getdate() Where sold_to=@Account and
	buyerct= @Contact and Valid_to = '12/31/9999'
	
	insert into PRODUCT_PURCHASE(sold_to,buyerct,FSM,FAE,LBL,LO,OS,
	PVM,PPE,[PI],SFS,SP,SLS,SCP,SC,TAGS,TPS,TC,W,ETO,UPDATEDBY,
	VALID_FROM,VALID_TO)
	
	values(@Account,@Contact,@FSM,@FAE,@LBL,
	@LO,@OS,@PVM,@PPE,@PI,@SFS,@SP,@SLS,@SCP,@SC,@TAGS,@TPS,@TC,@W,@ETO,
	@Username,GETDATE(),'12/31/9999')
	
	---Adding Tag if already update to Qualifying Question
	
	Select @Count = COUNT(*) from cont_repdata where
	VALID_TO = '12-31-9999' and SOLD_TO = @Account and
	BUYERCT = @Contact
	
	if @Count > 0 
	begin
		Update CONT_REPDATA SET Q5=@SP where valid_to = '12-31-9999' and SOLD_TO =@Account
                 AND buyerct = @Contact
	end
	else
	begin
		insert into cont_repdata(SOLD_TO,buyerct,valid_from,valid_to,Q5)
		values(@Account,@Contact,GETDATE(),'12-31-9999',@SP)
	end
	
	commit
	SET NOCOUNT OFF;
	
	select * from PRODUCT_PURCHASE where sold_to=@Account and buyerct=@Contact
	and VALID_TO ='12/31/9999';
    -- Insert statements for procedure here
	
END
GO
/****** Object:  StoredProcedure [dbo].[SelectColumnReorder]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectColumnReorder] 
    /* Input Parameters */
    @CAMPAIGN varchar(10),@USERNAME varchar(10),@LISTVIEW  Varchar(30)
    
      
           
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    
    
    set @SQLQuery = 'select * from ' + @CAMPAIGN + '.Column_Reorder where username =''' +
						@USERNAME + ''' and listview = ''' + @LISTVIEW  + ''''
	  Set @SQLQuery = @SQLQuery + ' order by Position '			
  
    Set @ParamDefinition =      '  @CAMPAIGN char(10),
									@USERNAME char(10),
									@LISTVIEW char(30)'
    
   
    Execute sp_Executesql     @SQLQuery
               
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)



select * from EMED.Column_Reorder where Username='PorrasJo' and Listview='lvwData'
GO
/****** Object:  StoredProcedure [dbo].[AddSecondaryQuestion]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddSecondaryQuestion]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Contact int,
	@Campaign varchar(12),
	@Q1 varchar(128),
	@Q2 varchar(128),
	@Q3 varchar(128),
	@Q4 varchar(128),
	@Q5 varchar(128),
	@Username varchar(16)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	BEGIN TRAN
	
	insert into SECONDARY_QUESTIONS values(@Account,@Contact,
	@Campaign,@Q1,@Q2,@Q3,@Q4,@Q5,GETDATE(),@Username)
	
	commit
	
	Set NoCount OFF;
	Select * from SECONDARY_QUESTIONS where SOLD_TO = @Account and
 	BUYERCT = @Contact;
END
GO
/****** Object:  StoredProcedure [dbo].[AddProjects]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddProjects]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Contact int,
	@Date DateTime,
	@ProjectType varchar(128),
	@Chance varchar(12),
	@EstimatedAmt int,
	@Username varchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	BEGIN TRAN
	
	insert into CUSTOMER_PROJECTS values(@Account,@Contact,@Date,
	@ProjectType,@Chance,@EstimatedAmt,GETDATE(),@Username)
	
	commit
	
	SET NOCOUNT OFF;
	Select * from CUSTOMER_PROJECTS where SOLD_TO=@Account and BUYERCT=
	@Contact and CREATEDON=GETDATE()

END
GO
/****** Object:  StoredProcedure [dbo].[AddPreferences]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddPreferences]
	-- Add the parameters for the stored procedure here
	@Username varchar(16),
	@Account varchar(10),
	@Contact int,
	@Phone varchar(100),
	@ContactName varchar(100),
	@Campaign varchar(16),
	@Mail varchar(100),
	@Fax varchar(100),
	@Email varchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	begin tran
	
	insert into Preferences Values(@Username,GETDATE(),@Account,@Contact,
	@Mail,@Fax,@Email,@Phone,@ContactName,@Campaign)
	
	commit
	
	SET NOCOUNT OFF;

    Select * From Preferences where sold_to=@Account and buyerct = @Contact and
     createdby = @Username
    
END
GO
/****** Object:  StoredProcedure [dbo].[AddOtherVendors]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddOtherVendors]
		@Account varchar(10),
		@Contact int,
		@Name varchar(64),
		@Other varchar(128),
		@Username varchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    -- Insert statements for procedure here
	BEGIN TRAN
	
	insert into OTHER_VENDORS values(@Account,@Contact,@Name,@Other,GETDATE(),
	@Username)
	
	commit
	
	Set NoCount OFF;
	Select * from OTHER_VENDORS where SOLD_TO=@Account and BUYERCT=
	@Contact and CREATEDON=GETDATE()
	
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewContact]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddNewContact] 
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Campaign varchar(16),
	@Firstname varchar(35),
	@Surname varchar(35),
	@Title varchar(35),
	@Email varchar(60),
	@Notes varchar(256),
	@Createdby varchar(12),
	@Name varchar(35),
	@Phone varchar(12)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	BEGIN TRAN
	Declare @MaxValue as Numeric(18,0)
	Declare @Count as Numeric(18,0)
	
	if @Campaign = 'EMED'
	begin
		
		Select @Count = COUNT(*) from EMED.Captured_Contacts where
		FIRSTNAME = @Firstname and SURNAME = @Surname
		
		if @Count = 0
		begin
			 Select @MaxValue= MAX(new_contact_key) from 
			 EMED.Captured_Contacts group by new_contact_key
			 
			 Insert into EMED.CAPTURED_CONTACTS VALUES(@Account,0,@Firstname,@Surname,
			@Title,@Email,@Notes,@Createdby,GETDATE(),@Name,@Phone,@MaxValue+1)
		end
		
	end
	else if @Campaign = 'PC'
	begin
		
		Select @Count = COUNT(*) from PC.Captured_Contacts where
		FIRSTNAME = @Firstname and SURNAME = @Surname
		
		if @Count = 0
		begin
	
		 Select @MaxValue= MAX(new_contact_key) from 
		 PC.Captured_Contacts group by new_contact_key
		 
		 Insert into PC.CAPTURED_CONTACTS VALUES(@Account,0,@Firstname,@Surname,
		@Title,@Email,@Notes,@Createdby,GETDATE(),@Name,@Phone,@MaxValue+1)
		
		end
	end
	else if @Campaign = 'CA'
	begin
		
		Select @Count = COUNT(*) from CA.CAPTURED_CONTACTS where
		FIRSTNAME = @Firstname and SURNAME = @Surname
		
		if @Count = 0
		begin
	
			 Select @MaxValue= MAX(new_contact_key) from 
			 CA.Captured_Contacts group by new_contact_key
			 
			 Insert into CA.CAPTURED_CONTACTS VALUES(@Account,0,@Firstname,@Surname,
			@Title,@Email,@Notes,@Createdby,GETDATE(),@Name,@Phone,@MaxValue+1)
		
		end
	end
	else if @Campaign = 'US'
	begin
		
		Select @Count = COUNT(*) from CA.CAPTURED_CONTACTS where
		FIRSTNAME = @Firstname and SURNAME = @Surname
		
		if @Count = 0
		begin
	
			 Select @MaxValue= MAX(new_contact_key) from 
			 US.Captured_Contacts group by new_contact_key
		 
		 	Insert into US.CAPTURED_CONTACTS VALUES(@Account,0,@Firstname,@Surname,
			@Title,@Email,@Notes,@Createdby,GETDATE(),@Name,@Phone,@MaxValue+1)
		
		end
		
	end
	
	commit
	
	SET NOCOUNT OFF;
	if @Count = 0
	begin
			if @Campaign = 'EMED'
			begin
				Select * from EMED.Captured_Contacts where new_contact_key = (@MaxValue + 1)
			end
			else if @Campaign ='PC'
			begin
				Select * from PC.Captured_Contacts where new_contact_key = (@MaxValue + 1)
			end
			else if @Campaign ='CA'
			begin
				Select * from CA.Captured_Contacts where new_contact_key = (@MaxValue + 1)
			end
			else if @Campaign ='US'
			begin
			Select * from US.Captured_Contacts where new_contact_key = (@MaxValue + 1)
			end
	end
	
    -- Insert statements for procedure here

	
END
GO
/****** Object:  StoredProcedure [dbo].[AddContactChanges]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddContactChanges]
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Firstname varchar(100),
	@Lastname varchar(100),
	@Status varchar(30),
	@Function varchar(25),
	@Title varchar(100),
	@Phone varchar(20),
	@PhoneExt varchar(30),
	@Department varchar(50),
	@Email varchar(50),
	@Username varchar(10),
	@Contact int,
	@Comment varchar(256)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	begin tran
	
	Insert into Contacts_Chg Values(@Firstname,@Lastname,@Status,@Function,@Title,@Phone,@PhoneExt,
	@Department,@Email,@Username,GETDATE(),@Account,@Contact,@Comment)
	
	commit
	
	SET NOCOUNT OFF;
	Select * from Contacts_Chg where Sold_to=@Account and Buyerct=@Contact and Createdon=GETDATE()
	
    -- Insert statements for procedure here
	
END
GO
/****** Object:  StoredProcedure [dbo].[AddAccountChanges]    Script Date: 03/20/2012 16:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddAccountChanges] 
	-- Add the parameters for the stored procedure here
	@Account varchar(10),
	@Phone varchar(16),
	@Fax varchar(50),
	@City varchar(100),
	@State varchar(100),
	@Zip varchar(20),
	@Country varchar(100),
	@Address1 varchar(500),
	@Address2 varchar(500),
	@AccountName varchar(100),
	@Username varchar(10),
	@Comment varchar(256)
AS
BEGIN
	begin tran
	
		insert into Accounts_Chg values(@Account,@Phone,@Fax,@City,@State,@Zip,@Country
		,@Address1,@Address2,@AccountName,@Username,GETDATE(),@Comment)
	
	commit
	
	SET NOCOUNT OFF;

    Select * From Accounts_Chg where sold_to=@Account and 
     createdby = @Username and Createdon = GETDATE()
	
END
GO
