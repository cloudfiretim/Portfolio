
public object? GetModuleContents<T>(DateTime date) where T : IJnlModule, new()
{
    var module = GetOrCreateModule<T>();

    if (!module.HasDataForDate(date))
        return null;

    return module.GetContents(date);
}

private T GetOrCreateModule<T>() where T : IJnlModule, new()
{
    var key = new T().ModuleKey;

    if (!_modules.TryGetValue(key, out var module))
    {
        module = new T();
        _modules[key] = module;
    }

    return (T)module;
}