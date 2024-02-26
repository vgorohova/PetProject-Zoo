namespace Zoo.Data;

public class ZooService
{
    private readonly ZooContext _context;
    public ZooService(ZooContext context)
    {
        _context = context;
    }
}