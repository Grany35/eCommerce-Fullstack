import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { OrderDto } from '../models/orderDto';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(
    @Inject('apiUrl') private apiUrl:string,
    private httpClient:HttpClient
  ) { }


getOrders(){
  let api = this.apiUrl+'Orders/getorders';
  return this.httpClient.get<OrderDto[]>(api);
}

}
