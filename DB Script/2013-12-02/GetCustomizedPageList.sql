USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomizedPageList]    Script Date: 12/02/2013 20:34:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetCustomizedPageList]
	-- Add the parameters for the stored procedure here
	@createdby varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select PageName,Username,CampaignName,
	CampaignValue from Customized_Page
	where Username=@createdby and
	Valid_to='9999-12-31'
END
