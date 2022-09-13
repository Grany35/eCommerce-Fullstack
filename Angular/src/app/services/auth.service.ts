import { Injectable,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Token } from '../models/token';
import { Observable } from 'rxjs';
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(
    @Inject('apiUrl') private apiUrl:string,
    private httpClient:HttpClient
  ) { }


  login(login:Login){
    let api = this.apiUrl+'Auth/login';
    return this.httpClient.post<Token>(api,login);
  }
}
