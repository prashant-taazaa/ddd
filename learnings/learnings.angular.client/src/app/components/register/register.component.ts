import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  success: boolean = false;
  error: string = '';
  userRegistration = { name: '', email: '', password: '', role: '' };
  submitted: boolean = false;
  Roles = ['Admin', 'AppUser'];
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  Submit() {
    this.submitted = true;
    this.authService.register(this.userRegistration).subscribe(
      (response: any) => {
        this.success = true;
        this.submitted = false;
      },
      (error) => {
        console.log(error);
        this.submitted = false;
      }
    );
  }
}
