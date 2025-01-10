import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IAssuntoDto } from 'src/app/models/dtos/assunto.dto.model';
import { AssuntoProvider } from 'src/app/providers/assunto.provider';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-assuntos',
  templateUrl: './assuntos.addedit.component.html',
  styleUrls: ['./assuntos.addedit.component.scss']
})
export class AssuntosAddEditComponent implements OnInit {

  assunto: IAssuntoDto = { descricao: '', id: 0 };
  assuntoForm!: FormGroup;

  constructor(
    private readonly assuntosProvider: AssuntoProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    this.assunto = history.state;

    this.assuntoForm = this.fb.group({
      descricao: [this.assunto?.descricao, [Validators.required, Validators.minLength(5), Validators.maxLength(40)]]
    });
  }

  onSubmit(): void {
    if (!this.assuntoForm.valid) {
      this.notification.error('Algum dado está incorreto.')
      return;
    }

    this.assunto.descricao = this.assuntoForm.get('descricao')?.value;

    this.spinner.show();

    (this.assunto.id ?
      this.assuntosProvider.EditarAssunto(this.assunto)
      :
      this.assuntosProvider.CadastrarAssunto(this.assunto))
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
    this.assuntoForm.reset();
  }

}
