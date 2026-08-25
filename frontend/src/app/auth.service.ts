import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';

const TOKEN_KEY = 'escape_auth_token';
const EMPLOYEE_ID_KEY = 'escape_employee_id';

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  employeeId = signal<string | null>(localStorage.getItem(EMPLOYEE_ID_KEY));
  isLoggedIn = computed(() => this.employeeId() !== null);

  login(employeeId: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, { employeeId, password }).pipe(
      tap(response => {
        localStorage.setItem(TOKEN_KEY, response.token);
        localStorage.setItem(EMPLOYEE_ID_KEY, employeeId);
        this.employeeId.set(employeeId);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(EMPLOYEE_ID_KEY);
    this.employeeId.set(null);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }
}
