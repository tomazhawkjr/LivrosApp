import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IAssuntoDto } from 'src/app/models/dtos/assunto.dto.model';
import { AssuntoProvider } from 'src/app/providers/assunto.provider';

@Component({
  selector: 'app-assuntos',
  templateUrl: './assuntos.component.html',
  styleUrls: ['./assuntos.component.scss']
})
export class AssuntosComponent implements OnInit {

   listaAssuntos: Array<IAssuntoDto> = [];
   displayedColumns: string[] = ['id', 'descricao', 'acoes'];
   dataSource: MatTableDataSource<IAssuntoDto> = new MatTableDataSource();
   @ViewChild(MatSort) sort: MatSort = new MatSort();
 
   constructor(
     private readonly assuntosProvider: AssuntoProvider,
     private readonly spinner: NgxSpinnerService,
     private readonly notification: ToastrService,
     private readonly router: Router
   ) { }
 
   ngOnInit(): void {
     this.spinner.show();
     this.assuntosProvider.GetAssuntos()
       .subscribe(
         {
           next: (v) => {
             this.listaAssuntos = v.data!;
             this.dataSource = new MatTableDataSource(this.listaAssuntos);
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
 
   IrParaAdicionarEditarAssunto(id?: number): void {
     var assunto = id ? this.listaAssuntos.find(assunto => assunto.id == id) : {};
     this.router.navigateByUrl(`/assunto/${id}`, {state: assunto});
   }
 
   ApagarAssunto(id: number): void {
 
     var assunto = this.listaAssuntos.find(l => l.id == id);
 
     if (confirm(`Tem certeza que deseja excluir o Assunto "${assunto?.descricao}"?`)) {
       this.spinner.show();
       this.assuntosProvider.ApagarAssunto(id)
       .subscribe(     
       {
         next: (v) => {
           this.listaAssuntos = this.listaAssuntos.filter(Assunto => Assunto.id !== id);
           this.dataSource.data = this.dataSource.data.filter(Assunto => Assunto.id !== id);
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
 
   createFilter(): (data: IAssuntoDto, filter: string) => boolean {
     const filterFunction = (data: IAssuntoDto, filter: string): boolean => {
 
       const filterLower = filter.toLowerCase();
 
       return data.id.toString().toLowerCase().includes(filterLower) ||
       data.descricao.toLowerCase().includes(filterLower);
     };
     return filterFunction;
   }
 
   applyFilter(event: EventTarget | null) {
     const filterValue = (event as HTMLInputElement).value;
     this.dataSource.filter = filterValue.trim().toLowerCase();
   }
}
