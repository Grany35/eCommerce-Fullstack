import { Order } from "./order";

export interface OrderDto{
  totalPrice:number,
  count:number,
  id:number,
  orderName:string,
  orders:Order[]
}
