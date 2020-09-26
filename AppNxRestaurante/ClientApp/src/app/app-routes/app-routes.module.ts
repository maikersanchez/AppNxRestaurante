import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RegistroComponent } from '../components/cliente/registro/registro.component';
import { ListaComponent } from '../components/cliente/lista/lista.component';

export const ROUTES: Routes = [
  { path: 'home', component: RegistroComponent },
  { path: 'lista', component: ListaComponent },
  // { path: 'triki', component: TrikiComponent, canActivate: [AuthGuardService] },
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: '**', pathMatch: 'full', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(ROUTES, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutesModule { }
