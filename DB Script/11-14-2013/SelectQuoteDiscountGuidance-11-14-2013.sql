USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[SelectQuoteDiscountGuidance]    Script Date: 11/14/2013 20:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*
Usage : [dbo].[SelectQuoteDiscountGuidance] @Account = '000001076819' , @Campaign = 'EMED' , 
[dbo].[SelectQuoteDiscountGuidance] @Doc_Number = '0021878542' , @Campaign = 'EMED'
*/
ALTER PROCEDURE [dbo].[SelectQuoteDiscountGuidance]
	-- Add the parameters for the stored procedure here
	@CustomerType varchar(20) =  '',
	@ProductLine varchar(30) = '',
	@Quote_Bucket varchar(16) = '',
	@QuoteType varchar(40) = '',
	@Campaign varchar(10) = '',
	@Account varchar(15) = '',
	@Contact float = '',
	@Doc_Number varchar(15) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @SQLQuery AS NVarchar(4000) 
    Declare @ParamDefinition AS NVarchar(2000)
	declare @Cusmerge as varchar(15)
	declare @DocNumber as varchar(15)
	declare @Buyerct as float
	declare @Custype as varchar(20)
	declare @Product_Line as varchar(30) 
	declare @Quote_Type as varchar(40) 
	declare @QuoteBucket as varchar(16)
	declare @Reseller as varchar(4) 
	declare @Government as varchar(4) 
	declare @NumQuotes as float
	declare @NumOrders as float
	declare @CloseRate as float
	declare @AveQuoteDis as float
	declare @AveOrderDis as float
	declare @SuccessRateCont as varchar(100)
	declare @SuccessRateSite as varchar(100)
	declare @SiteName as varchar(53)
	declare @ContactName as varchar(110)
	Declare @RowCount as bigint
	Declare @GMOrder as float
	Declare @GMQuote as float
	
	set RowCount 1
	
	if(@Doc_Number <> '')
	begin
		if (@Campaign = 'CA')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			CA.Quote a left join CA.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CA.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
						
			--Get Reseller
			Select @RowCount = COUNT(*) from
			CA.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'CA')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'CA')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'CA') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket,
				Reseller,Government 
			
			Select @Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote from Ca.QuoteDCHistory
			-- Getting Quote Discount Guidance
			
			   Select @Cusmerge as CUSMERGE,
			   @Buyerct as buyerct_SVR,
			   @DocNumber as Quote_Doc_No,
			   @Custype as tag,
			   @Product_Line as dm_product_line_desc,
			   @Quote_Type as Quote_Doc_Reason_Code_Desc,
			   @QuoteBucket as Quote_Bucket,
			   @Reseller as Reseller,
			   @Government as Government,
			   @NumQuotes as Hist_Num_Quotes,
			   @NumOrders as Hist_Num_Quotes_Conv,
			   @AveQuoteDis as ave_quote_disc,
			   @AveOrderDis as ave_order_disc,
			   @CloseRate as Hist_Perc_Conv,
			   @SuccessRateSite as Percent_Conversion_Site,
			   @SuccessRateCont as percent_conversion_cont,
			   @ContactName as ContactName,
			   @SiteName as SiteName,
			   @GMOrder as GM_Order,
			   @GMQuote as GM_Quote 
			   from Ca.QuoteDCHistory 
		end
		else if(@Campaign = 'US')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			US.Quote a left join US.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from US.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			Select @RowCount = COUNT(*) from
			US.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'US')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'US')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
		
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'US') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket,
				Reseller,Government 
			
			-- Getting Quote Discount Guidance
			
			Select @Cusmerge as CUSMERGE,
			   @Buyerct as buyerct_SVR,
			   @DocNumber as Quote_Doc_No,
			   @Custype as tag,
			   @Product_Line as dm_product_line_desc,
			   @Quote_Type as Quote_Doc_Reason_Code_Desc,
			   @QuoteBucket as Quote_Bucket,
			   @Reseller as Reseller,
			   @Government as Government,
			   @NumQuotes as Hist_Num_Quotes,
			   @NumOrders as Hist_Num_Quotes_Conv,
			   @AveQuoteDis as ave_quote_disc,
			   @AveOrderDis as ave_order_disc,
			   @CloseRate as Hist_Perc_Conv,
			   @SuccessRateSite as Percent_Conversion_Site,
			   @SuccessRateCont as percent_conversion_cont,
			   @ContactName as ContactName,
			   @SiteName as SiteName,
			   @GMOrder as GM_Order,
			   @GMQuote as GM_Quote  
			   from US.QuoteDCHistory 
		end
		else if(@Campaign = 'EMED')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			EMED.Quote a left join EMED.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from EMED.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			Select @RowCount = COUNT(*) from
			EMED.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'EMED')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'EMED')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'EMED') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket,
				Reseller,Government 
				
		Select @Cusmerge as CUSMERGE,
			   @Buyerct as buyerct_SVR,
			   @DocNumber as Quote_Doc_No,
			   @Custype as tag,
			   @Product_Line as dm_product_line_desc,
			   @Quote_Type as Quote_Doc_Reason_Code_Desc,
			   @QuoteBucket as Quote_Bucket,
			   @Reseller as Reseller,
			   @Government as Government,
			   @NumQuotes as Hist_Num_Quotes,
			   @NumOrders as Hist_Num_Quotes_Conv,
			   @AveQuoteDis as ave_quote_disc,
			   @AveOrderDis as ave_order_disc,
			   @CloseRate as Hist_Perc_Conv,
			   @SuccessRateSite as Percent_Conversion_Site,
			   @SuccessRateCont as percent_conversion_cont,
			   @ContactName as ContactName,
			   @SiteName as SiteName,
			   @GMOrder as GM_Order,
			   @GMQuote as GM_Quote 
			   from EMED.QuoteDCHistory 
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
		else if(@Campaign = 'UK')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME 
			from
			UK.Quote a left join UK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from UK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket
					 ,quote_cost,order_cost from UK.Quote a
					 left join UK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
		
		Select @Cusmerge as CUSMERGE,@Buyerct as buyerct_SVR,@DocNumber as Quote_Doc_No,@Custype as tag,
			@Product_Line as dm_product_line_desc,@Quote_Type as Quote_Doc_Reason_Code_Desc ,@QuoteBucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site ,@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
		else if(@Campaign = 'SUK')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME 
			from
			SUK.Quote a left join SUK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from SUK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			--Select @RowCount = COUNT(*) from
			--UK.SITEINFO 
			--where ISNULL(BuyerOrg_Desc,'') in 
			--(Select Name from Reseller where Campaign= 'UK')
			--and SOLD_TO = @Cusmerge
			--if @RowCount > 0
			--begin
			--	set @Reseller = 'Yes'
			--end
			--else
			--begin
			--	set @Reseller = 'No'
			--end
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket
					 ,quote_cost,order_cost from SUK.Quote a
					 left join SUK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					  ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
				
	Select @Cusmerge as CUSMERGE,@Buyerct as buyerct_SVR,@DocNumber as Quote_Doc_No,@Custype as tag,
			@Product_Line as dm_product_line_desc,@Quote_Type as Quote_Doc_Reason_Code_Desc ,@QuoteBucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site ,@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
		else if(@Campaign = 'DE')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME 
			from
			DE.Quote a left join DE.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from DE.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			--Select @RowCount = COUNT(*) from
			--UK.SITEINFO 
			--where ISNULL(BuyerOrg_Desc,'') in 
			--(Select Name from Reseller where Campaign= 'UK')
			--and SOLD_TO = @Cusmerge
			--if @RowCount > 0
			--begin
			--	set @Reseller = 'Yes'
			--end
			--else
			--begin
			--	set @Reseller = 'No'
			--end
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket
					 ,quote_cost,order_cost from DE.Quote a
					 left join DE.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					  ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
				
	Select @Cusmerge as CUSMERGE,@Buyerct as buyerct_SVR,@DocNumber as Quote_Doc_No,@Custype as tag,
			@Product_Line as dm_product_line_desc,@Quote_Type as Quote_Doc_Reason_Code_Desc ,@QuoteBucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site ,@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
		else if(@Campaign = 'AT')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME 
			from
			AT.Quote a left join AT.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from AT.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			--Select @RowCount = COUNT(*) from
			--UK.SITEINFO 
			--where ISNULL(BuyerOrg_Desc,'') in 
			--(Select Name from Reseller where Campaign= 'UK')
			--and SOLD_TO = @Cusmerge
			--if @RowCount > 0
			--begin
			--	set @Reseller = 'Yes'
			--end
			--else
			--begin
			--	set @Reseller = 'No'
			--end
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket
					 ,quote_cost,order_cost from AT.Quote a
					 left join AT.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					  ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
				
	Select @Cusmerge as CUSMERGE,@Buyerct as buyerct_SVR,@DocNumber as Quote_Doc_No,@Custype as tag,
			@Product_Line as dm_product_line_desc,@Quote_Type as Quote_Doc_Reason_Code_Desc ,@QuoteBucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site ,@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
		else if(@Campaign = 'CH')
		begin
			--Getting Filters
			select @Cusmerge = CUSMERGE, @Buyerct = buyerct_SVR,
			@Custype = tag, @Product_Line = dm_product_line_desc,
			@Quote_Type =Quote_Doc_Reason_Code_Desc, 
			@QuoteBucket = Quote_Bucket,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, @SiteName = b.NAME 
			from
			CH.Quote a left join CH.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CH.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where Quote_Doc_No = @Doc_Number
			group by CUSMERGE,buyerct_SVR,tag,dm_product_line_desc,
			Quote_Doc_Reason_Code_Desc,Quote_Bucket,num_quotes_contact
			,num_quotes_conv_site,num_quotes_site,percent_conversion_cont
			,Percent_Conversion_Site,num_quotes_conv_contact,ContactName,
			NAME,industry
			
			set @DocNumber = @Doc_Number
			
			--Get Reseller
			--Select @RowCount = COUNT(*) from
			--UK.SITEINFO 
			--where ISNULL(BuyerOrg_Desc,'') in 
			--(Select Name from Reseller where Campaign= 'UK')
			--and SOLD_TO = @Cusmerge
			--if @RowCount > 0
			--begin
			--	set @Reseller = 'Yes'
			--end
			--else
			--begin
			--	set @Reseller = 'No'
			--end
			
				-- Computing Quote Discount Guidance
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket
					 ,quote_cost,order_cost from CH.Quote a
					 left join CH.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					  ) tblQuoteWithResellerGovernment  
			where tag =  @Custype and
				ISNULL(dm_product_line_desc,'') = 
				ISNULL(@Product_Line,'') and
				Quote_Doc_Reason_Code_Desc = @Quote_Type and
				Quote_Bucket = @QuoteBucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
				
	Select @Cusmerge as CUSMERGE,@Buyerct as buyerct_SVR,@DocNumber as Quote_Doc_No,@Custype as tag,
			@Product_Line as dm_product_line_desc,@Quote_Type as Quote_Doc_Reason_Code_Desc ,@QuoteBucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site ,@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			-- Getting Quote Discount Guidance
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Buyerct,@DocNumber,@Custype,
			@Product_Line,@Quote_Type,@QuoteBucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Cusmerge and
			buyerct_SVR = @Buyerct and
			Quote_Doc_No = @DocNumber */
		end
	end
	else if(@Account <> '')
	begin
		if (@Campaign = 'CA')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			CA.Quote a left join CA.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CA.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			CA.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'CA')
			and SOLD_TO = @Account
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'CA')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'CA') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance
			
			
		Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		  ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@Reseller as Reseller,
			@Government as Government ,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote FROM  CA.QuoteDCHistory 
			
			/*
			truncate table Ca.QuoteDCHistory
			
			
			insert into CA.QuoteDCHistory(CUSMERGE,Buyerct_SVR, 
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Account,@Buyerct,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,null,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  CA.QuoteDCHistory 
			WHERE  CUSMERGE = @Account			
			*/
		end
		else if(@Campaign = 'US')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			US.Quote a left join US.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from US.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			US.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'US')
			and SOLD_TO = @Account
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'US')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'US') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance
			
			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		  ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@Reseller as Reseller,
			@Government as Government ,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote FROM  US.QuoteDCHistory 
