export interface ILivroDto {
    id: number;
    titulo: string;
    editora: string;
    edicao: number;
    anoPublicacao: string;
    dataCriacao: string;
    assuntos: ILivroDtoAssunto[];
    autores: ILivroDtoAutor[];
    valores: ILivroDtoValor[];
  }
  
  export interface ILivroDtoAssunto {
    id: number;
    descricao: string;
  }
  
  export interface ILivroDtoAutor {
    id: number;
    nome: string;
  }
  
  export interface ILivroDtoValor {
    valor: number;
    denominacaoFormaCompra: string;
    idFormaCompra: string;
  }
  