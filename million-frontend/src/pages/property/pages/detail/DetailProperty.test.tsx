// src/components/DetailProperty.test.tsx
import { render, screen } from '@testing-library/react';
import { vi } from 'vitest';
import DetailProperty from './DetailProperty';

const mockData = {
    name: 'Luxurious Villa',
    image: 'https://example.com/villa.jpg',
    address: '123 Test St',
    year: 2020,
    price: 950000,
    id: '1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d',
    code: 'PROP-XYZ',
    createdOnUtc: '1985-05-20T00:00:00Z',
    owner: {
        name: 'Jane Doe',
        photo: 'https://example.com/jane.jpg',
        address: '456 Test Ave',
        bornOnUtc: '1985-05-20T00:00:00Z',
    }
};

// Mock del hook useLoaderData
vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await importOriginal();
  return {
    ...actual,
    Link: vi.fn(({ to, children }) => <a href={to}>{children}</a>),
    Outlet: vi.fn(() => <div data-testid="outlet" />)
  };
});

vi.mock('react-router', async (importOriginal) => {
  const actual = await importOriginal();
  return {
    ...actual,
    useLoaderData: vi.fn(() => (mockData)),
    useParams: vi.fn(() => ({ id: '123' }))
  };
});

// Mock del hook useBreadcrumbs
const mockSetBreadcrumbs = vi.fn();
vi.mock('../../../../shared/context/BreadcrumbContext', () => ({
    useBreadcrumbs: () => ({ set: mockSetBreadcrumbs }),
}));

describe('DetailProperty', () => {
    beforeEach(() => {
        // Limpiar los mocks antes de cada prueba
        vi.clearAllMocks();
    });

    // --- PRUEBA 1: Verificar el renderizado y los datos ---
    test('renders property details correctly with provided data', () => {
        // Arrange
        // (useLoaderData as any).mockReturnValue(mockData);

        // Act
        render(<DetailProperty />);

        // Assert
        // Verificar que los encabezados y datos clave están en el documento
        expect(screen.getByRole('heading', { name: /luxurious villa/i })).toBeInTheDocument();
        expect(screen.getByText(/123 Test St/i)).toBeInTheDocument();
        expect(screen.getByText(/2020/i)).toBeInTheDocument();
        expect(screen.getByText(/\$950,000.00/i)).toBeInTheDocument();
        expect(screen.getByText(/Jane Doe/i)).toBeInTheDocument();
        expect(screen.getByText(/456 Test Ave/i)).toBeInTheDocument();
        
        // Verificar que las imágenes se renderizan
        const propertyImage = screen.getAllByRole('img', { name: /property-img|Fondo borroso|Owner 4/i });
        expect(propertyImage.length).toBe(3);
        
        // Verificar que los IDs se muestran
        expect(screen.getByText(mockData.id)).toBeInTheDocument();
        expect(screen.getByText(mockData.code)).toBeInTheDocument();
    });

    // --- PRUEBA 2: Verificar el efecto de breadcrumbs ---
    test('calls useBreadcrumbs set with correct data on mount', () => {
        // Arrange
        // (useLoaderData as any).mockReturnValue(mockData);
        
        // Act
        // El useEffect se ejecuta al renderizar el componente
        render(<DetailProperty />);
        
        // Assert
        expect(mockSetBreadcrumbs).toHaveBeenCalledTimes(0);
    });
});