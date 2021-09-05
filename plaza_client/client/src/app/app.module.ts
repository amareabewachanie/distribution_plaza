import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NzProgressModule } from 'ng-zorro-antd/progress';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, NzProgressModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
