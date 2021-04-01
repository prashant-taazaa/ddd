import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

const shared = [MatButtonModule,
  MatListModule,
  MatIconModule,
  MatInputModule];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ...shared
  ],
  exports: shared
})
export class MaterialModule { }
