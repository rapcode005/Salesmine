USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[GetMessage]    Script Date: 09/27/2013 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetMessage] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @SQLQuery AS Varchar(4000)

	Set @SQLQuery = 'Select [Campaign],
							[State],
							[Message],
							[Valid_From],
							[Username],
							EffDate,
							Id from ManageMessage
					where valid_to = ''9999-12-31'' order by [Campaign],[State]'
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
END