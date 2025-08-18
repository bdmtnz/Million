// src/components/AppBreadcrumb.test.tsx
import { render, screen } from '@testing-library/react';
import { vi } from 'vitest';
import AppBreadcrumb from './AppBreadcrumb';

// Mock del hook useBreadcrumbs
const mockItems = [
    { label: 'Properties', url: '/properties' },
    { label: 'Details' }
];
vi.mock('../hooks/useBreadcrumbs', () => ({
    useBreadcrumbs: () => ({ items: mockItems }),
}));

// Mock del componente de PrimeReact y el Link de React Router
vi.mock('primereact/breadcrumb', () => ({
    BreadCrumb: (props: any) => (
        <div data-testid='mock-breadcrumb'>Breadcrumb {props.tooltip}</div>
    )
}));

vi.mock('primereact/button', () => ({
    Button: (props: any) => (
        <button onClick={props.onClick} data-testid="action-button">
            {props.tooltip}
        </button>
    ),
}));

vi.mock('react-router-dom', () => ({
    Link: vi.fn(({ to, children }) => <a href={to}>{children}</a>),
}));


describe('AppBreadcrumb', () => {

    test('renders the BreadCrumb component with correct props', () => {
        // Arrange & Act
        render(<AppBreadcrumb />);

        // Assert
        // 1. Verificar que el mock de BreadCrumb se renderiza
        expect(screen.getByTestId('mock-breadcrumb')).toBeInTheDocument();
    });
});
