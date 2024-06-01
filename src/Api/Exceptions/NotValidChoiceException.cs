using System;

namespace Api.Exceptions;

public sealed class NotValidChoiceException(string choice) : Exception($"Choice '{choice}' is not valid");