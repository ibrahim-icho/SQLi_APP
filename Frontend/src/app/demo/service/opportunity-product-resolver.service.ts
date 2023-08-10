import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { OpportunityProduct } from '../models/OpportunityProduct';
import { OpportunityProductService } from './opportunity-product.service';

@Injectable({
  providedIn: 'root'
})
export class OpportunitiesResolverService implements Resolve<OpportunityProduct[]> {

  constructor(private opportunitiesService: OpportunityProductService) {}

  resolve(): Observable<OpportunityProduct[]> {
    return this.opportunitiesService.getOpportunityProductByAccountId();
  }
}
