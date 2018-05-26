import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

import { EqualValidator } from './directives/equal-validator.directive';

import { UserService } from './services/user.service';
import { LoginGuardGuard as LoginGuard } from './services/login-guard.guard';
import { RoleGuardGuard as RoleGuard } from './services/role-guard.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistrationComponent,
    EqualValidator
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [LoginGuard] },
      {path: 'counter', component: CounterComponent,
        canActivate: [LoginGuard, RoleGuard],
        data: {
          expectedRole: 'admin'
        } 
      },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [LoginGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ])
  ],
  providers: [UserService, LoginGuard, RoleGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
