import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
<<<<<<< HEAD
<<<<<<< HEAD
import { AuthGuard } from '../../guards/auth.guard';

@NgModule({
    imports: [RouterModule.forChild([
        { path: '', component: DashboardComponent , canActivate:[AuthGuard]}
=======
import { OpportunitiesResolverService } from '../../service/opportunities-resolver.service';
import { IncidentsResolverServiceService } from '../../service/incidents-resolver-service.service';

@NgModule({
    imports: [RouterModule.forChild([
        { path: '', component: DashboardComponent , resolve:{opportunity : OpportunitiesResolverService, incidents :IncidentsResolverServiceService}}
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
import { OpportunitiesResolverService } from '../../service/opportunities-resolver.service';
import { IncidentsResolverServiceService } from '../../service/incidents-resolver-service.service';

@NgModule({
    imports: [RouterModule.forChild([
        { path: '', component: DashboardComponent , resolve:{opportunity : OpportunitiesResolverService, incidents :IncidentsResolverServiceService}}
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
    ])],
    exports: [RouterModule]
})
export class DashboardsRoutingModule { }