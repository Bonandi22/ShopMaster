import { NgModule } from '@angular/core';
import { AuthService } from './auth.service';
import { ConfigService } from './config.service';
import { LoggerService } from './logger.service';

@NgModule({
  providers: [AuthService, ConfigService, LoggerService]
})
export class ServicesModule { }
