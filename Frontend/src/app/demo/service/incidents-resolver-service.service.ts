import { Injectable } from '@angular/core';
import { IncidentsServiceService } from './incidents-service.service';
import { Observable } from 'rxjs';
import { Incident } from '../models/Incident';

@Injectable({
  providedIn: 'root'
})
export class IncidentsResolverServiceService {
 constructor(private incidetsService: IncidentsServiceService) {}

  resolve(): Observable<Incident[]> {
    return this.incidetsService.getIncidentsForLogedInContact();
  }
}