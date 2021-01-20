
GO
/****** Object:  StoredProcedure [dbo].[sp_NotesDialer]    Script Date: 10/22/2013 21:19:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_NotesDialer]
   
@ACCOUNT nvarchar(10),    
@UNIT nvarchar(10),    
@CONTACT nvarchar(10) =''   
AS    
BEGIN    
  Set NoCount ON    
	--	SET @ACCOUNT='0001076819'
	--	SET @UNIT='EMED'
	
 Declare @ParamDefinition AS Varchar(2000)     
    Declare @SQLQuery AS NVarchar(4000)    
   
     
 if @CONTACT is null    
 begin    
 set @CONTACT=''    
 end    
     
     
 if @UNIT = 'PC'    
 begin    
   SELECT createdon as [DATE]    
       ,createdby as [CREATED BY]    
       ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
       ,x.notes as [NOTES]    
       ,convert(nvarchar, [date],101) as [SCHEDULED DATE]    
       ,x.customer as [ACCOUNT NUMBER]    
       ,s.SURNAME + ', '+ s.FIRSTNAME as [CONTACT NAME]     
       ,x.contact as [CONTACT NUMBER]    
       ,null as [DISPOSITION CODE]    
       ,null as [DISPOSITION DESCRIPTION]     
       ,null as [AGENT]    
       ,null as [PHONE NUMBER]     
     --From PC.ALL_NOTES  x left join (select contact,surname,firstname from PC.Continfo) s  on x.contact=s.contact     
     From PC.ALL_NOTES  x left join PC.Continfo s  on x.contact=s.contact    
        where x.customer = @ACCOUNT    
        UNION All    
      select  CALDAY as [Created_on]    
        , SLTEAMIN as [Created_by]    
        , upper(Note_type) as [Note_Type]    
        ,NOTES as [Notes]    
        ,null as [Date]    
        ,customer as [Account_Number]    
        ,'' as [Contact_Name]    
        ,contact as [Contact_Number]    
        ,a.DISP_CD as [Disposition_Code]    
        ,DESCRIPTION  as [DISPOSITION DESCRIPTION]     
        ,'' as [AGENT NAME]    
        ,'' as [PHONE NUMBER]     
        from DISP_HISTORY a     
      Inner join disp_desc b on (a.DISP_CD=b.Code)      
      where customer =  @ACCOUNT and UNIT ='pc'    
       and customer is not null and len(customer) > 0 order by createdon desc
        
  
        
 end    
     
 if @UNIT <>'PC' and @CONTACT <> ''    
 begin    
     
 set @SQLQuery = 'SELECT createdon as [DATE]    
       ,createdby as [CREATED BY]    
       ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
       ,x.notes as [NOTES]    
       ,convert(nvarchar, [date],101) as [SCHEDULED DATE]    
       ,x.customer as [ACCOUNT NUMBER]    
       ,s.SURNAME + '', ''+ s.FIRSTNAME as [CONTACT NAME]     
       ,x.contact as [CONTACT NUMBER]    
       ,null as [DISPOSITION CODE]    
       ,null as [DISPOSITION DESCRIPTION]     
       ,null as [AGENT NAME]    
       ,null as [PHONE NUMBER]     
     From ' + @UNIT + '.ALL_NOTES x left join ' + @UNIT + '.Continfo s  on x.contact=s.contact inner join    
      (select * from ' + @UNIT + '.SITEINFO) c on x.customer=c.sold_to    
        where c.sold_to =  @ACCOUNT  AND x.contact= @CONTACT      
        UNION     
      select CALDAY as [Created_on]    
        , SLTEAMIN as [Created_by]    
        , upper(Note_type) as [Note_Type]    
        ,NOTES as [Notes]    
        ,null as [Date]    
        ,customer as [Account_Number]    
        ,'''' as [Contact_Name]    
        ,'''' as [Contact_Number]    
        ,a.DISP_CD as [Disposition_Code]    
        ,DESCRIPTION    
        ,'''' as [AGENT NAME]    
        ,'''' as [PHONE NUMBER]     
      from DISP_HISTORY a     
      inner join disp_desc b on (a.DISP_CD=b.Code)      
      where customer =@ACCOUNT and UNIT =  @UNIT   AND contact=  @CONTACT      
      UNION     
      select createdon as [Created_on]    
        ,createdby as [Created_by]    
        ,''PROJECT'' as [Note_Type]    
        ,(''Project Type:'' + RTRIM(LTRIM(project_type))     
        + ''  '' + ''Chance:'' + RTRIM(LTRIM(chance)) + ''  ''     
        + ''Estimated Amount:'' + Cast(estimated_amt as varchar(50))) as [Notes]    
        ,Project_Date as [Date]    
        ,v.Sold_to as [Account_Number]    
        ,'''' as [Contact_Name]    
        ,''''as [Contact_Number]    
        ,null as [Disposition_Code]    
        ,null as [Disposition_Description]     
        ,'''' as [AGENT NAME]    
        ,'''' as [PHONE NUMBER]     
      from CUSTOMER_PROJECTS v  left join    
      (select * from ' + @UNIT + '.SITEINFO) c on v.SOLD_TO=c.sold_to    
        where c.sold_to =  @ACCOUNT   AND buyerct=  @CONTACT     
   and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc ';    
       
  --  Execute(@SQLQuery)   
   
   
  
  
EXECUTE sp_executesql @SQLQuery, N'@UNIT NVARCHAR(10),@CONTACT NVARCHAR(10),@ACCOUNT NVARCHAR(10) ', @UNIT ,@CONTACT,@ACCOUNT  
  
  
       
  end    
    
ELSE IF @UNIT <>'PC'and @CONTACT =''    
  BEGIN    
      
      
  set @SQLQuery = 'SELECT createdon as [DATE]    
          ,createdby as [CREATED BY]    
          ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
          ,x.notes as [NOTES]    
          ,date as [SCHEDULED DATE]    
          ,x.customer as [ACCOUNT NUMBER]    
          ,s.SURNAME + '', ''+ s.FIRSTNAME as [CONTACT NAME]     
          ,x.contact as [CONTACT NUMBER]    
          ,null as [DISPOSITION CODE]    
          ,null as [DISPOSITION DESCRIPTION]    
          ,'''' as [AGENT NAME]    
          ,'''' as [PHONE NUMBER]     
        From ' + @UNIT + '.ALL_NOTES  x left join ' + @UNIT + '.Continfo s  on x.contact=s.contact inner join    
         (select * from ' + @UNIT + '.SITEINFO) c on x.customer=c.sold_to    
           where c.sold_to =  @ACCOUNT       
           UNION     
         select CALDAY as [Created_on]    
           , SLTEAMIN as [Created_by]    
           , upper(Note_type) as [Note_Type]    
           , NOTES as [Notes]    
           , null as [Date]    
           , customer as [Account_Number]    
           , '''' as [Contact_Name]    
           , null as [Contact_Number]    
           , a.DISP_CD as [Disposition_Code]    
           , DESCRIPTION    
           , '''' as [AGENT NAME]    
           , '''' as [PHONE NUMBER]     
         from DISP_HISTORY a     
         inner join disp_desc b on (a.DISP_CD=b.Code)      
         where customer =  @ACCOUNT  and UNIT =  @UNIT     
         UNION     
         select  createdon as [Created_on]    
           ,createdby as [Created_by]    
           ,''PROJECT'' as [Note_Type]    
           ,(''Project Type:'' + RTRIM(LTRIM(project_type))     
           + ''  '' + ''Chance:'' + RTRIM(LTRIM(chance)) + ''  ''     
           + ''Estimated Amount:'' + Cast(estimated_amt as varchar(50))) as [Notes]    
           ,Project_Date as [Date]    
           ,v.Sold_to as [Account_Number]    
           ,'''' as [Contact_Name]    
           ,null as [Contact_Number]    
           ,null as [Disposition_Code]    
           ,null as [Disposition_Description]     
           ,'''' as [AGENT NAME]    
           ,'''' as [PHONE NUMBER]    
         from CUSTOMER_PROJECTS v  left join           
         (select * from ' + @UNIT + '.SITEINFO) c on v.SOLD_TO=c.sold_to    
           where c.sold_to =  @ACCOUNT     
         and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc ';   
           
           
  EXECUTE sp_executesql @SQLQuery, N'@UNIT NVARCHAR(10),@CONTACT NVARCHAR(10),@ACCOUNT NVARCHAR(10) ', @UNIT ,@CONTACT,@ACCOUNT  
         
  
  
  
  
           
            
    
     
  End     
       
       
   ----UK---    
       
       
   if @UNIT ='UK' and @CONTACT <> ''    
 begin    
 SELECT  createdon as [DATE]    
       ,createdby as [CREATED BY]    
       ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
       ,x.notes as [NOTES]    
       ,convert(nvarchar, [date],101) as [SCHEDULED DATE]    
       ,x.customer as [ACCOUNT NUMBER]    
       ,s.SURNAME + ', '+ s.FIRSTNAME as [CONTACT NAME]     
       ,x.contact as [CONTACT NUMBER]    
       ,null as [DISPOSITION CODE]    
       ,null as [DISPOSITION DESCRIPTION]     
       ,null as [AGENT NAME]    
       ,null as [PHONE NUMBER]     
     From UK.ALL_NOTES x left join UK.Continfo s  on x.contact=s.contact inner join    
      (select * from UK.SITEINFO) c on x.customer=c.sold_to    
        where c.sold_to = @ACCOUNT   AND x.contact=   @CONTACT      
        UNION     
      select CALDAY as [Created_on]    
        , SLTEAMIN as [Created_by]    
        , upper(Note_type) as [Note_Type]    
        ,NOTES as [Notes]    
        ,null as [Date]    
        ,customer as [Account_Number]    
        ,'' as [Contact_Name]    
        ,'' as [Contact_Number]    
        ,a.DISP_CD as [Disposition_Code]    
        ,DESCRIPTION    
        ,'' as [AGENT NAME]    
        ,'' as [PHONE NUMBER]     
      from DISP_HISTORY a     
      inner join disp_desc b on (a.DISP_CD=b.Code)      
      where customer =  @ACCOUNT  and UNIT = 'UK'  AND contact=   @CONTACT      
      UNION     
      select  createdon as [Created_on]    
        ,createdby as [Created_by]    
        ,'PROJECT' as [Note_Type]    
        ,('Project Type:' + RTRIM(LTRIM(project_type))     
        + '  ' + 'Chance:' + RTRIM(LTRIM(chance)) + '  '     
        + 'Estimated Amount:' + Cast(estimated_amt as varchar(50))) as [Notes]    
        ,Project_Date as [Date]    
        ,v.Sold_to as [Account_Number]    
        ,'' as [Contact_Name]    
        ,''as [Contact_Number]    
        ,null as [Disposition_Code]    
        ,null as [Disposition_Description]     
        ,'' as [AGENT NAME]    
        ,'' as [PHONE NUMBER]     
      from CUSTOMER_PROJECTS v  left join    
      (select * from UK.SITEINFO)c on v.SOLD_TO=c.sold_to    
        where c.sold_to =  @ACCOUNT   AND buyerct=  @CONTACT      
   and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc     
      
  end    
    
ELSE IF @UNIT ='UK'and @CONTACT =''    
  BEGIN    
      
  SELECT  createdon as [DATE]    
          ,createdby as [CREATED BY]    
          ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
          ,x.notes as [NOTES]    
          ,date as [SCHEDULED DATE]    
          ,x.customer as [ACCOUNT NUMBER]    
          ,s.SURNAME + ', '+ s.FIRSTNAME as [CONTACT NAME]     
          ,x.contact as [CONTACT NUMBER]    
          ,null as [DISPOSITION CODE]    
          ,null as [DISPOSITION DESCRIPTION]    
          ,'' as [AGENT NAME]    
          ,'' as [PHONE NUMBER]     
        From UK.ALL_NOTES  x left join UK.Continfo s  on x.contact=s.contact inner join    
         (select * from UK.SITEINFO) c on x.customer=c.sold_to    
           where c.sold_to =  @ACCOUNT       
           UNION     
         select  CALDAY as [Created_on]    
           , SLTEAMIN as [Created_by]    
           , upper(Note_type) as [Note_Type]    
           , NOTES as [Notes]    
           , null as [Date]    
           , customer as [Account_Number]    
           , '' as [Contact_Name]    
           , '' as [Contact_Number]    
           , a.DISP_CD as [Disposition_Code]    
           , DESCRIPTION    
           , '' as [AGENT NAME]    
           , '' as [PHONE NUMBER]     
         from DISP_HISTORY a     
         inner join disp_desc b on (a.DISP_CD=b.Code)      
         where customer =  @ACCOUNT  and UNIT = 'UK'    
         UNION     
         select createdon as [Created_on]    
           ,createdby as [Created_by]    
           ,'PROJECT' as [Note_Type]    
           ,('Project Type:' + RTRIM(LTRIM(project_type))     
           + '  ' + 'Chance:' + RTRIM(LTRIM(chance)) + '  '     
           + 'Estimated Amount:' + Cast(estimated_amt as varchar(50))) as [Notes]    
           ,Project_Date as [Date]    
           ,v.Sold_to as [Account_Number]    
           ,'' as [Contact_Name]    
           ,'' as [Contact_Number]    
           ,null as [Disposition_Code]    
           ,null as [Disposition_Description]     
           ,'' as [AGENT NAME]    
           ,'' as [PHONE NUMBER]    
         from CUSTOMER_PROJECTS v  left join    
         (select * from UK.SITEINFO) c on v.SOLD_TO=c.sold_to    
           where c.sold_to = @ACCOUNT      
         and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc     
      
     END    
     
      if @UNIT ='SUK' and @CONTACT <> ''    
 begin    
 SELECT  createdon as [DATE]    
       ,createdby as [CREATED BY]    
       ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
       ,x.notes as [NOTES]    
       ,convert(nvarchar, [date],101) as [SCHEDULED DATE]    
       ,x.customer as [ACCOUNT NUMBER]    
       ,s.SURNAME + ', '+ s.FIRSTNAME as [CONTACT NAME]     
       ,x.contact as [CONTACT NUMBER]    
       ,null as [DISPOSITION CODE]    
       ,null as [DISPOSITION DESCRIPTION]     
       ,null as [AGENT NAME]    
       ,null as [PHONE NUMBER]     
     From SUK.ALL_NOTES x left join SUK.Continfo s  on x.contact=s.contact inner join    
      (select * from SUK.SITEINFO) c on x.customer=c.sold_to    
        where c.sold_to = @ACCOUNT   AND x.contact=   @CONTACT      
        UNION     
      select CALDAY as [Created_on]    
        , SLTEAMIN as [Created_by]    
        , upper(Note_type) as [Note_Type]    
        ,NOTES as [Notes]    
        ,null as [Date]    
        ,customer as [Account_Number]    
        ,'' as [Contact_Name]    
        ,'' as [Contact_Number]    
        ,a.DISP_CD as [Disposition_Code]    
        ,DESCRIPTION    
        ,'' as [AGENT NAME]    
        ,'' as [PHONE NUMBER]     
      from DISP_HISTORY_DU a     
      inner join disp_desc b on (a.DISP_CD=b.Code)      
      where customer =  @ACCOUNT  and UNIT = 'SUK'  AND contact=   @CONTACT      
      UNION     
      select  createdon as [Created_on]    
        ,createdby as [Created_by]    
        ,'PROJECT' as [Note_Type]    
        ,('Project Type:' + RTRIM(LTRIM(project_type))     
        + '  ' + 'Chance:' + RTRIM(LTRIM(chance)) + '  '     
        + 'Estimated Amount:' + Cast(estimated_amt as varchar(50))) as [Notes]    
        ,Project_Date as [Date]    
        ,v.Sold_to as [Account_Number]    
        ,'' as [Contact_Name]    
        ,''as [Contact_Number]    
        ,null as [Disposition_Code]    
        ,null as [Disposition_Description]     
        ,'' as [AGENT NAME]    
        ,'' as [PHONE NUMBER]     
      from CUSTOMER_PROJECTS v  left join    
      (select * from SUK.SITEINFO)c on v.SOLD_TO=c.sold_to    
        where c.sold_to =  @ACCOUNT   AND buyerct=  @CONTACT      
   and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc     
      
  end    
    
ELSE IF @UNIT ='SUK'and @CONTACT =''    
  BEGIN    
      
  SELECT  createdon as [DATE]    
          ,createdby as [CREATED BY]    
          ,upper(LTRIM(RTRIM([type]))) as [NOTE TYPE]    
          ,x.notes as [NOTES]    
          ,date as [SCHEDULED DATE]    
          ,x.customer as [ACCOUNT NUMBER]    
          ,s.SURNAME + ', '+ s.FIRSTNAME as [CONTACT NAME]     
          ,x.contact as [CONTACT NUMBER]    
          ,null as [DISPOSITION CODE]    
          ,null as [DISPOSITION DESCRIPTION]    
          ,'' as [AGENT NAME]    
          ,'' as [PHONE NUMBER]     
        From SUK.ALL_NOTES  x left join SUK.Continfo s  on x.contact=s.contact inner join    
         (select * from SUK.SITEINFO) c on x.customer=c.sold_to    
           where c.sold_to =  @ACCOUNT       
           UNION     
         select  CALDAY as [Created_on]    
           , SLTEAMIN as [Created_by]    
           , upper(Note_type) as [Note_Type]    
           , NOTES as [Notes]    
           , null as [Date]    
           , customer as [Account_Number]    
           , '' as [Contact_Name]    
           , '' as [Contact_Number]    
           , a.DISP_CD as [Disposition_Code]    
           , DESCRIPTION    
           , '' as [AGENT NAME]    
           , '' as [PHONE NUMBER]     
         from DISP_HISTORY_DU a     
         inner join disp_desc b on (a.DISP_CD=b.Code)      
         where customer =  @ACCOUNT  and UNIT = 'SUK'    
         UNION     
         select createdon as [Created_on]    
           ,createdby as [Created_by]    
           ,'PROJECT' as [Note_Type]    
           ,('Project Type:' + RTRIM(LTRIM(project_type))     
           + '  ' + 'Chance:' + RTRIM(LTRIM(chance)) + '  '     
           + 'Estimated Amount:' + Cast(estimated_amt as varchar(50))) as [Notes]    
           ,Project_Date as [Date]    
           ,v.Sold_to as [Account_Number]    
           ,'' as [Contact_Name]    
           ,'' as [Contact_Number]    
           ,null as [Disposition_Code]    
           ,null as [Disposition_Description]     
           ,'' as [AGENT NAME]    
           ,'' as [PHONE NUMBER]    
         from CUSTOMER_PROJECTS v  left join    
         (select * from SUK.SITEINFO) c on v.SOLD_TO=c.sold_to    
           where c.sold_to = @ACCOUNT      
         and v.Sold_to is not null and len(v.Sold_to) > 0 order by createdon desc     
      
     END   
        
        
        
        
        
 --SELECT @SQLQuery    
     
--Set @ParamDefinition =      '@ACCOUNT nvarchar(10),    
--       @UNIT nvarchar(10)'    
    
   
    
--   If @@ERROR <> 0 GoTo ErrorHandler    
--  Set NoCount OFF    
--    Return(0)    
      
ErrorHandler:    
    Return(@@ERROR)    
        
END    
