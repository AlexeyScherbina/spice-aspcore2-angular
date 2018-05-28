import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public categories: Category[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      console.log(result);
      console.log(this.categories);
    }, error => console.error(error));
  }
}

interface Category {
  CategoryID: number;
  Title: string;
  Description: string;
  Process: Process[];
}
interface Process {
  ProcessID: number;
  Title: string;
  Description: string;
  Practice: Practice[];
}
interface Practice {
  PracticeID: number;
  Title: string;
  Description: string;
}
