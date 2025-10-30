using Microsoft.EntityFrameworkCore;

namespace Nix.Persistence;

/*// Example usage of UnitOfWork pattern
public class ExampleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Course> _courseRepository;

    // Using separate repositories and UnitOfWork
    public ExampleService(IUnitOfWork unitOfWork, IRepository<User> userRepository, IRepository<Course> courseRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public async Task CreateUserWithCourseAsync(User user, Course course)
    {
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await _userRepository.AddAsync(user);
            await _courseRepository.AddAsync(course);
            
            // SaveChanges будет вызван UoW внутри обёртки
        });
    }
}

// Example usage of GenericUnitOfWork pattern
public class ExampleServiceWithGenericUnitOfWork
{
    private readonly GenericUnitOfWork<UserDbContext> _unitOfWork;

    public ExampleServiceWithGenericUnitOfWork(GenericUnitOfWork<UserDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUserWithCourseAsync(User user, Course course)
    {
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var userRepository = _unitOfWork.Repository<User>();
            var courseRepository = _unitOfWork.Repository<Course>();
            
            await userRepository.AddAsync(user);
            await courseRepository.AddAsync(course);
            
            // SaveChanges будет вызван UoW внутри обёртки
        });
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var userRepository = _unitOfWork.Repository<User>();
        return await userRepository.GetByIdAsync(userId);
    }
}

// Example entity classes (these would be in your domain/entities)
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
} */
