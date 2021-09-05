import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehcleRoutingModule } from './vehcle-routing.module';
import { VehcleComponent } from './vehcle.component';


@NgModule({
  declarations: [
    VehcleComponent
  ],
  imports: [
    CommonModule,
    VehcleRoutingModule
  ]
})
export class VehcleModule { }
