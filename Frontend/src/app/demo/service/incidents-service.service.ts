import { Injectable } from '@angular/core';
import { Incident } from '../models/Incident';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class IncidentsServiceService {
  
  private cachedIncidents: Incident[] | null = null;
   apiBaseUrl : string ;
   contactId: string ;


  
  constructor(private http:HttpClient) { 
    this.apiBaseUrl= environment.apiUrl;
    this.contactId= environment.contactId;
  }

   getIncidentsForLogedInContact(): Observable<Incident[]> {
    if (this.cachedIncidents) {
      return of(this.cachedIncidents);
    }

    const apiUrl = `${this.apiBaseUrl}Incidents/GetIncidentesByCustomerId/${this.contactId}`;
    return this.http.get<Incident[]>(apiUrl);
  } 

  cacheIncidents(incidents: Incident[]): void {
    this.cachedIncidents = incidents;
  }


}
