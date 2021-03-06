USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[AddQuoteComputing]    Script Date: 09/19/2013 22:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
ALTER PROCEDURE [dbo].[AddQuoteComputing]    
 -- Add the parameters for the stored procedure here    
	@Probablilty float,    
	@Weighted float,    
	@ProposedDate date,    
	@Competition varchar(50),    
	@Notes varchar(300),    
	@Campaign varchar(16),    
	@Quote_Doc varchar(15),    
	@AccountNum varchar(15),    
	@Createdby varchar(10),    
	@QuoteDay date,     
	@AccountName varchar(35),    
	@QSTCurrent varchar(100),    
	@QSTIn varchar(100),    
	@QuoteValue float,    
	@WinorLoss varchar(5),    
	@Status varchar(15),    
	@QuoteCost float,    
	@QuoteGMPerc float,    
	@product_line varchar(300),    
	@sched_followup date,    
	@construction varchar(4),    
	@mining varchar(4)    
AS    
BEGIN     
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.     
 Begin tran    
 
 Declare @Identity int
 
 If @Probablilty <> -1    
				 Begin    
					  UPDATE QuoteComputing    
							SET [Valid_To]=GETDATE()    
					  WHERE Campaign=@Campaign    
							AND AccountNum=@AccountNum    
							AND Quote_Doc = @Quote_Doc     
							AND Valid_To='9999-12-31'    
							AND Probablilty <> -1    
			       
			  If(@sched_followup = null)    
				  Begin    
					   INSERT INTO QuoteComputing
								   (Probablilty
								   ,Weighted
								   ,ProposedDate
								   ,Competition
								   ,Notes
								   ,Campaign
								   ,Quote_Doc
								   ,AccountNum
								   ,createdby
								   ,Createdon
								   ,Valid_To
								   ,WinorLoss
								   ,product_line
								   ,sched_followup
								   ,construction
								   ,mining)    
					   VALUES(@Probablilty
							 ,@Weighted
							 ,@ProposedDate
							 ,@Competition
							 ,@Notes
							 ,@Campaign
							 ,@Quote_Doc
							 ,@AccountNum    
							 ,@Createdby
							 ,GETDATE()
							 ,'9999-12-31'
							 ,@WinorLoss    
							 ,@product_line
							 ,null
							 ,@construction
							 ,@mining)    
					End    
			  Else
			      
					Begin    
					   INSERT INTO QuoteComputing
								   (Probablilty
								   ,Weighted
								   ,ProposedDate
								   ,Competition
								   ,Notes
								   ,Campaign
								   ,Quote_Doc
								   ,AccountNum
								   ,createdby
								   ,Createdon
								   ,Valid_To
								   ,WinorLoss
								   ,product_line
								   ,sched_followup
								   ,construction
								   ,mining)    
					   VALUES(@Probablilty
							 ,@Weighted
							 ,@ProposedDate
							 ,@Competition
							 ,@Notes
							 ,@Campaign
							 ,@Quote_Doc
							 ,@AccountNum    
							 ,@Createdby,GETDATE()
							 ,'9999-12-31'
							 ,@WinorLoss    
							 ,@product_line
							 ,@sched_followup
							 ,@construction
							 ,@mining)    
					End    
				  
			 End
     
 Else 
    
	 Begin    
		  UPDATE QuoteComputing    
				set [Valid_To]=GETDATE()    
		  where Campaign=@Campaign    
			  AND AccountNum=@AccountNum    
			  AND Quote_Doc = @Quote_Doc     
			  and Valid_To='9999-12-31'    
			  and Probablilty = -1    
		      
		  If(@sched_followup = null)    
			  Begin    
				   INSERT INTO QuoteComputing
							   (Probablilty
							   ,Weighted
							   ,ProposedDate
							   ,Competition
							   ,Notes
							   ,Campaign
							   ,Quote_Doc
							   ,AccountNum
							   ,createdby
							   ,Createdon
							   ,Valid_To
							   ,WinorLoss
							   ,product_line
							   ,sched_followup
							   ,construction
							   ,mining)    
				   VALUES(@Probablilty
						 ,@Weighted
						 ,@ProposedDate
						 ,@Competition
						 ,@Notes
						 ,@Campaign
						 ,@Quote_Doc
						 ,@AccountNum    
						 ,@Createdby
						 ,GETDATE()
						 ,'9999-12-31'
						 ,@WinorLoss    
						 ,@product_line
						 ,null
						 ,@construction
						 ,@mining)    
			  End    
		 Else    
			  Begin    
				   INSERT INTO QuoteComputing
							   (Probablilty
							   ,Weighted
							   ,ProposedDate
							   ,Competition
							   ,Notes
							   ,Campaign
							   ,Quote_Doc
							   ,AccountNum
							   ,createdby
							   ,Createdon
							   ,Valid_To
							   ,WinorLoss
							   ,product_line
							   ,sched_followup
							   ,construction,mining)    
				   VALUES(@Probablilty
						  ,@Weighted
						  ,@ProposedDate
						  ,@Competition
						  ,@Notes
						  ,@Campaign
						  ,@Quote_Doc
						  ,@AccountNum    
						  ,@Createdby
						  ,GETDATE()
						  ,'9999-12-31'
						  ,@WinorLoss    
						  ,@product_line
						  ,@sched_followup
						  ,@construction
						  ,@mining)    
			  End    
 End    
     
     
 Declare @RowCount as int    
     
 If @Probablilty=100 or @Probablilty = 1 or @Probablilty = -1    
 Begin    
	  If (@Campaign = 'EMED')    
		  Begin        
			   SELECT @RowCount = COUNT(*) from EMED.Open_Quotes_WinOrLoss    
			   where Quote_Doc_createdon =@QuoteDay and CUSMERGE=@AccountNum and    
			   Quote_Doc_No = @Quote_Doc    
			       
			   If @RowCount = 0    
				   Begin    
					INSERT INTO EMED.Open_Quotes_WinOrLoss
								(Quote_Doc_createdon
								,CUSMERGE
								,Company_name
								,Quote_Doc_No
								,Quote_SlsTeamIN
								,Sales_Rep
								,quote_value
								,[Status]
								,quote_cost
								,Quote_GM_perc)    
					VALUES(@QuoteDay
						  ,@AccountNum
						  ,@AccountName
						  ,@Quote_Doc
						  ,@QSTIn
						  ,@QSTCurrent
						  ,@QuoteValue
						  ,@Status
						  ,@QuoteCost
						  ,@QuoteGMPerc)     
				   end    
			       
			   exec DeleteOpenQuotesEMED @Quote_Doc           
		  End    
		  
	  Else If (@Campaign = 'US')    
		  Begin     
		       
			   select @RowCount = COUNT(*) from US.Open_Quotes_WinOrLoss    
			   where Quote_Doc_No = @Quote_Doc    
			       
			   If @RowCount = 0    
				   Begin    
					INSERT INTO US.Open_Quotes_WinOrLoss
								(Quote_Doc_createdon
								,CUSMERGE
								,Company_name
								,Quote_Doc_No
								,Quote_SlsTeamIN
								,Sales_Rep
								,quote_value
								,[Status]
								,quote_cost
								,Quote_GM_perc)    
					VALUES(@QuoteDay,@AccountNum,@AccountName,@Quote_Doc,@QSTIn,    
					@QSTCurrent,@QuoteValue,@Status,@QuoteCost,@QuoteGMPerc)    
				   
				   End    
				       
			   exec DeleteOpenQuotes @Quote_Doc    
		       
		  End    
		  
	  Else If (@Campaign = 'CA')    
		  Begin    
		       
			   select @RowCount = COUNT(*) from CA.Open_Quotes_WinOrLoss    
			   where Quote_Doc_createdon =@QuoteDay and CUSMERGE=@AccountNum and    
			   Quote_Doc_No = @Quote_Doc    
			       
			   If @RowCount = 0    
				   Begin    
					INSERT INTO CA.Open_Quotes_WinOrLoss
								(Quote_Doc_createdon
								,CUSMERGE
								,Company_name
								,Quote_Doc_No
								,Quote_SlsTeamIN
								,Sales_Rep
								,quote_value
								,[Status]
								,quote_cost
								,Quote_GM_perc)    
					VALUES(@QuoteDay
						  ,@AccountNum
						  ,@AccountName
						  ,@Quote_Doc
						  ,@QSTIn
						  ,@QSTCurrent
						  ,@QuoteValue
						  ,@Status
						  ,@QuoteCost
						  ,@QuoteGMPerc)    
				   End    
			       
			   exec DeleteOpenQuotesCA @Quote_Doc    
		       
		  End    
 End    

     
 commit    
     
 SET NOCOUNT OFF;    
    set @Identity=SCOPE_IDENTITY()   
    
    SELECT [Probablilty]
		  ,[Weighted]
		  ,[ProposedDate] 
		  ,[Competition] 
		  ,[Notes] 
		  ,[Campaign] 
		  ,[Quote_Doc]
		  ,[AccountNum] 
		  ,[Createdon] 
		  ,[Createdby]
		  ,[Valid_To]
		  ,[ID]
		  ,[WinorLoss]
		  ,[sched_followup] 
		  ,[product_line]
		  ,[construction] 
		  ,[mining] 
	FROM QuoteComputing  
    WHERE ID = @Identity 
     
end    
    
------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------  

