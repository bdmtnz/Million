export const Utils = {
    formatCurrency: (value: number) => {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
    },   
    formatDate: (value: Date) => {
        return value.toString().split('T')[0]
    }
}