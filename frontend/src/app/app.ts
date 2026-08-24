import { Component, OnInit, inject } from '@angular/core';
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

  tasks: TaskItem[] = [];
  newTaskName = '';

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe(tasks => {
      this.tasks = tasks;
    });
  }

  addTask(): void {
    if (!this.newTaskName.trim()) {
      return;
    }

    this.taskService.createTask(this.newTaskName).subscribe(task => {
      this.tasks.push(task);
      this.newTaskName = '';
    });
  }

  toggleTask(task: TaskItem): void {
    task.isCompleted = !task.isCompleted;

    this.taskService.updateTask(task).subscribe();
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe(() => {
      this.tasks = this.tasks.filter(task => task.id !== id);
    });
  }
}