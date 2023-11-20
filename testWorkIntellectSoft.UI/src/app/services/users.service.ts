import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Answer } from '../models/answer';
import { User } from '../models/users';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private url = 'User';

  constructor(private http: HttpClient) {}

  public getUsers(): Observable<Answer> {
    return this.http.get<Answer>(`${environment.apiUrl}/${this.url}`);
  }

  public getSearch(search: string): Observable<Answer> {
    if (search == "" || null)
      return this.getUsers();
    
    let params = new HttpParams().set("search", search);
    return this.http.get<Answer>(`${environment.apiUrl}/${this.url}`, { params: params });
  }

  public updateUser(user: User): Observable<Answer> {
    return this.http.put<Answer>(
      `${environment.apiUrl}/${this.url}`,
      user
    );
  }

  public createUser(user: User): Observable<Answer> {
    return this.http.post<Answer>(
      `${environment.apiUrl}/${this.url}`,
      user
    );
  }

  public deleteUser(user: User): Observable<Answer> {
    return this.http.delete<Answer>(`${environment.apiUrl}/${this.url}/${user.id}`)
  }
  /*public updateHero(hero: SuperHero): Observable<SuperHero[]> {
    return this.http.put<SuperHero[]>(
      `${environment.apiUrl}/${this.url}`,
      hero
    );
  }

  public createHero(hero: SuperHero): Observable<SuperHero[]> {
    return this.http.post<SuperHero[]>(
      `${environment.apiUrl}/${this.url}`,
      hero
    );
  }

  public deleteHero(hero: SuperHero): Observable<SuperHero[]> {
    return this.http.delete<SuperHero[]>(
      `${environment.apiUrl}/${this.url}/${hero.id}`
    );
  }*/
}
