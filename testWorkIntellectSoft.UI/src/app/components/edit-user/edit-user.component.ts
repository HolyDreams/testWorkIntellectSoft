import { Component, EventEmitter, Injectable, Input, OnInit, Output } from '@angular/core';
import { Answer } from 'src/app/models/answer';
import { Phone } from 'src/app/models/phones';
import { User } from 'src/app/models/users';
import { CheckMaxLength } from 'src/app/services/check-max-length';
import { UserService } from 'src/app/services/users.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  @Input() user?: User;
  @Output() userUpdated = new EventEmitter<User[]>();
maxLength: number;

constructor(private userService: UserService, private checkMaxLength: CheckMaxLength) { 
  this.maxLength = checkMaxLength.getMaxLength();
}

  ngOnInit(): void {
  }

  updateUser(user:User) {
    user.phones = user.phones.filter(a => a.phoneNumber.length > 0)
    this.userService
      .updateUser(user)
      .subscribe((answer: Answer) => this.userUpdated.emit(answer.users));

      this.user = undefined;
  }

  createUser(user:User) {
    this.userService
    .createUser(user)
    .subscribe((answer: Answer) => this.userUpdated.emit(answer.users));

    this.user = undefined;
  }

  deletePhone(phone:Phone, user: User) {
    user.phones = user.phones.filter(a => a != phone);
  }

  addPhone(user:User) {
    if (user.phones == undefined)
    {
      let phones: Phone[] = [];
      user.phones = phones;
    }
    user.phones.push(new Phone);
  }

  cancelUser() {
    this.user = undefined;
  }

  editYear(event: any) {
    if (event.target.value.length > 4)
      event.target.value = event.target.value;
  }
}
