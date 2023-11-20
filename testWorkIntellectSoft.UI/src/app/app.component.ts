import { Component } from '@angular/core';
import { UserService } from './services/users.service';
import { Answer } from './models/answer';
import { User } from './models/users';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'User.UI';
  answer!: Answer;
  userToEdit?: User;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService
      .getUsers()
      .subscribe((result: Answer) => (this.answer = result));
  }

  initNewUser() {
      this.userToEdit = new User();
  }
  
  editUser(user: User) {
    this.userToEdit = user;
  }

  searchUser(event: any) {
    this.userService
      .getSearch(event.target.value)
      .subscribe((result: Answer) => (this.answer = result));
  }

  updateUserList(users: User[]) {
    this.answer.users = users;
  }

  delUser(user: User) {
    this.userService
    .deleteUser(user)
    .subscribe((result: Answer) => (this.answer = result));
  }
}
