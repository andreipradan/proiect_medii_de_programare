
### Tables

```
    Room:
        Id
        Number
        Type (single, double, suite etc.)
        Price
        IsAvailable
```
```
    Booking:
        Id
        GuestId - fk to ApplicationUser
        RoomId - fk to Room
        CheckinDate
        CheckoutDate
        State (confirmed, declined, processing, canceled etc.)
        Reason (for declined reservations)
```
```
    ApplicationUser - custom identity user 
        FirstName
        LastName
        Email
```
```
    Facilities:
        Id
        Name
        Description
```
```
    Reviews:
        Id
        BookingId - fk to Booking
        Date
        Description
        Rating
```
```
    AdditionalServices:
        Id
        Name
        Description
        Price
```

#### Views

General:

    Room:
        List all available rooms & their facilities.
        Search based on type and availability
        Reserve - requires auth
    Reviews:
        List all reviews

Client:

    Room list:
        all the above + add a reservation for a date (Requires auth)
    Booking list:
        see all current and previous reservations
        update status (cancel active ones)
    Reviews:
        add review after booking is complete

Employee:

    Room list:
        all the above + create and edit room facilities
    Booking list:
        List all reservations - can confirm/decline active reservations
