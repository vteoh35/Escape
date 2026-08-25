import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

// Mirrors Business_Logic.Tasks.TaskItem on the backend (both mock and real).
export interface TaskItem {
  taskId: string;
  name: string;
  description?: string | null;
  priorityId?: number | null;
  startDate?: string | null;
  endDate?: string | null;
  statusId?: number | null;
  projectId?: string | null;
  parentTaskId?: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/tasks`;

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  createTask(name: string): Observable<TaskItem> {
    // Task ids are manually assigned (not DB-generated), so the client picks one.
    const task: TaskItem = {
      taskId: crypto.randomUUID(),
      name
    };

    return this.http.post<TaskItem>(this.apiUrl, task);
  }

  updateTask(taskId: string, name: string): Observable<TaskItem> {
    return this.http.put<TaskItem>(`${this.apiUrl}/${taskId}`, { name });
  }

  deleteTask(taskId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${taskId}`);
  }
}
