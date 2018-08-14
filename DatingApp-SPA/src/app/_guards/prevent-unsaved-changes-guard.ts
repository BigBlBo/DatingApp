import { Injectable } from '@angular/core';
import { CanActivate, Router, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  canDeactivate(component: MemberEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue.');
    }

    return true;
  }
}
