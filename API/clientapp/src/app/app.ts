import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Member } from './app.model';
import { ApiResponse } from '../api-response.model';
import { ApiResponseUtil } from '../api-response.model';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {

  public title = "Members List";
  private http = inject(HttpClient);

  ngOnInit(): void {
    // Safe for SSR
    if (typeof window !== 'undefined') {
      (window as any).getMembers = this.getMembers.bind(this);
        const script = document.createElement('script');
          script.src = 'assets/members.js';
          script.onload = () => {
            console.log('members.js loaded');
            (window as any).loadMembers()
          };
          document.body.appendChild(script);
      
    }
  }

  getMembers(): Promise<ApiResponse<Member[]>> {
    const url = 'https://localhost:7297/api/Members/GetMembers';

    return firstValueFrom(
      this.http.get<ApiResponse<Member[]>>(url)
    ).catch(err => {
      console.error('Failed to fetch members:', err);
      return ApiResponseUtil.error<Member[]>('Failed to fetch members', []);
    });
  }
}
