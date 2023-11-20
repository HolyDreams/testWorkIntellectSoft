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
  answer: Answer | undefined;
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
  /*updateHeroList(heroes: SuperHero[]) {
    this.heroes = heroes;
  }

  initNewHero() {
    this.heroToEdit = new SuperHero();
  }

  editHero(hero: SuperHero) {
    this.heroToEdit = hero;
  }*/
}
