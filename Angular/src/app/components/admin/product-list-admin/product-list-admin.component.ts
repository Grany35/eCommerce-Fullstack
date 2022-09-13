import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/models/pagination';
import { Product } from 'src/app/models/product';
import { ProductPhoto } from 'src/app/models/productPhoto';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list-admin',
  templateUrl: './product-list-admin.component.html',
  styleUrls: ['./product-list-admin.component.css']
})
export class ProductListAdminComponent implements OnInit {

  @ViewChild('photoListDialog', { static: true }) photoListDialog: TemplateRef<any>;
  @ViewChild('photoAddDialog', { static: true }) photoAddDialog: TemplateRef<any>;

  pageNumber = 1;
  pageSize = 7;
  pagination: Pagination
  categoryName:string=null
  brandName:string=null

  products: Product[]
  productPhotos: ProductPhoto[];

  uploader: FileUploader;
  hasBaseDropZoneOver = false;

  constructor(
    @Inject('apiUrl') private apiUrl: string,
    private productService: ProductService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.listProducts();
  }

  listProducts() {
    this.productService.getProducts(this.pageNumber, this.pageSize,this.categoryName,this.brandName).subscribe((res) => {
      console.log(res);
      this.products = res.result;
      this.pagination = res.pagination;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.listProducts();
  }

  deleteProduct(productId: number) {
    this.productService.deleteProduct(productId).subscribe((res) => {
      this.toastr.success("Ürün silindi");
      this.listProducts();
    })
  }

  deleteProductPhoto(photoId: number) {
    this.productService.deleteProductPhoto(photoId).subscribe((res) => {
      this.toastr.success("Fotoğraf silindi");
      this.dialog.closeAll();
    })
  }

  openPhotoListDialog() {
    this.dialog.open(this.photoListDialog, {
      width: '70%'

    });
  }

  openPhotoAddDialog() {
    this.dialog.open(this.photoAddDialog, {
      width: '70%'

    });
  }

  getProductPhotos(productId: number) {
    this.productService.getProductPhotos(productId).subscribe((res) => {
      this.productPhotos = res;
    })
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  imageUploader(productId: number) {
    this.uploader = new FileUploader({
      url: this.apiUrl + 'Products/add-photo?productId=' + productId,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile=(file)=>{
      file.withCredentials=false;
    }
    this.uploader.onCompleteAll=()=>{
      this.dialog.ngOnDestroy();}
  }

  setMainPhoto(photoId:number){
    this.productService.setMainPhoto(photoId).subscribe((res)=>{
      this.toastr.success("Birincil fotoğraf olarak ayarlandı");
      this.dialog.closeAll();

    })
  }

}
