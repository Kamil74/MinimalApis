using FluentValidation;
using Library.Api.Models;

namespace Library.Api.Validetors;

public class Bookvalidator : AbstractValidator<Book>

{
    public Bookvalidator()
    {
        RuleFor(book => book.Isbn)
            .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")
            .WithMessage("Value was not a valid ISBN-13");
        RuleFor(book => book.Title).NotEmpty();
        RuleFor(book => book.ShortDescription).NotEmpty();
        RuleFor(book => book.PageCount).GreaterThan(0);
        RuleFor(book => book.Author).NotEmpty();
    }
}