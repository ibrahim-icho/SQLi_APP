import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-bottom-loading-indicator',
  templateUrl: './bottom-loading-indicator.component.html',
  styleUrls: ['./bottom-loading-indicator.component.scss']
})
export class BottomLoadingIndicatorComponent {
  @Input() isLoading: boolean = true;
}
