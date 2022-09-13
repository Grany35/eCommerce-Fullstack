import { ProductPhoto } from "./productPhoto";

export interface Product{
  id:number;
  productName:string;
  mainPhotoUrl:string;
  productStock:number;
  productPrice:number;
  isActive:boolean;
  categoryName:string;
  brandName:string;
  productPhotos:ProductPhoto[];
}
