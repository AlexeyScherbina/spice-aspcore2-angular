import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  public categories: Category[];
  private id: number;
  public category: Category;

  constructor(private route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.route.params.subscribe(params => { console.log(params); this.id = params.cid });
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      console.log(this.categories);
      for (let i = 0; i < this.categories.length; i++) {
        if (this.categories[i].CategoryId == this.id) {
          this.category = this.categories[i];
        }
      }
    }, error => console.error(error));
  }



}


interface Category {
  CategoryId: number;
  Title: string;
  Description: string;
  Process: Process[];
  active: boolean;
}
interface Process {
  ProcessId: number;
  Title: string;
  Description: string;
  Practice: Practice[];
  active: boolean;
}
interface Practice {
  PracticeId: number;
  Title: string;
  Description: string;
}
