import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of} from 'rxjs';
import { environment } from 'src/environments/environment';
import { OpportunityProduct } from '../models/OpportunityProduct';

@Injectable({
  providedIn: 'root'
})
export class OpportunityProductService {

  private cashedOP: OpportunityProduct[] | null = null;
  apiBaseUrl: string;
  //contactId: string;
  accountId:any;
  cachOpportunityProduct : OpportunityProduct[] | null = null;

  constructor(private http: HttpClient) { 
    this.apiBaseUrl = environment.apiUrl;
    this.accountId = environment.accountId;

  }

  getAllOpportunityProducts(): Observable<OpportunityProduct[]> {
    if (this.cashedOP) {
      return of(this.cashedOP);
    }

    const apiUrl = `${this.apiBaseUrl}OpportunityProducts`;
    return this.http.get<OpportunityProduct[]>(apiUrl);
  }


  getOpportunityProductByProductId(productId:any){
    if (this.cashedOP) {
      return of(this.cashedOP);
    }

    const apiUrl = `${this.apiBaseUrl}OpportunityProducts`;
    return this.http.get<OpportunityProduct[]>(apiUrl);
  }


  getOpportunityProductByAccountId(){
     if (this.cachOpportunityProduct) {
      return of(this.cachOpportunityProduct);
    }

    const apiUrl = `${this.apiBaseUrl}OpportunityProducts/GetOpportunityProductByAccountId/${this.accountId}`;
    return this.http.get<OpportunityProduct[]>(apiUrl);
  }
  
  cacheOpportunityProducts(products: OpportunityProduct[]): void {
    this.cashedOP = products;
  }

  cacheOpportunityProductsByAccountId(products: OpportunityProduct[]): void {
    this.cachOpportunityProduct = products;
  }
  getcashedOP(): OpportunityProduct[] | null {
    return this.cashedOP;
  }
}
