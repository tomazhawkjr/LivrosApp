CREATE FUNCTION dbo.fn_GCD (@a INT, @b INT)
RETURNS INT
AS
BEGIN
    -- Trata valores negativos convertendo para positivos
    SET @a = ABS(@a);
    SET @b = ABS(@b);
    
    -- Implementação do Algoritmo de Euclides
    WHILE @b <> 0
    BEGIN
        DECLARE @temp INT = @b;
        SET @b = @a % @b;
        SET @a = @temp;
    END
    
    RETURN @a;
END