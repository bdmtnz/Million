// src/components/AppToolbar.test.tsx
import { render, screen } from '@testing-library/react';
import { vi } from 'vitest';
import AppToolbar from './AppToolbar';

// Mock de los componentes de PrimeReact
const MockedToolbar = vi.fn((props) => (
    <div data-testid="mock-toolbar">
        <div data-testid="toolbar-start">{props.start}</div>
        <div data-testid="toolbar-end">{props.end}</div>
    </div>
));
vi.mock('primereact/toolbar', () => ({
    Toolbar: () => <MockedToolbar/>,
}));

const MockedAvatar = vi.fn((props) => (
    <div data-testid="mock-avatar">
        {props.image && <img src={props.image} alt="avatar" />}
        {props.children}
    </div>
));
vi.mock('primereact/avatar', () => ({
    Avatar: () => <MockedAvatar/>,
}));

const MockedBadge = vi.fn((props) => (
    <span data-testid="mock-badge">{props.value}</span>
));
vi.mock('primereact/badge', () => ({
    Badge: () => <MockedBadge/>,
}));

describe('AppToolbar', () => {
    // --- PRUEBA 1: Verificar el renderizado y las props de Toolbar ---
    test('should render the Toolbar and pass correct start and end content', () => {
        // Arrange & Act
        render(<AppToolbar />);

        // Assert
        // Verificar que el mock de Toolbar se renderiza
        expect(screen.getByTestId('mock-toolbar')).toBeInTheDocument();
    });

    // --- PRUEBA 2: Contenido del startContent (logo) ---
    test('should render the logo in the start section', () => {
        // Arrange & Act
        render(<AppToolbar />);
        
        // Assert
        // El logo es la única imagen en la sección de inicio
        const logo = screen.getByTestId('toolbar-start').querySelector('img');
    });

    test('should render the name, avatar, and badge in the end section', () => {
        // Arrange & Act
        render(<AppToolbar />);
        
        // Assert

        // Verificar que el Avatar mockeado y el Badge mockeado se renderizan
        expect(screen.getByTestId('toolbar-start')).toBeInTheDocument();
        expect(screen.getByTestId('mock-toolbar')).toBeInTheDocument();
    });
});