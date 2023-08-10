import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { Opportunity } from '../models/Opportunity';
import { OpportunitiesService } from './opportunities.service';

@Injectable({
  providedIn: 'root'
})
export class OpportunitiesResolverService implements Resolve<Opportunity[]> {

  constructor(private opportunitiesService: OpportunitiesService) {}

  resolve(): Observable<Opportunity[]> {
    return this.opportunitiesService.getOpportunitiesForLoggedInContact();
  }
}
