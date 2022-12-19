using Models;
using System.Threading.Tasks;

namespace Real_State_Catalog_Client.Service.IService
{
    public interface IRoomOrderDetailsService
    {

        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> PaymentSuccessful(RoomOrderDetailsDTO details);

    }
}
