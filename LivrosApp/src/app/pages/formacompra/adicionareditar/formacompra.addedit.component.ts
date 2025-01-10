import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { IFormaCompraDto } from 'src/app/models/dtos/formacompra.dto.model';
import { FormaCompraProvider } from 'src/app/providers/formacompra.provider';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-formacompra-addedit',
  templateUrl: './formacompra.addedit.component.html',
  styleUrls: ['./formacompra.addedit.component.scss']
})
export class FormaCompraAddEditComponent implements OnInit {

  formaCompra: IFormaCompraDto = { denominacao: '', id: 0 };
  formaCompraForm!: FormGroup;

  constructor(
    private readonly formaCompraProvider: FormaCompraProvider,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    this.formaCompra = history.state;

    this.formaCompraForm = this.fb.group({
      denominacao: [this.formaCompra?.denominacao, [Validators.required, Validators.minLength(5), Validators.maxLength(40)]]
    });
  }

  onSubmit(): void {
    if (!this.formaCompraForm.valid) {
      this.notification.error('Algum dado está incorreto.')
      return;
    }

    this.formaCompra.denominacao = this.formaCompraForm.get('denominacao')?.value;
    this.spinner.show();

    (this.formaCompra.id ?
      this.formaCompraProvider.EditarFormaCompra(this.formaCompra)
      :
      this.formaCompraProvider.CadastrarFormaCompra(this.formaCompra))
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
    this.formaCompraForm.reset();
  }

}
