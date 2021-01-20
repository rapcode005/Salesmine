
GO
/****** Object:  StoredProcedure [dbo].[sp_NewBuyerSince]    Script Date: 10/14/2013 18:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_NewBuyerSince]
	-- Add the parameters for the stored procedure here
	@fpdcont varchar(15),
    @salesteam  varchar(20),
    @Campaign varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000)
    
	Set @SQLQuery='select
	  [CONTACT_STATUS] as [CONTACT STATUS]
	  ,[cusmerge] as [ACCOUNT NUM]
	  ,[NAME] as [ACCOUNT NAME]
	  ,[MG_NAME] as [MANAGED GROUP]
	  ,[BuyerOrg_Desc] as [BUYER ORG]
	  ,[KAM_Mgr] as [KAM NAME]
      ,[contmerg] as [CONTACT NUM]
      ,[SURNAME] + '', '' + [FIRSTNAME] as [CONTACT NAME]
      ,[contact_type] as [CONTACT TYPE]
      ,[PHONE] as [CONTACT PHONE]
      ,[rec_mo] as [CONTACT RECENCY]
      ,[Sales12M] as [CONTACT SALES 12M]
      ,[Function] as [FUNCTION]
      ,convert(nvarchar, [last_disp_dt],101) as [LAST DISP DATE]
      ,[NOTES] as [LAST DISP NOTE]
      ,convert(nvarchar, [lpdcont],101) as [LAST PURCHASED DATE]
      ,[EMAIL_ADDR] as [EMAIL ADDRESS]
      ,[REP_CONTSTAT] as [REP CONTACT STATUS]
      ,[REP_JOBAREA] as [REP JOB AREA]     
	from ' + @Campaign + '.CONTACTS Where fpdcont >=''' +
	@fpdcont + ''' and salesteam=''' + @salesteam + ''' order by rec_mo'
	
	
	Set @ParamDefinition = '@fpdcont date,
							@salesteam  varchar(20),
							@Campaign varchar(20)'
	
	PRINT @SQLQuery
	EXEC(@SQLQuery)
     
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
	
END
