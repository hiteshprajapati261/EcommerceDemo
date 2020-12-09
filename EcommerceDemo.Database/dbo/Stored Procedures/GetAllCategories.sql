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