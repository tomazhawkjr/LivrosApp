import { Component, OnInit, ViewChild } from '@angular/core';
import { ILivroDto} from 'src/app/models/dtos/livro.dto.model';
import { LivroProvider } from 'src/app/providers/livro.provider';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  //host: { class: 'router-component' },
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  listaLivros: Array<ILivroDto> = [];
  displayedColumns: string[] = ['id', 'titulo', 'editora', 'edicao', 'anoPublicacao', 'assuntos', 'autores', 'valores', 'acoes'];
  dataSource: MatTableDataSource<ILivroDto> = new MatTableDataSource();
  @ViewChild(MatSort) sort: MatSort = new MatSort();

  constructor(
    private readonly livroProvider: LivroProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.livroProvider.GetLivros()
      .subscribe(
        {
          next: (v) => {
            this.listaLivros = v.data!;
            this.dataSource = new MatTableDataSource(this.listaLivros);
            this.dataSource.sort = this.sort;
            this.dataSource.filterPredicate = this.createFilter();
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

  IrParaAdicionarEditarLivro(id?: number): void {
    var livro = id ? this.listaLivros.find(livro => livro.id == id) : {};
    this.router.navigateByUrl(`/livro/${id}`, {state: livro});
  }

  BaixarRelatorio(): void{

    this.spinner.show();
    this.livroProvider.BaixarRelatorio().subscribe(     
      {
        next: (pdfBlob) => {
          this.spinner.hide();
          const pdfUrl = URL.createObjectURL(pdfBlob);
          window.open(pdfUrl, '_blank');
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

  // Método de remoção do livro
  ApagarLivro(id: number): void {

    var livro = this.listaLivros.find(l => l.id == id);

    if (confirm(`Tem certeza que deseja excluir o livro "${livro?.titulo}"?`)) {
      this.spinner.show();
      this.livroProvider.ApagarLivro(id)
      .subscribe(     
      {
        next: (v) => {
          this.listaLivros = this.listaLivros.filter(livro => livro.id !== id);
          this.dataSource.data = this.dataSource.data.filter(livro => livro.id !== id);
          this.notification.success(v.message);
        },
        error: (e) => {
          this.spinner.hide();
          this.notification.error(e.message ?? 'Ocorreu algum erro')        
        },
        complete: () => {
          this.spinner.hide();
        }
      });
    }
  }

  createFilter(): (data: ILivroDto, filter: string) => boolean {
    const filterFunction = (data: ILivroDto, filter: string): boolean => {

      const filterLower = filter.toLowerCase();

      return data.id.toString().toLowerCase().includes(filterLower) ||
        data.titulo.toLowerCase().includes(filterLower) ||
        data.editora.toLowerCase().includes(filterLower) ||
        data.anoPublicacao.toLowerCase().includes(filterLower) ||
        data.assuntos.some(assunto => assunto?.descricao?.toLowerCase().includes(filterLower)) ||
        data.autores.some(autor => autor?.nome?.toLowerCase().includes(filterLower)) ||
        data.valores.some(valor => valor?.denominacaoFormaCompra?.toLowerCase().includes(filterLower));
    };
    return filterFunction;
  }

  applyFilter(event: EventTarget | null) {
    const filterValue = (event as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
