import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { RegistersService } from './registers.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RegitersComponent } from './components/regiters/regiters.component';


@NgModule({
  declarations: [
    AppComponent,
    RegitersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot()

  ],
  providers: [HttpClientModule, RegistersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