/*			
			truncate table US.QuoteDCHistory
			
			insert into US.QuoteDCHistory(CUSMERGE,Buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Account,@Buyerct,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,null,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  US.QuoteDCHistory 
			WHERE  CUSMERGE = @Account	
*/
		end
		else if(@Campaign = 'EMED')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			EMED.Quote a left join EMED.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from EMED.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			EMED.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'EMED')
			and SOLD_TO = @Account
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'EMED')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					 ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'EMED') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		  ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@Reseller as Reseller,
			@Government as Government ,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote FROM  EMED.QuoteDCHistory 
/*
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,Buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Account,@Buyerct,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,null,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  CUSMERGE = @Account	
*/
		end
		else if(@Campaign = 'UK')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME 
			from
			UK.Quote a left join UK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from UK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from UK.Quote a
					 left join UK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		    ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote 

		end
		else if(@Campaign = 'SUK')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME 
			from
			SUK.Quote a left join SUK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from SUK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from SUK.Quote a
					 left join SUK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		    ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote 
		end
		else if(@Campaign = 'AT')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME 
			from
			AT.Quote a left join AT.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from AT.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from AT.Quote a
					 left join AT.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		    ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote 
		end
		else if(@Campaign = 'DE')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME 
			from
			DE.Quote a left join DE.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from DE.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from DE.Quote a
					 left join DE.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		    ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote 
		end
		else if(@Campaign = 'CH')
		begin
			select  @Buyerct = buyerct_SVR,
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME 
			from
			CH.Quote a left join CH.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CH.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where CUSMERGE= @Account
			group by buyerct_SVR,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from CH.Quote a
					 left join CH.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''), 
				Quote_Doc_Reason_Code_Desc, Quote_Bucket
						
			-- Getting Quote Discount Guidance


			Select 	@Account as CUSMERGE ,@Buyerct as Buyerct_SVR,'' as Quote_Doc_No,@CustomerType as tag, @ProductLine as dm_product_line_desc
		    ,@QuoteType as Quote_Doc_Reason_Code_Desc , @Quote_Bucket as Quote_Bucket,@NumQuotes as Hist_Num_Quotes,@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv ,
			@SuccessRateSite as Percent_Conversion_Site ,null as percent_conversion_cont ,@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,@GMQuote as GM_Quote 
		end
	end
    else if(@Contact <> '')
	begin
		if (@Campaign = 'CA')
		begin
			select  @Cusmerge=CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			CA.Quote a left join CA.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CA.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR= @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			CA.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'CA')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'CA')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from CA.Quote a
					 left join CA.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'CA') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,
			@Contact as buyerct_SVR, 
			'' as Quote_Doc_No, 
			@CustomerType as tag , 
			@ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, 
			@Quote_Bucket as Quote_Bucket, 
			@Reseller as Reseller, 
			@Government as Government ,
			@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,
			@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont,
			@ContactName as ContactName,@SiteName as SiteName,
			@GMOrder as GM_Order,
			@GMQuote as GM_Quote
			FROM  CA.QuoteDCHistory
			/*
			truncate table Ca.QuoteDCHistory
		
			insert into CA.QuoteDCHistory(CUSMERGE,Buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Contact,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  CA.QuoteDCHistory 
			WHERE  buyerct_SVR = @Contact			
			*/
		end
		else if(@Campaign = 'US')
		begin
			select  @Cusmerge=CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			US.Quote a left join US.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from US.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR= @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			US.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'US')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'US')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from US.Quote a
					 left join US.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'US') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance
		
			SELECT @Cusmerge as CUSMERGE ,@Contact as buyerct_SVR, '' as Quote_Doc_No, @CustomerType as tag , @ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, @Quote_Bucket as Quote_Bucket, @Reseller as Reseller, @Government as Government ,@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,@SuccessRateCont as percent_conversion_cont,@ContactName as ContactName,@SiteName as SiteName,@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			FROM  US.QuoteDCHistory
		/*
			truncate table US.QuoteDCHistory
			
			insert into US.QuoteDCHistory(CUSMERGE,Buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Contact,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  US.QuoteDCHistory 
			WHERE  buyerct_SVR = @Contact		
			*/
		end
		else if(@Campaign = 'EMED')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME ,
			@Government = (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end)
			from
			EMED.Quote a left join EMED.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from EMED.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
				
			--Get Reseller
			Select @RowCount = COUNT(*) from
			EMED.SITEINFO 
			where ISNULL(BuyerOrg_Desc,'') in 
			(Select Name from Reseller where Campaign= 'EMED')
			and SOLD_TO = @Cusmerge
			if @RowCount > 0
			begin
				set @Reseller = 'Yes'
			end
			else
			begin
				set @Reseller = 'No'
			end
			
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'Yes' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') in 
					 (Select Name from Reseller where Campaign= 'EMED')
					 union all  
					 select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,'No' as 'Reseller',
					 (Case when ISNULL(industry,'') like '%Public Admin%' then 'Yes' else 'No' end) as 'Government' 
					  ,quote_cost,order_cost from EMED.Quote a
					 left join EMED.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 where ISNULL(b.BuyerOrg_Desc,'') not in 
					 (Select Name from Reseller where Campaign= 'EMED') 
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				and
				Reseller = @Reseller and
				Government = @Government
				group by tag, ISNULL(dm_product_line_desc,''), Quote_Doc_Reason_Code_Desc, Quote_Bucket,Reseller,Government 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,@Contact as buyerct_SVR, '' as Quote_Doc_No, @CustomerType as tag , @ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, @Quote_Bucket as Quote_Bucket, @Reseller as Reseller, @Government as Government ,@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,@AveQuoteDis as ave_quote_disc,@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,@SuccessRateCont as percent_conversion_cont,@ContactName as ContactName,@SiteName as SiteName,@GMOrder as GM_Order,@GMQuote as GM_Quote
			
			FROM  EMED.QuoteDCHistory
