import { Component, OnInit } from '@angular/core';
import { AuthService } from './common/auth.service';
import { IAuthStatus } from './common/models/auth/IAuthStatus';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'Web.Angular';

  constructor(public authService: AuthService) {}

  ngOnInit (): void {
    
  }


}
