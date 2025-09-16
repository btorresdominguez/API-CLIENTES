import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  template: `
    <div class="app-container">
      <header class="app-header">
        <h1>Sistema de Gestión de Clientes</h1>
      </header>
      <main class="app-main">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  styles: [`
    .app-container {
      min-height: 100vh;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    }
    
    .app-header {
      background: rgba(255, 255, 255, 0.1);
      backdrop-filter: blur(10px);
      padding: 1rem 0;
      text-align: center;
      border-bottom: 1px solid rgba(255, 255, 255, 0.2);
    }
    
    .app-header h1 {
      color: white;
      margin: 0;
      font-size: 1.5rem;
      font-weight: 600;
    }
    
    .app-main {
      padding: 2rem 0;
    }
    
    @media (max-width: 768px) {
      .app-main {
        padding: 1rem;
      }
      
      .app-header h1 {
        font-size: 1.2rem;
      }
    }
  `]
})
export class AppComponent {
  title = 'ClientesFrontend';
}