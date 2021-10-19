using Odontology.Business.Infrastructure.Enums;

namespace Odontology.Web.ViewModels
{
    public class EntityCreateViewModel<TViewModel> where TViewModel : class
    {
        public TViewModel EntityViewModel { get; set; }

        public ActionTypeEnum ActionType { get; set; }
    }
}
