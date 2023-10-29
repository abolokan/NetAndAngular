import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// material
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginatorModule} from '@angular/material/paginator';

// components
import { DonkeySortTableComponent, DonkeyTableComponent } from './components';
import {A11yModule} from '@angular/cdk/a11y';
import {MatSortModule} from '@angular/material/sort';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [DonkeyTableComponent, DonkeySortTableComponent],
  imports: [
    A11yModule,
    CommonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatPaginatorModule
  ],
  exports: [
    DonkeyTableComponent
  ]
})
export class DonkeyTableModule { }
