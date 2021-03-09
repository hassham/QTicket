import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule  } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { MainContentComponent } from './components/main-content/main-content.component';
import { RightContentComponent } from './components/right-content/right-content.component';
import { NavComponent } from './components/nav/nav.component';
import { ServeComponent } from './components/serve/serve.component';
import { MyhistoryComponent } from './components/myhistory/myhistory.component';
import { HomeComponent } from './components/home/home.component';



@NgModule({
  declarations: [
    AppComponent,
    MainContentComponent,
    RightContentComponent,
    NavComponent,
    ServeComponent,
    MyhistoryComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
