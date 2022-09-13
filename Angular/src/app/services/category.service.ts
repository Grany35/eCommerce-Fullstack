import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    @Inject('apiUrl') private apiUrl:string,
    private httpClient:HttpClient
  ) { }


    addCategory(category:Category){
      let api = this.apiUrl+'categories';
      return this.httpClient.post(api,category);
    }

    getCategories(){
      let api =this.apiUrl+'categories';
      return this.httpClient.get<Category[]>(api);
    }

}
