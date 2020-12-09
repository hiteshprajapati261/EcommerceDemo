-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get all product attributes based on category
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