import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IAssuntoDto } from 'src/app/models/dtos/assunto.dto.model';
import { AssuntoProvider } from 'src/app/providers/assunto.provider';
import { AuthService } from 'src/app/services/auth.storage.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  title = 'LivrosApp';
  itensMenu = [
    {
      nome: 'Livros',
      url: '',
      icone: 'auto_stories'
    },
    {
      nome: 'Assuntos',
      url: 'assuntos',
      icone: 'topic_outline'
    },
    {
      nome: 'Autores',
      url: 'autores',
      icone: 'person_outline'
    },
    {
      nome: 'Forma Compra',
      url: 'formacompra',
      icone: 'shopping_cart_outline'
    }
  ]

  constructor(
    private readonly assuntosProvider: AssuntoProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private readonly router: Router,
    private readonly authService: AuthService
  ) { }
  
  ngOnInit(): void {

  }

  IrParaPagina(pagina: string) {
    this.router.navigate(['/' + pagina]);
  }

  Logout(){
    this.authService.clearTokens();
    this.router.navigate(['/login']);
  }
}
