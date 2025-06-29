// import { bootstrapApplication } from '@angular/platform-browser';
// import { provideRouter } from '@angular/router';
// import { importProvidersFrom } from '@angular/core';
// import { HttpClientModule } from '@angular/common/http';  // <--- Import here

// import { routes } from './app/app.routes';
// import { App } from './app/app';

// bootstrapApplication(App, {
//   providers: [
//     provideRouter(routes),
//     importProvidersFrom(HttpClientModule),  // <--- Add this
//   ],
// }).catch(err => console.error(err));
  import { bootstrapApplication } from '@angular/platform-browser';
  import { provideRouter } from '@angular/router';
  import { importProvidersFrom } from '@angular/core';
  import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

  import { routes } from './app/app.routes';
  import { App } from './app/app';
  import { AuthInterceptor } from './app/interceptors/auth';

  bootstrapApplication(App, {
    providers: [
      provideRouter(routes),
      importProvidersFrom(HttpClientModule),
      {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true,
      },
    ],
  }).catch(err => console.error(err));
