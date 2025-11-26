import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-navigation-menu',
  standalone: false,
  templateUrl: './navigation-menu.component.html',
  styleUrl: './navigation-menu.component.scss'
})
export class NavigationMenuComponent {
  @Input() isAuthenticated: boolean = false;
}
