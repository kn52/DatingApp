import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Member } from './app.model';
import { ApiResponse, ApiResponseUtil } from '../api-response.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})

export class App implements OnInit {

  private http = inject(HttpClient);

  ngOnInit(): void {
    if (typeof window === 'undefined') return;

    // Expose Angular method
    (window as any).getMembers = this.getMembers.bind(this);

    // Load external JS
    const script = document.createElement('script');
    script.src = 'assets/members.js';
    script.defer = true;

    document.body.appendChild(script);

    console.log("script loaded");
  }

  getMembers(): Promise<ApiResponse<Member[]>> {
    const url = 'https://localhost:7297/api/Members/GetMembers';

    return firstValueFrom(
      this.http.get<ApiResponse<Member[]>>(url)
    ).catch(() => ApiResponseUtil.error<Member[]>('Failed', []));
  }
}
