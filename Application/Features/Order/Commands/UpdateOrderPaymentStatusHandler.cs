using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderPaymentStatusHandler: IRequestHandler<UpdateOrderPaymentStatusCommand,CompleteOrderResponse>
    {
        private readonly IOrderCommand command;
        private readonly IOrderQuery query;
        private readonly IOrderDetailCommand Detailcommand;
        private readonly IOrderDetailQuery Detailquery;

        public UpdateOrderPaymentStatusHandler(IOrderCommand command, IOrderQuery query, IOrderDetailCommand detailcommand, IOrderDetailQuery detailquery)
        {
            this.command = command;
            this.query = query;
            this.Detailcommand = detailcommand;
            this.Detailquery = detailquery;
        }

        public async Task<CompleteOrderResponse> Handle(UpdateOrderPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await query.GetByIdAsync(request.Id, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"No se encontró la orden con el ID {request.Id}");
            }

            if (request.request.Status <= 0 || request.request.Status > 2)
            {
                throw new ArgumentException("Estado inválido");
            }

            if (request.request.Status < order.PaymentStatusId) 
            {
                throw new ArgumentException($"No se puede actualizar a un estado previo");
            }
            
            await command.UpdateOrderPaymentStatus(order, request.request.Status, cancellationToken);

            var updateOrder = await query.GetByIdAsync(order.OrderId, cancellationToken);


            var details = await Detailquery.GetOrderDetailsByOrderId(order.OrderId,cancellationToken);

            foreach (var od in details) 
            {
                await Detailcommand.updateTransactionIdAsync(od, request.request.transactionId, cancellationToken);
            }

            return new CompleteOrderResponse
            {
                OrderId= updateOrder.OrderId,
                UserId= updateOrder.UserId,
                TotalAmount= updateOrder.TotalAmount,
                PaymentType = new GenericResponse 
                { 
                Id= updateOrder.PaymentType.PaymentId,
                Name=updateOrder.PaymentType.PaymentName,
                },
                PaymentStatus = new GenericResponse
                {
                    Id = updateOrder.PaymentStatus.PaymentStatusId,
                    Name = updateOrder.PaymentStatus.PaymentStatusName,
                },
                OrderStatus=new GenericResponse
                {
                Id=updateOrder.OrderStatus.OrderStatusId,
                Name=updateOrder.OrderStatus.StatusName
                },
                Details=updateOrder.OrderDetails.Select(od => new OrderDetailsResponse 
                {
                DetailId=od.DetailId,
                TicketId=od.TicketId,
                transactionId=od.transactionId,
                UnitPrice=od.UnitPrice,
                Quantity=od.Quantity,
                SubTotal=od.Subtotal,
                }).ToList(),
                CreateAt=updateOrder.BuyDate,
            };
         
        }
    }
}
