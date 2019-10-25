Alter PROCEDURE [dbo].[spGoodsByCategory_SelectAll]	
@CategoryId INT,
@Take INT,
@Skip INT
AS
BEGIN
	SELECT * FROM Good g
	WHERE CategoryId = @CategoryId
	ORDER BY g.Id
	OFFSET @Skip ROWS
	FETCH NEXT @Take ROWS only	

	SELECT COUNT(Id)  FROM Good g
	WHERE CategoryId = @CategoryId
END	

