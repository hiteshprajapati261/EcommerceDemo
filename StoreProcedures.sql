USE [ECommerceDemo]
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get all categories
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCategories]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    -- Insert statements for procedure here
	SELECT ProdCatId AS Id
	,CategoryName  AS Name
	FROM [dbo].[ProductCategory]
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProductAttributeLookups]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get all products
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProductAttributeLookups]
	-- Add the parameters for the stored procedure here
	@CategoryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    -- Insert statements for procedure here
	SELECT AttributeId
		,AttributeName
	FROM [dbo].[ProductAttributeLookup] where ProdCatId = @CategoryId
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get all products
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProducts]
	-- Add the parameters for the stored procedure here
	@SearchText VARCHAR(50) = NULL
	,@RecordStart INT  = 0
    ,@PageSize INT  = 1000
    ,@SortColumn VARCHAR(50) = NULL
    ,@SortOrder VARCHAR(4) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	IF(@SearchText='')
	BEGIN
		SET @SearchText= NULL
	END
    -- Insert statements for procedure here
	SELECT P.ProductId
		,P.ProdCatId
		,PC.CategoryName
		,P.ProdName
		,P.ProdDescription
		,COUNT(1) OVER ( ) AS TotalCount
	FROM [dbo].[Product] P
	INNER JOIN [dbo].[ProductCategory] PC ON PC.ProdCatId = P.ProdCatId
	WHERE ((@SearchText IS NULL OR (@SearchText IS NOT NULL AND P.ProdName like '%' + @SearchText +'%'))
	OR
	(@SearchText IS NULL OR (@SearchText IS NOT NULL AND PC.CategoryName like '%' + @SearchText +'%'))
	OR
	(@SearchText IS NULL OR (@SearchText IS NOT NULL AND P.ProdDescription like '%' + @SearchText +'%')))
	ORDER BY P.ProductId DESC
	OFFSET @RecordStart ROWS FETCH NEXT @PageSize ROWS ONLY  
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get product by Id
-- =============================================
CREATE PROCEDURE [dbo].[GetProductById]
	@ProductId BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Product WHERE ProductId = @ProductId
END

GO
/****** Object:  StoredProcedure [dbo].[SaveProduct]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
GO
/****** Object:  StoredProcedure [dbo].[SaveProductAttribute]    Script Date: 12/9/2020 10:39:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Save Attibute
-- =============================================
CREATE PROCEDURE [dbo].[SaveProductAttribute]
(	
	@ProductId BIGINT
	,@AttributeId INT
	,@AttributeValue VARCHAR(250)
)    
AS
BEGIN			
	
	DELETE FROM ProductAttribute where ProductId = @ProductId AND AttributeId = @AttributeId

	INSERT INTO ProductAttribute VALUES(@ProductId,@AttributeId,@AttributeValue)	
	
END
GO


/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 09-12-2020 12:57:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteProduct]
(	
	@ProductId BIGINT
)    
AS
BEGIN			
		DELETE FROM ProductAttribute where ProductId = @ProductId
		DELETE FROM Product  where ProductId = @ProductId
END
GO

/****** Object:  StoredProcedure [dbo].[GetProductAttributesById]    Script Date: 09-12-2020 12:57:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get Product Attributes by Id
-- =============================================
CREATE PROCEDURE [dbo].[GetProductAttributesById]
	@ProductId BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM ProductAttribute WHERE ProductId = @ProductId
END

GO

