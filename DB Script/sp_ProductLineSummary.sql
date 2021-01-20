USE [SalesMine]
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductLineSummary]    Script Date: 09/20/2013 16:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
ALTER Procedure [dbo].[sp_ProductLineSummary]
    /* Input Parameters */
  (  @CampaignName varchar(100),
    @SoldTo Varchar(100),
    @BuyerCt DECIMAL(18))
           
AS
BEGIN
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS Varchar(4000)
    Declare @ParamDefinition AS Varchar(2000) 
   
   IF @CampaignName = 'PC' 
		BEGIN
		
				--Set @SQLQuery = 'SELECT sku_family
				--						,max(b.revision_date) as last_revision_date
				--						, sum(sales_3fy_ago) as sales_3fy_ago
				--						,sum(sales_2fy_ago) as sales_2fy_ago
				--						,sum(sales_1fy_ago) as sales_1fy_ago
				--						,sum(sales_currfy) as sales_currfy
				--						,sum(Total_sales) as Total_sales
				--						,sum(NO_orders) as NO_orders
				--						,max(last_order_date) as last_order_date
				--						,Cast(sum(units_3fy_ago) as Numeric(10,2)) as units_3fy_ago
				--						,Cast(sum(units_2fy_ago) as Numeric(10,2)) as units_2fy_ago
				--						,Cast(sum(units_1fy_ago) as Numeric(10,2)) as units_1fy_ago
				--						,Cast(sum(units_currfy) as Numeric(10,2)) as units_currfy 
				--				From PC.SKU_VIEW a left join PC.REVISION_DATE b 
				--				on a.SKU_Number = b.mat_entered where (sold_to = ' + @SoldTo + ')' 
				
				
				Set @SQLQuery = 'SELECT sku_family AS [PRODUCT FAMILY]
										,max(b.revision_date) as [LAST REVISION DATE]
										, sum(sales_3fy_ago) as [F11 SALES]
										,sum(sales_2fy_ago) as [F12 SALES]
										,sum(sales_1fy_ago) as [F13 SALES]
										,sum(sales_currfy) as [F14 SALES]
										,sum(Total_sales) as [TOTAL SALES]
										,sum(NO_orders) as [LIFETIME ORDERS]
										,max(last_order_date) as [LAST ORDER DATE]
										,Cast(sum(units_3fy_ago) as Numeric(10,2)) as [F11 UNITS]
										,Cast(sum(units_2fy_ago) as Numeric(10,2)) as [F12 UNITS]
										,Cast(sum(units_1fy_ago) as Numeric(10,2)) as [F13 UNITS]
										,Cast(sum(units_currfy) as Numeric(10,2)) as [F14 UNITS] 
								From PC.SKU_VIEW a left join PC.REVISION_DATE b 
								on a.SKU_Number = b.mat_entered where (sold_to = ' + @SoldTo + ')' 
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_family order by [TOTAL SALES] desc'
			         
	
		   END
    ELSE
		   BEGIN
				Set @SQLQuery = 'SELECT sku_category as [SKU CATEGORY]
										,sum(sales_3fy_ago) as [F11 SALES]
										,sum(sales_2fy_ago) as [F12 SALES]
										,sum(sales_1fy_ago) as [F13 SALES]
										,sum(sales_currfy) as [F14 SALES]
										,sum(Total_sales) AS [TOTAL SALES]
										,sum(NO_orders) as [LIFETIME ORDERS]
										,max(last_order_date) as [LAST ORDER DATE]
										,Cast(sum(units_3fy_ago) as Numeric(10,2)) as [F11 UNITS]
										,Cast(sum(units_2fy_ago) as Numeric(10,2)) as [F12 UNITS]
										,Cast(sum(units_1fy_ago) as Numeric(10,2)) as [F13 UNITS]
										,Cast(sum(units_currfy) as Numeric(10,2)) as [F14 UNITS]
								From  ' +  @CampaignName +'.SKU_VIEW where (sold_to = ' + @SoldTo + ')' 
				 print @BuyerCt
				If @BuyerCt Is Not Null
					 Set @SQLQuery = @SQLQuery + ' And BUYERCT = ' + CONVERT (VARCHAR(20),@BuyerCt)
			  
			  Set @SQLQuery = @SQLQuery + ' group by sku_category order by [TOTAL SALES] desc'
			         
		   END
		   
		   
   			
			         
	   
       Set @ParamDefinition =      ' @CampaignName char(10),
											  @SoldTo char(10),
												@BUYERCT DECIMAL(18)'
PRINT @SQLQuery
EXEC(@SQLQuery)

--Execute sp_Executesql     @SQLQuery, 
--                @ParamDefinition, 
--				@CampaignName,
--                @SoldTo, 
--				@BUYERCT										
   
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
    
    end

