using FluentValidation;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Types;
using Tools.ConsoleApp;

try
{
    var input = new ToDoItem
    {
        Title = "title",
        Description = "description",
        UpdatedAt = DateTimeOffset.Now
    };

    var result = new ToDoItemValidator().Validate(input);

    Console.WriteLine(result.NormalizeErrors().Serialize(x => x.WriteIndented = true));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

namespace Tools.ConsoleApp
{
    public sealed class ToDoItem : IEntity
    {
        public required string Title { get; init; }
        public required string Description { get; init; }

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; init; }
        public Guid Id { get; init; } = GuidV7.Create();
    }

    public sealed class ToDoItemValidator : AbstractValidator<ToDoItem>
    {
        public ToDoItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.UpdatedAt)
                .AfterDate(x => x.CreatedAt)
                .NotBeInTheFuture();

            RuleFor(x => x.CreatedAt)
                .BeforeDate(x => x.UpdatedAt)
                .NotBeInTheFuture();
        }
    }
}