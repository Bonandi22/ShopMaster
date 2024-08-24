export interface Order {
  id: string;
  customerName: string;
  totalAmount: number;
  orderDate: Date;
  status: string;
  // Adicione mais campos conforme necessário
}
