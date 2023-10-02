import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactsRoutingModule } from './contacts-routing.module';
import { ContactsComponent } from './contacts.component';
import { ContactDetailsComponent } from './contact-details/contact-details.component';

import { ButtonModule } from 'primeng/button'
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table'
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { MessageModule } from 'primeng/message';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ContactsComponent,
    ContactDetailsComponent,
  ],
  imports: [
    CommonModule,
    ContactsRoutingModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    CalendarModule,
    ConfirmDialogModule,
    ToastModule,
    MessageModule,
    ReactiveFormsModule,
  ]
})
export class ContactsModule { }
