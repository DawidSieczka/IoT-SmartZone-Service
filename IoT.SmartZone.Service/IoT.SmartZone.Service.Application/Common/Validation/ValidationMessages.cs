using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Application.Common.Validation;
public static class ValidationMessages
{
    public static string MaximumLengthExceeded(string propName, int maxLength) => $"{propName} length exceeded. Accepted max length: {maxLength}.";

    public static string EmptyValueNotAllowed(string propName) => $"{propName} value can not be empty.";

}
