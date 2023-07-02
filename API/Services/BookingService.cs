using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public IEnumerable<GetBookingDto>? GetBooking()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return null; // No Booking  found
        }

        var toDto = bookings.Select(booking =>
                                            new GetBookingDto
                                            {
                                                Guid = booking.Guid,
                                                StartDate = booking.StartDate,
                                                EndDate = booking.EndDate,
                                                Status = booking.Status,
                                                Remarks = booking.Remarks,
                                                RoomGuid = booking.RoomGuid,
                                                EmployeeGuid = booking.EmployeeGuid
                                            }).ToList();

        return toDto; // Booking found
    }

    public GetBookingDto? GetBooking(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return null; // booking not found
        }

        var toDto = new GetBookingDto
        {
            Guid = booking.Guid,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid
        };

        return toDto; // bookings found
    }

    public GetBookingDto? CreateBooking(NewBookingDto newBookingDto)
    {
        var booking = new Booking
        {
            Guid = new Guid(),
            StartDate = newBookingDto.StartDate,
            EndDate = newBookingDto.EndDate,
            Status = newBookingDto.Status,
            Remarks = newBookingDto.Remarks,
            RoomGuid = newBookingDto.RoomGuid,
            EmployeeGuid = newBookingDto.EmployeeGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdBooking = _bookingRepository.Create(booking);
        if (createdBooking is null)
        {
            return null; // Booking not created
        }

        var toDto = new GetBookingDto
        {
            Guid = createdBooking.Guid,
            StartDate = newBookingDto.StartDate,
            EndDate = newBookingDto.EndDate,
            Status = newBookingDto.Status,
            Remarks = newBookingDto.Remarks,
            RoomGuid = newBookingDto.RoomGuid,
            EmployeeGuid = newBookingDto.EmployeeGuid,
        };

        return toDto; // Booking created
    }

    public int UpdateBooking(UpdateBookingDto updateBookingDto)
    {
        var isExist = _bookingRepository.IsExist(updateBookingDto.Guid);
        if (!isExist)
        {
            return -1; // Booking not found
        }

        var getBooking = _bookingRepository.GetByGuid(updateBookingDto.Guid);

        var booking = new Booking
        {
            Guid = updateBookingDto.Guid,
            StartDate = updateBookingDto.StartDate,
            EndDate = updateBookingDto.EndDate,
            Status = updateBookingDto.Status,
            Remarks = updateBookingDto.Remarks,
            RoomGuid = updateBookingDto.RoomGuid,
            EmployeeGuid = updateBookingDto.EmployeeGuid,
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
        var isDelete = _bookingRepository.Delete(booking!);
        if (!isDelete)
        {
            return 0; // Booking not deleted
        }

        return 1;
    }

    public IEnumerable<BookingDetailsDto>? GetBookingsDetails()
    {
        var bookings = _bookingRepository.GetBookingsDetails();

        if (bookings == null)
        {
            return null;
        }

        return bookings;
    }

    public BookingDetailsDto? GetBookingDetailsByGuid(Guid guid)
    {
        var bookingsWithRelation = GetBookingsDetails();

        if (bookingsWithRelation == null)
        {
            return null;
        }

        var bookingWithRelationByGuid = bookingsWithRelation.FirstOrDefault(b => b.Guid == guid);

        return bookingWithRelationByGuid;
    }

    public IEnumerable<BookingLengthDto>? GetBookingDurations()
    {
        var bookingRooms = (from booking in _bookingRepository.GetAll()
                            join room in _roomRepository.GetAll()
                            on booking.RoomGuid equals room.Guid
                            select new BookingRoomDto
                            {
                                RoomGuid = room.Guid,
                                StartDate = booking.StartDate,
                                EndDate = booking.EndDate,
                                RoomName = room.Name,
                            }
                            ).ToList();

        if (bookingRooms.Count == 0)
        {
            return null;
        }

        var bookingDurations = new List<BookingLengthDto>();

        foreach (var bookingRoom in bookingRooms)
        {
            TimeSpan duration = bookingRoom.EndDate - bookingRoom.StartDate;

            int totalDays = (int)duration.TotalDays;
            int weekends = 0;

            for (int i = 0; i <= totalDays; i++)
            {
                var currentDate = bookingRoom.StartDate.AddDays(i);

                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekends++;
                }
            }

            TimeSpan bookingLength = duration - TimeSpan.FromDays(weekends);
            string bookingLengthFormat = $"{bookingLength.Days} Hari, {bookingLength.Hours} Jam, {bookingLength.Minutes} Menit";

            var bookingLengthDto = new BookingLengthDto
            {
                RoomGuid = bookingRoom.RoomGuid,
                RoomName = bookingRoom.RoomName,
                BookingLength = bookingLengthFormat
            };

            bookingDurations.Add(bookingLengthDto);

        }

        return bookingDurations;

    }
}

