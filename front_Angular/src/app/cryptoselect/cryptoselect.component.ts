import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DataTablesModule } from 'angular-datatables';

@Component({
  selector: 'app-cryptoselect',
  templateUrl: './cryptoselect.component.html',
  styleUrls: ['./cryptoselect.component.css']
})
export class CryptoselectComponent implements OnInit {

  data : any
  dataChecked:any = []
  dtOptions: DataTables.Settings = {};
  isDataSend : boolean;
  isRefreshSuccess : boolean;
  
  constructor(private router: Router, private http: HttpClient,private jwtHelper: JwtHelperService) { }
  isChecked = false;

  checkByOne(websitecheck){

    if(websitecheck.isChecked == true)
    websitecheck.isChecked = false;
    else
    websitecheck.isChecked = true;
  }
  dataToSend(){
    
    this.data.forEach(element => {
      if(element.isChecked)
      {
        this.dataChecked.push(element)
      }

    });
    this.http.post("http://localhost:5000/api/user/selectedCryptos", this.dataChecked, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {

      this.dataChecked = [];
      this.isDataSend = true;

    }, err => {
      this.isDataSend = false;
    });
  }
  getDataFromServer(){
    const token: string = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
    this.http.get("http://localhost:5000/api/user/getcrypto", {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.data = response;
    }, err => {
      console.log(err)
    });
  }
}
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
  ngOnInit(): void {
  this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      processing: true
    };
    this.getDataFromServer();
  }
}


