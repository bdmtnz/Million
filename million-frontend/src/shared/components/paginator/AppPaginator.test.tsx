// src/components/AppPaginator.test.tsx
import { render, screen } from '@testing-library/react';
import { vi } from 'vitest';
import AppPaginator from './AppPaginator';

// Mock de Paginator y Dropdown de PrimeReact
vi.mock('primereact/paginator', () => ({
    Paginator: vi.fn(() => <div data-testid="mock-paginator">Paginator</div>),
}));
vi.mock('primereact/dropdown', () => ({
    Dropdown: vi.fn(() => <div data-testid="mock-dropdown" />),
}));

describe('AppPaginator', () => {
    // Mocks de las funciones de estado
    const mockSetFirst = vi.fn();
    const mockSetPageSize = vi.fn();
    const mockSetPage = vi.fn();

    // Props de prueba
    const defaultProps = {
        first: 0,
        total: 100,
        pageSize: 10,
        setFirst: mockSetFirst,
        setPageSize: mockSetPageSize,
        setPage: mockSetPage,
    };

    beforeEach(() => {
        vi.clearAllMocks();
    });

    test('renders the Paginator with correct props', () => {
        // Arrange & Act
        render(<AppPaginator {...defaultProps} />);
    });

    test('renders the custom current page report template', () => {
        // Arrange
        render(<AppPaginator {...defaultProps} />);

        // Act & Assert
        const pageReportText = screen.getByText('Paginator');
        expect(pageReportText).toBeInTheDocument();
    });
});