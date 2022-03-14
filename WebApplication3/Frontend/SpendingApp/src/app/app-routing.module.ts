import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { CreateSpendingComponent } from './Spending/create-spending/create-spending.component';
import { DeleteSpendingComponent } from './Spending/delete-spending/delete-spending.component';
import { HomeBaseComponent } from './Spending/home-base/home-base.component';
import { ViewSpendingComponent } from './Spending/view-spending/view-spending.component';
import { VerifyComponent } from './verify/verify.component';

const routes: Routes = [
  { path: '', redirectTo:'home', pathMatch:'full'},
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, children:[
    {
      path: '', pathMatch:'full' ,
      component: HomeBaseComponent, 
    },
    {
      path: 'view', 
      component: ViewSpendingComponent, 
    },
    {
      path: 'add', 
      component: CreateSpendingComponent, 
    },
    {
      path: 'delete', 
      component: DeleteSpendingComponent, 
    },
  ]},
  { path: 'register', component: RegisterComponent},
  { path: 'resetpassword', component: ResetpasswordComponent},
  { path: 'forgotpassword', component: ForgotpasswordComponent},
  { path: 'verify', component: VerifyComponent},
  { path: '**', redirectTo:'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
