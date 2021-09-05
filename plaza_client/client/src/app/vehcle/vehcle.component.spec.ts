import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehcleComponent } from './vehcle.component';

describe('VehcleComponent', () => {
  let component: VehcleComponent;
  let fixture: ComponentFixture<VehcleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehcleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehcleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
