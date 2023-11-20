import { Component, Input, OnInit } from '@angular/core';
import { Phone } from 'src/app/models/phones';
import { User } from 'src/app/models/users';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  @Input() user?: User;

  constructor() { }

  ngOnInit(): void {
  }

  updateUser(user:User) {

  }

  deleteUser(user:User) {
    
  }

  createUser(user:User) {
    
  }

  deletePhone(phone:Phone) {

  }

  addPhone(phone:Phone) {
    
  }
}
