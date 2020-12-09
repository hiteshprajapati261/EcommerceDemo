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