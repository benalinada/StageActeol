import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthGuard } from './guards/oauth2.guard';


const routes: Routes =[

 
  {
    //path: 'dashboard',
    path: 'dashboard',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
    //canActivate: [AuthGuard]
  }, {
    path: '',
    component: AdminLayoutComponent,
    //canActivate: [AuthGuard],
    children: [{
      path: '',
     // canActivate: [AuthGuard],
      loadChildren: () => import('./layouts/admin-layout/admin-layout.module').then(m => m.AdminLayoutModule)
    }]
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
       //useHash: true
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
