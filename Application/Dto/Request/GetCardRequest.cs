namespace Application.Dto.Request;

public record GetCardRequest(
    DateTime StartDate,
    DateTime EndDate
    );