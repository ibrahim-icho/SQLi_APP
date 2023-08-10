import { Injectable } from '@angular/core';
import { product } from '../models/Product';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductesService {

   private cashedOP: product[] | null = null;
   private cashProduct : product | null =null;
   apiBaseUrl: string;
  //contactId: string;


  constructor(private http: HttpClient) { 
    this.apiBaseUrl = environment.apiUrl;

  }

  getAllProducts(): Observable<product[]> {
    if (this.cashedOP) {
      return of(this.cashedOP);
    }

    const apiUrl = `${this.apiBaseUrl}OpportunityProducts`;
    return this.http.get<product[]>(apiUrl);
  }


  getProductByProductId(productId:any) :Observable<product>{
    if (this.cashedOP) {
      return of(this.cashProduct);
    }

  const apiUrl = `${this.apiBaseUrl}Products/${productId}`;
    return this.http.get<product>(apiUrl);
  }
  
  cacheProducts(products: product[]): void {
    this.cashedOP = products;
  }

  getcashedOP(): product[] | null {
    return this.cashedOP;
  }
}
