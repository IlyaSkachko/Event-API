using Microsoft.EntityFrameworkCore;
using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories;

public class RepositoryTest
{
    private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task InsertAsync_Should_Add_Entity_To_Database()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);
        var category = new Category { Id = 1, Name = "Test Category" };

        await repository.InsertAsync(category, CancellationToken.None);
        var savedCategory = await context.Categories.FirstOrDefaultAsync();

        Assert.NotNull(savedCategory);
        Assert.Equal("Test Category", savedCategory.Name);
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Entity_From_Database()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);
        var category = new Category { Id = 1, Name = "Test Category" };
        context.Categories.Add(category);

        await context.SaveChangesAsync();

        await repository.DeleteAsync(category, CancellationToken.None);

        var savedCategory = await context.Categories.FirstOrDefaultAsync();

        Assert.Null(savedCategory);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Entities()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);

        context.Categories.AddRange(new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
        });

        await context.SaveChangesAsync();

        var categories = await repository.GetAllAsync(CancellationToken.None);

        Assert.Equal(2, categories.Count());
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Entity_By_Id()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);
        var category = new Category { Id = 1, Name = "Test Category" };

        context.Categories.Add(category);

        await context.SaveChangesAsync();

        var foundCategory = await repository.GetByIdAsync(1, CancellationToken.None);

        Assert.NotNull(foundCategory);
        Assert.Equal("Test Category", foundCategory.Name);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Entity_In_Database()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);
        var category = new Category { Id = 1, Name = "Test Category" };

        context.Categories.Add(category);

        await context.SaveChangesAsync();

        category.Name = "Updated Category";

        await repository.UpdateAsync(category, CancellationToken.None);

        var updatedCategory = await context.Categories.FirstOrDefaultAsync(c => c.Id == 1);

        Assert.NotNull(updatedCategory);
        Assert.Equal("Updated Category", updatedCategory.Name);
    }

    [Fact]
    public async Task GetAllAsync_With_Pagination_Should_Return_Paginated_Entities()
    {
        var options = GetDbContextOptions();
        using var context = new ApplicationDbContext(options);
        var repository = new CategoryRepository(context);

        context.Categories.AddRange(new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" }
        });

        await context.SaveChangesAsync();

        var paginatedCategories = await repository.GetAllAsync(1, 2, CancellationToken.None);

        Assert.Equal(2, paginatedCategories.Count());
        Assert.Equal("Category 1", paginatedCategories.First().Name);
        Assert.Equal("Category 2", paginatedCategories.Last().Name);
    }
}
