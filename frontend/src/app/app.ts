import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaskItem, TaskService } from './task.service';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  private taskService = inject(TaskService);

  tasks = signal<TaskItem[]>([]);
  newTaskName = '';

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
}
