import { Component, OnInit,AfterContentChecked } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable,map } from 'rxjs';
import { BasketModel } from 'src/app/models/basket';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/services/auth.service';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public totalItem:number=0;

  isAuthenticated:boolean;

  loginForm:FormGroup;

  totalAmount:number=0;

  baskets:BasketModel[];

  constructor(
    private authService:AuthService,
    private basketService:BasketService,
    private formBuilder:FormBuilder,
    private toastr:ToastrService,

  ) { }

  ngOnInit(): void {

    this.check();

    this.createLoginForm();
    this.getBasket();

  }




  login(){
    if (this.loginForm.valid) {
      let login=Object.assign({},this.loginForm.value);

      this.authService.login(login).subscribe((res)=>{
        localStorage.setItem("token",res.token);
        this.isAuthenticated=true;
        this.toastr.success("Giriş Yapıldı");
      })
    }
  }

  createLoginForm(){
    this.loginForm=this.formBuilder.group({
      mail:["",Validators.required],
      password:["",Validators.required]
    })
  }


  getBasket(){
    this.basketService.getBasket().subscribe((res)=>{
      this.baskets=res;
      this.totalItem=res.length;
    })
  }

   getTotal(){
     let total = 0;
    for (var i = 0; i < this.baskets?.length; i++) {
        if (this.baskets[i].productPrice) {
            total += this.baskets[i].productPrice
           this.totalAmount = total;
         }
    }

    return total;
 }




  logout(){
    localStorage.removeItem("token");
    this.isAuthenticated=false;
  }

  check(){
   let token= localStorage.getItem("token");
    if (token) {
      this.isAuthenticated=true;
    }else{
      this.isAuthenticated=false;
    }
  }

  deleteBasket(basketId:number){
    this.basketService.deleteBasket(basketId).subscribe((res)=>{
      this.getBasket();
    })
  }



}
