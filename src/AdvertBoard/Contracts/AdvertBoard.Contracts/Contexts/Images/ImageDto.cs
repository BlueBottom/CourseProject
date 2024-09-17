﻿namespace AdvertBoard.Contracts.Contexts.Images;

/// <summary>
/// Модель изображения.
/// </summary>
public class ImageDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; } = null!;
}