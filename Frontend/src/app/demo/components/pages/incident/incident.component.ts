import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ActivatedRoute, Router } from '@angular/router';
import * as FileSaver from 'file-saver';

interface expandedRows {
    [key: string]: boolean;
}

@Component({
    templateUrl: './incident.component.html',
    providers: [MessageService]
})
export class IncidentComponent  implements OnInit {

    incidents: any[] = [];
    isLoading: boolean = true;
    showFullDescription: boolean[] = [];
    product:any;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        public messageService: MessageService,
        private route: ActivatedRoute,
        private router: Router
    ) {
       this.incidents = route.snapshot.data['incidents'];
       this.product = route.snapshot.data['productIncident'];

       console.log(this.product);
    }

    ngOnInit() {
        this.route.data.subscribe((data: { incidents: any }) => {
            this.incidents = data.incidents;
            this.isLoading = false;
        });
    }


     
    toggleDescription(event: Event, index: number) {
        event.preventDefault(); 
        this.showFullDescription[index] = !this.showFullDescription[index];
    }

    exportExcel() {
        import('xlsx').then((xlsx) => {
            const worksheet = xlsx.utils.json_to_sheet(this.incidents);
            const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
            const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
            this.saveAsExcelFile(excelBuffer, 'incidents');
        });
    }

    saveAsExcelFile(buffer: any, fileName: string): void {
        let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
        let EXCEL_EXTENSION = '.xlsx';
        const data: Blob = new Blob([buffer], {
            type: EXCEL_TYPE
        });
        FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
}
