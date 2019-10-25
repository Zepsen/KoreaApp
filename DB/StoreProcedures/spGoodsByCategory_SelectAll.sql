Alter PROCEDURE [dbo].[spGoodsByCategory_SelectAll]	
@CategoryId INT,
@Take INT,
@Skip INT,
@Search VARCHAR(100)
AS
BEGIN
	SELECT * FROM Good g
	WHERE CategoryId = @CategoryId
	AND (@Search IS NULL OR g.Title Like ('%' + @Search + '%'))
	ORDER BY g.Id
	OFFSET @Skip ROWS
	FETCH NEXT @Take ROWS only	

	SELECT COUNT(Id)  FROM Good g
	WHERE CategoryId = @CategoryId 
	AND (@Search IS NULL OR g.Title Like ('%' + @Search + '%'))
END	

