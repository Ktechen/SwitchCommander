using Microsoft.EntityFrameworkCore;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserDto> Users { get; set; }
}