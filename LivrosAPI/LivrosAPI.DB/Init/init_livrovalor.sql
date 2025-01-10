-- Inserção de valores para livros em diferentes formas de compra
INSERT INTO [dbo].[LivroValor] (IdLivro, IdFormaCompra, Valor, DataCriacao) VALUES
(1, 1, 39.90, GETDATE()),  -- Livro 1, FormaCompra "Internet", Valor R$39,90
(1, 2, 49.90, GETDATE()),  -- Livro 1, FormaCompra "Livraria", Valor R$49,90
(2, 1, 29.90, GETDATE()),  -- Livro 2, FormaCompra "Internet", Valor R$29,90
(2, 3, 35.00, GETDATE()),  -- Livro 2, FormaCompra "Feira de Livros", Valor R$35,00
(3, 2, 44.90, GETDATE()),  -- Livro 3, FormaCompra "Livraria", Valor R$44,90
(3, 4, 40.00, GETDATE()),  -- Livro 3, FormaCompra "Assinatura de Revista", Valor R$40,00
(4, 1, 59.90, GETDATE()),  -- Livro 4, FormaCompra "Internet", Valor R$59,90
(4, 2, 69.90, GETDATE()),  -- Livro 4, FormaCompra "Livraria", Valor R$69,90
(5, 1, 24.90, GETDATE()),  -- Livro 5, FormaCompra "Internet", Valor R$24,90
(5, 3, 29.90, GETDATE()),  -- Livro 5, FormaCompra "Feira de Livros", Valor R$29,90
(6, 2, 44.90, GETDATE()),  -- Livro 6, FormaCompra "Livraria", Valor R$44,90
(6, 4, 50.00, GETDATE()),  -- Livro 6, FormaCompra "Assinatura de Revista", Valor R$50,00
(7, 1, 39.90, GETDATE()),  -- Livro 7, FormaCompra "Internet", Valor R$39,90
(7, 2, 49.90, GETDATE()),  -- Livro 7, FormaCompra "Livraria", Valor R$49,90
(8, 3, 25.00, GETDATE()),  -- Livro 8, FormaCompra "Feira de Livros", Valor R$25,00
(8, 4, 30.00, GETDATE()),  -- Livro 8, FormaCompra "Assinatura de Revista", Valor R$30,00
(9, 1, 32.90, GETDATE()),  -- Livro 9, FormaCompra "Internet", Valor R$32,90
(9, 2, 39.90, GETDATE()),  -- Livro 9, FormaCompra "Livraria", Valor R$39,90
(10, 1, 45.90, GETDATE()), -- Livro 10, FormaCompra "Internet", Valor R$45,90
(10, 3, 50.00, GETDATE()), -- Livro 10, FormaCompra "Feira de Livros", Valor R$50,00
(11, 2, 39.90, GETDATE()), -- Livro 11, FormaCompra "Livraria", Valor R$39,90
(11, 4, 42.50, GETDATE()), -- Livro 11, FormaCompra "Assinatura de Revista", Valor R$42,50
(12, 1, 49.90, GETDATE()), -- Livro 12, FormaCompra "Internet", Valor R$49,90
(12, 2, 55.00, GETDATE()), -- Livro 12, FormaCompra "Livraria", Valor R$55,00
(13, 1, 38.90, GETDATE()), -- Livro 13, FormaCompra "Internet", Valor R$38,90
(13, 3, 42.90, GETDATE()), -- Livro 13, FormaCompra "Feira de Livros", Valor R$42,90
(14, 2, 48.00, GETDATE()), -- Livro 14, FormaCompra "Livraria", Valor R$48,00
(14, 4, 51.00, GETDATE()), -- Livro 14, FormaCompra "Assinatura de Revista", Valor R$51,00
(15, 1, 60.00, GETDATE()), -- Livro 15, FormaCompra "Internet", Valor R$60,00
(15, 3, 65.00, GETDATE()), -- Livro 15, FormaCompra "Feira de Livros", Valor R$65,00
(16, 2, 52.00, GETDATE()), -- Livro 16, FormaCompra "Livraria", Valor R$52,00
(16, 4, 57.00, GETDATE()), -- Livro 16, FormaCompra "Assinatura de Revista", Valor R$57,00
(17, 1, 37.50, GETDATE()), -- Livro 17, FormaCompra "Internet", Valor R$37,50
(17, 2, 43.00, GETDATE()), -- Livro 17, FormaCompra "Livraria", Valor R$43,00
(18, 3, 30.00, GETDATE()), -- Livro 18, FormaCompra "Feira de Livros", Valor R$30,00
(18, 4, 35.00, GETDATE()), -- Livro 18, FormaCompra "Assinatura de Revista", Valor R$35,00
(19, 1, 50.00, GETDATE()), -- Livro 19, FormaCompra "Internet", Valor R$50,00
(19, 2, 55.90, GETDATE()), -- Livro 19, FormaCompra "Livraria", Valor R$55,90
(20, 3, 28.90, GETDATE()); -- Livro 20, FormaCompra "Feira de Livros", Valor R$28,90
