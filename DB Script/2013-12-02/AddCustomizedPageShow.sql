USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[AddCustomizedPageShow]    Script Date: 12/02/2013 20:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddCustomizedPageShow] 
	-- Add the parameters for the stored procedure here
	@Username varchar(10),
	@CampaignName varchar(max),
	@CampaignValue varchar(100),
	@PageName varchar(max)
AS
BEGIN
	declare @identity as int
	begin tran
		update Customized_Page set Valid_to=GETDATE()
		where Username = @Username and
		Valid_To='9999-12-31'
		
		Insert Into Customized_Page
			values(@PageName,@Username,
			'9999-12-31',GETDATE(),@CampaignName,
			@CampaignValue)
			
	Commit
	
	SET NOCOUNT OFF;
		set @Identity=SCOPE_IDENTITY()     
      
		Select Username,
		valid_from,valid_to From Customized_Page
			WHERE ID = @Identity  
	
END
