import { Component, OnInit, OnDestroy } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Product } from '../../api/product';
import { ProductService } from '../../service/product.service';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ContactService } from '../../service/contact.service';
import { Contact } from '../../models/Contact';
<<<<<<< HEAD
<<<<<<< HEAD
import { AuthService } from '../../services/auth.service';
=======
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
import { OpportunityProductService } from '../../service/opportunity-product.service';
import { OpportunityProduct } from '../../models/OpportunityProduct';
import { ActivatedRoute, Router } from '@angular/router';
import { Opportunity } from '../../models/Opportunity';
import { Incident } from '../../models/Incident';
<<<<<<< HEAD
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371

@Component({
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit, OnDestroy {

    items!: MenuItem[];

    products!: Product[];

    pieData: any;

    pieOptions: any;

    chartData: any;

    chartOptions: any;

    subscription!: Subscription;

    contact : Contact;
    
    opportunities:Opportunity[];

<<<<<<< HEAD
<<<<<<< HEAD
    constructor(private productService: ProductService, public layoutService: LayoutService ,private auth: AuthService) {
=======
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
    incidents:Incident[];

    incidentsNumber :any = 0;

    opportunitiesNumber:any = 0;

    Total:any = 0;

    recentOpportunities: Opportunity[] = [];

    constructor(
        private productService: ProductService, 
        public layoutService: LayoutService,
        private opportunityProductService: OpportunityProductService,
        private route: ActivatedRoute,
        private router: Router) {
<<<<<<< HEAD
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
        this.subscription = this.layoutService.configUpdate$.subscribe(() => {
            this.initChart();
        });
        
        
        this.contact =  route.snapshot.data['contact'];
        this.opportunities = route.snapshot.data['opportunity'];
        this.incidents = route.snapshot.data['incidents'];

        this.countOpportunitiesNumber();
        this.countEstimatedValue();
        this.countIncidentsNumber();
    }
  

    ngOnInit() {
       
        this.initChart();
        this.productService.getProductsSmall().then(data => this.products = data);

        this.items = [
            { label: 'Add New', icon: 'pi pi-fw pi-plus' },
            { label: 'Remove', icon: 'pi pi-fw pi-minus' }
        ];

        this.opportunityProductService.getOpportunityProductByAccountId().subscribe(products => {
            const productCounts = this.countProductsByName(products);
            console.log(productCounts);
            this.updatePieChart(productCounts);
            
        });


         this.recentOpportunities = this.getMostRecentOpportunities(this.opportunities, 2);
        
    }



navigateToOpportunityDetails(opportunityId: string) {
  this.router.navigate(['pages/crud/opportunity', opportunityId]); 
}

getMostRecentOpportunities(opportunities: Opportunity[], count: number): Opportunity[] {
    return opportunities.slice(0, count);
}


   countOpportunitiesNumber(){
        this.opportunitiesNumber = this.opportunities.length;
   }


   countIncidentsNumber(){
         this.incidentsNumber = this.incidents.length;
   }

   
   countEstimatedValue(){
        this.opportunities.forEach(opportunity=>{
            this.Total =this.Total + opportunity.estimatedvalue;
        });
   }







    
    countProductsByName(products: OpportunityProduct[]): Map<string, number> {
        const counts = new Map<string, number>();

        products.forEach(product => {
            if (counts.has(product.opportunityproductname)) {
                counts.set(product.opportunityproductname, counts.get(product.opportunityproductname)! + 1);
            } else {
                counts.set(product.opportunityproductname, 1);
            }
        });
        
        // Convert Map to Array of [key, value] pairs
        const sortedEntries = Array.from(counts.entries()).sort((a, b) => b[1] - a[1]); // Sort by value in descending order

        // Convert Array back to Map
        return new Map(sortedEntries);

        //return counts;
    }


   

    initChart() {
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        this.chartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: false,
                    backgroundColor: documentStyle.getPropertyValue('--bluegray-700'),
                    borderColor: documentStyle.getPropertyValue('--bluegray-700'),
                    tension: .4
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: false,
                    backgroundColor: documentStyle.getPropertyValue('--green-600'),
                    borderColor: documentStyle.getPropertyValue('--green-600'),
                    tension: .4
                }
            ]
        };

        this.chartOptions = {
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                }
            }
        };


    
       



        this.pieOptions = {
            indexAxis: 'y',
            maintainAspectRatio: false,
            aspectRatio: 0.8,
            plugins: false,
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary,
                        font: {
                            weight: 500
                        }
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                }
            }
        };

    }
    
    updatePieChart(productCounts: Map<string, number>) {
        const documentStyle = getComputedStyle(document.documentElement);
        const labels = Array.from(productCounts.keys());
        const data = Array.from(productCounts.values());

        this.pieData = {
            labels: labels,
            datasets: [
                {
                    data: data,
                    backgroundColor: [
                        documentStyle.getPropertyValue('--indigo-500'),
                        documentStyle.getPropertyValue('--purple-500'),
                        documentStyle.getPropertyValue('--teal-500'),
                        documentStyle.getPropertyValue('--red-500'),
                        documentStyle.getPropertyValue('--blue-500'),
                        documentStyle.getPropertyValue('--yellow-500'),
                        documentStyle.getPropertyValue('--green-500'),
                        documentStyle.getPropertyValue('--orange-500'),
                        documentStyle.getPropertyValue('--pink-500')
                    ],
                    hoverBackgroundColor: [
                        documentStyle.getPropertyValue('--indigo-400'),
                        documentStyle.getPropertyValue('--purple-400'),
                        documentStyle.getPropertyValue('--teal-400'),
                        documentStyle.getPropertyValue('--red-400'),
                        documentStyle.getPropertyValue('--blue-400'),
                        documentStyle.getPropertyValue('--yellow-400'),
                        documentStyle.getPropertyValue('--green-400'),
                        documentStyle.getPropertyValue('--orange-400'),
                        documentStyle.getPropertyValue('--pink-400')
                    ]
                }
            ]
        };
    }
    

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
<<<<<<< HEAD
<<<<<<< HEAD
    
}
=======
}
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
}
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
