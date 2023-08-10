import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentComponent } from './incident.component';
import { IncidentRoutingModule } from './incident-routing.module';

import { FormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ChipsModule } from 'primeng/chips'
import { AccordionModule } from 'primeng/accordion';

@NgModule({
    imports: [
        CommonModule,
        IncidentRoutingModule,
         CommonModule,
        TableModule,
        FileUploadModule,
        FormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        RatingModule,
        InputTextModule,
        InputTextareaModule,
        DropdownModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        HttpClientModule,
        ChipsModule,
        AccordionModule
    ],
    declarations: [IncidentComponent]
})
export class IncidentModule { }
