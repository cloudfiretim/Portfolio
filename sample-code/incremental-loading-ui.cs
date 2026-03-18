// Demonstrates scroll-driven incremental loading of journal entries.
// Dynamically inserts UI elements based on user navigation direction,
// improving performance by avoiding full dataset rendering.

using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

public class IncrementalJournalLoader
{
    private readonly JournalService _journalService;
    private readonly StackPanel _container;

    private DateTime _currentTopDate;
    private DateTime _currentBottomDate;

    public IncrementalJournalLoader(JournalService journalService, StackPanel container)
    {
        _journalService = journalService;
        _container = container;
    }

    public async Task LoadPreviousAsync()
    {
        var entry = _journalService.GetAdjacentEntry(_currentTopDate, previous: true);

        if (entry == null)
            return;

        var element = CreateDayBlock(entry);

        _container.Children.Insert(0, element);
        _currentTopDate = entry.Date;
    }

    public async Task LoadNextAsync()
    {
        var entry = _journalService.GetAdjacentEntry(_currentBottomDate, previous: false);

        if (entry == null)
            return;

        var element = CreateDayBlock(entry);

        _container.Children.Add(element);
        _currentBottomDate = entry.Date;
    }

    private UIElement CreateDayBlock(JournalEntry entry)
    {
        // Encapsulates transformation from data → UI
        return new DayBlock(entry);
    }
}