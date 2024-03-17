import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegisterEventComponent } from 'src/app/event/component/register-event/register-event.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor( public dialog: MatDialog){}

  operFormRegister(): void {

    this.dialog.open(RegisterEventComponent, { width: '500px',  });

}

}
