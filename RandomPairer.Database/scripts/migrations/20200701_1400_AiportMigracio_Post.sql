UPDATE Airport
SET Airport.Type = #airportTmp.Size
FROM
	Airport
	JOIN #airportTmp on #airportTmp.Id = Airport.Id

DROP TABLE #airportTmp