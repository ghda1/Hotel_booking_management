public abstract class Room
{
    private int roomNumber;
    private double price;
    private bool isAvailable = true;

    public int RoomNumber
    {
        get { return roomNumber; }
        set
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)) || value < 0)
            {
                throw new Exception("Room number can not be negative, null or empty.");
            }
            roomNumber = value;
        }
    }
    public double Price
    {
        get { return price; }
        set
        {
            if (string.IsNullOrEmpty(Convert.ToString(price)) || value < 0)
            {
                throw new Exception("The price can not be negative, null or empty.");
            }
            price = value;
        }
    }
    public bool IsAvailable
    {
        get { return isAvailable; }
        set
        {
            if (string.IsNullOrEmpty(Convert.ToString(isAvailable)))
            {
                throw new Exception("Room availability can not be null or empty.");
            }
            isAvailable = value;
        }
    }
    public abstract void CalculatePrice();
    public void BookRoom(int roomNumber)
    {

    }

}
public interface IPayment
{
    public void ProcessPayment(double price);
    public void GenerateReceipt();
}
public class CreditCardPayment : IPayment
{
    public void GenerateReceipt()
    {

    }

    public void ProcessPayment(double price)
    {

    }
}
public class PayPalPayment : IPayment
{
    public void GenerateReceipt()
    {

    }

    public void ProcessPayment(double price)
    {

    }
}

public record Customer(int CustomerID, string Name, string ContactInfo);

public class StandardRoom : Room
{
    public override void CalculatePrice()
    {

    }
}

public class DeluxeRoom : Room
{
    public override void CalculatePrice()
    {

    }

}

public class SuiteRoom : Room
{
    public override void CalculatePrice()
    {

    }
}

public class Booking
{
    private int bookingId;
    private Room room;
    private Customer customer;
    private double totalAmount;
    public DateTime checkInDate;
    public DateTime checkOutDate;
    public static List<Booking> bookingList = new List<Booking>();
    public int BookingId
    {
        get { return bookingId; }
        set
        {
            if (string.IsNullOrEmpty(Convert.ToString(bookingId)) || value < 0)
            {
                throw new Exception("The booking ID can not be negative, null or empty.");
            }
            bookingId = value;
        }
    }
    public Room Room
    {
        get { return room; }
        set
        {
            if (value == null)
            {
                throw new Exception("This room is null!");
            }
            room = value;
        }
    }
    private Customer Customer
    {
        get { return customer; }
        set
        {
            if (value == null)
            {
                throw new Exception("The customer is null!");
            }
            customer = value;
        }
    }
    
    public Booking(int id, Room room, Customer customer, DateTime checkIn, DateTime checkOut, double? totalAmount)
    {
        if (string.IsNullOrEmpty(Convert.ToString(id)) || id < 0)
        {
            throw new Exception("The booking ID can not be negative, null or empty.");
        }
        BookingId = id;
        if (room == null)
        {
            throw new Exception("This room is null!");
        }
        Room = room;
        if (customer == null)
        {
            throw new Exception("The customer is null!");
        }
        Customer = customer;

        this.checkInDate = checkIn;
        this.checkOutDate = checkOut;
        this.totalAmount = totalAmount ?? CalculateTotalAmount();
    }

    public static double CalculateTotalAmount()
    {
        return 0;
    }

    public void ConfirmBooking()
    {

    }
    public static void CreateBooking(int id, Room room, Customer customer, DateTime checkIn, DateTime checkOut)
    {
        Booking booking = new Booking(id, room, customer, checkIn, checkOut, CalculateTotalAmount());
        bookingList.Add(booking);

    }
    public static void CancelBooking(int bookingId)
    {
        var foundBooking = bookingList.FirstOrDefault(booking => booking.BookingId == bookingId);
        if (foundBooking == null)
        {
            Console.WriteLine($"There is no booking match this ID.");
            return;
        }
        bookingList.Remove(foundBooking);

    }
    public static void UpdateBooking(int id, DateTime newCheckIn, DateTime newCheckOut)
    {
        var foundBooking = bookingList.FirstOrDefault(booking => booking.BookingId == id);
        if (foundBooking == null)
        {
            throw new Exception("There is no booking match this ID.");
        }

    }
    public static void DisplayBookings()
    {
        foreach (var booking in bookingList)
        {
            Console.WriteLine($"Booking ID: {booking.BookingId}, Customer: {booking.Customer}, Room: {booking.Room}, Total: ${booking.totalAmount}");
        }
    }
}

class Program
{

    static void Main(string[] args)
    {
        // Create sample rooms and customers
        Room standardRoom = new StandardRoom { RoomNumber = 101 };
        Room deluxeRoom = new DeluxeRoom { RoomNumber = 102 };
        Room suiteRoom = new SuiteRoom { RoomNumber = 103 };

        Customer customer1 = new(1, "John Doe", "john@example.com");
        Customer customer2 = new(2, "Jane Smith", "jane@example.com");

        // Create bookings
        Booking.CreateBooking(1, standardRoom, customer1, DateTime.Now, DateTime.Now.AddDays(3));
        Booking.CreateBooking(2, deluxeRoom, customer2, DateTime.Now, DateTime.Now.AddDays(2));

        // Display all bookings
        Booking.DisplayBookings();

        // Update booking
        Booking.UpdateBooking(1, DateTime.Now, DateTime.Now.AddDays(4));

        // Display all bookings after update
        Booking.DisplayBookings();

        // Cancel a booking
        Booking.CancelBooking(1);

        // Display bookings using LINQ query
        //Booking.DisplayAvailableRooms();

        // Process payment
        IPayment payment = new CreditCardPayment();
        payment.ProcessPayment(300);
        payment.GenerateReceipt();
    }

}