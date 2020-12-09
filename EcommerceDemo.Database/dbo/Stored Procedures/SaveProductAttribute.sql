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