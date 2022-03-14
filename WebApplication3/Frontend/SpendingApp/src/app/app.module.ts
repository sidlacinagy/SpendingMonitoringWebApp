import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { JwtInterceptor } from './jwt.interceptor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { RegistersuccessComponent } from './register/registersuccess/registersuccess.component';
import { RegisterformComponent } from './register/registerform/registerform.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { VerifyComponent } from './verify/verify.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ViewSpendingComponent } from './Spending/view-spending/view-spending.component';
import { DeleteSpendingComponent } from './Spending/delete-spending/delete-spending.component';
import { CreateSpendingComponent } from './Spending/create-spending/create-spending.component';
import { HomeBaseComponent } from './Spending/home-base/home-base.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    ResetpasswordComponent,
    RegistersuccessComponent,
    RegisterformComponent,
    ForgotpasswordComponent,
    VerifyComponent,
    NavbarComponent,
    ViewSpendingComponent,
    DeleteSpendingComponent,
    CreateSpendingComponent,
    HomeBaseComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
  ],
  providers: [ 
    {
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
