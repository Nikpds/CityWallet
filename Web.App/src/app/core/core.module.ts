import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotifyService} from './notify.service';

@NgModule({
  imports: [
    CommonModule,
  
  ],
  declarations: [

  ],
  providers:[
   
    NotifyService
  ]
})
export class CoreModule { }
