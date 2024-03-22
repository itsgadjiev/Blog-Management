import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { CategoryListModel } from '../models/category-list.model';
import { CategoryUdpateModel } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy {

  id: string | null = null;
  paramsSubsciption?: Subscription;
  editCategorySubsciption?: Subscription;
  category?: CategoryListModel;

  constructor(private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router) {

  }

  ngOnInit(): void {
    this.paramsSubsciption = this.route.paramMap.subscribe({
      next: (params => {
        this.id = params.get('id');

        if (this.id) {
          this.categoryService.getCategoryById(this.id)
            .subscribe({
              next: (res) => {
                this.category = res
              }
            })
        }
      })
    })
  }



  onFormSubmit(): void {
    const updateCategoryRequest: CategoryUdpateModel = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    }

    if (this.id) {
      this.editCategorySubsciption = this.categoryService.updateCategory(this.id, updateCategoryRequest)
        .subscribe({
          next: (res) => {
            this.router.navigateByUrl('/admin/categories')
          }
        })
    }
  }

  ngOnDestroy(): void {
    this.paramsSubsciption?.unsubscribe();
    this.editCategorySubsciption?.unsubscribe();
  }
}
