import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { EventService } from '../../service/event.service';
import Swal from 'sweetalert2';
import { EventResponseDto } from '../../dtos/event-response-dto';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { EditEventComponent } from '../edit-event/edit-event.component';
import { FormGroup } from '@angular/forms';
import { FormBuilder,FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ValidationCustomization } from 'src/app/shared/util/validation-customization';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule, MatIconModule,FormsModule,ReactiveFormsModule ],
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css'],
  providers:[  DatePipe]
})
export class EventComponent implements OnInit{
  msg = "El campo es obligatorio";
  listEventResponseDto:EventResponseDto[]=[];
  listEventResponseTemp:EventResponseDto[]=[];

  private service: EventService;

  searchForm: FormGroup;

  constructor(service: EventService,   public dialog: MatDialog, private fb: FormBuilder,private datePipe: DatePipe){
    this.service = service;
    this.searchForm = this.fb.group({
      findSearch: [],
      typeFiler: [],

    });
    this.searchForm.setValue({
      typeFiler: 1,
      findSearch: ""



    });

  }
  ngOnInit(): void {
    this.listEvent();
    this.updateList();

  }

  listEvent(){
    this.service.listEvent().subscribe({
      next: (data) => {
        this.listEventResponseDto = data;
        this.listEventResponseTemp =  data;
        console.log(this.listEventResponseDto);
      },
      error: (e) => {
        Swal.fire({
          title: "Error",
          icon: "error",
          text: "Error en obtener el listado de recargas",
          confirmButtonText: "Aceptar",

          showCloseButton: true,
        });
      }
    })
  }


  delete(id: number): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: '¡No podrás revertir esto!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminarlo',
      cancelButtonText:"Cancelar"
    }).then((result) => {
      if (result.isConfirmed) {

        this.service.deleteEvent(id).subscribe(
          response => {

              Swal.fire(
                '¡Eliminado!',
                'El evento ha sido eliminado.',
                'success'
              );


          },

        );
      }
    });
  }

  updateList(){
    this.service.changeSubject.subscribe((estado) => {
      this.listEvent();

    });
  }

  openModalEdit(
    id:any,
    nameInstitution:string,
    addressInstution:string,
    numberOfStudents:number,
    servicePrice:number,
    startTime:Date,
    email:string
    ){
      this.dialog.open(EditEventComponent, { width: '500px',
      data: {id, nameInstitution,addressInstution,numberOfStudents,servicePrice,startTime,email}
    });
    }


    filter(){
      const search = this.searchForm.get("findSearch")?.value.toLowerCase();
      const value = parseInt(this.searchForm.get("typeFiler")?.value);
      if (value == 2){
        this.listEventResponseDto = this.listEventResponseTemp.filter(s => s.institutionName.toLowerCase().includes(search))
        return  this.listEventResponseDto;
      }else if  (value == 3) {
        return     this.listEventResponseDto = this.listEventResponseTemp.filter(s => s.institutionAddress.toLowerCase().includes(search))
      }else if  (value == 4) {
        return     this.listEventResponseDto = this.listEventResponseTemp.filter(s => (s.numberOfStudents).toString().toLowerCase().includes(search))
      }else if  (value == 5) {
        return     this.listEventResponseDto = this.listEventResponseTemp.filter(s => (s.servicePrice).toString().toLowerCase().includes(search))
      }else if  (value == 6) {
        return this.listEventResponseDto = this.listEventResponseTemp.filter(s => {
          if (s.startTime !== null ) {
            const formattedDate = this.datePipe.transform(s.startTime, 'dd/MM/yy');
            return formattedDate !== null && formattedDate.toLowerCase().includes(search);
          }
          return [];
        });
      }else{
        return this.listEventResponseDto;
      }

    }


}
