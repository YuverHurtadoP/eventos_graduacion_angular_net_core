import { FormControl } from "@angular/forms";

export class ValidationCustomization {
  static spaceValidator(control: FormControl) {
    if ((control.value || "").trim().length === 0) {
      return { havespace: true };
    }

    return null;
  }
}
