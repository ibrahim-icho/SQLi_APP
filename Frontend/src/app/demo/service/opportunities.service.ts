import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Opportunity } from '../models/Opportunity';

@Injectable({
  providedIn: 'root'
})
export class OpportunitiesService {
  private opportunitiesCache: Opportunity[] | null = null;
  apiBaseUrl: string;
  accountId: string;

  constructor(private http: HttpClient) { 
    this.apiBaseUrl = environment.apiUrl;
    this.accountId = environment.accountId;
  }

  getOpportunitiesForLoggedInContact(): Observable<Opportunity[]> {
    if (this.opportunitiesCache) {
      return of(this.opportunitiesCache);
    }

    const apiUrl = `${this.apiBaseUrl}Opportunities/GetOpportunitiesByAccountId/${this.accountId}`;
    return this.http.get<Opportunity[]>(apiUrl);
  }

  cacheOpportunities(opportunities: Opportunity[]): void {
    this.opportunitiesCache = opportunities;
  }

  getChacheOpportunities():Opportunity[] | null{
    return this.opportunitiesCache;
  }
}
