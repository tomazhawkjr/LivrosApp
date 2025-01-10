import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IAutorDto } from 'src/app/models/dtos/autor.dto.model';
import { AutorProvider } from 'src/app/providers/autor.provider';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-autores',
  templateUrl: './autores.addedit.component.html',
  styleUrls: ['./autores.addedit.component.scss']
})
export class AutoresAddEditComponent implements OnInit {

  autor: IAutorDto = { nome: '', id: 0 };
  autorForm!: FormGroup;

  constructor(
    private readonly autoresProvider: AutorProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    this.autor = history.state;

    this.autorForm = this.fb.group({
      nome: [this.autor?.nome, [Validators.required, Validators.minLength(5), Validators.maxLength(40)]]
    });
  }

  onSubmit(): void {
    if (!this.autorForm.valid) {
      this.notification.error('Algum dado está incorreto.')
      return;
    }

    this.autor.nome = this.autorForm.get('nome')?.value;

    this.spinner.show();

    (this.autor.id ?
      this.autoresProvider.EditarAutor(this.autor)
      :
      this.autoresProvider.CadastrarAutor(this.autor))
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
    this.autorForm.reset();
  }

}
