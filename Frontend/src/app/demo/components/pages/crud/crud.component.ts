import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { Customer, Representative } from 'src/app/demo/api/customer';
import { CustomerService } from 'src/app/demo/service/customer.service';
import { OpportunitiesService } from 'src/app/demo/service/opportunities.service';

import { ActivatedRoute, Router } from '@angular/router';
import * as FileSaver from 'file-saver';

interface expandedRows {
    [key: string]: boolean;
}

@Component({
    templateUrl: './crud.component.html',
    providers: [MessageService]
})
export class CrudComponent implements OnInit {

    opportunities: any[] = [];
    isLoading: boolean = true;

    customers1: Customer[] = [];
    customers2: Customer[] = [];
    customers3: Customer[] = [];
    selectedCustomers1: Customer[] = [];
    selectedCustomer: Customer = {};
    representatives: Representative[] = [];
    statuses: any[] = [];
    rowGroupMetadata: any;
    expandedRows: expandedRows = {};
    activityValues: number[] = [0, 100];
    isExpanded: boolean = false;
    idFrozen: boolean = false;
    

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private customerService: CustomerService,
        private productService: ProductService,
        public opportunitiesService: OpportunitiesService,
        private route: ActivatedRoute,
        private router: Router
    ) {}

     ngOnInit() {
         
      this.route.data.subscribe((data: { opportunities: any }) => {
      this.opportunities = data.opportunities;
      this.isLoading = false;
      this.calculateProgressColors();
      
      // Cache the opportunities data after resolving
      this.opportunitiesService.cacheOpportunities(this.opportunities);
    });

       
    }

navigateToOpportunityDetails(opportunityId: string) {
  console.log(opportunityId); 
  this.router.navigate(['pages/crud/opportunity', opportunityId]); 
}


    getStatusBadgeClass(statusCode: number): string {
      switch (statusCode) {
        case 1:
          return 'status-new';
        case 2:
          return 'status-negotiation';
        case 3:
          return 'status-qualified';
        case 4:
          return 'status-renewal';
        case 5:
          return 'status-unqualified';
        default:
          return '';
      }
    }

    getStatusLabel(statusCode: number): string {
        switch (statusCode) {
          case 1:
            return 'In Progress';
          case 2:
            return 'Suspended';
          case 3:
            return 'Concluded';
          case 4:
            return 'Cancelled';
          case 5:
            return 'Out of Stock';
          default:
            return 'Unknown';
        }
      }

    calculateProgressColors() {
    for (const opportunity of this.opportunities) {
        console.log(opportunity.closeprobability);
        opportunity.progressColor = this.getColor(opportunity.closeprobability);
        console.log(opportunity.progressColor);
    }
    }

    getColor(closeprobability: number): string {
        if (closeprobability < 25 ) {
            return '#f97316';
        } else if (closeprobability < 50) {
            return '#06b6d4';
        } else if(closeprobability < 75) {
            return '#22c55e';
        }else{
            return '#06b6d4';
        }
    }

     exportExcel() {
        import('xlsx').then((xlsx) => {
            const worksheet = xlsx.utils.json_to_sheet(this.opportunities);
            const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
            const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
            this.saveAsExcelFile(excelBuffer, 'products');
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
 

    fetchOpportunities() {
        this.opportunitiesService.getOpportunitiesForLoggedInContact().subscribe(
            (opportunities) => {
                this.opportunities = opportunities;
                this.isLoading = false;
                console.log(opportunities);
            },
            (error) => {
                console.error('Error fetching opportunities:', error);
                this.isLoading = false;
            }
        );
      }

    onSort() {
            this.updateRowGroupMetaData();
        }

    updateRowGroupMetaData() {
        this.rowGroupMetadata = {};

        if (this.customers3) {
            for (let i = 0; i < this.customers3.length; i++) {
                const rowData = this.customers3[i];
                const representativeName = rowData?.representative?.name || '';

                if (i === 0) {
                    this.rowGroupMetadata[representativeName] = { index: 0, size: 1 };
                }
                else {
                    const previousRowData = this.customers3[i - 1];
                    const previousRowGroup = previousRowData?.representative?.name;
                    if (representativeName === previousRowGroup) {
                        this.rowGroupMetadata[representativeName].size++;
                    }
                    else {
                        this.rowGroupMetadata[representativeName] = { index: i, size: 1 };
                    }
                }
            }
        }
    }



  /*  expandAll() {
        if (!this.isExpanded) {
            this.products.forEach(product => product && product.name ? this.expandedRows[product.name] = true : '');

        } else {
            this.expandedRows = {};
        }
        this.isExpanded = !this.isExpanded;
    }*/

    formatCurrency(value: number) {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
    
}

