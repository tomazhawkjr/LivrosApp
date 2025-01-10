export interface ILivroRequest {
    id: number;
    dataCriacao: string; // Pode ser um tipo Date se preferir trabalhar com datas nativas
    titulo: string;
    editora: string;
    edicao: number;
    anoPublicacao: string;
    livroAssuntos: ILivroRequestLivroAssunto[];
    livroAutores: ILivroRequestLivroAutor[];
    livroValores: ILivroRequestLivroValor[];
  }
  
  export interface ILivroRequestLivroAssunto {
    idAssunto: number;
  }
  
  export interface ILivroRequestLivroAutor {
    idAutor: number;
  }
  
  export interface ILivroRequestLivroValor {
    idFormaCompra: number;
    valor: number;
  }
  