/*			
			truncate table EMED.QuoteDCHistory
			
			insert into EMED.QuoteDCHistory(CUSMERGE,Buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,
			Percent_Conversion_Site,percent_conversion_cont,
			ContactName,SiteName,GM_Order,GM_Quote)
			values(@Cusmerge,@Contact,'',@CustomerType,
			@ProductLine,@QuoteType,@Quote_Bucket,@Reseller,
			@Government,@NumQuotes,@NumOrders,
			@AveQuoteDis,@AveOrderDis,@CloseRate,
			@SuccessRateSite,@SuccessRateCont,@ContactName,@SiteName,
			@GMOrder,@GMQuote)
	
			SELECT CUSMERGE,buyerct_SVR,
			Quote_Doc_No,tag,dm_product_line_desc,Quote_Doc_Reason_Code_Desc,
			Quote_Bucket,Reseller,Government,Hist_Num_Quotes,Hist_Num_Quotes_Conv,
			ave_quote_disc,ave_order_disc,Hist_Perc_Conv,Percent_Conversion_Site,
			percent_conversion_cont,ContactName,SiteName,GM_Order,GM_Quote
			FROM  EMED.QuoteDCHistory 
			WHERE  buyerct_SVR = @Contact	
			*/
		end
		else if(@Campaign = 'UK')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME
			from
			UK.Quote a left join UK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from UK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
							
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,quote_cost,order_cost from UK.Quote a
					 left join UK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''),
				 Quote_Doc_Reason_Code_Desc, Quote_Bucket 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,@Contact as buyerct_SVR, '' as Quote_Doc_No, 
			@CustomerType as tag , @ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, @Quote_Bucket as Quote_Bucket
			,@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont,@ContactName as ContactName,
			@SiteName as SiteName,@GMOrder as GM_Order,@GMQuote as GM_Quote
		end
		else if(@Campaign = 'SUK')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME
			from
			SUK.Quote a left join SUK.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from SUK.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
							
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from SUK.Quote a
					 left join SUK.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''),
				 Quote_Doc_Reason_Code_Desc, Quote_Bucket 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,
			@Contact as buyerct_SVR, 
			'' as Quote_Doc_No, 
			@CustomerType as tag , 
			@ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, 
			@Quote_Bucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,
			@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont
			,@ContactName as ContactName,
			@SiteName as SiteName,@GMOrder as GM_Order,
			@GMQuote as GM_Quote
		end
		else if(@Campaign = 'DE')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME
			from
			DE.Quote a left join DE.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from DE.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
							
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from DE.Quote a
					 left join DE.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''),
				 Quote_Doc_Reason_Code_Desc, Quote_Bucket 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,
			@Contact as buyerct_SVR, 
			'' as Quote_Doc_No, 
			@CustomerType as tag , 
			@ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, 
			@Quote_Bucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,
			@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont
			,@ContactName as ContactName,
			@SiteName as SiteName,@GMOrder as GM_Order,
			@GMQuote as GM_Quote
		end
		else if(@Campaign = 'AT')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME
			from
			AT.Quote a left join AT.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from AT.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
							
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from AT.Quote a
					 left join AT.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''),
				 Quote_Doc_Reason_Code_Desc, Quote_Bucket 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,
			@Contact as buyerct_SVR, 
			'' as Quote_Doc_No, 
			@CustomerType as tag , 
			@ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, 
			@Quote_Bucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,
			@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont
			,@ContactName as ContactName,
			@SiteName as SiteName,@GMOrder as GM_Order,
			@GMQuote as GM_Quote
		end
		else if(@Campaign = 'CH')
		begin
			select  @Cusmerge = CUSMERGE,
			@SuccessRateCont = STR(percent_conversion_cont * 100,10,2) + ' % (' + STR(num_quotes_conv_contact,10,0) + ' out of ' +
			STR(num_quotes_contact,10,0) + ' )',
			@SuccessRateSite = STR(Percent_Conversion_Site * 100,10,2) + ' % (' + STR(num_quotes_conv_site,10,0) + ' out of ' + 
			STR(num_quotes_site,10,0) + ' )',
			@ContactName = c.ContactName, 
			@SiteName = b.NAME
			from
			CH.Quote a left join CH.SITEINFO b on a.CUSMERGE=b.SOLD_TO
			left join (Select (SURNAME + ', ' + FIRSTNAME) as 'ContactName',
			CONTACT from CH.Continfo ) c on a.Buyerct_SVR = c.CONTACT
			where buyerct_SVR = @Contact
			group by CUSMERGE,percent_conversion_cont,num_quotes_conv_contact
			,num_quotes_contact,num_quotes_conv_site,num_quotes_site
			,percent_conversion_site,ContactName,NAME,industry
							
			select	@NumQuotes = count(distinct Quote_Doc_No),
			@NumOrders = count(distinct Quote_conv),
			@CloseRate = (case when @NumQuotes = 0 then 0 else @NumOrders / @NumQuotes end),
			@AveQuoteDis = sum(abs(quote_discount))/(sum(quote_net_sales)+sum(abs(quote_discount))),
			@AveOrderDis = sum(abs(order_discount))/(sum(order_net_sales)+sum(abs(order_discount))),
			@GMQuote = ((sum(quote_net_sales) - sum(quote_cost)))/sum(quote_net_sales),
			@GMOrder = ((sum(order_net_sales) - sum(order_cost)))/sum(order_net_sales)
			from (select Quote_Doc_No,CUSMERGE,b.BuyerOrg_Desc,Quote_conv,Buyerct_SVR
					 ,quote_discount,quote_net_sales,order_discount,
					 order_net_sales,tag,dm_product_line_desc,
					 Quote_Doc_Reason_Code_Desc,Quote_Bucket,
					 quote_cost,order_cost from CH.Quote a
					 left join CH.SITEINFO b on (a.CUSMERGE=b.SOLD_TO)
					 ) tblQuoteWithResellerGovernment  
			where tag =  @CustomerType and
				ISNULL(dm_product_line_desc,'') = ISNULL(@ProductLine,'') and
				Quote_Doc_Reason_Code_Desc = @QuoteType and
				Quote_Bucket = @Quote_Bucket
				group by tag, ISNULL(dm_product_line_desc,''),
				 Quote_Doc_Reason_Code_Desc, Quote_Bucket 
						
			-- Getting Quote Discount Guidance
			
			SELECT @Cusmerge as CUSMERGE ,
			@Contact as buyerct_SVR, 
			'' as Quote_Doc_No, 
			@CustomerType as tag , 
			@ProductLine as dm_product_line_desc,
			@QuoteType as Quote_Doc_Reason_Code_Desc, 
			@Quote_Bucket as Quote_Bucket,
			@NumQuotes as Hist_Num_Quotes,
			@NumOrders as Hist_Num_Quotes_Conv,
			@AveQuoteDis as ave_quote_disc,
			@AveOrderDis as ave_order_disc,
			@CloseRate as Hist_Perc_Conv,
			@SuccessRateSite as Percent_Conversion_Site,
			@SuccessRateCont as percent_conversion_cont
			,@ContactName as ContactName,
			@SiteName as SiteName,@GMOrder as GM_Order,
			@GMQuote as GM_Quote
		end
    end 
	else if(@CustomerType <> '')
	begin
		Set @SqlQuery = 'Select ave_quote_disc,ave_order_disc,Hist_Num_Quotes
			,Hist_Num_Quotes_Conv,Hist_Perc_Conv,GM_Quote,GM_Order
			 from ' + @Campaign + '.Quote 
			where tag=''' + @CustomerType + ''' and Isnull(DM_Product_Line_Desc,'''')=''' +
			ISNULL(@ProductLine,'') + ''' and Quote_Bucket=''' + @Quote_Bucket + ''' and
			Quote_Doc_Reason_Code_Desc = ''' + @QuoteType + ''''
		
	   -- Set @ParamDefinition = 
            
	--	PRINT @SQLQuery 
		execute sp_executesql @SqlQuery,  N'@CustomerType varchar(20) , @ProductLine varchar(30), @Quote_Bucket varchar(16),@QuoteType varchar(40),
						@Campaign varchar(10)', @CustomerType ,  @ProductLine , @Quote_Bucket , @QuoteType , @Campaign
	--	EXEC(@SQLQuery)
    end 
    
    set RowCount 0
    
    
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
	ErrorHandler:
		Return(@@ERROR)
	
END

