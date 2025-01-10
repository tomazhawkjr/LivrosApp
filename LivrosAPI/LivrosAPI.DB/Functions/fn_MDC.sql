/*
  Input: [15,30,40]
*/
CREATE FUNCTION dbo.fn_MDC (@Numbers NVARCHAR(MAX))
RETURNS INT
AS
BEGIN
    DECLARE @GCD INT = NULL;
    DECLARE @Number INT;

    -- Declara um cursor para iterar sobre os números fornecidos
    DECLARE NumberCursor CURSOR LOCAL FOR
    SELECT CAST(value AS INT) AS Number
    FROM OPENJSON(@Numbers)
    WHERE ISNUMERIC(value) = 1;

    OPEN NumberCursor;
    FETCH NEXT FROM NumberCursor INTO @Number;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @GCD IS NULL
            SET @GCD = @Number;
        ELSE
            SET @GCD = dbo.fn_GCD(@GCD, @Number);

        FETCH NEXT FROM NumberCursor INTO @Number;
    END

    CLOSE NumberCursor;
    DEALLOCATE NumberCursor;

    RETURN @GCD;
END