import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreComponent } from './components/store/store.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminComponent } from './components/admin/admin.component';
import { ProductAdminComponent } from './components/admin/product-admin/product-admin.component';
import { ToastrModule } from 'ngx-toastr';
import { config } from 'rxjs';
import { FileUploadModule } from 'ng2-file-upload';
import { Auth0ClientFactory, Auth0ClientService, AuthClientConfig, AuthConfigService } from '@auth0/auth0-angular';
import { ProductListAdminComponent } from './components/admin/product-list-admin/product-list-admin.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {MatDialogModule} from '@angular/material/dialog';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { OrdersAdminComponent } from './components/admin/orders-admin/orders-admin.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import {MatExpansionModule} from '@angular/material/expansion';

@NgModule({
  declarations: [
    AppComponent,
    StoreComponent,
    NavbarComponent,
    AdminComponent,
    ProductAdminComponent,
    ProductListAdminComponent,
    OrdersAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatExpansionModule,
    ReactiveFormsModule,
    MatDialogModule,
    SweetAlert2Module.forRoot(),
    PaginationModule,
    CollapseModule.forRoot(),
    FileUploadModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right',
      timeOut:6000
    })
  ],
  providers: [
    {provide:'apiUrl',useValue:'https://localhost:7216/api/'},
    {provide:HTTP_INTERCEPTORS,useClass:AuthInterceptor,multi:true},
    {provide:AuthConfigService,useValue:config},
    {provide:Auth0ClientService,useFactory:Auth0ClientFactory.createClient,deps:[AuthClientConfig]},
    NavbarComponent,
    StoreComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
