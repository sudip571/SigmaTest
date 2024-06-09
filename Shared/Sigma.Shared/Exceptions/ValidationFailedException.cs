using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Exceptions;


public class ValidationFailedException : ApplicationException
{   
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    public ValidationFailedException(IReadOnlyDictionary<string, string[]> errorsDictionary) => ErrorsDictionary = errorsDictionary;
}
