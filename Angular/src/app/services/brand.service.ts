import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(
    @Inject('apiUrl') private apiUrl:string,
    private httpClient:HttpClient
  ) { }


    addBrand(brand:Brand){
      let api = this.apiUrl +'brands';
      return this.httpClient.post(api,brand);
    }

    getBrands(){
      let api = this.apiUrl+'brands';
      return this.httpClient.get<Brand[]>(api);
    }

}
