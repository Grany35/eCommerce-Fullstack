import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category';
import { Pagination } from 'src/app/models/pagination';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import { CategoryService } from 'src/app/services/category.service';
import { BrandService } from 'src/app/services/brand.service';
import { Brand } from 'src/app/models/brand';
import { NavbarComponent } from '../navbar/navbar.component';
import { BasketService } from 'src/app/services/basket.service';
import { BasketModel } from 'src/app/models/basket';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent implements OnInit {

  pageNumber=1;
  pageSize=10;

  pagination:Pagination

  products:Product[]
  categories:Category[]
  brands:Brand[]

  baskets:BasketModel[]



  constructor(
    private productService:ProductService,
    private categoryService:CategoryService,
    private basketService:BasketService,
    private brandService:BrandService,
    public navbar:NavbarComponent
  ) { }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getBrands();
    this.navbar.getBasket();
  }


  getProducts(categoryName:string=null,brandName:string=null){
    this.productService.getProducts(this.pageNumber,this.pageSize,categoryName,brandName).subscribe((res)=>{
    this.products=res.result;
    this.pagination=res.pagination;
    })
  }

  getCategories(){
    this.categoryService.getCategories().subscribe((res)=>{
      this.categories=res;
    })
  }

  getBrands(){
    this.brandService.getBrands().subscribe((res)=>{
      this.brands=res;
    })
  }


  addBasket(productId:number){
    this.basketService.addBasket(productId).subscribe(()=>{
      this.navbar.baskets.push();
      this.navbar.getBasket();
      this.basketService.getBasket();
    })
  }




  resetFilters(){
    this.getProducts();
  }

  pageChanged(event:any){
    this.pageNumber = event.page;
    this.getProducts();
  }
}
