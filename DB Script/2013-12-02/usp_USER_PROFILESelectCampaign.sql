USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILESelectCampaign]    Script Date: 12/02/2013 20:35:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_USER_PROFILESelectCampaign] 
   
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
select distinct campaignName,campaignValue

 from CAMPAIGNList order by campaignName
	COMMIT
	

	
	
	
