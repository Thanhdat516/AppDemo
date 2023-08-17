import { Component } from '@angular/core';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {
  constructor() { }
  navigation: any[] =
  [
    {
      id: 1,
      text: "Company",
      path: "company"
    },
    {
      id: 2,
      text: "Employee",
      path: "employee"
    },
  ]
}
