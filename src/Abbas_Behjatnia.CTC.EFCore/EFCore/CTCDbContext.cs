
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Abbas_Behjatnia.CTC.EFCore;

public class CTCDbContext : BaseDbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }
}