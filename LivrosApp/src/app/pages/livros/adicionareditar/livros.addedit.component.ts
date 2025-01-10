import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ILivroDto, ILivroDtoValor } from 'src/app/models/dtos/livro.dto.model';
import { LivroProvider } from 'src/app/providers/livro.provider';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ILivroRequest, ILivroRequestLivroAssunto, ILivroRequestLivroAutor, ILivroRequestLivroValor } from 'src/app/models/Requests/livro.request.model';
import { IAssuntoDto } from 'src/app/models/dtos/assunto.dto.model';
import { IAutorDto } from 'src/app/models/dtos/autor.dto.model';
import { IFormaCompraDto } from 'src/app/models/dtos/formacompra.dto.model';
import { FormaCompraProvider } from 'src/app/providers/formacompra.provider';
import { AssuntoProvider } from 'src/app/providers/assunto.provider';
import { AutorProvider } from 'src/app/providers/autor.provider';
import { forkJoin } from 'rxjs';
import { MatSelectChange } from '@angular/material/select';


@Component({
  selector: 'app-livros-addedit',
  templateUrl: './livros.addedit.component.html',
  styleUrls: ['./livros.addedit.component.scss']
})
export class LivrosAddEditComponent implements OnInit {

  private FORM_VALORES = 'valores'; 
  private FORM_FORMASCOMPRA = 'formasCompra';

  livro: ILivroDto = {
    id: 0, 
    anoPublicacao: '',
    assuntos: [],
    autores: [],
    valores: [],
    dataCriacao: '',
    edicao: 0,
    editora: '',
    titulo: ''
  };
  livroForm!: FormGroup;
  listaAssuntos: IAssuntoDto[] = [];
  listaAutores: IAutorDto[] = [];
  listaValores: IFormaCompraDto[] = [];
  listaValoresSelecionados : number[] = [];
  Object: any;

  constructor(
    private readonly livroProvider: LivroProvider,
    private readonly formaCompraProvider: FormaCompraProvider,
    private readonly assuntosProvider: AssuntoProvider,
    private readonly autoresProvider: AutorProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private fb: FormBuilder
  ) { 
    
  }

  ngOnInit(): void {

    this.livro = history.state;

    this.populateListagens();

    this.livroForm = this.fb.group({
      titulo: [ this.livro.id ? this.livro.titulo : '', Validators.required],
      editora: [ this.livro.id ? this.livro.editora :'', Validators.required],
      edicao: [ this.livro.id ? this.livro.edicao : null, [Validators.required, Validators.min(1)]],
      anoPublicacao: [ this.livro.id ? this.livro.anoPublicacao :'', Validators.required],     
      assuntos: [this.livro.id ? this.livro.assuntos.map(a => a.id) : []],
      autores: [this.livro.id ? this.livro.autores.map(a => a.id) : []], 
      formasCompra: [this.livro.id ? this.livro.valores.map(a => a.idFormaCompra) : []],
      valores: this.fb.group({})
    });
  }

  onSubmit(): void {
    if (!this.livroForm.valid) {
      this.notification.error('Algum dado está incorreto.')
      return;
    }

    this.spinner.show();

    var livroRequest = this.createLivroRequest();

    (this.livro.id ?
      this.livroProvider.EditarLivro(livroRequest)
      :
      this.livroProvider.CadastrarLivro(livroRequest))
      .subscribe(
        {
          next: (v) => {
            this.notification.success(v.message);
          },
          error: (e) => {
            this.spinner.hide();
            this.notification.error(e.message ?? 'Ocorreu algum erro')
          },
          complete: () => {
            this.spinner.hide();
          }
        }
      );


  }
  
  onReset(): void {
    this.livroForm.reset();
  }

  getFormaCompraName(idFormaCompra : number){
    return this.listaValores.find(forma => forma.id == idFormaCompra)?.denominacao!;
  }

  onSelectionFormaCompraChange($event: MatSelectChange) {
    this.preencherListaValoresSelecionados();
  }
  
  private preencherListaValoresSelecionados(): void{
    this.listaValoresSelecionados = this.livroForm.get(this.FORM_FORMASCOMPRA)?.value as Array<number>
    var formValores = this.livroForm.get(this.FORM_VALORES) as FormGroup;

    this.listaValoresSelecionados.forEach(valor => {

      var nomeFormaCompra = this.listaValores.find(a => a.id == valor)?.denominacao!;
      var valorFormaCompra = this.livro?.valores?.find(a => a.idFormaCompra == valor.toString())?.valor;

      var formValoresFormaCompra = formValores.get(nomeFormaCompra);      

      if(!formValoresFormaCompra){
        formValores.addControl(nomeFormaCompra, new FormControl(valorFormaCompra ?? '', Validators.required));       
      }
   
    });

    Object.keys(formValores.controls).forEach(formName => {

      var idFormaCompra = this.listaValores.find(a => a.denominacao == formName)?.id;
      var valorSelecionado = this.listaValoresSelecionados.find(a => a == idFormaCompra);

      if(!valorSelecionado)
        formValores.removeControl(formName);      
    });

  }

  private populateListagens(): void{
    const observables = [
      this.assuntosProvider.GetAssuntos(),
      this.autoresProvider.GetAutores(),
      this.formaCompraProvider.GetFormaCompras()
    ];

    this.spinner.show();

    forkJoin(observables).subscribe({
      next: (results) => {    
        this.spinner.hide();
        this.listaAssuntos = results[0].data as IAssuntoDto[];        
        this.listaAutores = results[1].data as IAutorDto[];
        this.listaValores = results[2].data as IFormaCompraDto[];

        this.preencherListaValoresSelecionados();
      },
      error: (err) => {
        this.spinner.hide();
        this.notification.error('Ocorreu algum erro no carregamento dos dados.')
      },
    });
  }

  private createLivroRequest(): ILivroRequest {

    const livroRequestAssuntos: ILivroRequestLivroAssunto[] = (this.livroForm.get('assuntos')?.value as Array<number>)?.map(id => ({
        idAssunto: id
      })) || [];

      const livroRequestAutores: ILivroRequestLivroAutor[] = (this.livroForm.get('autores')?.value as Array<number>)?.map(id => ({
        idAutor: id
      })) || [];

      const livroRequestValores: ILivroRequestLivroValor[] = (this.livroForm.get(this.FORM_FORMASCOMPRA)?.value as Array<number>)?.map(id => ({
        idFormaCompra: id,
        valor: this.livroForm.get(this.FORM_VALORES)?.get(this.listaValores.find(a => a.id == id)?.denominacao!)?.value
      })) || [];

    return {
      id: this.livro.id,
      titulo: this.livroForm.get('titulo')?.value,
      editora: this.livroForm.get('editora')?.value,
      edicao: this.livroForm.get('edicao')?.value,
      anoPublicacao: this.livroForm.get('anoPublicacao')?.value,
      dataCriacao: this.livroForm.get('dataCriacao')?.value,
      livroAssuntos: livroRequestAssuntos,
      livroAutores: livroRequestAutores,
      livroValores: livroRequestValores,
    };
  }
}
