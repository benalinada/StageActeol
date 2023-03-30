import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule,  NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { OAuthModule, OAuthService, provideOAuthClient } from 'angular-oauth2-oidc';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ServerService } from './services/server.service';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';

import { CommonModule } from '@angular/common';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';



@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,  
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule,
    NgMultiSelectDropDownModule.forRoot(),
    MatProgressSpinnerModule,
    AuthModule.forRoot({
      config: {
        authority: 'https://localhost:5001',
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: 'acteol',
        scope: 'openid profile',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
      },
    }),
    
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent, 
  

  ],

  providers: [
    OAuthService,
    ServerService,
    provideOAuthClient()
  ], 
  schemas: [
    NO_ERRORS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }




