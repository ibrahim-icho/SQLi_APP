import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CrudComponent } from './crud.component';
import { OpportunitiesResolverService } from 'src/app/demo/service/opportunities-resolver.service';
import { OpportunitiesDetailsComponent } from './opportunities-details/opportunities-details.component';
import { OpportunityDetailsResolver } from 'src/app/demo/service/opportunity-details-resolver.service';

@NgModule({
  imports: [RouterModule.forChild([
    { 
      path: 'opportunities',
      component: CrudComponent,
      resolve: { opportunities: OpportunitiesResolverService }
    },
    { 
      path: 'opportunity/:opportunityId', 
      component: OpportunitiesDetailsComponent,
      resolve: { opportunityDetails: OpportunityDetailsResolver }
    }
  ])],
  exports: [RouterModule]
})
export class CrudRoutingModule { }