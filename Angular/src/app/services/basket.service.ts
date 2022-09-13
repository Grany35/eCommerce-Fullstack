import { Injectable,Inject } from '@angular/core';
import { BasketModel } from '../models/basket';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(
    @Inject('apiUrl') private apiUrl:string,
    private httpClient:HttpClient
  ) { }


  getBasket(){
    let api = this.apiUrl+'Baskets';
    return this.httpClient.get<BasketModel[]>(api);
  }

  addBasket(productId:number){
    let api=this.apiUrl+'Baskets?productId='+productId;
    return this.httpClient.post(api,productId);
  }

  deleteBasket(basketId:number){
    let api = this.apiUrl+'Baskets/delete-basket?basketId='+basketId;
    return this.httpClient.get(api);
  }
}
