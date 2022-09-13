import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { ToastrService } from 'ngx-toastr';
import { BrandService } from 'src/app/services/brand.service';
import { ProductService } from 'src/app/services/product.service';
import { Brand } from 'src/app/models/brand';
import { FileUploader } from 'ng2-file-upload';
import { ChildActivationStart } from '@angular/router';

@Component({
  selector: 'app-product-admin',
  templateUrl: './product-admin.component.html',
  styleUrls: ['./product-admin.component.css']
})
export class ProductAdminComponent implements OnInit {

  categoryAddForm: FormGroup
  brandAddForm: FormGroup
  productAddForm: FormGroup

  category:Category[]
  brand:Brand[]

  currentFile: any;


  constructor(
    private categoryService: CategoryService,
    private brandService: BrandService,
    private productService:ProductService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,

  ) { }

  ngOnInit(): void {
    this.createProductAddForm();
    this.createCategoryAddForm();
    this.createBrandAddForm();
    this.getBrands();
    this.getCategories();

  }

  addCategory() {
    if (this.categoryAddForm.valid) {
      let category = Object.assign({}, this.categoryAddForm.value);
      this.categoryService.addCategory(category).subscribe((res) => {
        this.createCategoryAddForm();
        this.getCategories();
        this.toastr.success("Kategori başarıyla eklendi");
      })
    }
  }

  addBrand() {
    if (this.brandAddForm.valid) {
      let brand = Object.assign({}, this.brandAddForm.value);
      this.brandService.addBrand(brand).subscribe((res) => {
        this.createBrandAddForm();
        this.getBrands();
        this.toastr.success("Marka başarıyla eklendi");
      })
    }
  }

  addProduct(){
    if (this.productAddForm.valid) {
      let product=Object.assign({},this.productAddForm.value);

      let formdata = new FormData();
      formdata.append("productName", product.productName);
      formdata.append("productStock", product.productStock);
      formdata.append("productPrice", product.productPrice);
      formdata.append("categoryId", product.categoryId);
      formdata.append("brandId", product.brandId);
      formdata.append("photo", this.currentFile, this.currentFile.fileName);

      this.productService.addProduct(formdata).subscribe((res)=>{
        this.createProductAddForm();
        this.toastr.success("Ürün Eklendi");
      })
    }
  }

  setFile(event: any){
    this.currentFile = event.target.files[0]
  }

  createCategoryAddForm() {
    this.categoryAddForm = this.formBuilder.group({
      categoryName: ["", Validators.required]
    })
  }

  createBrandAddForm() {
    this.brandAddForm = this.formBuilder.group({
      brandName: ["", Validators.required]
    })
  }

  createProductAddForm() {
    this.productAddForm = this.formBuilder.group({
      productName: [""],
      productStock: [""],
      productPrice: [""],
      categoryId: [""],
      brandId: [""],
      mainPhoto:[""],
    })
  }




  getCategories(){
    this.categoryService.getCategories().subscribe((res)=>{
      this.category=res;
    })
  }

  getBrands(){
    this.brandService.getBrands().subscribe((res)=>{
      this.brand=res;
    })
  }







}
