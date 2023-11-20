import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { CheckMaxLength } from './services/check-max-length';

@NgModule({
  declarations: [
    AppComponent,
    EditUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [CheckMaxLength],
  bootstrap: [AppComponent]
})
export class AppModule { }
