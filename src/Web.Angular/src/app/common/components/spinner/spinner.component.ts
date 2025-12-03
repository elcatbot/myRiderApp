import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-spinner',
  standalone: false,
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss'
})
export class SpinnerComponent {
  @Input() message: string = 'Searching for drivers nearby...';
  @Output() cancel = new EventEmitter<void>();

   onCancel() {
    this.cancel.emit();
  }
}
