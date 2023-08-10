import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IncidentComponent } from './incident.component';
import { IncidentsResolverServiceService } from 'src/app/demo/service/incidents-resolver-service.service';




@NgModule({
    imports: [RouterModule.forChild([
        { path: '', component: IncidentComponent ,resolve:{
            incidents: IncidentsResolverServiceService,
        }}
    ])],
    exports: [RouterModule]
})
export class IncidentRoutingModule { }
