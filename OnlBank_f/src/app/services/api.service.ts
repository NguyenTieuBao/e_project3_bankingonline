import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl: string = 'http://localhost:5071/api/';
  constructor(private http: HttpClient, private router:Router) { }

  loginUser(loginObj:  { username: string, password: string })
  {
    return this.http.post<any>(`${this.baseUrl}User/login`, loginObj);
  }

  registerUser(registerDto: RegisterDto): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}User/register`, registerDto);
  }

  signOutUser()
  {
    localStorage.clear();
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

  isLoggedInUser(): boolean
  {
    return !!localStorage.getItem('token');
  }

  storeToken(tokenValue: string)
  {
    localStorage.setItem('token', tokenValue);
  }

  getToken()
  {
    return localStorage.getItem('token');
  }

  getHistoryTrans(data: string): Observable<any>
  {
    return this.http.get<any>(`${this.baseUrl}Transaction/transactionhistory?username=${data}`);
  }
  getHistoryTransfer(accountnumber: string): Observable<any>
  {
    return this.http.get<any>(`${this.baseUrl}Transaction/transferhistory?fromAccountNumber=${accountnumber}`);
  }

  getAccountByUsername(username: string): Observable<any>
  {
    return this.http.get<any>(`${this.baseUrl}Account/GetAccount?username=${username}`);
  }

  getAccountCountByUsername(username: string): Observable<number>
  {
    return this.http.get<number>(`${this.baseUrl}Account/accountcount?username=${username}`);
  }

  getTotalCurrentBalance(username: string): Observable<number>
  {
    return this.http.get<number>(`${this.baseUrl}Account/TotalCurrentBalance?username=${username}`);
  }

  getSearchAccount(accountNumber: string): Observable<any>
  {
    return this.http.get<any>(`${this.baseUrl}Account/SearchAccount?accountNumber=${accountNumber}`);
  }

  transfer(data: any): Observable<any>
  {
    return this.http.post<any>(`${this.baseUrl}Transaction/Transfer`, data);
  }

  getFullNameFromToken(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    
    const decodedToken: any = jwtDecode(token);
    return decodedToken ? decodedToken.name : null;
  }

  getUsernameFromToken(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    const decodedToken: any = jwtDecode(token);
    return decodedToken ? decodedToken.given_name : null;
  }

  createAccount(data: any): Observable<any>
  {
    return this.http.post<any>(`${this.baseUrl}Account/createAccount`, data);
  }

  changedPasswordUser(data: any): Observable<any>
  {
    return this.http.post<any>(`${this.baseUrl}User/ChangedPassword`, data);
  }

  getProfileUser(data: string): Observable<any>
  {
    return this.http.get<any>(`${this.baseUrl}User/GetProfile?username=${data}`);
  }

  getRequests(): Observable<RequestDto[]> {
    return this.http.get<RequestDto[]>(`${this.baseUrl}Request`);
  }

  getRequestsByUsername(username: string): Observable<RequestDto[]> {
    return this.http.get<RequestDto[]>(`${this.baseUrl}Request/${username}`);
  }

  postRequest(request: RequestDto): Observable<RequestDto> {
    return this.http.post<RequestDto>(`${this.baseUrl}Request`, request);
  }
}

export interface RequestDto {
  username: string;
  requestType: string;
  requestDate: Date;
  status: string;
}

export interface RegisterDto {
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  role: string;
}
