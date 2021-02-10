import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../shared/user.model';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-registration-form',
  templateUrl: './user-registration-form.component.html',
  styles: [
  ]
})
export class UserRegistrationFormComponent implements OnInit {

  constructor(public service: UserService,private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new User();
  }
  onSubmit(form: NgForm) {
    this.insertRecord(form);
  }
  
  insertRecord(form: NgForm) {
    this.service.postUser().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Successfully Submitted!', 'User Registration');
      },
      err => { 
        console.log(err); 
        this.toastr.error('Registration failed.','User Registration');
      }
    )
  }

}
