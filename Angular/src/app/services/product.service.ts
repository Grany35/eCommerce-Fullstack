import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Product } from '../models/product';
import { map,Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination';
import { ProductPhoto } from '../models/productPhoto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();

  constructor(
    @Inject('apiUrl') private apiUrl: string,
    private httpClient: HttpClient
  ) { }

  getProducts(pageNumber?: number, pageSize?: number,categoryName?:string,brandName?:string) {
    let params = new HttpParams();

    if (pageNumber !== null && pageSize !== null) {
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
    }
    if (categoryName!==null) {
      params=params.append('categoryName',categoryName);

    }

    if (brandName!==null) {
      params=params.append('brandName',brandName);
    }



    return this.httpClient.get<Product[]>(this.apiUrl+'products', { observe: 'response', params }).pipe(
      map(response => {
        this.paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    );
  }

  addProduct(form: FormData) {
    let api = this.apiUrl + 'Products/addproduct';
    return this.httpClient.post(api, form);
  }

  deleteProduct(productId: number) {
    let api = this.apiUrl + 'Products/delete-product?productId=' + productId;
    return this.httpClient.get(api);
  }

  getProductPhotos(productId: number) {
    let api = this.apiUrl + 'Products/' + productId;
    return this.httpClient.get<ProductPhoto[]>(api);
  }

  deleteProductPhoto(photoId: number) {
    let api = this.apiUrl + 'Products/delete-photo?photoId=' + photoId;
    return this.httpClient.get(api);
  }

  setMainPhoto(photoId: number) {
    let api = this.apiUrl + 'Products/set-main-photo?photoId=' + photoId;
    return this.httpClient.get(api);
  }

}
