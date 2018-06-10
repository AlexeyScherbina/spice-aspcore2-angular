import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AdminComponent } from './components/admin/admin.component';
import { ErrorComponent } from './components/error/error.component';
import { CategoryComponent } from './components/category/category.component';
import { ProcessComponent } from './components/process/process.component';
import { PracticeComponent } from './components/practice/practice.component';
import { EstimationComponent } from './components/estimation/estimation.component';

import { EqualValidator } from './directives/equal-validator.directive';
 
import { UserService } from './services/user.service';
import { LoginGuard } from './services/login.guard';
import { RoleGuard } from './services/role.guard';
import { TokenInterceptor } from './services/token.interceptor';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistrationComponent,
    EqualValidator,
    AdminComponent,
    ErrorComponent,
    CategoryComponent,
    ProcessComponent,
    PracticeComponent,
    EstimationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'counter', component: CounterComponent,
        canActivate: [LoginGuard, RoleGuard],
        data: {
          expectedRole: 'admin'
        } 
      },
      { path: 'admin', component: AdminComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'category/:cid', component: CategoryComponent },
      { path: 'process/:pid', component: ProcessComponent },
      { path: 'estmation', component: EstimationComponent },
      { path: '**', component: ErrorComponent }
    ])
  ],
  providers: [UserService, LoginGuard, RoleGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
