import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-estimation',
  templateUrl: './estimation.component.html',
  styleUrls: ['./estimation.component.css']
})
export class EstimationComponent {

  public categories: Category[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      console.log(this.categories);
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
