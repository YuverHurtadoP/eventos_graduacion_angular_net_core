import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { EventService } from '../../service/event.service';
import { ValidationCustomization } from 'src/app/shared/util/validation-customization';
import { EventRequestDto } from '../../dtos/event-request-dto';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-register-event',
  standalone: true,
  imports: [CommonModule,FormsModule, MatIconModule, ReactiveFormsModule, MatDialogModule],
  templateUrl: './register-event.component.html',
  styleUrls: ['./register-event.component.css']
})
export class RegisterEventComponent {
  msg = "El campo es obligatorio"

  private service: EventService;
  formRegister: FormGroup;

  objectRequest:EventRequestDto = new EventRequestDto  ;

  constructor(service: EventService,private fb: FormBuilder,public dialogRef: MatDialogRef<RegisterEventComponent>){
    this.service = service;

    this.formRegister = this.fb.group({
      institution: [null, [ ValidationCustomization.spaceValidator]],
      institutionAddress: ['', [ ValidationCustomization.spaceValidator]],
      numberOfStudents: ['',  [ ValidationCustomization.spaceValidator,Validators.pattern('^[0-9]+$')]],
      servicePrice: ['',  [ ValidationCustomization.spaceValidator,Validators.pattern('^[0-9]+$')]],
      startTime: ['',  [ ValidationCustomization.spaceValidator]],
      serviceEvent: ['',  [ ValidationCustomization.spaceValidator]],
      email: ['',  [ ValidationCustomization.spaceValidator,Validators.email]],

    });
  }
save(){

  this.objectRequest.institutionName = this.formRegister.get("institution")?.value;
  this.objectRequest.institutionAddress =  this.formRegister.get("institutionAddress")?.value;
  this.objectRequest.numberOfStudents =  this.formRegister.get("numberOfStudents")?.value;
  this.objectRequest.servicePrice =  this.formRegister.get("servicePrice")?.value;
  this.objectRequest.email =  this.formRegister.get("email")?.value;
  this.objectRequest.startTime =  this.formRegister.get("startTime")?.value;

  if (this.formRegister.valid){
    this.service.saveEvent(this.objectRequest).subscribe({
      next:()=>{
        this.dialogRef.close();
        Swal.fire(
          '¡Registrado!',
          'El evento se realizó con éxito.',
          'success'
        );
      },
      error:(e)=>{

        Swal.fire({
          title: "Error",
          icon: "error",
          text: e.error.mensaje,
          confirmButtonText: "Aceptar",

          showCloseButton: true,
        });
      }
    })
  }else{
    this.markFieldsAsTouched(this.formRegister);
  }

}

calculateValue(){

  var serviceEvent = this.formRegister.get("serviceEvent")?.value;
  if(
    this.formRegister.get("numberOfStudents")?.valid &&
     this.formRegister.get("serviceEvent")?.valid
  ){
    if(parseInt(this.formRegister.get("serviceEvent")?.value)==1){
      this.formRegister.get("servicePrice")?.setValue((200 +(parseInt( this.formRegister.get("numberOfStudents")?.value)*20)+'') )
    }else{
      this.formRegister.get("servicePrice")?.setValue((200 )+'');
    }

  }

}

private markFieldsAsTouched(formGroup: FormGroup) {
  Object.values(formGroup.controls).forEach(control => {
    if (control instanceof FormGroup) {
      this.markFieldsAsTouched(control);
    } else {
      control.markAsTouched();
    }
  });
}
}
