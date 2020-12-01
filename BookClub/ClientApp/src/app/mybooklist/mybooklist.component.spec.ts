import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBookListComponent } from './mybooklist.component';

describe('MybooklistComponent', () => {
  let component: MyBookListComponent;
  let fixture: ComponentFixture<MyBookListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyBookListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyBookListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
