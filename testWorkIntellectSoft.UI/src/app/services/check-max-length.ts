import { Injectable } from "@angular/core";

@Injectable()
export class CheckMaxLength {


  MAXLENGTH = 4;

  getMaxLength(){
    return this.MAXLENGTH;
  }
}