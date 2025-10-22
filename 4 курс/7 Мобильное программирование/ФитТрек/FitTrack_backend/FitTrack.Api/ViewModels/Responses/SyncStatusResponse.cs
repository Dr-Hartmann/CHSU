

/// <summary>
/// Статус синхронизации сервера
/// </summary>
public record SyncStatusResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Временная метка сервера
    /// </summary>
    public long ServerTimestamp { get; init; }

    /// <summary>
    /// Статус (например, ready)
    /// </summary>
    public string Status { get; init; } = "ready";
}
