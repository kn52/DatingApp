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

  protected apiBaseUrl = 'https://localhost:7297';
  public title = "Members List";
  private http = inject(HttpClient);

  ngOnInit(): void {
    if (typeof window === 'undefined') return;

    // Expose Angular method
    (window as any).getMembers = this.getMembers.bind(this);
    (window as any).getMemberById = this.getMemberById.bind(this);
    (window as any).addMember = this.addMember.bind(this);
    (window as any).updateMember = this.updateMember.bind(this);
    (window as any).deleteMember = this.deleteMember.bind(this);

    // Load external JS
    const script = document.createElement('script');
    script.src = 'assets/members.js';
    script.defer = true;

    document.body.appendChild(script);

    console.log("script loaded");
  }

  getMembers(): Promise<ApiResponse<Member[]>> {
    const url = `${this.apiBaseUrl}/api/Members/GetMembers`;

    return firstValueFrom(
      this.http.get<ApiResponse<Member[]>>(url)
    ).catch(() => ApiResponseUtil.error<Member[]>('Failed', []));
  }

  getMemberById(id: number): Promise<ApiResponse<Member>> {
    const url = `${this.apiBaseUrl}/api/Members/GetMemberById/?Id=${id}`;

    return firstValueFrom(
      this.http.get<ApiResponse<Member>>(url)
    ).catch(() => ApiResponseUtil.error<Member>('Failed', {} as Member));
  }

  addMember(member: Member): Promise<ApiResponse<Member>> {
    const url = `${this.apiBaseUrl}/api/Members/AddMember`;

    return firstValueFrom(
      this.http.post<ApiResponse<Member>>(url, member)
    ).catch(() => ApiResponseUtil.error<Member>('Failed', {} as Member));
  }

  updateMember(member: Member): Promise<ApiResponse<Member>> {
    const url = `${this.apiBaseUrl}/api/Members/UpdateMember`;

    return firstValueFrom(
      this.http.put<ApiResponse<Member>>(url, member)
    ).catch(() => ApiResponseUtil.error<Member>('Failed', {} as Member));
  }

  deleteMember(id: number): Promise<ApiResponse<boolean>> {
    const url = `${this.apiBaseUrl}/api/Members/DeleteMember/?Id=${id}`;

    return firstValueFrom(
      this.http.delete<ApiResponse<boolean>>(url)
    ).catch(() => ApiResponseUtil.error<boolean>('Failed', false));
  }
}
