import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegitersComponent } from './components/regiters/regiters.component';


const routes: Routes = [{
   path:'registers' , component: RegitersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
