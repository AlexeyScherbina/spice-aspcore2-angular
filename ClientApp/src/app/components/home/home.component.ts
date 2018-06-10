import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public categories: Category[];
  public current = { type: 'none' };
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/Category/getHierarchy').subscribe(result => {
      this.categories = result;
      this.disActivateAll();
      console.log(this.categories);
    }, error => console.error(error));

  }

  public disActivateAll() {
    this.categories.forEach(function (cat) {
      cat.active = false;
      cat.Process.forEach(function (proc) {
        proc.active = false;
      })
    });
  }

  public checkActive(cat: Category) {
    if (cat.active == true) {
      return true;
    }
    for (let i = 0; i < cat.Process.length; i++) {
      if (cat.Process[i].active == true) {
        return true;
      }

    }
    
    return false;
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
