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

  constructor(private route: ActivatedRoute,private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
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



  public addRow() {
    var cat = <Process>{
      ProcessId: 0,
      Title: '',
      Description: ''
    };
    this.category.Process.push(cat);
  }

  public addProcess(cat: Process) {
    if (cat.ProcessId == 0) {
      cat.CategoryId = this.id;

      this.http.post(this.baseUrl + 'api/Process/Add', JSON.stringify(cat), {
        headers: { 'Content-Type': 'application/json' }
      }).subscribe((result: string) => {
        alert(result);
        this.http.get<Category[]>(this.baseUrl + 'api/Category/getHierarchy').subscribe(result => {
          this.categories = result;
          console.log(this.categories);
          for (let i = 0; i < this.categories.length; i++) {
            if (this.categories[i].CategoryId == this.id) {
              this.category = this.categories[i];
            }
          }
        }, error => console.error(error));
      }, error => console.error(error));
    }


    else {
      this.http.post(this.baseUrl + 'api/Process/Update', JSON.stringify(cat), {
        headers: { 'Content-Type': 'application/json' }
      }).subscribe((result: string) => {
        alert(result);
        this.http.get<Category[]>(this.baseUrl + 'api/Category/getHierarchy').subscribe(result => {
          this.categories = result;
          console.log(this.categories);
          for (let i = 0; i < this.categories.length; i++) {
            if (this.categories[i].CategoryId == this.id) {
              this.category = this.categories[i];
            }
          }
        }, error => console.error(error));
      }, error => console.error(error));
    }
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
  CategoryId: number;
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
