import { Component, ElementRef, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { LayoutService } from "./service/app.layout.service";
import { AuthService } from '../demo/services/auth.service';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent {

    items!: MenuItem[];

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;

<<<<<<< HEAD
<<<<<<< HEAD
    constructor(public layoutService: LayoutService,private auth: AuthService) { }

    logout(){
        this.auth.signOut();
      }
=======
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
    isLoading: boolean = false; // Initialize as false

    constructor(public layoutService: LayoutService) { }

    fetchProfileData() {
        this.isLoading = true; // Show loading indicator

        console.log(this.isLoading);
        // Simulate an asynchronous operation
        setTimeout(() => {
            // After the operation is complete
            this.isLoading = false; // Hide loading indicator
        }, 2000); // Simulating a delay of 2 seconds
    }
<<<<<<< HEAD
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
=======
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
}
