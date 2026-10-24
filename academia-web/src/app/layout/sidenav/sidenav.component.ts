import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { HasRoleDirective } from '../../shared/directives/has-role.directive';

@Component({
  selector: 'app-sidenav',
  imports: [RouterLink, RouterLinkActive, MatListModule, MatIconModule, HasRoleDirective],
  templateUrl: './sidenav.component.html',
})
export class SidenavComponent {}
