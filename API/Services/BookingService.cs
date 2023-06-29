using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public IEnumerable<BookingDto>? GetBooking()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return null; // No bookings found
        }

        var toDto = bookings.Select(booking =>
                                            new BookingDto
                                            {
                                                GUID = booking.GUID,
                                                StartDate = booking.StartDate,
                                                EndDate = booking.EndDate,
                                                Remarks = booking.Remarks,
                                                Status = booking.Status,
                                                RoomGUID = booking.RoomGUID,
                                                EmployeeGUID = booking.EmployeeGUID,
                                            }).ToList();

        return toDto; // Universities found
    }

    public BookingDto? GetBooking(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return null; // Booking not found
        }

        var toDto = new BookingDto
        {
            GUID = booking.GUID,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Remarks = booking.Remarks,
            Status = booking.Status,
            RoomGUID = booking.RoomGUID,
            EmployeeGUID = booking.EmployeeGUID,
        };

        return toDto; // Universities found
    }

    public BookingDto? CreateBooking(NewBookingDto newBookingDto)
    {
        var booking = new Booking
        {
            GUID = new Guid(),
            StartDate = newBookingDto.StartDate,
            EndDate = newBookingDto.EndDate,
            Remarks = newBookingDto.Remarks,
            Status = newBookingDto.Status,
            RoomGUID = newBookingDto.RoomGUID,
            EmployeeGUID = newBookingDto.EmployeeGUID,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdBooking = _bookingRepository.Create(booking);
        if (createdBooking is null)
        {
            return null; // Booking not created
        }

        var toDto = new BookingDto
        {
            GUID = booking.GUID,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Remarks = booking.Remarks,
            Status = booking.Status,
            RoomGUID = booking.RoomGUID,
            EmployeeGUID = booking.EmployeeGUID,
        };

        return toDto; // Booking created
    }

    public int UpdateBooking(BookingDto updateBookingDto)
    {
        var isExist = _bookingRepository.IsExist(updateBookingDto.GUID);
        if (!isExist)
        {
            return -1; // Booking not found
        }

        var getBooking = _bookingRepository.GetByGuid(updateBookingDto.GUID);

        var booking = new Booking
        {
            GUID = updateBookingDto.GUID,
            StartDate = updateBookingDto.StartDate,
            EndDate = updateBookingDto.EndDate,
            Remarks = updateBookingDto.Remarks,
            Status = updateBookingDto.Status,
            RoomGUID = updateBookingDto.RoomGUID,
            EmployeeGUID = updateBookingDto.EmployeeGUID,
            ModifiedDate = DateTime.Now,
            CreatedDate = getBooking!.CreatedDate
        };

        var isUpdate = _bookingRepository.Update(booking);
        if (!isUpdate)
        {
            return 0; // Booking not updated
        }

        return 1;

    }

    public int DeleteBooking(Guid guid)
    {
        var isExist = _bookingRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Booking not found
        }

        var booking = _bookingRepository.GetByGuid(guid);
        var isDelete = _bookingRepository.Delete(booking!.GUID);
        if (!isDelete)
        {
            return 0; // Booking not deleted
        }

        return 1;
    }
}


