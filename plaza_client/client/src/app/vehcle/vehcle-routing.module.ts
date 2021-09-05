import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehcleComponent } from './vehcle.component';

const routes: Routes = [{ path: '', component: VehcleComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehcleRoutingModule { }
