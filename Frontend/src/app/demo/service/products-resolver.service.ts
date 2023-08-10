import { Injectable } from '@angular/core';
import { ProductesService } from './productes.service';
import { product } from '../models/Product';
import { Observable } from 'rxjs';
import { ActivatedRoute, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductsResolverService {

constructor(private productService: ProductesService, private route: ActivatedRoute) {}

  resolve(): Observable<product> {
    var productId = this.route.params['productId']; 
    return this.productService.getProductByProductId(productId);
  }
}
