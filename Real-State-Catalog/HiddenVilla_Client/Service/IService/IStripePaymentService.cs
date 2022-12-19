using Models;
using System.Threading.Tasks;

namespace Real_State_Catalog_Client.Service.IService
{
    public interface IStripePaymentService
    {

        public Task<SuccessModel> CheckOutCompleted(StripePaymentDTO model);

    }
}
