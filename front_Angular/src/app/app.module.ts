import { AuthGuard } from './guards/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {JwtModule} from '@auth0/angular-jwt';
import { DataTablesModule } from 'angular-datatables';


import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { CryptoselectComponent } from './cryptoselect/cryptoselect.component';
import { FavoritecryptosComponent } from './favoritecryptos/favoritecryptos.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    AppComponent,
    RegisterComponent,
    CryptoselectComponent,
    FavoritecryptosComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    DataTablesModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register',component:RegisterComponent},
      { path:'cryptoselect',component:CryptoselectComponent},
      { path:'favoritecryptos',component:FavoritecryptosComponent}
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})

export class AppModule { }
