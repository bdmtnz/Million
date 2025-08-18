// src/components/AppProperty.test.tsx
import { render, screen } from '@testing-library/react';
import AppProperty from './AppProperty';

// Mockear el componente Outlet de React Router
vi.mock('react-router-dom', async (importOriginal: () => any) => {
    const actual = await importOriginal();
    return {
        ...actual,
        Outlet: () => <div data-testid="outlet-mock" />,
    };
});

describe('AppProperty', () => {
  test('renders the page header with correct titles', () => {
    // 1. Renderizar el componente
    render(<AppProperty />);

    // 2. Usar 'screen' para encontrar los elementos por su texto
    const titleElement = screen.getByRole('heading', { name: 'Properties' });
    const subtitleElement = screen.getByRole('heading', { name: 'Here you can manage the properties' });
    
    // 3. Afirmar que los elementos están en el documento
    expect(titleElement).toBeInTheDocument();
    expect(subtitleElement).toBeInTheDocument();
  });
});