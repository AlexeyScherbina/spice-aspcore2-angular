import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent{

  public categories: Category[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      console.log(this.categories);
    }, error => console.error(error));

  }


  public addRow() {
    var cat = <Category>{
      CategoryId: 0,
      Title: '',
      Description: ''
    };
    this.categories.push(cat);
  }

  public addCategory(cat: Category) {
    if (cat.CategoryId == 0) {
      this.http.post(this.baseUrl + 'api/Category/Add', JSON.stringify(cat), {
        headers: { 'Content-Type': 'application/json' }
      }).subscribe((result: string) => {
        alert(result);
        this.http.get<Category[]>(this.baseUrl + 'api/Category/getHierarchy').subscribe(result => {
          this.categories = result;
          console.log(this.categories);
        }, error => console.error(error));
      }, error => console.error(error));
    }
    else {
      this.http.post(this.baseUrl + 'api/Category/Update', JSON.stringify(cat), {
        headers: { 'Content-Type': 'application/json' }
      }).subscribe((result: string) => {
        alert(result);
        this.http.get<Category[]>(this.baseUrl + 'api/Category/getHierarchy').subscribe(result => {
          this.categories = result;
          console.log(this.categories);
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
