import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({ providedIn: 'root' })
export class HttpHandlerService {
  constructor(private http: HttpClient) {}

  asFormData(data:any){
    var form_data = new FormData();
    for (var key in data ) {
      form_data.append(key, data[key]);
    }
    return form_data;
  }

  testAuth() {
    return this.http
      .get<String[]>(
        'https://localhost:7098/api/user/getToken',
        {withCredentials:true}
      );
  }

  getSubUsers(){
    return this.http
    .get<Array<any>>(
      'https://localhost:7098/api/subuser/get-all',
      {withCredentials:true}
    );
  }

  login(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/user/login',
      this.asFormData(data),
      {withCredentials:true}
    );
  }
  logout(){
    return this.http
    .get<String>(
      'https://localhost:7098/api/user/logout',
      {withCredentials:true}
    );
  }

  renameSubUser(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/subuser/rename',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  deleteSubUser(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/subuser/delete',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  removeSubUser(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/subuser/remove',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  addSubUser(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/subuser/add',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  register(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/user/register',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  getResetToken(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/user/getResetToken',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  resetpassword(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/user/resetPassword',
      this.asFormData(data),
      {withCredentials:true}
    );
  }

  resetToken(){
    return this.http
    .get<any>(
      'https://localhost:7098/api/user/refreshtoken',
      {withCredentials:true}
    );
  }

  verify(data:any){
    return this.http
    .post<String>(
      'https://localhost:7098/api/user/verify',
      this.asFormData(data),
      {withCredentials:true}
    );
  }
}
