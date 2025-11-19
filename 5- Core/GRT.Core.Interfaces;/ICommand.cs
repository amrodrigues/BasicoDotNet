using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace GRT.Core.Interfaces
{
    public interface ICommand : IRequest { }
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
