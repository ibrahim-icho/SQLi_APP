import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Opportunity } from 'src/app/demo/models/Opportunity';


@Component({
  selector: 'app-opportunity-details',
  templateUrl: './opportunities-details.component.html'
})
export class OpportunitiesDetailsComponent implements OnInit {
  opportunityDetails: Opportunity;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.opportunityDetails = this.route.snapshot.data['opportunityDetails'];
  }
}
