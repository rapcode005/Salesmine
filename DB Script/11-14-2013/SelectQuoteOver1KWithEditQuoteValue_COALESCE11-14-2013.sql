USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE]    Script Date: 11/14/2013 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
------------------------------------------------------------------      
-----------------------------------------------------------------------------------------      
      
-- =============================================            
-- Author:  <Author,,Name>            
-- Create date: <Create Date,,>            
-- Description: <Description,,>            
-- =============================================        
  -- [dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE] @Campaign='EMED',@Username='mohapasa',@Listview='lvwQuotePipeline'    
  
          
ALTER PROCEDURE [dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE]         
 -- Add the parameters for the stored procedure here            
 @Campaign varchar(16),        
 @Username char(10)='',      
 @Listview char(30)=''       
AS            
BEGIN            
 -- SET NOCOUNT ON added to prevent extra result sets from            
 -- interfering with SELECT statements.            
 SET NOCOUNT ON;          
             
    Declare @SQLQuery AS NVarchar(4000)            
    Declare @ParamDefinition AS NVarchar(2000)            
    DECLARE @cols as NVARCHAR(2000)       
            
                
            
  if(@Campaign='EMED')          
  Begin      
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')       
  FROM    EMED.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '' or @cols is not null)      
   Begin
   
    SET @SQLQuery = 'SELECT'+      
    @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
    FROM      
   (Select       
    status as ''SOURCE'',      
    Quote_Doc_createdon as ''QUOTE DAY'',      
    CUSMERGE as ''ACCOUNT NO.'',      
    Company_name as ''ACCOUNT NAME'',      
    Quote_Doc_No as ''QUOTE DOC'',            
    Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
    Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
    Quote_value as ''QUOTE VALUE'',      
    quote_cost as ''QUOTE COST'',      
    Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
    [Probablilty] as ''CLOSE PROBABILITY'',      
    Weighted as ''WEIGHTED VALUE'',      
    ProposedDate as ''PROPOSED DATE'',      
    Competition as ''COMPETITION'',      
    Notes as ''NOTES'',      
    sched_followup as ''SCHEDULE FOLLOWUP'',      
    product_line as ''PRODUCT LINE'',            
    construction as ''CONSTRUCTION'',      
    mining as ''MINING''       
    from EMED.QuotePipeline ) EMEDQuotePipeline order by [QUOTE ASSIGNMENT], [QUOTE VALUE] DESC'
          
    EXECUTE sp_executesql @SQLQuery       
       
   End      
   Else      
   Begin      
    Select        
    status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
     from EMED.QuotePipeline       
     order by Quote_SlsTeamIN , Quote_value DESC  
   End           
  End         
          
  Else if(@Campaign='CA' or  @cols is not null)          
  Begin      
      
         
       
   SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    CA.Column_Reorder where Username = @Username and Listview=@Listview      
 
       
       
  if(@cols <> '')      
    Begin      
  SET @SQLQuery = 'SELECT'+      
   @cols +',ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''         
   from CA.QuotePipeline       
   ) CAQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
         
    EXECUTE sp_executesql @SQLQuery    
      
 End      
  Else       
  Begin       
              
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
     from CA.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC     
   End           
  End            
          
  Else if(@Campaign='US' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    US.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from US.QuotePipeline ) EMEDQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from US.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End   
  
  -- UK
  Else if(@Campaign='UK' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    UK.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from UK.QuotePipeline ) UKQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from UK.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End              
  
  -- SUK  
  Else if(@Campaign='SUK' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    SUK.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from SUK.QuotePipeline ) SUKQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from SUK.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End   
 
 -- DE   
  Else if(@Campaign='DE' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    DE.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from DE.QuotePipeline ) SUKQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from DE.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End    
  
  -- CH
  Else if(@Campaign='CH' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    CH.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from CH.QuotePipeline ) SUKQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from CH.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End    
  
  -- AT
  Else if(@Campaign='AT' or @cols is not null)          
  Begin        
        
    SELECT  @cols = COALESCE( @cols + ',[' + LTRIM(RTRIM(ColumnName)) + ']',    
                         '[' + LTRIM(RTRIM(ColumnName)) + ']')      
  FROM    AT.Column_Reorder where Username = @Username and Listview=@Listview      
      
         
  if(@cols <> '')      
   Begin      
         
   SET @SQLQuery = 'SELECT'+      
   @cols +', ROW_NUMBER() OVER (ORDER BY [QUOTE DAY]) AS ROW      
   FROM      
  (Select       
   status as ''SOURCE'',      
   Quote_Doc_createdon as ''QUOTE DAY'',      
   CUSMERGE as ''ACCOUNT NO.'',      
   Company_name as ''ACCOUNT NAME'',      
   Quote_Doc_No as ''QUOTE DOC'',            
   Quote_SlsTeamIN as ''QUOTE ASSIGNMENT'',      
   Sales_Rep as ''ACCOUNT ASSIGNMENT'',      
   Quote_value as ''QUOTE VALUE'',      
   quote_cost as ''QUOTE COST'',      
   Quote_GM_perc as ''QUOTE GM PERCENTAGE'',      
   [Probablilty] as ''CLOSE PROBABILITY'',      
   Weighted as ''WEIGHTED VALUE'',      
   ProposedDate as ''PROPOSED DATE'',      
   Competition as ''COMPETITION'',      
   Notes as ''NOTES'',      
   sched_followup as ''SCHEDULE FOLLOWUP'',      
   product_line as ''PRODUCT LINE'',            
   construction as ''CONSTRUCTION'',      
   mining as ''MINING''       
   from AT.QuotePipeline ) SUKQuotePipeline order by [QUOTE ASSIGNMENT] , [QUOTE VALUE] DESC '      
       
    EXECUTE sp_executesql @SQLQuery       
   END      
      Else      
   Begin             
   Select       
       status as 'SOURCE',      
    Quote_Doc_createdon as 'QUOTE DAY',      
    CUSMERGE as 'ACCOUNT NO.',      
    Company_name as 'ACCOUNT NAME',      
    Quote_Doc_No as 'QUOTE DOC',            
    Quote_SlsTeamIN as 'QUOTE ASSIGNMENT',      
    Sales_Rep as 'ACCOUNT ASSIGNMENT',      
    Quote_value as 'QUOTE VALUE',      
    quote_cost as 'QUOTE COST',      
    Quote_GM_perc as 'QUOTE GM PERCENTAGE',      
    [Probablilty] as 'CLOSE PROBABILITY',      
    Weighted as 'WEIGHTED VALUE',      
    ProposedDate as 'PROPOSED DATE',      
    Competition as 'COMPETITION',      
    Notes as 'NOTES',      
    sched_followup as 'SCHEDULE FOLLOWUP',      
    product_line as 'PRODUCT LINE',            
    construction as 'CONSTRUCTION',      
    mining as 'MINING',      
    ROW_NUMBER() OVER (ORDER BY Quote_Doc_createdon) as ROW      
    from AT.QuotePipeline order by Quote_SlsTeamIN , Quote_value DESC
   End         
  End    
         
END      
      