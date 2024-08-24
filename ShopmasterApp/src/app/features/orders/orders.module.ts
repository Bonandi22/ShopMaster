import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './containers/orders/orders.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { OrderItemComponent } from './components/order-item/order-item.component';
import { OrdersRoutingModule } from './orders-routing.module';

@NgModule({
  declarations: [
    OrdersComponent,
    OrderListComponent,
    OrderDetailComponent,
    OrderItemComponent
  ],
  imports: [
    CommonModule,
    OrdersRoutingModule,

  ]
})
export class OrdersModule { }
