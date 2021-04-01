using System.Collections.Generic;
using System.Linq;

namespace TjakraProject.Core.Application.Command
{
    public class BaseCommandResult<TData> : BaseCommandResult
    {
        #region Properties

        public TData Data { get; set; }

        #endregion
    }

    public class BaseCommandResult
    {
        #region Fields

        private readonly List<string> _errorMessages = new List<string>();
        
        private readonly List<string> _successMessages = new List<string>();

        #endregion
        
        #region Properties

        public bool IsSuccess => !_errorMessages.Any();

        public List<string> ErrorMessages => _errorMessages ?? new List<string>();

        public List<string> SuccessMessages => _successMessages ?? new List<string>();

        #endregion

        #region Public Methods

        public void AddErrorMessage(string message)
        {
            _errorMessages.Add(message);
        }

        public void AddErrorMessages(IEnumerable<string> messages)
        {
            _errorMessages.AddRange(messages);
        }

        public void AddSuccessMessage(string message)
        {
            _successMessages.Add(message);
        }

        public void AddSuccessMessages(IEnumerable<string> messages)
        {
            _successMessages.AddRange(messages);
        }

        #endregion
    }
}