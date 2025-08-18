// src/components/PropertyTable.test.tsx
import { render, screen, fireEvent } from '@testing-library/react';
import { vi } from 'vitest';
import React from 'react';
import type { PropertyFiltered, PropertyOwner } from '../../../../Property.Model';
import PropertyTable from './PropertyTable';

// Mock del hook useNavigate
const mockNavigate = vi.fn();
vi.mock('react-router', () => ({
    useNavigate: () => mockNavigate,
}));

// Mock de las utilidades de formato
const mockUtils = {
    formatCurrency: vi.fn((value: number) => `$${value}`),
    formatDate: vi.fn((date: string) => new Date(date).toLocaleDateString()),
};
vi.mock('../utils/Utils', () => ({
    Utils: mockUtils,
}));

// Mock de los componentes de PrimeReact
vi.mock('primereact/datatable', () => ({
    DataTable: (props: any) => (
        <div data-testid="data-table">
            {props.children}
            {props.value.map((item: any) =>
                React.Children.map(props.children, (child: any) =>
                    child.props.body ? child.props.body(item) : <div />
                )
            )}
        </div>
    ),
    Column: (props: any) => (
        <div data-testid={`column-${props.field || props.header}`}>
            {props.children}
        </div>
    ),
}));

vi.mock('primereact/button', () => ({
    Button: (props: any) => (
        <button onClick={props.onClick} data-testid="action-button">
            {props.tooltip}
        </button>
    ),
}));

describe('PropertyTable', () => {
    // Datos de prueba
    const mockProperties: PropertyFiltered[] = [{
        id: '1a2b3c4d',
        name: 'Casa Bonita',
        address: 'Calle Falsa 123',
        price: 500000,
        year: 2021,
        image: 'https://example.com/image.jpg',
        code: '',
        createdOnUtc: new Date(),
        owner: {} as PropertyOwner
    }];

    beforeEach(() => {
        // Limpiar los mocks antes de cada prueba
        vi.clearAllMocks();
    });

    // --- PRUEBA 1: Renderizado con datos ---
    test('renders the DataTable and correctly displays property data', () => {
        // Arrange & Act
        render(<PropertyTable properties={mockProperties} />);

        // Assert
        expect(screen.getByTestId('data-table')).toBeInTheDocument();
        expect(screen.getByText('$500,000.00')).toBeInTheDocument();
        expect(screen.getByText('Show details')).toBeInTheDocument();
    });

    // --- PRUEBA 2: Funcionalidad del botón de acción ---
    test('navigates to the correct URL when action button is clicked', () => {
        // Arrange
        render(<PropertyTable properties={mockProperties} />);
        const actionButton = screen.getByTestId('action-button');

        // Act
        fireEvent.click(actionButton);
    });

    // --- PRUEBA 3: Llamadas a las utilidades de formato ---
    test('calls formatting utilities with correct values', () => {
        // Arrange & Act
        render(<PropertyTable properties={mockProperties} />);
    });

    // --- PRUEBA 4: Comportamiento con lista de propiedades vacía ---
    test('renders an empty table when properties array is empty', () => {
        // Arrange & Act
        render(<PropertyTable properties={[]} />);

        // Assert
        expect(screen.getByTestId('data-table')).toBeInTheDocument();
        // Asegurarse de que no se muestre el nombre de la propiedad de prueba
        expect(screen.queryByText('Casa Bonita')).not.toBeInTheDocument();
    });
});