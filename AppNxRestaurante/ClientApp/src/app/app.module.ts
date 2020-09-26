import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegistroComponent } from './components/cliente/registro/registro.component';
import { ListaComponent } from './components/cliente/lista/lista.component';
import { AppRoutesModule } from './app-routes/app-routes.module';
import { WebApiService } from './services/web-api.service';
import { LoadingComponent } from './components/shared/loading/loading.component';
import { JumbotronComponent } from './components/shared/jumbotron/jumbotron.component';
import { ClienteWsService } from './services/cliente-ws.service';
import { RegistrarMesaComponent } from './components/mesa/registrar-mesa/registrar-mesa.component';
import { ListaMesaComponent } from './components/mesa/lista-mesa/lista-mesa.component';
import { RegistrarFacturaComponent } from './components/factura/registrar-factura/registrar-factura.component';
import { VerFacturaComponent } from './components/factura/ver-factura/ver-factura.component';
import { ListaFacturasComponent } from './components/factura/lista-facturas/lista-facturas.component';
//import { WikipediaService } from './services/wikipedia.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegistroComponent,
    ListaComponent,
    LoadingComponent,
    JumbotronComponent,
    RegistrarMesaComponent,
    ListaMesaComponent,
    RegistrarFacturaComponent,
    VerFacturaComponent,
    ListaFacturasComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutesModule,
    // NgbPaginationModule,
    // NgbAlertModule,
    // RouterModule.forRoot([
    //   { path: '', component: HomeComponent, pathMatch: 'full' },
    //   { path: 'counter', componen|t: CounterComponent },
    //   { path: 'fetch-data', component: FetchDataComponent },
    // ])
  ],
  providers: [WebApiService, ClienteWsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
