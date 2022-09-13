import { Component, OnInit } from '@angular/core';
import { OrderDto } from 'src/app/models/orderDto';
import { OrdersService } from 'src/app/services/orders.service';

@Component({
  selector: 'app-orders-admin',
  templateUrl: './orders-admin.component.html',
  styleUrls: ['./orders-admin.component.css']
})
export class OrdersAdminComponent implements OnInit {

  orders:OrderDto[];

  panelOpenState = false;

  constructor(
    private orderService:OrdersService
  ) { }

  ngOnInit(): void {
    this.getOrders();
  }


  getOrders(){
    this.orderService.getOrders().subscribe((res)=>{
      console.log(res);
      this.orders=res;
    })
  }

}
