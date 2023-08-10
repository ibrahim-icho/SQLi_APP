import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Opportunity } from '../models/Opportunity';


@Injectable({
  providedIn: 'root'
})
export class OpportunitiesDetailsService {
  private opportunityDetailsCache: { [opportunityId: number]: Opportunity } = {};
  apiBaseUrl: string;

  constructor(private http: HttpClient) { 
    this.apiBaseUrl = environment.apiUrl;
  }

  getOpportunityDetails(opportunityId: number): Observable<Opportunity> {
    if (this.opportunityDetailsCache[opportunityId]) {
      return of(this.opportunityDetailsCache[opportunityId]);
    }

    const apiUrl = `${this.apiBaseUrl}Opportunities/GetOpportunityById/${opportunityId}`;
    return this.http.get<Opportunity>(apiUrl);
  }

  cacheOpportunityDetails(opportunityId: number, details: Opportunity): void {
    this.opportunityDetailsCache[opportunityId] = details;
  }
}
