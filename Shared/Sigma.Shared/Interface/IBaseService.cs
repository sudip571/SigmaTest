using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Interface;

public interface IBaseService { }
public interface ITransientService : IBaseService { }
public interface IScopedService : IBaseService { }
public interface ISingletonService : IBaseService { }