import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import { SidenavComponent } from '../sidenav/sidenav.component';

// Estructura de las páginas privadas: toolbar arriba, menú lateral y contenido
@Component({
  selector: 'app-shell',
  imports: [RouterOutlet, MatSidenavModule, ToolbarComponent, SidenavComponent],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss',
})
export class ShellComponent {
  protected readonly menuOpen = signal(true);
}
