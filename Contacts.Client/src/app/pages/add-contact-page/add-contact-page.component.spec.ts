import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddContactPageComponent } from './add-contact-page.component';

describe('AddContactPageComponent', () => {
  let component: AddContactPageComponent;
  let fixture: ComponentFixture<AddContactPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddContactPageComponent]
    });
    fixture = TestBed.createComponent(AddContactPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
