-- =============================================
-- Author:		Hitesh Prajapati
-- Create date: 9-Dec-2020
-- Description:	Get product by Id
-- =============================================
CREATE PROCEDURE GetProductById
	@ProductId BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Product WHERE ProductId = @ProductId
END