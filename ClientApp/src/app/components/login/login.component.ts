import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public user: User;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private userService: UserService) { }

  ngOnInit() {

    this.user = {
      Email: "",
      Password: ""
    }
  }

  sendRequest(model: User){
    this.http.post(this.baseUrl + 'token', JSON.stringify(model), {
      headers: { 'Content-Type': 'application/json' }
    })
      .subscribe((result: Result) => {
        alert(result.access_token);
        this.userService.setToken(result.access_token);
      });
  }

}

interface User {
  Email: string;
  Password: string;
}
interface Result {
  access_token: string;
  username: string;
  roles: string[];
}
