import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { ContactService } from '../demo/service/contact.service';
import { Contact } from '../demo/models/Contact';
import { co } from '@fullcalendar/core/internal-common';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];
    contact:Contact;
    constructor(public layoutService: LayoutService,private contactService :ContactService, private route: ActivatedRoute) { }

    ngOnInit() {

        this.contact = this.route.snapshot.data['contact'];
        this.contactService.cacheContact(this.contact);
        
        this.model = [
            {
                label: 'Home',
                items: [
                    { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }
                ]
            },
      
            {
                label: 'Pages',
                icon: 'pi pi-fw pi-briefcase',
                items: [
                 {
                    label: 'Opportunities',
                    icon: 'opportunity-icon',
                    routerLink: ['/pages/crud/opportunities']
                },
                
                {
                        label: 'Account',
                        icon: 'account-icon',
                        routerLink: ['/pages/empty']
                },
                {
                        label: 'Incidents',
                        icon: 'pi pi-fw pi-globe',
                        routerLink: ['/pages/incident']
                }
                ]
            },
           
        ];
    };
}
