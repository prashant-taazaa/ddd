import { Component, OnInit } from '@angular/core';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-task-manager',
  templateUrl: './task-manager.component.html',
  styleUrls: ['./task-manager.component.scss'],
})
export class TaskManagerComponent implements OnInit {
  tasks: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];

  newTaskDescription = '';

  constructor(private _todoService: TodoService) {}

  ngOnInit(): void {}

  CreateTask() {
    const description = this.newTaskDescription.trim();
    if (description) {
      this._todoService.CreateTask(description).then(
        (response: any) => {},
        (error: any) => {
          console.log(error);
        }
      );
    }
  }
}
