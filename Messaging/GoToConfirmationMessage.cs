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
    public class GoToConfirmationMessage { }
}
