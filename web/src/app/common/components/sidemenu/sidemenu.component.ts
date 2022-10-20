import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html'
})
export class SidemenuComponent implements OnInit {
  public modules: INavbar[];

  constructor(public router: Router) {
    this.modules = [
      {
        Menu: "Payroll", items: [
          { Parent: "payroll", Component: "employee", Name: "Employee" },
          { Parent: "payroll", Component: "incident", Name: "Incident" },
          { Parent: "payroll", Component: "incidentApproval", Name: "Incident Approval" }
        ]
      },
      {
        Menu: "Security", items: [
          { Parent: "security", Component: "user", Name: "User" },
          { Parent: "security", Component: "role", Name: "Role" }
        ]
      }
    ];
  }

  ngOnInit(): void {
  }



  public toggleMenu(idx: string) {
    // Level 1 : collapse others
    this.modules.forEach((mod, index) => {
      if (!$(`#mn-${index}`).prev('a').hasClass('collapsed') && `mn-${index}` !== idx) {
        $(`#mn-${index}`).slideUp('fast', () => $(`#mn-${index}`).prev('a').addClass('collapsed'))
      }
    });

    // Level 1: toggle collapse class for the click level
    $(`#${idx}`).slideToggle('fast');
  }

}
interface INavbar {
  Menu: string;
  items?: IItem[];
}

interface IItem {
  Component: string;
  Parent: string;
  Name: string;
}
