import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTableRowSelectedComponent } from './test-table-row-selected.component';

describe('TestTableRowSelectedComponent', () => {
  let component: TestTableRowSelectedComponent;
  let fixture: ComponentFixture<TestTableRowSelectedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestTableRowSelectedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TestTableRowSelectedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
