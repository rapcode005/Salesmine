USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[SelectQuoteOver1KWithEditQuoteValue]    Script Date: 11/14/2013 18:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SelectQuoteOver1KWithEditQuoteValue]  
 -- Add the parameters for the stored procedure here    
 @Campaign varchar(16)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;  
     
    Declare @SQLQuery AS NVarchar(4000)    
    Declare @ParamDefinition AS NVarchar(2000)    
    
    
  if(@Campaign='EMED')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN
			,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition
			,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction,mining 
	 from EMED.QuotePipeline    
  End 
  
  Else if(@Campaign='CA')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN
			,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition
			,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
		from CA.QuotePipeline    
  End    
  
  Else if(@Campaign='US')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from US.QuotePipeline    
  End
  
  
  Else if(@Campaign='UK')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from UK.QuotePipeline    
  End  
  
    Else if(@Campaign='SUK')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from SUK.QuotePipeline    
  End    
  
  Else if(@Campaign='AT')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from AT.QuotePipeline    
  End 
  
  Else if(@Campaign='DE')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from DE.QuotePipeline    
  End
  
    Else if(@Campaign='CH')  
  Begin       
	  Select Quote_Doc_createdon
			,CUSMERGE
			,Company_name
			,Quote_Doc_No
			,Quote_SlsTeamIN,Sales_Rep
			,Quote_value
			,[Probablilty]
			,Weighted
			,ProposedDate    
			,Competition,Notes
			,WinorLoss
			,status
			,quote_cost
			,Quote_GM_perc
			,sched_followup
			,product_line
			,construction
			,mining 
	from CH.QuotePipeline    
  End     
    --PRINT @SQLQuery    
       -- EXEC(@SQLQuery)    
       
     
--    If @@ERROR <> 0 GoTo ErrorHandler    
--    Set NoCount OFF    
--    Return(0)    
      
--ErrorHandler:    
--    Return(@@ERROR)    
END