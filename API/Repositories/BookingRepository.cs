using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    public BookingRepository(BookingDBContext context) : base(context)
    {

    }

}

