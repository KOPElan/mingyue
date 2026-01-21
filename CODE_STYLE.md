# MingYue ¡ª Code Style and Best Practices

This document describes coding conventions, patterns, and best practices for the MingYue Blazor project. Follow these guidelines to keep the codebase consistent, secure, maintainable, and testable.

## Scope
- C# code (libraries and services)
- Blazor components and layouts
- Project conventions and common workflows

## Principles
- Prefer clarity over cleverness
- Follow SOLID principles
- Prefer composition over inheritance where appropriate
- Favor small, focused methods and classes
- Use dependency injection for external dependencies and services

## 1. C# Coding Conventions

- Use file-scoped namespaces when appropriate:

```csharp
namespace MingYue.Services;
```

- Naming and style:
  - PascalCase for public types and members
  - camelCase with leading underscore for private fields (`_logger`)
  - Async methods end with `Async`
  - Prefer `var` when the type is obvious from the right-hand side

- Null checks and argument validation:
  - Use `ArgumentNullException.ThrowIfNull(param)`
  - Validate strings with `string.IsNullOrWhiteSpace`

- Exceptions and logging:
  - Do not swallow exceptions; log and rethrow or return meaningful results
  - Prefer specific exception types

## 2. Project Structure

- Recommended folders:
  - `Components/` for Blazor components
  - `Services/` for DI-registered services
  - `Models/` for DTOs and request/response types
  - `Data/` for EF Core contexts
  - `Resources/` for localization

- Component styles: use `Component.razor.css` next to the component for scoped styles

## 3. Blazor Component Guidelines

- Keep components small and focused; split large components into child components
- Use `[Parameter]` for inputs and `EventCallback` for outputs
- Business logic belongs in services, components orchestrate UI and services
- Lifecycle:
  - Use `OnInitializedAsync` to load data
  - Avoid heavy IO in `OnAfterRenderAsync` unless required

Example:

```csharp
protected override async Task OnInitializedAsync()
{
    _data = await _service.GetAsync();
}
```

## 4. Dependency Injection

- Register services in `Program.cs` with appropriate lifetime:
  - `AddSingleton` for stateless singletons
  - `AddScoped` for per-circuit services (Blazor Server)
  - `AddTransient` for short-lived services
- Prefer constructor injection

## 5. Logging

- Use `ILogger<T>` and structured logging:

```csharp
_logger.LogWarning("Failed to add share {ShareName} for path {Path}", shareName, path);
```

- Avoid logging sensitive data; choose appropriate log levels

## 6. Localization and Messages

- Centralize user-facing strings in resources (`.resx`) or a `Resources` folder
- Use `IStringLocalizer<T>` in services and components for localizable strings

## 7. Result Objects and Error Handling

- Prefer typed result objects instead of string messages for programmatic responses

Example `OperationResult`:

```csharp
public enum ResultCode { Success, NotFound, Conflict, ValidationError, Forbidden }

public class OperationResult
{
    public bool Success { get; init; }
    public ResultCode Code { get; init; }
    public string Message { get; init; } = string.Empty;
}
```

## 8. Security

- Validate and sanitize inputs, especially when calling shell commands or writing files
- Principle of least privilege: do not run as root unless necessary
- Store secrets in configuration or secret stores

## 9. Testing

- Add unit tests for services; use bUnit for component tests
- Mock external dependencies via DI

## 10. Performance

- Use async IO end-to-end; avoid blocking calls
- Stream large payloads rather than loading into memory

## 11. CSS and Layout

- Use scoped component CSS (`.razor.css`) for component-specific styles
- For full-height layouts ensure `html, body, #app` are 100% height and use flexbox for content expansion

## 12. Pre-commit Checklist

- Build solution
- Run tests
- Run code formatter
- Ensure no secrets in commits

---

Follow this document when contributing. If a rule conflicts with an existing strong project convention, prefer the project's conventions and open a discussion.
