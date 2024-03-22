import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogpostAddComponent } from './blogpost-add.component';

describe('BlogpostAddComponent', () => {
  let component: BlogpostAddComponent;
  let fixture: ComponentFixture<BlogpostAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogpostAddComponent]
    });
    fixture = TestBed.createComponent(BlogpostAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
