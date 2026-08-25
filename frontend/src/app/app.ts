import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaskItem, TaskService } from './task.service';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  private taskService = inject(TaskService);
  auth = inject(AuthService);

  tasks = signal<TaskItem[]>([]);
  newTaskName = '';

  loginEmployeeId = '';
  loginPassword = '';
  loginError = '';

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe(tasks => {
      this.tasks.set(tasks);
    });
  }

  addTask(): void {
    if (!this.newTaskName.trim()) {
      return;
    }

    this.taskService.createTask(this.newTaskName).subscribe(task => {
      this.tasks.update(tasks => [...tasks, task]);
      this.newTaskName = '';
    });
  }

  deleteTask(taskId: string): void {
    this.taskService.deleteTask(taskId).subscribe(() => {
      this.tasks.update(tasks => tasks.filter(task => task.taskId !== taskId));
    });
  }

  login(): void {
    this.loginError = '';

    this.auth.login(this.loginEmployeeId, this.loginPassword).subscribe({
      next: () => {
        this.loginEmployeeId = '';
        this.loginPassword = '';
      },
      error: () => {
        this.loginError = 'Login failed. Check your employee ID and password.';
      }
    });
  }

  logout(): void {
    this.auth.logout();
  }
}
