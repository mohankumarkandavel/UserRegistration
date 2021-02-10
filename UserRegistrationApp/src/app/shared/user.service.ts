import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user.model';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  formData: User= new User();
  readonly baseURL = 'http://localhost:50531/api/users';
  list : User[] | undefined;
  constructor(private http: HttpClient) { }

  postUser() {
    return this.http.post(this.baseURL, this.formData);
  }
  putUser() {
    return this.http.put(`${this.baseURL}/${this.formData.Id}`, this.formData);
  }
  deleteUser(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as User[]);
  }
}
