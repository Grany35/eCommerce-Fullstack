import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './components/admin/admin.component';
import { StoreComponent } from './components/store/store.component';

const routes: Routes = [
  { path: '', component: StoreComponent },
  { path: 'admin', component: AdminComponent },
  {path:'admin/product',component:AdminComponent},
  {path:'admin/productlist',component:AdminComponent},
  {path:'admin/orders',component:AdminComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
