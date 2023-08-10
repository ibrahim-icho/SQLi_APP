import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BottomLoadingIndicatorComponent } from './bottom-loading-indicator.component';

describe('BottomLoadingIndicatorComponent', () => {
  let component: BottomLoadingIndicatorComponent;
  let fixture: ComponentFixture<BottomLoadingIndicatorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BottomLoadingIndicatorComponent]
    });
    fixture = TestBed.createComponent(BottomLoadingIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
