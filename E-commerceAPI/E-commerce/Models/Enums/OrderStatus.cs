namespace E_commerce.Models.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,  // 🔹 New status for orders being prepared
        Shipped = 3,
        Delivered = 4,
        Canceled = 5
    }


}
