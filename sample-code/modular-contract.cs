// Defines a modular plugin system for journal features.
// Each module encapsulates its own data storage, UI rendering,
// and lifecycle behavior, enabling extensibility without modifying core logic.

using System;
using System.Collections.Generic;
using System.Text.Json;
using SQLite;

public interface IJnlModule
{
    // Unique identifier used for registration and lookup
    string ModuleKey { get; }

    // Visual identity used in UI
    Windows.UI.Color ModuleColor { get; set; }

    // Lifecycle hook after loading
    void OnLoaded();

    // Persistence (JSON + SQLite hybrid support)
    void LoadFromJson(JsonElement element, SQLiteConnection connection);
    void IntializeDatabase(SQLiteConnection connection);

    // Data interaction
    void SetContents(DateTime date, object obj);
    object GetContents(DateTime date);

    // UI rendering
    object? GetJournalElement(DateTime date);
    object? GetTempElement(DateTime date, object data);

    // Widget system
    IEnumerable<string> GetAvailableWidgets();
    object? GetWidget(string widgetKey);

    // Data availability
    bool HasDataForDate(DateTime date);
    IEnumerable<DateTime> GetDatesWithData();
}
