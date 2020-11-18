CREATE TABLE #airportTmp
(
	[Id]	INT,
	[Size]	CHAR
)

INSERT INTO #airportTmp
SELECT
	Airport.Id,
	CASE
		WHEN Airport.Type = 1 THEN 'S'
		WHEN Airport.Type = 2 THEN 'M'
		WHEN Airport.Type = 3 THEN 'L'
	END AS Size
FROM Airport