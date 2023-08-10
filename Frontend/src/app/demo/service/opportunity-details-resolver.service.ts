import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { OpportunitiesDetailsService } from './opportunities-details.service';
import { Opportunity } from '../models/Opportunity';

@Injectable({
  providedIn: 'root'
})
export class OpportunityDetailsResolver implements Resolve<Opportunity> {

  constructor(private opportunitiesDetailsService: OpportunitiesDetailsService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Opportunity> {
    const opportunityId = route.params['opportunityId'];
    return this.opportunitiesDetailsService.getOpportunityDetails(opportunityId);
  }
}
