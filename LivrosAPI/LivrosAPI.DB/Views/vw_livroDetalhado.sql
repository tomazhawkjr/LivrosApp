CREATE VIEW vw_LivroDetalhado
AS
SELECT 
    l.Id AS IdLivro,
    l.Titulo,
    l.Editora,
    l.Edicao,
    l.AnoPublicacao,
    l.DataCriacao AS DataCriacaoLivro,
    a.Id AS IdAutor,
    a.Nome AS NomeAutor,
    s.Id AS IdAssunto,
    s.Descricao AS DescricaoAssunto,
    lv.Valor AS ValorLivro,
    fc.Denominacao AS DenominacaoFormaCompra,
    fc.Id as IdFormaCompra
FROM Livro l
LEFT JOIN LivroAutor la ON l.Id = la.IdLivro
LEFT JOIN Autor a ON la.IdAutor = a.Id
LEFT JOIN LivroAssunto las ON l.Id = las.IdLivro
LEFT JOIN Assunto s ON las.IdAssunto = s.Id
LEFT JOIN LivroValor lv ON l.Id = lv.IdLivro
LEFT JOIN FormaCompra fc ON lv.IdFormaCompra = fc.Id;
