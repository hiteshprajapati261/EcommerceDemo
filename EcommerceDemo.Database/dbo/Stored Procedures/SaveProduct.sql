-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Save Product
-- =============================================
CREATE PROCEDURE [dbo].[SaveProduct]
(	
	@ProductId BIGINT
	,@ProdCatId INT
	,@ProdName VARCHAR(250)
	,@ProdDescription VARCHAR(MAX)
)    
AS
BEGIN			
	
	
	MERGE [dbo].[Product] AS TARGET
	USING
	( SELECT  @ProductId  		
			,@ProdCatId
			,@ProdName
			,@ProdDescription			
	) 
	AS source
	( 
		      ProductId  		
			,ProdCatId
			,ProdName
			,ProdDescription		
	)
	ON ( TARGET.ProductId = SOURCE.ProductId )
	WHEN MATCHED THEN
	UPDATE SET
		 [ProdName]		   = SOURCE.[ProdName]
		,[ProdCatId]	   = SOURCE.[ProdCatId]
		,[ProdDescription] = SOURCE.[ProdDescription]
	WHEN NOT MATCHED THEN
	INSERT
	(	
		   [ProdCatId]
		   ,[ProdName]
           ,[ProdDescription]           
	)
	VALUES 
	(
		 SOURCE.[ProdCatId]		
		,SOURCE.[ProdName]
		,SOURCE.[ProdDescription]
	);		

	IF COALESCE(@ProductId, 0) = 0
	BEGIN
		SET  @ProductId = SCOPE_IDENTITY();
	END

	SELECT  @ProductId
END