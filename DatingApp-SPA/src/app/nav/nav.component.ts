import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { baseDirectiveCreate } from '../../../node_modules/@angular/core/src/render3/instructions';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('loged succ');
    }, error => {
      console.log('loged error');
    });
  }

  loggedin() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
