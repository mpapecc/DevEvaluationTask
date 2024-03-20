import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ContactComponent } from './components/contact/contact.component';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { TagComponent } from './components/tag/tag.component';
import { AddContactPageComponent } from './pages/add-contact-page/add-contact-page.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactComponent,
    ContactListComponent,
    ContactFormComponent,
    MainPageComponent,
    SidenavComponent,
    TagComponent,
    AddContactPageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: MainPageComponent, pathMatch: 'full' },
      { path: 'add-contact', component: AddContactPageComponent },
      { path: 'contact-details/:id', component: AddContactPageComponent }

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
