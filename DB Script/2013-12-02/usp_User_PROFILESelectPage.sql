USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[usp_USER_PROFILESelectPage]    Script Date: 12/02/2013 20:35:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_USER_PROFILESelectPage] 
   
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
select ' ' + PageName as PageName from PageList

	COMMIT
	
	



	
	
	
