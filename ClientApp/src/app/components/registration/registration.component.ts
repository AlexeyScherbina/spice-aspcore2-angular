import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  public user: User;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private userService: UserService) { }

  ngOnInit() {
    // initialize model here
    this.user = {
      FirstName: "",
      LastName: "",
      Email: "",
      Password: "",
      confirmPassword: ""
    }
  }
  sendRequest(model: User): void {
    this.http.post(this.baseUrl + 'registration', JSON.stringify(model),{
      headers: { 'Content-Type': 'application/json' }
    }).subscribe((result:Result) => {
      alert(result.access_token);
      this.userService.setToken(result.access_token);
    }, error => console.error(error));
  }

}

interface User {
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
  confirmPassword: string;
}
interface Result {
  access_token: string;
  username: string;
  roles: string[];
}
