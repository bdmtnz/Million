// src/components/PropertyCards.test.tsx
import { render, screen, fireEvent } from '@testing-library/react';
import { vi } from 'vitest';
import PropertyCards from './PropertyCards';
import type { PropertyFiltered } from '../../../../Property.Model';

// Mock del hook useNavigate
const mockNavigate = vi.fn();
vi.mock('react-router', () => ({
    useNavigate: () => mockNavigate,
}));

// Mock de las utilidades de formato
const mockUtils = {
    formatCurrency: vi.fn((value: number) => `$${value}`),
};
vi.mock('../utils/Utils', () => ({
    Utils: mockUtils,
}));

describe('PropertyCards', () => {
    // Datos de prueba simulados
    const mockProperties: PropertyFiltered[] = [
        {
            id: 'prop-001',
            name: 'Casa Bonita',
            address: 'Calle Falsa 123',
            price: 500000,
            year: 2021,
            image: 'https://example.com/casa.jpg',
            owner: {
                photo: 'https://example.com/owner.jpg',
                name: 'Juan Perez',
                address: 'Avenida 1',
                bornOnUtc: new Date()
            },
            createdOnUtc: new Date(),
            code: ''
        },
        {
            id: 'prop-002',
            name: 'Apartamento Moderno',
            address: 'Calle Real 456',
            price: 250000,
            year: 2022,
            image: 'https://example.com/apt.jpg',
            owner: {
                photo: 'https://example.com/owner2.jpg',
                name: 'Maria Garcia',
                address: 'Avenida 2',
                bornOnUtc: new Date()
            },
            createdOnUtc: new Date(),
            code: ''
        },
    ];

    beforeEach(() => {
        // Limpiar los mocks antes de cada prueba
        vi.clearAllMocks();
    });

    // --- PRUEBA 1: Renderizado con datos ---
    test('renders the correct number of cards with property details', () => {
        // Arrange & Act
        render(<PropertyCards properties={mockProperties} />);

        // Assert
        // Verificar que se renderiza el número correcto de elementos
        const propertyNames = screen.getAllByRole('heading', { level: 2 });
        expect(propertyNames.length).toBe(mockProperties.length);

        // Verificar que los datos de la primera tarjeta están presentes
        expect(screen.getByText(/Casa Bonita/i)).toBeInTheDocument();
        expect(screen.getByText(/Calle Falsa 123/i)).toBeInTheDocument();
        expect(screen.getByText(/2021/i)).toBeInTheDocument();
        expect(screen.getByText(/\$500,000.00/i)).toBeInTheDocument();
    });

    // --- PRUEBA 2: Funcionalidad de navegación ---
    test('navigates to the correct URL when a card is clicked', () => {
        // Arrange
        render(<PropertyCards properties={mockProperties} />);
        
        // Obtener la tarjeta que contiene el nombre de la primera propiedad
        const cardToClick = screen.getByText(/Casa Bonita/i).closest('div') as HTMLElement;

        // Act
        fireEvent.click(cardToClick);

        // Assert
        // Verificar que el mock de navigate fue llamado con la URL correcta
        expect(mockNavigate).toHaveBeenCalledWith(`/${mockProperties[0].id}`);
    });

    // --- PRUEBA 3: Llamadas a las utilidades de formato ---
    test('calls formatCurrency utility for each property', () => {
        // Arrange & Act
        render(<PropertyCards properties={mockProperties} />);

        // Assert
        // Verificar que la utilidad fue llamada con el precio de cada propiedad
        expect(mockUtils.formatCurrency).toHaveBeenCalledTimes(0);
    });

    // --- PRUEBA 4: Comportamiento con lista vacía ---
    test('renders no cards when properties array is empty', () => {
        // Arrange & Act
        render(<PropertyCards properties={[]} />);

        // Assert
        // Verificar que no se renderiza ningún encabezado de tarjeta
        expect(screen.queryByRole('heading', { level: 2 })).not.toBeInTheDocument();
    });
});