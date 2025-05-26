using LuxuryCarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarRental.Messaging
{
    /// <summary>
    /// Sent by CheckoutViewModel when payment is successful,
    /// to tell the shell to show the Confirmation view.
    /// </summary>
    // Messaging/GoToConfirmationMessage.cs
    public class GoToConfirmationMessage
    {
        public Money Total { get; }
        public IEnumerable<CartItem> Items { get; }
        public Card PaymentCard { get; }

        public GoToConfirmationMessage(
            Money total,
            IEnumerable<CartItem> items,
            Card paymentCard)
        {
            Total = total;
            Items = items;
            PaymentCard = paymentCard;
        }
    }


}
