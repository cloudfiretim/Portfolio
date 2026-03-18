// Illustrates a hybrid persistence strategy where modules can load data
// from JSON while also leveraging SQLite for structured storage and queries.
// This allows flexible data evolution without sacrificing performance.

using System;
using System.Text.Json;
using SQLite;

public class JournalPersistenceCoordinator
{
    private readonly SQLiteConnection _connection;

    public JournalPersistenceCoordinator(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public void InitializeModule(IJnlModule module)
    {
        // Ensure required tables exist
        module.IntializeDatabase(_connection);
    }

    public void LoadModule(IJnlModule module, JsonElement jsonData)
    {
        // Combine JSON-based state with database-backed storage
        module.LoadFromJson(jsonData, _connection);

        // Allow module to rebuild internal state
        module.OnLoaded();
    }
}