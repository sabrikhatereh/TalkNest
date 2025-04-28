using AutoFixture;
using TalkNest.Infrastructure.Persistence.DbContexts;

namespace TalkNest.IntegrationTests.Setup;

[Collection(nameof(DatabaseTestCollection))]
public abstract class DatabaseTest : IAsyncLifetime
{
    private Func<Task> _resetDatabase;
    protected readonly TalkNestWriteDbContext Db;
    protected readonly Fixture Fixture;

    public DatabaseTest(TalkNestHostFixture talkNestHost)
    {
        _resetDatabase = talkNestHost.ResetDatabase;
        Db = talkNestHost.Db;
        Fixture = new Fixture();
        //Fixture.Customize(new NoCircularReferencesCustomization());
        //Fixture.Customize(new IgnoreVirtualMembersCustomization());
    }

    public async Task Insert<T>(T entity) where T : class
    {
        await Db.AddAsync(entity);
        await Db.SaveChangesAsync();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}
