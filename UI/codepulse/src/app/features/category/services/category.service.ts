import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoryListModel } from '../models/category-list.model';
import { environment } from 'src/environments/environment.development';
import { CategoryUdpateModel } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  addCategory(model: AddCategoryRequest): Observable<void> {

    return this.http.post<void>(`${environment.apiUrl}/api/Categories`, model);

  }

  getCategoryList(): Observable<CategoryListModel[]> {

    return this.http.get<CategoryListModel[]>(`${environment.apiUrl}/api/Categories`)
  }

  getCategoryById(id: string): Observable<CategoryListModel> {
    return this.http.get<CategoryListModel>(`${environment.apiUrl}/api/Categories/${id}`)
  }

  updateCategory(id: string, updateCategoryRequest: CategoryUdpateModel): Observable<number> {
    return this.http.put<number>(`${environment.apiUrl}/api/Categories/${id}`, updateCategoryRequest);
  }
}
