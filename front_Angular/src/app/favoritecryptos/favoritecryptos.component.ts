import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DataTablesModule } from 'angular-datatables';
@Component({
  selector: 'app-favoritecryptos',
  templateUrl: './favoritecryptos.component.html',
  styleUrls: ['./favoritecryptos.component.css']
})
export class FavoritecryptosComponent implements OnInit {
  data : any;
  isRefreshSuccess : boolean;
  constructor(private router: Router, private http: HttpClient,private jwtHelper: JwtHelperService) { }
  getDataFromServer(){
    const token: string = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
    this.http.get("http://localhost:5000/api/user/getfavoritecrypto", {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.data = response;
    }, err => {
      console.log(err)
    });
  }
  };
  refreshToken(){
    
    const token = localStorage.getItem("jwt");
    const refreshToken: string = localStorage.getItem("refreshToken");
    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    try {
      const response =  this.http.post("http://localhost:5000/api/token/refresh", credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).subscribe(response => {

        const newToken = (<any>response).accessToken;
        const newRefreshToken = (<any>response).refreshToken;
        localStorage.setItem("jwt", newToken);
        localStorage.setItem("refreshToken", newRefreshToken);
        this.isRefreshSuccess = true;
        if(this.isRefreshSuccess){
          this.getDataFromServer();
        }
      }, err => {
        console.log(err)
      });
    }
    catch (ex) {      
      this.isRefreshSuccess = false;
    }
    return this.isRefreshSuccess;

  }
  ngOnInit() {
    this.getDataFromServer();
  }

}
