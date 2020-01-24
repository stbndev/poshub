import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsAddSetComponent } from './products-add-set.component';

describe('ProductsAddSetComponent', () => {
  let component: ProductsAddSetComponent;
  let fixture: ComponentFixture<ProductsAddSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductsAddSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductsAddSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
