using EhCommerce.Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Contracts.Payment.MakePayment
{
    public interface IMakePaymentUseCase : IUseCase<MakePaymentInput, MakePaymentOutput>
    {
    }
}
