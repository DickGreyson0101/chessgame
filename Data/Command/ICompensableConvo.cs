using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Command
{
    public interface ICompensableConvo
    {
        void Execute(ICompensableCommand command);

        ICompensableCommand Redo();
        ICompensableCommand Undo();
    }
}
