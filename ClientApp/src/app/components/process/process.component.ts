import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['./process.component.css']
})
export class ProcessComponent{
  public categories: Category[];
  private id: number;
  public process: Process;

  constructor(private route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.route.params.subscribe(params => { console.log(params); this.id = params.pid });
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      console.log(this.categories);
      for (let i = 0; i < this.categories.length; i++) {
        for (let j = 0; j < this.categories[i].Process.length; j++) {
          if (this.categories[i].Process[j].ProcessId == this.id) {
            this.process = this.categories[i].Process[j];
          }
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
