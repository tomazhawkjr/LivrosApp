import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { HomeComponent } from '../home/home.component';
import { LivrosComponent } from '../livros/livros.component';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ToastrModule } from 'ngx-toastr';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        loadChildren: () => import('../home/home.module').then((m) => m.HomeModule),
      },     
      {
        path: 'livro/:id',
        loadChildren: () => import('../livros/adicionareditar/livros.addedit.module').then((m) => m.LivroAddEditModule),
      },
      {
        path: 'autores',
        loadChildren: () => import('../autores/autores.module').then((m) => m.AutoresModule),
      },
      {
        path: 'autor/:id',
        loadChildren: () => import('../autores/adicionareditar/autores.addedit.module').then((m) => m.AutoresAddEditModule),
      },
      {
        path: 'assuntos',
        loadChildren: () => import('../assuntos/assuntos.module').then((m) => m.AssuntosModule),
      },
      {
        path: 'assunto/:id',
        loadChildren: () => import('../assuntos/adicionareditar/assuntos.addedit.module').then((m) => m.AssuntosAddEditModule),
      },
      {
        path: 'formacompra',
        loadChildren: () => import('../formacompra/formacompra.module').then((m) => m.FormaCompraModule),
      },
      {
        path: 'formacompra/:id',
        loadChildren: () => import('../formacompra/adicionareditar/formacompra.addedit.module').then((m) => m.FormaCompraAddEditModule),
      }
    ]
  }
];

@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
     MatIconModule,
      MatSidenavModule,
      MatListModule,
      NgxSpinnerModule,
      MatTableModule,
      MatSortModule,
      MatFormFieldModule,
      MatInputModule,
      MatButtonModule,
      ToastrModule.forRoot()
  ],
  exports: [RouterModule]
})
export class LayoutModule { }
