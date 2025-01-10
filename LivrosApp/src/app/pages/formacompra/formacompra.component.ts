import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IFormaCompraDto } from 'src/app/models/dtos/formacompra.dto.model';
import { FormaCompraProvider } from 'src/app/providers/formacompra.provider';

@Component({
  selector: 'app-formacompra',
  templateUrl: './formacompra.component.html',
  styleUrls: ['./formacompra.component.scss']
})
export class FormaCompraComponent implements OnInit {

   listaFormaCompra: Array<IFormaCompraDto> = [];
   displayedColumns: string[] = ['id', 'denominacao', 'acoes'];
   dataSource: MatTableDataSource<IFormaCompraDto> = new MatTableDataSource();
   @ViewChild(MatSort) sort: MatSort = new MatSort();
 
   constructor(
     private readonly formaCompraProvider: FormaCompraProvider,
     private readonly spinner: NgxSpinnerService,
     private readonly notification: ToastrService,
     private readonly router: Router
   ) { }
 
   ngOnInit(): void {
     this.spinner.show();
     this.formaCompraProvider.GetFormaCompras()
       .subscribe(
         {
           next: (v) => {
             this.listaFormaCompra = v.data!;
             this.dataSource = new MatTableDataSource(this.listaFormaCompra);
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
 
   IrParaAdicionarEditarFormaCompra(id?: number): void {
     var formaCompra = id ? this.listaFormaCompra.find(formaCompra => formaCompra.id == id) : {};
     this.router.navigateByUrl(`/formacompra/${id}`, {state: formaCompra});
   }
 
   ApagarFormaCompra(id: number): void {
 
     var formaCompra = this.listaFormaCompra.find(l => l.id == id);
 
     if (confirm(`Tem certeza que deseja excluir a Forma Compra "${formaCompra?.denominacao}"?`)) {
       this.spinner.show();
       this.formaCompraProvider.ApagarFormaCompra(id)
       .subscribe(     
       {
         next: (v) => {
           this.listaFormaCompra = this.listaFormaCompra.filter(Assunto => Assunto.id !== id);
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
 
   createFilter(): (data: IFormaCompraDto, filter: string) => boolean {
     const filterFunction = (data: IFormaCompraDto, filter: string): boolean => {
 
       const filterLower = filter.toLowerCase();
 
       return data.id.toString().toLowerCase().includes(filterLower) ||
       data.denominacao.toLowerCase().includes(filterLower);
     };
     return filterFunction;
   }
 
   applyFilter(event: EventTarget | null) {
     const filterValue = (event as HTMLInputElement).value;
     this.dataSource.filter = filterValue.trim().toLowerCase();
   }
}
