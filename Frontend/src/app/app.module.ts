import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { ProductService } from './demo/service/product.service';
import { CountryService } from './demo/service/country.service';
import { CustomerService } from './demo/service/customer.service';
import { EventService } from './demo/service/event.service';
import { IconService } from './demo/service/icon.service';
import { NodeService } from './demo/service/node.service';
import { PhotoService } from './demo/service/photo.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppLayoutModule } from './layout/app.layout.module';
<<<<<<< HEAD
<<<<<<< HEAD
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './demo/interceptors/token.interceptor';
=======
import { ProgressSpinnerModule } from 'primeng/progressspinner';

>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
import { ProgressSpinnerModule } from 'primeng/progressspinner';

>>>>>>> 19842453ff4d060109eca9a963038f557ac61371

@NgModule({
    declarations: [
        AppComponent, NotfoundComponent 
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        HttpClientModule,
<<<<<<< HEAD
<<<<<<< HEAD
        HttpClientModule,
        NgToastModule
=======
        ProgressSpinnerModule,
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
        ProgressSpinnerModule,
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
    ],
    providers: [
        {  provide:HTTP_INTERCEPTORS, useClass:TokenInterceptor, multi:true },/*provide: LocationStrategy, useClass: HashLocationStrategy */
        CountryService, CustomerService, EventService, IconService, NodeService,
        PhotoService, ProductService,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
