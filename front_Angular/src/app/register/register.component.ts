import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent  {
  invalidRegister:boolean;
  constructor(private router: Router, private http: HttpClient) { }
  register(form: NgForm) {
    if(this.ValidateEmail(form.value.email)){
    const credentials = JSON.stringify(form.value);
    this.http.post("https://localhost:5001/api/auth/register", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.invalidRegister = false;
      this.router.navigate(["/"]);
    }, err => {
      this.invalidRegister = true;
    });
  }
}
 ValidateEmail(mail:any) 
{
 if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(mail))
  {
    return (true)
  }
    alert("You have entered an invalid email address!")
    return (false)
}

 

}
