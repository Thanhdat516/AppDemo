import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatecompanyComponent } from './updatecompany.component';

describe('UpdatecompanyComponent', () => {
  let component: UpdatecompanyComponent;
  let fixture: ComponentFixture<UpdatecompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdatecompanyComponent]
    });
    fixture = TestBed.createComponent(UpdatecompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
