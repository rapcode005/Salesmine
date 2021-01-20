
GO
/****** Object:  StoredProcedure [dbo].[sp_CustomerLookup]    Script Date: 10/14/2013 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_CustomerLookup]

@filterby nvarchar(max),
@filtertype nvarchar(max),
@filtertxt nvarchar(max)
as

Declare @lookUpQuery nvarchar(max)

set @lookUpQuery='select
	  [CONTACT_STATUS] as [CONTACT STATUS]
	  ,[cusmerge] as [ACCOUNT NUM]
	  ,[NAME] as [ACCOUNT NAME]
	  ,[MG_NAME] as [MANAGED GROUP]
	  ,[BuyerOrg_Desc] as [BUYER ORG]
	  ,[KAM_Mgr] as [KAM NAME]
      ,[contmerg] as [CONTACT NUM]
      ,[FIRSTNAME] + '' '' + [SURNAME] as [CONTACT NAME]
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
	
	set  @filtertype='firstname + '' '' + surname like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Managed Group'
	begin
	
	set  @filtertype='MG_Name like ''%'+ @filtertxt + '%'''

	end

if @filtertype='Buyer Org'
	begin
	
	set  @filtertype='BuyerOrg_Desc like ''%'+ @filtertxt + '%'''

	end

set @lookUpQuery=@lookUpQuery + @filtertype + ' order by [rec_mo]'

print @lookUpQuery
exec (@lookUpQuery)

