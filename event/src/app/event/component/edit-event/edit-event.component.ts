import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { EventService } from '../../service/event.service';
import { ValidationCustomization } from 'src/app/shared/util/validation-customization';
import { EventRequestDto } from '../../dtos/event-request-dto';
import Swal from 'sweetalert2';
import { RegisterEventComponent } from '../register-event/register-event.component';
@Component({
  selector: 'app-edit-event',
  standalone: true,
  imports: [CommonModule,FormsModule, MatIconModule, ReactiveFormsModule, MatDialogModule],
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent {msg = "El campo es obligatorio"

private service: EventService;
formRegister: FormGroup;

objectRequest:EventRequestDto = new EventRequestDto  ;
id =  0;
constructor(   @Inject(MAT_DIALOG_DATA) public data: any,

  service: EventService,private fb: FormBuilder,public dialogRef: MatDialogRef<EditEventComponent>){
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
  this.formRegister.setValue({
    institution: this.data.nameInstitution.toString(),
    institutionAddress: this.data.addressInstution.toString(),
    numberOfStudents: this.data.numberOfStudents.toString(),
    servicePrice: this.data.servicePrice.toString(),
    startTime:  this.data.startTime.substring(0, 10),
    email: this.data.email.toString(),
    serviceEvent: ""


  });
  console.log(this.data.startTime)
  this.id = parseInt( this.data.id);
}
save(){

this.objectRequest.institutionName = this.formRegister.get("institution")?.value;
this.objectRequest.institutionAddress =  this.formRegister.get("institutionAddress")?.value;
this.objectRequest.numberOfStudents =  this.formRegister.get("numberOfStudents")?.value;
this.objectRequest.servicePrice =  this.formRegister.get("servicePrice")?.value;
this.objectRequest.email =  this.formRegister.get("email")?.value;
this.objectRequest.startTime =  this.formRegister.get("startTime")?.value;

if (this.formRegister.valid){
  this.service.updateEvent(this.id, this.objectRequest).subscribe({
    next:()=>{
      this.dialogRef.close();
      Swal.fire(
        '¡Registrado!',
        'El evento se actualizó con éxito.',
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
