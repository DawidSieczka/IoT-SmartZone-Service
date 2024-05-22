using FluentValidation;
using MediatR;

namespace IoT.SmartZone.Service.Application;

/// <summary>
/// Dumb object for MediatR registration.
/// </summary>
public class DumbRequest : IRequest
{

}

public class DumbRequestValidator : AbstractValidator<DumbRequest>
{

}