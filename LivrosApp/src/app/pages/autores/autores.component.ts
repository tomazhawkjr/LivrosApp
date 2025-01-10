import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IAutorDto } from 'src/app/models/dtos/autor.dto.model';
import { AutorProvider } from 'src/app/providers/autor.provider';

@Component({
  selector: 'app-autores',
  templateUrl: './autores.component.html',
  styleUrls: ['./autores.component.scss']
})
export class AutoresComponent implements OnInit {

   listaAutores: Array<IAutorDto> = [];
   displayedColumns: string[] = ['id', 'nome', 'acoes'];
   dataSource: MatTableDataSource<IAutorDto> = new MatTableDataSource();
   @ViewChild(MatSort) sort: MatSort = new MatSort();
 
   constructor(
     private readonly autoresProvider: AutorProvider,
     private readonly spinner: NgxSpinnerService,
     private readonly notification: ToastrService,
     private readonly router: Router
   ) { }
 
   ngOnInit(): void {
     this.spinner.show();
     this.autoresProvider.GetAutores()
       .subscribe(
         {
           next: (v) => {
             this.listaAutores = v.data!;
             this.dataSource = new MatTableDataSource(this.listaAutores);
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
 
   IrParaAdicionarEditarAutor(id?: number): void {
    var autor = id ? this.listaAutores.find(autor => autor.id == id) : {};
    this.router.navigateByUrl(`/autor/${id}`, {state: autor});
  }
 
   ApagarAutor(id: number): void {
 
     var Autor = this.listaAutores.find(l => l.id == id);
 
     if (confirm(`Tem certeza que deseja excluir o Autor "${Autor?.nome}"?`)) {
       this.spinner.show();
       this.autoresProvider.ApagarAutor(id)
       .subscribe(     
       {
         next: (v) => {
           this.listaAutores = this.listaAutores.filter(Autor => Autor.id !== id);
           this.dataSource.data = this.dataSource.data.filter(Autor => Autor.id !== id);
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
   }
 
   createFilter(): (data: IAutorDto, filter: string) => boolean {
     const filterFunction = (data: IAutorDto, filter: string): boolean => {
 
       const filterLower = filter.toLowerCase();
 
       return data.id.toString().toLowerCase().includes(filterLower) ||
       data.nome.toLowerCase().includes(filterLower);
     };
     return filterFunction;
   }
 
   applyFilter(event: EventTarget | null) {
     const filterValue = (event as HTMLInputElement).value;
     this.dataSource.filter = filterValue.trim().toLowerCase();
   }
}
